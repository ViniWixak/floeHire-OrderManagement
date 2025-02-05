using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OrderManagement.Domain.Models.MongoModel
{
    public class OrderMongoModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } // ID do MongoDB

        [BsonRepresentation(BsonType.String)]
        public Guid OrderId { get; set; } // ID original do pedido no SQL Server

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = string.Empty;

        public List<OrderItemMongoModel> OrderItems { get; set; } = new();
    }
}
