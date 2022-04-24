using System.ComponentModel.DataAnnotations.Schema;

namespace Korepetynder.Data.DbModels
{
    public class StudentLesson
    {
        public int Id { get; set; }
        public int PreferredCostMinimum { get; set; }
        public int PreferredCostMaximum { get; set; }
        public int? FrequencyId { get; set; }
        [ForeignKey(nameof(FrequencyId))]
        public int? Frequency { get; set; } = null!;

        public int SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; } = null!;
        public ICollection<Level> Levels { get; set; } = new List<Level>();
        public ICollection<Language> Languages { get; set; } = new List<Language>();

        public int StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; } = null!;
    }
}
