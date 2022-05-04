namespace Korepetynder.Data.DbModels
{
    public class ProfilePicture
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public Tutor Owner { get; set; } = null!;

        public ProfilePicture(string url)
        {
            Url = url;
        }
    }
}
