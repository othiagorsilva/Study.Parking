using Parking_Code.Models;
using System.ComponentModel.DataAnnotations;

namespace Parking_Code.View
{
    public class PriceInput
    {
        [Required]
        public VehicleType Type { get; set; }

        [Required,Range(0, double.MaxValue)]
        public double Inicial_Price { get; set; }

        [Required, Range(0, double.MaxValue)]
        public double Adicional_price { get; set; }

        [Required, Range(0, double.MaxValue)]
        public double Day_price { get; set; }

    }
}
