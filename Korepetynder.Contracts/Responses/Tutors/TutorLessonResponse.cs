using Korepetynder.Contracts.Responses.Languages;
using Korepetynder.Contracts.Responses.Levels;
using Korepetynder.Contracts.Responses.Subjects;
using Korepetynder.Data.DbModels;

namespace Korepetynder.Contracts.Responses.Tutors
{
    public class TutorLessonResponse
    {
        public int Id { get; set; }
        public SubjectResponse Subject { get; set; }
        public int? Frequency { get; set; }
        public IEnumerable<LanguageResponse> Languages { get; set; }
        public IEnumerable<LevelResponse> Levels { get; set; }
        public int Cost { get; set; }

        public TutorLessonResponse(TutorLesson lesson)
        {
            Id = lesson.Id;
            Frequency = lesson.Frequency;
            Subject = new SubjectResponse(lesson.Subject);
            List<LanguageResponse> languages = new List<LanguageResponse>();
            foreach (var language in lesson.Languages)
            {
                languages.Add(new LanguageResponse(language));
            }
            Languages = languages;
            List<LevelResponse> levels = new List<LevelResponse>();
            foreach (var level in lesson.Levels)
            {
                levels.Add(new LevelResponse(level));
            }
            Levels = levels;
            Cost = lesson.Cost;
        }
    }
}
