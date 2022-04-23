using Korepetynder.Contracts.Requests.Frequencies;
using Korepetynder.Contracts.Responses.Frequencies;
using Korepetynder.Services.Models;
using Sieve.Models;

namespace Korepetynder.Services.Frequencies
{
    public interface IFrequenciesService
    {
        Task<PagedData<FrequencyResponse>> GetFrequencies(SieveModel sieveModel);
        Task<FrequencyResponse?> GetFrequency(int id);
        Task<FrequencyResponse> AddFrequency(FrequencyRequest subjectRequest);
    }
}
