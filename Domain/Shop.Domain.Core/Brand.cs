using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Core
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RegistrationCountryId { get; set; }
        public Country RegistrationCountry { get; set; }
    }
}
