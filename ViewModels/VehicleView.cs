using System.ComponentModel.DataAnnotations;

namespace Study.Parking.ViewModels
{
    public class VehicleView
    {
        public string Chassis {get;set;}
        [Required(ErrorMessage = "License Plate is required")]
        public string LicensePlate {get;set;}
        [Required(ErrorMessage = "Name of conductor is required")]
        public string ConductorName {get;set;}//Model Conductor
        [Required(ErrorMessage = "Model of Vehicle is required")]
        public string Model {get;set;}
        [Range(1,4,ErrorMessage = "The value of Type need be between 1 and 4")]
        public int Kind {get;set;}
    }
}