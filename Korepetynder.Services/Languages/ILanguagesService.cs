using Korepetynder.Contracts.Requests.Languages;
using Korepetynder.Contracts.Responses.Languages;
using Korepetynder.Services.Models;
using Sieve.Models;

namespace Korepetynder.Services.Languages
{
    public interface ILanguagesService
    {
        Task<PagedData<LanguageResponse>> GetLanguages(SieveModel sieveModel);
        Task<LanguageResponse?> GetLanguage(int id);
        Task<LanguageResponse> AddLanguage(LanguageRequest subjectRequest);
    }
}
