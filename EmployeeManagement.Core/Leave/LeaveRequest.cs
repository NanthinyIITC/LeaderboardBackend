using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Leave
{
    public class LeaveRequest
    {
        public int LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double NumberOfDays { get; set; }
        public int StaffId { get; set; }
        public bool IsHalfday { get; set; }
        public bool IsMorningHalfday { get; set; }
        public bool IsAfternoonHalfday { get; set; }
        public bool IsFullday { get; set; }
        public string Description { get; set; }
        public string AdminComment { get; set; }
        public int LeaveStatusId { get; set; }
        
    }
}
