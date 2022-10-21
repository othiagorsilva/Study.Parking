using System.ComponentModel.DataAnnotations;

namespace Study.Parking.ViewModels
{
    public class TicketView
    {
        [Required(ErrorMessage = "Id of Vehicle is required")]
        public string IdVehicle {get;set;}
    }
}