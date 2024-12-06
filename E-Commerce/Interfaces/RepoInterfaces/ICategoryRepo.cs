using E_Commerce.Models;

namespace E_Commerce.Interfaces.RepoInterfaces
{
    public interface ICategoryRepo
    {
        public void AddCategory(Category category);
        public Task<List<Category>> GetCategories();

        public void UpdateCategory(Category category);

        public void DeleteCategory(int id);

        public Task<Category> GetCategoryByid(int id);
        public Task<List<Category>> Search(string query);

        public void saveChanges();


    }
}
