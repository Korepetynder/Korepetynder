using Korepetynder.Contracts.Requests.Users;
using Korepetynder.Contracts.Responses.Users;

namespace Korepetynder.Services.Users
{
    public interface IUserService
    {
        Task<UserResponse> InitializeUser(UserRequest request);
        Task<UserResponse> UpdateUser(UserRequest request);
        Task<UserResponse> GetUser();
        Task<UserRolesResponse> GetUserRoles();
    }
}
