using BookMyMeal.Model;

namespace BookMyMeal.Repository.Interface
{
    public interface ICouponRepository
    {
        List<CouponDetails> getPreRedemptionCouponDetail(int Empid);
        bool CouponRedemption(string couponNo);



    }
}
