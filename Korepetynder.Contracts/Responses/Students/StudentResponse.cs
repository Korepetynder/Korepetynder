namespace Korepetynder.Contracts.Responses.Students
{
    public class StudentResponse
    {
        public Guid UserId { get; set; }
        public IEnumerable<int> Locations { get; set; }
        public StudentResponse(Guid id, IEnumerable<int> locations)
        {
            UserId = id;
            Locations = locations;
        }
    }
}
