using Korepetynder.Contracts.Requests.Users;
using Korepetynder.Contracts.Responses.Users;
using Korepetynder.Data;
using Korepetynder.Data.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;

namespace Korepetynder.Services.Users
{
    public class UserService : IUserService
    {
        private readonly KorepetynderDbContext _korepetynderDbContext;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(KorepetynderDbContext korepetynderDbContext, ISieveProcessor sieveProcessor, IHttpContextAccessor httpContextAccessor)
        {
            _korepetynderDbContext = korepetynderDbContext;
            _sieveProcessor = sieveProcessor;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<UserResponse> InitializeUser(UserRequest request)
        {
            var id = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var userExists = _korepetynderDbContext.Users.Any(u => u.Id == id);
            if (userExists)
            {
                throw new InvalidOperationException("User with this Guid already exists");
            }
            var user = new User(id, request.FirstName, request.LastName, request.Age, request.Email, request.TelephoneNumber);
            await _korepetynderDbContext.Users.AddAsync(user);
            await _korepetynderDbContext.SaveChangesAsync();
            return new UserResponse(user.Id, user.FirstName, user.LastName, user.TelephoneNumber, user.Email, user.Age, false, false);
        }
        public async Task<UserResponse> UpdateUser(UserRequest request)
        {
            var id = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var user = await _korepetynderDbContext.Users.Where(user => user.Id == id).SingleAsync();
            user.SetValues(request.FirstName, request.LastName, request.Age, request.Email, request.TelephoneNumber);
            await _korepetynderDbContext.SaveChangesAsync();
            return new UserResponse(user.Id, user.FirstName, user.LastName, user.TelephoneNumber, user.Email, user.Age, user.StudentId is not null, user.TeacherId is not null);
        }

        public async Task<UserResponse> GetUser()
        {
            var id = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var user = await _korepetynderDbContext.Users.Where(u => u.Id == id).SingleAsync();
            return new UserResponse(user.Id, user.FirstName, user.LastName, user.TelephoneNumber, user.Email, user.Age, user.StudentId is not null, user.TeacherId is not null);

        }
    }
}
