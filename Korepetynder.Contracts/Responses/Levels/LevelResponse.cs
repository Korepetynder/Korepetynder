using Korepetynder.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Contracts.Responses.Levels
{
    public class LevelResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Weight { get; set; }
        public LevelResponse(Level level)
        {
            Id = level.Id;
            Name = level.Name;
            Weight = level.Weight;
        }
        public LevelResponse(int id, string name, int weight)
        {
            Id = id;
            Name = name;
            Weight = weight;
        }
    }
}
