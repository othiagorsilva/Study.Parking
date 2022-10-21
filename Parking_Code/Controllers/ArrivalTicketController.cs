using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking_Code.DataSettings;
using Parking_Code.Models;
using Parking_Code.View;

namespace Parking_Code.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArrivalTicketController : ControllerBase
    {
        private readonly ArrivalTicketSettings ArrivalTicketDataAccess;
        private readonly VehicleSettings vehicleDataAccess;
        public ArrivalTicketController(ArrivalTicketSettings ArrivalTicketDataAccess, VehicleSettings vehicleDataAccess)
        {
            this.ArrivalTicketDataAccess = ArrivalTicketDataAccess;
            this.vehicleDataAccess = vehicleDataAccess;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ticket = await ArrivalTicketDataAccess.GetTicketAsync();

            return Ok(ticket);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var model = await ArrivalTicketDataAccess.GetTicketAsync(id);
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ArrivalTicketInput TicketInputModel)
        {
            var vehicleId = await vehicleDataAccess.GetVehicleAsync(TicketInputModel.VehicleId);
            if (vehicleId == null)
                return NotFound("Vehicle dont exist");

            var EnterTicket = new ArrivalTicket(vehicleId);
            await ArrivalTicketDataAccess.CreateTicketAsync(EnterTicket);

            return Ok(TicketInputModel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ArrivalTicketInput TicketInputModel)
        {
            var vehicleId = await vehicleDataAccess.GetVehicleAsync(TicketInputModel.VehicleId);
            if (vehicleId == null)
                return NotFound("Vehicle dont exist");

            var model = new ArrivalTicket(vehicleId);


            await ArrivalTicketDataAccess.UpdateTicketAsync(id, model);
            return Ok(model);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var model = ArrivalTicketDataAccess.GetTicketAsync(id);
            if (model == null)
                return NotFound("Arrival ticket dont exist");

            await ArrivalTicketDataAccess.DeleteTicketAsync(id);
            return Ok(model);
        }
    }
}
