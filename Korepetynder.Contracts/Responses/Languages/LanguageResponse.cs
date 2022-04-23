using Korepetynder.Data.DbModels;

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
