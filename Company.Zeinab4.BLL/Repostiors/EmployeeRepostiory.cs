using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Zeinab4.BLL.Interfaces;
using Company.Zeinab4.DAL.Data.Context;
using Company.Zeinab4.DAL.Modules;

namespace Company.Zeinab4.BLL.Repostiors
{
    public class EmployeeRepostiory : IEmployeeRepostiory
    {
        private readonly CompanyDbContext _Context;
        public EmployeeRepostiory(CompanyDbContext companyDbContext)
        {
            _Context = companyDbContext;
            
        }

        public IEnumerable<Employee> GetAll()
        {
            return _Context.Employees.ToList();
            
        }


        public Employee? Get(int Id)
        {
            return _Context.Employees.Find(Id);
        }
        public int Add(Employee employee)
        {
            _Context.Employees.Add(employee);
            return _Context.SaveChanges();
        }


        public int Update(Employee employee)
        {
            _Context.Employees.Update(employee);
            return _Context.SaveChanges();
        }

        public int Delete(Employee employee)
        {
            _Context.Employees.Remove(employee);
            return _Context.SaveChanges();
        }






    }
}
