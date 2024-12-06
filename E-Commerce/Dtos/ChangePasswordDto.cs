using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Dtos
{
    public class ChangePasswordDto
    {
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
