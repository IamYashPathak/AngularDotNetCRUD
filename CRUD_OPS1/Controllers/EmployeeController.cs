using CRUD_OPS1.Model;
using CRUD_OPS1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_OPS1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo repo;
        private readonly ICredentialRepo credRepo;

        public EmployeeController(IEmployeeRepo repo, ICredentialRepo credRepo)
        {
            this.repo = repo;
            this.credRepo = credRepo;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() { 
            var _list = await this.repo.GetAll();

            if (_list != null)
            {
                return Ok(_list);
            }
            else { 
                return NotFound();
            }
        }

        [HttpGet("GetById/{userId}")]
        public async Task<IActionResult> GetById(int userId)
        {
            var _list = await this.repo.GetById(userId);

            if (_list != null)
            {
                return Ok(_list);
            }
            else
            {
                return NotFound();
            }
        }

        //[HttpPost("Insert")]
        //public async Task<IActionResult> Insert([FromBody] Employee emp)
        //{
        //    var response = await this.repo.Insert(emp);
        //    return Ok(response);
        //}

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Employee emp)
        {
            var response = await this.repo.Update(emp);

            return Ok(response);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int userId,string email)
        {
            Console.WriteLine(email);
            this.credRepo.DeleteById(email);
            var response = await this.repo.DeleteById(userId);

            if (response != null) {
                var obj = new { message = "true" };
                //return Ok("Employee Valid - ");
                return new ObjectResult(obj);
            } else {
                var obj = new { message = "Employee Not Found" };

                return new ObjectResult(obj);
            }
        }
    }
}/**/
