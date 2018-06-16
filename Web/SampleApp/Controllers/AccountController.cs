using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleCommerce.Models.Consts;
using SimpleCommerce.Services;
using SimpleCommerce.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleCommerce.Controllers
{
    public class AccountController : Controller
    {
        private IAccountClient _accountClient;

        public AccountController(IAccountClient accountClient)
        {
            _accountClient = accountClient;
        }
 
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(SignInViewModel model,string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
 
            if (ModelState.IsValid)
            {
                var result = _accountClient.SignIn(model);
                if (result.Succeed)
                {
                    
                    var claimsIdentity = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, model.UserId),}, "Basic");
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Expiration, result.ExpireInSeconds.ToString()));
                    claimsIdentity.AddClaim(new Claim(ClaimTypesConstants.AccessToken, result.AccessToken));
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                }
            }
            return View(model);
        }


        

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        
    }
}
