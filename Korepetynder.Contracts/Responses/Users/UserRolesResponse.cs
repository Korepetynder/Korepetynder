namespace Korepetynder.Contracts.Responses.Users
{
    public class UserRolesResponse
    {
        public bool IsStudent { get; set; }
        public bool IsTutor { get; set; }
        public bool IsAdmin { get; set; }

        public UserRolesResponse(bool isStudent, bool isTutor, bool isAdmin)
        {
            IsStudent = isStudent;
            IsTutor = isTutor;
            IsAdmin = isAdmin;
        }
    }
}
