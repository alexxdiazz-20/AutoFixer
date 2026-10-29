using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TallerAutomotriz.Application.DTOs;
using TallerAutomotriz.Application.Exceptions;
using TallerAutomotriz.Application.Interfaces;

namespace TallerAutomotriz.Presentation.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesApiController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesApiController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        // GET: api/VehiclesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehicles()
        {
            var vehicles = await _vehicleService.GetAllAsync();
            return Ok(vehicles);
        }

        // GET: api/VehiclesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDto>> GetVehicle(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        // GET: api/VehiclesApi/customer/5
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehiclesByCustomer(int customerId)
        {
            var vehicles = await _vehicleService.GetByCustomerIdAsync(customerId);
            return Ok(vehicles);
        }

        // POST: api/VehiclesApi
        [HttpPost]
        public async Task<ActionResult<VehicleDto>> CreateVehicle(CreateVehicleDto vehicleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdVehicle = await _vehicleService.CreateAsync(vehicleDto);

            return CreatedAtAction(nameof(GetVehicle), new { id = createdVehicle.Id }, createdVehicle);
        }

        // PUT: api/VehiclesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, UpdateVehicleDto vehicleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedVehicle = await _vehicleService.UpdateAsync(id, vehicleDto);

            if (updatedVehicle == null)
            {
                return NotFound();
            }

            return Ok(updatedVehicle);
        }

        // DELETE: api/VehiclesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            try
            {
                var result = await _vehicleService.DeleteAsync(id);

                if (!result)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (BusinessRuleException ex)
            {
                // 409: el registro existe pero tiene datos que dependen de él
                return Conflict(new { error = ex.Message });
            }
        }
    }
}