using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Contracts.Responses.Teachers
{
    public class TeacherResponse
    {
        public int TeacherId { get; set; }
        public IEnumerable<int> Locations { get; set; }
        public TeacherResponse(int id, IEnumerable<int> locations)
        {
            TeacherId = id;
            Locations = locations;
        }
    }
}
