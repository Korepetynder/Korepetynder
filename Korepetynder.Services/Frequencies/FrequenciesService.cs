using Korepetynder.Contracts.Requests.Frequencies;
using Korepetynder.Contracts.Responses.Frequencies;
using Korepetynder.Data;
using Korepetynder.Data.DbModels;
using Korepetynder.Services.Models;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace Korepetynder.Services.Frequencies
{
    internal class FrequenciesService : IFrequenciesService
    {
        private readonly KorepetynderDbContext _korepetynderDbContext;
        private readonly ISieveProcessor _sieveProcessor;

        public FrequenciesService(KorepetynderDbContext korepetynderDbContext, ISieveProcessor sieveProcessor)
        {
            _korepetynderDbContext = korepetynderDbContext;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<FrequencyResponse> AddFrequency(FrequencyRequest frequencyRequest)
        {
            var frequencyExists = await _korepetynderDbContext.Frequencies
                .AnyAsync(frequency => frequency.Name == frequencyRequest.Name);
            if (frequencyExists)
            {
                throw new InvalidOperationException("Frequency with name " + frequencyRequest.Name + " already exists");
            }

            var frequency = new Frequency(frequencyRequest.Name, frequencyRequest.Weight);
            _korepetynderDbContext.Frequencies.Add(frequency);
            await _korepetynderDbContext.SaveChangesAsync();

            return new FrequencyResponse(frequency.Id, frequency.Name);
        }

        public async Task<PagedData<FrequencyResponse>> GetFrequencies(SieveModel sieveModel)
        {
            var frequencies = _korepetynderDbContext.Frequencies
                .OrderBy(frequency => frequency.Weight)
                .AsNoTracking();

            frequencies = _sieveProcessor.Apply(sieveModel, frequencies, applyPagination: false);

            var count = await frequencies.CountAsync();

            frequencies = _sieveProcessor.Apply(sieveModel, frequencies, applyFiltering: false, applySorting: false);

            return new PagedData<FrequencyResponse>(count, await frequencies
                .Select(frequency => new FrequencyResponse(frequency.Id, frequency.Name))
                .ToListAsync());
        }

        public async Task<FrequencyResponse?> GetFrequency(int id) =>
            await _korepetynderDbContext.Frequencies
                .AsNoTracking()
                .Where(frequency => frequency.Id == id)
                .OrderBy(frequency => frequency.Weight)
                .Select(frequency => new FrequencyResponse(frequency.Id, frequency.Name))
                .SingleOrDefaultAsync();
    }
}
