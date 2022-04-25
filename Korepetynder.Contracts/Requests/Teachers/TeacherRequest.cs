using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Contracts.Requests.Teachers
{
    public class TeacherRequest
    {
        public IEnumerable<int> Locations { get; set; }
        public TeacherRequest(IEnumerable<int> locations)
        {
            Locations = locations;
        }
    }
}
