using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.DailyReportDetails
{
    public class DailyReportChart
    {
        public string Label { get; set; }
        public double Data { get; set; }
    }
    public class ChartData
    {
        public List<DailyReportChart> DailyReportChartData { get; set; }
        public double TotalWorkingTimeInMins { get; set; }
        public double TotalWorkingDays { get; set; }
        public double TotalActualWorkingDays { get; set; }
        public double TotalLeaveDays { get; set; }       
    }
    public class ChartViewRequest
    {
        public string Start { get; set; }
        public string End { get; set; }
        public int StaffId { get; set; }
        public int TaskId { get; set; }
    }
}
