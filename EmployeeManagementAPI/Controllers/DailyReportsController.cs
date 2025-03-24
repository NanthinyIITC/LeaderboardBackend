using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.DailyReportDetails;
using EmployeeManagement.Service.DailyReportDetails;
using EmployeeManagement.Service.Staff;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/daily-reports")]
    public class DailyReportsController : ControllerBase
    {
        //private properties
        private readonly IDailyReportService _dailyReportService;
        //constructor
        public DailyReportsController(IDailyReportService dailyReportService)
        {
            _dailyReportService = dailyReportService;
        }
        //add reports
        [HttpPost]
        [Route("add-reports")]
        public IActionResult AddReports([FromBody] DailyReport report)
        {
            try
            {
                // Declare response
               this._dailyReportService.AddDailyReports(report);
                // Returning the result
                return new JsonResult(new { Success=true,Message="Added successfully.."});
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
        //get all task types
        [HttpGet]
        [Route("tasks")]
        public IActionResult GetAllTask()
        {
            try
            {
                // Declare response
               var res= this._dailyReportService.GetTaskTypes();
                // Returning the result
                return new JsonResult(res);
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
        //get daily reports per staff
        [HttpGet]
        [Route("reports")]
        public IActionResult GetReportsByStaff([FromQuery] int staffId, [FromQuery] string date, Filter filter)
        {
            try
            {
                // Declare response
               var res= this._dailyReportService.GetReportDetails(staffId, date, filter);
                // Returning the result
                return new JsonResult(res);
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
        /// <summary>
        /// get daily report without pagination
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="date"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("reports-by-date")]
        public IActionResult GetReportsByStaff([FromQuery] int staffId, [FromQuery] string date)
        {
            try
            {
                // Declare response
                var res = this._dailyReportService.GetReportDetails(staffId, date);
                // Returning the result
                return new JsonResult(res);
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
        //get daily reports per staff
        [HttpGet]
        [Route("filter-by-date")]
        public IActionResult GetReportsByStaff([FromQuery] string date,Filter filter)
        {
            try
            {
                // Declare response
                var res = this._dailyReportService.GetAllReportDetailsByDate(date,filter);
                // Returning the result
                return new JsonResult(res);
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
        // remove report details
        [HttpDelete]
        [Route("remove-report")]
        public IActionResult RemoveReport([FromQuery] int id)
        {
            try
            {
                // Declare response
                this._dailyReportService.RemoveDailyReportTask(id);
                // Returning the result
                return new JsonResult(new { Success = true, Message = "Removed succesfully" });
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
        //get daily reports summary to chart view
        [HttpPost]
        [Route("chart-view")]
        public IActionResult GetDataToChart([FromBody]  ChartViewRequest chart)
        {
            try
            {
                // Declare response
                  var res = this._dailyReportService.GetStaffDailyActivitySummaryToChart(chart);
                // Returning the result
                return new JsonResult(res);               
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
    }
}
