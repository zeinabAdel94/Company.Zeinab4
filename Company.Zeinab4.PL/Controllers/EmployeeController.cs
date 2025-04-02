using System.CodeDom;
using AutoMapper;
using Company.Zeinab4.BLL.Interfaces;
using Company.Zeinab4.BLL.Repostiors;
using Company.Zeinab4.DAL.Modules;
using Company.Zeinab4.PL.DTO;
using Company.Zeinab4.PL.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using NuGet.Protocol;

namespace Company.Zeinab4.PL.Controllers
{
    public class EmployeeController : Controller
    {
        //private readonly EmployeeRepostiory _employeeRepostiory;
       private readonly DepartmentRepostiory _departmentRepostiory;


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(
            //EmployeeRepostiory employeeRepostiory,
            DepartmentRepostiory departmentRepostiory,

             IUnitOfWork unitOfWork,
            IMapper mapper)
        {
           //_employeeRepostiory = employeeRepostiory;
           _departmentRepostiory = departmentRepostiory;


            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Index(string?SearchInput)
        {
            IEnumerable<Employee> employees;
            if(string.IsNullOrEmpty(SearchInput))
            {
                employees =_unitOfWork.EmployeeRepostiory.GetAll();

        

            }else
            {
                employees =_unitOfWork.EmployeeRepostiory.GetByName(SearchInput);
             
            }
            return View(employees);

        }

        [HttpGet]
        public IActionResult Create()
        {
           // var departments =_unitOfWork.DepartmentRepostiory.GetAll();
            var departments = _departmentRepostiory.GetAll();
            ViewData["Departments"] = departments;
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( CreateEmployeeDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(model.Image is not null)
                    {
                      
                      model.ImageName  =DocumentSetting.UploadFile(model.Image, "images");
                    }

                  
                     var employee=_mapper.Map<Employee>(model);

                     _unitOfWork.EmployeeRepostiory.Add(employee);
                    var count = _unitOfWork.Complete();

                    if (count > 0)
                    {
                        TempData["Message"] = "Employee Is Created :)";
                        return RedirectToAction(nameof(Index));
                    }


                }
                catch(Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }


            return View(model);

        }

        [HttpGet]
        public IActionResult Details(int? Id,string viewName="Details")
        {
            var departments = _departmentRepostiory.GetAll();
            ViewData["Departments"] = departments;
            if (Id is null) return BadRequest("Invaild Id ");
            var employee = _unitOfWork.EmployeeRepostiory.Get(Id.Value);
            if (employee is null) return NotFound(new {StatusCode=404 , message =$"The  Employee with id ={Id} is  Not Found "});
            return View(viewName,employee);
        }


        [HttpGet]
        public IActionResult Update(int? id)
        {
            var departments = _departmentRepostiory.GetAll();
            ViewData["Departments"] = departments;

            if (id is null) return BadRequest("Id is Invaild ");
           var employee=  _unitOfWork.EmployeeRepostiory.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"The  Employee with id ={id} is  Not Found " });
            return  View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update( [FromRoute]int?id ,CreateEmployeeDTO model)
        {
            

            if (ModelState.IsValid)
            {
              if(model.ImageName is not null &&model.Image is not null)
                {
                    DocumentSetting.DeleteFile(model.ImageName, "images");
                }
              if(model.Image is not null)
                {
                    model.ImageName = DocumentSetting.UploadFile(model.Image, "images");
                }

                if (id is not null)
                {
      
                var employee = _mapper.Map<Employee>(model);

                   _unitOfWork.EmployeeRepostiory.Update(employee);
                var count = _unitOfWork.Complete();
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }
           }


            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id )
        {
            
   
            return Details(id, "Delete");
            

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete( [FromRoute]int? id ,CreateEmployeeDTO model)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);
                if (id ==employee.Id)
                {
                    _unitOfWork.EmployeeRepostiory.Delete(employee);
                    var count = _unitOfWork.Complete();
                    if (count>0)
                    {
                        if(model.ImageName is not null)
                        {
                            DocumentSetting.DeleteFile(model.ImageName, "images");
                        }
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return Details(id, "Delete");

        }
    }
    
}
