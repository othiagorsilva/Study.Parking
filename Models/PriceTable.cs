using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
namespace Study.Parking.Models
{
    public class PriceTable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get; set;}
        public int Hours {get; set;}
        public decimal Price {get; set;}
        public PriceTable(int Hours, decimal Price)
        {
            this.Hours = Hours;
            this.Price = Price;
        }
    }
}