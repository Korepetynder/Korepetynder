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
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public int? TeacherId { get; set; }
        public int? StudentId { get; set; }

        [ForeignKey(nameof(TeacherId))]
        public Teacher? Teacher { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student? Student { get; set; }

        public User(Guid id, string firstName, string lastName, DateTime birthDate, string email, string? phoneNumber)
        {
            Id = id;
            SetValues(firstName, lastName, birthDate, email, phoneNumber);
        }

        public void SetValues(string firstName, string lastName, DateTime birthDate, string email, string? phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            FullName = firstName + ' ' + lastName;
            BirthDate = birthDate;
            Email = email;
            PhoneNumber = phoneNumber;
        }

    }
}
