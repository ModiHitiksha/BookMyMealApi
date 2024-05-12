namespace BookMyMeal.Model
{
    public class Calender
    {
        public int? CALERDATEID { get; set; }
        public DateTime BLOCKEDDATE { get; set; }
     
    }
    public class BookingDate
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
