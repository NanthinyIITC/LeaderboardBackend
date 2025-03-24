using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Authentication
{
    public class AuthResponse
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public bool IsAuthSuccess { get; set; }
        public int StaffId { get; set; }
    }
}
