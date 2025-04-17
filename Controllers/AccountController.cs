using System.Security.Claims;
using Job_Portal_Project.Models;
using Job_Portal_Project.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Job_Portal_Project.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> _userManage, SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManage;
            signInManager = _signInManager;
        }

        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userFromDB = new ApplicationUser()
                {
                    UserName = userVM.UserName,
                    Email = userVM.Email,
                    //PasswordHash = userVM.Password,
                    Address = userVM.Address,
                    Company = userVM.Company,
                };

                //create db
                IdentityResult result = await userManager.CreateAsync(userFromDB, userVM.Password);

                //create cookie
                if (result.Succeeded)
                {
                    //to assign admins ==>>
                    //await userManager.AddToRoleAsync(userFromDB, "Admin");
                    await signInManager.SignInAsync(userFromDB,false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View("Register", userVM);
        }

        #endregion

        #region Login
  
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {    //check
                ApplicationUser userFromDB = await userManager.FindByNameAsync(loginVM.UserName);

                if (userFromDB != null)
                {

                    bool found = await userManager.CheckPasswordAsync(userFromDB, loginVM.Password);
                    if (found)
                    {
                        //create cookie
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim("Address", userFromDB.Address));
                        claims.Add(new Claim("Company", userFromDB.Company));

                        //await signInManager.SignInAsync(userFromDB, loginVM.RememberMe);
                        await signInManager.SignInWithClaimsAsync(userFromDB, loginVM.RememberMe,claims);

                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError("", "Username or Password is invalid");

            }
            return View("Login", loginVM);
        }
        #endregion
    }
}
