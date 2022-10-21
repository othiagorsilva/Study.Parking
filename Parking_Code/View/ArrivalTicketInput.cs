using System.ComponentModel.DataAnnotations;

namespace Parking_Code.View
{
    public class ArrivalTicketInput
    {
        [Required(ErrorMessage = "Must contain a valid vehicle")]
        public string VehicleId{ get; set; }
    }
}
