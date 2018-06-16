
using SimpleCommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCommerce.Services
{
    public interface IAccountClient
    {
        SignResult SignIn(SignInViewModel user);
    }

}
