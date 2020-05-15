using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {

        private readonly ApplicationDbContext _db;

        public LeaveRequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        // CRUD Functins

        public bool Create(LeaveRequest Entity)
        {
            _db.LeaveRequests.Add(Entity);
            return Save();  // return the result of the save funtion

        }


        public bool Update(LeaveRequest Entity)
        {
            _db.LeaveRequests.Update(Entity);
            return Save();  // return the result of the save funtion
        }

        // CRUD
        public bool Delete(LeaveRequest Entity)
        {
            _db.LeaveRequests.Remove(Entity);
            return Save();  // return the result of the save funtion
        }


        // READ functions
        public ICollection<LeaveRequest> FindAll()
        {
            // include fireign key details
            var leaveRequests = _db.LeaveRequests
                .Include(q => q.RequestingEmployee)
                .Include(q => q.ApprovedBy)
                .Include(q => q.LeaveType)
                .ToList();
            return leaveRequests;
        }

        public LeaveRequest FindById(int id)
        {
            // include fireign key details
            var LeaveRequest = _db.LeaveRequests
                .Include(q => q.RequestingEmployee)
                .Include(q => q.ApprovedBy)
                .Include(q => q.LeaveType)
                .FirstOrDefault(q => q.Id == id); 
            return LeaveRequest;
        }


        // HELPER Functions
        public bool Save()
        {
            var changes = _db.SaveChanges();

            // Return value is true if savechanges returns > 1 record saved else false
            return changes > 0;
        }

        public ICollection<LeaveRequest> GetLeaveRequestsByEmployee(string employeeid)
        {
            var leaveRequests = FindAll()
                .Where(q => q.RequestingEmployeeId == employeeid)
                .ToList();
            return leaveRequests;
        }

        public bool isExists(int id)
        {
            var exists = _db.LeaveRequests.Any(q => q.Id == id);
            return exists;
        }

    }
}
