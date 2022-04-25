using Korepetynder.Contracts.Requests.Teachers;
using Korepetynder.Contracts.Responses.Teachers;
using Korepetynder.Services.Teachers;
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
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teachersService;

        public TeacherController(ITeacherService teachersService)
        {
            _teachersService = teachersService;
        }

        /// <summary>
        /// Creates a new teacher connected to provided user.
        /// </summary>
        /// <param name="teacherRequest">Request containing data for a new teacher.</param>
        /// <returns>Newly created teacher.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeacherResponse>> PostTeacher([FromBody] TeacherRequest teacherRequest)
        {
            try
            {
                var teacher = await _teachersService.InitializeTeacher(teacherRequest);

                return teacher;
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Gets teacher connected to provided user.
        /// </summary>
        /// <returns> Teacher connected to current user.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeacherResponse>> GetTeacher()
        {
            try
            {
                var teacher = await _teachersService.GetTeacherData();

                return teacher;
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates teacher profile connected to provided user.
        /// </summary>
        /// <returns>Updated teacher.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeacherResponse>> PutTeacher(TeacherRequest request)
        {
            try
            {
                var teacher = await _teachersService.UpdateTeacher(request);

                return teacher;
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Removes teacher connected to provided user.
        /// </summary>
        /// <returns>Newly created subject.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTeacher()
        {
            try
            {
                await _teachersService.DeleteTeacher();

                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Creates a new lesson that currently active teacher is looking for.
        /// </summary>
        /// <param name="lessonRequest">Request containing data for a new lesson.</param>
        /// <returns>Newly created lesson.</returns>
        [HttpPost("Lessons")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeacherLessonResponse>> PostLesson([FromBody] TeacherLessonRequest lessonRequest)
        {
            try
            {
                var lesson = await _teachersService.AddLesson(lessonRequest);

                return lesson;
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Returns list of lessons that teacher is looking for
        /// </summary>
        /// <param name="sieveModel">Sieve model containing data for sorting, filtering and pagination.</param>
        /// <returns>List of lessons.</returns>
        [HttpGet("Lessons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TeacherLessonResponse>>> GetLessons([FromQuery] SieveModel sieveModel)
        {
            try
            {
                var lessons = await _teachersService.GetLessons(sieveModel);
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
                await _teachersService.DeleteLesson(id);

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
