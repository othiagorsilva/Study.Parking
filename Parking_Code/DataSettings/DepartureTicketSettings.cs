using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Parking_Code.Models;

namespace Parking_Code.DataSettings
{
    public class DepartureTicketSettings
    {
        private readonly IMongoCollection<DepartureTicket> collection;

        public DepartureTicketSettings(IOptions<DatabaseSettings> DatabaseSetting)
        {
            var mongoClient = new MongoClient(DatabaseSetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(DatabaseSetting.Value.DatabaseName);
            collection = mongoDatabase.GetCollection<DepartureTicket>(DatabaseSetting.Value.DepartureTicketCollection);
        }
        public async Task<List<DepartureTicket>> GetTicketsAsync() => await collection.Find(x => true).ToListAsync();

        public async Task<DepartureTicket?> GetTicketAsync(string id) => await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateTicketAsync(DepartureTicket ticket) => await collection.InsertOneAsync(ticket);
        public async Task UpdateTicketAsync(string id, DepartureTicket ticket) => await collection.ReplaceOneAsync(x => x.Id == id, ticket);
        public async Task DeleteTicketAsync(string id) => await collection.DeleteOneAsync(x => x.Id == id);
    }
}

