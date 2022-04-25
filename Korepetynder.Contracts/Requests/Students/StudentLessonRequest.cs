namespace Korepetynder.Contracts.Requests.Students
{
    public class StudentLessonRequest
    {
        public int? Frequency { get; set; }
        public int SubjectId { get; set; }
        public int PreferredCostMinimum { get; set; }
        public int PreferredCostMaximum { get; set; }
        public IEnumerable<int> LevelsIds { get; set; }
        public IEnumerable<int> LanguagesIds { get; set; }

        public StudentLessonRequest(int? frequency, int subjectId, IEnumerable<int> levelsIds, IEnumerable<int> languagesIds, int preferredCostMinimum, int preferredCostMaximum)
        {
            PreferredCostMinimum = preferredCostMinimum;
            PreferredCostMaximum = preferredCostMaximum;
            Frequency = frequency;
            SubjectId = subjectId;
            LevelsIds = levelsIds;
            LanguagesIds = languagesIds;
        }

    }
}
