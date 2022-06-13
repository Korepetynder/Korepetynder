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
        Task<PagedData<LanguageResponse>> GetNewLanguages(SieveModel sieveModel);
        Task<LanguageResponse> AcceptLanguage(int id);
        Task DeleteLanguage(int id);
        Task<LanguageResponse> AddLanguage(LanguageRequest subjectRequest);
    }
}
