using BookMyMeal.Model;
using BookMyMeal.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BookMyMeal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PreCouponController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;
        public PreCouponController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;

        }
        
        [HttpGet("{Empid}")]
        public ActionResult getPreRedemptionDetails( int Empid)
        {

            var data = _couponRepository.getPreRedemptionCouponDetail(Empid).ToList();
            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest();
            }

        }
       
        [HttpPost("{CouponNo}")]
        public ActionResult isCouponRedeeem(string CouponNo)
        {
            var isRedeemed = _couponRepository.CouponRedemption(CouponNo);
            if (isRedeemed)
            {
                return Ok(isRedeemed);
            }
            else
            {
                return BadRequest(isRedeemed);
            }
        }
  
 

    }
}
