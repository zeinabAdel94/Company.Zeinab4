﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Company.Zeinab4.DAL.Modules
{
   public class AppUser:IdentityUser
    {
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAgree { get; set; }
    }
}
