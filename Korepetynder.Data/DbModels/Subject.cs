using Microsoft.EntityFrameworkCore;
using Sieve.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Korepetynder.Data.DbModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class Subject
    {
        public bool WasAccepted { get; set; }
        public int Id { get; set; }

        [MaxLength(100)]
        [Sieve(CanFilter = true)]
        public string Name { get; set; }

        public ICollection<StudentLesson> StudentLessons { get; set; } = new List<StudentLesson>();
        public ICollection<TutorLesson> TutorLessons { get; set; } = new List<TutorLesson>();

        public Subject(string name)
        {
            Name = name;
            WasAccepted = false;
        }
    }
}
