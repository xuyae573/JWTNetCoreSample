using SimpleCommerce.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCommerce.Services
{
    public interface IProductClient
    {
        List<ProductDto> GetAllProducts(string access_token);
    }
}
