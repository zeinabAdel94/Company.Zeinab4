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
    public  class GenericRepository<T> :IGenericRepositiory<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _Context;
        public GenericRepository( CompanyDbContext companyDbContext)
        {
            _Context =companyDbContext;
            
        }



        public IEnumerable<T> GetAll()
        {
            return _Context.Set<T>().ToList();
        }
        public T? Get(int Id)
        {
            return _Context.Set<T>().Find(Id);
        }


        public int Add(T model)
        {
            _Context.Set<T>().Add(model);
            return _Context.SaveChanges();
        }
        public int Update(T model)
        {
            _Context.Set<T>().Update(model);
            return _Context.SaveChanges();
        }

        public int Delete(T model)
        {
            _Context.Set<T>().Remove(model);
            return _Context.SaveChanges();
        }

       

      
    }
}
