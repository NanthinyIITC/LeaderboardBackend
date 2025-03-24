using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.DailyReportDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.DailyReportDetails
{
    public interface IDailyReportDataAccess
    {
        //insert list of daily reports
        void InsertBulkDailyReports(DailyReport reports);
        /// <summary>
        /// get report details per staff
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        List<ReportDetails> GetReportDetailsByStaffId(int staffId, string date, Filter filter);
        /// <summary>
        /// get report details per staff without pagination
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        List<ReportDetails> GetReportDetailsByStaffId(int staffId, string date);
        //get list of task type details
        List<TaskDetails> GetTaskTypesDetails();
        /// <summary>
        /// Get all daily reports by date to all staffs
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        List<ReportDetails> GetAllReportDetailsByDate(string date, Filter filter);
        /// <summary>
        /// delete daily report task from table
        /// </summary>
        /// <param name="reportId"></param>
        void DeleteDailyReportTask(int reportId);
       /// <summary>
       /// Get staff activity summary by daily report
       /// </summary>
       /// <param name="start"></param>
       /// <param name="end"></param>
       /// <param name="staffId"></param>
       /// <param name="taskTypeId"></param>
       /// <returns></returns>
       ChartData GetStaffActivitySummary(ChartViewRequest chart);
    }
}
