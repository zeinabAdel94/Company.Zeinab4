using Company.Zeinab4.DAL.Modules;
using Company.Zeinab4.PL.DTO;
using Company.Zeinab4.PL.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Zeinab4.PL.Controllers
{
    public class RoleController : Controller
    {

            private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,UserManager<AppUser> userManager)
            {
                _roleManager =roleManager;
              _userManager = userManager;
        }



            [HttpGet]
            public async Task< IActionResult> Index(string? SearchInput)
            {


            IEnumerable<RoleDTO> roles ;
                if (string.IsNullOrEmpty(SearchInput))
                {
                 roles  =  _roleManager.Roles.Select(r => new RoleDTO() { 
                    Id=r.Id,
                    Name=r.Name,
                });
            



                }
                else
                {
                    roles = _roleManager.Roles.Select(r => new RoleDTO()
                    {
                        Id =r.Id,
                        Name=r.Name
                        

                    }).Where(r => r.Name.ToLower().Contains(SearchInput.ToLower()));



                }
                return View(roles);

            }



        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                  var role  =await   _roleManager.FindByNameAsync(model.Name);
                    if (role  is not null) return  BadRequest("Invaild operation ==> this role is Created Be fore ");
                    role = new IdentityRole()
                    {
                        Name = model.Name
                    };


                    var result =await  _roleManager.CreateAsync(role);
                    if(result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }



                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }


            return View(model);

        }







        [HttpGet]
            public async Task<IActionResult> Details(string? Id, string viewName = "Details")
            {
                var role  = await _roleManager.FindByIdAsync(Id);

                if (role is null) return NotFound(new { StatusCode = 404, message = $"The Role with id ={Id} is  Not Found " });
                var dto = new RoleDTO()
                {
                    Id=role.Id,
                    Name=role.Name
                  


                };
                return View(viewName, dto);
            }


            [HttpGet]
            public async Task<IActionResult> Update(string? id)
            {
                return await Details(id, "Update");
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Update([FromRoute] string? id,RoleDTO model)
            {


                if (ModelState.IsValid)
                {

                    if (id != model.Id) return BadRequest("InValid Oparation");
                    var role  = await _roleManager.FindByIdAsync(id);
                    if (role is null) return BadRequest("invaild opration ");
                 var roleResult = await _roleManager.FindByNameAsync(model.Name);
               if(roleResult is null)
                {
                    role.Name = model.Name;
                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }


                }





            }

            ModelState.AddModelError("","Inavild operation ");
                return View(model);
            }


            [HttpGet]
            public async Task<IActionResult> Delete(string? id)
            {


                return await Details(id, "Delete");


            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Delete([FromRoute] string? id, RoleDTO model)
            {

                if (ModelState.IsValid)
                {

                    if (id != model.Id) return BadRequest("InValid Oparation");
                    var user = await _roleManager.FindByIdAsync(id);
                    if (user is null) return BadRequest("invaild opration ");

                    var result = await _roleManager.DeleteAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }





                }


                return View(model);
            }


        [HttpGet]
        public async Task < IActionResult > AddOrRemoveUser(string roleId )
        {


          var role =  await _roleManager.FindByIdAsync(roleId);
            if (role is null) return NotFound();

            ViewData["RoleId"] = role.Id;

            var usersInRole = new List<UserInRoleViewModel>();
            var users =await _userManager.Users.ToListAsync();
            foreach(var user in users)
            {
                var userInRole = new UserInRoleViewModel()
                {
                    UserId=user.Id,
                    UserName=user.UserName

                };
                if( await _userManager.IsInRoleAsync(user , role.Name))
                {
                    userInRole.IsSelected = true;
                }else
                {
                    userInRole.IsSelected = false;
                }
                usersInRole.Add(userInRole);
            }
            return View(usersInRole);

        }


        [HttpPost]
        public async Task< IActionResult> AddOrRemoveUser(string roleId , List<UserInRoleViewModel> users )
        {
            var role =await  _roleManager.FindByIdAsync(roleId);
            if (role is null) return NotFound();
            if(ModelState.IsValid)
            {
                foreach (var user  in users)
                {
                    var appUser =await  _userManager.FindByIdAsync(user.UserId); 
                    if(appUser is not null)
                    {
                        if (user.IsSelected &&! await  _userManager.IsInRoleAsync(appUser,role.Name))
                        {
                           await _userManager.AddToRoleAsync(appUser,role.Name);

                        }
                        else if ( !user.IsSelected && await _userManager.IsInRoleAsync(appUser , role.Name))
                        {
                          await  _userManager.RemoveFromRoleAsync(appUser,role.Name);
                        }
                    }
                    
                }
                return RedirectToAction(nameof(Update), new {id=roleId});

            }

            return View(users);


        }


    }



  


}

//P@ssW0rd
//leref20394@exclussi.com