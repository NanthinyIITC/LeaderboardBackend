using EmployeeManagement.Core.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Authentication
{
    public interface IAuthenticationDataAccess
    {
        /// <summary>
        /// find inserted staff details with email and password. If staff not added then status of auth response will be false
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        AuthResponse CheckStaffExistWithEmailAndPassword(string email, string password);
    }
}
