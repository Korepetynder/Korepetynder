using System.ComponentModel.DataAnnotations.Schema;

namespace Korepetynder.Data.DbModels
{
    public class Lesson
    {
        public int Id { get; set; }

        public int LengthId { get; set; }
        [ForeignKey(nameof(LengthId))]
        public Length Length { get; set; } = null!;

        public int SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; } = null!;
        public ICollection<Level> Levels { get; set; } = new List<Level>();
        public ICollection<Language> Languages { get; set; } = new List<Language>();

        public int? StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student? Student { get; set; }

        public int? TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public Teacher? Teacher { get; set; }
    }
}
