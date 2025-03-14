using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Zeinab4.DAL.Modules;

namespace Company.Zeinab4.BLL.Interfaces
{
   public interface IEmployeeRepostiory
    {
      IEnumerable<Employee>  GetAll();
       Employee? Get(int Id );
       int  Add(Employee employee);
       int  Update( Employee employee);
       int Delete( Employee employee);
    }
}
