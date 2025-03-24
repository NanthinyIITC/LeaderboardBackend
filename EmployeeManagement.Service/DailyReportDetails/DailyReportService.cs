using EmployeeManagement.Core.DailyReportDetails;
using EmployeeManagement.Core.Staff;
using EmployeeManagement.DataAccess.Authentication;
using EmployeeManagement.DataAccess.DailyReportDetails;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Core.Common;

namespace EmployeeManagement.Service.DailyReportDetails
{
    public class DailyReportService : IDailyReportService
    {
        //private properties
        private readonly IDailyReportDataAccess _idailyReportDataAccess;
        //constructor
        public DailyReportService(IDailyReportDataAccess idailyReportDataAccess)
        {
            _idailyReportDataAccess = idailyReportDataAccess;
        }
        /// <summary>
        /// implementation method of add reports
        /// </summary>
        /// <param name="reports"></param>
        public void AddDailyReports(DailyReport reports)
        {
            _idailyReportDataAccess.InsertBulkDailyReports(reports);
        }
        /// <summary>
        /// get all reports by date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<ReportDetails> GetAllReportDetailsByDate(string date,Filter filter)
        {
            return _idailyReportDataAccess.GetAllReportDetailsByDate(date,filter);
        }

        /// <summary>
        /// get daily task reort method implementation
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<ReportDetails> GetReportDetails(int staffId, string date, Filter filter)
        {
            return _idailyReportDataAccess.GetReportDetailsByStaffId(staffId,date,filter);
        }

        public List<ReportDetails> GetReportDetails(int staffId, string date)
        {
            return _idailyReportDataAccess.GetReportDetailsByStaffId(staffId, date);
        }
        /// <summary>
        ///  implementation of get data for chart view of daily report activity
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="staffId"></param>
        /// <param name="taskTypeId"></param>
        /// <returns></returns>
        public ChartData GetStaffDailyActivitySummaryToChart(ChartViewRequest chart)
        {
          //return result
             return _idailyReportDataAccess.GetStaffActivitySummary(chart);
        }

        /// <summary>
        /// Get all task type method implementation
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<TaskDetails> GetTaskTypes()
        {
            return _idailyReportDataAccess.GetTaskTypesDetails();
        }
        /// <summary>
        /// implementation of remove report
        /// </summary>
        /// <param name="reportId"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void RemoveDailyReportTask(int reportId)
        {
            _idailyReportDataAccess.DeleteDailyReportTask(reportId);
        }
    }
}
