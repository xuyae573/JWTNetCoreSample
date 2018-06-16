using SimpleCommerce.Core.Dto;
using System.Collections.Generic;

namespace ProductAPI.Service
{
    public interface IProductService
    {

        List<ProductDto> GetProducts();

    }
}
