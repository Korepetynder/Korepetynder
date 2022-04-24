namespace Korepetynder.Contracts.Requests.Students
{

    public class StudentRequest
    {
        public IEnumerable<int> Locations { get; set; }
        public StudentRequest(IEnumerable<int> locations)
        {
            Locations = locations;
        }
    }
}
