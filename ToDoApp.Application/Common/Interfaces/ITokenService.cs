using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Common.Models;

namespace ToDoApp.Application.Common.Interfaces
{
    public interface ITokenService
    {
        TokenModel CreateAuthenticationToken(string userId, string uniqueName, IEnumerable<(string claimType, string claimValue)>? customClaims = null);
    }
}
