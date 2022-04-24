using Korepetynder.Data.DbModels;
using System.ComponentModel.DataAnnotations;

namespace Korepetynder.Contracts.Responses.Locations
{
    public class LocationResponse
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public IEnumerable<LocationResponse> ChildrenLocations { get; set; }

        public LocationResponse(Location location)
        {
            Id = location.Id;
            Name = location.Name;
            if (location.Sublocations != null)
                ChildrenLocations = location.Sublocations.Select(location => new LocationResponse(location));
            else
                ChildrenLocations = new List<LocationResponse>();
            ParentId = location.ParentLocationId;
        }

        public LocationResponse(int id, string name, int? parentId)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
            ChildrenLocations = new List<LocationResponse>();

        }
    }
}
