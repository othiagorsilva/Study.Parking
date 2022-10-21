using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Parking_Code.Models;

namespace Parking_Code.DataSettings
{
    public class PriceSettings
    {
        private readonly IMongoCollection<Price> collection;

        public PriceSettings(IOptions<DatabaseSettings> DatabaseSetting)
        {
            var mongoClient = new MongoClient(DatabaseSetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(DatabaseSetting.Value.DatabaseName);
            collection = mongoDatabase.GetCollection<Price>(DatabaseSetting.Value.PriceCollection);
        }
        public async Task<List<Price>> GetPricesAsync() => await collection.Find(x => true).ToListAsync();

        public async Task<Price?> GetPriceTypeAsync(VehicleType type) => await collection.Find(x => x.Type == type).FirstOrDefaultAsync();
        public async Task<Price?> GetPriceId(string id) => await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreatePriceAsync(Price price) => await collection.InsertOneAsync(price);
        public async Task UpdatePriceAsync(string id, Price price) => await collection.ReplaceOneAsync(x => x.Id == id, price);
        public async Task DeletePriceAsync(string id) => await collection.DeleteOneAsync(x => x.Id == id);
    }
}
