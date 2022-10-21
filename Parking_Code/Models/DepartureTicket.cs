using MongoDB.Bson;
using MongoDB.Driver;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Parking_Code.Models
{
    public class DepartureTicket
    {
        public DepartureTicket(ArrivalTicket arrivalTicket, Price price)
        {
            Id = ObjectId.GenerateNewId().ToString();

            Arrival_Ticket = arrivalTicket;
            Departure_Time = DateTime.UtcNow;
            
            var permanenceHours = (DateTime.UtcNow - arrivalTicket.Arrival_Time).Hours;
            var permaneceDays = (DateTime.UtcNow - arrivalTicket.Arrival_Time).Days;

            Permanence_Days = permaneceDays;
            Permanence_hours = permanenceHours;

           
            if (permaneceDays == 1)
                Final_Price = price.Day_price;

            if (permanenceHours < 1)
                Final_Price = price.Inicial_price;

            if (permanenceHours > 1)
                Final_Price = price.Inicial_price + ((permanenceHours - 1) * price.Adicional_price);
        }

        public string Id { get; set; }
        public DateTime Departure_Time{ get; set; }
        public ArrivalTicket Arrival_Ticket { get; set; }
        public double Permanence_Days { get; set; }
        public double Permanence_hours { get; set; }
        public double Final_Price { get; set; }
    }
}
