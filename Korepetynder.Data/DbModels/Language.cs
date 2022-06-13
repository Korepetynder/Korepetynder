using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Korepetynder.Data.DbModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class Language
    {
        public bool WasAccepted { get; set; }
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        public ICollection<StudentLesson> StudentLessons { get; set; } = new List<StudentLesson>();
        public ICollection<TutorLesson> TutorLessons { get; set; } = new List<TutorLesson>();

        public Language(string name)
        {
            Name = name;
            WasAccepted = false;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj is Language other && other.Id == Id)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
