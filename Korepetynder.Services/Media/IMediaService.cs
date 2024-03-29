using Korepetynder.Contracts.Requests.Media;
using Korepetynder.Contracts.Responses.Media;
using Korepetynder.Data.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Korepetynder.Services.Media
{
    public interface IMediaService
    {
        Task<ProcessFileResult?> ProcessFile(HttpRequest httpRequest, ModelStateDictionary modelState);
        Task<MultimediaFileResponse> AddMultimediaFile(MultimediaFileRequest multimediaFileRequest, string url);
        Task<IEnumerable<MultimediaFileResponse>> GetMultimediaFiles();
        Task DeleteMultimediaFile(int id);
    }
}
