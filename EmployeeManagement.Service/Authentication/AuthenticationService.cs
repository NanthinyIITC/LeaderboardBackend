using EmployeeManagement.Core.Authentication;
using EmployeeManagement.DataAccess.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        //private properties
        private readonly IAuthenticationDataAccess _iauthenticationDataAccess;
        //constructor
        public AuthenticationService(IAuthenticationDataAccess iauthenticationDataAccess)
        {
                _iauthenticationDataAccess = iauthenticationDataAccess;
        }
        /// <summary>
        /// Implementation of LoginWithEmailAndPassword method
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public AuthResponse LoginWithEmailAndPassword(string email, string password)
        {
           //return response
           return _iauthenticationDataAccess.CheckStaffExistWithEmailAndPassword(email,password);
        }
    }
}
