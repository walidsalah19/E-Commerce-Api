using E_Commerce.Dtos;
using E_Commerce.Models;

namespace E_Commerce.Interfaces.RepoInterfaces
{
    public interface IProductRepo
    {
        public string AddProduct(Product product);

        public string DeleteProduct(int id);

        public string UpdateProduct(Product product);

        public Product GetProductById(int id);
        public Product GetProductByName(string name);


        public List<Product> GetAll();

        public List<Product> GetVendotProducts(string vendorId);


        public IEnumerable<Product> GetCategoryProducts(string category);

        public void SaveChanges();



    }
}
