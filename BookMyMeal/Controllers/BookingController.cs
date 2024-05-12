using BookMyMeal.Model;
using BookMyMeal.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookMyMeal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IOrderRepository _orderClass;


        public BookingController(IOrderRepository bookMyMeal)
        {
            _orderClass = bookMyMeal;
 
          

        }

        [HttpPost("setBookOrder")]
        public ActionResult setBookOrder(Order orderLog)
        {
           
            if (orderLog == null)
            {

                return BadRequest();
            }
            else
            {


                decimal BookOrderId = _orderClass.BookOrder(orderLog);
                if (BookOrderId > 0)
                {
                  
                    
                    return Ok(BookOrderId);
                }
                else
                {
                    return BadRequest(BookOrderId);
                }

            }



        }

        [HttpPost("setBulkOrder")]
        public ActionResult setBulkOrder(Order order)
        {
            if (_orderClass == null)
            {
                return BadRequest();
            }
            else
            {

                decimal BulkOrderId = _orderClass.BulkOrder(order);
                if (BulkOrderId > 0)
                {
                    int orderId = (int)BulkOrderId;
                    var data = _orderClass.AdminBookedBookingDates(order.EMPLOYEEIDBOOKEDBY);
                    if (data)
                    {
                        var Emailid = _orderClass.getEmailBookedFor(orderId);
                        if (Emailid != null)
                        {
                            _orderClass.AdminBooKingNotification(Emailid);
                        }
                    }
                    return Ok(BulkOrderId);
                }
                else { return BadRequest(BulkOrderId); }

            }
        }

        [HttpPut("CancellBookingForLunch")]
        public ActionResult CancellBookingForLunch(int id)
        {

            bool resultCancel = _orderClass.CancelOrderForLunch(id);
            if (resultCancel)
            {
                var emailId = _orderClass.getEmail(id);
                _orderClass.cancelBookingNotification(emailId);
                return Ok(resultCancel);
            }
            else
            {
                return BadRequest(resultCancel);
            }

        }

        [HttpPut("CancellBookingForDinner")]
        public ActionResult CancellBookingForDinner(int id)
        {

            bool resultCancel = _orderClass.CancelOrderForDinner(id);
            if (resultCancel)
            {
                      var emailId= _orderClass.getEmail(id);
                _orderClass.cancelBookingNotification(emailId);
                return Ok(resultCancel);
         
            }
            else
            {
                return BadRequest(resultCancel);
            }

        }

        [HttpGet("{id}")]
        public ActionResult getOderHistory([FromRoute] int id)
        {
            if (id != null)
            {
                var orderHistory = _orderClass.OrderHistory(id);
                return Ok(orderHistory);

            }
            else
            {
                return NotFound();
            }

        }





    }


}

