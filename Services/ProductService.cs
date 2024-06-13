using DotnetMongoDbApi.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DotnetMongoDbApi.Services;

public class ProcuctService
{
    private readonly IMongoCollection<Product> productCollection;

    public ProcuctService(IOptions<ProductStoreDatabaseSettings> productStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            productStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(productStoreDatabaseSettings.Value.DatabaseName);

        productCollection = mongoDatabase.GetCollection<Product>( productStoreDatabaseSettings.Value.CollectionName);
    }

    public async Task<List<Product>> GetAsync() =>
        await productCollection.Find(_ => true).ToListAsync();

    public async Task<Product?> GetAsync(string id) => await productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Product newProduct) =>
        await productCollection.InsertOneAsync(newProduct);

    public async Task UpdateAsync(string id, Product updatedProduct) =>
        await productCollection.ReplaceOneAsync(x => x.Id == id, updatedProduct);

    public async Task RemoveAsync(string id) =>
        await productCollection.DeleteOneAsync(x => x.Id == id);
}
