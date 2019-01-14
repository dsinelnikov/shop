using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Dto.Products
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public ProductDto Product { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}
