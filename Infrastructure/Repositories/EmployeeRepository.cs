using Core.Entities;
using Core.Interfaces;
using Core.ViewModel;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDBContext context;
        public EmployeeRepository(EmployeeDBContext context)
        {
            this.context=context;
        }
        public async Task<Employee> CreateEmployee(Employee newemployee)
        {

            var res = await context.Employees.AddAsync(newemployee);
            await context.SaveChangesAsync();
            return res.Entity;
        }


        public async Task<Employee> DeleteEmployee(int EmployeeID)
        {
            var res = await context.Employees.FirstOrDefaultAsync(e => e.Id == EmployeeID);
            if (res != null)
            {
                context.Employees.Remove(res);
                await context.SaveChangesAsync();
                return res;
            }
            return null;
        }

        public async Task<IEnumerable> GetAllEmployees()
        {
            var res = await context.Employees.Include(a=>a.Education).ToListAsync();
            return res;
        }

        public async Task<IEnumerable> GetEducationDropDown()
        {
            var res = await context.Educations.ToListAsync();
            return res;
        }

        public async Task<Employee> GetEmployeeByID(int EmployeeID)
        {
            var res = await context.Employees
                .FirstOrDefaultAsync(x => x.Id == EmployeeID);         
            return res;
        }     
        public async Task<Employee> UpdateEmployee(EmployeeViewModel employee)
        {
            var res = await context.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);
            var file = employee.ImageFile;
            if (file != null)
            {
                if (file.Length > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {

                        res.Name = employee.Name;
                        res.Email = employee.Email;
                        res.Mobile = employee.Mobile;
                        res.EducationId = employee.EducationId;
                        res.FileName = file.FileName;
                        res.ContentType = file.ContentType;
                        
                        using (var memoryStream = new MemoryStream())
                        {
                            await stream.CopyToAsync(memoryStream);
                            res.Data = memoryStream.ToArray();
                        }
                        context.Employees.Update(res);
                        context.SaveChanges();
                    }

                }
            }
            else
            {
                res.Name = employee.Name;
                res.Email = employee.Email;
                res.Mobile = employee.Mobile;
                res.EducationId = employee.EducationId;
               

               
                context.Employees.Update(res);
                context.SaveChanges();
            }
            return null;
        } 
    }
}
