using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Contracts.Requests.Students
{

    public class StudentCreationRequest
    {
        public int MinimalCost { get; set; }
        public int MaximalCost { get; set; }
        public IEnumerable<int> Locations { get; set; }
        public StudentCreationRequest(int minimal, int maximal, IEnumerable<int> locationIds)
        {
            MinimalCost = minimal;
            MaximalCost = maximal;
            Locations = locationIds;
        }
    }
}
