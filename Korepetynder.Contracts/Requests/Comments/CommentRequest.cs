using System.ComponentModel.DataAnnotations;

namespace Korepetynder.Contracts.Requests.Comments
{
    public class CommentRequest
    {
        [Range(1, 5)]
        public int Score { get; set; }

        [MaxLength(300)]
        public string Comment { get; set; }

        public CommentRequest(int score, string comment)
        {
            Score = score;
            Comment = comment;
        }
    }
}
