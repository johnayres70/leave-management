using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CheckAllocation(int leavetypeid, string employeeid)
        {
            var period = DateTime.Now.Year;
            return FindAll()
                .Where(q => q.EmployeeId == employeeid && q.LeaveTypeId == leavetypeid && q.Period == period)
                .Any();
        }

        public ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string id)
        {
            var period = DateTime.Now.Year;
            return FindAll()
                    .Where(q => q.EmployeeId == id && q.Period == period)
                    .ToList();
        }

        // CRUD |Functions
        public bool Create(LeaveAllocation Entity)
        {
            _db.LeaveAllocations.Add(Entity);
            return Save();  // return the result of the save funtion
        }

        public bool Update(LeaveAllocation Entity)
        {
            _db.LeaveAllocations.Update(Entity);
            return Save();  // return the result of the save funtion
        }

        public bool Delete(LeaveAllocation Entity)
        {
            _db.LeaveAllocations.Remove(Entity);
            return Save();  // return the result of the save funtion
        }

      
        // READ Functions
        public ICollection<LeaveAllocation> FindAll()
        {
            var leaveAllocations = _db.LeaveAllocations.Include(q => q.LeaveType).ToList(); // join on LeaveType
            return leaveAllocations;
        }

        //public LeaveAllocation FindById(int id)
        //{
        //    var leaveAllocation = _db.LeaveAllocations.Find(id);
        //    return leaveAllocation;
        //}

        public LeaveAllocation FindById(int id)
        {
             var LeaveAllocation = _db.LeaveAllocations
                .Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .FirstOrDefault(q => q.Id == id);
            return LeaveAllocation;
        }

        // HELPER Functions
        public bool Save()
        {
            var changes = _db.SaveChanges();

            // Return value is true if savechanges returns > 1 record saved else false
            return changes > 0;

        }

        public bool isExists(int id)
        {
            var exists = _db.LeaveAllocations.Any(q => q.Id == id);
            return exists;
        }
    }
}
