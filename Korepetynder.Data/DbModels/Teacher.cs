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
        public ICollection<Location> TeachingLocation { get; set; } = new List<Location>();

        public int? ProfilePictureId { get; set; }
        [ForeignKey(nameof(ProfilePicture))]
        public ProfilePicture? ProfilePicture { get; set; }

        public ICollection<MultimediaFile> MultimediaFiles { get; set; } = new List<MultimediaFile>();
    }
}

