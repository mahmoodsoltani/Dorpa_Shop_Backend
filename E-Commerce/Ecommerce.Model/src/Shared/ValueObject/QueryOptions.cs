namespace Ecommerce.Model.src.Shared.ValueObject
{
    public class QueryOptions
    {
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; }
        public string SortBy { get; set; } = "Id";
        public bool IsAscending { get; set; } = true;
        public string? SearchTerm { get; set; }
        public string? SearchBy { get; set; } = "ID";
    }
}
