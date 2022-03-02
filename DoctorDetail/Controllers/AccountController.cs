using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoctorDetail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public string UserValidation(int empid, string pass)
        {
            var val = (from data in _employeeService.GetEmployeesList()
                       where data.UserName == empid && data.Password == pass
                       select data).FirstOrDefault();
            if (val == null)
            {
                return null;
            }
            else
            {
                var key = _configuration.GetValue<string>("JwtConfig:Key");
                var keybytes = Encoding.ASCII.GetBytes(key);
                var tokenhandler = new JwtSecurityTokenHandler();
                var tokendescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, empid.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(keybytes), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenhandler.CreateToken(tokendescriptor);
                return tokenhandler.WriteToken(token);
            }
        }
    }
}
