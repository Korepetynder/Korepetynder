namespace Korepetynder.Data.DbModels
{
    public class Student
    {
        public int Id { get; set; }

        public User User { get; set; } = null!;
        public ICollection<Location> PreferredLocations { get; set; } = new List<Location>();
        public ICollection<StudentLesson> PreferredLessons { get; set; } = new List<StudentLesson>();
        public ICollection<Teacher> DiscardedTeacher { get; set; } = new List<Teacher>();
    }
}
