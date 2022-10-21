using MongoDB.Bson;
using System;
using System.Data;

namespace Parking_Code.Models
{
    public class Vehicle
    {
        public Vehicle(VehicleType type, string brand, string model, string color, string licensePlate)
        {
            Id = ObjectId.GenerateNewId().ToString();

            Type = type;
            LicensePlate = licensePlate;
            Brand = brand;
            Model = model;
            Color = color;
        }
        public string Id { get; set; }
        public string LicensePlate { get; set; }
        public VehicleType Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        

    }
}
