using System.ComponentModel.DataAnnotations;

namespace Korepetynder.Contracts.Requests.Locations
{
    public class LocationRequest
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public int? ParentLocationId { get; set; }

        public LocationRequest(string name)
        {
            Name = name;
        }
    }
}

