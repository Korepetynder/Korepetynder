namespace Korepetynder.Contracts.Responses.Users
{
    public class UserRolesResponse
    {
        public bool IsStudent { get; set; }
        public bool IsTutor { get; set; }

        public UserRolesResponse(bool isStudent, bool isTutor)
        {
            IsStudent = isStudent;
            IsTutor = isTutor;
        }
    }
}
