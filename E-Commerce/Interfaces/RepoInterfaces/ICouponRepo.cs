using E_Commerce.Models;

namespace E_Commerce.Interfaces.RepoInterfaces
{
    public interface ICouponRepo
    {
        public List<Coupon> GetCoupons();
        public List<Coupon> GetVendorCoupons(string vendorId);
        public void AddCoupon(Coupon coupon);
        public int DeleteCoupon(int couponId,string vendorId);
        public int UpdateCoupon(Coupon coupon);
        public Coupon GetCoupon(int couponId);
        public Coupon GetVendorCoupon(int couponId,string vendorId);

        public void saveChanges();
    }
}
