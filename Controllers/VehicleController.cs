using Microsoft.AspNetCore.Mvc;
using Study.Parking.DataAccess;
using Study.Parking.Models;
using Study.Parking.ViewModels;

namespace Study.Parking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IDataAccessMongo<Vehicle> _data;
        private readonly IDataAccessMongo<PriceTable> _dataPT;
        private readonly IDataAccessMongo<Ticket> _dataTicket;
        public VehicleController(
            IDataAccessMongo<Vehicle> Data,
            IDataAccessMongo<PriceTable> DataPT, 
            IDataAccessMongo<Ticket> DataTicket)
        {
            _data = Data;
            _dataPT = DataPT;
            _dataTicket = DataTicket;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vehicle = await _data.Get();
            return Ok(vehicle);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(string Id)
        {
            var vehicle = await _data.Get(Id);

            if(vehicle == null) return NotFound();

            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VehicleView Vehicle)
        {

            var vehicle = await CreateVehicle(Vehicle);

            if(vehicle == null)
                return BadRequest("Vehicle duplicated");

            return CreatedAtAction(nameof(Get), new {Id = vehicle.Id}, vehicle);

        }

        [HttpPost]
        [Route("TicketCheckIn")]
        public async Task<IActionResult> CreateWithTicket([FromBody] VehicleView Vehicle)
        {
            var vehicle = await CreateVehicle(Vehicle);

            if(vehicle == null)
                return BadRequest("Vehicle duplicated");

            List<PriceTable> PTable = await _dataPT.Get();

            if(PTable == null)
                return BadRequest("No one Price Table registered");

            Ticket ticket = new Ticket(DateTime.UtcNow, PTable, vehicle);

            await _dataTicket.Post(ticket);

            return CreatedAtAction(nameof(Get), new {Id = vehicle.Id}, vehicle);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(string Id, [FromBody]VehicleView Vehicle)
        {

            var vehicle = new Vehicle(Vehicle.LicensePlate,Vehicle.Model, Vehicle.ConductorName, Vehicle.Kind);
            vehicle.Id = Id;
            vehicle.Chassis = Vehicle.Chassis;
            
            var model = await _data.Put(Id, vehicle);

            if(model == null) return NotFound(vehicle);

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            var model = await _data.Get(Id);

            if(model == null) return NotFound();

            await _data.Delete(Id);

            return NoContent();
        }

        async Task<Vehicle> CreateVehicle(VehicleView Vehicle)
        {


            var model = await _data.Get();

            bool cond = model.Where(x=>x.LicensePlate == Vehicle.LicensePlate && x.Model == Vehicle.Model).FirstOrDefault() != null;

            if(cond)
                return null;

            var vehicle = new Vehicle(Vehicle.LicensePlate,Vehicle.Model,Vehicle.ConductorName, Vehicle.Kind);
            vehicle.Chassis = Vehicle.Chassis;

            await _data.Post(vehicle);

            return vehicle;
        }
    }
}