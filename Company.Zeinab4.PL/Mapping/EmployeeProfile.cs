using AutoMapper;
using Company.Zeinab4.DAL.Modules;
using Company.Zeinab4.PL.DTO;

namespace Company.Zeinab4.PL.Mapping
{
    public class EmployeeProfile: Profile 
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDTO, Employee>();
            CreateMap<UpdateEmployeeDTOcs, Employee>();
        }
    }
}
