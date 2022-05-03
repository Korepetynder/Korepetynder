namespace Korepetynder.Contracts.Responses.Tutors
{
    public class TutorResponse
    {
        public Guid UserId { get; set; }
        public IEnumerable<int> Locations { get; set; }
        public TutorResponse(Guid id, IEnumerable<int> locations)
        {
            UserId = id;
            Locations = locations;
        }
    }
}
