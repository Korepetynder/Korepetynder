namespace Korepetynder.Contracts.Requests.Media
{
    public class MultimediaFileRequest
    {
        public IEnumerable<int> TutorLessons { get; set; }

        public MultimediaFileRequest(IEnumerable<int> tutorLessons)
        {
            TutorLessons = tutorLessons;
        }
    }
}
