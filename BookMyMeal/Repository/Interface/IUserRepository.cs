using BookMyMeal.Model;
using System.ComponentModel.DataAnnotations;

namespace BookMyMeal.Repository.Interface
{
    public interface IUserRepository
    {
        decimal RegisterUser(EmployeeDetail employeeDetail);

        bool UpdatePassword(UpdatePassword updatePassword);
        bool LoginUser(LoginAuthentication authentication);
        int getEmpid( string email);
        List<UserLoginHistory> getEmpDetails(string token);
        string setToken(string token,string email);  
        bool signOut (string token);
    }
}
