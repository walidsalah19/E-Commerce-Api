using E_Commerce.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Dtos
{
    public class ProductDtoManage
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        [ImageValidation(new[] { ".jpg", ".jpeg", ".png", ".gif" }, 2 * 1024 * 1024)]
        public IFormFile? image { get; set; }
        public int? CouponId { get; set; }
        public int CategoryId { get; set; }
    }
}
