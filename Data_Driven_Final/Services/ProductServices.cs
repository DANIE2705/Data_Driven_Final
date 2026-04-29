using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Data_Driven_Final.Models;

namespace Data_Driven_Final.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IOptions<MongoDBSettings> settings)
        {
            var mongo = settings.Value;

            var client = new MongoClient(mongo.ConnectionString);
            var database = client.GetDatabase(mongo.DatabaseName);

            _products = database.GetCollection<Product>(mongo.ProductsCollectionName);
        }

        // CREATE
        public async Task CreateAsync(Product product) =>
            await _products.InsertOneAsync(product);

        // READ ALL
        public async Task<List<Product>> GetAllAsync() =>
            await _products.Find(_ => true).ToListAsync();

        // READ BY ID
        public async Task<Product> GetByIdAsync(string id) =>
            await _products.Find(p => p.Id == id).FirstOrDefaultAsync();

        // UPDATE
        public async Task UpdateAsync(string id, Product updatedProduct) =>
            await _products.ReplaceOneAsync(p => p.Id == id, updatedProduct);

        // DELETE
        public async Task DeleteAsync(string id) =>
            await _products.DeleteOneAsync(p => p.Id == id);
    }
}