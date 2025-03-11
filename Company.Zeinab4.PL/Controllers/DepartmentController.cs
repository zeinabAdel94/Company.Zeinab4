using Company.Zeinab4.BLL.Repostiors;
using Company.Zeinab4.DAL.Modules;
using Company.Zeinab4.PL.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Company.Zeinab4.PL.Controllers
{
    //MVC Controller 

    public class DepartmentController : Controller
    {
        private readonly DepartmentRepostiory _departmentRepostiory;
        public DepartmentController( DepartmentRepostiory departmentRepostiory)
        {
            _departmentRepostiory = departmentRepostiory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            
            var departments= _departmentRepostiory.GetAll();
            return View(departments);
            
        }
        [HttpGet]
        public IActionResult Create()
        {
         
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDTO model)
        {
            if(ModelState.IsValid) //Server Vaildat
            {
              var  department = new Department()
                {

                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };
               var count = _departmentRepostiory.Add(department);
                if(count>0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
           

            return View(model);
        }
    }
}
