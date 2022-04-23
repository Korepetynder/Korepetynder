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

        public FrequencyResponse(Frequency frequency)
        {
            Id = frequency.Id;
            Name = frequency.Name;
        }
        public FrequencyResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
