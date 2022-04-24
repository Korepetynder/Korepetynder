using Korepetynder.Contracts.Requests.User;
using Korepetynder.Contracts.Responses.User;
using Korepetynder.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Korepetynder.Api.Controllers
{
    [Authorize]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes")]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _usersService;

        public UserController(IUserService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Creates a new user connected to current guid.
        /// </summary>
        /// <param name="userRequest">Request containing data for a new user.</param>
        /// <returns>Newly created user.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserResponse>> PostUser([FromBody] UserCreationRequest userRequest)
        {
            try
            {
                var user = await _usersService.InitializeUser(userRequest);

                return user;
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Returns currently logged user data (if he was initiated)
        /// </summary>
        /// <returns>User data.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponse>> GetUser()
        {
            try
            {
                var user = await _usersService.GetUser();

                return user;
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Gets the roles assigned to the user.
        /// </summary>
        /// <returns>Roles.</returns>
        [HttpGet("roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserRolesResponse>> GetUserRoles()
        {
            try
            {
                var userRoles = await _usersService.GetUserRoles();

                return userRoles;
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}
