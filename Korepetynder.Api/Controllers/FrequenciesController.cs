using Korepetynder.Contracts.Requests.Frequencies;
using Korepetynder.Contracts.Responses.Frequencies;
using Korepetynder.Services.Frequencies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Sieve.Models;

namespace Korepetynder.Api.Controllers
{
    [Authorize]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes")]
    [Route("api/[controller]")]
    [ApiController]
    public class FrequenciesController : ControllerBase
    {
        private readonly IFrequenciesService _frequenciesService;

        public FrequenciesController(IFrequenciesService frequenciesService)
        {
            _frequenciesService = frequenciesService;
        }

        /// <summary>
        /// Gets the list of frequencies.
        /// </summary>
        /// <param name="sieveModel">Sieve model containing data for sorting, filtering and pagination.</param>
        /// <returns>List of frequencies.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<FrequencyResponse>>> GetFrequencies([FromQuery] SieveModel sieveModel)
        {
            var frequency = await _frequenciesService.GetFrequencies(sieveModel);

            Response.Headers.Add("X-Total-Count", frequency.TotalCount.ToString());

            return frequency.Entities.ToList();
        }

        /// <summary>
        /// Gets the specified frequency.
        /// </summary>
        /// <param name="id">ID of the frequency.</param>
        /// <returns>Frequency.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FrequencyResponse>> GetFrequency([FromRoute] int id)
        {
            var frequency = await _frequenciesService.GetFrequency(id);

            if (frequency == null)
            {
                return NotFound();
            }

            return frequency;
        }

        /// <summary>
        /// Creates a new frequency.
        /// </summary>
        /// <param name="frequencyRequest">Request containing data for a new frequency.</param>
        /// <returns>Newly created frequency.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<FrequencyResponse>> PostFrequency([FromBody] FrequencyRequest frequencyRequest)
        {
            try
            {
                var frequency = await _frequenciesService.AddFrequency(frequencyRequest);

                return CreatedAtAction(nameof(GetFrequency), new { id = frequency.Id }, frequency);
            }
            catch (InvalidOperationException)
            {
                return Conflict();
            }
        }
    }
}
