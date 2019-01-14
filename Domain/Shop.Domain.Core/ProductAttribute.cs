using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Core
{
    public class ProductAttribute
    {
        public int Id { get; set; }
        public int ProductTypeAttributeId { get; set; }
        public ProductAttribute ProductAttribyte { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public double Value { get; set; }
    }
}
