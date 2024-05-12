using BookMyMeal.Model;
using BookMyMeal.Repository.Interface;
using Microsoft.Data.SqlClient;

namespace BookMyMeal.Repository.Implementation
{
    public class CouponRepository : ICouponRepository
    {
        private readonly string _cs;
        public CouponRepository(IConfiguration configuration)
        {
            _cs = configuration.GetConnectionString("dbcs");
        }
        #region PreRedemptionDetails
        public List<CouponDetails> getPreRedemptionCouponDetail(int Empid)
        {
            try
            {
                List<CouponDetails> couponDetails = new List<CouponDetails>();
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spPreRedemptionCouponDetails", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpId", Empid);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var couponInfo = new CouponDetails()
                        {
                            Date = Convert.ToDateTime(reader[0]),
                            Day = reader[1].ToString(),
                            EmployeeName = reader[2].ToString(),
                            EmployeeId = Convert.ToInt32(reader[3]),
                            CouponNo = reader[4].ToString(),
                            RedemptionStatus = reader[5].ToString(),
                            MealType = reader[6].ToString()

                        };
                        couponDetails.Add(couponInfo);
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

        #region CouponRedemption
        public bool CouponRedemption(string couponNo)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    con.Open() ;
                    SqlCommand cmd = new SqlCommand("spCouponRedemption", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@couponno", couponNo);
                    int rowsAffected = (int)cmd.ExecuteScalar();
                    if (rowsAffected==1 )
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        #endregion

   
    }

}
