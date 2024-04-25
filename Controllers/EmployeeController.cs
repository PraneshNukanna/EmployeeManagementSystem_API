using API_ManagementSystem_ClassActivity.RequestsLayer;
using API_ManagementSystem_ClassActivity.ResponseLayer;
using API_ManagementSystem_ClassActivity.ServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace API_ManagementSystem_ClassActivity.Controllers
{
    [ApiController]
    [Route("employees")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeResponse>>> GetAll()
        {
            var response = await _employeeService.GetAll();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResponse>> Get(Guid id)
        {
            var response = await _employeeService.Get(id);
            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<EmployeeResponse>>> Search(string keyword)
        {
            var response = await _employeeService.Search(keyword);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeResponse>> Create([FromBody] EmployeeRequest request)
        {
            var response = await _employeeService.Create(request);
            return StatusCode(StatusCodes.Status201Created, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeResponse>> Update(Guid id, [FromBody] EmployeeRequest request)
        {
            var response = await _employeeService.Update(id, request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _employeeService.Delete(id);
            return NoContent();
        }
    }
}
