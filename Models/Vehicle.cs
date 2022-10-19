using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Study.Parking.Models
{
    public class Vehicle
    {
        public Vehicle(string Licensce, string Model)
        {
            this.LicensePlate = Licensce;
            this.Model = Model;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get;set;}
        public string Chassis {get;set;}
        public string LicensePlate {get;set;}
        public string ConductorName {get;set;}//Model Conductor
        public string Model {get;set;}
        public Dictionary<int, string>? Kind {get; set;}

    }
}