using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking_Code.DataSettings;
using Parking_Code.Models;
using Parking_Code.View;

namespace Parking_Code.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartureTicketController : ControllerBase
    {
        private readonly ArrivalTicketSettings ArrivalTicketDataAccess;
        private readonly DepartureTicketSettings DepartureTicketDataAccess;
        private readonly PriceSettings PriceDataAccess;
        public DepartureTicketController(DepartureTicketSettings DepartureTicketDataAccess,
                                    ArrivalTicketSettings ArrivalTicketDataAcess,
                                    PriceSettings priceDataAcess)
        {
            this.ArrivalTicketDataAccess = ArrivalTicketDataAcess;
            this.DepartureTicketDataAccess = DepartureTicketDataAccess;
            this.PriceDataAccess = priceDataAcess;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await DepartureTicketDataAccess.GetTicketsAsync();
            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var model = await DepartureTicketDataAccess.GetTicketAsync(id);
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post(DepartureTicketInput exitTicketInput)
        {
            var enterTicket = await ArrivalTicketDataAccess.GetTicketAsync(exitTicketInput.ArrivalTicketId);
            if (enterTicket == null)
                return NotFound("Arrival Ticket not find");

            var Price = await PriceDataAccess.GetPriceTypeAsync(enterTicket.Vehicle.Type);
            if (Price == null)
                return NotFound("Price not find");

            DepartureTicket model = new DepartureTicket(enterTicket, Price);
            await DepartureTicketDataAccess.CreateTicketAsync(model);
            return Ok(exitTicketInput);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] DepartureTicketInput exitTicketInput)
        {
            var enterTicket = await ArrivalTicketDataAccess.GetTicketAsync(exitTicketInput.ArrivalTicketId);
            if (enterTicket == null)
                return NotFound("Arrival Ticket not find");

            var Price = await PriceDataAccess.GetPriceTypeAsync(enterTicket.Vehicle.Type);
            if (Price == null)
                return NotFound("Price not find");

            var model = new DepartureTicket(enterTicket, Price);

            await DepartureTicketDataAccess.UpdateTicketAsync(id, model);
            return Ok(model);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var model = DepartureTicketDataAccess.GetTicketAsync(id);
            if (model == null)
                return NotFound("Departure ticket dont exist");

            await DepartureTicketDataAccess.DeleteTicketAsync(id);
            return Ok(model);
        }
    }
}
