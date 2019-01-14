using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Core
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public Person Person { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}
