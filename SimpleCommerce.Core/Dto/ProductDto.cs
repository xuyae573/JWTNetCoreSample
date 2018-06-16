using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCommerce.Core.Dto
{
    public class ProductDto
    {
     
        public string Name { get; set; }

        public string ShortDescription { get; set; }
 
        public string Sku { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdatedOnUtc { get; set; }

        public string CreateBy { get; set; }

        public string UpdateBy { get; set; }

    }
}
