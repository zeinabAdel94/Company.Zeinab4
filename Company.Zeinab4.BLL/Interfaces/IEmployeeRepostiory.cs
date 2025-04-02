using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Zeinab4.DAL.Modules;

namespace Company.Zeinab4.BLL.Interfaces
{
    public interface IEmployeeRepostiory : IGenericRepositiory<Employee>
    {
        List<Employee> GetByName(string name);
       

    }
}
