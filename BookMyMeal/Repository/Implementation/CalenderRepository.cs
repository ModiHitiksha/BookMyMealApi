using BookMyMeal.Model;
using BookMyMeal.Repository.Interface;
using Microsoft.Data.SqlClient;

namespace BookMyMeal.Repository.Implementation
{
    public class CalenderRepository : ICalenderRepository
    {
        private readonly string _cs;
        public CalenderRepository(IConfiguration configuration)
        {
            _cs = configuration.GetConnectionString("dbcs");
        }
        #region BlockDates
        public List<DateTime> getBlockedDate()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_cs))
                {
                    conn.Open();
                    List<DateTime> calenders = new List<DateTime>();
                    SqlCommand cmd = new SqlCommand("SpShowBlockedDate", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        DateTime blockDate = (DateTime)reader["BLOCKEDDATE"];
                        calenders.Add(blockDate);




                    }
                    return calenders;
                }
            }
            catch(Exception ex) 
            {
                return null;
                Console.WriteLine(ex.Message);
            }
          
            
        }

        #endregion

        #region StartAndEndDate
        public DateTime getBookingDate(int EmpId)
        {
            try
            {

            
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spGetBookingDate", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empid", EmpId);
                    DateTime StartDate = Convert.ToDateTime(  cmd.ExecuteScalar() );
                    return StartDate;
                }
            }
            catch
            {
                throw;
            }
        }
        public DateTime getBookingDateEndDate(int EmpId)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(_cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spGetBookingDateEndDate", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empid", EmpId);
                    DateTime EndDate =Convert.ToDateTime( cmd.ExecuteScalar());
                    return EndDate;

                }
            }
            catch
            {
                throw;           
            }
        }

        #endregion


    }
}
