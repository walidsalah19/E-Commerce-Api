using E_Commerce.Dtos;
using E_Commerce.Models;

namespace E_Commerce.Interfaces.ServicesInterfaces
{
    public interface ICouponServices
    {
        public List<Coupon> GetCoupons();
        public List<Coupon> GetVendorCoupons(string vendorId);
        public Task<string> AddCoupon(CouponDto coupon, string vendorId);
        public string DeleteCoupon(int couponId, string vendorId);
        public string UpdateCoupon(CouponDto coupon, string vendorId);
        public Coupon GetCoupon(int couponId);
        public Coupon GetVendorCoupon(int couponId, string vendorId);
        public Task<CouponProductDto> GetCouponeProduct(int id);

        public void saveChanges();
    }
}
