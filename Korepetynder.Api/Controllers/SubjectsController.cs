using Korepetynder.Contracts.Requests.Subjects;
using Korepetynder.Contracts.Responses.Subjects;
using Korepetynder.Services.Subjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Sieve.Models;

namespace Korepetynder.Api.Controllers
{
    /// <summary>
    /// Controller for subjects management.
    /// </summary>
    [Authorize]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes")]
    [Route("[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectsService _subjectsService;

        public SubjectsController(ISubjectsService subjectsService)
        {
            _subjectsService = subjectsService;
        }

        /// <summary>
        /// Gets the list of subjects.
        /// </summary>
        /// <param name="sieveModel">Sieve model containing data for sorting, filtering and pagination.</param>
        /// <returns>List of subjects.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SubjectResponse>>> GetSubjects([FromQuery] SieveModel sieveModel)
        {
            var subjects = await _subjectsService.GetSubjects(sieveModel);

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
        public async Task<ActionResult<SubjectResponse>> GetSubject([FromRoute] int id)
        {
            var subject = await _subjectsService.GetSubject(id);

            if (subject == null)
            {
                return NotFound();
            }

            return subject;
        }

        /// <summary>
        /// Creates a new subject.
        /// </summary>
        /// <param name="subjectRequest">Request containing data for a new subject.</param>
        /// <returns>Newly created subject.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<SubjectResponse>> PostSubject([FromBody] SubjectRequest subjectRequest)
        {
            try
            {
                var subject = await _subjectsService.AddSubject(subjectRequest);

                return CreatedAtAction(nameof(GetSubject), new { id = subject.Id }, subject);
            }
            catch (InvalidOperationException)
            {
                return Conflict();
            }
        }
    }
}
