using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Leave
{
    public class LeaveBalance
    {
        public int StaffId { get; set; }      
        public int LeaveTypeId { get; set; }
        public string LeaveType { get; set; }
        public double NumberOfDays { get; set; }
        public int Year { get; set; }
    }
}
