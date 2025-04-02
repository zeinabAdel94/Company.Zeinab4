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
    public  class GenericRepository<T> :IGenericRepositiory<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _Context;
        public GenericRepository( CompanyDbContext companyDbContext)
        {
            _Context =companyDbContext;
            
        }



        public IEnumerable<T> GetAll()
        {
            if(typeof(T)==typeof(Employee))
            {
                return(IEnumerable<T>) _Context.Employees.Include(E=>E.Department).ToList();
            }
            return _Context.Set<T>().ToList();
        }
        public T? Get(int Id)
            
        {
            if (typeof(T) == typeof(Employee))
            {
               return  _Context.Employees.Include(E=>E.Department).FirstOrDefault(E=>E.Id==Id) as T;
            }
             return _Context.Set<T>().Find(Id);
        }


        public void Add(T model)
        {
            _Context.Set<T>().Add(model);
        
        }
        public void Update(T model)
        {
            _Context.Set<T>().Update(model);
         
        }

        public void Delete(T model)
        {
            _Context.Set<T>().Remove(model);
            
        }

       

      
    }
}
