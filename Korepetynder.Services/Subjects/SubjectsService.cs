using Korepetynder.Contracts.Requests.Subjects;
using Korepetynder.Contracts.Responses.Subjects;
using Korepetynder.Data;
using Korepetynder.Data.DbModels;
using Korepetynder.Services.Exceptions;
using Korepetynder.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace Korepetynder.Services.Subjects
{
    internal class SubjectsService : AdBaseService, ISubjectsService
    {
        private readonly KorepetynderDbContext _korepetynderDbContext;
        private readonly ISieveProcessor _sieveProcessor;

        public SubjectsService(KorepetynderDbContext korepetynderDbContext, ISieveProcessor sieveProcessor, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
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
                .Where(subject => subject.WasAccepted)
                .OrderBy(subject => subject.Name)
                .AsQueryable();

            subjects = _sieveProcessor.Apply(sieveModel, subjects, applyPagination: false);

            var count = await subjects.CountAsync();

            subjects = _sieveProcessor.Apply(sieveModel, subjects, applyFiltering: false, applySorting: false);

            return new PagedData<SubjectResponse>(count, await subjects
                .Select(subject => new SubjectResponse(subject.Id, subject.Name))
                .ToListAsync());
        }

        public async Task<SubjectResponse?> GetSubject(int id) =>
            await _korepetynderDbContext.Subjects
                .Where(subject => subject.Id == id)
                .Select(subject => new SubjectResponse(subject.Id, subject.Name))
                .SingleOrDefaultAsync();
        public async Task<PagedData<SubjectResponse>> GetNewSubjects(SieveModel sieveModel)
        {
            if (!await IsAdmin())
            {
                throw new PermissionDeniedException();
            }

            var subjects = _korepetynderDbContext.Subjects
                   .Where(subject => !subject.WasAccepted)
                   .OrderBy(subject => subject.Name)
                   .AsQueryable();

            subjects = _sieveProcessor.Apply(sieveModel, subjects, applyPagination: false);

            var count = await subjects.CountAsync();

            subjects = _sieveProcessor.Apply(sieveModel, subjects, applyFiltering: false, applySorting: false);

            return new PagedData<SubjectResponse>(count, await subjects
                .Select(subject => new SubjectResponse(subject.Id, subject.Name))
                .ToListAsync());
        }
        public async Task<SubjectResponse> AcceptSubject(int id)
        {
            if (!await IsAdmin())
            {
                throw new PermissionDeniedException();
            }

            var subject = await _korepetynderDbContext.Subjects
                .Where(subject => subject.Id == id).SingleAsync();

            if (subject.WasAccepted)
            {
                throw new InvalidOperationException("Subject with id " + id + " was already accepted");
            }
            subject.WasAccepted = true;
            await _korepetynderDbContext.SaveChangesAsync();

            return new SubjectResponse(subject.Id, subject.Name);
        }

        public async Task DeleteSubject(int id)
        {
            if (!await IsAdmin())
            {
                throw new PermissionDeniedException();
            }

            var subject = await _korepetynderDbContext.Subjects
                .Where(subject => subject.Id == id)
                .SingleAsync();
            _korepetynderDbContext.Remove(subject);
            await _korepetynderDbContext.SaveChangesAsync();
        }

        private async Task<bool> IsAdmin()
        {
            var id = GetCurrentUserId();

            return await _korepetynderDbContext.Users
                .Where(user => user.Id == id)
                .Select(user => user.IsAdmin)
                .SingleAsync();
        }
    }
}
