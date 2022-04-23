using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Korepetynder.Data.DbModels
{
    public class User
    {
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; }
        [MaxLength(100)]
        public string UserName { get; set; }
        public int Age { get; set; }
        public int? TeacherId { get; set; }
        public int? StudentId { get; set; }

        [ForeignKey(nameof(TeacherId))]
        public Teacher? Teacher { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student? Student { get; set; }

        public User(Guid id, string firstName, string lastName, string userName, int age)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            FullName = firstName + ' ' + lastName;
            UserName = userName;
            Age = age;
        }

    }
}
