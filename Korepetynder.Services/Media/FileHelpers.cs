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

        public static void ProcessStreamedFile(MultipartSection section,
            ContentDispositionHeaderValue contentDisposition,
            ModelStateDictionary modelState, string[] permittedExtensions, long sizeLimit)
        {
            try
            {
                // Check if the file is empty or exceeds the size limit.
                // TODO: it doesn't work (length is always zero)
                // if (section.Body.Length == 0)
                // {
                //     modelState.AddModelError("File", "The file is empty.");
                // }
                // else if (section.Body.Length > sizeLimit)
                // {
                //     var megabyteSizeLimit = sizeLimit / 1048576;
                //     modelState.AddModelError("File",
                //     $"The file exceeds {megabyteSizeLimit:N1} MB.");
                // }
                if (!IsValidFileExtensionAndSignature(
                    contentDisposition.FileName.Value, section.Body,
                    permittedExtensions))
                {
                    modelState.AddModelError("File",
                        "The file type isn't permitted or the file's " +
                        "signature doesn't match the file's extension.");
                }
            }
            catch (Exception ex)
            {
                modelState.AddModelError("File",
                    $"The upload failed. Error: {ex.HResult}");
            }
        }

        private static bool IsValidFileExtensionAndSignature(string fileName, Stream data, string[] permittedExtensions)
        {
            if (string.IsNullOrEmpty(fileName))
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

            var signatures = _fileSignature[ext];
            byte[] buffer = new byte[signatures.Max(m => m.Length)];
            data.ReadAsync(buffer, 0, signatures.Max(m => m.Length));
            data.Position = 0;

            return signatures.Any(signature =>
                buffer.Take(signature.Length).SequenceEqual(signature));
        }
    }
}
