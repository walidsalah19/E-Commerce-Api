using E_Commerce.Dtos;
using E_Commerce.Models;

namespace E_Commerce.Interfaces.ServicesInterfaces
{
    public interface ICouponServices
    {
        public List<Coupon> GetCoupons();
        public List<Coupon> GetVendorCoupons(string vendorId);
        public void AddCoupon(CouponDto coupon, string vendorId);
        public int DeleteCoupon(int couponId, string vendorId);
        public int UpdateCoupon(CouponDto coupon, string vendorId);
        public Coupon GetCoupon(int couponId);
        public Coupon GetVendorCoupon(int couponId, string vendorId);

        public void saveChanges();
    }
}
