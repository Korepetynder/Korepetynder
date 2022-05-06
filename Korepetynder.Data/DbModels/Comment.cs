using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Data.DbModels
{
    public class Comment
    {
        public int Id { get; set; }
        [MaxLength(300)]
        public string Text { get; set; }
        public int Score { get; set; }  //number between 1 and 10

        public Guid CommentedTutorId { get; set; }

        [ForeignKey(nameof(CommentedTutorId))]
        public Tutor CommentedTutor = null!;
        public Comment(int score, string text)
        {
            Score = score;
            Text = text;
        }
    }
}
