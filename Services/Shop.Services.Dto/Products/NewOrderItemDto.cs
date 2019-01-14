using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Dto.Products
{
    public class NewOrderItemDto
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
