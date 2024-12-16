using E_Commerce.Models;

namespace E_Commerce.Interfaces.RepoInterfaces
{
    public interface ICouponRepo
    {
        public List<Coupon> GetCoupons();
        public List<Coupon> GetVendorCoupons(string vendorId);
        public Task<string> AddCoupon(Coupon coupon);
        public string DeleteCoupon(int couponId,string vendorId);
        public string UpdateCoupon(Coupon coupon);
        public Coupon GetCoupon(int couponId);
        public Coupon GetVendorCoupon(int couponId,string vendorId);
        public Task<Coupon> GetCouponeProduct(int id);
        public void saveChanges();
    }
}
