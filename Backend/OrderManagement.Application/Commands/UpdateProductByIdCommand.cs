using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Commands
{
    public class UpdateProductByIdCommand : IRequest<Product>
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public UpdateProductByIdCommand(Product product)
        {
            ProductId = product.Id;
            Name = product.Name;
            Price = product.Price;
        }
    }
}
