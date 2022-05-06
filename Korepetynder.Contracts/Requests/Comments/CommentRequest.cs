using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Contracts.Requests.Comments
{
    public class CommentRequest
    {
        public Guid CommentedTeacherId { get; set; }
        public int Score { get; set; } // number between 1 and 10

        [MaxLength(300)]
        public string Comment { get; set; }

        public CommentRequest(Guid teacherId, int score, string comment)
        {
            CommentedTeacherId = teacherId;
            Score = score;
            Comment = comment;
        }
    }
}
