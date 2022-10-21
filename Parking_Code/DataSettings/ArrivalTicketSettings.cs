using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Parking_Code.Models;

namespace Parking_Code.DataSettings
{
    public class ArrivalTicketSettings
    {
        private readonly IMongoCollection<ArrivalTicket> collection;

        public ArrivalTicketSettings(IOptions<DatabaseSettings> DatabaseSetting)
        {
            var mongoClient = new MongoClient(DatabaseSetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(DatabaseSetting.Value.DatabaseName);
            collection = mongoDatabase.GetCollection<ArrivalTicket>(DatabaseSetting.Value.ArrivalTicketCollection);
        }
        public async Task<List<ArrivalTicket>> GetTicketAsync() => await collection.Find(x => true).ToListAsync();

        public async Task<ArrivalTicket?> GetTicketAsync(string id) => await collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateTicketAsync(ArrivalTicket ticket) => await collection.InsertOneAsync(ticket);
        public async Task UpdateTicketAsync(string id, ArrivalTicket ticket) => await collection.ReplaceOneAsync(x => x.Id == id, ticket);
        public async Task DeleteTicketAsync(string id) => await collection.DeleteOneAsync(x => x.Id == id);
    }
}
