namespace Korepetynder.Data.DbModels
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();

        public Subject(string name)
        {
            Name = name;
        }
    }
}
