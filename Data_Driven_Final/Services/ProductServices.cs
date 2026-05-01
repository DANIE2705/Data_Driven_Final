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
        public async Task<List<Product>> QueryAsync(
    string? category,
    decimal? maxPrice,
    string? sortBy,
    string? sortDirection)
{
    var filter = Builders<Product>.Filter.Empty;

    if (!string.IsNullOrEmpty(category))
        filter &= Builders<Product>.Filter.Eq(p => p.Category, category);

    if (maxPrice.HasValue)
        filter &= Builders<Product>.Filter.Lte(p => p.Price, maxPrice.Value);

    var query = _products.Find(filter);

    if (!string.IsNullOrEmpty(sortBy))
    {
        bool asc = sortDirection?.ToLower() == "asc";

        if (sortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
            query = asc ? query.SortBy(p => p.Price) : query.SortByDescending(p => p.Price);
    }

    return await query.ToListAsync();
}
        public async Task<List<Product>> Query(
            string? category,
            decimal? maxPrice,
            string? sortBy,
            string? sortDirection)
        {
            var filter = Builders<Product>.Filter.Empty;

            if (!string.IsNullOrEmpty(category))
                filter &= Builders<Product>.Filter.Eq(p => p.Category, category);

            if (maxPrice.HasValue)
                filter &= Builders<Product>.Filter.Lte(p => p.Price, maxPrice.Value);

            var query = _products.Find(filter);

            if (!string.IsNullOrEmpty(sortBy))
            {
                bool asc = sortDirection?.ToLower() == "asc";

                if (sortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
                    query = asc ? query.SortBy(p => p.Price) : query.SortByDescending(p => p.Price);
            }

            return await query.ToListAsync();
        }

    }
}