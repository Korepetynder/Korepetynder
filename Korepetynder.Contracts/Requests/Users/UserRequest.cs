using System.ComponentModel.DataAnnotations;

namespace Korepetynder.Contracts.Requests.Users
{
    public class UserRequest
    {

        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public UserRequest(string firstName, string lastName, string email, DateTime birthDate, string? phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
        }
    }
}
