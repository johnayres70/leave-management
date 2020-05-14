using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class LeaveAllocation
    {
        [Key]
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        
        public DateTime DateCreated { get; set; }

        public Employee Employee { get; set; }

        [ForeignKey("EmployeeId")]
        public string EmployeeId  { get; set; }

        [ForeignKey("LeaveTypeId")]
        public LeaveType LeaveType { get; set; }

        public int LeaveTypeId{ get; set; }

        // jpa added  stores the current year ie 2020
        public int Period { get; set; } 



    }
}
