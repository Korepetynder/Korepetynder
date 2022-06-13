using Korepetynder.Contracts.Requests.Levels;
using Korepetynder.Contracts.Responses.Levels;
using Korepetynder.Services.Models;
using Sieve.Models;

namespace Korepetynder.Services.Levels
{
    public interface ILevelsService
    {
        Task<PagedData<LevelResponse>> GetLevels(SieveModel sieveModel);
        Task<LevelResponse?> GetLevel(int id);
        Task<PagedData<LevelResponse>> GetNewLevels(SieveModel sieveModel);
        Task<LevelResponse> AcceptLevel(int id, int newWeight);
        Task DeleteLevel(int id);
        Task<LevelResponse> AddLevel(LevelRequest subjectRequest);
    }
}
