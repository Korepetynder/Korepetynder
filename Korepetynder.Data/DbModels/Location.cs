using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Korepetynder.Data.DbModels
{
    public class Location
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        public int? ParentLocationId { get; set; }
        [ForeignKey(nameof(ParentLocationId))]
        public Location? ParentLocation { get; set; }
        public ICollection<Location> SubLocations { get; set; } = new List<Location>();

        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
        public ICollection<Student> Students { get; set; } = new List<Student>();

        public Location(string name)
        {
            Name = name;
        }
    }
}
