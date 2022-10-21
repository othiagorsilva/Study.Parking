using Microsoft.AspNetCore.Mvc;
using Parking_Code.DataSettings;
using Parking_Code.Models;
using Parking_Code.View;

namespace Parking_Code.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly VehicleSettings vehicleDataAccess;

        public VehicleController(VehicleSettings vehicleDataAccess)
        {
            this.vehicleDataAccess = vehicleDataAccess;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await vehicleDataAccess.GetVehiclesAsync();

            return Ok(model);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var model = await vehicleDataAccess.GetVehicleAsync(id);

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VehicleInput vehicleInputModel)
        {
            var LicensePlate = await vehicleDataAccess.GetVehiclePlateAsync(vehicleInputModel.LicensePlate);
            if (LicensePlate != null)
                return NotFound("License Plate already exists");

            var vehicle = new Vehicle(vehicleInputModel.Type,
                                      vehicleInputModel.Brand,
                                      vehicleInputModel.Model,
                                      vehicleInputModel.Color,
                                      vehicleInputModel.LicensePlate);

            await vehicleDataAccess.CreateVehicleAsync(vehicle);
            return Ok(vehicleInputModel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] VehicleInput vehicleInput)
        {
            var vehicle = await vehicleDataAccess.GetVehicleAsync(id);
            if (vehicle == null)
                return NotFound("vehicle not find");

            var model = new Vehicle(vehicleInput.Type,
                                      vehicleInput.Brand,
                                      vehicleInput.Model,
                                      vehicleInput.Color,
                                      vehicleInput.LicensePlate);
            model.Id = id;
            await vehicleDataAccess.UpdateVehicleAsync(id, model);
            return Ok(model);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var model = vehicleDataAccess.GetVehicleAsync(id);
            if (model == null)
                return NotFound("vehicle not find");

            await vehicleDataAccess.DeleteVehicleAsync(id);
            return NoContent();
        }
    }
}
