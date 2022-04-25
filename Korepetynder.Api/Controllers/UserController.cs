using Korepetynder.Contracts.Requests.Users;
using Korepetynder.Contracts.Responses.Users;
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
        public async Task<ActionResult<UserResponse>> PostUser([FromBody] UserRequest userRequest)
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
        /// Updates user data with provided one
        /// </summary>
        /// <returns>User data.</returns>
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserResponse>> PutUser([FromBody] UserRequest userRequest)
        {
            try
            {
                var user = await _usersService.UpdateUser(userRequest);

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
