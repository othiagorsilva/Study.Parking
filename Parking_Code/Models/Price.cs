using MongoDB.Bson;
using Parking_Code.Models;
using Parking_Code.View;
using System.Runtime.ConstrainedExecution;

namespace Parking_Code.Models
{
    public class Price
    {
        public Price(VehicleType type, double inicialPrice, double adicional_price, double day_price)
        {
            Id = ObjectId.GenerateNewId().ToString();

            Type = type;
            Inicial_price = inicialPrice;
            Adicional_price = adicional_price;
            Day_price = day_price;
        }
        public string Id { get; set; }
        public VehicleType Type { get; set; }
        public double Inicial_price { get; set; }
        public double Adicional_price { get; set; }
        public double Day_price { get; set; }
    }
}
