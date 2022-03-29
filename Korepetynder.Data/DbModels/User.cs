using System.ComponentModel.DataAnnotations.Schema;

namespace Korepetynder.Data.DbModels
{
    public class User
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public int? TeacherId { get; set; }
        public int? StudentId { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher? Teacher { get; set; }

        [ForeignKey("StudentId")]
        public Student? Student { get; set; }
    }
}
