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
        Task<LevelResponse> AddLevel(LevelRequest subjectRequest);
    }
}
