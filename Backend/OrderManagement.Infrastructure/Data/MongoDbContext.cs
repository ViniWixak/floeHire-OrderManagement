using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Infrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoSettings:ConnectionString"]);
            _database = client.GetDatabase("OrderManagementDb");
        }

        public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
    }

}
