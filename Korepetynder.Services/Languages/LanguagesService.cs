using Korepetynder.Contracts.Requests.Languages;
using Korepetynder.Contracts.Responses.Languages;
using Korepetynder.Data;
using Korepetynder.Data.DbModels;
using Korepetynder.Services.Models;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace Korepetynder.Services.Languages
{
    internal class LanguagesService : ILanguagesService
    {
        private readonly KorepetynderDbContext _korepetynderDbContext;
        private readonly ISieveProcessor _sieveProcessor;

        public LanguagesService(KorepetynderDbContext korepetynderDbContext, ISieveProcessor sieveProcessor)
        {
            _korepetynderDbContext = korepetynderDbContext;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<LanguageResponse> AddLanguage(LanguageRequest languageRequest)
        {
            var languageExists = await _korepetynderDbContext.Languages
                .AnyAsync(language => language.Name == languageRequest.Name);
            if (languageExists)
            {
                throw new InvalidOperationException("Language with name " + languageRequest.Name + " already exists");
            }

            var language = new Language(languageRequest.Name);
            _korepetynderDbContext.Languages.Add(language);
            await _korepetynderDbContext.SaveChangesAsync();

            return new LanguageResponse(language.Id, language.Name);
        }

        public async Task<PagedData<LanguageResponse>> GetLanguages(SieveModel sieveModel)
        {
            var languages = _korepetynderDbContext.Languages
                .OrderBy(Language => Language.Name)
                .AsNoTracking();

            languages = _sieveProcessor.Apply(sieveModel, languages, applyPagination: false);

            var count = await languages.CountAsync();

            languages = _sieveProcessor.Apply(sieveModel, languages, applyFiltering: false, applySorting: false);

            return new PagedData<LanguageResponse>(count, await languages
                .Select(Language => new LanguageResponse(Language.Id, Language.Name))
                .ToListAsync());
        }

        public async Task<LanguageResponse?> GetLanguage(int id) =>
            await _korepetynderDbContext.Languages
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(language => new LanguageResponse(language.Id, language.Name))
                .SingleOrDefaultAsync();
    }
}
