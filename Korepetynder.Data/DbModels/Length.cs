using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Data.DbModels
{
    public class Length
    {
        public int Id { get; set; }
        public string Name { get; set; } //such as "one time", "regular" and so on
        public Length(string name)
        {
            Name = name;
        }
    }
}
