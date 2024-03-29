namespace Korepetynder.Contracts.Responses.Users
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }

        public UserResponse(Guid id, string firstName, string lastName, string? phoneNumber, string email, DateTime birthDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            BirthDate = birthDate;
        }
    }
}
