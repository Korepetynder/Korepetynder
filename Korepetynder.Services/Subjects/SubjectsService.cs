using Korepetynder.Contracts.Requests.Subjects;
using Korepetynder.Contracts.Responses.Subjects;
using Korepetynder.Data;
using Korepetynder.Data.DbModels;
using Korepetynder.Services.Models;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace Korepetynder.Services.Subjects
{
    internal class SubjectsService : ISubjectsService
    {
        private readonly KorepetynderDbContext _korepetynderDbContext;
        private readonly ISieveProcessor _sieveProcessor;

        public SubjectsService(KorepetynderDbContext korepetynderDbContext, ISieveProcessor sieveProcessor)
        {
            _korepetynderDbContext = korepetynderDbContext;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<SubjectResponse> AddSubject(SubjectRequest subjectRequest)
        {
            var subjectExists = await _korepetynderDbContext.Subjects
                .AnyAsync(subject => subject.Name == subjectRequest.Name);
            if (subjectExists)
            {
                throw new InvalidOperationException("Subject with name " + subjectRequest.Name + " already exists");
            }    

            var subject = new Subject(subjectRequest.Name);
            _korepetynderDbContext.Subjects.Add(subject);
            await _korepetynderDbContext.SaveChangesAsync();

            return new SubjectResponse(subject.Id, subject.Name);
        }

        public async Task<PagedData<SubjectResponse>> GetSubjects(SieveModel sieveModel)
        {
            var subjects = _korepetynderDbContext.Subjects
                .OrderBy(subject => subject.Name)
                .AsNoTracking();

            subjects = _sieveProcessor.Apply(sieveModel, subjects, applyPagination: false);

            var count = await subjects.CountAsync();

            subjects = _sieveProcessor.Apply(sieveModel, subjects, applyFiltering: false, applySorting: false);

            return new PagedData<SubjectResponse>(count, await subjects
                .Select(subject => new SubjectResponse(subject.Id, subject.Name))
                .ToListAsync());
        }

        public async Task<SubjectResponse?> GetSubject(int id) =>
            await _korepetynderDbContext.Subjects
                .AsNoTracking()
                .Select(subject => new SubjectResponse(subject.Id, subject.Name))
                .SingleOrDefaultAsync(subject => subject.Id == id);
    }
}
