using E_Commerce.Dtos;
using E_Commerce.Interfaces.ServicesInterfaces;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using static Azure.Core.HttpHeader;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponServices couponServices;
        private readonly UserManager<UserApplication> userManager;

        public CouponController(ICouponServices couponServices, UserManager<UserApplication> userManager)
        {
            this.couponServices = couponServices;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpGet("AllCoupons")]
        public IActionResult GetCoupons()
        {
            if(ModelState.IsValid)
            {
                var coupons = couponServices.GetCoupons();
                if(coupons.IsNullOrEmpty())
                {
                    return NoContent();
                }

                return Ok(coupons);
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles ="Vendor")]
        [HttpGet("VendorGetCoupons")]
        public async Task<IActionResult> GetVendorCoupons()
        {
            if (ModelState.IsValid)
            {
                var id = await getVendorId();
                var coupons = couponServices.GetVendorCoupons(id);
                if (coupons.IsNullOrEmpty())
                {
                    return NoContent();
                }

                return Ok(coupons);
            }
            return BadRequest(ModelState);
        }
        [Authorize]
        [HttpGet("UserCoupon/{id:int}")]
        public IActionResult GetCoupon([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
               var coupon= couponServices.GetCoupon(id);
                return Ok(coupon);
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "Vendor")]
        [HttpGet("VendorCoupon/{id:int}")]
        public async Task<IActionResult> GetVendorCoupon([FromRoute]int id)
        {
            if (ModelState.IsValid)
            {
                var vendorId = await getVendorId();
                var coupon= couponServices.GetVendorCoupon(id, vendorId);
                return Ok(coupon);
            }
            return BadRequest(ModelState);
        }
        [Authorize()]
        [HttpGet("CouponProducts/{id:int}")]
        public async Task<IActionResult> CouponProducts([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                var data = await couponServices.GetCouponeProduct(id);
                if(data ==null)
                {
                    return NotFound("No Data Found");
                }
               
                return Ok(data);
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "Vendor")]
        [HttpPost()]
        public async Task<IActionResult> AddCoupon([FromBody]CouponDto dto)
        {
            if (ModelState.IsValid)
            {
                var id = await getVendorId();
                var result=await couponServices.AddCoupon(dto, id);
                if (result.Equals("Success"))
                {
                    couponServices.saveChanges();
                    return Ok("Add Succesffully");
                }
                return BadRequest(result);   
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "Vendor")]
        [HttpDelete()]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            if (ModelState.IsValid)
            {
                var vendorId = await getVendorId();
                var result= couponServices.DeleteCoupon(id, vendorId);
                if (!result.Equals("Success"))
                {
                    return NotFound("You Cant't Delete this Coupon");
                }
                couponServices.saveChanges();
                return Ok("Delete Succesffully");
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "Vendor")]
        [HttpPut()]
        public async Task<IActionResult> UpdateCoupon([FromBody]CouponDto coupon)
        {
            if (ModelState.IsValid)
            {
                var id = await getVendorId();
                var result= couponServices.UpdateCoupon(coupon, id);
                if(!result.Equals("Success"))
                {
                    return NotFound("You Cant Update this Coupon");
                }
                couponServices.saveChanges();
                return Ok("Update Succesffully");
            }
            return BadRequest(ModelState);
        }
        private async Task<string> getVendorId()
        {
            var user =await userManager.GetUserAsync(User);
            return user.Id;
        }

       
    }
}
 