using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Interfaces.Exceptions
{
    public class InvalidItemException : Exception
    {
        public object Item { get;  }

        public InvalidItemException(object item)
            :this(item, null)
        {            
        }

        public InvalidItemException(object item, Exception innerException)
            : base("Item has invalid data. Maybe some ralations are wrong.")
        {
            Item = item;
        }
    }
}
