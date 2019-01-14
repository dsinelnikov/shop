using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Core
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }
}
