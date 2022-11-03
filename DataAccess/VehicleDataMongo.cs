using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Study.Parking.Models;

namespace Study.Parking.DataAccess
{
    public class VehicleDataMongo : IDataAccessMongo<Vehicle>
    {

        private readonly IMongoCollection<Vehicle> _collection;
        public VehicleDataMongo(IOptions<DataBaseSettings> config)
        {
            IMongoClient cmd = new MongoClient(config.Value.ConnectionString);
            IMongoDatabase db = cmd.GetDatabase(config.Value.DatabaseName);
            _collection = db.GetCollection<Vehicle>(config.Value.VehicleCollection);
        }

        public async Task Delete(string Id) => await _collection.DeleteOneAsync(x=>x.Id == Id);

        public async Task<List<Vehicle>> Get() => await _collection.Find(x=>true).ToListAsync();

        public async Task<Vehicle> Get(string Id) => await _collection.Find(x=>x.Id == Id).FirstOrDefaultAsync();

        public async Task Post(Vehicle model) => await _collection.InsertOneAsync(model);

        public async Task<Vehicle> Put(string Id, Vehicle model)
        {
            var Vehicle = await _collection.Find(x=>x.Id == Id).FirstOrDefaultAsync();

            if(Vehicle == null) return null;

            await _collection.ReplaceOneAsync(x=>x.Id == Id, model);
            return model;
        }
    }
}