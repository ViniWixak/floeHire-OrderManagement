using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.ReadModel;

namespace OrderManagement.Domain.Interfaces
{
    public interface IOrderWriteRepository
    {
        Task InsertOrderAsync(OrderMongoModel order);
        Task UpdateOrderAsync(OrderMongoModel order);
        Task DeleteOrderAsync(Guid orderId);
    }

}
