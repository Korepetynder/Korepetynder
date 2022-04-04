namespace Korepetynder.Data.DbModels
{
    public class Student
    {
        public int Id { get; set; }

        public int PreferedCost { get; set; }

        public ICollection<Location> PreferedLocation { get; set; } = new List<Location>();
        public ICollection<LessonData> PreferedLesson { get; set; } = new List<LessonData>();
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
