﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Zeinab4.BLL.Interfaces;
using Company.Zeinab4.DAL.Data.Context;
using Company.Zeinab4.DAL.Modules;

namespace Company.Zeinab4.BLL.Repostiors
{
    public class EmployeeRepostiory : GenericRepository<Employee>
    {
        public EmployeeRepostiory(CompanyDbContext companyDbContext) : base(companyDbContext)
        {

        }
    }
}
