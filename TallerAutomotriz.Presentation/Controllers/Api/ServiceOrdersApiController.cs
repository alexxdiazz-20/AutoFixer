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
    public class ServiceOrdersApiController : ControllerBase
    {
        private readonly IServiceOrderService _serviceOrderService;

        public ServiceOrdersApiController(IServiceOrderService serviceOrderService)
        {
            _serviceOrderService = serviceOrderService;
        }

        // GET: api/ServiceOrdersApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceOrderDto>>> GetServiceOrders()
        {
            var serviceOrders = await _serviceOrderService.GetAllAsync();
            return Ok(serviceOrders);
        }

        // GET: api/ServiceOrdersApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceOrderDto>> GetServiceOrder(int id)
        {
            var serviceOrder = await _serviceOrderService.GetByIdAsync(id);

            if (serviceOrder == null)
            {
                return NotFound();
            }

            return Ok(serviceOrder);
        }

        // GET: api/ServiceOrdersApi/vehicle/5
        [HttpGet("vehicle/{vehicleId}")]
        public async Task<ActionResult<IEnumerable<ServiceOrderDto>>> GetServiceOrdersByVehicle(int vehicleId)
        {
            var serviceOrders = await _serviceOrderService.GetByVehicleIdAsync(vehicleId);
            return Ok(serviceOrders);
        }

        // GET: api/ServiceOrdersApi/mechanic/5
        [HttpGet("mechanic/{mechanicId}")]
        public async Task<ActionResult<IEnumerable<ServiceOrderDto>>> GetServiceOrdersByMechanic(int mechanicId)
        {
            var serviceOrders = await _serviceOrderService.GetByMechanicIdAsync(mechanicId);
            return Ok(serviceOrders);
        }

        // POST: api/ServiceOrdersApi
        [HttpPost]
        public async Task<ActionResult<ServiceOrderDto>> CreateServiceOrder(CreateServiceOrderDto serviceOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdServiceOrder = await _serviceOrderService.CreateAsync(serviceOrderDto);
                return CreatedAtAction(nameof(GetServiceOrder), new { id = createdServiceOrder.Id }, createdServiceOrder);
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // PUT: api/ServiceOrdersApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceOrder(int id, UpdateServiceOrderDto serviceOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedServiceOrder = await _serviceOrderService.UpdateAsync(id, serviceOrderDto);

                if (updatedServiceOrder == null)
                {
                    return NotFound();
                }

                return Ok(updatedServiceOrder);
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE: api/ServiceOrdersApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceOrder(int id)
        {
            var result = await _serviceOrderService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}