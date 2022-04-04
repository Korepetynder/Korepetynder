using System.ComponentModel.DataAnnotations.Schema;

namespace Korepetynder.Data.DbModels
{
    public class Teacher
    {
        public int Id { get; set; }
        public int Score { get; set; } //can be calculated using comments, however it is inefficient (number between 1 and 10, 0 if no comments)

        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<LessonData> LessonTypes { get; set; } = new List<LessonData>();

        public int Cost { get; set; }
        public int? PictureId { get; set; }
        public ICollection<Location> TeachingLocation { get; set; } = new List<Location>();

        [ForeignKey("PictureId")]
        public MultimediaFile? ProfilePicture { get; set; }
        public ICollection<MultimediaFile> Pictures { get; set; } = new List<MultimediaFile>();
        public ICollection<MultimediaFile> Videos { get; set; } = new List<MultimediaFile>();
    }
}
