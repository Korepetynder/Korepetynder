namespace Korepetynder.Data.DbModels
{
    public class Student
    {
        public int Id { get; set; }

        public int PreferredCostMinimum { get; set; }
        public int PreferredCostMaximum { get; set; }

        public ICollection<Location> PreferredLocations { get; set; } = new List<Location>();
        public ICollection<StudentLesson> PreferredLessons { get; set; } = new List<StudentLesson>();
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
