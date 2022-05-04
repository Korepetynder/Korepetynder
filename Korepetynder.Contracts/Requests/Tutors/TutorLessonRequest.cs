namespace Korepetynder.Contracts.Requests.Tutors
{
    public class TutorLessonRequest
    {
        public int Frequency { get; set; }
        public int SubjectId { get; set; }
        public int Cost { get; set; }
        public IEnumerable<int> LevelsIds { get; set; }
        public IEnumerable<int> LanguagesIds { get; set; }

        public TutorLessonRequest(int frequency, int subjectId, IEnumerable<int> levelsIds, IEnumerable<int> languagesIds, int cost)
        {
            Cost = cost;
            Frequency = frequency;
            SubjectId = subjectId;
            LevelsIds = levelsIds;
            LanguagesIds = languagesIds;
        }
    }
}
