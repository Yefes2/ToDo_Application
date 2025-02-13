using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Business.Interfaces
{
    public interface IUserService
    {
        User GetUserById(int id);
        User GetUserByUsername(string username);
        bool Register(string username, string password);
        bool Login(string username, string password);
    }
}
