using Korepetynder.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Contracts.Responses.Languages
{
    public class LanguageResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public LanguageResponse(Language language)
        {
            Id = language.Id;
            Name = language.Name;
        }
        public LanguageResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
