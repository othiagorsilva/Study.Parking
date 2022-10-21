using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Study.Parking.Models
{
    public class Vehicle
    {
        private string _kind;
        public Vehicle(string Licensce, string Model, string ConductorName, int Kind)
        {
            this.LicensePlate = Licensce;
            this.Model = Model;
            this.ConductorName = ConductorName;
            this.Kind = Kind.ToString();
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get;set;}
        public string? Chassis {get;set;}
        public string LicensePlate {get;set;}
        public string ConductorName {get;set;}
        public string Model {get;set;}
        public string Kind
        {
            get
            {
                return _kind;
            }
            private set
            {
                if(int.TryParse(value, out int number))
                {
                    switch(number)
                    {
                        case 1:
                            _kind = "Car";
                            break;
                        case 2:
                            _kind = "Motorcycle";
                            break;
                        case 3:
                            _kind = "Truck";
                            break;
                        case 4:
                            _kind = "Bus";
                            break;
                    }
                }
                else
                {
                    _kind = value;
                }


                
            }
        }
    }
}