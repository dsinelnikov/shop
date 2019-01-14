using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Dto
{
    public class PaginationDto
    {
        public int Offset { get; set; }
        public int Count { get; set; }

        public static PaginationDto Create(int page, int pageSize, int count)
        {
            return new PaginationDto
            {
                Count = count,
                Offset = page * pageSize
            };
        }
    }
}
