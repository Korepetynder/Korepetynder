using Korepetynder.Contracts.Requests.Levels;
using Korepetynder.Contracts.Responses.Levels;
using Korepetynder.Data;
using Korepetynder.Data.DbModels;
using Korepetynder.Services.Models;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace Korepetynder.Services.Levels
{
    internal class LevelsService : ILevelsService
    {
        private readonly KorepetynderDbContext _korepetynderDbContext;
        private readonly ISieveProcessor _sieveProcessor;

        public LevelsService(KorepetynderDbContext korepetynderDbContext, ISieveProcessor sieveProcessor)
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
                .OrderBy(level => level.Weight)
                .AsNoTracking();

            levels = _sieveProcessor.Apply(sieveModel, levels, applyPagination: false);

            var count = await levels.CountAsync();

            levels = _sieveProcessor.Apply(sieveModel, levels, applyFiltering: false, applySorting: false);

            return new PagedData<LevelResponse>(count, await levels
                .Select(level => new LevelResponse(level.Id, level.Name))
                .ToListAsync());
        }

        public async Task<LevelResponse?> GetLevel(int id) =>
            await _korepetynderDbContext.Levels
                .AsNoTracking()
                .Where(level => level.Id == id)
                .OrderBy(level => level.Weight)
                .Select(level => new LevelResponse(level.Id, level.Name))
                .SingleOrDefaultAsync();
    }
}
