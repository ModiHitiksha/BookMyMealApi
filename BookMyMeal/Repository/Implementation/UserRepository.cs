using BookMyMeal.Model;
using BookMyMeal.Repository.Interface;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BookMyMeal.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private string _cs;

        public UserRepository(IConfiguration configuration)
        {
            _cs = configuration.GetConnectionString("dbcs");

        }

        #region LoginAuthentication
        public bool LoginUser(LoginAuthentication authentication)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spAuthenticationLogin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@emailid", authentication.email);
                    cmd.Parameters.AddWithValue("@password", authentication.password);
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
            catch
            {
                return false;
            }

        }
        #endregion

        #region Registration
        public decimal RegisterUser(EmployeeDetail employeeDetail)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spSetEmpData", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empid", employeeDetail.EMPLOYEEID);
                    cmd.Parameters.AddWithValue("@name", employeeDetail.EmpName);
                    cmd.Parameters.AddWithValue("@emailid", employeeDetail.EMAIL);
                    cmd.Parameters.AddWithValue("@phoneno", employeeDetail.PHONENUMBER);
                    cmd.Parameters.AddWithValue("@password", employeeDetail.PASSWORD);
                    cmd.Parameters.AddWithValue("@Role", employeeDetail.Role);

                    decimal _UserId = (decimal)cmd.ExecuteScalar();
                    if (_UserId > 0)
                    {
                        return _UserId;
                    }
                    else
                    {
                        return _UserId;
                    }

                }
            }
            catch
            {
                return -1;

            }



        }

        #endregion

        #region ViewBooking
        public List<UserLoginHistory> getEmpDetails(string token)
        {
            List<UserLoginHistory> empDetails = new List<UserLoginHistory>();
            using (SqlConnection con = new SqlConnection(_cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("getDetailsByToken", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@token", token);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var employeeDetail = new UserLoginHistory()
                    {
                        emailId = reader["emailId"].ToString(),
                        EmpId = (int)reader["EmpId"],
                        Token = reader["token"].ToString()
                    };
                    empDetails.Add(employeeDetail);
                }
                return empDetails;
                



            }
        }

        #endregion

        #region LoginLog
        public string setToken(string token , string email)
        {
            using(SqlConnection con= new SqlConnection(_cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spInsertLoginLog", con);
                cmd.CommandType=System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@emailid", email);
                cmd.Parameters.AddWithValue("@token", token);
                
               string Token= cmd.ExecuteScalar().ToString();
                if(Token != null)
                {
                    return Token;
                }
                else
                {
                    return null;
                }
            }
        }

       public  int getEmpid(string email)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spgetEmpIdByEmail", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@emailId", email);
                    int Empid = (int)cmd.ExecuteScalar();
                    return Empid;
                  
                   
                }
            }
            catch { return 0; }
      
        }

        #endregion

        #region UpdatePassword
        public bool UpdatePassword(UpdatePassword updatePassword)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SPCHANGEPASSWORD", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpId", updatePassword.EmpId);
                    cmd.Parameters.AddWithValue("@CURRENT_PASSWORD", updatePassword.PASSWORD);
                    cmd.Parameters.AddWithValue("@NEW_PASSWORD", updatePassword.NewPassword);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }
            }
            catch { return false ; }

        }
        #endregion

        #region SignOut
        public bool signOut(string token)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(_cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spSignOut",con);
                    cmd.CommandType= System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@token", token);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}