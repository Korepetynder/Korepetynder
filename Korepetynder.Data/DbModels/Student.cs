namespace Korepetynder.Data.DbModels
{
    public class Student
    {
        public int Id { get; set; }
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
