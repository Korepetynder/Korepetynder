namespace Korepetynder.Contracts.Requests.Students
{

    public class StudentCreationRequest
    {
        public int MinimalCost { get; set; }
        public int MaximalCost { get; set; }
        public IEnumerable<int> Locations { get; set; }
        public StudentCreationRequest(int minimalCost, int maximalCost, IEnumerable<int> locations)
        {
            MinimalCost = minimalCost;
            MaximalCost = maximalCost;
            Locations = locations;
        }
    }
}
