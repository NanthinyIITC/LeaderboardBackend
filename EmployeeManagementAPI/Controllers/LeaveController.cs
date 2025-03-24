using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.Leave;
using EmployeeManagement.Service.Leave;
using EmployeeManagement.Service.Staff;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/leave")]
    public class LeaveController : ControllerBase
    {
        //private properties
        private readonly ILeaveService _leaveService;
        //constructor
        public LeaveController(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }
        // get all leave details
        [HttpGet]
        [Route("get-all")]
        public IActionResult GetAllLeaveDetails()
        {
            try
            {
                // Declare response
                var response = this._leaveService.GetAllLeaveDetails();
                // Returning the result
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
        // get all leave details
        [HttpGet]
        [Route("leavebalance")]
        public IActionResult GetAllLeaveBalanceDetails([FromQuery] int staffId)
        {
            try
            {
                // Declare response
                var response = this._leaveService.GetLeaveBalanceDetailsPerStaff(staffId);
                // Returning the result
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
        // get all leave details
        [HttpGet]
        [Route("appliedleaves")]
        public IActionResult GetAppliedLeaveDetails([FromQuery] int staffId)
        {
            try
            {
                // Declare response
                var response = this._leaveService.GetAppliedLeaveDetailPerStaff(staffId);
                // Returning the result
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
        // get all leave details of staff
        [HttpGet]
        [Route("staff-leaves")]
        public IActionResult GetAppliedLeaveDetails([FromQuery] int staffId, int year,Filter filter)
        {
            try
            {
                // Declare response
                var response = this._leaveService.GetStaffAllAppliedLeaveDetails(staffId,year,filter);
                // Returning the result
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
        // get all leave details
        [HttpGet]
        [Route("leave-types")]
        public IActionResult GetAllLeavetypesDetails()
        {
            try
            {
                // Declare response
                var response = this._leaveService.GetAllLeaveTypesDetails();
                // Returning the result
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
        // get all leave details
        [HttpPost]
        [Route("leave-request")]
        public IActionResult CreateLeaveRequest([FromBody] LeaveRequest request)
        {
            try
            {
                // Declare response
              int status= this._leaveService.CreateLeaveRquest(request);
                // Returning the result
                if (status == 0)
                {
                    return new JsonResult(new { Success = false, Message = "Selected leave date already applied..." });
                }
                else
                {
                    return new JsonResult(new { Success = true, Message = "Leave Updated..." });
                }
               
            }
            catch (Exception ex)
            {
                // Returning the exception
                return new JsonResult("System Failed: " + ex.Message.ToString());
            }
        }
    }
}
