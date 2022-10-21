using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Parking_Code.Models;

namespace Parking_Code.DataSettings
{
    public class VehicleSettings
    {
        private readonly IMongoCollection<Vehicle> collection;

        public VehicleSettings(IOptions<DatabaseSettings> DatabaseSetting)
        {
            var mongoClient = new MongoClient(DatabaseSetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(DatabaseSetting.Value.DatabaseName);
            collection = mongoDatabase.GetCollection<Vehicle>(DatabaseSetting.Value.VehicleCollection);
        }
        public async Task<List<Vehicle>> GetVehiclesAsync() => await collection.Find(x => true).ToListAsync();

        public async Task<Vehicle?> GetVehicleAsync(string id) => await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task<Vehicle?> GetVehiclePlateAsync(string vehiclePlate) => await collection.Find(x => x.LicensePlate == vehiclePlate).FirstOrDefaultAsync();
        public async Task CreateVehicleAsync(Vehicle vehicle) => await collection.InsertOneAsync(vehicle);
        public async Task UpdateVehicleAsync(string id, Vehicle vehicle) => await collection.ReplaceOneAsync(x => x.Id == id, vehicle);
        public async Task DeleteVehicleAsync(string id) => await collection.DeleteOneAsync(x => x.Id == id);
    }
}
