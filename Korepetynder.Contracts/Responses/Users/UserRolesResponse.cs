namespace Korepetynder.Contracts.Responses.Users
{
    public class UserRolesResponse
    {
        public bool IsStudent { get; set; }
        public bool IsTeacher { get; set; }

        public UserRolesResponse(bool isStudent, bool isTeacher)
        {
            IsStudent = isStudent;
            IsTeacher = isTeacher;
        }
    }
}
