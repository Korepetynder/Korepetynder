using Korepetynder.Contracts.Requests.Levels;
using Korepetynder.Contracts.Responses.Levels;
using Korepetynder.Data;
using Korepetynder.Data.DbModels;
using Korepetynder.Services.Exceptions;
using Korepetynder.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace Korepetynder.Services.Levels
{
    internal class LevelsService : AdBaseService, ILevelsService
    {
        private readonly KorepetynderDbContext _korepetynderDbContext;
        private readonly ISieveProcessor _sieveProcessor;

        public LevelsService(KorepetynderDbContext korepetynderDbContext, ISieveProcessor sieveProcessor, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _korepetynderDbContext = korepetynderDbContext;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<LevelResponse> AddLevel(LevelRequest levelRequest)
        {
            var levelExists = await _korepetynderDbContext.Levels
                .AnyAsync(level => level.Name == levelRequest.Name);

            if (levelExists)
            {
                throw new InvalidOperationException("Level with name " + levelRequest.Name + " already exists");
            }

            var level = new Level(levelRequest.Name, levelRequest.Weight);
            _korepetynderDbContext.Levels.Add(level);
            await _korepetynderDbContext.SaveChangesAsync();

            return new LevelResponse(level.Id, level.Name);
        }

        public async Task<PagedData<LevelResponse>> GetLevels(SieveModel sieveModel)
        {
            var levels = _korepetynderDbContext.Levels
                .Where(level => level.WasAccepted)
                .OrderBy(level => level.Weight)
                .AsQueryable();

            levels = _sieveProcessor.Apply(sieveModel, levels, applyPagination: false);

            var count = await levels.CountAsync();

            levels = _sieveProcessor.Apply(sieveModel, levels, applyFiltering: false, applySorting: false);

            return new PagedData<LevelResponse>(count, await levels
                .Select(level => new LevelResponse(level.Id, level.Name))
                .ToListAsync());
        }

        public async Task<LevelResponse?> GetLevel(int id) =>
            await _korepetynderDbContext.Levels
                .Where(level => level.Id == id)
                .Select(level => new LevelResponse(level.Id, level.Name))
                .SingleOrDefaultAsync();

        public async Task<PagedData<LevelResponse>> GetNewLevels(SieveModel sieveModel)
        {
            if (!await IsAdmin())
            {
                throw new PermissionDeniedException();
            }

            var levels = _korepetynderDbContext.Levels
                   .Where(level => !level.WasAccepted)
                   .OrderBy(level => level.Weight)
                   .AsQueryable();

            levels = _sieveProcessor.Apply(sieveModel, levels, applyPagination: false);

            var count = await levels.CountAsync();

            levels = _sieveProcessor.Apply(sieveModel, levels, applyFiltering: false, applySorting: false);

            return new PagedData<LevelResponse>(count, await levels
                .Select(level => new LevelResponse(level.Id, level.Name))
                .ToListAsync());
        }
        public async Task<LevelResponse> AcceptLevel(int id, int newWeight)
        {
            if (!await IsAdmin())
            {
                throw new PermissionDeniedException();
            }

            var level = await _korepetynderDbContext.Levels
                .Where(level => level.Id == id).SingleAsync();

            if (level.WasAccepted)
            {
                throw new InvalidOperationException("Level with id " + id + " was already accepted");
            }
            level.WasAccepted = true;
            level.Weight = newWeight;
            await _korepetynderDbContext.SaveChangesAsync();

            return new LevelResponse(level.Id, level.Name);
        }

        public async Task DeleteLevel(int id)
        {
            if (!await IsAdmin())
            {
                throw new PermissionDeniedException();
            }

            var level = await _korepetynderDbContext.Levels
                .Where(level => level.Id == id)
                .SingleAsync();
            _korepetynderDbContext.Remove(level);
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
