using EmployeeManagement.Core.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Service.Authentication
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// staff login with email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        AuthResponse LoginWithEmailAndPassword(string email, string password);
    }
}
