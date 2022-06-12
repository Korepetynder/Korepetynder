using Korepetynder.Contracts.Requests.Comments;
using Korepetynder.Contracts.Requests.Tutors;
using Korepetynder.Contracts.Responses.Comments;
using Korepetynder.Contracts.Responses.Students;
using Korepetynder.Contracts.Responses.Tutors;
using Korepetynder.Services.Models;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Services.Tutors
{
    public interface ITutorService
    {
        Task<TutorResponse> InitializeTutor(TutorRequest request);
        Task<TutorResponse> UpdateTutor(TutorRequest request);

        Task DeleteTutor();
        Task<TutorResponse> GetTutorData();
        Task<PagedData<TutorLessonResponse>> GetLessons(SieveModel model);
        Task<TutorLessonResponse> AddLesson(TutorLessonRequest request);
        Task<TutorLessonResponse> UpdateLesson(int id, TutorLessonRequest request);
        Task DeleteLesson(int id);
        Task<PagedData<CommentResponse>> GetComments(Guid tutorId, SieveModel model);
        Task<CommentResponse> AddComment(CommentRequest request);
    }
}
