using Korepetynder.Contracts.Requests.User;
using Korepetynder.Contracts.Responses.User;

namespace Korepetynder.Services.Users
{
    public interface IUserService
    {
        Task<UserResponse> InitializeUser(UserRequest request);
        Task<UserResponse> UpdateUser(UserRequest request);
        Task<UserResponse> GetUser();
    }
}
