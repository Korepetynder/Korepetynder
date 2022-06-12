using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Korepetynder.Contracts.Requests.Media;
using Korepetynder.Contracts.Responses.Media;
using Korepetynder.Data;
using Korepetynder.Data.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Text;

namespace Korepetynder.Services.Media
{
    internal class MediaService : AdBaseService, IMediaService
    {
        // Get the default form options so that we can use them to set the default 
        // limits for request body data.
        private static readonly FormOptions _defaultFormOptions = new();

        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly long _fileSizeLimit;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly KorepetynderDbContext _korepetynderDbContext;

        public MediaService(IConfiguration configuration,
            BlobServiceClient blobServiceClient,
            IHttpContextAccessor httpContextAccessor,
            KorepetynderDbContext korepetynderDbContext)
            : base(httpContextAccessor)
        {
            _fileSizeLimit = configuration.GetValue<long>("FileSizeLimit");
            _blobServiceClient = blobServiceClient;
            _korepetynderDbContext = korepetynderDbContext;
        }

        public async Task<ProcessFileResult?> ProcessFile(HttpRequest httpRequest, ModelStateDictionary modelState)
        {
            if (httpRequest.ContentType is null || !MultipartRequestHelper.IsMultipartContentType(httpRequest.ContentType))
            {
                modelState.AddModelError("File",
                    $"The request couldn't be processed (Error 1).");

                return null;
            }

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(httpRequest.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, httpRequest.Body);
            var section = await reader.ReadNextSectionAsync();

            var formAccumulator = new KeyValueAccumulator();
            string blobUri = "";

            while (section is not null)
            {
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(
                        section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    // This check assumes that there's a file
                    // present without form data. If form data
                    // is present, this method immediately fails
                    // and returns the model error.
                    if (MultipartRequestHelper
                        .HasFormDataContentDisposition(contentDisposition!))
                    {
                        // Don't limit the key name length because the 
                        // multipart headers length limit is already in effect.
                        var key = HeaderUtilities
                            .RemoveQuotes(contentDisposition!.Name).Value;
                        var encoding = GetEncoding(section);

                        if (encoding == null)
                        {
                            modelState.AddModelError("File",
                                $"The request couldn't be processed (Error 2).");
                            // Log error

                            return null;
                        }

                        using var streamReader = new StreamReader(
                            section.Body,
                            encoding,
                            detectEncodingFromByteOrderMarks: true,
                            bufferSize: 1024,
                            leaveOpen: true);
                        // The value length limit is enforced by 
                        // MultipartBodyLengthLimit
                        var value = await streamReader.ReadToEndAsync();

                        if (string.Equals(value, "undefined",
                            StringComparison.OrdinalIgnoreCase))
                        {
                            value = string.Empty;
                        }

                        formAccumulator.Append(key, value);

                        if (formAccumulator.ValueCount >
                            _defaultFormOptions.ValueCountLimit)
                        {
                            // Form key count limit of 
                            // _defaultFormOptions.ValueCountLimit 
                            // is exceeded.
                            modelState.AddModelError("File",
                                $"The request couldn't be processed (Error 3).");

                            return null;
                        }
                    }
                    else if (MultipartRequestHelper
                        .HasFileContentDisposition(contentDisposition!))
                    {
                        // Don't trust the file name sent by the client. To display
                        // the file name, HTML-encode the value.
                        var trustedFileNameForDisplay = WebUtility.HtmlEncode(
                                contentDisposition!.FileName.Value);

                        FileHelpers.ProcessStreamedFile(
                            section, contentDisposition, modelState,
                            _permittedExtensions, _fileSizeLimit);

                        if (!modelState.IsValid)
                        {
                            return null;
                        }

                        var fileName = Guid.NewGuid() + Path.GetExtension(trustedFileNameForDisplay).ToLowerInvariant();
                        var containerClient = _blobServiceClient.GetBlobContainerClient("media");
                        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
                        var blobClient = containerClient.GetBlobClient(fileName);

                        section.Body.Position = 0;
                        await blobClient.UploadAsync(section.Body);
                        blobUri = blobClient.Uri.AbsoluteUri;
                    }
                }

                // Drain any remaining section body that hasn't been consumed and
                // read the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }

            return new ProcessFileResult(blobUri, formAccumulator);
        }

        private static Encoding GetEncoding(MultipartSection section)
        {
            var hasMediaTypeHeader =
                MediaTypeHeaderValue.TryParse(section.ContentType, out var mediaType);

            // UTF-7 is insecure and shouldn't be honored. UTF-8 succeeds in 
            // most cases.
#pragma warning disable SYSLIB0001 // Type or member is obsolete
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType!.Encoding))
            {
                return Encoding.UTF8;
            }
#pragma warning restore SYSLIB0001 // Type or member is obsolete

            return mediaType.Encoding!;
        }

        public async Task<MultimediaFileResponse> AddMultimediaFile(MultimediaFileRequest multimediaFileRequest, string url)
        {
            Guid currentId = GetCurrentUserId();

            var tutorLessons = await _korepetynderDbContext.TutorLessons
                .Where(tutorLesson => multimediaFileRequest.TutorLessons.Contains(tutorLesson.Id))
                .Where(tutorLesson => tutorLesson.TutorId == currentId)
                .ToListAsync();
            if (tutorLessons.Count != multimediaFileRequest.TutorLessons.Count())
            {
                throw new ArgumentException("At least one provided tutor lesson does not exist");
            }

            var multimediaFile = new MultimediaFile(url, currentId)
            {
                TutorLessons = tutorLessons
            };
            _korepetynderDbContext.MultimediaFiles.Add(multimediaFile);
            await _korepetynderDbContext.SaveChangesAsync();

            return new MultimediaFileResponse(multimediaFile.Id, multimediaFile.Url,
                multimediaFile.TutorLessons.Select(multimediaFile => multimediaFile.Id));
        }

        public async Task<IEnumerable<MultimediaFileResponse>> GetMultimediaFiles()
        {
            Guid currentId = GetCurrentUserId();

            var isTutor = await _korepetynderDbContext.Tutors
                .AnyAsync(tutor => tutor.UserId == currentId);
            if (!isTutor)
            {
                throw new InvalidOperationException("User with id: " + currentId + " is not a tutor");
            }

            return await _korepetynderDbContext.MultimediaFiles
                .Where(multimediaFile => multimediaFile.TutorId == currentId)
                .Select(multimediaFile => new MultimediaFileResponse(multimediaFile.Id, multimediaFile.Url,
                    multimediaFile.TutorLessons.Select(tutorLesson => tutorLesson.Id)))
                .ToListAsync();
        }

        public async Task DeleteMultimediaFile(int id)
        {
            Guid currentId = GetCurrentUserId();

            var isTutor = await _korepetynderDbContext.Tutors
                .AnyAsync(tutor => tutor.UserId == currentId);
            if (!isTutor)
            {
                throw new InvalidOperationException("User with id: " + currentId + " is not a tutor");
            }

            var multimediaFile = await _korepetynderDbContext.MultimediaFiles
                .SingleAsync(multimediaFile => multimediaFile.Id == id);
            if (currentId != multimediaFile.TutorId)
            {
                throw new ArgumentException("Multimedia file does not belong to tutor");
            }

            _korepetynderDbContext.MultimediaFiles.Remove(multimediaFile);
            await _korepetynderDbContext.SaveChangesAsync();
        }
    }
}
