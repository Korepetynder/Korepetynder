using Korepetynder.Contracts.Requests.Students;
using Korepetynder.Contracts.Responses.Students;
using Korepetynder.Contracts.Responses.Tutors;
using Korepetynder.Services.Models;
using Sieve.Models;

namespace Korepetynder.Services.Students
{
    public interface IStudentService
    {
        Task<StudentResponse> InitializeStudent(StudentRequest request);
        Task<StudentResponse> UpdateStudent(StudentRequest request);

        Task DeleteStudent();
        Task<StudentResponse> GetStudentData();
        Task<PagedData<StudentLessonResponse>> GetLessons(SieveModel model);
        Task<StudentLessonResponse> AddLesson(StudentLessonRequest request);
        Task<StudentLessonResponse> UpdateLesson(int id, StudentLessonRequest request);
        Task DeleteLesson(int id);
        Task<IEnumerable<TutorDataResponse>> GetSuggestedTutors();
        Task AddFavoriteTutor(Guid id);
        Task DeleteFavoriteTutor(Guid id);
        Task<PagedData<TutorDataResponse>> GetFavoriteTutors(SieveModel model);
        Task AddTutorToIgnored(Guid tutorId);
    }
}
