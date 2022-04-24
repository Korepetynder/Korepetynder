namespace Korepetynder.Contracts.Requests.Students
{
    public class LessonCreationRequest
    {
        public int Frequency { get; set; }
        public int SubjectId { get; set; }
        public IEnumerable<int> LevelsIds { get; set; }
        public IEnumerable<int> LanguagesIds { get; set; }

        public LessonCreationRequest(int frequency, int subjectId, IEnumerable<int> levelsIds, IEnumerable<int> languagesIds)
        {
            Frequency = frequency;
            SubjectId = subjectId;
            LevelsIds = levelsIds;
            LanguagesIds = languagesIds;
        }

    }
}
