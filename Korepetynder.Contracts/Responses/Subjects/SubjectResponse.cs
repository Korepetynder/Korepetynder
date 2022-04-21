namespace Korepetynder.Contracts.Responses.Subjects
{
    public class SubjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SubjectResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
