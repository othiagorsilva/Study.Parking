using System.ComponentModel.DataAnnotations;

namespace Study.Parking.ViewModels
{
    public class PriceTableView
    {
        [Range(1 , 24, ErrorMessage = "The Hour need be between 1 and 24")]
        public int Hours {get; set;}
        [Range(0.01, 99999999999, ErrorMessage ="The price need be more than zero")]
        public decimal Price {get; set;}

    }
}