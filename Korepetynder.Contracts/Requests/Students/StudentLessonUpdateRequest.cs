namespace Korepetynder.Contracts.Requests.Students
{
    public class StudentLessonUpdateRequest
    {
        public int Id { get; set; }
        public int Frequency { get; set; }
        public int SubjectId { get; set; }
        public int MinimalCost { get; set; }
        public int MaximalCost { get; set; }
        public IEnumerable<int> LevelsIds { get; set; }
        public IEnumerable<int> LanguagesIds { get; set; }

        public StudentLessonUpdateRequest(int id, int frequency, int subjectId, IEnumerable<int> levelsIds, IEnumerable<int> languagesIds, int minimalCost, int maximalCost)
        {
            Id = id;
            MinimalCost = minimalCost;
            MaximalCost = maximalCost;
            Frequency = frequency;
            SubjectId = subjectId;
            LevelsIds = levelsIds;
            LanguagesIds = languagesIds;
        }

    }
}