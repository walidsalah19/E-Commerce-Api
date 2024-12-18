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
    
    public class ReviewController : ControllerBase
    {
        private readonly UserManager<UserApplication> userManager;
        private readonly IReviewServices reviewServices;

        public ReviewController(UserManager<UserApplication> userManager, IReviewServices reviewServices)
        {
            this.userManager = userManager;
            this.reviewServices = reviewServices;
        }
        [AllowAnonymous]
        [HttpGet("ProductReviews")]
        public async Task<IActionResult> GetProductReview([FromQuery] int productId)
        {
            if (ModelState.IsValid)
            {
                var item = reviewServices.GetProductReview(productId);
                if (item == null)
                {
                    return NotFound("No Items Found");
                }
                return Ok(item);
            }

            return BadRequest(ModelState);
        }
        [Authorize(Roles = "User")]
        [HttpGet("UserReviews")]
        public async Task<IActionResult> GetUserReviews()
        {
            if (ModelState.IsValid)
            {
                var userId = await getUserId();
                var item = reviewServices.GetUserReviewa(userId);
                if (item == null)
                {
                    return NotFound("No Items Found");
                }
                return Ok(item);
            }

            return BadRequest(ModelState);
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] ReviewDtoManage dto)
        {
            if (ModelState.IsValid)
            {
                var userId = await getUserId();
                var result = reviewServices.AddReview(dto, userId);
                if (result.Equals("Success"))
                {
                    reviewServices.SaveChanges();
                    return Ok("Adding Review to Product Successfully");
                }
                return BadRequest(result);
            }

            return BadRequest(ModelState);
        }
        [Authorize(Roles = "User")]
        [HttpDelete]
        public async Task<IActionResult> DeleteItem([FromQuery] int id)
        {
            if (ModelState.IsValid)
            {
                var result = reviewServices.RemoveReview(id);
                if (result.Equals("Success"))
                {

                    reviewServices.SaveChanges();
                    return Ok("Remove review From Product Successfully");
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
