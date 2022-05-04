using Korepetynder.Contracts.Requests.Tutors;
using Korepetynder.Contracts.Responses.Tutors;
using Korepetynder.Data;
using Korepetynder.Data.DbModels;
using Korepetynder.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace Korepetynder.Services.Tutors
{
    internal class TutorService : AdBaseService, ITutorService
    {
        private readonly KorepetynderDbContext _korepetynderDbContext;
        private readonly ISieveProcessor _sieveProcessor;

        public TutorService(KorepetynderDbContext korepetynderDbContext, ISieveProcessor sieveProcessor, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _korepetynderDbContext = korepetynderDbContext;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<TutorLessonResponse> AddLesson(TutorLessonRequest request)
        {
            Guid currentId = GetCurrentUserId();

            var isTutor = await _korepetynderDbContext.Tutors
                .AnyAsync(tutor => tutor.UserId == currentId);
            if (!isTutor)
            {
                throw new InvalidOperationException("User with id: " + currentId + " is not a tutor");
            }

            var subject = await _korepetynderDbContext.Subjects.Where(subject => subject.Id == request.SubjectId).SingleAsync();
            var levels = await _korepetynderDbContext.Levels.Where(level => request.LevelsIds.Contains(level.Id)).ToListAsync();
            var languages = await _korepetynderDbContext.Languages.Where(language => request.LanguagesIds.Contains(language.Id)).ToListAsync();
            if (levels.Count != request.LevelsIds.Count() || languages.Count != request.LanguagesIds.Count())
            {
                throw new InvalidOperationException("Provided incorrect id");
            }

            var lesson = new TutorLesson
            {
                TutorId = currentId,
                Frequency = request.Frequency,
                Subject = subject,
                Levels = levels,
                Languages = languages,
                Cost = request.Cost
            };

            await _korepetynderDbContext.TutorLessons.AddAsync(lesson);
            await _korepetynderDbContext.SaveChangesAsync();

            return new TutorLessonResponse(lesson);
        }

        public async Task DeleteLesson(int id)
        {
            Guid currentId = GetCurrentUserId();

            var currentUser = await _korepetynderDbContext.Users.Where(user => user.Id == currentId).SingleAsync();
            var lesson = await _korepetynderDbContext.TutorLessons.Where(lesson => lesson.Id == id).SingleAsync();
            if (currentId != lesson.TutorId)
            {
                throw new ArgumentException("Lesson does not belong to tutor");
            }

            _korepetynderDbContext.TutorLessons.Remove(lesson);
            await _korepetynderDbContext.SaveChangesAsync();
        }

        public async Task DeleteTutor()
        {
            Guid currentId = GetCurrentUserId();

            var tutor = await _korepetynderDbContext.Tutors
                .SingleAsync(tutor => tutor.UserId == currentId);

            _korepetynderDbContext.Tutors.Remove(tutor);
            await _korepetynderDbContext.SaveChangesAsync();
        }

        public async Task<PagedData<TutorLessonResponse>> GetLessons(SieveModel model)
        {
            Guid currentId = GetCurrentUserId();

            var isTutor = await _korepetynderDbContext.Tutors
                .AnyAsync(tutor => tutor.UserId == currentId);
            if (!isTutor)
            {
                throw new InvalidOperationException("User with id: " + currentId + " is not a tutor");
            }

            var userLessons = _korepetynderDbContext.TutorLessons
                .Where(lesson => lesson.TutorId == currentId)
                .Include(lesson => lesson.Languages)
                .Include(lesson => lesson.Levels)
                .Include(lesson => lesson.Subject)
                .OrderBy(lesson => lesson.Id)
                .AsNoTracking();

            userLessons = _sieveProcessor.Apply(model, userLessons, applyPagination: false);

            var count = await userLessons.CountAsync();

            userLessons = _sieveProcessor.Apply(model, userLessons, applyFiltering: false, applySorting: false);

            return new PagedData<TutorLessonResponse>(count, await userLessons
                .Select(lesson => new TutorLessonResponse(lesson))
                .ToListAsync());
        }

        public async Task<TutorResponse> GetTutorData()
        {
            Guid currentId = GetCurrentUserId();

            return await _korepetynderDbContext.Tutors
                .Where(tutor => tutor.UserId == currentId)
                .Select(tutor => new TutorResponse(tutor.UserId, tutor.TeachingLocations.Select(location => location.Id)))
                .SingleAsync();
        }

        public async Task<TutorResponse> InitializeTutor(TutorRequest request)
        {
            Guid currentId = GetCurrentUserId();

            var isTutor = await _korepetynderDbContext.Tutors
                .AnyAsync(tutor => tutor.UserId == currentId);
            if (isTutor)
            {
                throw new InvalidOperationException("User with id: " + currentId + " already is a tutor");
            }

            var locations = await _korepetynderDbContext.Locations.Where(location => request.Locations.Contains(location.Id)).ToListAsync();
            if (locations.Count != request.Locations.Count())
            {
                throw new ArgumentException("Location does not exists");
            }

            var tutor = new Tutor(currentId);
            tutor.TeachingLocations = locations;

            _korepetynderDbContext.Tutors.Add(tutor);
            await _korepetynderDbContext.SaveChangesAsync();

            return new TutorResponse(tutor.UserId, locations.Select(location => location.Id));
        }

        public async Task<TutorLessonResponse> UpdateLesson(int id, TutorLessonRequest request)
        {
            Guid currentId = GetCurrentUserId();

            var isTutor = await _korepetynderDbContext.Tutors
                .AnyAsync(tutor => tutor.UserId == currentId);
            if (!isTutor)
            {
                throw new InvalidOperationException("User with id: " + currentId + " is not a tutor");
            }

            var lesson = await _korepetynderDbContext.TutorLessons
                .Where(lesson => lesson.Id == id)
                .Include(lesson => lesson.Subject)
                .Include(lesson => lesson.Levels)
                .Include(lesson => lesson.Languages)
                .SingleAsync();
            if (lesson.TutorId != currentId)
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

            return new TutorLessonResponse(lesson);
        }

        public async Task<TutorResponse> UpdateTutor(TutorRequest request)
        {
            Guid currentId = GetCurrentUserId();

            var tutor = await _korepetynderDbContext.Tutors
                .Include(tutor => tutor.TeachingLocations)
                .SingleAsync(tutor => tutor.UserId == currentId);

            var locations = await _korepetynderDbContext.Locations.Where(location => request.Locations.Contains(location.Id)).ToListAsync();
            if (locations.Count != request.Locations.Count())
            {
                throw new ArgumentException("Location does not exists");
            }


            tutor.TeachingLocations = locations;
            await _korepetynderDbContext.SaveChangesAsync();

            return new TutorResponse(tutor.UserId, locations.Select(location => location.Id));
        }
    }
}
