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
        [HttpGet("GetEducationDropDown")]
        public async Task<IActionResult> GetEducationDropDown()
        {
            try
            {
                return Ok(await repository.GetEducationDropDown());
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
        public async Task<ActionResult<Employee>> CreateEmployee([FromForm]EmployeeViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                var file = model.ImageFile;
                if (file != null)
                {
                    if (file.Length > 0)
                    {
                        using (var stream = file.OpenReadStream())
                        {
                            var entity = new Employee
                            {
                                Name = model.Name,
                                Email = model.Email,
                                Mobile = model.Mobile,
                                EducationId = model.EducationId,
                                FileName = file.FileName,
                                ContentType = file.ContentType
                            };
                            using (var memoryStream = new MemoryStream())
                            {
                                await stream.CopyToAsync(memoryStream);
                                entity.Data = memoryStream.ToArray();
                            }
                            var res = await repository.CreateEmployee(entity);

                        }

                    }
                }
                else
                {
                    var entity = new Employee
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Mobile = model.Mobile,
                        EducationId = model.EducationId,

                    };
                    var res = await repository.CreateEmployee(entity);
                }
                return Ok();

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Create data");
            }
        }
        [HttpPost("UpdateEmployee")]
        public async Task<ActionResult<Employee>> UpdateEmployee([FromForm] EmployeeViewModel employeeModel)
        {
            try
            {
                var Prod = await repository.GetEmployeeByID(employeeModel.Id);
                if (Prod == null)
                {
                    return NotFound($"this Employee = {employeeModel.Id} Can not found");
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
