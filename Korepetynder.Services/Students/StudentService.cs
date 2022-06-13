using Korepetynder.Contracts.Requests.Students;
using Korepetynder.Contracts.Responses.Students;
using Korepetynder.Contracts.Responses.Tutors;
using Korepetynder.Data;
using Korepetynder.Data.DbModels;
using Korepetynder.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace Korepetynder.Services.Students
{
    internal class StudentService : AdBaseService, IStudentService
    {
        private readonly KorepetynderDbContext _korepetynderDbContext;
        private readonly ISieveProcessor _sieveProcessor;

        public StudentService(KorepetynderDbContext korepetynderDbContext, ISieveProcessor sieveProcessor, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _korepetynderDbContext = korepetynderDbContext;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<StudentLessonResponse> AddLesson(StudentLessonRequest request)
        {
            if (!request.LevelsIds.Any() || !request.LanguagesIds.Any())
            {
                throw new InvalidOperationException("Wrong number of arguments");
            }
            Guid currentId = GetCurrentUserId();

            var isStudent = await _korepetynderDbContext.Students
                .AnyAsync(student => student.UserId == currentId);
            if (!isStudent)
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

            var lesson = new StudentLesson
            {
                StudentId = currentId,
                Frequency = request.Frequency,
                Subject = subject,
                Levels = levels,
                Languages = languages,
                PreferredCostMinimum = request.PreferredCostMinimum,
                PreferredCostMaximum = request.PreferredCostMaximum
            };

            await _korepetynderDbContext.StudentLessons.AddAsync(lesson);
            await _korepetynderDbContext.SaveChangesAsync();

            return new StudentLessonResponse(lesson);
        }

        public async Task DeleteLesson(int id)
        {
            Guid currentId = GetCurrentUserId();

            var lesson = await _korepetynderDbContext.StudentLessons
                .SingleAsync(lesson => lesson.Id == id);
            if (currentId != lesson.StudentId)
            {
                throw new ArgumentException("Lesson does not belong to student");
            }

            _korepetynderDbContext.StudentLessons.Remove(lesson);
            await _korepetynderDbContext.SaveChangesAsync();
        }

        public async Task<StudentResponse> InitializeStudent(StudentRequest request)
        {
            if (!request.Locations.Any())
            {
                throw new InvalidOperationException("No location selected");
            }
            Guid currentId = GetCurrentUserId();

            var isStudent = await _korepetynderDbContext.Students
                .AnyAsync(student => student.UserId == currentId);
            if (isStudent)
            {
                throw new InvalidOperationException("User with id: " + currentId + " already is a student");
            }

            var locations = await _korepetynderDbContext.Locations.Where(location => request.Locations.Contains(location.Id)).ToListAsync();
            if (locations.Count != request.Locations.Count())
            {
                throw new ArgumentException("Location does not exists");
            }

            var student = new Student(currentId);
            student.PreferredLocations = locations;

            _korepetynderDbContext.Students.Add(student);
            await _korepetynderDbContext.SaveChangesAsync();
            return new StudentResponse(student.UserId, locations.Select(location => location.Id));
        }

        public async Task<StudentResponse> UpdateStudent(StudentRequest request)
        {
            Guid currentId = GetCurrentUserId();

            var student = await _korepetynderDbContext.Students
                .Include(student => student.PreferredLocations)
                .SingleAsync(student => student.UserId == currentId);

            var locations = await _korepetynderDbContext.Locations
                .Where(location => request.Locations.Contains(location.Id))
                .ToListAsync();
            if (locations.Count != request.Locations.Count())
            {
                throw new ArgumentException("Location does not exists");
            }

            student.PreferredLocations = locations;
            await _korepetynderDbContext.SaveChangesAsync();

            return new StudentResponse(student.UserId, locations.Select(location => location.Id));
        }

        public async Task<StudentResponse> GetStudentData()
        {
            Guid currentId = GetCurrentUserId();

            return await _korepetynderDbContext.Students
                .Where(student => student.UserId == currentId)
                .Select(student => new StudentResponse(student.UserId, student.PreferredLocations.Select(location => location.Id)))
                .SingleAsync();
        }
        public async Task<PagedData<StudentLessonResponse>> GetLessons(SieveModel model)
        {
            Guid currentId = GetCurrentUserId();

            var isStudent = await _korepetynderDbContext.Students
                .AnyAsync(student => student.UserId == currentId);
            if (!isStudent)
            {
                throw new InvalidOperationException("User with id: " + currentId + " is not a student");
            }

            var userLessons = _korepetynderDbContext.StudentLessons
                .Where(lesson => lesson.StudentId == currentId)
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
            Guid currentId = GetCurrentUserId();

            var student = await _korepetynderDbContext.Students
                .SingleAsync(student => student.UserId == currentId);

            _korepetynderDbContext.Students.Remove(student);
            _korepetynderDbContext.SaveChanges();
        }

        public async Task<IEnumerable<TutorDataResponse>> GetSuggestedTutors()
        {
            Guid currentId = GetCurrentUserId();

            var studentUser = await _korepetynderDbContext.Users
                .Where(user => user.Id == currentId)
                .Include(user => user.Student)
                .Include(user => user.Student!.PreferredLocations)
                .Include(user => user.Student!.PreferredLessons)
                .ThenInclude(lesson => lesson.Subject)
                .Include(user => user.Student!.PreferredLessons)
                .ThenInclude(lesson => lesson.Languages)
                .Include(user => user.Student!.PreferredLessons)
                .ThenInclude(lesson => lesson.Levels)
                .SingleAsync();
            if (studentUser.Student is null)
            {
                throw new InvalidOperationException("User with id: " + currentId + " is not a student");
            }

            IEnumerable<TutorLesson> allLessons = new LinkedList<TutorLesson>();
            foreach (var searchLesson in studentUser.Student.PreferredLessons)
            {
                var lessons = await _korepetynderDbContext.TutorLessons
                    .Where(lesson => lesson.Cost >= searchLesson.PreferredCostMinimum && lesson.Cost <= searchLesson.PreferredCostMaximum)
                    .Where(lesson => lesson.Subject == searchLesson.Subject)
                    .Where(lesson => searchLesson.Frequency == null || lesson.Frequency >= searchLesson.Frequency)
                    .Where(lesson => lesson.Tutor.TeachingLocations.Any(el => studentUser.Student.PreferredLocations.Contains(el)))
                    .Where(lesson => lesson.Languages.Any(el => searchLesson.Languages.Contains(el)))
                    .Where(lesson => lesson.Levels.Any(el => searchLesson.Levels.Contains(el)))
                    .Include(lesson => lesson.Tutor)
                    .Include(lesson => lesson.Languages)
                    .Include(lesson => lesson.Levels)
                    .Include(lesson => lesson.Tutor.User)
                    .Include(lesson => lesson.Tutor.TeachingLocations)
                    .OrderBy(lesson => lesson.Cost)
                    .ToListAsync();
                allLessons = allLessons.Concat(lessons);
            }

            var tutors = new HashSet<Tutor>();
            foreach (var lesson in allLessons)
            {
                tutors.Add(lesson.Tutor);
            }
            return tutors.Select(tutor => new TutorDataResponse(tutor.User)).ToList();
        }

        public async Task<StudentLessonResponse> UpdateLesson(int id, StudentLessonRequest request)
        {
            Guid currentId = GetCurrentUserId();

            var isStudent = await _korepetynderDbContext.Students
                .AnyAsync(student => student.UserId == currentId);
            if (!isStudent)
            {
                throw new InvalidOperationException("User with id: " + currentId + " is not a student");
            }

            var lesson = await _korepetynderDbContext.StudentLessons
                .Where(lesson => lesson.Id == id)
                .Include(lesson => lesson.Subject)
                .Include(lesson => lesson.Levels)
                .Include(lesson => lesson.Languages)
                .SingleAsync();
            if (lesson.StudentId != currentId)
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
            lesson.PreferredCostMinimum = request.PreferredCostMinimum;
            lesson.PreferredCostMaximum = request.PreferredCostMaximum;
            await _korepetynderDbContext.SaveChangesAsync();

            return new StudentLessonResponse(lesson);
        }

        public async Task AddFavouriteTutor(Guid id)
        {
            Guid currentId = GetCurrentUserId();
            var student = await _korepetynderDbContext.Students.Where(student => student.UserId == currentId)
                .Include(student => student.FavouriteTutors)
                .SingleAsync();
            var tutor = await _korepetynderDbContext.Tutors.Where(tutor => tutor.UserId == id).SingleAsync();
            student.FavouriteTutors.Add(tutor);
            await _korepetynderDbContext.SaveChangesAsync();
        }

        public async Task DeleteFavouriteTutor(Guid id)
        {
            Guid currentId = GetCurrentUserId();
            var student = await _korepetynderDbContext.Students.Where(student => student.UserId == currentId)
                .Include(student => student.FavouriteTutors)
                .SingleAsync();
            var tutor = await _korepetynderDbContext.Tutors.Where(tutor => tutor.UserId == id).SingleAsync();
            student.FavouriteTutors.Remove(tutor);
            await _korepetynderDbContext.SaveChangesAsync();
        }

        public async Task<PagedData<TutorDataResponse>> GetFavouriteTutors(SieveModel model)
        {
            Guid currentId = GetCurrentUserId();

            var student = await _korepetynderDbContext.Students
                .Where(student => student.UserId == currentId)
                .SingleAsync();

            var favouriteTutors = _korepetynderDbContext.Users
                .Where(user => user.Tutor!.FavouritedByStudents.Contains(student))
                .Include(user => user.Tutor!.TutorLessons)
                .ThenInclude(lesson => lesson.Subject)
                .Include(user => user.Tutor!.TutorLessons)
                .ThenInclude(lesson => lesson.Levels)
                .Include(user => user.Tutor!.TutorLessons)
                .ThenInclude(lesson => lesson.Languages)
                .AsNoTracking();
            favouriteTutors = _sieveProcessor.Apply(model, favouriteTutors, applyPagination: false);

            var count = await favouriteTutors.CountAsync();

            favouriteTutors = _sieveProcessor.Apply(model, favouriteTutors, applyFiltering: false, applySorting: false);

            return new PagedData<TutorDataResponse>(count, await favouriteTutors
                .Select(tutor => new TutorDataResponse(tutor))
                .ToListAsync());
        }
    }
}
