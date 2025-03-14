using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Zeinab4.DAL.Modules;

namespace Company.Zeinab4.BLL.Interfaces
{
  public  interface IGenericRepositiory<T>where  T:BaseEntity
    {

        IEnumerable<T> GetAll();
        T? Get(int Id);
        int Add(T model);
        int Update(T model);
        int Delete(T model);
    }
}
