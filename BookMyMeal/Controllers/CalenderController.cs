using BookMyMeal.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyMeal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CalenderController : ControllerBase
    {
        private readonly ICalenderRepository _calender;
        public CalenderController(ICalenderRepository calender)
        {
            _calender= calender;
        }

        [HttpGet]
        public ActionResult getBlockedDate()
        {
          var data  =_calender.getBlockedDate();
            return Ok(data);
        }

        [HttpGet("getBookingDate")]
        public ActionResult getBookingDateStart(int Empid)
        {
            var result = _calender.getBookingDate(Empid);
            return Ok(new { date = result.ToString("yyyy-MM-dd") });
        }

        [HttpGet("getBookingDateEnd")]
        public ActionResult getBookingDateEnd(int Empid)
        {
            var data = _calender.getBookingDateEndDate(Empid);
            return Ok(new { date = data.ToString("yyyy-MM-dd") });

        }
    }
}
