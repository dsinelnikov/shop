using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Dto.Products
{
    public class EditProductDto
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public double Price { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int ManufactureCountryId { get; set; }
    }
}
