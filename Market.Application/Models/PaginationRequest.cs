namespace Market.Application.Models
{
    public class PaginationRequest
    {
        public int Skip { get; set; }
        public int? Take { get; set; }
    }
}