using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Dto.Products
{
    public class NewOrderDto
    {
        public int PersonId { get; set; }
        public List<NewOrderItemDto> Items { get; set; }
    }
}
