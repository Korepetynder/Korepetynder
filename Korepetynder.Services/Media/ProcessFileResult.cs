using Microsoft.AspNetCore.WebUtilities;

namespace Korepetynder.Services.Media
{
    public class ProcessFileResult
    {
        public string BlobUrl { get; set; }
        public KeyValueAccumulator FormAccumulator { get; set; }

        public ProcessFileResult(string blobUrl, KeyValueAccumulator formAccumulator)
        {
            BlobUrl = blobUrl;
            FormAccumulator = formAccumulator;
        }
    }
}
