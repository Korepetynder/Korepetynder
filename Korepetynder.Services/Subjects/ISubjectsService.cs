using Korepetynder.Contracts.Requests.Subjects;
using Korepetynder.Contracts.Responses.Subjects;
using Korepetynder.Services.Models;
using Sieve.Models;

namespace Korepetynder.Services.Subjects
{
    public interface ISubjectsService
    {
        Task<PagedData<SubjectResponse>> GetSubjects(SieveModel sieveModel);
        Task<SubjectResponse?> GetSubject(int id);
        Task<SubjectResponse> AddSubject(SubjectRequest subjectRequest);
    }
}
