using E_Commerce.Models;

namespace E_Commerce.Interfaces.RepoInterfaces
{
    public interface IProductRepo
    {
        public void AddProduct(Product product);

        public void DeleteProduct(int id);

        public void UpdateProduct(Product product);

        public Product GetProductById(int id);

        public List<Product> GetAll();

        public List<Product> GetVendotProducts(string vendorId);

        public List<Product> GetCategoryProducts(string category);



    }
}
