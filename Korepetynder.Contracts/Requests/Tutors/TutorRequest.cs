namespace Korepetynder.Contracts.Requests.Tutors
{
    public class TutorRequest
    {
        public IEnumerable<int> Locations { get; set; }
        public TutorRequest(IEnumerable<int> locations)
        {
            Locations = locations;
        }
    }
}
