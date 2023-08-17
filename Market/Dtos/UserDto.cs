using System.ComponentModel.DataAnnotations;

namespace Market.Dtos
{
    public class UserDto
    {
        public Guid? Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }

    }
}
