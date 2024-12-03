using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Dtos
{
    public class LoginDto
    {
        public string UserName { get; set; }       
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsVendor { get; set; }
    }
}
