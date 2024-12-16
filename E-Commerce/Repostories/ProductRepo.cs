using E_Commerce.Data;
using E_Commerce.Dtos;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repostories
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext context;

        public ProductRepo(AppDbContext context)
        {
            this.context = context;
        }

        public string AddProduct(Product product)
        {
            try
            {
                context.Products.Add(product);
                return "Success";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public string DeleteProduct(int id)
        {
            try
            {
                var product = GetProductById(id);
                if(product ==null)
                {
                    return "Not Found";
                }
                context.Products.Remove(product);
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public List<Product> GetAll()
        {
            return context.Products.ToList();
        }

        public IEnumerable<Product> GetCategoryProducts(string category)
        {
            var product = context.Products.Include(x => x.Category).Where(x => x.Category.Name.Equals(category)).ToList();
            return product;
        }

        public Product GetProductById(int id)
        {
            return context.Products.FirstOrDefault(x=>x.ProductId==id);
        }

        public Product GetProductByName(string name)
        {
            return context.Products.FirstOrDefault(x => x.Name.Contains(name));
        }

        public List<Product> GetVendotProducts(string vendorId)
        {
            var product = context.Products.Where(x => x.VederId.Equals(vendorId)).ToList();
            return product;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public string UpdateProduct(Product productDto)
        {
            try
            {
                var p =context.Products.FirstOrDefault(x=>x.ProductId==productDto.ProductId && x.VederId.Contains(productDto.VederId));
                if (p == null)
                    return "Not Found";

                p.Price = productDto.Price;
                p.CategoryId = productDto.CategoryId;
                p.CouponId = productDto.CouponId;
                p.Description = productDto.Description;
                p.Stock = productDto.Stock;
                p.Name = productDto.Name;
                
                context.Products.Update(p);
                return "Success";

            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}
