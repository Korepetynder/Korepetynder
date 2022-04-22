using Korepetynder.Contracts.Requests.Students;
using Korepetynder.Contracts.Responses.Students;
using Korepetynder.Services.Students;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace Korepetynder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentsService;

        public StudentsController(IStudentService studentsService)
        {
            _studentsService = studentsService;
        }

        /// <summary>
        /// Creates a new student connected to provided user.
        /// </summary>
        /// <param name="studentRequest">Request containing data for a new user.</param>
        /// <returns>Newly created subject.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentResponse>> PostSubject([FromBody] StudentCreationRequest studentRequest)
        {
            try
            {
                var student = await _studentsService.AddStudent(studentRequest);

                return student;
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
        [HttpPost("/lessons")]
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
        [HttpGet("/lessons")]
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
    }
}