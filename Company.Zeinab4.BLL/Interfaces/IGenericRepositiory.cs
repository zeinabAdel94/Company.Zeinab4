﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Zeinab4.DAL.Modules;

namespace Company.Zeinab4.BLL.Interfaces
{
  public  interface IGenericRepositiory<TEninty>where TEninty : BaseEntity
    {

        IEnumerable<TEninty> GetAll();
        TEninty? Get(int Id);
        void Add(TEninty model);
        void Update(TEninty model);
        void Delete(TEninty model);
    }
}
