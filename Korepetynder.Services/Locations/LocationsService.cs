using Korepetynder.Contracts.Requests.Locations;
using Korepetynder.Contracts.Responses.Locations;
using Korepetynder.Data;
using Korepetynder.Data.DbModels;
using Korepetynder.Services.Models;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace Korepetynder.Services.Locations
{
    internal class LocationsService : ILocationsService
    {
        private readonly KorepetynderDbContext _korepetynderDbContext;
        private readonly ISieveProcessor _sieveProcessor;

        public LocationsService(KorepetynderDbContext korepetynderDbContext, ISieveProcessor sieveProcessor)
        {
            _korepetynderDbContext = korepetynderDbContext;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<LocationResponse> AddLocation(LocationRequest locationRequest)
        {
            var locationExists = await _korepetynderDbContext.Locations
                .AnyAsync(location => location.Name == locationRequest.Name);

            if (locationExists)
            {
                throw new InvalidOperationException("Location with name " + locationRequest.Name + " already exists");
            }

            var location = new Location(locationRequest.Name);

            if (locationRequest.ParentLocationId != null)
            {
                var parent = await _korepetynderDbContext.Locations
                    .Where(location => location.Id == locationRequest.ParentLocationId)
                    .SingleAsync();
                location.ParentLocation = parent;
            }
            _korepetynderDbContext.Locations.Add(location);
            await _korepetynderDbContext.SaveChangesAsync();

            return new LocationResponse(location.Id, location.Name, location.ParentLocationId);
        }

        public async Task<PagedData<LocationResponse>> GetLocations(SieveModel sieveModel)
        {
            var locations = _korepetynderDbContext.Locations
                .Where(location => location.WasAccepted)
                .OrderBy(location => location.Name)
                .AsNoTracking();

            locations = _sieveProcessor.Apply(sieveModel, locations, applyPagination: false);

            var count = await locations.CountAsync();

            locations = _sieveProcessor.Apply(sieveModel, locations, applyFiltering: false, applySorting: false);

            return new PagedData<LocationResponse>(count, await locations
                .Where(location => location.ParentLocationId == null)
                .Include(location => location.Sublocations)
                .Select(location => new LocationResponse(location))
                .ToListAsync());
        }

        public async Task<LocationResponse?> GetLocation(int id) =>
            await _korepetynderDbContext.Locations
                .AsNoTracking()
                .Where(location => location.Id == id)
                .Select(location => new LocationResponse(location.Id, location.Name, location.ParentLocationId))
                .SingleOrDefaultAsync();
        public async Task<PagedData<LocationResponse>> GetNewLocations(SieveModel sieveModel)
        {
            //TODO assert user is admin
            var locations = _korepetynderDbContext.Locations
                   .Where(location => !location.WasAccepted)
                   .OrderBy(location => location.Name)
                   .AsQueryable();

            locations = _sieveProcessor.Apply(sieveModel, locations, applyPagination: false);

            var count = await locations.CountAsync();

            locations = _sieveProcessor.Apply(sieveModel, locations, applyFiltering: false, applySorting: false);

            return new PagedData<LocationResponse>(count, await locations
                .Select(location => new LocationResponse(location))
                .ToListAsync());
        }
        public async Task<LocationResponse> AcceptLocation(int id)
        {
            //TODO assert user is admin
            var location = await _korepetynderDbContext.Locations
                .Where(location => location.Id == id).SingleAsync();

            if (location.WasAccepted)
            {
                throw new InvalidOperationException("Location with id " + id + " was already accepted");
            }
            location.WasAccepted = true;
            await _korepetynderDbContext.SaveChangesAsync();

            return new LocationResponse(location);
        }

        public async Task DeleteLocation(int id)
        {
            //TODO assert user is admin
            var location = await _korepetynderDbContext.Locations
                .Where(location => location.Id == id)
                .SingleAsync();
            _korepetynderDbContext.Remove(location);
            await _korepetynderDbContext.SaveChangesAsync();
        }
    }
}
