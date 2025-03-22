using System.Security.Policy;
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
                var department = new Department()
                {
                    Id=id,
                    Code=model.Code,
                    Name=model.Name,
                    CreateAt=model.CreateAt

                };
                int count = _departmentRepostiory.Update(department);
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
            //if (Id is null) return BadRequest("Invald id");
            //var department = _departmentRepostiory.Get(Id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, message = $"the department eith id :{Id} is not found " });
            //return View(department);
            return Details(Id, "Delete");
            

        }



        [HttpPost]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
           // if (ModelState.IsValid)
           // {

                if (id == department.Id)
                {
                    var count = _departmentRepostiory.Delete(department);
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
