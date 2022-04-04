using System.ComponentModel.DataAnnotations.Schema;

namespace Korepetynder.Data.DbModels
{
    public class MultimediaFile
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public Subject? Subject { get; set; } //Some files are connected to particular subject

        [InverseProperty("ProfilePicture")]
        public Teacher? Owner { get; set; } //thats stupid but does not give warnings

        [InverseProperty("Pictures")]
        public Teacher? Owner1 { get; set; } //thats stupid but does not give warnings

        [InverseProperty("Videos")]
        public Teacher? Owner2 { get; set; } //thats stupid but does not give warnings
        public MultimediaFile(string url)
        {
            Url = url;
        }
    }
}
