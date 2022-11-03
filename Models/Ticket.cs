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
        public decimal Price {get;set;}
        public DateTime? TimeOut {get;set;}
        public bool Active {get;set;}

        public Ticket(DateTime TimeIn, List<PriceTable> Prices, Vehicle Vehicle)
        {
            this.Vehicle = Vehicle;
            this.TimeIn = TimeIn;
            this.Permanence = Convert.ToInt32(DateTime.UtcNow.Subtract(TimeIn).TotalMinutes);

            var maxHr = Prices.Max(x=>x.Hours)*60;
            var maxPr = Prices.Max(x=>x.Price);

            if(this.Permanence > maxHr)
                this.Price = (this.Permanence - maxHr) * (decimal)0.015 * maxPr;
            else
                this.Price = Prices.Where(x => Permanence   <=  (x.Hours * 60)).FirstOrDefault()!.Price;
            
            this.Active = true;
        }
    }
}