using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Zeinab4.DAL.Modules;

namespace Company.Zeinab4.BLL.Interfaces
{
  public interface IDepartmentRepostiory
    {
       IEnumerable<Department> GetAll();
       Department? Get(int Id);
       int Add(Department model);
        int Update(Department model);
        int Delete(Department model);



    }
}
