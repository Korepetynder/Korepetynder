using System.ComponentModel.DataAnnotations;

namespace Korepetynder.Contracts.Responses.Locations
{
    public class LocationResponse
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public int? ParentLocationId { get; set; }

        public LocationResponse(int id, string name, int? parentId)
        {
            Id = id;
            Name = name;
            ParentLocationId = parentId;
        }
    }
}
