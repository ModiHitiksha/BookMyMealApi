using BookMyMeal.Model;

namespace BookMyMeal.Repository.Interface
{
    public interface IPostCouponRepository
    {
        List<CouponDetails> getPostRedemptionCouponDetail(int Empid);
    }
}
