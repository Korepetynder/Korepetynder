using Korepetynder.Contracts.Requests.User;
using Korepetynder.Contracts.Responses.User;

namespace Korepetynder.Services.Users
{
    public interface IUserService
    {
        Task<UserResponse> InitializeUser(UserCreationRequest request);
        Task<UserResponse> GetUser();
        Task<UserRolesResponse> GetUserRoles();
    }
}
