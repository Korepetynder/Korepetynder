using Korepetynder.Contracts.Requests.Students;
using Korepetynder.Contracts.Responses.Students;
using Korepetynder.Data;
using Korepetynder.Data.DbModels;
using Korepetynder.Services.Models;
using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Korepetynder.Contracts.Responses.Languages;
using Korepetynder.Contracts.Responses.Levels;
using Microsoft.AspNetCore.Http;

namespace Korepetynder.Services.Students
{
    internal class StudentService : IStudentService
    {
        private readonly KorepetynderDbContext _korepetynderDbContext;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentService(KorepetynderDbContext korepetynderDbContext, ISieveProcessor sieveProcessor, IHttpContextAccessor httpContextAccessor)
        {
            _korepetynderDbContext = korepetynderDbContext;
            _sieveProcessor = sieveProcessor;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<StudentLessonResponse> AddLesson(LessonCreationRequest request)
        {
            Guid currentId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var studentUser = await _korepetynderDbContext.Users.Where(user => user.Id == currentId).SingleAsync();
            if (studentUser.StudentId is null)
            {
                throw new InvalidOperationException("User with id: " + currentId + " is not a student");
            }
            var frequency = await _korepetynderDbContext.Frequencies.Where(frequency => frequency.Id == request.FrequencyId).SingleAsync();
            var subject = await _korepetynderDbContext.Subjects.Where(subject => subject.Id == request.SubjectId).SingleAsync();
            var levels = await _korepetynderDbContext.Levels.Where(level => request.LevelsIds.Contains(level.Id)).ToListAsync();
            var languages = await _korepetynderDbContext.Languages.Where(language => request.LanguagesIds.Contains(language.Id)).ToListAsync();
            if (levels.Count != request.LevelsIds.Count() || languages.Count != request.LanguagesIds.Count())
            {
                throw new InvalidOperationException("Provided incorrect id");
            }

            var lesson = new StudentLesson();
            lesson.StudentId = studentUser.StudentId.Value;
            lesson.Frequency = frequency;
            lesson.Subject = subject;
            lesson.Levels = levels;
            lesson.Languages = languages;
            await _korepetynderDbContext.StudentLesson.AddAsync(lesson);
            await _korepetynderDbContext.SaveChangesAsync();

            return new StudentLessonResponse(lesson);
        }

        public async Task<StudentResponse> InitializeStudent(StudentCreationRequest request)
        {
            Guid currentId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var studentUser = await _korepetynderDbContext.Users.Where(user => user.Id == currentId).SingleAsync();
            if (studentUser.Student is not null)
            {
                throw new InvalidOperationException("User with id: " + currentId + " already is a student");
            }
            var student = new Student
            {
                PreferredCostMinimum = request.MinimalCost,
                PreferredCostMaximum = request.MaximalCost,
            };
            var locations = await _korepetynderDbContext.Locations.Where(location => request.Locations.Contains(location.Id)).ToListAsync();
            if (locations.Count != request.Locations.Count())
            {
                throw new ArgumentException("Location does not exists");
            }
            student.PreferredLocations = locations;
            studentUser.Student = student;
            _korepetynderDbContext.Students.Add(student);
            await _korepetynderDbContext.SaveChangesAsync();
            return new StudentResponse(student.Id, student.PreferredCostMinimum, student.PreferredCostMaximum, locations.Select(location => location.Id));
        }

        public async Task<PagedData<StudentLessonResponse>> GetLessons(SieveModel model)
        {
            Guid currentId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var studentUser = await _korepetynderDbContext.Users.Where(user => user.Id == currentId).SingleAsync();
            if (studentUser.StudentId is null)
            {
                throw new InvalidOperationException("User with id: " + currentId + " is not a student");
            }
            var userLessons =  _korepetynderDbContext.StudentLesson
                .Where(lesson => lesson.StudentId == studentUser.StudentId)
                .Include(lesson => lesson.Frequency)
                .Include(lesson => lesson.Languages)
                .Include(lesson => lesson.Levels)
                .OrderBy(lesson => lesson.Id)
                .AsNoTracking();
            userLessons = _sieveProcessor.Apply(model, userLessons, applyPagination: false);

            var count = await userLessons.CountAsync();

            userLessons = _sieveProcessor.Apply(model, userLessons, applyFiltering: false, applySorting: false);

            return new PagedData<StudentLessonResponse>(count, await userLessons
                .Select(lesson => new StudentLessonResponse(lesson))
                .ToListAsync());
        }
    }
}
