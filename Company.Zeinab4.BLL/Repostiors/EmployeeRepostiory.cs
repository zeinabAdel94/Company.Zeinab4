using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Zeinab4.BLL.Interfaces;
using Company.Zeinab4.DAL.Data.Context;
using Company.Zeinab4.DAL.Modules;
using Microsoft.EntityFrameworkCore;

namespace Company.Zeinab4.BLL.Repostiors
{
    public class EmployeeRepostiory :GenericRepository<Employee>,IEmployeeRepostiory
    {
        private readonly CompanyDbContext _Context;

        public EmployeeRepostiory(CompanyDbContext companyDbContext) : base(companyDbContext)
        {
            _Context = companyDbContext;
        }

        public async Task< List<Employee> >GetByNameAsync(string name)
        {
            return await _Context.Employees.Include(E=>E.Department).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToListAsync();


        }
    }
}
