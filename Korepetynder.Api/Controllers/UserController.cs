using Korepetynder.Contracts.Requests.User;
using Korepetynder.Contracts.Responses.User;
using Korepetynder.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Sieve.Models;

namespace Korepetynder.Api.Controllers
{
    [Authorize]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes")]
    [Route("api/[controller]")]
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
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserResponse>> GetUser()
        {
            try
            {
                var user = await _usersService.GetUser();

                return user;
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
    }
}
