using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Core.Authentication;
using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.Staff;

namespace EmployeeManagement.DataAccess.Staff
{
    public interface IStaffDataAccess
    {
        /// <summary>
        /// get all staff details from database
        /// </summary>
        /// <returns></returns>
        List<StaffDetails> GetAllStaffDetails(Filter filter);
        /// <summary>
        /// get all staff details from database without filter
        /// </summary>
        /// <returns></returns>
        List<StaffDetails> GetAllStaffDetails();
        /// <summary>
        /// get staff information using id
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        StaffDetails GetStaffDetailsById(int staffId);
        /// <summary>
        /// insert new staff record to database
        /// </summary>
        /// <param name="newStaff"></param>
        /// <returns></returns>
        int InsertNewStaff(StaffDetails newStaff);
        /// <summary>
        /// update staff details
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="details"></param>
        void UpdateStaffDetails(int staffId, StaffDetails details);
        /// <summary>
        /// remove staff from database
        /// </summary>
        /// <param name="staffId"></param>
        void RemoveStaffDetails(int staffId);
        //get all user type details
        List<UserType> GetAllUserTypes();
        //user type action create,update,delete
        ResponseMessage UserTypesActions(string action,UserType type);
    }
}
