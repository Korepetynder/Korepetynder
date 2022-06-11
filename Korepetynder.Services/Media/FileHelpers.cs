using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace Korepetynder.Services.Media
{
    internal static class FileHelpers
    {
        private static readonly Dictionary<string, List<byte[]>> _fileSignature = new()
        {
            { ".gif", new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } } },
            { ".png", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
            { ".jpeg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                }
            },
            { ".jpg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                }
            }
        };

        public static async Task<byte[]> ProcessStreamedFile(MultipartSection section,
            ContentDispositionHeaderValue contentDisposition,
            ModelStateDictionary modelState, string[] permittedExtensions, long sizeLimit)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await section.Body.CopyToAsync(memoryStream);

                    // Check if the file is empty or exceeds the size limit.
                    if (memoryStream.Length == 0)
                    {
                        modelState.AddModelError("File", "The file is empty.");
                    }
                    else if (memoryStream.Length > sizeLimit)
                    {
                        var megabyteSizeLimit = sizeLimit / 1048576;
                        modelState.AddModelError("File",
                        $"The file exceeds {megabyteSizeLimit:N1} MB.");
                    }
                    else if (!IsValidFileExtensionAndSignature(
                        contentDisposition.FileName.Value, memoryStream,
                        permittedExtensions))
                    {
                        modelState.AddModelError("File",
                            "The file type isn't permitted or the file's " +
                            "signature doesn't match the file's extension.");
                    }
                    else
                    {
                        return memoryStream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                modelState.AddModelError("File",
                    $"The upload failed. Error: {ex.HResult}");
            }

            return Array.Empty<byte>();
        }

        private static bool IsValidFileExtensionAndSignature(string fileName, Stream data, string[] permittedExtensions)
        {
            if (string.IsNullOrEmpty(fileName) || data == null || data.Length == 0)
            {
                return false;
            }

            var ext = Path.GetExtension(fileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                return false;
            }

            data.Position = 0;

            // File signature check
            // --------------------
            // With the file signatures provided in the _fileSignature
            // dictionary, the following code tests the input content's
            // file signature.
            using var reader = new BinaryReader(data);

            var signatures = _fileSignature[ext];
            var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

            return signatures.Any(signature =>
                headerBytes.Take(signature.Length).SequenceEqual(signature));
        }
    }
}
