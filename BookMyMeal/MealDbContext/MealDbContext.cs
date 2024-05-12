using BookMyMeal.Model;
using Microsoft.EntityFrameworkCore;
namespace BookMyMeal.MealDbContext
{
    public class MealDbContext:DbContext
    {
        public MealDbContext(DbContextOptions<MealDbContext> options):base(options) 
        {
            Database.EnsureCreated();

        }

        public DbSet<EmployeeDetail> EmployeeDetails { get; set; }
        public DbSet<Order> OrderDetails { get; set; }
        public DbSet<UserLoginHistory> UserLoginHistory { get; set; }
        public DbSet<CouponDetails> CouponDetails { get; set; }
    }
}
