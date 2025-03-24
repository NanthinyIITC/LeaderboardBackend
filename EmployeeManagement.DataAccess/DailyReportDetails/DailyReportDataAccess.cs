using EmployeeManagement.Core.DailyReportDetails;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Core.Staff;
using EmployeeManagement.Core.Common;
using System.Globalization;

namespace EmployeeManagement.DataAccess.DailyReportDetails
{
    public class DailyReportDataAccess : IDailyReportDataAccess
    {
        protected string EMPDBString;
        private IConfiguration _config { get; set; }
        //constructor
        public DailyReportDataAccess(IConfiguration _config)
        {
            this._config = _config;
            EMPDBString = _config.GetConnectionString("EMPDBString");
        }
        /// <summary>
        /// Implementation of bulk insert
        /// </summary>
        /// <param name="reports"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void InsertBulkDailyReports(DailyReport reports)
        {
            

            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    // Check Token expired
                    using (SqlCommand sqlCommandToken = new SqlCommand("DailyReport_Set", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters
                        SqlParameter UserParameter = sqlCommandToken.Parameters.Add("@Action", SqlDbType.VarChar, 50);
                        UserParameter.Value = "INERTBULK$Reports";
                       
                        SqlParameter StaffIdParameter = sqlCommandToken.Parameters.Add("@StaffId", SqlDbType.Int);
                        StaffIdParameter.Value = reports.StaffId;
                        string dateFormat = "yyyy-dd-MM";  // Adjust this if needed
                        DateTime parsedDate;
                      
                        // Try to parse the date string
                        bool isDateValid = DateTime.TryParseExact(DateTime.Today.ToString("yyyy-dd-MM"), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
                        SqlParameter DateParameter = sqlCommandToken.Parameters.Add("@Date", SqlDbType.DateTime);
                        DateParameter.Value = parsedDate;

                        //set datatable to pass list of reports
                        DataTable dataTable = new DataTable();
                        dataTable.Columns.Add("FK_StaffId", typeof(int));
                        dataTable.Columns.Add("FK_TaskTypeId", typeof(int));
                        dataTable.Columns.Add("StartTime");
                        dataTable.Columns.Add("EndTime");
                        dataTable.Columns.Add("Comments");
                        dataTable.Columns.Add("CompletedPercentage");
                        dataTable.Columns.Add("Duration");
                        dataTable.Columns.Add("CreatedDate", typeof(DateTime));                        
                       
                        //add loop to add row to table
                        foreach (ReportDetails dR in reports.Reports)
                        {
                            dataTable.Rows.Add(reports.StaffId,dR.TaskId,dR.StartTime,dR.EndTime,dR.Comments,dR.Percentage,dR.Duration, DateTime.Today);
                        }
                        //set tvp value
                        SqlParameter tableParameter = sqlCommandToken.Parameters.Add("@DailyReports", SqlDbType.Structured);
                        tableParameter.Value = dataTable;
                        // Executing the sql SP command
                       sqlCommandToken.ExecuteReader();
                       
                    }

                    // Closing the connection
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AuthenticationDataAccess_InsertNewuserDetails ! :" + ex);
            }
            
        }
        //get reports
        public List<ReportDetails> GetReportDetailsByStaffId(int staffId, string date, Filter filter)
        {
            //declare auth response
            List<ReportDetails> details = new List<ReportDetails>();
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("DailyReport_Get", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters

                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 100);
                        ActionParameter.Value = "GetAllStaff$Reports";
                        SqlParameter StaffIdParameter = sqlCommand.Parameters.Add("@StaffId", SqlDbType.Int);
                        StaffIdParameter.Value = staffId;
                        string dateFormat = "yyyy-MM-dd";  // Adjust this if needed
                        DateTime parsedDate;

                        // Try to parse the date string
                        bool isDateValid = DateTime.TryParseExact(date, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
                        SqlParameter dateParameter = sqlCommand.Parameters.Add("@SelectedDate", SqlDbType.DateTime);
                        dateParameter.Value = parsedDate.ToString("yyyy-MM-dd");
                        SqlParameter CurrentPageParameter = sqlCommand.Parameters.Add("@CurrentPage", SqlDbType.Int);
                        CurrentPageParameter.Value = filter.PageNumber;
                        SqlParameter RecordsperPageParameter = sqlCommand.Parameters.Add("@RecordsPerPage", SqlDbType.Int);
                        RecordsperPageParameter.Value = filter.RecordsperPage;
                        // Executing the sql SP command
                        var resultToken = sqlCommand.ExecuteReader();

                        while (resultToken.Read())
                        {
                            //get staff details into the list
                            details.Add(new ReportDetails
                            {
                                Id = Convert.ToInt32(resultToken["Id"].ToString()),
                                Task = resultToken["TaskType"].ToString(),                               
                                StartTime = resultToken["StartTime"].ToString(),
                                EndTime = resultToken["EndTime"].ToString(),
                                Comments = resultToken["Comments"].ToString(),
                                Percentage = Convert.ToInt32(resultToken["Percentage"].ToString()),                               
                                Duration = Convert.ToDouble(resultToken["Duration"].ToString()),
                                CreatedDate = DateTime.Parse(resultToken["CreatedDate"].ToString()),
                                TotalRecords = Convert.ToInt32(resultToken["TotalRecords"].ToString())
                            });

                        }
                    }

                    // Closing the connection
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AuthenticationDataAccess_CheckEmailExists ! :" + ex);
            }
            //return auth response
            return details;
        }
        //get task types
        public List<TaskDetails> GetTaskTypesDetails()
        {
            //declare auth response
            List<TaskDetails> details = new List<TaskDetails>();
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("TaskType_Get", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters

                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 40);
                        ActionParameter.Value = "GetAll$TaskType";

                        // Executing the sql SP command
                        var resultToken = sqlCommand.ExecuteReader();

                        while (resultToken.Read())
                        {
                            //adding staff details into the list
                            details.Add(new TaskDetails
                            {
                                Id = Convert.ToInt32(resultToken["Id"].ToString()),
                                Name = resultToken["TaskType"].ToString()                            

                            });

                        }
                    }

                    // Closing the connection
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AuthenticationDataAccess_CheckEmailExists ! :" + ex);
            }
            //return auth response
            return details;
        }
        /// <summary>
        /// Implemntation to get all reports for the date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<ReportDetails> GetAllReportDetailsByDate(string date, Filter filter)
        {
            //declare auth response
            List<ReportDetails> details = new List<ReportDetails>();
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("DailyReport_Get", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters

                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 40);
                        ActionParameter.Value = "GetAll$Reports";
                        string dateFormat = "yyyy-MM-dd";  // Adjust this if needed
                        DateTime parsedDate;

                        // Try to parse the date string
                        bool isDateValid = DateTime.TryParseExact(date, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
                        SqlParameter dateParameter = sqlCommand.Parameters.Add("@SelectedDate", SqlDbType.DateTime);
                        dateParameter.Value = parsedDate.ToString("yyyy-MM-dd");
                        SqlParameter CurrentPageParameter = sqlCommand.Parameters.Add("@CurrentPage", SqlDbType.Int);
                        CurrentPageParameter.Value = filter.PageNumber;
                        SqlParameter RecordsperPageParameter = sqlCommand.Parameters.Add("@RecordsPerPage", SqlDbType.Int);
                        RecordsperPageParameter.Value = filter.RecordsperPage;
                        // Executing the sql SP command
                        var resultToken = sqlCommand.ExecuteReader();

                        while (resultToken.Read())
                        {
                            //adding staff details into the list
                            details.Add(new ReportDetails
                            {
                                Id = Convert.ToInt32(resultToken["Id"].ToString()),
                                Task = resultToken["TaskType"].ToString(),
                                StartTime = resultToken["StartTime"].ToString(),
                                EndTime = resultToken["EndTime"].ToString(),
                                Comments = resultToken["Comments"].ToString(),
                                Percentage = Convert.ToInt32(resultToken["Percentage"].ToString()),
                                Duration = Convert.ToDouble(resultToken["Duration"].ToString()),
                                CreatedDate = DateTime.Parse(resultToken["CreatedDate"].ToString()),
                                TotalRecords = Convert.ToInt32(resultToken["TotalRecords"].ToString()),
                            });

                        }
                    }

                    // Closing the connection
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AuthenticationDataAccess_CheckEmailExists ! :" + ex);
            }
            //return auth response
            return details;
        }
        /// <summary>
        /// delete report implementation
        /// </summary>
        /// <param name="reportId"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteDailyReportTask(int reportId)
        {
           
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("DailyReport_Set", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters

                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 40);
                        ActionParameter.Value = "RMV$Reports";
                        SqlParameter idParameter = sqlCommand.Parameters.Add("@ReportId", SqlDbType.Int);
                        idParameter.Value = reportId;
                      
                        // Executing the sql SP command
                       sqlCommand.ExecuteReader();                        
                    }
                    // Closing the connection
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AuthenticationDataAccess_CheckEmailExists ! :" + ex);
            }          
        }
        /// <summary>
        /// implemetation to staff report without pagination
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<ReportDetails> GetReportDetailsByStaffId(int staffId, string date)
        {
            //declare auth response
            List<ReportDetails> details = new List<ReportDetails>();
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("DailyReport_Get", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters

                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 100);
                        ActionParameter.Value = "GetAllStaffWOPagination$Reports";
                        SqlParameter StaffIdParameter = sqlCommand.Parameters.Add("@StaffId", SqlDbType.Int);
                        StaffIdParameter.Value = staffId;
                        string dateFormat = "yyyy-dd-MM";  // Adjust this if needed
                        DateTime parsedDate;

                        // Try to parse the date string
                        bool isDateValid = DateTime.TryParseExact(date, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
                        SqlParameter dateParameter = sqlCommand.Parameters.Add("@SelectedDate", SqlDbType.DateTime);
                        dateParameter.Value = parsedDate;
                        
                        // Executing the sql SP command
                        var resultToken = sqlCommand.ExecuteReader();

                        while (resultToken.Read())
                        {
                            //get staff details into the list
                            details.Add(new ReportDetails
                            {
                                Id = Convert.ToInt32(resultToken["Id"].ToString()),
                                Task = resultToken["TaskType"].ToString(),
                                StartTime = resultToken["StartTime"].ToString(),
                                EndTime = resultToken["EndTime"].ToString(),
                                Comments = resultToken["Comments"].ToString(),
                                Percentage = Convert.ToInt32(resultToken["Percentage"].ToString()),
                                Duration = Convert.ToDouble(resultToken["Duration"].ToString()),
                                CreatedDate = DateTime.Parse(resultToken["CreatedDate"].ToString()),
                                TaskId= Convert.ToInt32(resultToken["TaskId"].ToString())
                            });

                        }
                    }

                    // Closing the connection
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AuthenticationDataAccess_CheckEmailExists ! :" + ex);
            }
            //return auth response
            return details;
        }
        /// <summary>
        /// implementation of staff activity summary
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="staffId"></param>
        /// <param name="taskTypeId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ChartData GetStaffActivitySummary(ChartViewRequest chart)
        {
            //declare  chart data
            ChartData details = new ChartData();
            List<DailyReportChart> reportDetails = new List<DailyReportChart>();
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();
                    //daily report get
                    using (SqlCommand sqlCommand = new SqlCommand("DailyReport_Get", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters

                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 100);
                        ActionParameter.Value = "GetChartSummary$Activity";
                        SqlParameter StaffIdParameter = sqlCommand.Parameters.Add("@StaffId", SqlDbType.Int);
                        StaffIdParameter.Value = chart.StaffId;

                        SqlParameter TaskIdParameter = sqlCommand.Parameters.Add("@TaskId", SqlDbType.Int);
                        TaskIdParameter.Value = chart.TaskId;

                        SqlParameter StartDateParameter = sqlCommand.Parameters.Add("@StartDate", SqlDbType.DateTime);
                        StartDateParameter.Value = chart.Start;

                        SqlParameter EndDateParameter = sqlCommand.Parameters.Add("@EndDate", SqlDbType.DateTime);
                        EndDateParameter.Value = chart.End;
                      

                        // Executing the sql SP command
                        var resultToken = sqlCommand.ExecuteReader();

                        while (resultToken.Read())
                        {
                            //get staff details into the list
                            reportDetails.Add(new DailyReportChart
                            {                              
                                Label = resultToken["TaskType"].ToString(),                               
                                Data = Convert.ToDouble(resultToken["TotalDuration"].ToString())                               
                            });
                        }
                        //get and set total working days time in mins
                        var timeCalculation = GetTotalWorkingTime(chart.Start, chart.End, chart.StaffId);
                        details.TotalWorkingTimeInMins = timeCalculation.Item1;
                        details.DailyReportChartData = reportDetails;
                        foreach(var report in details.DailyReportChartData) {
                            report.Data = Math.Round((report.Data / details.TotalWorkingTimeInMins) * 100,2);
                        }
                        details.TotalWorkingDays = timeCalculation.Item2;
                        details.TotalActualWorkingDays = timeCalculation.Item3;
                        details.TotalLeaveDays = timeCalculation.Item4;
                    }

                    // Closing the connection
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AuthenticationDataAccess_CheckEmailExists ! :" + ex);
            }
            //return auth response
            return details;
        }
        //get total working days time except weekend and leave
        private Tuple<double,double,double,double> GetTotalWorkingTime(string start, string end, int staffId)
        {
            //declare variables
            double totalLeaveDays = 0;
            double totalWorkedTime= 0;
            double totalWorkingDays = 0;
            double totalActualWorkingDays = 0;
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();
                    //daily report get
                    using (SqlCommand sqlCommand = new SqlCommand("Leave_Get", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters

                       
                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 100);
                        ActionParameter.Value = "GetTotalLeavedays$Details";
                        SqlParameter StaffIdParameter = sqlCommand.Parameters.Add("@StaffId", SqlDbType.Int);
                        StaffIdParameter.Value = staffId;
                       

                        SqlParameter StartDateParameter = sqlCommand.Parameters.Add("@StartDate", SqlDbType.DateTime);
                        StartDateParameter.Value = start;

                        SqlParameter EndDateParameter = sqlCommand.Parameters.Add("@EndDate", SqlDbType.DateTime);
                        EndDateParameter.Value = end;


                        // Executing the sql SP command
                        var resultToken = sqlCommand.ExecuteReader();

                        while (resultToken.Read())
                        {
                            //set value
                            totalLeaveDays = Convert.ToDouble(resultToken["Totalleavedays"].ToString());
                        }
                        var startDate = DateTime.Parse(start);
                        var endDate = DateTime.Parse(end);
                        //steps to avoid weekend within date range 
                        int totalDays = (endDate - startDate).Days + 1;
                        int weekendDays = Enumerable.Range(0, totalDays)
                                                    .Select(i => startDate.AddDays(i))
                                                     .Count(date => date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);
                        totalWorkingDays = totalDays - weekendDays;
                        totalActualWorkingDays = (double)totalWorkingDays - totalLeaveDays;
                        //get final working time
                        totalWorkedTime = totalActualWorkingDays * 8 * 60;
                    }


                    // Closing the connection
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AuthenticationDataAccess_CheckEmailExists ! :" + ex);
            }
            //return auth response
            return Tuple.Create(totalWorkedTime,totalWorkingDays,totalActualWorkingDays,totalLeaveDays);
        }
    }
}
