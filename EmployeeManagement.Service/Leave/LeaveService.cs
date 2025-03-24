using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.Leave;
using EmployeeManagement.DataAccess.Leave;
using EmployeeManagement.DataAccess.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Service.Leave
{
    public class LeaveService : ILeaveService
    {
        //private properties
        private readonly ILeaveDataAccess _ileaveDataAccess;
        //constructor
        public LeaveService(ILeaveDataAccess ileaveDataAccess)
        {
            _ileaveDataAccess = ileaveDataAccess;
        }
        //add leave request
        public int CreateLeaveRquest(LeaveRequest request)
        {
            return _ileaveDataAccess.InsertLeaveRquest(request);
        }
        /// <summary>
        /// Get all leave details
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<LeaveCalenderOptions> GetAllLeaveDetails()
        {
            return _ileaveDataAccess.GetAllLeaveDetails();
        }
        /// <summary>
        /// get all leave types
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<LeaveType> GetAllLeaveTypesDetails()
        {
           return _ileaveDataAccess.GetAllLeaveTypes();
        }

        public List<LeaveSummary> GetAppliedLeaveDetailPerStaff(int staffId)
        {
            return _ileaveDataAccess.GetAppliedleaveDetails(staffId);
        }

        /// <summary>
        /// get leave balance
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public List<LeaveBalance> GetLeaveBalanceDetailsPerStaff(int staffId)
        {
           return _ileaveDataAccess.GetLeaveBalancePerStaff(staffId);
        }
        //get staff leave details
        public List<LeaveDetails> GetStaffAllAppliedLeaveDetails(int staffId, int year, Filter filter)
        {
            return _ileaveDataAccess.GetStaffLeaveDetails(staffId, year, filter);
        }
    }
}
