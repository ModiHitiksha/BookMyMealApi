using BookMyMeal.Model;
using BookMyMeal.Repository.Interface;
using Microsoft.Data.SqlClient;
using MimeKit;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

using MailKit.Net.Smtp;
using MailKit;

namespace BookMyMeal.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _cs;
        public OrderRepository(IConfiguration configuration)
        {
            _cs = configuration.GetConnectionString("dbcs");
        }

        #region MealBooking
        public decimal BookOrder(Order order_Log)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spQuickMeal", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookedBy", order_Log.EMPLOYEEIDBOOKEDBY);
                    cmd.Parameters.AddWithValue("@BookedFor", order_Log.EMPLOYEEIDBOOKEDFOR);
                    cmd.Parameters.AddWithValue("@MealType", order_Log.MealType);


                    decimal resultId = Convert.ToDecimal(cmd.ExecuteScalar());

                    if (resultId != null)
                    {
                        return resultId;
                    }
                    else
                    {
                        return -1;
                    }



                }
            }
            catch (Exception ex)
            {
                return -1;
            }


        }

        public decimal BulkOrder(Order order)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spBulkOrder", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpidFor", order.EMPLOYEEIDBOOKEDFOR);
                    cmd.Parameters.AddWithValue("@EmpidBy", order.EMPLOYEEIDBOOKEDBY);
                    cmd.Parameters.AddWithValue("@InitialDate", order.BOOKINGINITIALDATE);
                    cmd.Parameters.AddWithValue("@EndDate", order.BOOKINGENDDATE);
                    cmd.Parameters.AddWithValue("@MealType", order.MealType);

                    decimal resultId = (decimal)cmd.ExecuteScalar();

                    return resultId;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }

        }
        #endregion

        #region CancelOrder
        public bool CancelOrderForLunch(int OrderId)
        {
            try
            {
                /*int orderId = (int)this.resultId;*/
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SpCancelBookingLunch", con);
                    cmd.Parameters.AddWithValue("@ORDER_ID", OrderId);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    int rowsaffetced = cmd.ExecuteNonQuery();
                    if (rowsaffetced > 0)
                    {

                        return true;
                    }
                    else { return false; }

                }
            }
            catch
            {
                return false;
            }

        }

       
        public bool CancelOrderForDinner(int OrderId)
        {
            try
            {
                /*int orderId = (int)this.resultId;*/
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SpCancelBookingDinner", con);
                    cmd.Parameters.AddWithValue("@ORDER_ID", OrderId);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    int rowsaffetced = cmd.ExecuteNonQuery();
                    if (rowsaffetced > 0)
                    {

                        return true;
                    }
                    else { return false; }

                }
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region OrderHistory
        public List<OrderHistory> OrderHistory(int Empid)
        {
            try
            {
                List<OrderHistory> orderList = new List<OrderHistory>();
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("spBookingHistory", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empid", Empid);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        var orderHistory = new OrderHistory()
                        {
                            OrderId =(int) reader[0],
                            OrderBookedFor = Convert.ToInt32(reader[1]),
                            OrderType = reader[2].ToString(),
                            BookingDate = Convert.ToDateTime(reader[3]),
                            BookingInitialDate = Convert.ToDateTime(reader[4]),
                            BookingEndDate = Convert.ToDateTime(reader[5]),
                            MealType = reader[6].ToString(),
                            OrderStatus = reader[7].ToString()

                        };
                        orderList.Add(orderHistory);


                    }
                    return orderList;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Notification
        public void cancelBookingNotification(string Receiversaddress)
        {
            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress("Prajwal Patil", "patilprajwal264@mail.com"));
                email.To.Add(new MailboxAddress("Rishabh Employee", Receiversaddress));

                email.Subject = "Your Booking Successfully Cancelled";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = "<b>Hello , your Booking is successfully cancell</b>"
                };

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    smtp.Authenticate("rishabhboookmymeal@gmail.com", "smnt dtnf maqs pcqz");

                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
            }
            catch (Exception ex)
            {


                Console.WriteLine(ex.Message);

            }

        }
        public void AdminBooKingNotification(string Receiversaddress)
        {
            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress("Prajwal Patil", "patilprajwal264@mail.com"));
                email.To.Add(new MailboxAddress("Rishabh Employee", Receiversaddress));

                email.Subject = "Your Booking Has Been Made By Admin";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = $"<b>Hello, your booking has been made by admin .Kindly Login to our portal for more information</b>"
                };

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    smtp.Authenticate("rishabhboookmymeal@gmail.com", "smnt dtnf maqs pcqz");

                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);




            }

        }
        public string getEmail(int orderId)
        {
            try {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spGetDetailsByid", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@orderid", orderId);
                    string EmailId = cmd.ExecuteScalar().ToString();
                    return EmailId;
                }
            }
            catch(Exception ex)
            {
                return null;
                Console.WriteLine(ex.Message);
            }
          
        }
        public string getEmailBookedFor(int orderId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spGetEmailBookedFor", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@orderid", orderId);
                    string EmailId = cmd.ExecuteScalar().ToString();
                    return EmailId;
                }
            }
            catch (Exception ex)
            {
                return null;
                Console.WriteLine(ex.Message);
            }
         
        }

        public bool AdminBookedBookingDates( int EmpId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    con.Open();
                    List<BookingDatesNotification> BookingDates = new List<BookingDatesNotification>();
                    SqlCommand cmd = new SqlCommand("spGetDetailsByRole", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Empid", EmpId);
                    int result = (int)cmd.ExecuteScalar();
                    if (result == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch {
                return false;

            }
           


        }
        #endregion

    }
}



