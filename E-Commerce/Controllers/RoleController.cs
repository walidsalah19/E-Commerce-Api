using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
       
        [HttpGet]
        public async Task<IActionResult> GetRole([FromQuery]string qurey) 
        {
           if(ModelState.IsValid)
            {
                var role = roleManager.RoleExistsAsync(qurey);

                if(role !=null)
                {
                    return Ok("the role was found ");
                }
                else
                {
                    return NoContent();
                }
            }

            return BadRequest(ModelState);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string role)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole();
                identityRole.Name = role;
                var result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return Ok("Add Role Succesffully");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveRole(string role)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole();
                identityRole.Name = role;
                var result = await roleManager.GetRoleNameAsync(identityRole);
                if (result != null)
                {
                    var removeResult = await roleManager.DeleteAsync(identityRole);
                    if (removeResult.Succeeded)
                    {
                        return Ok("Remove Role Succesffully");
                    }
                    foreach (var item in removeResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "This role isnt exist");
                }
            }



            return BadRequest(ModelState);
        }
    }
}
