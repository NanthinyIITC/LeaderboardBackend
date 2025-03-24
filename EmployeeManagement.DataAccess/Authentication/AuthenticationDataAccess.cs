using EmployeeManagement.Core.Authentication;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Authentication
{
    public class AuthenticationDataAccess : IAuthenticationDataAccess
    {
        protected string EMPDBString;
        private IConfiguration _config { get; set; }
        //constructor
        public AuthenticationDataAccess(IConfiguration _config)
        {
                this._config=_config;
            EMPDBString = _config.GetConnectionString("EMPDBString");
        }
        /// <summary>
        /// Implementation to find existing staff details with email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public AuthResponse CheckStaffExistWithEmailAndPassword(string email, string password)
        {
            //declare auth response
           AuthResponse authResponse = new AuthResponse();
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
                        SqlParameter EmailParameter = sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar, 40);
                        EmailParameter.Value = email;
                        SqlParameter PasswordParameter = sqlCommand.Parameters.Add("@Password", SqlDbType.VarChar, 20);
                        PasswordParameter.Value = password;
                        // Adding stored procedure parameters
                        SqlParameter ActionParameter = sqlCommand.Parameters.Add("@Action", SqlDbType.VarChar, 40);
                        ActionParameter.Value = "GetExist$STAFF";

                        // Executing the sql SP command
                        var resultToken = sqlCommand.ExecuteReader();

                        while (resultToken.Read())
                        {
                            //get the status of user exist
                            bool status = Convert.ToBoolean(resultToken["Status"].ToString());
                            if(status)
                            {
                                //set values of staff to auth response
                                authResponse.Email = resultToken["Email"].ToString();
                                authResponse.UserName = resultToken["NickName"].ToString();
                                authResponse.UserType = resultToken["UserType"].ToString();
                                authResponse.IsAuthSuccess = true;
                                authResponse.StaffId = Convert.ToInt32(resultToken["StaffId"].ToString());
                            }
                            else
                            {
                                //set to false
                                authResponse.IsAuthSuccess = false;
                            }
                           
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
            return authResponse;
        }
    }
}
