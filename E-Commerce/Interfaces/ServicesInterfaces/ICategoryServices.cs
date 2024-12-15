using E_Commerce.Dtos;
using E_Commerce.Models;

namespace E_Commerce.Interfaces.ServicesInterfaces
{
    public interface ICategoryServices
    {
        public void AddCategory(CategoryDto category);
        public Task<List<Category>> GetCategories();

        public int UpdateCategory(CategoryDto category, int id);

        public void DeleteCategory(int id);

        public Task<Category> GetCategoryByid(int id);
        public Task<List<Category>> Search(string query);
        public void saveChanges();
    }
}
