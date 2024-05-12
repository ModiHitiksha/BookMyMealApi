using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace BookMyMeal.Model
{
 
    public class CouponDetails
    {
        public DateTime Date { get; set; }
        public string Day {  get; set; }    
        public string EmployeeName { get; set; }
        public int EmployeeId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CouponNo { get; set; }
        public string RedemptionStatus { get; set; }
        public string MealType { get; set; }
    }
    
}
