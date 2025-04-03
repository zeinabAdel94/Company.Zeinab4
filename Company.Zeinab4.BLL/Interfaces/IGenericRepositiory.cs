using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Zeinab4.DAL.Modules;

namespace Company.Zeinab4.BLL.Interfaces
{
  public  interface IGenericRepositiory<TEninty>where TEninty : BaseEntity
    {

        Task<IEnumerable<TEninty> >GetAllAsync();
        Task<TEninty? >GetAsync(int Id);
      
        Task AddAsync(TEninty model);
        void Update(TEninty model);
        void Delete(TEninty model);
    }
}
