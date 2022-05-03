using Korepetynder.Contracts.Requests.Users;
using Korepetynder.Contracts.Responses.Users;
using Korepetynder.Data;
using Korepetynder.Data.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;

namespace Korepetynder.Services.Users
{
    internal class UserService : AdBaseService, IUserService
    {
        private readonly KorepetynderDbContext _korepetynderDbContext;

        public UserService(KorepetynderDbContext korepetynderDbContext, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _korepetynderDbContext = korepetynderDbContext;
        }
        public async Task<UserResponse> InitializeUser(UserRequest request)
        {
            var id = GetCurrentUserId();

            var userExists = _korepetynderDbContext.Users.Any(u => u.Id == id);
            if (userExists)
            {
                throw new InvalidOperationException("User with this ID already exists");
            }

            var user = new User(id, request.FirstName, request.LastName, request.BirthDate, request.Email, request.PhoneNumber);
            _korepetynderDbContext.Users.Add(user);
            await _korepetynderDbContext.SaveChangesAsync();

            return new UserResponse(user.Id, user.FirstName, user.LastName, user.PhoneNumber, user.Email, user.BirthDate);
        }
        public async Task<UserResponse> UpdateUser(UserRequest request)
        {
            var id = GetCurrentUserId();

            var user = await _korepetynderDbContext.Users.Where(user => user.Id == id).SingleAsync();
            user.SetValues(request.FirstName, request.LastName, request.BirthDate, request.Email, request.PhoneNumber);
            await _korepetynderDbContext.SaveChangesAsync();

            return new UserResponse(user.Id, user.FirstName, user.LastName, user.PhoneNumber, user.Email, user.BirthDate);
        }

        public async Task<UserResponse> GetUser()
        {
            var id = GetCurrentUserId();

            return await _korepetynderDbContext.Users
                .Where(u => u.Id == id)
                .Select(u => new UserResponse(u.Id, u.FirstName, u.LastName, u.PhoneNumber, u.Email, u.BirthDate))
                .SingleAsync();

        }

        public async Task<UserRolesResponse> GetUserRoles()
        {
            var id = GetCurrentUserId();

            return await _korepetynderDbContext.Users
                .Where(u => u.Id == id)
                .Select(user => new UserRolesResponse(user.Student != null, user.Tutor != null))
                .SingleAsync();
        }
    }
}
