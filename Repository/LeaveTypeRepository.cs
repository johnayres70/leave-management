using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public LeaveTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        // CRUD |Functions
        public bool Create(LeaveType Entity)
        {
            _db.LeaveTypes.Add(Entity);
            return Save();  // return the result of the save funtion
        }


        public bool Update(LeaveType Entity)
        {
            _db.LeaveTypes.Update(Entity);
            return Save();  // return the result of the save funtion
        }

        public bool Delete(LeaveType Entity)
        {
            _db.LeaveTypes.Remove(Entity);
            return Save();  // return the result of the save funtion
        }

        // READ functions
        public ICollection<LeaveType> FindAll()
        {
            var leaveTypes = _db.LeaveTypes.ToList();
            return leaveTypes;
        }

        public LeaveType FindById(int id)
        {
            var leaveType = _db.LeaveTypes.Find(id);
            return leaveType;
        }

        public ICollection<LeaveType> GetEmployeesByLeaveType(int Id)
        {
            throw new NotImplementedException();
        }

        // HELPER functions
        public bool Save()
        {
            var changes = _db.SaveChanges();

            // Return value is true if savechanges returns > 1 record saved else false
            return changes > 0;
        }

        public bool isExists(int id)
        {
            var exists = _db.LeaveTypes.Any(q => q.Id == id);
            return exists;
        }
    }
}
