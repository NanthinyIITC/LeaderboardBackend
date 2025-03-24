using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Service.Staff
{
    public interface IStaffService
    {
        /// <summary>
        /// get all staff details 
        /// </summary>
        /// <returns></returns>
        List<StaffDetails> GetAllExistingStaffDetails(Filter filter);
        /// <summary>
        /// get all staff details without filter
        /// </summary>
        /// <returns></returns>
        List<StaffDetails> GetAllExistingStaffDetails();
        /// <summary>
        /// get staff information using id
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        StaffDetails GetExistingStaffDetailUsingId(int staffId);
        /// <summary>
        /// insert new staff record to database
        /// </summary>
        /// <param name="newStaff"></param>
        /// <returns></returns>
        int RegisterNewStaff(StaffDetails newStaff);
        /// <summary>
        /// update staff details
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="details"></param>
        void UpdateStaffDetails(int staffId, StaffDetails details);
        /// <summary>
        /// remove staff details
        /// </summary>
        /// <param name="staffId"></param>
        void RemoveStaffDetails(int staffId);
        /// <summary>
        /// Get all user types
        /// </summary>
        /// <returns></returns>
        List<UserType> GetUserTypes();
        //user type action create,update,delete
        ResponseMessage UserTypesActionsCUD(string action, UserType type);
    }
}
