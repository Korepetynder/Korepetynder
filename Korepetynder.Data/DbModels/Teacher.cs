using System.ComponentModel.DataAnnotations.Schema;

namespace Korepetynder.Data.DbModels
{
    public class Teacher
    {
        public int Id { get; set; }
        public int Score { get; set; } //can be calculated using comments, however it is inefficient (number between 1 and 10, 0 if no comments)

        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Subject> TaughtSubjects { get; set; } = new List<Subject>();

        public int? ProfilePictureId { get; set; }
        [ForeignKey(nameof(ProfilePicture))]
        public ProfilePicture? ProfilePicture { get; set; }

        public ICollection<MultimediaFile> MultimediaFiles { get; set; } = new List<MultimediaFile>();
    }
}

