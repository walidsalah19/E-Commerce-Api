using AutoMapper;
using E_Commerce.Dtos;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Interfaces.ServicesInterfaces;
using E_Commerce.Models;

namespace E_Commerce.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepo productRepo;
        private readonly IMapper _mapper;

        public ProductServices(IProductRepo productRepo, IMapper mapper)
        {
            this.productRepo = productRepo;
            _mapper = mapper;
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
            var products = productRepo.GetAll();
            var productDto = _mapper.Map<List<ProductDto>>(products);
            return productDto;
        }

        public IEnumerable<ProductDto> GetCategoryProducts(string category)
        {
            var products = productRepo.GetCategoryProducts(category);
            var productDto = _mapper.Map<List<ProductDto>>(products);
            return productDto;
        }

        public ProductDto GetProductById(int id)
        {
            var product = productRepo.GetProductById(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public ProductDto GetProductByName(string name)
        {
            var product= productRepo.GetProductByName(name);
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

      

        public List<ProductDto> GetVendotProducts(string vendorId)
        {
            var products = productRepo.GetVendotProducts(vendorId);
            var productDto = _mapper.Map<List<ProductDto>>(products);
            return productDto;
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
