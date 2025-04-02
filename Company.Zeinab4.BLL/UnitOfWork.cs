using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Zeinab4.BLL.Interfaces;
using Company.Zeinab4.BLL.Repostiors;
using Company.Zeinab4.DAL.Data.Context;
using Company.Zeinab4.DAL.Modules;

namespace Company.Zeinab4.BLL
{
   public  class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext _context;

        public IDepartmentRepostiory DepartmentRepostiory { get; }

        public IEmployeeRepostiory EmployeeRepostiory { get; }

        public UnitOfWork(CompanyDbContext context)
        {
            _context = context;
       

            EmployeeRepostiory = new EmployeeRepostiory(_context);
           //DepartmentRepostiory = new DepartmentRepostiory(_context);

        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}