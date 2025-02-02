using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Data;

namespace OrderManagement.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly MongoDbContext _mongoContext;

        public OrderRepository(AppDbContext context, MongoDbContext mongoContext)
        {
            _context = context;
            _mongoContext = mongoContext;
        }

        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            try
            {
                return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id) ?? throw new InvalidOperationException("Pedido não encontrada");
            }
            catch (Exception ex)
            {                
                throw new ApplicationException("Ocorreu um erro ao obter pedido: ", ex);
            }
        }

        public async Task AddOrderAsync(Order order)
        {
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocorreu um erro ao adicionar pedido: ", ex);
            }
        }

        public async Task<Order> GetOrderDatailsByIdAsync(Guid id)
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefaultAsync(o => o.Id == id) ?? throw new InvalidOperationException("Pedido não encontrado");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocorreu um erro ao bucar itens do pedido: ", ex);
            }
        }

        public async Task UpdateOrderAsync(Order order)
        {
            try
            {
                var existingOrder = await _context.Orders.FindAsync(order.Id);
                if (existingOrder != null)
                {
                    _context.Entry(existingOrder).CurrentValues.SetValues(order);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocorreu um erro ao atualizar o pedido", ex);
            }
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            try
            {
                var order = await _context.Orders.FindAsync(orderId);
                if (order != null)
                {
                    _context.Orders.Remove(order);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocorreu um erro ao deletar o pedido: ", ex);
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {
                return await _context.Orders.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocorreu um erro ao buscar todos os pedidos: ", ex);
            }
        }

        public async Task UpdateOrderStatus(Order order)
        {
            try
            {
                var existingOrder = await _context.Orders.FindAsync(order.Id);
                if (existingOrder != null)
                {
                    existingOrder.Status = order.Status;
                    _context.Orders.Update(existingOrder);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new InvalidOperationException("Pedido não encontrado");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocorreu um erro ao atualizar o status do pedido: ", ex);
            }
        }
    }

}
