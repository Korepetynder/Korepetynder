using Korepetynder.Contracts.Responses.Frequencies;
using Korepetynder.Contracts.Responses.Languages;
using Korepetynder.Contracts.Responses.Levels;
using Korepetynder.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Contracts.Responses.Students
{
    public class StudentLessonResponse
    {
        public int Id { get; set; }
        public FrequencyResponse? Frequency { get; set; }
        public IEnumerable<LanguageResponse> Languages { get; set; }
        public IEnumerable<LevelResponse> Levels { get; set; }

        public StudentLessonResponse(StudentLesson lesson)
        {
            Id = lesson.Id;
            if (lesson.Frequency != null)
                Frequency = new FrequencyResponse(lesson.Frequency);
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
        }

        public StudentLessonResponse(int id, FrequencyResponse frequency, IEnumerable<LanguageResponse> languages, IEnumerable<LevelResponse> levels)
        {
            Id = id;
            Frequency = frequency;
            Languages = languages;
            Levels = levels;
        }
    }
}
