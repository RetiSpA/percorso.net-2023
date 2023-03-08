using Esercizio5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Esercizio5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public IActionResult CreateToken([FromBody] LoginModel login)
        {
            //call this endpoint whit POST and Data {"username": "mario", "password": "secret"}
            //return the token

            var user = Authenticate(login);

            if (user is null)
                return Unauthorized();

            var tokenString = BuildToken(user);
            return Ok(new { token = tokenString });
        }

        private string BuildToken(UserModel user)
        {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Birthdate, user.Birthdate.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            if (user.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
                claims.Add(new Claim(ClaimTypes.Role, "Super-Administrator"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "Contributor"));
                claims.Add(new Claim("PhoneController", "true"));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(LoginModel login)
        {
            UserModel user = null;

            if (login.Username == "mario" && login.Password == "secret")
            {
                user = new UserModel { Name = "Mario Rossi", Email = "mario.rossi@domain.com", IsAdmin = false, Birthdate = DateTime.Now.AddYears(-30) };
            }

            if (login.Username == "Admin" && login.Password == "Password01")
            {
                user = new UserModel { Name = "Admin", Email = "admin@domain.com", IsAdmin = true, Birthdate = DateTime.Now.AddYears(-10) };
            }

            return user;
        }
    }
}