using E_Commerce.Dtos;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Interfaces.ServicesInterfaces;
using E_Commerce.Models;

namespace E_Commerce.Services
{
    public class CouponServices : ICouponServices
    {
        private ICouponRepo couponRepo;

        public CouponServices(ICouponRepo couponRepo)
        {
            this.couponRepo = couponRepo;
        }

        public void AddCoupon(CouponDto coupon,string vendorId)
        {
            Coupon c = new Coupon
            {
                Code = coupon.Code,
                DiscountPercentage = coupon.DiscountPercentage,
                ValidFrom = coupon.ValidFrom,
                ValidTo = coupon.ValidTo,
                VendorId = vendorId
            };
            couponRepo.AddCoupon(c);
        }

        public int DeleteCoupon(int couponId, string vendorId)
        {
           return couponRepo.DeleteCoupon(couponId, vendorId);
        }

        public Coupon GetCoupon(int couponId)
        {
            return couponRepo.GetCoupon(couponId);
        }

        public List<Coupon> GetCoupons()
        {
            return couponRepo.GetCoupons();
        }

        public Coupon GetVendorCoupon(int couponId, string vendorId)
        {
            return couponRepo.GetVendorCoupon(couponId,vendorId);

        }

        public List<Coupon> GetVendorCoupons(string vendorId)
        {
            return couponRepo.GetVendorCoupons(vendorId);
        }

        public void saveChanges()
        {
            couponRepo.saveChanges();
        }

        public int UpdateCoupon(CouponDto coupon, string vendorId)
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
