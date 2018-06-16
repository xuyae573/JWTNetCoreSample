using SimpleCommerce.Core.Dto;
using System;
using System.Collections.Generic;

namespace ProductAPI.Service
{
    public class ProductService : IProductService
    {
        public List<ProductDto> GetProducts()
        {
            return new List<ProductDto>()
           {
               new ProductDto()
               {
                   Name = "apple-macbook-pro-13-inch",
                   ShortDescription="A groundbreaking Retina display. A new force-sensing trackpad. All-flash architecture. Powerful dual-core and quad-core Intel processors. Together, these features take the notebook to a new level of performance. And they will do the same for you in everything you create.",
                   Price = 1800,
                   Sku = "AE_00001",
                   CreateBy = "Arthur",
                   CreatedOnUtc = new DateTime(1,6,2018),
                   UpdatedOnUtc = new DateTime(2,6,2018),
                   UpdateBy = "John",
               },
              new ProductDto()
               {
                   Name = "Apple iPhone 7 Plus 256GB Red Special Edition",
                   ShortDescription="Laptop Asus N551JK Intel Core i7-4710HQ 2.5 GHz, RAM 16GB, HDD 1TB, Video NVidia GTX 850M 4GB, BluRay, 15.6, Full HD, Win 8.1.",
                   Price = 1800,
                   Sku = "AS_551_LP",
                   CreateBy = "Arthur",
                   CreatedOnUtc = new DateTime(1,6,2018),
                   UpdatedOnUtc = new DateTime(2,6,2018),
                   UpdateBy = "John",
               },
           };
        }
    }
}
