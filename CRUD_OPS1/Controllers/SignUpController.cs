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
        public SignUpController(IEmployeeRepo repo)
        {
               this.empRepo = repo;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] Employee emp)
        {
            var response = await this.empRepo.Insert(emp);
            return Ok(response);
        }

    }
}
