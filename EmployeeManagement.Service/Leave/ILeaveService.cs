using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Service.Leave
{
    public interface ILeaveService
    {
        //record leave request
        int CreateLeaveRquest(LeaveRequest request);
        //get all leave details
        List<LeaveCalenderOptions> GetAllLeaveDetails();
        //get leave balance
        List<LeaveBalance> GetLeaveBalanceDetailsPerStaff(int staffId);
        //get allocated leave types
        List<LeaveType> GetAllLeaveTypesDetails();
        //get applied leave details
        List<LeaveSummary> GetAppliedLeaveDetailPerStaff(int staffId);
        //get all leave details
        List<LeaveDetails> GetStaffAllAppliedLeaveDetails(int staffId, int year, Filter filter);
    }
}
