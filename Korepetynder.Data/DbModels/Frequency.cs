using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Korepetynder.Data.DbModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class Frequency
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; } //such as "one time", "regular" and so on
        public int Weight { get; set; } // the higher weight the less frequent it is

        public ICollection<StudentLesson> Lessons { get; set; } = new List<StudentLesson>();

        public Frequency(string name, int weight)
        {
            Name = name;
            Weight = weight;
        }
    }
}
