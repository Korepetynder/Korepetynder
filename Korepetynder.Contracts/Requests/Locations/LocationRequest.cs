using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Contracts.Requests.Locations
{
    public class LocationRequest
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public int? ParentLocationId { get; set; }

        public LocationRequest(string name)
        {
            Name = name;
        }
    }
}

