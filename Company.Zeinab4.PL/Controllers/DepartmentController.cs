using System.Security.Policy;
using AutoMapper;
using Company.Zeinab4.BLL.Interfaces;
using Company.Zeinab4.BLL.Repostiors;
using Company.Zeinab4.DAL.Modules;
using Company.Zeinab4.PL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Company.Zeinab4.PL.Controllers
{
    //MVC Controller 

    public class DepartmentController : Controller
    {
        private readonly DepartmentRepostiory _departmentRepostiory;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public DepartmentController( DepartmentRepostiory departmentRepostiory,IMapper mapper ,IUnitOfWork unitOfWork)
        {
            _departmentRepostiory = departmentRepostiory;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
               
              var department= _mapper.Map<Department>(model);
                 _departmentRepostiory.Add(department);
                var count = _unitOfWork.Complete();
                if (count>0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
           

            return View(model);
        }





        [HttpGet]
        public IActionResult Details (int? Id,string viewName="Details" )
        {
            if (Id is null) return BadRequest("Id is Invaild ");
            var department = _departmentRepostiory.Get(Id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"the department with id :{Id}is not found " });
            return View(viewName,department);
        }




        [HttpGet]
        public IActionResult Update(int? Id)
        {
            return Details(Id, "Update");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute] int id  ,UpdateDepartmentDTO model)
        {
            if(ModelState.IsValid)
            {
                
                var department = _mapper.Map<Department>(model);
        _departmentRepostiory.Update(department);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            
                 
            
      
            return View(model);
        }



        [HttpGet]
        public IActionResult Delete (int?Id )
        {
         
            return Details(Id, "Delete");
            

        }



        [HttpPost]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
           // if (ModelState.IsValid)
           // {

                if (id == department.Id)
                {
                     _departmentRepostiory.Delete(department);
                var count = _unitOfWork.Complete();
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

            //}
                return View(department);

            }

        }

        }
