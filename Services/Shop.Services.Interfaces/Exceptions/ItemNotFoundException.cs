using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Interfaces.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public ItemNotFoundException(string message)
            : base(message)
        {

        }

        public ItemNotFoundException(int id, string itemName)
            : base($"{itemName} with Id = {id} is not found.")
        {

        }

        public ItemNotFoundException(int id, string itemName, Exception innerException)
            : base($"{itemName} with Id = {id} is not found.", innerException)
        {

        }
    }
}
