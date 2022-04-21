namespace Korepetynder.Services.Models
{
    public record PagedData<T>
    {
        public int TotalCount { get; init; }
        public IEnumerable<T> Entities { get; init; }

        public PagedData(int totalCount, IEnumerable<T> entities)
        {
            TotalCount = totalCount;
            Entities = entities;
        }
    }
}
