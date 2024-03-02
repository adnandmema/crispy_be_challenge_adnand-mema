using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Common.Models;

namespace ToDoApp.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<(MySignInResult result, SignInData? data)> SignIn(string username, string password);
    }
}
