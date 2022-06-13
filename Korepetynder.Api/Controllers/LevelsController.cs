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
        /// <param name="id">ID of the level to get.</param>
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
        /// Creates a new level it has to be approved by admin before being seen by everyone.
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
        /// <summary>
        /// Returns list of all levels that are to be accepted.
        /// </summary>
        /// <param name="sieveModel">Sieve model containing data for sorting, filtering and pagination.</param>
        /// <returns>List of levels.</returns>
        [HttpGet("manage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<IEnumerable<LevelResponse>>> GetNewLevels([FromQuery] SieveModel sieveModel)
        {
            try
            {

                var levels = await _levelsService.GetNewLevels(sieveModel);

                Response.Headers.Add("X-Total-Count", levels.TotalCount.ToString());

                return levels.Entities.ToList();
            }
            catch (InvalidOperationException)
            {
                return Conflict();
            }
        }
        /// <summary>
        /// Approves level with given id, furthermore weight of level can be specified before accepting (the higher the weight the lower it will be in returned lists)
        /// </summary>
        /// <param name="id">ID of the level to approve.</param>
        /// <param name="weight">Optional - weight of approved level </param>
        /// <returns>Approved level.</returns>
        [HttpPost("manage/{id}/{weight}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<LevelResponse>> PostAcceptedLevel([FromRoute] int id, [FromRoute] int weight)
        {
            try
            {
                var level = await _levelsService.AcceptLevel(id, weight);

                return CreatedAtAction(nameof(GetLevel), new { id = level.Id }, level);
            }
            catch (InvalidOperationException)
            {
                return Conflict();
            }
        }
        /// <summary>
        /// Removes level with given id
        /// </summary>
        /// <param name="id">ID of the level to approve.</param>
        [HttpDelete("manage/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteLevel([FromRoute] int id)
        {
            try
            {
                await _levelsService.DeleteLevel(id);

                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
    }
}
