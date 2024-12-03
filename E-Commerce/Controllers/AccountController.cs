using E_Commerce.Dtos;
using E_Commerce.Models;
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

        public AccountController(UserManager<UserApplication> userManager, IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
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
                        if(addRoleResult.Succeeded)
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
        [HttpPost("login")]
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
    }
}
