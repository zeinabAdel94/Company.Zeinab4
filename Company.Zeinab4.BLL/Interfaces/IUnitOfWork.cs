using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Zeinab4.BLL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IDepartmentRepostiory DepartmentRepostiory { get; }
        IEmployeeRepostiory EmployeeRepostiory { get; }
       Task< int > CompleteAsync();
    }
}
