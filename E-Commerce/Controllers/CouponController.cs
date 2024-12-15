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
        public IActionResult GetCoupon([FromQuery] int couponId)
        {
            if (ModelState.IsValid)
            {
                couponServices.GetCoupon(couponId);
                couponServices.saveChanges();
                return Ok("Add Succesffully");
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "Vendor")]
        [HttpGet("VendorCoupon/{id:int}")]
        public async Task<IActionResult> GetVendorCoupon(int couponId)
        {
            if (ModelState.IsValid)
            {
                var id = await getVendorId();
                couponServices.GetVendorCoupon(couponId, id);
                couponServices.saveChanges();
                return Ok("Add Succesffully");
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "Vendor")]
        [HttpPost()]
        public async Task<IActionResult> AddCoupon(CouponDto dto)
        {
            if (ModelState.IsValid)
            {
                var id = await getVendorId();
                couponServices.AddCoupon(dto, id);
                couponServices.saveChanges();
                return Ok("Add Succesffully");
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "Vendor")]
        [HttpDelete()]
        public async Task<IActionResult> DeleteCoupon(int couponId)
        {
            if (ModelState.IsValid)
            {
                var id = await getVendorId();
                couponServices.DeleteCoupon(couponId,id);
                couponServices.saveChanges();
                return Ok("Add Succesffully");
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "Vendor")]
        [HttpPut()]
        public async Task<IActionResult> UpdateCoupon(CouponDto coupon)
        {
            if (ModelState.IsValid)
            {
                var id = await getVendorId();
                couponServices.UpdateCoupon(coupon, id);
                couponServices.saveChanges();
                return Ok("Add Succesffully");
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
