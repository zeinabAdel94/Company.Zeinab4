using System.CodeDom;
using Company.Zeinab4.BLL.Interfaces;
using Company.Zeinab4.BLL.Repostiors;
using Company.Zeinab4.DAL.Modules;
using Company.Zeinab4.PL.DTO;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace Company.Zeinab4.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepostiory _employeeRepostiory;
        private readonly DepartmentRepostiory _departmentRepostiory;

        public EmployeeController(EmployeeRepostiory employeeRepostiory,DepartmentRepostiory departmentRepostiory)
        {
            _employeeRepostiory = employeeRepostiory;
           _departmentRepostiory = departmentRepostiory;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var Employees = _employeeRepostiory.GetAll();
      
            return View(Employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
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
                    var employee = new Employee()
                    {
                        Name = model.Name,
                        Age = model.Age,
                        Address = model.Address,
                        Email = model.Email,
                        IsActive = model.IsActive,
                        IsDeleted = model.IsDeleted,
                        HiringDate = model.HiringDate,
                        CreateAt = model.CreateAt,
                        Salary = model.Salary,
                        phone = model.phone,
                        DepartmentId=model.DepartmentId

                    };

                    int count = _employeeRepostiory.Add(employee);

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
            var employee = _employeeRepostiory.Get(Id.Value);
            if (employee is null) return NotFound(new {StatusCode=404 , message =$"The  Employee with id ={Id} is  Not Found "});
            return View(viewName,employee);
        }


        [HttpGet]
        public IActionResult Update(int? id)
        {
            var departments = _departmentRepostiory.GetAll();
            ViewData["Departments"] = departments;

            if (id is null) return BadRequest("Id is Invaild ");
           var employee=  _employeeRepostiory.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"The  Employee with id ={id} is  Not Found " });
            return  View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update( [FromRoute]int?id ,UpdateEmployeeDTOcs model)
        {
            

            //if (ModelState.IsValid)
            //{
              

                if (id is not null)
                {
                    var employee = new Employee()
                    {
                        Id = id.Value,
                        Name = model.Name,
                        Age = model.Age,
                        Email = model.Email,
                        phone = model.phone,
                        Address = model.Address,
                        Salary = model.Salary,
                        IsActive = model.IsActive,
                        IsDeleted = model.IsDeleted,
                        CreateAt = model.CreateAt,
                        HiringDate = model.HiringDate,
                        DepartmentId = model.DepartmentId,


                    };

                    var count = _employeeRepostiory.Update(employee);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                //}
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
        public IActionResult Delete( [FromRoute]int? id , Employee model)
        {
            if (ModelState.IsValid)
            {
                if (id ==model.Id)
                {
                    var count = _employeeRepostiory.Delete(model);
                    if(count>0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return Details(id, "Delete");

        }
    }
    
}
