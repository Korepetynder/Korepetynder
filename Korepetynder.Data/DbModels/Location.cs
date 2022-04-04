namespace Korepetynder.Data.DbModels
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Location> SubLocation { get; set; } = new List<Location>();

        public Location(string name)
        {
            Name = name;
        }
    }
}
