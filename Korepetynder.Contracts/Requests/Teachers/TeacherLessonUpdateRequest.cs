using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Contracts.Requests.Teachers
{
    public class TeacherLessonUpdateRequest
    {
        public int Id { get; set; }
        public int Frequency { get; set; }
        public int SubjectId { get; set; }
        public int Cost { get; set; }
        public IEnumerable<int> LevelsIds { get; set; }
        public IEnumerable<int> LanguagesIds { get; set; }

        public TeacherLessonUpdateRequest(int id, int frequency, int subjectId, IEnumerable<int> levelsIds, IEnumerable<int> languagesIds, int cost)
        {
            Id = id;
            Cost = cost;
            Frequency = frequency;
            SubjectId = subjectId;
            LevelsIds = levelsIds;
            LanguagesIds = languagesIds;
        }
    }
}
