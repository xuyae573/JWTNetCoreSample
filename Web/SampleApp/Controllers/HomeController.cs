namespace SimpleCommerce.Controllers
{
    using SimpleCommerce.Models;
    using SimpleCommerce.Models.Consts;
    using SimpleCommerce.Web.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SimpleCommerce.Services;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    [Authorize]
    public class HomeController : Controller
    {

        private IProductClient _productClient;

        public HomeController(IProductClient client)
        {
              this._productClient = client;
        }

        public IActionResult Error()
        {
            ErrorViewModel model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? base.HttpContext.TraceIdentifier
            };
            return this.View(model);
        }


        public IActionResult Index()
        {
            var viewModel = new HomeViewModel();
            var claims = HttpContext.User.Claims;

            var tokenClaim = claims.First(x => x.Type == ClaimTypesConstants.AccessToken).Value.ToString();
 
            viewModel.Products = _productClient.GetAllProducts(tokenClaim);
           
            return View(viewModel);
        }

   
      

        
    }
}
