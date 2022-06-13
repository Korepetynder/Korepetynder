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
    }
}
