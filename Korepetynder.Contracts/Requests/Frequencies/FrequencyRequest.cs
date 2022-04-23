using System.ComponentModel.DataAnnotations;

namespace Korepetynder.Contracts.Requests.Frequencies
{
    public class FrequencyRequest
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public int Weight { get; set; }

        public FrequencyRequest(string name, int weight)
        {
            Name = name;
            Weight = weight;
        }
    }
}
