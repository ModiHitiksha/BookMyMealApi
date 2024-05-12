using BookMyMeal.Repository.Implementation;
using BookMyMeal.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyMeal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostCouponControlller : ControllerBase
    {
        private readonly IPostCouponRepository _postRedeem;
        public PostCouponControlller(IPostCouponRepository postRedeem)
        {
            _postRedeem = postRedeem;
        }
      
        [HttpGet("{EmpId}")]
        public ActionResult getPostRedemptionCouponDetail(int EmpId)
        {

            var data = _postRedeem.getPostRedemptionCouponDetail(EmpId);
            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
