using EmployeeManagement.Core.Authentication;
using EmployeeManagement.DataAccess.Authentication;
using EmployeeManagement.Service.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{

    [Produces("application/json")]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        //private properties
        private readonly IAuthenticationService _authenticationService;
        //constructor
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        // Check if the email exists with password then login will be success
        [HttpPost]
        [Route("login")]
        public IActionResult LoginWithEmailAndPassword([FromBody] AuthRequest request)
        {
            try
            {
                // Declare response
                var response = this._authenticationService.LoginWithEmailAndPassword(request.Email, request.Password);
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
