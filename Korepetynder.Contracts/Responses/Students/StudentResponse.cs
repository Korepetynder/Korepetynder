namespace Korepetynder.Contracts.Responses.Students
{
    public class StudentResponse
    {
        public int StudentId { get; set; }
        public int MinimalCost { get; set; }
        public int MaximalCost { get; set; }
        public IEnumerable<int> Locations { get; set; }
        public StudentResponse(int id, int minimalCost, int maximalCost, IEnumerable<int> locations)
        {
            StudentId = id;
            MinimalCost = minimalCost;
            MaximalCost = maximalCost;
            Locations = locations;
        }
    }
}
