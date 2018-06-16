using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Service;
using SimpleCommerce.Core.Domain;

namespace ProductAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private IProductService _productService;
         
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        

        [HttpGet]
        public JsonResult Get()
        {
            var res = new GenericAPIResponse();
            res.Success = true;
            res.Result =  _productService.GetProducts();
            return Json(res);
        }
    }
}