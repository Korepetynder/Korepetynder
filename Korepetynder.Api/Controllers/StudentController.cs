using Korepetynder.Contracts.Requests.Students;
using Korepetynder.Contracts.Responses.Students;
using Korepetynder.Contracts.Responses.Tutors;
using Korepetynder.Services.Students;
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
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentsService;

        public StudentController(IStudentService studentsService)
        {
            _studentsService = studentsService;
        }

        /// <summary>
        /// Creates a new student connected to provided user.
        /// </summary>
        /// <param name="studentRequest">Request containing data for a new user.</param>
        /// <returns>Newly created user.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentResponse>> PostStudent([FromBody] StudentRequest studentRequest)
        {
            try
            {
                var student = await _studentsService.InitializeStudent(studentRequest);

                return student;
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Gets student connected to provided user.
        /// </summary>
        /// <returns>Newly created user.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentResponse>> GetStudent()
        {
            try
            {
                var student = await _studentsService.GetStudentData();

                return student;
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates student profile connected to provided user.
        /// </summary>
        /// <returns>Updated student.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentResponse>> PutStudent(StudentRequest request)
        {
            try
            {
                var student = await _studentsService.UpdateStudent(request);

                return student;
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Removes student connected to provided user.
        /// </summary>
        /// <returns>Newly created subject.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteStudent()
        {
            try
            {
                await _studentsService.DeleteStudent();

                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Updates the specified lesson.
        /// </summary>
        /// <param name="id">ID of the lesson to update.</param>
        /// <param name="lessonRequest">Request containing data for updated lesson.</param>
        [HttpPut("Lessons/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentLessonResponse>> PutLesson([FromRoute] int id, [FromBody] StudentLessonRequest lessonRequest)
        {
            try
            {
                var lesson = await _studentsService.UpdateLesson(id, lessonRequest);

                return lesson;
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Creates a new lesson that currently active student is looking for.
        /// </summary>
        /// <param name="lessonRequest">Request containing data for a new lesson.</param>
        /// <returns>Newly created lesson.</returns>
        [HttpPost("Lessons")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentLessonResponse>> PostLesson([FromBody] StudentLessonRequest lessonRequest)
        {
            try
            {
                var lesson = await _studentsService.AddLesson(lessonRequest);

                return lesson;
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Returns list of lessons that student is looking for.
        /// </summary>
        /// <param name="sieveModel">Sieve model containing data for sorting, filtering and pagination.</param>
        /// <returns>List of lessons.</returns>
        [HttpGet("Lessons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<StudentLessonResponse>>> GetLessons([FromQuery] SieveModel sieveModel)
        {
            try
            {
                var lessons = await _studentsService.GetLessons(sieveModel);
                Response.Headers.Add("X-Total-Count", lessons.TotalCount.ToString());

                return lessons.Entities.ToList();
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes lesson with the given id, if it belongs to the currently logged user.
        /// </summary>
        /// <returns>List of lessons.</returns>
        [HttpDelete("Lessons/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> GetLessons([FromRoute] int id)
        {
            try
            {
                await _studentsService.DeleteLesson(id);

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
        /// <summary>
        /// Gets suggestions of tutors.
        /// </summary>
        /// <returns>List of tutors.</returns>
        [HttpGet("Tutors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TutorDataResponse>>> GetSuggestedTutors()
        {
            try
            {
                var tutors = await _studentsService.GetSuggestedTutors();

                Response.Headers.Add("X-Total-Count", tutors.Count().ToString());
                return tutors.ToList();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Gets list of favorite tutors.
        /// </summary>
        /// <returns>List of favorite tutors.</returns>
        [HttpGet("Favorite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TutorDataResponse>>> GetFavoriteTutors([FromQuery] SieveModel sieveModel)
        {
            try
            {
                var tutors = await _studentsService.GetFavoriteTutors(sieveModel);

                Response.Headers.Add("X-Total-Count", tutors.TotalCount.ToString());
                return tutors.Entities.ToList();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Adds tutor to favorite
        /// </summary>
        [HttpPost("Favorite/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostFavoriteTutor([FromRoute] Guid id)
        {
            try
            {
                await _studentsService.AddFavoriteTutor(id);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Removes tutor from favorite.
        /// </summary>
        [HttpDelete("Favorite/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteFavoriteTutor([FromRoute] Guid id)
        {
            try
            {
                await _studentsService.DeleteFavoriteTutor(id);
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
    }
}
