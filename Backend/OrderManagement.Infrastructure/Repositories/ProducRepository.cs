using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Data;

namespace OrderManagement.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocorreu um erro ao buscar todos os produtos: .", ex);
            }
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            try
            {
                return await _context.Products.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Ocorreu um erro ao buscar o produto com ID {id}.", ex);
            }
        }

        public async Task AddProductAsync(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocorreu um erro ao adicionar o produto.", ex);
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Ocorreu um erro ao atualizar o produto com ID {product.Id}.", ex);
            }
        }

        public async Task DeleteProductAsync(Guid id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Ocorreu um erro ao deletar o produto com ID {id}.", ex);
            }
        }
    }

}
