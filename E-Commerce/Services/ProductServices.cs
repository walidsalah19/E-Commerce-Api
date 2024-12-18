using E_Commerce.Dtos;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Interfaces.ServicesInterfaces;
using E_Commerce.Models;

namespace E_Commerce.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepo productRepo;

        public ProductServices(IProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }

        public string AddProduct(ProductDtoManage productDto, string vindorId,string ImageUrl)
        {
            var product = new Product
            {
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
                CouponId = productDto.CouponId,
                Description = productDto.Description,
                Stock = productDto.Stock,
                Name = productDto.Name,
                VederId = vindorId,
                ImageUrl= $"images/{ImageUrl}"
            };
            return productRepo.AddProduct(product);

        }

        public string DeleteProduct(int id)
        {
            return productRepo.DeleteProduct(id);
        }

        public List<ProductDto> GetAll()
        {
            return productRepo.GetAll().
                Select(x=> new ProductDto
                {
                    Price=x.Price,
                    Description=x.Description,
                    Stock=x.Stock,
                    Name=x.Name,
                    ProductId=x.ProductId,
                    ImageUrl = $"images/{x.ImageUrl}"

                }).ToList();
        }

        public IEnumerable<ProductDto> GetCategoryProducts(string category)
        {
            return productRepo.GetCategoryProducts(category).Select(x => new ProductDto { 
                Name = x.Name, Price = x.Price, Description = x.Description, Stock = x.Stock
                , ProductId = x.ProductId,ImageUrl = $"images/{x.ImageUrl}"
            });
        }

        public ProductDto GetProductById(int id)
        {
            var product = productRepo.GetProductById(id);
            return new ProductDto
            {
                Price = product.Price,
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Stock = product.Stock,
                ImageUrl = $"images/{product.ImageUrl}"

            };
        }

        public ProductDto GetProductByName(string name)
        {
            var product= productRepo.GetProductByName(name);
            return new ProductDto
            {
                Price = product.Price,
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Stock = product.Stock,
                ImageUrl= $"images/{product.ImageUrl}"
            };
        }

        public List<ProductDto> GetVendotProducts(string vendorId)
        {
            return productRepo.GetVendotProducts(vendorId).Select(x => new ProductDto
            {
                Price = x.Price,
                Description = x.Description,
                Stock = x.Stock,
                Name = x.Name,
                ProductId = x.ProductId,
                ImageUrl= $"images/{x.ImageUrl}"
            }).ToList();
        }

        public void SaveChanges()
        {
            productRepo.SaveChanges();
        }

        public string UpdateProduct(ProductDtoManage productDto, string vindorId, string ImageUrl)
        {
            var product = new Product
            {
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
                CouponId = productDto.CouponId,
                Description = productDto.Description,
                Stock = productDto.Stock,
                Name = productDto.Name,
                VederId = vindorId,
                ProductId=productDto.ProductId,
                ImageUrl=ImageUrl

            };
            return productRepo.UpdateProduct(product);
        }
    }
}
