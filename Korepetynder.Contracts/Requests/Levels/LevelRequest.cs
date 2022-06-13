using System.ComponentModel.DataAnnotations;

namespace Korepetynder.Contracts.Requests.Levels
{
    public class LevelRequest
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public int? Weight { get; set; }

        public LevelRequest(string name)
        {
            Name = name;
        }
    }
}
