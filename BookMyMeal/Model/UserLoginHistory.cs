
namespace BookMyMeal.Model
{
    public class UserLoginHistory
    {
        public int LoginId { get; set; }
        public string emailId { get; set; }
        public int EmpId { get; set; }

        public string Token { get; set; }

        public DateTime LoginDateTime { get; set; }

    }
    public class LoginAuthentication
    {
        public string email { get; set;}
        public string password { get; set;}
    }

}
