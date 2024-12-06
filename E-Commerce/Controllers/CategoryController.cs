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

        [HttpPost("AddCategry")]
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
        [HttpPost("UpdateCategry")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCategory(CategoryDto category)
        {
            if (ModelState.IsValid)
            {
                categoryServices.UpdateCategory(category);
                categoryServices.saveChanges();
                return Ok("Update successffully");

            }

            return BadRequest(ModelState);
        }
        [HttpPost("DeleteCategry")]
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
        [HttpGet("GetById")]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Search(string query)
        {
            if (ModelState.IsValid)
            {
                var category = await categoryServices.Search(query );
                return Ok(category);

            }

            return BadRequest(ModelState);
        }

    }
}
