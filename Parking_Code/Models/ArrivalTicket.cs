using MongoDB.Bson;

namespace Parking_Code.Models
{
    public class ArrivalTicket
    {
        public ArrivalTicket(Vehicle vehicle)
        {
            Id = ObjectId.GenerateNewId().ToString();

            Vehicle = vehicle;
            Arrival_Time = DateTime.Now;
        }
        public string Id { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime Arrival_Time { get; set; }
    }
}
    

