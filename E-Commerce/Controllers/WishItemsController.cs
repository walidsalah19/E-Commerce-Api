using E_Commerce.Dtos;
using E_Commerce.Interfaces.ServicesInterfaces;
using E_Commerce.Models;
using E_Commerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]

    public class WishItemsController : ControllerBase
    {
        private readonly UserManager<UserApplication> userManager;
        private readonly IWishItemsServices itemsServices;

        public WishItemsController(UserManager<UserApplication> userManager, IWishItemsServices itemsServices)
        {
            this.userManager = userManager;
            this.itemsServices = itemsServices;
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetCardItemById([FromQuery] int id)
        {
            if (ModelState.IsValid)
            {
                var userId = await getUserId();
                var item = itemsServices.GetItemById(id, userId);
                if (item == null)
                {
                    return NotFound("No Item Found");
                }
                return Ok(item);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("ProductName")]
        public async Task<IActionResult> GetCardItemByProductName([FromQuery] string name)
        {
            if (ModelState.IsValid)
            {
                var userId = await getUserId();
                var item = itemsServices.GetItemByProductName(name, userId);
                if (item == null)
                {
                    return NotFound("No Item Found");
                }
                return Ok(item);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetCardItems()
        {
            if (ModelState.IsValid)
            {
                var userId = await getUserId();
                var item = itemsServices.GetItems(userId);
                if (item == null)
                {
                    return NotFound("No Items Found");
                }
                return Ok(item);
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromQuery] int productId)
        {
            if (ModelState.IsValid)
            {
                var userId = await getUserId();
                var result = itemsServices.AddItem(productId, userId);
                if (result.Equals("Success"))
                {
                    itemsServices.SaveChanges();
                    return Ok("Adding Product to WishList Successfully");
                }
                return BadRequest(result);
            }

            return BadRequest(ModelState);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteItem([FromQuery] int id)
        {
            if (ModelState.IsValid)
            {
                var userId = await getUserId();
                var result = itemsServices.RemoveItem(id, userId);
                if (result.Equals("Success"))
                {

                    itemsServices.SaveChanges();
                    return Ok("Remove Product From Card Successfully");
                }
                return BadRequest(result);
            }

            return BadRequest(ModelState);
        }
        private async Task<string> getUserId()
        {
            var user = await userManager.GetUserAsync(User);
            return user.Id;
        }
    }
}
