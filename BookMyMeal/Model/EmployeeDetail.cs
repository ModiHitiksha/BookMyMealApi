using System.Data;

namespace BookMyMeal.Model
{
    public class EmployeeDetail
    {
        public int? EDID { get; set; }
        public int EMPLOYEEID { get; set; }
        public string EmpName { get; set; }
        public string PHONENUMBER { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
        public DateTime? REGISTRATIONDATE { get; set; }
        public DateTime?  MODIFIEDON { get; set; }
        public string Role { get; set; }

    }
    public class UpdatePassword
    {
        public int EmpId { get; set; }
        public string PASSWORD { get; set; }
        public string NewPassword { get; set; }


    }
}
