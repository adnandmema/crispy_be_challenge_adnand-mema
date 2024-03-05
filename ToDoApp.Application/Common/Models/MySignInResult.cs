using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Application.Common.Models
{
    // Used instead of Identity.SignInResult to avoid unnecessary coupling
    // between authentication pipeline participants and Identity.
    public enum MySignInResult
    {
        Failed,
        Success,
        LockedOut,
        NotAllowed
    }
}
