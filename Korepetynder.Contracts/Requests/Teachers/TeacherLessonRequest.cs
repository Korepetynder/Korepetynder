using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Contracts.Requests.Teachers
{
    public class TeacherLessonRequest
    {
        public int Frequency { get; set; }
        public int SubjectId { get; set; }
        public int Cost { get; set; }
        public IEnumerable<int> LevelsIds { get; set; }
        public IEnumerable<int> LanguagesIds { get; set; }

        public TeacherLessonRequest(int frequency, int subjectId, IEnumerable<int> levelsIds, IEnumerable<int> languagesIds, int cost)
        {
            Cost = cost;
            Frequency = frequency;
            SubjectId = subjectId;
            LevelsIds = levelsIds;
            LanguagesIds = languagesIds;
        }
    }
}
