using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Staff
{
    public class StaffDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string ContactNumber { get; set; }
        public string Designation { get; set; }
        public int UserTypeId { get; set; }
        public string UserType { get; set; }
        public int TotalRecords { get; set; }

    }
}
