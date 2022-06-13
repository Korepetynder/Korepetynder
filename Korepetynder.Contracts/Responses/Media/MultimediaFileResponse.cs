namespace Korepetynder.Contracts.Responses.Media
{
    public class MultimediaFileResponse
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public IEnumerable<int> Lessons { get; set; }

        public MultimediaFileResponse(int id, string url, IEnumerable<int> lessons)
        {
            Id = id;
            Url = url;
            Lessons = lessons;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj is MultimediaFileResponse other && other.Id == Id)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
