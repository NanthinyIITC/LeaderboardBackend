using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.Staff;
using EmployeeManagement.DataAccess.Authentication;
using EmployeeManagement.DataAccess.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Service.Staff
{
    public class StaffService : IStaffService
    {
        //private properties
        private readonly IStaffDataAccess _istaffDataAccess;
        //constructor
        public StaffService(IStaffDataAccess istaffDataAccess)
        {
            _istaffDataAccess = istaffDataAccess;
        }
      /// <summary>
      /// method implementation og get all staff details
      /// </summary>
      /// <returns></returns>
      /// <exception cref="NotImplementedException"></exception>
        public List<StaffDetails> GetAllExistingStaffDetails(Filter filter)
        {
           //return list of staffs
           return _istaffDataAccess.GetAllStaffDetails(filter);
        }
        /// <summary>
        /// method implementation to get all staff without filter
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<StaffDetails> GetAllExistingStaffDetails()
        {
            //return list of staffs
            return _istaffDataAccess.GetAllStaffDetails();
        }

        /// <summary>
        /// getting staff details by id
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public StaffDetails GetExistingStaffDetailUsingId(int staffId)
        {
            //return staffby id
            return _istaffDataAccess.GetStaffDetailsById(staffId);
        }

        public List<UserType> GetUserTypes()
        {
            //return user types
            return _istaffDataAccess.GetAllUserTypes();
        }

        /// <summary>
        /// method implemetation of create new staff
        /// </summary>
        /// <param name="newStaff"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int RegisterNewStaff(StaffDetails newStaff)
        {
            //return response after new staff added
            return _istaffDataAccess.InsertNewStaff(newStaff);
        }
        /// <summary>
        /// method implementation of remove staff details
        /// </summary>
        /// <param name="staffId"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void RemoveStaffDetails(int staffId)
        {
            //call method to perform remove operation
            _istaffDataAccess.RemoveStaffDetails(staffId);
        }
        /// <summary>
        /// method implementation of update staff details
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="details"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateStaffDetails(int staffId, StaffDetails details)
        {
           //call method to update staff details
           _istaffDataAccess.UpdateStaffDetails(staffId, details);
        }
        /// <summary>
        /// service method to handle create,update,delete actions
        /// </summary>
        /// <param name="action"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ResponseMessage UserTypesActionsCUD(string action, UserType type)
        {
            return _istaffDataAccess.UserTypesActions(action,type);
        }
    }
}
