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



        public async Task< IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T)==typeof(Employee))
            {
                return(IEnumerable<T>)await _Context.Employees.Include(E=>E.Department).ToListAsync();
            }
            return await _Context.Set<T>().ToListAsync();
        }


        public async  Task<T?> GetAsync(int Id)
            
        {
            if (typeof(T) == typeof(Employee))
            {
               return await _Context.Employees.Include(E=>E.Department).FirstOrDefaultAsync(E=>E.Id==Id) as T;
            }
             return _Context.Set<T>().Find(Id);
        }


        public async Task AddAsync(T model)
        {
           await _Context.Set<T>().AddAsync(model);
        
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
