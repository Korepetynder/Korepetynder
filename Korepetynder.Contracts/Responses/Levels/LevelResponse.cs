using Korepetynder.Data.DbModels;

namespace Korepetynder.Contracts.Responses.Levels
{
    public class LevelResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public LevelResponse(Level level)
        {
            Id = level.Id;
            Name = level.Name;
        }
        public LevelResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
