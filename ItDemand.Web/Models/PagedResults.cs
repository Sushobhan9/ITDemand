namespace ItDemand.Web.Models
{
    public class PagedResults<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Rows { get; set; } = Enumerable.Empty<T>();
    }
}
