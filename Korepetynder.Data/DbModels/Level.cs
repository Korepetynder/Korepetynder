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

        public ICollection<StudentLesson> StudentLessons { get; set; } = new List<StudentLesson>();
        public ICollection<TeacherLesson> TeacherLessons { get; set; } = new List<TeacherLesson>();

        public Level(string name, int weight)
        {
            Name = name;
            Weight = weight;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj is Level other && other.Id == Id)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
