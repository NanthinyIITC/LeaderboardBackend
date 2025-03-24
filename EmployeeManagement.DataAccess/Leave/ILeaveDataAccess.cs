using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Leave
{
    public interface ILeaveDataAccess
    {
        //record leave request
        int InsertLeaveRquest(LeaveRequest request);
        //get all leave details
        List<LeaveCalenderOptions> GetAllLeaveDetails();
        //get all leave details
        List<LeaveDetails> GetStaffLeaveDetails(int staffId,int year,Filter filter);
        //get leave balance
        List<LeaveBalance> GetLeaveBalancePerStaff(int staffId);
        //get allocated leave types
        List<LeaveType> GetAllLeaveTypes();
        //get applied leave details
        List<LeaveSummary> GetAppliedleaveDetails(int staffId);
    }
}
