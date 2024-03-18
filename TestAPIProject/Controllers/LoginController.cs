using TestAPIProject.Data;
using TestAPIProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TestAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly ProductDBContext learnDBContext;
        public readonly IConfiguration Configuration;
        public LoginController(ProductDBContext learnDB, IConfiguration configuration)
        {
            learnDBContext = learnDB;
            this.Configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var user = await this.learnDBContext.Login.FirstOrDefaultAsync(item => item.UserName == userLogin.UserName && item.Password == userLogin.Password);
            if (user != null)
            {
                //generate token
                var tokenhandler = new JwtSecurityTokenHandler();
                var tokenkey = Encoding.UTF8.GetBytes(Configuration.GetSection("JwtSettings:securitykey").Value);
                var tokendesc = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name,userLogin.UserName),
                        new Claim(ClaimTypes.Role,userLogin.Password)
                    }),
                    Expires = DateTime.UtcNow.AddSeconds(300),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
                };
                var token = tokenhandler.CreateToken(tokendesc);
                var finaltoken = tokenhandler.WriteToken(token);
                return Ok(new TokenResponse() { Token = finaltoken });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}

