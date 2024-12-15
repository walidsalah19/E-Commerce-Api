using E_Commerce.Models;

namespace E_Commerce.Interfaces.RepoInterfaces
{
    public interface ICategoryRepo
    {
        public void AddCategory(Category category);
        public Task<List<Category>> GetCategories();

        public int UpdateCategory(Category category, int id);

        public void DeleteCategory(int id);

        public Task<Category> GetCategoryByid(int id);
        public Task<List<Category>> Search(string query);

        public void saveChanges();


    }
}
