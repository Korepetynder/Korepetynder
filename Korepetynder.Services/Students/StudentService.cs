using Korepetynder.Contracts.Requests.Students;
using Korepetynder.Contracts.Responses.Students;
using Korepetynder.Data;
using Korepetynder.Data.DbModels;
using Korepetynder.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

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
            var subject = await _korepetynderDbContext.Subjects.Where(subject => subject.Id == request.SubjectId).SingleAsync();
            var levels = await _korepetynderDbContext.Levels.Where(level => request.LevelsIds.Contains(level.Id)).ToListAsync();
            var languages = await _korepetynderDbContext.Languages.Where(language => request.LanguagesIds.Contains(language.Id)).ToListAsync();
            if (levels.Count != request.LevelsIds.Count() || languages.Count != request.LanguagesIds.Count())
            {
                throw new InvalidOperationException("Provided incorrect id");
            }

            var lesson = new StudentLesson();
            lesson.StudentId = studentUser.StudentId.Value;
            lesson.Frequency = request.Frequency;
            lesson.Subject = subject;
            lesson.Levels = levels;
            lesson.Languages = languages;
            await _korepetynderDbContext.StudentLesson.AddAsync(lesson);
            await _korepetynderDbContext.SaveChangesAsync();

            return new StudentLessonResponse(lesson);
        }

        public async Task DeleteLesson(int id)
        {
            Guid currentId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var currentUser = await _korepetynderDbContext.Users.Where(user => user.Id == currentId).SingleAsync();
            var lesson = await _korepetynderDbContext.StudentLesson.Where(lesson => lesson.Id == id).SingleAsync();
            if (currentUser.StudentId != lesson.StudentId)
            {
                throw new ArgumentException("Lesson does not belong to student");
            }

            _korepetynderDbContext.StudentLesson.Remove(lesson);
            await _korepetynderDbContext.SaveChangesAsync();
        }

        public async Task<StudentResponse> InitializeStudent(StudentRequest request)
        {
            Guid currentId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var studentUser = await _korepetynderDbContext.Users.Where(user => user.Id == currentId).SingleAsync();
            if (studentUser.StudentId is not null)
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

        public async Task<StudentResponse> UpdateStudent(StudentRequest request)
        {
            Guid currentId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var studentUser = await _korepetynderDbContext.Users
                .Where(user => user.Id == currentId)
                .Include(user => user.Student!)
                    .ThenInclude(student => student.PreferredLocations)
                .SingleAsync();
            if (studentUser.StudentId is null)
            {
                throw new InvalidOperationException("User with id: " + currentId + " already is not a student");
            }
            var locations = await _korepetynderDbContext.Locations.Where(location => request.Locations.Contains(location.Id)).ToListAsync();
            if (locations.Count != request.Locations.Count())
            {
                throw new ArgumentException("Location does not exists");
            }
            var student = studentUser.Student!;
            student.PreferredCostMinimum = request.MinimalCost;
            student.PreferredCostMaximum = request.MaximalCost;
            student.PreferredLocations = locations;
            await _korepetynderDbContext.SaveChangesAsync();
            return new StudentResponse(student.Id, student.PreferredCostMinimum, student.PreferredCostMaximum, locations.Select(location => location.Id));
        }
        public async Task<StudentResponse> GetStudentData()
        {
            Guid currentId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var studentUser = await _korepetynderDbContext.Users
                .Where(user => user.Id == currentId)
                .Include(user => user.Student)
                .Include(user => user.Student!.PreferredLocations)
                .SingleAsync();
            if (studentUser.Student is null)
            {
                throw new InvalidOperationException("User with id: " + currentId + " already is not a student");
            }
            var student = studentUser.Student;
            return new StudentResponse(student.Id, student.PreferredCostMinimum, student.PreferredCostMaximum, student.PreferredLocations.Select(location => location.Id));
        }
        public async Task<PagedData<StudentLessonResponse>> GetLessons(SieveModel model)
        {
            Guid currentId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var studentUser = await _korepetynderDbContext.Users.Where(user => user.Id == currentId).SingleAsync();
            if (studentUser.StudentId is null)
            {
                throw new InvalidOperationException("User with id: " + currentId + " is not a student");
            }
            var userLessons = _korepetynderDbContext.StudentLesson
                .Where(lesson => lesson.StudentId == studentUser.StudentId)
                .Include(lesson => lesson.Frequency)
                .Include(lesson => lesson.Languages)
                .Include(lesson => lesson.Levels)
                .Include(lesson => lesson.Subject)
                .OrderBy(lesson => lesson.Id)
                .AsNoTracking();
            userLessons = _sieveProcessor.Apply(model, userLessons, applyPagination: false);

            var count = await userLessons.CountAsync();

            userLessons = _sieveProcessor.Apply(model, userLessons, applyFiltering: false, applySorting: false);

            return new PagedData<StudentLessonResponse>(count, await userLessons
                .Select(lesson => new StudentLessonResponse(lesson))
                .ToListAsync());
        }

        public async Task DeleteStudent()
        {
            Guid currentId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var studentUser = await _korepetynderDbContext.Users
                .Where(user => user.Id == currentId)
                .Include(user => user.Student)
                .SingleAsync();
            if (studentUser.Student is null)
            {
                throw new InvalidOperationException("User with id: " + currentId + " is not a student");
            }
            _korepetynderDbContext.Students.Remove(studentUser.Student);
            studentUser.Student = null;
            studentUser.StudentId = null;
            _korepetynderDbContext.SaveChanges();

        }

        public async Task<IEnumerable<TeacherDataResponse>> GetSuggestedTeachers()
        {
            Guid currentId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value!);
            var studentUser = await _korepetynderDbContext.Users
                .Where(user => user.Id == currentId)
                .Include(user => user.Student)
                .Include(user => user.Student!.PreferredLocations)
                .Include(user => user.Student!.PreferredLessons)
                .ThenInclude(lesson => lesson.Languages)
                .Include(user => user.Student!.PreferredLessons)
                .ThenInclude(lesson => lesson.Levels)
                .SingleAsync();
            if (studentUser.Student is null)
            {
                throw new InvalidOperationException("User with id: " + currentId + " is not a student");
            }
            var allLessons = new LinkedList<TeacherLesson>();
            foreach (var searchLesson in studentUser.Student.PreferredLessons) {
                var lessons = await _korepetynderDbContext.TeacherLesson
                .Where(lesson => lesson.Cost >= studentUser.Student.PreferredCostMinimum && lesson.Cost <= studentUser.Student.PreferredCostMaximum)
                .Where(lesson => lesson.Subject == searchLesson.Subject)
                .Where(lesson => searchLesson.Frequency == null || lesson.Frequency >= searchLesson.Frequency)
                .Where(lesson => lesson.Teacher.TeachingLocations.Any(el => studentUser.Student.PreferredLocations.Contains(el)))
                .Where(lesson => lesson.Languages.Any(el => searchLesson.Languages.Contains(el)))
                .Where(lesson => lesson.Levels.Any(el => searchLesson.Levels.Contains(el)))
                .Include(lesson => lesson.Teacher)
                .Include(lesson => lesson.Teacher.User)
                .OrderBy(lesson => lesson.Cost).ToListAsync();
                allLessons.Concat(lessons);
            }
            var teachers = new HashSet<Teacher>();
            foreach (var lesson in allLessons) {
                teachers.Add(lesson.Teacher);
            }
            return teachers.Select(teacher => new TeacherDataResponse(teacher.User)).ToList();
        }
    }
}
