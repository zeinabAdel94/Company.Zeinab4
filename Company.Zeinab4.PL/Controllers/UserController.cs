using Company.Zeinab4.DAL.Modules;
using Company.Zeinab4.PL.DTO;
using Company.Zeinab4.PL.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Identity.Client;
using NuGet.Protocol.Core.Types;

namespace Company.Zeinab4.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController( UserManager<AppUser> userManager)
        {
           _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index(string? SearchInput)
        {
       
           
            IEnumerable<UserToReturnDTO> users;
            if (string.IsNullOrEmpty(SearchInput))
            {
             users=   _userManager.Users.Select(U => new UserToReturnDTO()
                {
                    Id=U.Id,
                    UserName = U.UserName,
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Eamil = U.Email,
                    Roles = _userManager.GetRolesAsync(U).Result

                });

            

            }
            else
            {
                users = _userManager.Users.Select(U => new UserToReturnDTO()
                {
                    Id = U.Id,
                    UserName = U.UserName,
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Eamil = U.Email,
                    Roles = _userManager.GetRolesAsync(U).Result

                }).Where(U => U.FirstName.ToLower() == SearchInput.ToLower());
              


            }
            return View(users);

        }




        [HttpGet]
        public async Task<IActionResult> Details(string? Id, string viewName = "Details")
        {
            var user = await _userManager.FindByIdAsync(Id);
         
            if (user is null) return NotFound(new { StatusCode = 404, message = $"The  User with id ={Id} is  Not Found " });
            var dto = new UserToReturnDTO()
            {
                Id=user.Id,
                UserName=user.UserName,
                FirstName=user.FirstName,
                LastName=user.LastName,
                Eamil=user.Email,
                Roles=_userManager.GetRolesAsync(user).Result



            };
            return View(viewName,dto);
        }


        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            return await Details(id, "Update");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] string? id, UserToReturnDTO model)
        {


            if (ModelState.IsValid)
            {

                if (id != model.Id) return BadRequest("InValid Oparation");
                var user =await _userManager.FindByIdAsync(id);
                if (user is null) return BadRequest("invaild opration ");
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Eamil;
              var result =await  _userManager.UpdateAsync(user);

                if(result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                

           

              
            }


            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {


            return await Details(id, "Delete");


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string? id, UserToReturnDTO model)
        {

            if (ModelState.IsValid)
            {

                if (id != model.Id) return BadRequest("InValid Oparation");
                var user = await _userManager.FindByIdAsync(id);
                if (user is null) return BadRequest("invaild opration ");
          
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }





            }


            return View(model);
        }
    }

}

