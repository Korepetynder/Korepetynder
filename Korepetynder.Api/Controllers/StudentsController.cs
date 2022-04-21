using Korepetynder.Contracts.Requests.Students;
using Korepetynder.Contracts.Responses.Students;
using Korepetynder.Services.Students;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        /// Creates a new subject.
        /// </summary>
        /// <param name="subjectRequest">Request containing data for a new subject.</param>
        /// <returns>Newly created subject.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentResponse>> PostSubject([FromBody] StudentCreationRequest subjectRequest)
        {
            try
            {
                var student = await _studentsService.AddStudent(subjectRequest);

                return student;
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
            {
                return BadRequest();
            }
        }
    }
}
