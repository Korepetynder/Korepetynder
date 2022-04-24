namespace Korepetynder.Contracts.Responses.Students
{
    public class StudentResponse
    {
        public int StudentId { get; set; }
        public IEnumerable<int> Locations { get; set; }
        public StudentResponse(int id, IEnumerable<int> locations)
        {
            StudentId = id;
            Locations = locations;
        }
    }
}
