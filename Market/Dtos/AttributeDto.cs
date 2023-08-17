namespace Market.Dtos
{
    public class AttributeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public Guid ProductId { get; set; } 
    }
}
