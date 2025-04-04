using System.Security.Policy;
using Company.Zeinab4.DAL.Modules;
using Company.Zeinab4.PL.DTO;
using Company.Zeinab4.PL.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace Company.Zeinab4.PL.Controllers
{
    public class AccountController : Controller
    {
     
        private readonly UserManager<AppUser> _userManger;
        private readonly SignInManager<AppUser> _singInManger;

        public AccountController(UserManager<AppUser> UserManger, SignInManager<AppUser> singInManger)
        {
           
            _userManger = UserManger;
            _singInManger = singInManger;
        }

        #region SignUp 

        [HttpGet]
        public IActionResult SingUp()
        {
            return View();
        }

        //P@ssW0rd
        [HttpPost]
        public async Task<IActionResult>SingUp(SingUpDTO model)
        {
            if(ModelState.IsValid)
            {
               var User =await  _userManger.FindByNameAsync(model.UserName);
                if(User is null)
                {
                    User = await _userManger.FindByEmailAsync(model.Email);
                    if(User is null)
                    {
                        User = new AppUser()
                        {
                            UserName = model.UserName,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            IsAgree = model.IsAgree

                        };

                        var result = await _userManger.CreateAsync(User, model.Password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("SingIn");
                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }


                    }

                }
                ModelState.AddModelError("", "InVaild SingUp !!");
               
            }
            return View(model );
        }
        #endregion

        #region SignIn

        [HttpGet]
        public IActionResult SingIn()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult>SingIn(SingInDTO model)
        {
            if(ModelState.IsValid)
            {
               var User =   await _userManger.FindByEmailAsync(model.Email);
                if(User is not  null)
                {
                  var  flag =   await _userManger.CheckPasswordAsync(User ,model.Password);
                    if (flag ==true )
                    {
                        //SingIn 
                       var result =await _singInManger.PasswordSignInAsync(User, model.Password,model.RemmberMe, false);
                        if( result.Succeeded )
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");

                        }
                        
                    }
                }
                ModelState.AddModelError("","InVaild Log In ");

            }
            return View(model);

        }
        #endregion


        #region Singout
        public new  async Task< IActionResult> SingOut()
        {
             await _singInManger.SignOutAsync();
            return RedirectToAction(nameof(SingIn));
        }
        #endregion

        #region ForgetPasswoed
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async  Task< IActionResult> ResetPasswordUrl( ForgetPasswordDTO model)
        {
            if(ModelState.IsValid)
            {
                var user  =await  _userManger.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    //Genrate ToKen 
                   var token=  await _userManger.GeneratePasswordResetTokenAsync(user);

                    //Create URL 
                 var url=   Url.Action("ResetPassword", "Account", new { email = model.Email,token }, Request.Scheme);
                    //Create Emaill 
                    var email = new Email()
                    {
                        To = model.Email,
                        Subject = "",
                        Body = url
                    };


                   var flag =  EmailSetting.SendEmail(email);
                    if(flag==true )
                    {
                        //Check Your Emaill 
                        return RedirectToAction(nameof(CheckYourBox));
                       
                    }

                }


            }
            ModelState.AddModelError("", "InVaild Restpassword ");
            return View("ForgetPassword", model);

            
            

        }

        [HttpGet]
        public IActionResult  CheckYourBox()
        {
            return View(); 

        }


        #endregion


        #region ResetPassword 
        [HttpGet]
         public IActionResult ResetPassword( string email , string token )
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();

        }
        [HttpPost]

        public async Task< IActionResult> ResetPassword(ResetPasswordDTO model)
        {
            if(ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;
                if (email is null || token is null) return BadRequest();
                var user =await _userManger.FindByEmailAsync(email);
                if(user is not null)
                {
                  var result = await  _userManger.ResetPasswordAsync(user, token, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(SingIn));
                    }
                }
            }

            ModelState.AddModelError("", "Invaild Reset Password ");
            return View(model);
        }

        #endregion 
    }
}
