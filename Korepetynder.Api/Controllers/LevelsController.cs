using Korepetynder.Contracts.Requests.Levels;
using Korepetynder.Contracts.Responses.Levels;
using Korepetynder.Services.Levels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace Korepetynder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelsController : ControllerBase
    {
        private readonly ILevelsService _levelsService;

        public LevelsController(ILevelsService levelsService)
        {
            _levelsService = levelsService;
        }

        /// <summary>
        /// Gets the list of subjects.
        /// </summary>
        /// <param name="sieveModel">Sieve model containing data for sorting, filtering and pagination.</param>
        /// <returns>List of subjects.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LevelResponse>>> GetLevels([FromQuery] SieveModel sieveModel)
        {
            var subjects = await _levelsService.GetLevels(sieveModel);

            Response.Headers.Add("X-Total-Count", subjects.TotalCount.ToString());

            return subjects.Entities.ToList();
        }

        /// <summary>
        /// Gets the specified subject.
        /// </summary>
        /// <param name="id">ID of the subject.</param>
        /// <returns>Subject.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LevelResponse>> GetLevel([FromRoute] int id)
        {
            var subject = await _levelsService.GetLevel(id);

            if (subject == null)
            {
                return NotFound();
            }

            return subject;
        }

        /// <summary>
        /// Creates a new subject.
        /// </summary>
        /// <param name="levelRequest">Request containing data for a new subject.</param>
        /// <returns>Newly created subject.</returns>
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
