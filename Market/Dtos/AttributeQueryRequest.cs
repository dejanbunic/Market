namespace Market.Dtos
{
    public class AttributeQueryRequest: PaginationRequest
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
    }
}
