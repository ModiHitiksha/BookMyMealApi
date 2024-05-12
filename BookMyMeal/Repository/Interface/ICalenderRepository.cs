using BookMyMeal.Model;


namespace BookMyMeal.Repository.Interface
{
    public interface ICalenderRepository
    {
        public List<DateTime> getBlockedDate();
        public DateTime getBookingDate(int EmpId);
        public DateTime getBookingDateEndDate(int EmpId);

    }
}
