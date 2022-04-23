namespace Korepetynder.Contracts.Requests.Students
{
    public class LessonCreationRequest
    {
        public int FrequencyId { get; set; }
        public int SubjectId { get; set; }
        public IEnumerable<int> LevelsIds { get; set; }
        public IEnumerable<int> LanguagesIds { get; set; }

        public LessonCreationRequest(int frequencyId, int subjectId, IEnumerable<int> levelsIds, IEnumerable<int> languagesIds)
        {
            FrequencyId = frequencyId;
            SubjectId = subjectId;
            LevelsIds = levelsIds;
            LanguagesIds = languagesIds;
        }

    }
}
