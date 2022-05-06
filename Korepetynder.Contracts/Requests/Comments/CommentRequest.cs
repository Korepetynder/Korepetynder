using System.ComponentModel.DataAnnotations;

namespace Korepetynder.Contracts.Requests.Comments
{
    public class CommentRequest
    {
        public Guid CommentedTeacherId { get; set; }
        public int Score { get; set; } // number between 1 and 10

        [MaxLength(300)]
        public string Comment { get; set; }

        public CommentRequest(Guid commentedTeacherId, int score, string comment)
        {
            CommentedTeacherId = commentedTeacherId;
            Score = score;
            Comment = comment;
        }
    }
}
