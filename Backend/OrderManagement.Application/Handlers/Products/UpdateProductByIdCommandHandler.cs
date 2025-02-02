using MediatR;
using OrderManagement.Application.Commands;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Handlers.Products
{
    public class UpdateProductByIdCommandHandler : IRequestHandler<UpdateProductByIdCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductByIdCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(UpdateProductByIdCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.ProductId);
            if (product == null)
            {
                return null;
            }

            product.Name = request.Name;
            product.Price = request.Price;

            await _productRepository.UpdateProductAsync(product);
            return product;
        }
    }

}
