using System.ComponentModel.DataAnnotations;

namespace Korepetynder.Contracts.Requests.Subjects
{
    public class SubjectRequest
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public SubjectRequest(string name)
        {
            Name = name;
        }
    }
}
