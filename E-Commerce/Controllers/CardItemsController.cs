using E_Commerce.Dtos;
using E_Commerce.Interfaces.ServicesInterfaces;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class CardItemsController : ControllerBase
    {
        private readonly ICardItemServises cardItemServises;
        private readonly UserManager<UserApplication> userManager;

        public CardItemsController(ICardItemServises cardItemServises, UserManager<UserApplication> userManager)
        {
            this.cardItemServises = cardItemServises;
            this.userManager = userManager;
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetCardItemById([FromQuery]int id)
        {
            if (ModelState.IsValid)
            {
                var userId = await getUserId();
                var item = cardItemServises.GetCardItemById(id, userId);
                if(item ==null)
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
                var item = cardItemServises.GetCardItemByProductName(name, userId);
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
                var item = cardItemServises.GetCardItems(userId);
                if (item == null)
                {
                    return NotFound("No Items Found");
                }
                return Ok(item);
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] CardItemDtoManage dto)
        {
            if (ModelState.IsValid)
            {
                var userId = await getUserId();
                var result = cardItemServises.AddItem(dto,userId);
                if (result.Equals("Success"))
                {
                    cardItemServises.SaveChanges();
                    return Ok("Adding Product to Card Successfully");
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
                var result = cardItemServises.RemoveItem(id, userId);
                if (result.Equals("Success"))
                {
                    cardItemServises.SaveChanges();
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
