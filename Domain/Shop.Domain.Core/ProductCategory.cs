using System.Collections.Generic;

namespace Shop.Domain.Core
{
    public class ProductCategory
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public ProductCategory Parent { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
