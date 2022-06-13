namespace Korepetynder.Contracts.Responses.Tutors
{
    public class TutorResponse
    {
        public Guid UserId { get; set; }
        public IEnumerable<int> Locations { get; set; }
        public decimal Score { get; set; }
        public TutorResponse(Guid id, IEnumerable<int> locations, decimal score)
        {
            UserId = id;
            Locations = locations;
            Score = score;
        }
    }
}
