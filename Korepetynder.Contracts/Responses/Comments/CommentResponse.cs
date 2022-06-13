using Korepetynder.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Contracts.Responses.Comments
{
    public class CommentResponse
    {
        public Guid TutorId { get; set; }
        public int Score { get; set; }
        public string Text { get; set; }

        public CommentResponse(Comment comment)
        {
            Score = comment.Score;
            Text = comment.Text;
            TutorId = comment.CommentedTutorId;
        }
    }
}
