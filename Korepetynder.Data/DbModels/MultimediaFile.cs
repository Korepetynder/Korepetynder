using System.ComponentModel.DataAnnotations.Schema;

namespace Korepetynder.Data.DbModels
{
    public enum MultimediaFileType
    {
        Picture,
        Video
    }

    public class MultimediaFile
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public MultimediaFileType Type { get; set; }

        public ICollection<TutorLesson> TutorLessons { get; set; } = new List<TutorLesson>();

        public Guid? TutorId { get; set; }
        [ForeignKey(nameof(TutorId))]
        public Tutor? Owner { get; set; }

        public MultimediaFile(string url)
        {
            Url = url;
        }
    }
}
