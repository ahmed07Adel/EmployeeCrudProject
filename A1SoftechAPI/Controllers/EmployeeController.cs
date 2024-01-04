using Core.Entities;
using Core.Interfaces;
using Core.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace A1SoftechAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository repository;
        public EmployeeController(IEmployeeRepository repository)
        {
            this.repository=repository;
        }
        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                return Ok(await repository.GetAllEmployees());
            }
            catch (Exception)
            { 

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }
        [HttpGet("GetEmployeeById/{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            try
            {
                var res = await repository.GetEmployeeByID(id);
                
                if (res == null)
                {
                    return NotFound();
                }
                return res;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }
        [HttpPost("CreateEmployee")]
        public async Task<ActionResult<Employee>> CreateEmployee(EmployeeViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                var entity = new Employee
                {
                    Name = model.Name,
                    Salary = model.Salary,
                    Email = model.Email,
                    Mobile = model.Mobile,
                };
               
                var res = await repository.CreateEmployee(entity);               
                return Ok(res); 
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Create data");
            }
        }
        [HttpPost("UpdateEmployee/{id:int}")]
        public async Task<ActionResult<Employee>> UpdateEmployee([FromBody] EmployeeViewModel employeeModel)
        {
            try
            {
                var Prod = await repository.GetEmployeeByID(employeeModel.Id);
                if (Prod == null)
                {
                    return NotFound($"this product = {employeeModel.Id} Can not found");
                }
                var res= await repository.UpdateEmployee(employeeModel);
                return Ok(res);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Updating data");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                return await repository.DeleteEmployee(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }
       
        
    }
}
