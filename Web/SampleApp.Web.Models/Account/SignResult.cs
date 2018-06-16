using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCommerce.Web.Models
{
    public class SignResult
    {
        public bool Succeed { get; set; }

        public User User { get; set; }

        public string AccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public string ErrorMessage { get; set; }
    }
}
