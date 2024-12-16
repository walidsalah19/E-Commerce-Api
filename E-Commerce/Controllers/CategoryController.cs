using E_Commerce.Dtos;
using E_Commerce.Interfaces.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }
        [HttpGet("GetById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            if (ModelState.IsValid)
            {
                var category = await categoryServices.GetCategoryByid(id);
                return Ok(category);

            }

            return BadRequest(ModelState);
        }
        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategories()
        {
            if (ModelState.IsValid)
            {
                var category =await categoryServices.GetCategories();
                return Ok(category);

            }

            return BadRequest(ModelState);
        }
        [HttpGet("Search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search(string query)
        {
            if (ModelState.IsValid)
            {
                var category = await categoryServices.Search(query );
                return Ok(category);

            }

            return BadRequest(ModelState);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCategory(CategoryDto category)
        {
            if (ModelState.IsValid)
            {
                categoryServices.AddCategory(category);
                categoryServices.saveChanges();
                return Ok("Add successffully");

            }

            return BadRequest(ModelState);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCategory([FromBody] CategoryDto category, [FromQuery] int id)
        {
            if (ModelState.IsValid)
            {
                var result = categoryServices.UpdateCategory(category, id);
                if (result == 1)
                {
                    categoryServices.saveChanges();
                    return Ok("Update successffully");
                }
                else
                {
                    return NoContent();
                }

            }

            return BadRequest(ModelState);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCategory(int id)
        {
            if (ModelState.IsValid)
            {
                categoryServices.DeleteCategory(id);
                categoryServices.saveChanges();
                return Ok("Delete successffully");
            }

            return BadRequest(ModelState);
        }
    }
}
