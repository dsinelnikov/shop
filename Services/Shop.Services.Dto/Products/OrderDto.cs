using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Dto.Products
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public List<OrderItemDto> Items {get;set;}
    }
}
