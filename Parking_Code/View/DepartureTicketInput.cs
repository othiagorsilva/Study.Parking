using Microsoft.AspNetCore.Mvc;
using Parking_Code.Models;
using System.ComponentModel.DataAnnotations;

namespace Parking_Code.View
{
    public class DepartureTicketInput
    {
        [Required(ErrorMessage = "Must contain a valid Enter Ticket")]
        public string ArrivalTicketId { get; set; }

    }
}
