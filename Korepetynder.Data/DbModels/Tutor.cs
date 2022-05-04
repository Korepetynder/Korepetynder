using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Korepetynder.Data.DbModels
{
    public class Tutor
    {
        [Key]
        public Guid UserId { get; set; }
        public int Score { get; set; } //can be calculated using comments, however it is inefficient (number between 1 and 10, 0 if no comments)

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
        public ICollection<Student> DiscardedByStudents { get; set; } = new List<Student>();
        public ICollection<Student> FavouritedByStudents { get; set; } = new List<Student>();

        public ICollection<TutorLesson> Lessons { get; set; } = new List<TutorLesson>();

        public ICollection<Location> TeachingLocations { get; set; } = new List<Location>();

        public int? ProfilePictureId { get; set; }
        [ForeignKey(nameof(ProfilePictureId))]
        public ProfilePicture? ProfilePicture { get; set; }

        public ICollection<MultimediaFile> MultimediaFiles { get; set; } = new List<MultimediaFile>();

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj is Tutor other && other.UserId == UserId)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return UserId.GetHashCode();
        }

        public Tutor(Guid userId)
        {
            UserId = userId;
        }
    }
}

