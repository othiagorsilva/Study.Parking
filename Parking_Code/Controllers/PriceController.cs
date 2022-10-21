using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking_Code.DataSettings;
using Parking_Code.Models;
using Parking_Code.View;

namespace Parking_Code.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly PriceSettings PriceDataAccess;
        public PriceController(PriceSettings PriceDataAccess)
        {
            this.PriceDataAccess = PriceDataAccess;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Price = await PriceDataAccess.GetPricesAsync();

            return Ok(Price);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var Price = await PriceDataAccess.GetPriceId(id);

            return Ok(Price);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PriceInput PriceInputModel)
        {
            var type = await PriceDataAccess.GetPriceTypeAsync(PriceInputModel.Type);
            if (type != null)
                return NotFound("Price already exists");

            var price = new Price(PriceInputModel.Type, PriceInputModel.Inicial_Price, PriceInputModel.Adicional_price, PriceInputModel.Day_price);
            await PriceDataAccess.CreatePriceAsync(price);

            return Ok(PriceInputModel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] PriceInput PriceInputModel)
        {
            var Price = await PriceDataAccess.GetPriceTypeAsync(PriceInputModel.Type);
            if (Price == null)
                return NotFound("Price dont find");

            var model = new Price(PriceInputModel.Type, PriceInputModel.Inicial_Price, PriceInputModel.Adicional_price, PriceInputModel.Day_price);

            await PriceDataAccess.UpdatePriceAsync(id, model);
            return Ok(model);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var model = PriceDataAccess.GetPriceId(id);
            if (model == null)
                return NotFound("Price dont exist");

            await PriceDataAccess.DeletePriceAsync(id);
            return Ok(model);
        }
    }
}
