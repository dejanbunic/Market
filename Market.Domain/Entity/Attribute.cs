
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Entity
{
    public class Attribute
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Value { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public Guid ProductId { get; set; }
    }
}
