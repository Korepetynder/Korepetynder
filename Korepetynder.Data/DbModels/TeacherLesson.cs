using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Data.DbModels
{
    public class TeacherLesson
    {
        public int Id { get; set; }

        public int Frequency { get; set; }
        public int Cost { get; set; }
        public int SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; } = null!;
        public ICollection<Level> Levels { get; set; } = new List<Level>();
        public ICollection<Language> Languages { get; set; } = new List<Language>();

        public int TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public Teacher Teacher { get; set; } = null!;
    }
}
