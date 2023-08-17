namespace Market.Dtos
{
    public class ProductQueryRequest: PaginationRequest
    {
        public ulong? Barcode { get; set; }
        public string? Name { get; set; }
        public string? Group { get; set; }
        public string? MeasureUnit { get; set; }
        public ICollection<Guid>? Attributes { get; set; }
    }
}
