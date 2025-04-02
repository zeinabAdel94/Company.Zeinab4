using AutoMapper;
using Company.Zeinab4.DAL.Modules;
using Company.Zeinab4.PL.DTO;

namespace Company.Zeinab4.PL.Mapping
{
    public class DepartmentProfile:Profile
    {

        public DepartmentProfile()
        {
            CreateMap<CreateDepartmentDTO,Department>();
            CreateMap<UpdateDepartmentDTO, Department>();

        }
    }
}
