using Microsoft.AspNetCore.Mvc;
using SimpleCommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCommerce
{
    public class LoginPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var user = new User();
            user.UserId = HttpContext.User.Identity.Name;
            return View(user);
        }
    }
}
