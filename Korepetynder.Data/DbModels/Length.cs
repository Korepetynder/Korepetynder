using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Korepetynder.Data.DbModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class Length
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; } //such as "one time", "regular" and so on

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

        public Length(string name)
        {
            Name = name;
        }
    }
}
