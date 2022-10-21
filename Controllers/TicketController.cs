using Microsoft.AspNetCore.Mvc;
using Study.Parking.DataAccess;
using Study.Parking.Models;
using Study.Parking.ViewModels;

namespace Study.Parking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly IDataAccessMongo<Ticket> _dataTicket;
        private readonly IDataAccessMongo<Vehicle> _dataVehicle;
        private readonly IDataAccessMongo<PriceTable> _dataPTable;
        public TicketController(IDataAccessMongo<Ticket> DataT, IDataAccessMongo<Vehicle> DataV, IDataAccessMongo<PriceTable> DataPT)
        {
            _dataTicket = DataT;
            _dataVehicle = DataV;
            _dataPTable = DataPT;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await _dataTicket.Get();
            return Ok(model.Where(x=>!x.Active));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(string Id)
        {
            var model = await _dataTicket.Get(Id);

            if(model == null)
            return NotFound();

            if(model.Active)
            {
                List<PriceTable> pTable = await _dataPTable.Get();
                var tckt = new Ticket(model.TimeIn, pTable, model.Vehicle);
                tckt.Id = model.Id;
                return Ok(tckt);
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TicketView Ticket)
        {
            List<PriceTable> PTable = await _dataPTable.Get();
            Vehicle vehicle = await _dataVehicle.Get(Ticket.IdVehicle);
            var model = await _dataTicket.Get();

            if(vehicle == null) return NotFound("Vehicle not found");
            if(model.Where(x=>x.Active && x.Vehicle.Id == Ticket.IdVehicle).FirstOrDefault() != null) return BadRequest("Activated vehicle has already registered");

            Ticket ticket = new Ticket(DateTime.UtcNow, PTable, vehicle);

            await _dataTicket.Post(ticket);
            return CreatedAtAction(nameof(Get), new {Id = ticket.Id}, ticket);
        }

        [HttpPut("CheckOut/{Id}")]
        public async Task<IActionResult> TicketCheckOut(string Id)
        {
            
            List<PriceTable> PTable = await _dataPTable.Get();
            var model = await _dataTicket.Get(Id);
            Vehicle vehicle = await _dataVehicle.Get(model.Vehicle.Id);

            if(model == null) return NotFound("Ticket not found");
            if(vehicle == null) return NotFound("Vehicle not found");


            Ticket ticket = new Ticket(model.TimeIn, PTable, vehicle);
            ticket.TimeOut = ticket.TimeIn.AddMinutes(ticket.Permanence);
            ticket.Id = Id;
            ticket.Active = false;

            model = await _dataTicket.Put(Id,ticket);

            if(model == null) return NotFound();

            return NoContent();
        }


    }
}