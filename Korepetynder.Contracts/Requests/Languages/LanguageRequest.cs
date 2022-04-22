using System.ComponentModel.DataAnnotations;

namespace Korepetynder.Contracts.Requests.Languages
{
    public class LanguageRequest
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public LanguageRequest(string name)
        {
            Name = name;
        }
    }
}
