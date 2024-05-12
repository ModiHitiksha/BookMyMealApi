using BookMyMeal.Model;
using BookMyMeal.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
    
namespace BookMyMeal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _user;
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _order;

        public UserController(IUserRepository userRepository, IConfiguration configuration,IOrderRepository order)
        {
            _user = userRepository;
            _configuration = configuration;
            _order = order;
        }

        [HttpPost("setUserDetail")]
        public ActionResult setUserDetail(EmployeeDetail emp)
        {
            if (emp != null)
            {

                decimal RegisterUserId = _user.RegisterUser(emp);
                if (RegisterUserId < 0)
                {
                    return BadRequest(RegisterUserId);
                }
                else
                {
                    return Ok(RegisterUserId);
                }
            }
            else
            {
                return BadRequest();

            }
        }

        [HttpPut("UpdatePassword")]
        public ActionResult UpdatePassword(UpdatePassword updatePassword)
        {
           

            if (updatePassword != null)
            {

                bool isUpdated = _user.UpdatePassword(updatePassword);
                if (isUpdated)
                {
                    return Ok(isUpdated);

                }
                else
                {
                    return BadRequest(isUpdated);
                }
            }
            else { return BadRequest(); }
        }


        private string GenerateToken(LoginAuthentication authentication)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            int empId = _user.getEmpid(authentication.email);
            var isAdmin = _order.AdminBookedBookingDates(empId);
            string Role;
            if (isAdmin)
            {
                Role = "Admin";
            }
            else
            {
                Role = "Engineering";
            }
            var claims = new[] 
            {
                  new Claim("EmpId",empId.ToString(), ClaimValueTypes.Integer),
                  new Claim("Role",Role.ToString(),ClaimValueTypes.String),

                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30), // Adjust the expiry time as needed
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
          
        }

        [HttpPost("LoginAuthentication")]
        public ActionResult LoginAuthentication(LoginAuthentication authentication)
        {
        

            bool result = _user.LoginUser(authentication);


            if (result)
            {
                var genToken = GenerateToken(authentication);
                string setTokenResut = _user.setToken(genToken, authentication.email);

                return Ok(setTokenResut);
                


            }
            else
            {
                return BadRequest();
            }
        }

  
        [HttpDelete]
        public ActionResult LogOut()
        {
            
            string bearerToken = Request.Headers[HeaderNames.Authorization];
            string accessTokenWithoutBearerPrefix = bearerToken.Substring("bearer ".Length);
            bool result = _user.signOut(accessTokenWithoutBearerPrefix);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }




    }
}
