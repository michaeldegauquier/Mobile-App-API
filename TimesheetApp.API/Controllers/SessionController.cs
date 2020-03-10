using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TimesheetApp.API.Dto;
using TimesheetApp.API.Services;

namespace TimesheetApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : Controller
    {
        private readonly IServices _service;
        public IConfiguration _config;

        public SessionController(IServices service, IConfiguration config)
        {
            _service = service;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserToLoginDto userToLogin)
        {
            try
            {
                var user = await _service.CreateSession(userToLogin);

                if (user == null)
                {
                    //Unauthorized
                    return StatusCode(401, "Bad login");
                }
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("Appsettings:Token").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenhandler = new JwtSecurityTokenHandler();
                var token = tokenhandler.CreateToken(tokenDescriptor);

                //Ok
                return StatusCode(200, new { token = tokenhandler.WriteToken(token) });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete()
        {
            //Implement logout (bail)
            return StatusCode(200, "Successfully logged out.");
        }
    }
}