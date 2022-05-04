using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Korepetynder.Data.DbModels
{
    public class Student
    {
        [Key]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
        public ICollection<Location> PreferredLocations { get; set; } = new List<Location>();
        public ICollection<StudentLesson> PreferredLessons { get; set; } = new List<StudentLesson>();
        public ICollection<Tutor> DiscardedTeachers { get; set; } = new List<Tutor>();
        public ICollection<Tutor> FavouriteTeachers { get; set; } = new List<Tutor>();
        public Student(Guid userId)
        {
            UserId = userId;
        }
    }
}
