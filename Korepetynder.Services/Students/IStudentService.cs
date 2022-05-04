using Korepetynder.Contracts.Requests.Students;
using Korepetynder.Contracts.Responses.Students;
using Korepetynder.Contracts.Responses.Teachers;
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
        Task<IEnumerable<TeacherDataResponse>> GetSuggestedTeachers();
        Task AddFavouriteTeacher(int id);
        Task DeleteFavouriteTeacher(int id);
        Task<PagedData<FullTeacherInfoResponse>> GetFavouriteTeachers();
    }
}
