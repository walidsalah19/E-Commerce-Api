using E_Commerce.Dtos;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Interfaces.ServicesInterfaces;
using E_Commerce.Models;
using static Azure.Core.HttpHeader;

namespace E_Commerce.Services
{
    public class CouponServices : ICouponServices
    {
        private ICouponRepo couponRepo;

        public CouponServices(ICouponRepo couponRepo)
        {
            this.couponRepo = couponRepo;
        }

        public async Task<string> AddCoupon(CouponDto coupon,string vendorId)
        {
            Coupon c = new Coupon
            {
                Code = coupon.Code,
                DiscountPercentage = coupon.DiscountPercentage,
                ValidFrom = coupon.ValidFrom,
                ValidTo = coupon.ValidTo,
                VendorId = vendorId
            };
           return await couponRepo.AddCoupon(c);
        }

        public string DeleteCoupon(int couponId, string vendorId)
        {
           return couponRepo.DeleteCoupon(couponId, vendorId);
        }

        public Coupon GetCoupon(int couponId)
        {
            return couponRepo.GetCoupon(couponId);
        }

        public async Task<CouponProductDto> GetCouponeProduct(int id)
        {
            var coupon =await couponRepo.GetCouponeProduct(id);
            if (coupon == null)
                return null;
            var products = new List<ProductDto>();
            foreach(var item in coupon.products)
            {
                products.Add(new ProductDto { Description=item.Description,
                Name=item.Name,Price=item.Price,ProductId=item.ProductId,Stock=item.Stock});
            }

            var dto = new CouponProductDto
            {
                Code=coupon.Code,
                DiscountPercentage=coupon.DiscountPercentage,
                ValidFrom=coupon.ValidFrom,
                ValidTo=coupon.ValidTo,
                products=products
            };
            


            return dto;
        }

        public List<Coupon> GetCoupons()
        {
            var coupon= couponRepo.GetCoupons();
            if (coupon == null)
            {
                return null;
            }
            return coupon;
        }

        public Coupon GetVendorCoupon(int couponId, string vendorId)
        {
            var coupon= couponRepo.GetVendorCoupon(couponId,vendorId);
            if (coupon == null)
            {
                return null;
            }
            return coupon;
        }

        public List<Coupon> GetVendorCoupons(string vendorId)
        {
            return couponRepo.GetVendorCoupons(vendorId);
        }

        public void saveChanges()
        {
            couponRepo.saveChanges();
        }

        public string UpdateCoupon(CouponDto coupon, string vendorId)
        {
            Coupon c = new Coupon
            {
                Code = coupon.Code,
                DiscountPercentage = coupon.DiscountPercentage,
                ValidFrom = coupon.ValidFrom,
                ValidTo = coupon.ValidTo,
                VendorId = vendorId
            };
            return couponRepo.UpdateCoupon(c);
        }
    }
}
