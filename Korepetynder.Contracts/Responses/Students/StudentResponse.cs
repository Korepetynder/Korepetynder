using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Contracts.Responses.Students
{
    public class StudentResponse
    {
        public int StudentId { get; set; }

        public StudentResponse(int id)
        {
            StudentId = id;
        }
    }
}
