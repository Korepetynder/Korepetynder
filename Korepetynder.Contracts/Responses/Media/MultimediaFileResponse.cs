namespace Korepetynder.Contracts.Responses.Media
{
    public class MultimediaFileResponse
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? SubjectId { get; set; }

        public MultimediaFileResponse(int id, string url, int? subjectId)
        {
            Id = id;
            Url = url;
            SubjectId = subjectId;
        }
    }
}
