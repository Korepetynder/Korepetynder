using Korepetynder.Contracts.Requests.User;
using Korepetynder.Contracts.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Services.Users
{
    public interface IUsersService
    {
        Task<UserResponse> InitializeUser(UserCreationRequest request);
        Task<UserResponse> GetUser();
    }
}
