using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Study.Parking.Models;

namespace Study.Parking.DataAccess
{
    public class TicketDataMongo : ITicketDataMongo
    {
        private readonly IMongoCollection<Ticket> _collection;
        private readonly IMongoCollection<PriceTable> _collectionPriceTables;
        public TicketDataMongo(IOptions<DataBaseSettings> config)
        {
            IMongoClient cmd = new MongoClient(config.Value.ConnectionString);
            IMongoDatabase db = cmd.GetDatabase(config.Value.DatabaseName);
            _collection = db.GetCollection<Ticket>(config.Value.TicketCollection);
            _collectionPriceTables = db.GetCollection<PriceTable>(config.Value.PriceTableCollection);
        }

        public async Task Delete(string Id) => await _collection.DeleteOneAsync(x=>x.Id == Id);

        public async Task<List<Ticket>> Get() => await _collection.Find(x=>true).ToListAsync();

        public async Task<Ticket> Get(string Id) => await _collection.Find(x=>x.Id == Id).FirstOrDefaultAsync();

        public async Task Post(Ticket model) => await _collection.InsertOneAsync(model);

        public async Task<Ticket> Put(string Id, Ticket model)
        {
            var Ticket = await _collection.Find(x=>x.Id == Id).FirstOrDefaultAsync();

            if(Ticket == null) return null;

            await _collection.ReplaceOneAsync(x=>x.Id == Id, model);
            return model;
        }

        public async Task<Ticket> Patch(string Id, Ticket model)
        {
            var ticket = await _collection.Find(x=>x.Id == Id).FirstOrDefaultAsync();

            if(ticket == null)
            return null;

            var update = Builders<Ticket>.Update.Set(x=>x.TimeOut, model.TimeOut);
            await _collection.UpdateOneAsync(x=>x.Id == Id, update);

            return model;
        }
    }
}