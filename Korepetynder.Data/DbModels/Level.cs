using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Korepetynder.Data.DbModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class Level
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; } // such as "elementary", "high school" etc.

        public int Weight { get; set; } // the higher education level it is, the higher the level

        public ICollection<StudentLesson> Lessons { get; set; } = new List<StudentLesson>();

        public Level(string name, int weight)
        {
            Name = name;
            Weight = weight;
        }
    }
}
