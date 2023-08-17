namespace Market.Dtos
{
    public class UserQueryRequest : PaginationRequest
    {
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

    }
}