using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Korepetynder.Data.DbModels
{
    public class Comment
    {
        public int Id { get; set; }
        [MaxLength(300)]
        public string Text { get; set; }
        public int Score { get; set; }  //number between 1 and 5

        public Guid CommentedTutorId { get; set; }

        [ForeignKey(nameof(CommentedTutorId))]
        public Tutor CommentedTutor = null!;
        public Comment(int score, string text, Guid commentedTutorId)
        {
            Score = score;
            Text = text;
            CommentedTutorId = commentedTutorId;
        }
    }
}
