using Korepetynder.Contracts.Requests.Locations;
using Korepetynder.Contracts.Responses.Locations;
using Korepetynder.Services.Models;
using Sieve.Models;

namespace Korepetynder.Services.Locations
{
    public interface ILocationsService
    {
        Task<PagedData<LocationResponse>> GetLocations(SieveModel sieveModel);
        Task<LocationResponse?> GetLocation(int id);
        Task<LocationResponse> AddLocation(LocationRequest subjectRequest);
    }
}
