using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Staff
{
    public class UserType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsEditable { get; set; }
    }
}
