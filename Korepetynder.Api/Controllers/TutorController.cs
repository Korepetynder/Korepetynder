using Korepetynder.Contracts.Requests.Tutors;
using Korepetynder.Contracts.Responses.Tutors;
using Korepetynder.Services.Tutors;
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
    public class TutorController : ControllerBase
    {
        private readonly ITutorService _tutorsService;

        public TutorController(ITutorService tutorsService)
        {
            _tutorsService = tutorsService;
        }

        /// <summary>
        /// Creates a new tutor connected to provided user.
        /// </summary>
        /// <param name="tutorRequest">Request containing data for a new tutor.</param>
        /// <returns>Newly created tutor.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TutorResponse>> PostTutor([FromBody] TutorRequest tutorRequest)
        {
            try
            {
                var tutor = await _tutorsService.InitializeTutor(tutorRequest);

                return tutor;
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Gets tutor connected to provided user.
        /// </summary>
        /// <returns> Tutor connected to current user.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TutorResponse>> GetTutor()
        {
            try
            {
                var tutor = await _tutorsService.GetTutorData();

                return tutor;
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates tutor profile connected to provided user.
        /// </summary>
        /// <returns>Updated tutor.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TutorResponse>> PutTutor(TutorRequest request)
        {
            try
            {
                var tutor = await _tutorsService.UpdateTutor(request);

                return tutor;
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates chosen lesson.
        /// </summary>
        /// <param name="id">ID of the lesson to update.</param>
        /// <param name="lessonRequest">Request containing data for updated lesson.</param>
        [HttpPut("Lessons/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TutorLessonResponse>> PutLesson([FromRoute] int id, [FromBody] TutorLessonRequest lessonRequest)
        {
            try
            {
                var lesson = await _tutorsService.UpdateLesson(id, lessonRequest);

                return lesson;
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Removes tutor connected to provided user.
        /// </summary>
        /// <returns>Newly created subject.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTutor()
        {
            try
            {
                await _tutorsService.DeleteTutor();

                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Creates a new lesson that currently active tutor is looking for.
        /// </summary>
        /// <param name="lessonRequest">Request containing data for a new lesson.</param>
        /// <returns>Newly created lesson.</returns>
        [HttpPost("Lessons")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TutorLessonResponse>> PostLesson([FromBody] TutorLessonRequest lessonRequest)
        {
            try
            {
                var lesson = await _tutorsService.AddLesson(lessonRequest);

                return lesson;
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Returns list of lessons that tutor is looking for
        /// </summary>
        /// <param name="sieveModel">Sieve model containing data for sorting, filtering and pagination.</param>
        /// <returns>List of lessons.</returns>
        [HttpGet("Lessons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TutorLessonResponse>>> GetLessons([FromQuery] SieveModel sieveModel)
        {
            try
            {
                var lessons = await _tutorsService.GetLessons(sieveModel);
                Response.Headers.Add("X-Total-Count", lessons.TotalCount.ToString());

                return lessons.Entities.ToList();
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes lesson with given id, if it belongs to currently logged user
        /// </summary>
        [HttpDelete("Lessons/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteLesson([FromRoute] int id)
        {
            try
            {
                await _tutorsService.DeleteLesson(id);

                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
            catch (ArgumentException)
            {
                return Forbid();
            }
        }
    }
}
