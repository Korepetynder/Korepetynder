using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Korepetynder.Data.DbModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class Level
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; } //such as "elementary", "high school" etc.

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

        public Level(string name)
        {
            Name = name;
        }
    }
}
