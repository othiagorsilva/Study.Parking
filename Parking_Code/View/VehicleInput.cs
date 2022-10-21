using Parking_Code.Models;
using System.ComponentModel.DataAnnotations;

namespace Parking_Code.View
{
    public class VehicleInput
    {
        [Required]
        public VehicleType Type { get; set; }
          
        [Required(ErrorMessage = "Must be a valid brand")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Must contain a color")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Must be a valid model")]
        public string Model { get; set; }

        [Required, StringLength(7, MinimumLength = 7,
            ErrorMessage = "License Plate must contain 7 Characters")]
        public string LicensePlate { get; set; }
    }
}
