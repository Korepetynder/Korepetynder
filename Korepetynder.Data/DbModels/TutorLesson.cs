using System.ComponentModel.DataAnnotations.Schema;

namespace Korepetynder.Data.DbModels
{
    public class TutorLesson
    {
        public int Id { get; set; }

        public int Frequency { get; set; }
        public int Cost { get; set; }
        public int SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; } = null!;
        public ICollection<Level> Levels { get; set; } = new List<Level>();
        public ICollection<Language> Languages { get; set; } = new List<Language>();

        public Guid TutorId { get; set; }
        [ForeignKey(nameof(TutorId))]
        public Tutor Tutor { get; set; } = null!;
    }
}
