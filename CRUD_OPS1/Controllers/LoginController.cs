using CRUD_OPS1.Model;
using CRUD_OPS1.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRUD_OPS1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IEmployeeRepo empRepo;
        private readonly ICredentialRepo credRepo;
        private readonly IConfiguration configuration;

        public LoginController(ICredentialRepo cred, IEmployeeRepo repo, IConfiguration configuration)
        {
            this.empRepo = repo;
            this.credRepo = cred;
            this.configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> ValidateEmployee([FromBody] Credentials cred) {

            var _creds = await this.credRepo.ValidateCredentials(cred);
            var emp = await this.empRepo.GetByEmail(cred.email);

            if (_creds != null)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("email",_creds.email.ToString()),
                    new Claim("role",emp.roleId.ToString())
                };

              

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials:signIn);

                string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new { token = tokenValue, role = emp.roleId, email= emp.email});
            }
            else {
                throw new AuthenticationFailureException("Invalid Credentials :(");
            }
        }
    }
}
