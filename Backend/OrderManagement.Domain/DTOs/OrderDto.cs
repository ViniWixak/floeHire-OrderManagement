using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.DTOs
{
    public class OrderDto
    {
        public decimal TotalAmount { get; set; }
        public List<OrderProductDto> Products { get; set; }
    }

}
