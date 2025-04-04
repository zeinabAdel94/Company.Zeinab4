﻿using System.Security.Policy;
using AutoMapper;
using Company.Zeinab4.BLL.Interfaces;
using Company.Zeinab4.BLL.Repostiors;
using Company.Zeinab4.DAL.Modules;
using Company.Zeinab4.PL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Company.Zeinab4.PL.Controllers
{
    [Authorize]
    //MVC Controller 

    public class DepartmentController : Controller
    {
        
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public DepartmentController(IMapper mapper ,IUnitOfWork unitOfWork)
        {
            
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task< IActionResult> Index()
        {
            
        var departments=await _unitOfWork.DepartmentRepostiory.GetAllAsync();
          
            return View(departments);
            
        }





        [HttpGet]
        public IActionResult Create()
        {
         
            return View();
        }





        [HttpPost]
        public async Task< IActionResult> Create(CreateDepartmentDTO model)
        {
            if(ModelState.IsValid) //Server Vaildat
            {
               
              var department= _mapper.Map<Department>(model);
                await _unitOfWork.DepartmentRepostiory.AddAsync(department);
                var count =await _unitOfWork.CompleteAsync();
                if (count>0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
           

            return View(model);
        }





        [HttpGet]
        public async Task< IActionResult> Details (int? Id,string viewName="Details" )
        {
            if (Id is null) return BadRequest("Id is Invaild ");
            var department =await _unitOfWork.DepartmentRepostiory.GetAsync(Id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"the department with id :{Id}is not found " });
            return View(viewName,department);
        }




        [HttpGet]
        public async  Task< IActionResult> Update(int? Id)
        {
            return await Details(Id, "Update");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Update([FromRoute] int id  ,UpdateDepartmentDTO model)
        {
            if(ModelState.IsValid)
            {
                
                var department = _mapper.Map<Department>(model);
                _unitOfWork.DepartmentRepostiory.Update(department);
                var count =await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            
                 
            
      
            return View(model);
        }



        [HttpGet]
        public async  Task <IActionResult> Delete (int?Id )
        {
         
            return await Details(Id, "Delete");
            

        }



        [HttpPost]
        public async Task< IActionResult >Delete([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {

                if (id == department.Id)
                {
                     _unitOfWork.DepartmentRepostiory.Delete(department);
                var count =await _unitOfWork.CompleteAsync();
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
                return View(department);

            }

        }

        }
