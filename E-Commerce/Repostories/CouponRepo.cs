using E_Commerce.Data;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repostories
{
    public class CouponRepo : ICouponRepo
    {
        private AppDbContext context;

        public CouponRepo(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<string> AddCoupon(Coupon coupon)
        {
            try
            {
                await context.Coupons.AddAsync(coupon);
                return "Success";
            }catch(Exception e)
            {
                return e.Message;
            }
        }

        public string DeleteCoupon(int couponId, string vendorId)
        {
            var coupone = GetCoupon(couponId);
            if (!coupone.VendorId.Equals(vendorId) || coupone != null)
            {
                return "Not Found";
            }
            else 
            {
                context.Coupons.Remove(coupone);
                return "Success";
            }
        }

        public Coupon GetCoupon(int couponId)
        {
           return context.Coupons.FirstOrDefault(x => x.CouponId == couponId);
        }

        public async Task<Coupon> GetCouponeProduct(int id)
        {
            
            var coupon =await context.Coupons.Include(x => x.products).FirstOrDefaultAsync(x=>x.CouponId==id);
            return coupon;
            
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

        public string UpdateCoupon(Coupon coupon)
        {
            try
            {
                var coupone = context.Coupons.FirstOrDefault(x => x.Code.Equals(coupon.Code) && x.VendorId.Equals(coupon.VendorId));
                if ( coupone == null)
                {
                    return "UnAuthorized";
                }
                else
                {
                    context.Coupons.Update(coupon);
                    return "Success";
                }
            }catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}
