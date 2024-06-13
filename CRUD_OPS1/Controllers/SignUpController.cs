using CRUD_OPS1.Dtos;
using CRUD_OPS1.Model;
using CRUD_OPS1.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_OPS1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IEmployeeRepo empRepo;
        private readonly ICredentialRepo credRepo;
        public SignUpController(IEmployeeRepo repo, ICredentialRepo credRepo)
        {
               this.empRepo = repo;
            this.credRepo = credRepo;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] Employee empDetails)
        {
            var response =  await this.empRepo.Insert(empDetails);
            this.credRepo.InsertNewCredentials(empDetails.email, empDetails.password);
            return Ok(response);
        }

    }
}
