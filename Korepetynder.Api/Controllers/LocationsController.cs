
using Korepetynder.Contracts.Requests.Locations;
using Korepetynder.Contracts.Responses.Locations;
using Korepetynder.Services.Locations;
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
    public class LocationsController : ControllerBase
    {
        private readonly ILocationsService _locationsService;

        public LocationsController(ILocationsService locationsService)
        {
            _locationsService = locationsService;
        }

        /// <summary>
        /// Gets the list of locations.
        /// </summary>
        /// <param name="sieveModel">Sieve model containing data for sorting, filtering and pagination.</param>
        /// <returns>List of locations.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LocationResponse>>> GetLocations([FromQuery] SieveModel sieveModel)
        {
            var locations = await _locationsService.GetLocations(sieveModel);

            Response.Headers.Add("X-Total-Count", locations.TotalCount.ToString());

            return locations.Entities.ToList();
        }

        /// <summary>
        /// Gets the specified location.
        /// </summary>
        /// <param name="id">ID of the location.</param>
        /// <returns>Location.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LocationResponse>> GetLocation([FromRoute] int id)
        {
            var location = await _locationsService.GetLocation(id);

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        /// <summary>
        /// Creates a new location.
        /// </summary>
        /// <param name="locationRequest">Request containing data for a new location.</param>
        /// <returns>Newly created location.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LocationResponse>> PostLocation([FromBody] LocationRequest locationRequest)
        {
            try
            {
                var location = await _locationsService.AddLocation(locationRequest);

                return CreatedAtAction(nameof(GetLocation), new { id = location.Id }, location);
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
    }
}
