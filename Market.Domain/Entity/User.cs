using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Entity
{
    public class User
    {
        [Required]
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(256)]
        public string Password { get; set; }

        public bool isActive { get; set; } = false;
        
    }
}
