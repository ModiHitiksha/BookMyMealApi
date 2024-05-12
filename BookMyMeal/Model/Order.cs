using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Principal;

namespace BookMyMeal.Model
{
    public class Order
    {
        public int? ORDERID { get; set; }
        [Required]
        public int EMPLOYEEIDBOOKEDBY { get; set; }
        [Required]
        public int EMPLOYEEIDBOOKEDFOR { get; set; }
       public string? ORDERTYPE { get; set; }
        public DateTime? BOOKINGDATE { get; set; }
        public DateTime? BOOKINGINITIALDATE { get; set; }
        public DateTime? BOOKINGENDDATE { get; set; }
        public string? ORDERSTATUS { get; set; }
        public string MealType { get; set; }


    }


    public class OrderHistory
    {
      
         public int OrderId { get; set; }   
        public int OrderBookedFor { get; set; }
        public string OrderType { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime BookingInitialDate { get; set; }
        public DateTime BookingEndDate { get; set; }
        public string MealType { get; set; }
        public string OrderStatus { get; set; }

    }
    public class BookingDatesNotification
    {

        
        public DateTime BookingInitialDate { get; set; }
        public DateTime BookingEndDate { get; set; }


    }
}