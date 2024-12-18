using E_Commerce.Dtos;
using E_Commerce.Models;

namespace E_Commerce.Interfaces.ServicesInterfaces
{
    public interface IProductServices
    {
        public string AddProduct(ProductDtoManage product,string vindorId, string ImageUrl);

        public string DeleteProduct(int id);

        public string UpdateProduct(ProductDtoManage product, string vindorId,string ImageUrl);

        public ProductDto GetProductById(int id);
        public ProductDto GetProductByName(string name);

        public List<ProductDto> GetAll();

        public List<ProductDto> GetVendotProducts(string vendorId);

        public IEnumerable<ProductDto> GetCategoryProducts(string category);

        public void SaveChanges();
    }
}
