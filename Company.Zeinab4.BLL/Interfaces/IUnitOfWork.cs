using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Zeinab4.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDepartmentRepostiory DepartmentRepostiory { get; }
        IEmployeeRepostiory EmployeeRepostiory { get; }
        int Complete();
    }
}
