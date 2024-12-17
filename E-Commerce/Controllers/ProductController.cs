using E_Commerce.Dtos;
using E_Commerce.Helpers;
using E_Commerce.Interfaces.ServicesInterfaces;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices productServices;
        private readonly UserManager<UserApplication> userManager;
        private readonly ILogger<ProductController> logger;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(IProductServices productServices, UserManager<UserApplication> userManager, ILogger<ProductController> logger, IWebHostEnvironment webHostEnvironment)
        {
            this.productServices = productServices;
            this.userManager = userManager;
            this.logger = logger;
            this.webHostEnvironment = webHostEnvironment;
        }

        [AllowAnonymous]
        [HttpGet("ProductName")]
        public async Task<IActionResult> GetProductByName(string name)
        {
           
            var result = productServices.GetProductByName(name);
            if(result ==null)
            {
                return NotFound($"Product {name} Not Found");
            }
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("ProductId")]
        public async Task<IActionResult> GetProductById(int Id)
        {

            var result = productServices.GetProductById(Id);
            //logger.LogInformation(result.Description);
            if (result == null)
            {
                return NotFound($"Product Not Found");
            }
            Log.Information("the result => {@result}", result);

            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("CategoryName")]
        public async Task<IActionResult> GetCategoryProducts(string name)
        {
            var result = productServices.GetCategoryProducts(name);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("AllProducts")]
        public async Task<IActionResult> AllProducts()
        {
            var result = productServices.GetAll();
            return Ok(result);
        }
        [Authorize(Roles = "Vendor")]
        [HttpGet("VendorProducts")]
        public async Task<IActionResult> GetVendotProducts()
        {
                var vendorId = await getVendorId();
                var result = productServices.GetVendotProducts(vendorId);  
                return Ok(result);
        }


        [Authorize(Roles ="Vendor")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm]ManageProductDto dto)
        {
            if(ModelState.IsValid)
            {
                var imageUrl = await UploadImage.ProcessUploadedFile(dto.image, webHostEnvironment);
                var vendorId = await getVendorId();
               var result= productServices.AddProduct(dto,vendorId,imageUrl);
                if (result.Equals("Success"))
                {
                    productServices.SaveChanges();
                    return Ok("Adding product Succesffully");
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "Vendor")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromForm]ManageProductDto dto)
        {
            if (ModelState.IsValid)
            {
                var imageUrl = await UploadImage.ProcessUploadedFile(dto.image, webHostEnvironment);
                var vendorId = await getVendorId();
                var result = productServices.UpdateProduct(dto, vendorId,imageUrl);
                if (result.Equals("Success"))
                {
                    productServices.SaveChanges();
                    return Ok("Update product Succesffully");
                }
                else if (result.Equals("Not Found"))
                {
                    return NotFound("the product you need to update not found");
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "Vendor")]
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (ModelState.IsValid)
            {
                
                var result = productServices.DeleteProduct(id);
                if (result.Equals("Success"))
                {
                    productServices.SaveChanges();
                    return Ok("Delete product Succesffully");
                }
                else if (result.Equals("Not Found"))
                {
                    return NotFound("the product you need to Delete not found");
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }


        private async Task<string> getVendorId()
        {
            var user = await userManager.GetUserAsync(User);
            return user.Id;
        }

    }
}
