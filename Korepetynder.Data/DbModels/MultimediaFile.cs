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
        public Subject? Subject { get; set; } //Some files are connected to particular subject

        public int TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public Teacher Owner { get; set; } = null!;

        public MultimediaFile(string url)
        {
            Url = url;
        }
    }
}
