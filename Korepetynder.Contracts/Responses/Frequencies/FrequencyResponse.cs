using Korepetynder.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Contracts.Responses.Frequencies
{
    public class FrequencyResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }

        public FrequencyResponse(Frequency frequency)
        {
            Id = frequency.Id;
            Name = frequency.Name;
            Weight = frequency.Weight;
        }
        public FrequencyResponse(int id, string name, int weight)
        {
            Id = id;
            Name = name;
            Weight = weight;
        }
    }
}
