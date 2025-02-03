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
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly AppDbContext _context;
        private readonly MongoDbContext _mongoContext;

        public OrderItemRepository(AppDbContext context, MongoDbContext mongoContext)
        {
            _context = context;
            _mongoContext = mongoContext;
        }

        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            try
            {
                _context.OrderItems.Add(orderItem);
                await _context.SaveChangesAsync();

                var order = await _context.Orders.FindAsync(orderItem.OrderId);
                if (order != null)
                {
                    order.TotalAmount += orderItem.TotalPrice;
                    _context.Orders.Update(order);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao adicionar o item do pedido.", ex);
            }
        }

        public async Task DeleteOrderItemAsync(Guid id)
        {
            try
            {
                var orderItem = await _context.OrderItems.FindAsync(id);
                if (orderItem != null)
                {
                    var order = await _context.Orders.FindAsync(orderItem.OrderId);
                    if (order != null)
                    {
                        order.TotalAmount -= orderItem.TotalPrice;
                        _context.Orders.Update(order);
                    }

                    _context.OrderItems.Remove(orderItem);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao deletar o item do pedido.", ex);
            }
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(Guid orderId)
        {
            try
            {
                return await _context.OrderItems.Where(oi => oi.OrderId == orderId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao obter os itens do pedido.", ex);
            }
        }
    }
}
