using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Leave
{
    public class LeaveDetails
    {
       
        public string LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double NumberOfDays { get; set; }
        public int Year { get; set; }
        public bool IsHalfday { get; set; }
        public bool IsMorningHalfday { get; set; }
        public bool IsAfternoonHalfday { get; set; }
        public bool IsFullday { get; set; }
        public string Description { get; set; }
        public string AdminComment { get; set; }
        public string LeaveStatus { get; set; }
        public int TotalRecords { get; set; }

    }
    public class LeaveCalenderOptions
    {
        public string Title { get; set; }         
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Color { get; set; }
        public string LeaveName { get; set; }
        public bool IsHalfday { get; set; }
        public bool IsMorningHalfday { get; set; }
        public bool IsAfternoonHalfday { get; set; }
        public bool IsFullday { get; set; }       
    }
}
