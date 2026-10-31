
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehicles()
        {
            var vehicles = await _vehicleService.GetAllAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDto>> GetVehicle(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);

            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehiclesByCustomer(int customerId)
        {
            var vehicles = await _vehicleService.GetByCustomerIdAsync(customerId);
            return Ok(vehicles);
        }

        [HttpPost]
        public async Task<ActionResult<VehicleDto>> CreateVehicle(CreateVehicleDto vehicleDto)
        {
            try
            {
                var createdVehicle = await _vehicleService.CreateAsync(vehicleDto);

                return CreatedAtAction(
                    nameof(GetVehicle),
                    new { id = createdVehicle.Id },
                    createdVehicle
                );
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, UpdateVehicleDto vehicleDto)
        {
            try
            {
                var updatedVehicle = await _vehicleService.UpdateAsync(id, vehicleDto);

                if (updatedVehicle == null)
                    return NotFound();

                return Ok(updatedVehicle);
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            try
            {
                var result = await _vehicleService.DeleteAsync(id);

                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (BusinessRuleException ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }
    }
}
