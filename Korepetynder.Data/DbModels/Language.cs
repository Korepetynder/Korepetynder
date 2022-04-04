using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Korepetynder.Data.DbModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class Language
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

        public Language(string name)
        {
            Name = name;
        }

    }
}
