using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ProductAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TestUserInfoController : Controller
    {
        // GET api/UserInfomration/
        [HttpGet]
        public string Get()
        {
            var claims = HttpContext.User.Claims;
            var userid = claims.First(x=> x.Type == ClaimTypes.NameIdentifier).Value.ToString();
            var username = HttpContext.User.Identity.Name;
            var email = claims.First(x => x.Type == ClaimTypes.Email).Value.ToString();
            return JsonConvert.SerializeObject(new
            {
                UserId = userid,
                UserName = username,
                Email = email
            });
        }
    }
}
