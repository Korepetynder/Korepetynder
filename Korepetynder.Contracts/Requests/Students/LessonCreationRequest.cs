using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Contracts.Requests.Students
{
    public class LessonCreationRequest
    {
        public int FrequencyId { get; set; }
        public int SubjectId { get; set; }
        public IEnumerable<int> LevelsIds { get; set; }
        public IEnumerable<int> LanguagesIds { get; set; }

        public LessonCreationRequest(int frequencyId, int subjectId, IEnumerable<int> levelsIds, IEnumerable<int> languageIds)
        {
            FrequencyId = frequencyId;
            SubjectId = subjectId;
            LevelsIds = levelsIds;
            LanguagesIds = languageIds;
        }

    }
}
