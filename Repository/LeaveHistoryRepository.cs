using leave_management.Contracts;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveHistoryRepository : ILeaveHistoryRepository
    {

        private readonly ApplicationDbContext _db;

        public LeaveHistoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        // CRUD Functins

        public bool Create(LeaveHistory Entity)
        {
            _db.LeaveHistories.Add(Entity);
            return Save();  // return the result of the save funtion

        }


        public bool Update(LeaveHistory Entity)
        {
            _db.LeaveHistories.Update(Entity);
            return Save();  // return the result of the save funtion
        }

        // CRUD
        public bool Delete(LeaveHistory Entity)
        {
            _db.LeaveHistories.Remove(Entity);
            return Save();  // return the result of the save funtion
        }


        // READ functions
        public ICollection<LeaveHistory> FindAll()
        {
            var leaveHistories = _db.LeaveHistories.ToList();
            return leaveHistories;
        }

        public LeaveHistory FindById(int id)
        {
            var leaveHistory = _db.LeaveHistories.Find(id);
            return leaveHistory;
        }


        // HELPER Functions
        public bool Save()
        {
            var changes = _db.SaveChanges();

            // Return value is true if savechanges returns > 1 record saved else false
            return changes > 0;
        }

    }
}
