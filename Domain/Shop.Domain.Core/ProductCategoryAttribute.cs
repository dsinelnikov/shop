using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Core
{
    public class ProductCategoryAttribute
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public int AttributeId { get; set; }
        public Attribute Attribute { get; set; }
    }
}
