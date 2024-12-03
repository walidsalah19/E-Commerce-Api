using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [MaxLength(11)]
        [MinLength(11)]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Address { get; set; }

    }
}
