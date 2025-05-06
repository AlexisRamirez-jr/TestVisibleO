using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.DTOs
{
    public class UpdatePriceDto
    {
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
    }
}
