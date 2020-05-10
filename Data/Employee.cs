﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class Employee:IdentityUser
    {
        public string Fisrtname { get; set; }
        public string Lasttname { get; set; }
        public string TaxId{ get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; }

       

    }
}