using CRUD_OPS1.Model;
using CRUD_OPS1.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_OPS1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IEmployeeRepo empRepo;
        private readonly ICredentialRepo credRepo;

        public LoginController(ICredentialRepo cred, IEmployeeRepo repo)
        {
            this.empRepo = repo;
            this.credRepo = cred;
        }

        [HttpPost("login")]
        public async Task<IActionResult> ValidateEmployee([FromBody] Credentials cred) {

            var _creds = await this.credRepo.ValidateCredentials(cred);

            if (_creds != null)
            {
                var obj = new {message = "true" };
                //return Ok("Employee Valid - ");
                return new ObjectResult(obj) ;
            }
            else {
                var obj = new { message = "false" };

                return new ObjectResult(obj);
            }
        }
    }
}
