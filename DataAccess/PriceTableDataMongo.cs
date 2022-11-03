using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Study.Parking.Models;

namespace Study.Parking.DataAccess
{
    public class PriceTableDataMongo : IDataAccessMongo<PriceTable>
    {
        private readonly IMongoCollection<PriceTable> _collection;
        public PriceTableDataMongo(IOptions<DataBaseSettings> config)
        {
            IMongoClient cmd = new MongoClient(config.Value.ConnectionString);
            IMongoDatabase db = cmd.GetDatabase(config.Value.DatabaseName);
            _collection = db.GetCollection<PriceTable>(config.Value.PriceTableCollection);
        }

        public async Task Delete(string Id) => await _collection.DeleteOneAsync(x=>x.Id == Id);

        public async Task<List<PriceTable>> Get() => await _collection.Find(x=>true).ToListAsync();

        public async Task<PriceTable> Get(string Id) => await _collection.Find(x=>x.Id == Id).FirstOrDefaultAsync();

        public async Task Post(PriceTable model) => await _collection.InsertOneAsync(model);

        public async Task<PriceTable> Put(string Id, PriceTable model)
        {
            var priceTable = await _collection.Find(x=>x.Id == Id).FirstOrDefaultAsync();

            if(priceTable == null) return null;

            await _collection.ReplaceOneAsync(x=>x.Id == Id, model);
            return model;
        }
    }
}