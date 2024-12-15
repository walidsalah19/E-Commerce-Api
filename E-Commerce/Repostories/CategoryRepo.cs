using E_Commerce.Data;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repostories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly AppDbContext context;

        public CategoryRepo(AppDbContext context)
        {
            this.context = context;
        }

        public void AddCategory(Category category)
        {
            context.Categories.Add(category);
        }

        public void DeleteCategory(int id)
        {
            var category = context.Categories.FirstOrDefault(x => x.CategoryId == id);
            context.Categories.Remove(category);
        }

        public async Task<List<Category>> GetCategories()
        {
            return context.Set<Category>().AsNoTracking().ToList();
        }

        public async Task<Category> GetCategoryByid(int id)
        {
            return context.Set<Category>().AsNoTracking().FirstOrDefault(x => x.CategoryId == id);
        }

        public void saveChanges()
        {
            context.SaveChanges();
        }

        public async Task<List<Category>> Search(string query)
        {
            var categories = context.Set<Category>().Where(x=>x.Name.Contains(query) 
            || x.Description.Contains(query))
                .AsNoTracking().ToList();


            return categories;
        }

        public int UpdateCategory(Category category, int id)
        {
            var categoryData = context.Categories.SingleOrDefault(x => x.CategoryId==id);
            if(categoryData !=null)
            {
                categoryData.Description = category.Description;
                categoryData.Name = category.Name;
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
