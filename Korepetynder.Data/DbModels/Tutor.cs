using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Korepetynder.Data.DbModels
{
    public class Tutor
    {
        [Key]
        public Guid UserId { get; set; }

        [Precision(3, 1)]
        public decimal Score { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
        public ICollection<Student> DiscardedByStudents { get; set; } = new List<Student>();
        public ICollection<Student> FavoritedByStudents { get; set; } = new List<Student>();

        public ICollection<TutorLesson> TutorLessons { get; set; } = new List<TutorLesson>();

        public ICollection<Location> TeachingLocations { get; set; } = new List<Location>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

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

