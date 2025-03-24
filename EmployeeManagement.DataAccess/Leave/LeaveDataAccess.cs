using EmployeeManagement.Core.Leave;
using EmployeeManagement.Core.Staff;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using EmployeeManagement.Core.Common;
using System.Collections;

namespace EmployeeManagement.DataAccess.Leave
{
    public class LeaveDataAccess : ILeaveDataAccess
    {
        protected string EMPDBString;
        private IConfiguration _config { get; set; }
        //constructor
        public LeaveDataAccess(IConfiguration _config)
        {
            this._config = _config;
            EMPDBString = _config.GetConnectionString("EMPDBString");
        }
        //Get all staff leave details implementaion method
        public List<LeaveCalenderOptions> GetAllLeaveDetails()
        {
            //declare auth response
            List<LeaveCalenderOptions> details = new List<LeaveCalenderOptions>();
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("Leave_Get", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters

                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 40);
                        ActionParameter.Value = "GetAllLeave$Details";
                        SqlParameter YearParameter = sqlCommand.Parameters.Add("@Year", SqlDbType.VarChar, 40);
                        YearParameter.Value = DateTime.Today.Year;

                        // Executing the sql SP command
                        var resultToken = sqlCommand.ExecuteReader();

                        while (resultToken.Read())
                        {
                            //adding staff details into the list
                            details.Add(new LeaveCalenderOptions
                            {                   
                               
                                LeaveName = resultToken["LeaveType"].ToString(),                              
                                Title = resultToken["FirstName"].ToString(),
                                Start = DateTime.Parse(resultToken["StartDate"].ToString()),
                                End = resultToken["EndDate"].ToString() != string.Empty ?  DateTime.Parse(resultToken["EndDate"].ToString()):null,                             
                                Color = resultToken["Color"].ToString(),
                                IsHalfday = resultToken["IsHalfday"].ToString() != string.Empty ? Convert.ToBoolean(resultToken["IsHalfday"].ToString()) : false,
                                IsMorningHalfday = resultToken["IsMorningHalfday"].ToString() != string.Empty ? Convert.ToBoolean(resultToken["IsMorningHalfday"].ToString()) : false,
                                IsAfternoonHalfday = resultToken["IsAfternoonHalfday"].ToString() != string.Empty ? Convert.ToBoolean(resultToken["IsAfternoonHalfday"].ToString()) : false,
                                IsFullday = resultToken["IsFullday"].ToString() != string.Empty ? Convert.ToBoolean(resultToken["IsFullday"].ToString()) : false,
                                

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
        //Get all  leave type details implementaion method
        public List<LeaveType> GetAllLeaveTypes()
        {
            //declare auth response
            List<LeaveType> details = new List<LeaveType>();
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("Leave_Get", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters

                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 40);
                        ActionParameter.Value = "GetLeaveTypes$Details";

                        // Executing the sql SP command
                        var resultToken = sqlCommand.ExecuteReader();

                        while (resultToken.Read())
                        {
                            //adding staff details into the list
                            details.Add(new LeaveType
                            {
                                id = Convert.ToInt32(resultToken["Id"].ToString()),                             
                                Name = resultToken["LeaveType"].ToString(),
                                Days= Convert.ToInt32(resultToken["NumberOfDays"].ToString())
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
        //Get  leavebalance details implementaion method
        public List<LeaveBalance> GetLeaveBalancePerStaff(int staffId)
        {
            //declare auth response
            List<LeaveBalance> details = new List<LeaveBalance>();
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("Leave_Get", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters

                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 40);
                        ActionParameter.Value = "GetLeaveBal$Details";

                        SqlParameter StaffParameter = sqlCommand.Parameters.Add("@StaffId", SqlDbType.VarChar, 40);
                        StaffParameter.Value = staffId;

                        SqlParameter YearParameter = sqlCommand.Parameters.Add("@Year", SqlDbType.VarChar, 40);
                        YearParameter.Value = DateTime.Today.Year;

                        // Executing the sql SP command
                        var resultToken = sqlCommand.ExecuteReader();

                        while (resultToken.Read())
                        {
                            details.Add(new LeaveBalance
                            {
                                StaffId = staffId,
                                Year = DateTime.Today.Year,
                                NumberOfDays = Convert.ToDouble(resultToken["AvailableDays"].ToString()),
                                LeaveType = resultToken["LeaveType"].ToString()
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
        //insert leave details implementaion method
        public int InsertLeaveRquest(LeaveRequest request)
        {
            //delcare status as int
            int status = 0;
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();
                    //check leave half day or not
                    if (request.IsHalfday)
                    {
                        request.NumberOfDays = 0.5;
                        if (request.IsMorningHalfday)
                        {
                            request.StartDate = request.StartDate.Date.AddHours(8).AddMinutes(30);
                            request.EndDate = request.StartDate.Date.AddHours(12).AddMinutes(30);
                        }
                        else
                        {
                            request.StartDate = request.StartDate.Date.AddHours(13).AddMinutes(30);
                            request.EndDate = request.StartDate.Date.AddHours(17).AddMinutes(30);
                        }
                    }
                    else
                    {
                        //check if end date is null
                        if (request.EndDate == null || (request.StartDate==request.EndDate))
                        {
                            request.NumberOfDays = 1;
                            request.IsFullday = true;
                            request.EndDate = request.StartDate;
                        }
                        //for date range seletcion
                        else
                        {      
                            //steps to avoid weekend within date range
                            int totalDays = ((request.EndDate ?? DateTime.MinValue) - request.StartDate).Days + 1;
                            int weekendDays = Enumerable.Range(0, totalDays)
                                                        .Select(i => request.StartDate.AddDays(i))
                                                         .Count(date => date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);
                            int workingDays = totalDays - weekendDays;
                            //add 1 day with difference to include start date
                            request.NumberOfDays = workingDays;
                            request.EndDate = (request.EndDate ?? DateTime.MinValue).Date.AddDays(1).AddTicks(-1);
                        }
                      
                    }


                    // Check Token expired
                    using (SqlCommand sqlCommandToken = new SqlCommand("Leave_Set", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters
                        SqlParameter UserParameter = sqlCommandToken.Parameters.Add("@Action", SqlDbType.VarChar, 400);
                        UserParameter.Value = "UPD$Leave";

                        SqlParameter StaffParameter = sqlCommandToken.Parameters.Add("@StaffId", SqlDbType.Int);
                        StaffParameter.Value = request.StaffId;

                        SqlParameter LeaveParameter = sqlCommandToken.Parameters.Add("@LeaveTypeId", SqlDbType.Int);
                        LeaveParameter.Value = request.LeaveTypeId;

                        SqlParameter StartDateParameter = sqlCommandToken.Parameters.Add("@StartDate", SqlDbType.DateTime2);
                        StartDateParameter.Value = request.StartDate;

                        SqlParameter EndDateParameter = sqlCommandToken.Parameters.Add("@EndDate", SqlDbType.DateTime2);
                        EndDateParameter.Value = request.EndDate;

                        SqlParameter DaysParameter = sqlCommandToken.Parameters.Add("@Days", SqlDbType.Float);
                        DaysParameter.Value = request.NumberOfDays;

                        SqlParameter YearParameter = sqlCommandToken.Parameters.Add("@Year", SqlDbType.Int);
                        YearParameter.Value = DateTime.Today.Year;

                        SqlParameter HalfdayParameter = sqlCommandToken.Parameters.Add("@IsHalfday", SqlDbType.Bit);
                        HalfdayParameter.Value = request.IsHalfday;

                        SqlParameter FulldayParameter = sqlCommandToken.Parameters.Add("@IsFullday", SqlDbType.Bit);
                        FulldayParameter.Value = request.IsFullday;

                        SqlParameter MorningHalfdayParameter = sqlCommandToken.Parameters.Add("@IsMorningHalfday", SqlDbType.Bit);
                        MorningHalfdayParameter.Value =request.IsMorningHalfday;

                        SqlParameter EveningHalfdayParameter = sqlCommandToken.Parameters.Add("@IsAfternoonHalfday", SqlDbType.Bit);
                        EveningHalfdayParameter.Value =request.IsAfternoonHalfday;

                        SqlParameter DescriptionParameter = sqlCommandToken.Parameters.Add("@Description", SqlDbType.VarChar,500);
                        DescriptionParameter.Value = request.Description;

                        SqlParameter AdminCommentParameter = sqlCommandToken.Parameters.Add("@AdminComment", SqlDbType.VarChar, 500);
                        AdminCommentParameter.Value = request.AdminComment;

                        SqlParameter LeaveStatusIdParameter = sqlCommandToken.Parameters.Add("@LeaveStatusId", SqlDbType.Int);
                        LeaveStatusIdParameter.Value = request.LeaveStatusId;
                        // Executing the sql SP command
                        var resultToken = sqlCommandToken.ExecuteReader();
                        while (resultToken.Read())
                        {
                            //set created user type id
                            status = Convert.ToInt32(resultToken["Status"].ToString());
                        }
                    }

                    // Closing the connection
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AuthenticationDataAccess_InsertNewuserDetails ! :" + ex);
            }
            return status;
        }
        //get applied leave details
        public List<LeaveSummary> GetAppliedleaveDetails(int staffId)
        {
            //declare auth response
            List<LeaveSummary> details = new List<LeaveSummary>();
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("Leave_Get", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters

                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 40);
                        ActionParameter.Value = "GetAppliedLeave$Details";

                        SqlParameter StaffParameter = sqlCommand.Parameters.Add("@StaffId", SqlDbType.VarChar, 40);
                        StaffParameter.Value = staffId;

                        SqlParameter YearParameter = sqlCommand.Parameters.Add("@Year", SqlDbType.VarChar, 40);
                        YearParameter.Value = DateTime.Today.Year;

                        // Executing the sql SP command
                        var resultToken = sqlCommand.ExecuteReader();

                        while (resultToken.Read())
                        {
                            details.Add(new LeaveSummary
                            {                               
                                NumberOfDays = Convert.ToDouble(resultToken["TotalDays"].ToString()),
                                LeaveType = resultToken["LeaveType"].ToString()
                            });

                        }
                    }
                    var leaveTypes = GetAllLeaveTypes();
                    foreach (var leave in leaveTypes)
                    {
                        if (!details.Any(x => x.LeaveType == leave.Name))
                        {
                            details.Add(new LeaveSummary { LeaveType= leave.Name,NumberOfDays=0 });
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
        //get all staff leave details
        public List<LeaveDetails> GetStaffLeaveDetails(int staffId, int year, Filter filter)
        {
            //declare LeaveDetails
            List<LeaveDetails> details = new List<LeaveDetails>();
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("Leave_Get", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters

                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 40);
                        ActionParameter.Value = "GetStaffleave$Summary";
                        SqlParameter YearParameter = sqlCommand.Parameters.Add("@Year", SqlDbType.Int);
                        YearParameter.Value =year;

                        SqlParameter StaffParameter = sqlCommand.Parameters.Add("@StaffId", SqlDbType.Int);
                        StaffParameter.Value = staffId;

                        SqlParameter CurrentPageParameter = sqlCommand.Parameters.Add("@CurrentPage", SqlDbType.Int);
                        CurrentPageParameter.Value = filter.PageNumber;

                        SqlParameter RecordsPerPageParameter = sqlCommand.Parameters.Add("@RecordsPerPage", SqlDbType.Int);
                        RecordsPerPageParameter.Value =filter.RecordsperPage;

                        // Executing the sql SP command
                        var resultToken = sqlCommand.ExecuteReader();

                        while (resultToken.Read())
                        {
                            //adding staff leave details into the list
                            details.Add(new LeaveDetails
                            {
                                LeaveType = resultToken["LeaveType"].ToString(),
                                LeaveStatus = resultToken["LeaveStatus"].ToString(),
                                StartDate = DateTime.Parse(resultToken["StartDate"].ToString()),
                                EndDate = resultToken["EndDate"].ToString() != string.Empty ? DateTime.Parse(resultToken["EndDate"].ToString()) : null,
                                Description = resultToken["Description"].ToString(),
                                AdminComment = resultToken["AdminComment"].ToString(),
                                IsHalfday = resultToken["IsHalfday"].ToString() != string.Empty ? Convert.ToBoolean(resultToken["IsHalfday"].ToString()) : false,
                                IsMorningHalfday = resultToken["IsMorningHalfday"].ToString() != string.Empty ? Convert.ToBoolean(resultToken["IsMorningHalfday"].ToString()) : false,
                                IsAfternoonHalfday = resultToken["IsAfternoonHalfday"].ToString() != string.Empty ? Convert.ToBoolean(resultToken["IsAfternoonHalfday"].ToString()) : false,                               
                                TotalRecords =Convert.ToInt32(resultToken["TotalRecords"].ToString()),
                                NumberOfDays = Convert.ToDouble(resultToken["NumberOfDays"].ToString()),
                                Year = Convert.ToInt32(resultToken["Year"].ToString()),
                                IsFullday = resultToken["IsFullday"].ToString() != string.Empty ? Convert.ToBoolean(resultToken["IsFullday"].ToString()) : false
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
    }
}
