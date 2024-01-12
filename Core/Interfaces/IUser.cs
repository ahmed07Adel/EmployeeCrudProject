using Core.Entities;
using Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUser
    {
        User CreateUser(RegisterViewModel registerViewModel);
        User GetUserByEmail(string email);
        User GetUserById(int id);
        IEnumerable<Role> GetRoles();   
    }
}
