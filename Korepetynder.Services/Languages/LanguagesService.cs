using Korepetynder.Contracts.Requests.Languages;
using Korepetynder.Contracts.Responses.Languages;
using Korepetynder.Data;
using Korepetynder.Data.DbModels;
using Korepetynder.Services.Exceptions;
using Korepetynder.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace Korepetynder.Services.Languages
{
    internal class LanguagesService : AdBaseService, ILanguagesService
    {
        private readonly KorepetynderDbContext _korepetynderDbContext;
        private readonly ISieveProcessor _sieveProcessor;

        public LanguagesService(KorepetynderDbContext korepetynderDbContext, ISieveProcessor sieveProcessor, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
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
                .Where(language => language.WasAccepted)
                .OrderBy(Language => Language.Name)
                .AsQueryable();

            languages = _sieveProcessor.Apply(sieveModel, languages, applyPagination: false);

            var count = await languages.CountAsync();

            languages = _sieveProcessor.Apply(sieveModel, languages, applyFiltering: false, applySorting: false);

            return new PagedData<LanguageResponse>(count, await languages
                .Select(Language => new LanguageResponse(Language.Id, Language.Name))
                .ToListAsync());
        }

        public async Task<LanguageResponse?> GetLanguage(int id) =>
            await _korepetynderDbContext.Languages
                .Where(x => x.Id == id)
                .Select(language => new LanguageResponse(language.Id, language.Name))
                .SingleOrDefaultAsync();
        public async Task<PagedData<LanguageResponse>> GetNewLanguages(SieveModel sieveModel)
        {
            if (!await IsAdmin())
            {
                throw new PermissionDeniedException();
            }

            var languages = _korepetynderDbContext.Languages
                   .Where(language => !language.WasAccepted)
                   .OrderBy(language => language.Name)
                   .AsQueryable();

            languages = _sieveProcessor.Apply(sieveModel, languages, applyPagination: false);

            var count = await languages.CountAsync();

            languages = _sieveProcessor.Apply(sieveModel, languages, applyFiltering: false, applySorting: false);

            return new PagedData<LanguageResponse>(count, await languages
                .Select(language => new LanguageResponse(language.Id, language.Name))
                .ToListAsync());
        }
        public async Task<LanguageResponse> AcceptLanguage(int id)
        {
            if (!await IsAdmin())
            {
                throw new PermissionDeniedException();
            }

            var language = await _korepetynderDbContext.Languages
                .Where(language => language.Id == id).SingleAsync();

            if (language.WasAccepted)
            {
                throw new InvalidOperationException("Language with id " + id + " was already accepted");
            }
            language.WasAccepted = true;
            await _korepetynderDbContext.SaveChangesAsync();

            return new LanguageResponse(language.Id, language.Name);
        }

        public async Task DeleteLanguage(int id)
        {
            if (!await IsAdmin())
            {
                throw new PermissionDeniedException();
            }

            var language = await _korepetynderDbContext.Languages
                .Where(language => language.Id == id)
                .SingleAsync();
            _korepetynderDbContext.Remove(language);
            await _korepetynderDbContext.SaveChangesAsync();
        }

        private async Task<bool> IsAdmin()
        {
            var id = GetCurrentUserId();

            return await _korepetynderDbContext.Users
                .Where(user => user.Id == id)
                .Select(user => user.IsAdmin)
                .SingleAsync();
        }
    }
}
