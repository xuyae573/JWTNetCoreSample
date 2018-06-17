using SimpleCommerce.Core.Dto;
using SimpleCommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCommerce.Models
{
    public class HomeViewModel
    {
        public List<ProductDto> Products { get; set; }

        public User UserModel { get; set; }
    }
}
