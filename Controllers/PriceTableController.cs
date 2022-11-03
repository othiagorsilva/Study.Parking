using Microsoft.AspNetCore.Mvc;
using Study.Parking.DataAccess;
using Study.Parking.Models;
using Study.Parking.ViewModels;

namespace Study.Parking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceTableController : ControllerBase
    {
        private readonly IDataAccessMongo<PriceTable> _data;
        public PriceTableController (IDataAccessMongo<PriceTable> Data)
        {
            _data = Data;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var priceTable = await _data.Get();
            return Ok(priceTable);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(string Id)
        {
            var priceTable = await _data.Get(Id);

            if(priceTable == null) return NotFound();

            return Ok(priceTable);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PriceTableView PriceTable)
        {
            var priceTable = new PriceTable(PriceTable.Hours,PriceTable.Price);
            await _data.Post(priceTable);
            return CreatedAtAction(nameof(Get), new {Id = priceTable.Id}, PriceTable);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(string Id, [FromBody]PriceTableView PriceTable)
        {
            var priceTable = new PriceTable(PriceTable.Hours,PriceTable.Price);
            priceTable.Id = Id;
            var model = await _data.Put(Id, priceTable);

            if(model == null) return NotFound(priceTable);

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

    }
}