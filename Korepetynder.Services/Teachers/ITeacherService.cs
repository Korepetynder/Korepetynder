using Korepetynder.Contracts.Requests.Teachers;
using Korepetynder.Contracts.Responses.Students;
using Korepetynder.Contracts.Responses.Teachers;
using Korepetynder.Services.Models;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Services.Teachers
{
    public interface ITeacherService
    {
        Task<TeacherResponse> InitializeTeacher(TeacherRequest request);
        Task<TeacherResponse> UpdateTeacher(TeacherRequest request);

        Task DeleteTeacher();
        Task<TeacherResponse> GetTeacherData();
        Task<PagedData<TeacherLessonResponse>> GetLessons(SieveModel model);
        Task<TeacherLessonResponse> AddLesson(TeacherLessonRequest request);
        Task DeleteLesson(int id);
    }
}
