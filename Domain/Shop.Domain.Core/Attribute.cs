using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Core
{
    public class Attribute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitId { get; set; }
        public AttributeUnit Unit { get; set; }
    }
}
