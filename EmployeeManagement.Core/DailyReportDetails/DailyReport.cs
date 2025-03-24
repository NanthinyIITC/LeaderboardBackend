using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.DailyReportDetails
{
    public class DailyReport
    {
        public int StaffId { get; set; }
        public List<ReportDetails> Reports { get; set; }
    }
    public class ReportDetails
    {
        public int Id { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Comments { get; set; }
        public int Percentage { get; set; }
        public double Duration { get; set; }
        public DateTime CreatedDate { get; set; }
     
        public int TaskId { get; set; }
        public string Task { get; set; }
        public int TotalRecords { get; set; }
    }
}
