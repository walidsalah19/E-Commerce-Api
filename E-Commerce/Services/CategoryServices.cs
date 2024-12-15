using E_Commerce.Dtos;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Interfaces.ServicesInterfaces;
using E_Commerce.Models;

namespace E_Commerce.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepo categoryRepo;

        public CategoryServices(ICategoryRepo categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        public void AddCategory(CategoryDto category)
        {
            var model = new Category
            {
                Description = category.Description,
                Name = category.Name

            };
            categoryRepo.AddCategory(model);
        }

        public void DeleteCategory(int id)
        {
            categoryRepo.DeleteCategory(id);
        }

        public async Task<List<Category>> GetCategories()
        {
            return await categoryRepo.GetCategories();
        }

        public async Task<Category> GetCategoryByid(int id)
        {
            return await categoryRepo.GetCategoryByid(id);
        }

        public void saveChanges()
        {
            categoryRepo.saveChanges();
        }

        public async Task<List<Category>> Search(string query)
        {
          return await categoryRepo.Search(query);
        }

        public int UpdateCategory(CategoryDto category, int id)
        {
           return  categoryRepo.UpdateCategory(new Category
            {
                Description = category.Description,
                Name = category.Name
            }, id);
        }
    }
}
