using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Dto.Products
{
    public class EditBrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int RegistrationCountryId { get; set; }
    }
}
