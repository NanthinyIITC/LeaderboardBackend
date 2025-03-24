using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.Staff;
using EmployeeManagement.DataAccess.Staff;
using EmployeeManagement.Service.Authentication;
using EmployeeManagement.Service.Staff;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/staff")]
    public class StaffController : ControllerBase
    {
        //private properties
        private readonly IStaffService _staffService;
        //constructor
        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        /// <summary>
        /// get all staff without filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-all-staff")]
        public IActionResult GetAllStaffDetails()
        {
            try
            {
                // Declare response
                var response = this._staffService.GetAllExistingStaffDetails();
                // Returning the result
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("get-all")]
        public IActionResult GetAllStaffDetails(Filter filter)
        {
            try
            {
                // Declare response
                var response = this._staffService.GetAllExistingStaffDetails(filter);
                // Returning the result
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
        // get all staff details by staffId
        [HttpGet]
        [Route("get-by-id")]
        public IActionResult GetAllStaffDetailsById([FromQuery] int staffId)
        {
            try
            {
                // Declare response
                var response = this._staffService.GetExistingStaffDetailUsingId(staffId);
                // Returning the result
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
        //create new staff
        [HttpPost]
        [Route("new-staff")]
        public IActionResult CreateNewStaff([FromBody] StaffDetails details)
        {
            try
            {
                // Declare response
                int response = this._staffService.RegisterNewStaff(details);
                if (response == 0)
                {
                    return new JsonResult(new { Success = false, Message = "Email already exist..." });
                }
                // Returning the result
                return new JsonResult(new { Success = true, Message = "Created succesfully" });
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult(new { Success = "", Message = ex.Message.ToString() });
            }
        }
        //update staff details
        [HttpPut]
        [Route("update-staff")]
        public IActionResult UpdateStaffDetails([FromBody] StaffDetails details, [FromQuery] int staffId)
        {
            try
            {
                // Declare response
                 this._staffService.UpdateStaffDetails(staffId,details);
                // Returning the result
                return new JsonResult(new{ Success=true,Message="Updated succesfully"});
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
        // remove staff details
        [HttpDelete]
        [Route("remove-staff")]
        public IActionResult RemoveStaffDetails([FromQuery] int staffId)
        {
            try
            {
                // Declare response
                this._staffService.RemoveStaffDetails(staffId);
                // Returning the result
                return new JsonResult(new { Success = true, Message = "Removed succesfully" });
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
        //get user types
        [HttpGet]
        [Route("usertypes")]
        public IActionResult GetUserTypes()
        {
            try
            {
                // Declare response
                var response = this._staffService.GetUserTypes();
                // Returning the result
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
        //get user types
        [HttpGet]
        [Route("usertype-actions")]
        public IActionResult GetUserTypes([FromQuery]string action,UserType type)
        {
            try
            {
                // Declare response
                var response = this._staffService.UserTypesActionsCUD(action,type);
                // Returning the result
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
    }
}
