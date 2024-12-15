using E_Commerce.Data;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Models;

namespace E_Commerce.Repostories
{
    public class CouponRepo : ICouponRepo
    {
        private AppDbContext context;

        public CouponRepo(AppDbContext context)
        {
            this.context = context;
        }

        public void AddCoupon(Coupon coupon)
        {
            context.Coupons.Add(coupon);
        }

        public int DeleteCoupon(int couponId, string vendorId)
        {
            var coupone = GetCoupon(couponId);
            if (!coupone.VendorId.Equals(vendorId) || coupone != null)
            {
                return 0;
            }
            else 
            {
                context.Coupons.Remove(coupone);
                return 1;
            }
        }

        public Coupon GetCoupon(int couponId)
        {
           return context.Coupons.FirstOrDefault(x => x.CouponId == couponId);
        }

        public List<Coupon> GetCoupons()
        {
            return context.Coupons.ToList();
        }

        public Coupon GetVendorCoupon(int couponId, string vendorId)
        {
            return context.Coupons.FirstOrDefault(x => x.CouponId == couponId && x.VendorId==vendorId);
        }

        public List<Coupon> GetVendorCoupons(string vendorId)
        {
            var vendorCoupons = new List<Coupon>();
            var coupons=  context.Coupons.ToList();
            foreach(var item in coupons)
            {
                if(item.VendorId.Contains(vendorId))
                {
                    vendorCoupons.Add(item);
                }
            }
            return vendorCoupons;
        }

        public void saveChanges()
        {
            context.SaveChanges();
        }

        public int UpdateCoupon(Coupon coupon)
        {
            var coupone = context.Coupons.FirstOrDefault(x=>x.Code.Equals(coupon.Code));
            if (!coupone.VendorId.Equals(coupon.VendorId) || coupone != null)
            {
                return 0;
            }
            else
            {
                context.Coupons.Update(coupon);
                return 1;
            }
        }
    }
}
