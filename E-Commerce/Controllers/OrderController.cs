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
    public class OrderController : ControllerBase
    {
        private readonly UserManager<UserApplication> userManager;
        private readonly IOrderServices orderServices;

        public OrderController(UserManager<UserApplication> userManager, IOrderServices orderServices)
        {
            this.userManager = userManager;
            this.orderServices = orderServices;
        }
        [HttpGet("VendorOrders")]
        [Authorize(Roles ="Vendor")]
        public async Task<IActionResult> GetOrdersForVendor()
        {
            var vendorId = await getUserId();

            var orders = orderServices.GetOrdersForVendor(vendorId);
            return Ok(orders);
        }
        [HttpGet("UserOrders")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetOrdersForUser()
        {
            var id = await getUserId();

            var orders = orderServices.GetOrdersForUser(id);
            return Ok(orders);
        }
        [HttpPost("CreateOrder")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> createOrder()
        {
            var id = await getUserId();
            var result = orderServices.createOrder(id);
            if(result.Result.Equals("Success"))
            {
                return Ok("Create Order Successfully");
            }
          return BadRequest(result);
        }
        [HttpDelete("CancelOrder")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> cancelOrder([FromQuery] int OrderId)
        {
            var result = orderServices.cancelOrder(OrderId);
            if (result.Equals("Success"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("UpdateStatus")]
        [Authorize(Roles = "Vendor")]
        public async Task<IActionResult> cancelOrder([FromQuery]string status, [FromQuery] int OrderId)
        {
            var result = orderServices.UpdateOrderStatus(status,OrderId);
            if (result.Equals("Success"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        private async Task<string> getUserId()
        {
            var user = await userManager.GetUserAsync(User);
            return user.Id;
        }
    }
}
