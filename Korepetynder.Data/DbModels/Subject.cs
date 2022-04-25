using Microsoft.EntityFrameworkCore;
using Sieve.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Korepetynder.Data.DbModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class Subject
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Sieve(CanFilter = true)]
        public string Name { get; set; }

        public ICollection<StudentLesson> StudentLessons { get; set; } = new List<StudentLesson>();
        public ICollection<TeacherLesson> TeacherLessons { get; set; } = new List<TeacherLesson>();

        public Subject(string name)
        {
            Name = name;
        }
    }
}
