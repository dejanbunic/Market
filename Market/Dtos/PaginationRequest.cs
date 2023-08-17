namespace Market.Dtos
{
    public class PaginationRequest
    {
        public int Skip { get; set; } = 0;
        public int? Take { get; set; } = null;
    }
}
