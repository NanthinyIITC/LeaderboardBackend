using EmployeeManagement.Core.Authentication;
using EmployeeManagement.Core.Staff;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Core.Common;

namespace EmployeeManagement.DataAccess.Staff
{
    public class StaffDataAccess : IStaffDataAccess
    {
        protected string EMPDBString;
        private IConfiguration _config { get; set; }
        //constructor
        public StaffDataAccess(IConfiguration _config)
        {
            this._config = _config;
            EMPDBString = _config.GetConnectionString("EMPDBString");
        }

        public List<StaffDetails> GetAllStaffDetails(Filter filter)
        {
            //declare auth response
            List<StaffDetails> details = new List<StaffDetails>();
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("Staff_Get", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters

                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 40);
                        ActionParameter.Value = "GetAll$STAFF";
                        SqlParameter CurrentPageParameter = sqlCommand.Parameters.Add("@CurrentPage", SqlDbType.Int);
                        CurrentPageParameter.Value = filter.PageNumber;
                        SqlParameter RecordsperPageParameter = sqlCommand.Parameters.Add("@RecordsPerPage", SqlDbType.Int);
                        RecordsperPageParameter.Value = filter.RecordsperPage;
                        // Executing the sql SP command
                        var resultToken = sqlCommand.ExecuteReader();

                        while (resultToken.Read())
                        {
                            //adding staff details into the list
                            details.Add(new StaffDetails
                            {
                                FirstName = resultToken["FirstName"].ToString(),
                                LastName = resultToken["LastName"].ToString(),
                                Email = resultToken["Email"].ToString(),
                                Id = Convert.ToInt32(resultToken["StaffId"].ToString()),
                                NickName = resultToken["NickName"].ToString(),
                                ContactNumber = resultToken["ContactNumber"].ToString(),
                                Designation = resultToken["Designation"].ToString(),
                                UserType = resultToken["UserType"].ToString(),
                                UserTypeId= Convert.ToInt32(resultToken["UserTypeId"].ToString()),
                                Password= resultToken["Password"].ToString(),
                                TotalRecords= Convert.ToInt32(resultToken["TotalRecords"].ToString())
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
        /// insert new staff details
        /// </summary>
        /// <param name="newStaff"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int InsertNewStaff(StaffDetails newStaff)
        {
            // Declare the token
            int newId = 0;

            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    // Check Token expired
                    using (SqlCommand sqlCommandToken = new SqlCommand("Staff_Set", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters
                        SqlParameter UserParameter = sqlCommandToken.Parameters.Add("@Action", SqlDbType.VarChar, 40);
                        UserParameter.Value = "NEW$STAFF";

                        SqlParameter EmailParameter = sqlCommandToken.Parameters.Add("@Email", SqlDbType.VarChar);
                        EmailParameter.Value = newStaff.Email;

                        SqlParameter PasswordParameter = sqlCommandToken.Parameters.Add("@Password", SqlDbType.VarChar);
                        PasswordParameter.Value = newStaff.Password;

                        SqlParameter NickNameParameter = sqlCommandToken.Parameters.Add("@NickName", SqlDbType.VarChar);
                        NickNameParameter.Value = newStaff.NickName;

                        SqlParameter FirstNameParameter = sqlCommandToken.Parameters.Add("@FirstName", SqlDbType.VarChar);
                        FirstNameParameter.Value = newStaff.FirstName;

                        SqlParameter LastNameParameter = sqlCommandToken.Parameters.Add("@LastName", SqlDbType.VarChar);
                        LastNameParameter.Value = newStaff.LastName;

                        SqlParameter ContactNumberParameter = sqlCommandToken.Parameters.Add("@ContactNumber", SqlDbType.VarChar);
                        ContactNumberParameter.Value = newStaff.ContactNumber;

                        SqlParameter DesignationParameter = sqlCommandToken.Parameters.Add("@Designation", SqlDbType.VarChar);
                        DesignationParameter.Value = newStaff.Designation;

                        SqlParameter UserTypeIdParameter = sqlCommandToken.Parameters.Add("@UserTypeId", SqlDbType.Int);
                        UserTypeIdParameter.Value = newStaff.UserTypeId;

                        SqlParameter CreatedDateParameter = sqlCommandToken.Parameters.Add("@CreatedDate", SqlDbType.DateTime);
                        CreatedDateParameter.Value =DateTime.Now;

                        // Executing the sql SP command
                        var resultToken = sqlCommandToken.ExecuteReader();

                        while (resultToken.Read())
                        {
                            //set new staff id
                            newId = Convert.ToInt32(resultToken["StaffId"].ToString());
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
            return newId;
        }
        /// <summary>
        /// remove visibility of staff 
        /// </summary>
        /// <param name="staffId"></param>
        /// <exception cref="Exception"></exception>
        public void RemoveStaffDetails(int staffId)
        {
          
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("Staff_Set", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters
                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 40);
                        ActionParameter.Value = "RMV$STAFF";
                        SqlParameter StaffIdParameter = sqlCommand.Parameters.Add("@StaffId", SqlDbType.VarChar, 40);
                        StaffIdParameter.Value = staffId;
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
        /// Update staff details
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="details"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateStaffDetails(int staffId, StaffDetails details)
        {
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    // Check Token expired
                    using (SqlCommand sqlCommandToken = new SqlCommand("Staff_Set", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters
                        SqlParameter UserParameter = sqlCommandToken.Parameters.Add("@Action", SqlDbType.VarChar, 40);
                        UserParameter.Value = "UPDTE$STAFF";

                        SqlParameter EmailParameter = sqlCommandToken.Parameters.Add("@Email", SqlDbType.VarChar);
                        EmailParameter.Value = details.Email;

                        SqlParameter PasswordParameter = sqlCommandToken.Parameters.Add("@Password", SqlDbType.VarChar);
                        PasswordParameter.Value = details.Password;

                        SqlParameter NickNameParameter = sqlCommandToken.Parameters.Add("@NickName", SqlDbType.VarChar);
                        NickNameParameter.Value = details.NickName;

                        SqlParameter FirstNameParameter = sqlCommandToken.Parameters.Add("@FirstName", SqlDbType.VarChar);
                        FirstNameParameter.Value = details.FirstName;

                        SqlParameter LastNameParameter = sqlCommandToken.Parameters.Add("@LastName", SqlDbType.VarChar);
                        LastNameParameter.Value = details.LastName;

                        SqlParameter ContactNumberParameter = sqlCommandToken.Parameters.Add("@ContactNumber", SqlDbType.VarChar);
                        ContactNumberParameter.Value = details.ContactNumber;

                        SqlParameter DesignationParameter = sqlCommandToken.Parameters.Add("@Designation", SqlDbType.VarChar);
                        DesignationParameter.Value = details.Designation;

                        SqlParameter UserTypeIdParameter = sqlCommandToken.Parameters.Add("@UserTypeId", SqlDbType.VarChar);
                        UserTypeIdParameter.Value = details.UserTypeId;

                        SqlParameter modifiedDateParameter = sqlCommandToken.Parameters.Add("@ModifiedDate", SqlDbType.VarChar);
                        modifiedDateParameter.Value = DateTime.Now;

                        SqlParameter staffIdParameter = sqlCommandToken.Parameters.Add("@StaffId", SqlDbType.VarChar);
                        staffIdParameter.Value = details.Id;
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
        /// <summary>
        /// implementation method to get staff details by id
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public StaffDetails GetStaffDetailsById(int staffId)
        {
            //declare auth response
            StaffDetails details = new StaffDetails();
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("Staff_Get", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters
                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 40);
                        ActionParameter.Value = "GetById$STAFF";
                        SqlParameter StaffIdParameter = sqlCommand.Parameters.Add("@StaffId", SqlDbType.VarChar, 40);
                        StaffIdParameter.Value = staffId;
                        // Executing the sql SP command
                        var resultToken = sqlCommand.ExecuteReader();

                        while (resultToken.Read())
                        {
                            //set values to staff details object
                            details.FirstName = resultToken["FirstName"].ToString();
                            details.LastName = resultToken["LastName"].ToString();
                            details.Email = resultToken["Email"].ToString();
                            details.Id = Convert.ToInt32(resultToken["StaffId"].ToString());
                            details.NickName = resultToken["NickName"].ToString();
                            details.ContactNumber = resultToken["ContactNumber"].ToString();
                            details.Designation = resultToken["Designation"].ToString();
                            details.UserType = resultToken["UserType"].ToString();
                            details.UserTypeId = (int)resultToken["UserTypeId"];
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

        public List<UserType> GetAllUserTypes()
        {
            //declare auth response
            List<UserType> details = new List<UserType>();
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("UserType_Get", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters

                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 40);
                        ActionParameter.Value = "GetAll$UserType";

                        // Executing the sql SP command
                        var resultToken = sqlCommand.ExecuteReader();

                        while (resultToken.Read())
                        {
                            //adding staff details into the list
                            details.Add(new UserType
                            {
                                Id = Convert.ToInt32(resultToken["Id"].ToString()),
                                Name = resultToken["UserType"].ToString(),
                                IsEditable=Convert.ToBoolean(resultToken["IsEditable"].ToString())
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
        /// get all staff without filter
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<StaffDetails> GetAllStaffDetails()
        {
            //declare auth response
            List<StaffDetails> details = new List<StaffDetails>();
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("Staff_Get", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters

                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 40);
                        ActionParameter.Value = "GetAllWOPagination@Staff";

                        // Executing the sql SP command
                        var resultToken = sqlCommand.ExecuteReader();

                        while (resultToken.Read())
                        {
                            //adding staff details into the list
                            details.Add(new StaffDetails
                            {
                                FirstName = resultToken["FirstName"].ToString(),
                                LastName = resultToken["LastName"].ToString(),
                                Email = resultToken["Email"].ToString(),
                                Id = Convert.ToInt32(resultToken["StaffId"].ToString()),
                                NickName = resultToken["NickName"].ToString(),
                                ContactNumber = resultToken["ContactNumber"].ToString(),
                                Designation = resultToken["Designation"].ToString(),
                                UserType = resultToken["UserType"].ToString(),
                                UserTypeId = Convert.ToInt32(resultToken["UserTypeId"].ToString()),
                                Password = resultToken["Password"].ToString()
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
        /// implementation to create,update or delete user type actions
        /// </summary>
        /// <param name="action"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ResponseMessage UserTypesActions(string action, UserType type)
        {
            //handle various actions
            switch (action)
            {
                case "add":
                      int id=InsertUserType(type);
                    if (id > 0)
                    {
                        return new ResponseMessage { Success = true, Message = "User type created...." };
                    }
                    return new ResponseMessage { Success = false, Message = "User type already exist..." };
                   
                case "update":
                    id = UpdateUserType(type);
                    if (id > 0)
                    {
                        return new ResponseMessage { Success = true, Message = "User type updated...." };
                    }
                    return new ResponseMessage { Success = false, Message = "User type already exist...." };
                case "remove":
                    RemoveUserType(type);
                    return new ResponseMessage { Success = true, Message = "User type removed...." };
                default:
                    return new ResponseMessage { Success = false, Message = string.Empty };
            }
        }
        private int InsertUserType(UserType type)
        {
            //initialize id
            int id = 0;
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("UserType_Set", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters

                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 500);
                        ActionParameter.Value = "INSERT$UserType";

                        SqlParameter UserTypeParameter = sqlCommand.Parameters.Add("@Name", SqlDbType.VarChar, 500);
                        UserTypeParameter.Value = type.Name;
                        // Executing the sql SP command
                        var resultToken = sqlCommand.ExecuteReader();

                        while (resultToken.Read())
                        {
                            //set created user type id
                            id = Convert.ToInt32(resultToken["NewId"].ToString());
                        }
                    }

                    // Closing the connection
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Usertype create actions :" + ex);
            }
            //return auth response
            return id;
        }
        //update user type
        private int UpdateUserType(UserType type)
        {
            //declare id
            int id = 0;
            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("UserType_Set", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters

                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 500);
                        ActionParameter.Value = "UPD$UserType";

                        SqlParameter UserTypeParameter = sqlCommand.Parameters.Add("@Name", SqlDbType.VarChar, 500);
                        UserTypeParameter.Value = type.Name;

                        SqlParameter UserTypeIdParameter = sqlCommand.Parameters.Add("@UserTypeId", SqlDbType.Int);
                        UserTypeIdParameter.Value = type.Id;
                        // Executing the sql SP command
                        var resultToken = sqlCommand.ExecuteReader();

                        while (resultToken.Read())
                        {
                            //set created user type id
                            id = Convert.ToInt32(resultToken["Status"].ToString());
                        }
                    }

                    // Closing the connection
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Usertype update actions :" + ex);
            }
            return id;
        }
        //delete user type
        private void RemoveUserType(UserType type)
        {

            try
            {
                //Setting the SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(this.EMPDBString))
                {
                    // Openning the connection
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("UserType_Set", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        // Adding stored procedure parameters

                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 500);
                        ActionParameter.Value = "RMV$UserType";
                       
                        SqlParameter UserTypeIdParameter = sqlCommand.Parameters.Add("@UserTypeId", SqlDbType.Int);
                        UserTypeIdParameter.Value = type.Id;
                        // Executing the sql SP command
                        sqlCommand.ExecuteReader();
                    }

                    // Closing the connection
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Usertype delete actions :" + ex);
            }

        }
    }
}
