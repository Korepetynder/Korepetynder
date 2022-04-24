using System.ComponentModel.DataAnnotations.Schema;

namespace Korepetynder.Data.DbModels
{
    public class Teacher
    {
        public int Id { get; set; }
        public int Score { get; set; } //can be calculated using comments, however it is inefficient (number between 1 and 10, 0 if no comments)
        public User User { get; set; } = null!;
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<TeacherLesson> Lessons { get; set; } = new List<TeacherLesson>();

        public ICollection<Location> TeachingLocations { get; set; } = new List<Location>();

        public int? ProfilePictureId { get; set; }
        [ForeignKey(nameof(ProfilePictureId))]
        public ProfilePicture? ProfilePicture { get; set; }

        public ICollection<MultimediaFile> MultimediaFiles { get; set; } = new List<MultimediaFile>();

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj is Teacher other && other.Id == Id)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}

