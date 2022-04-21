namespace Korepetynder.Data.DbModels
{
    public class Student
    {
        public int Id { get; set; }

        public int PreferredCostMinimum { get; set; }
        public int PreferredCostMaximum { get; set; }

        public ICollection<Location> PreferredLocations { get; set; } = new List<Location>();
        public ICollection<Lesson> PreferredLessons { get; set; } = new List<Lesson>();
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
