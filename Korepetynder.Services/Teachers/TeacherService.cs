using Korepetynder.Contracts.Requests.Teachers;
using Korepetynder.Contracts.Responses.Students;
using Korepetynder.Contracts.Responses.Teachers;
using Korepetynder.Data;
using Korepetynder.Data.DbModels;
using Korepetynder.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Services.Teachers
{
    public class TeacherService : ITeacherService
    {
        private readonly KorepetynderDbContext _korepetynderDbContext;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TeacherService(KorepetynderDbContext korepetynderDbContext, ISieveProcessor sieveProcessor, IHttpContextAccessor httpContextAccessor)
        {
            _korepetynderDbContext = korepetynderDbContext;
            _sieveProcessor = sieveProcessor;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TeacherLessonResponse> AddLesson(TeacherLessonRequest request)
        {
            Guid currentId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var studentUser = await _korepetynderDbContext.Users.Where(user => user.Id == currentId).SingleAsync();
            if (studentUser.TeacherId is null)
            {
                throw new InvalidOperationException("User with id: " + currentId + " is not a teacher");
            }
            var subject = await _korepetynderDbContext.Subjects.Where(subject => subject.Id == request.SubjectId).SingleAsync();
            var levels = await _korepetynderDbContext.Levels.Where(level => request.LevelsIds.Contains(level.Id)).ToListAsync();
            var languages = await _korepetynderDbContext.Languages.Where(language => request.LanguagesIds.Contains(language.Id)).ToListAsync();
            if (levels.Count != request.LevelsIds.Count() || languages.Count != request.LanguagesIds.Count())
            {
                throw new InvalidOperationException("Provided incorrect id");
            }

            var lesson = new TeacherLesson();
            lesson.TeacherId = studentUser.TeacherId.Value;
            lesson.Frequency = request.Frequency;
            lesson.Subject = subject;
            lesson.Levels = levels;
            lesson.Languages = languages;
            lesson.Cost = request.Cost;
            await _korepetynderDbContext.TeacherLesson.AddAsync(lesson);
            await _korepetynderDbContext.SaveChangesAsync();

            return new TeacherLessonResponse(lesson);
        }

        public async Task DeleteLesson(int id)
        {
            Guid currentId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var currentUser = await _korepetynderDbContext.Users.Where(user => user.Id == currentId).SingleAsync();
            var lesson = await _korepetynderDbContext.TeacherLesson.Where(lesson => lesson.Id == id).SingleAsync();
            if (currentUser.TeacherId != lesson.TeacherId)
            {
                throw new ArgumentException("Lesson does not belong to teacher");
            }

            _korepetynderDbContext.TeacherLesson.Remove(lesson);
            await _korepetynderDbContext.SaveChangesAsync();
        }

        public async Task DeleteTeacher()
        {
            Guid currentId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var teacherUser = await _korepetynderDbContext.Users
                .Where(user => user.Id == currentId)
                .Include(user => user.Teacher)
                .SingleAsync();
            if (teacherUser.Teacher is null)
            {
                throw new InvalidOperationException("User with id: " + currentId + " is not a student");
            }
            _korepetynderDbContext.Teachers.Remove(teacherUser.Teacher);
            teacherUser.Teacher = null;
            teacherUser.TeacherId = null;
            _korepetynderDbContext.SaveChanges();
        }

        public async Task<PagedData<TeacherLessonResponse>> GetLessons(SieveModel model)
        {
            Guid currentId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var studentUser = await _korepetynderDbContext.Users.Where(user => user.Id == currentId).SingleAsync();
            if (studentUser.TeacherId is null)
            {
                throw new InvalidOperationException("User with id: " + currentId + " is not a teacher");
            }
            var userLessons = _korepetynderDbContext.TeacherLesson
                .Where(lesson => lesson.TeacherId == studentUser.TeacherId)
                .Include(lesson => lesson.Languages)
                .Include(lesson => lesson.Levels)
                .Include(lesson => lesson.Subject)
                .OrderBy(lesson => lesson.Id)
                .AsNoTracking();
            userLessons = _sieveProcessor.Apply(model, userLessons, applyPagination: false);

            var count = await userLessons.CountAsync();

            userLessons = _sieveProcessor.Apply(model, userLessons, applyFiltering: false, applySorting: false);

            return new PagedData<TeacherLessonResponse>(count, await userLessons
                .Select(lesson => new TeacherLessonResponse(lesson))
                .ToListAsync());
        }

        public async Task<TeacherResponse> GetTeacherData()
        {
            Guid currentId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var studentUser = await _korepetynderDbContext.Users
                .Where(user => user.Id == currentId)
                .Include(user => user.Teacher)
                .Include(user => user.Teacher!.TeachingLocations)
                .SingleAsync();
            if (studentUser.Teacher is null)
            {
                throw new InvalidOperationException("User with id: " + currentId + " already is not a student");
            }
            var teacher = studentUser.Teacher;
            return new TeacherResponse(teacher.Id, teacher.TeachingLocations.Select(location => location.Id));
        }

        public async Task<TeacherResponse> InitializeTeacher(TeacherRequest request)
        {
            Guid currentId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var teacherUser = await _korepetynderDbContext.Users.Where(user => user.Id == currentId).SingleAsync();
            if (teacherUser.TeacherId is not null)
            {
                throw new InvalidOperationException("User with id: " + currentId + " already is a teacher");
            }
            var teacher = new Teacher();
            var locations = await _korepetynderDbContext.Locations.Where(location => request.Locations.Contains(location.Id)).ToListAsync();
            if (locations.Count != request.Locations.Count())
            {
                throw new ArgumentException("Location does not exists");
            }
            teacher.TeachingLocations = locations;
            teacherUser.Teacher = teacher;
            _korepetynderDbContext.Teachers.Add(teacher);
            await _korepetynderDbContext.SaveChangesAsync();
            return new TeacherResponse(teacher.Id, locations.Select(location => location.Id));
        }

        public async Task<TeacherLessonResponse> UpdateLesson(int id, TeacherLessonRequest request)
        {
            Guid currentId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var teacherUser = await _korepetynderDbContext.Users.Where(user => user.Id == currentId).SingleAsync();
            if (teacherUser.TeacherId is null)
            {
                throw new InvalidOperationException("User with id: " + currentId + " is not a student");
            }
            var lesson = await _korepetynderDbContext.TeacherLesson
                .Where(lesson => lesson.Id == id)
                .Include(lesson => lesson.Subject)
                .Include(lesson => lesson.Levels)
                .Include(lesson => lesson.Languages)
                .SingleAsync();
            if (lesson.TeacherId != teacherUser.TeacherId)
            {
                throw new ArgumentException("Lesson does not belong to current user");
            }
            var subject = await _korepetynderDbContext.Subjects.Where(subject => subject.Id == request.SubjectId).SingleAsync();
            var levels = await _korepetynderDbContext.Levels.Where(level => request.LevelsIds.Contains(level.Id)).ToListAsync();
            var languages = await _korepetynderDbContext.Languages.Where(language => request.LanguagesIds.Contains(language.Id)).ToListAsync();
            if (levels.Count != request.LevelsIds.Count() || languages.Count != request.LanguagesIds.Count())
            {
                throw new InvalidOperationException("Provided incorrect id");
            }

            lesson.Frequency = request.Frequency;
            lesson.Subject = subject;
            lesson.Levels = levels;
            lesson.Languages = languages;
            lesson.Cost = request.Cost;
            await _korepetynderDbContext.SaveChangesAsync();

            return new TeacherLessonResponse(lesson);
        }

        public async Task<TeacherResponse> UpdateTeacher(TeacherRequest request)
        {
            Guid currentId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var teacherUser = await _korepetynderDbContext.Users
                .Where(user => user.Id == currentId)
                .Include(user => user.Teacher)
                .Include(user => user.Teacher!.TeachingLocations)
                .SingleAsync();
            if (teacherUser.TeacherId is null)
            {
                throw new InvalidOperationException("User with id: " + currentId + " is not a teacher");
            }
            var locations = await _korepetynderDbContext.Locations.Where(location => request.Locations.Contains(location.Id)).ToListAsync();
            if (locations.Count != request.Locations.Count())
            {
                throw new ArgumentException("Location does not exists");
            }
            var teacher = teacherUser.Teacher!;
            teacher.TeachingLocations = locations;
            await _korepetynderDbContext.SaveChangesAsync();
            return new TeacherResponse(teacher.Id, locations.Select(location => location.Id));
        }
    }
}
