using BookMyMeal.Model;
using BookMyMeal.Repository.Interface;
using Microsoft.Data.SqlClient;

namespace BookMyMeal.Repository.Implementation
{
    public class PostCouponRepository:IPostCouponRepository
    {
        private readonly string _cs;
        public PostCouponRepository(IConfiguration configuration)
        {
            _cs = configuration.GetConnectionString("dbcs");
        }
        #region getPostRedemptionDetail
        public List<CouponDetails> getPostRedemptionCouponDetail(int Empid)
        {
            try
            {
                List<CouponDetails> couponDetails = new List<CouponDetails>();
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spPostRedemptionCouponDetails", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpId", Empid);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var coupon = new CouponDetails()
                        {
                            Date = Convert.ToDateTime(reader[0]),
                            Day = reader[1].ToString(),
                            EmployeeName = reader[2].ToString(),
                            EmployeeId = Convert.ToInt32(reader[3]),
                            CouponNo = reader[4].ToString(),
                            RedemptionStatus = reader[5].ToString(),
                            MealType = reader[6].ToString()

                        };
                        couponDetails.Add(coupon);
                    }
                    if (couponDetails != null)
                    {
                        return couponDetails;
                    }
                    else
                    {
                        return null;
                    }

                }

            }
            catch
            {
                return null;

            }
        }

        #endregion
    }
}
