using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Data.DbModels
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Language(string name)
        {
            Name = name;
        }
    }
}
