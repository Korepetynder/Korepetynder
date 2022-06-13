using Korepetynder.Contracts.Requests.Languages;
using Korepetynder.Contracts.Responses.Languages;
using Korepetynder.Services.Languages;
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
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguagesService _languagesService;

        public LanguagesController(ILanguagesService languagesService)
        {
            _languagesService = languagesService;
        }

        /// <summary>
        /// Gets the list of languages.
        /// </summary>
        /// <param name="sieveModel">Sieve model containing data for sorting, filtering and pagination.</param>
        /// <returns>List of languages.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LanguageResponse>>> GetLanguages([FromQuery] SieveModel sieveModel)
        {
            var languages = await _languagesService.GetLanguages(sieveModel);

            Response.Headers.Add("X-Total-Count", languages.TotalCount.ToString());

            return languages.Entities.ToList();
        }

        /// <summary>
        /// Gets the specified language.
        /// </summary>
        /// <param name="id">ID of the language to get.</param>
        /// <returns>Language.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LanguageResponse>> GetLanguage([FromRoute] int id)
        {
            var language = await _languagesService.GetLanguage(id);

            if (language == null)
            {
                return NotFound();
            }

            return language;
        }

        /// <summary>
        /// Creates a new language.
        /// </summary>
        /// <param name="languageRequest">Request containing data for a new language.</param>
        /// <returns>Newly created language.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<LanguageResponse>> PostLanguage([FromBody] LanguageRequest languageRequest)
        {
            try
            {
                var language = await _languagesService.AddLanguage(languageRequest);

                return CreatedAtAction(nameof(GetLanguage), new { id = language.Id }, language);
            }
            catch (InvalidOperationException)
            {
                return Conflict();
            }
        }
        /// <summary>
        /// Returns list of all languages that are to be accepted.
        /// </summary>
        /// <param name="sieveModel">Sieve model containing data for sorting, filtering and pagination.</param>
        /// <returns>List of languages.</returns>
        [HttpGet("manage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<IEnumerable<LanguageResponse>>> GetNewLanguages([FromQuery] SieveModel sieveModel)
        {
            try
            {

                var languages = await _languagesService.GetNewLanguages(sieveModel);

                Response.Headers.Add("X-Total-Count", languages.TotalCount.ToString());

                return languages.Entities.ToList();
            }
            catch (InvalidOperationException)
            {
                return Conflict();
            }
        }
        /// <summary>
        /// Approves language with given id
        /// </summary>
        /// <param name="id">ID of the language to approve.</param>
        /// <returns>Approved language.</returns>
        [HttpPost("manage/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<LanguageResponse>> PostAcceptedLanguage([FromRoute] int id)
        {
            try
            {
                var language = await _languagesService.AcceptLanguage(id);

                return CreatedAtAction(nameof(GetLanguage), new { id = language.Id }, language);
            }
            catch (InvalidOperationException)
            {
                return Conflict();
            }
        }
        /// <summary>
        /// Removes language with given id
        /// </summary>
        /// <param name="id">ID of the language to approve.</param>
        [HttpDelete("manage/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteLanguage([FromRoute] int id)
        {
            try
            {
                await _languagesService.DeleteLanguage(id);

                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
    }
}
