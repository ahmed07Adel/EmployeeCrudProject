using Core.Entities;
using Core.Interfaces;
using Core.ViewModel;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUser
    {
        private readonly EmployeeDBContext context;
        public UserRepository(EmployeeDBContext context)
        {
            this.context = context;
        }
        public User CreateUser(RegisterViewModel registerViewModel)
        {
            var user = new User
            {
                Name = registerViewModel.Name,
                Email = registerViewModel.Email,    
                Password = BCrypt.Net.BCrypt.HashPassword(registerViewModel.Password),
                RoleId = registerViewModel.RoleId
            };
            context.Users.Add(user);
             context.SaveChanges();
            return user;
        }

        public IEnumerable<Role> GetRoles()
        {
            var res = context.Roles.ToList();
            return res; 
        }

        public User GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(a => a.Email == email);
        }

        public User GetUserById(int id)
        {
            return context.Users.FirstOrDefault(a => a.Id == id);
        }
    }
}
