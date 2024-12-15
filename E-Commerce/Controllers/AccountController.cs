using E_Commerce.Dtos;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserApplication> userManager;
        private readonly IConfiguration config;
        private readonly ICardRepo cardRepo;
        private readonly IWishListRepo wishListRepo;

        public AccountController(UserManager<UserApplication> userManager, IConfiguration config, ICardRepo cardRepo, IWishListRepo wishListRepo)
        {
            this.userManager = userManager;
            this.config = config;
            this.cardRepo = cardRepo;
            this.wishListRepo = wishListRepo;
        }

        [HttpPost("RegistrationUser")]
        public async Task<IActionResult> RegistrationUser(UserDto userDto)
        {
            if(ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    User user = new User
                    {
                        UserName=userDto.UserName,
                        Email=userDto.Email,
                        Address=userDto.Address,
                        CreatedAt=DateTime.Now,
                        IsVendor=false,
                        PhoneNumber=userDto.Phone

                    };

                    
                    var createValue = await userManager.CreateAsync(user, userDto.Password);
                    if (createValue.Succeeded)
                    {
                        var addRoleResult = await userManager.AddToRoleAsync(user, "User");
                        if (addRoleResult.Succeeded)
                        {
                            cardRepo.AddCart(new Cart { UserId=user.Id});
                            cardRepo.saveChanges();
                            wishListRepo.AddWishList(new Wishlist { UserId = user.Id });
                            wishListRepo.saveChanges();
                            return Created("", "the Accout was created");
                        }
                    }
                    foreach (var item in createValue.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost("RegistrationVendor")]
        public async Task<IActionResult> RegistrationVedor(VendorDto vendorDto)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    Vendor vendor = new Vendor
                    {
                        UserName = vendorDto.UserName,
                        Email = vendorDto.Email,
                        Address = vendorDto.Address,
                        CreatedAt = DateTime.Now,
                        IsVendor = true,
                        PhoneNumber = vendorDto.Phone,
                        CompanyName=vendorDto.CompanyName,
                        Description=vendorDto.Description
                    };


                    var createValue = await userManager.CreateAsync(vendor, vendorDto.Password);
                    if (createValue.Succeeded)
                    {
                        var addRoleResult = await userManager.AddToRoleAsync(vendor, "Vendor");
                        if (addRoleResult.Succeeded)
                            return Created("", "the Accout was created");
                    }
                    foreach (var item in createValue.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (ModelState.IsValid)
            {
                UserApplication user = await userManager.FindByNameAsync(dto.UserName);
                var passResult = await userManager.CheckPasswordAsync(user, dto.Password);
                if (passResult)
                {
                    List<Claim> userClaims = new List<Claim>();
                    userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    userClaims.Add(new Claim(ClaimTypes.Email, user.Email));
                    userClaims.Add(new Claim(ClaimTypes.Name, user.UserName));
                    var role = await userManager.GetRolesAsync(user);

                    var symmetric = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecritKey"]));

                    SigningCredentials signing = new SigningCredentials(symmetric, SecurityAlgorithms.HmacSha256);
                    foreach (var item in role)
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, item));
                    }

                    JwtSecurityToken jwtSecurity = new JwtSecurityToken(
                            audience: config["JWT:AudienceIP"],
                            issuer: config["JWT:IssuerIP"],
                            expires: DateTime.Now.AddHours(1),
                            claims: userClaims,
                            signingCredentials: signing
                        );

                    return Ok(new
                    {
                        message = "Login succesffully",
                        token = new JwtSecurityTokenHandler().WriteToken(jwtSecurity),
                        expiration = DateTime.Now.AddHours(1)

                    });
                }
                ModelState.AddModelError("", "Please check the enterd data");
            }
            return BadRequest(ModelState);
        }
        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassworf(ChangePasswordDto dto)
        {

            if(ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);

                if (user == null)
                {
                    return Unauthorized("User not found or not logged in.");
                }
                
                var status = await userManager.ChangePasswordAsync(user,dto.OldPassword,dto.NewPassword);
                if(status.Succeeded)
                {
                    await userManager.UpdateSecurityStampAsync(user);
                    return Ok("Password changed successfully.");
                }
                foreach(var item in status.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }

            }
            return BadRequest(ModelState);
        }
        [HttpGet("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok("Logged out successfully.");
        }
        [HttpDelete("RemoveAccount")]
        [Authorize]
        public async Task<IActionResult> RemoveAccount()
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                var deleteResult = await userManager.DeleteAsync(user);

                if (deleteResult.Succeeded)
                {

                    return Ok("Account removed successfully.");
                }
                foreach (var item in deleteResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return BadRequest(ModelState);
        }
    }
}
