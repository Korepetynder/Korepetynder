using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Data.DbModels
{
    public class LessonData
    {
        public int Id { get; set; }
        public Length? LessonLength { get; set; }
        public Subject? Subject { get; set; }
        public ICollection<Level> Level { get; set; } = new List<Level>();
        public ICollection<Language> Language { get; set; } = new List<Language>();
    }
}
