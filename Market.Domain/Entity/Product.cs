using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Entity
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public ulong Barcode { get; set; }
       
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Group { get; set; }
        [StringLength(50)]
        public string MeasureUnit { get; set; }

        public ICollection<Attribute> Attributes { get; } = new List<Attribute>();


    }
}
