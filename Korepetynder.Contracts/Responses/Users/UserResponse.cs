namespace Korepetynder.Contracts.Responses.Users
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? TelephoneNumber { get; set; }
        public int Age { get; set; }
        public bool IsStudent { get; set; }
        public bool IsTeacher { get; set; }
        public UserResponse(Guid id, string firstName, string lastName, string? telephoneNumber, string email, int age, bool isStudent, bool isTeacher)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            TelephoneNumber = telephoneNumber;
            Email = email;
            Age = age;
            IsStudent = isStudent;
            IsTeacher = isTeacher;
        }
    }
}
