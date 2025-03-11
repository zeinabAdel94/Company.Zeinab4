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
    public class DepartmentRepostiory : IDepartmentRepostiory
    {
     private readonly  CompanyDbContext _Context;
        public DepartmentRepostiory(CompanyDbContext companyDbContext)
        {
            _Context =companyDbContext;
        }
        public IEnumerable<Department> GetAll()
        {
            return _Context.Departments.ToList();
        
        }
        public Department? Get(int Id)
        {
           return _Context.Departments.Find(Id);

        }
        public int Add(Department model)
        {
            _Context.Departments.Add(model);
            return _Context.SaveChanges();
        }


        public int Update(Department model)
        {
            _Context.Departments.Update(model);
            return _Context.SaveChanges();
        }

        public int Delete(Department model)
        {
            _Context.Departments.Remove(model);
            return _Context.SaveChanges();
        }

   

      

    }
}
