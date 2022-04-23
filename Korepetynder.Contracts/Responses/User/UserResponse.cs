namespace Korepetynder.Contracts.Responses.User
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public bool IsStudent { get; set; }
        public bool IsTeacher { get; set; }
        public UserResponse(Guid id, string firstName, string lastName, string userName, int age, bool isStudent, bool isTeacher)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Age = age;
            IsStudent = isStudent;
            IsTeacher = isTeacher;
        }
    }
}
