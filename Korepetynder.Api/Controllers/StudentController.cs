using Korepetynder.Contracts.Requests.Students;
using Korepetynder.Contracts.Responses.Students;
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
        /// Creates a new lesson that currently active student is looking for.
        /// </summary>
        /// <param name="lessonRequest">Request containing data for a new lesson.</param>
        /// <returns>Newly created lesson.</returns>
        [HttpPost("Lessons")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentLessonResponse>> PostLesson([FromBody] LessonCreationRequest lessonRequest)
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
        /// Returns list of lessons that student is looking for
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
        /// Deletes lesson with given id, if it belongs to currently logged user
        /// </summary>
        /// <param name="id">ID of the lesson.</param>
        [HttpDelete("Lessons/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteLesson([FromRoute] int id)
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

    }
}
