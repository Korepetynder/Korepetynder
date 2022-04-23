using Korepetynder.Contracts.Requests.Students;
using Korepetynder.Contracts.Responses.Students;
using Korepetynder.Services.Models;
using Sieve.Models;

namespace Korepetynder.Services.Students
{
    public interface IStudentService
    {
        Task<StudentResponse> InitializeStudent(StudentCreationRequest request);

        Task<PagedData<StudentLessonResponse>> GetLessons(SieveModel model);
        Task<StudentLessonResponse> AddLesson(LessonCreationRequest request);
    }
}
