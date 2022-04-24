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
        public string? TelephoneNumber { get; set; }
        public int Age { get; set; }
        public UserRequest(string firstName, string lastName, string email, int age, string? telephoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            TelephoneNumber = telephoneNumber;
            Age = age;
        }
    }
}
