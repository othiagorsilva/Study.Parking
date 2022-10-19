using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Study.Parking.Models
{
    public class Ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get;set;}
        public DateTime TimeIn {get;private set;}
        public int Permanence {get;private set;}
        public Vehicle Vehicle {get;set;}
        public decimal Price {get;private set;}
        public DateTime? TimeOut {get;set;}

        public Ticket(DateTime TimeIn, List<PriceTable> Prices)
        {
            this.TimeIn = TimeIn;
            this.Permanence = DateTime.Now.Subtract(this.TimeIn).Minutes;
            this.Price = Prices.Where(x => Permanence  <= (x.Hours * 60)).FirstOrDefault()!.Price;
            
        }
    }
}