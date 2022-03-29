namespace Korepetynder.Data.DbModels
{
    public class MultimediaFile
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public Subject? Subject { get; set; } //Some files are connected to particular subject

        public MultimediaFile(string url)
        {
            Url = url;
        }
    }
}
