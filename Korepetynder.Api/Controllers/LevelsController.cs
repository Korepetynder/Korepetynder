using Korepetynder.Contracts.Requests.Levels;
using Korepetynder.Contracts.Responses.Levels;
using Korepetynder.Services.Levels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Sieve.Models;

namespace Korepetynder.Api.Controllers
{
    [Authorize]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes")]
    [Route("[controller]")]
    [ApiController]
    public class LevelsController : ControllerBase
    {
        private readonly ILevelsService _levelsService;

        public LevelsController(ILevelsService levelsService)
        {
            _levelsService = levelsService;
        }

        /// <summary>
        /// Gets the list of levels.
        /// </summary>
        /// <param name="sieveModel">Sieve model containing data for sorting, filtering and pagination.</param>
        /// <returns>List of levels.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LevelResponse>>> GetLevels([FromQuery] SieveModel sieveModel)
        {
            var levels = await _levelsService.GetLevels(sieveModel);

            Response.Headers.Add("X-Total-Count", levels.TotalCount.ToString());

            return levels.Entities.ToList();
        }

        /// <summary>
        /// Gets the specified level.
        /// </summary>
        /// <param name="id">ID of the level.</param>
        /// <returns>Level.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LevelResponse>> GetLevel([FromRoute] int id)
        {
            var level = await _levelsService.GetLevel(id);

            if (level == null)
            {
                return NotFound();
            }

            return level;
        }

        /// <summary>
        /// Creates a new level.
        /// </summary>
        /// <param name="levelRequest">Request containing data for a new level.</param>
        /// <returns>Newly created level.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<LevelResponse>> PostLevel([FromBody] LevelRequest levelRequest)
        {
            try
            {
                var level = await _levelsService.AddLevel(levelRequest);

                return CreatedAtAction(nameof(GetLevel), new { id = level.Id }, level);
            }
            catch (InvalidOperationException)
            {
                return Conflict();
            }
        }
    }
}
