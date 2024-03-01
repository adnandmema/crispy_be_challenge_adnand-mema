using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Infrastructure.Authentication.Core.Model;

namespace ToDoApp.Infrastructure.Authentication.Core.Services
{
    public interface ITokenService
    {
        TokenModel CreateAuthenticationToken(string userId, string uniqueName, IEnumerable<(string claimType, string claimValue)>? customClaims = null);
    }
}
