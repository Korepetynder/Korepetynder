using System.ComponentModel.DataAnnotations;

namespace Korepetynder.Contracts.Requests.Comments
{
    public class CommentRequest
    {
        public Guid CommentedTutorId { get; set; }

        [Range(1, 10)]
        public int Score { get; set; } // number between 1 and 10

        [MaxLength(300)]
        public string Comment { get; set; }

        public CommentRequest(Guid commentedTutorId, int score, string comment)
        {
            CommentedTutorId = commentedTutorId;
            Score = score;
            Comment = comment;
        }
    }
}
