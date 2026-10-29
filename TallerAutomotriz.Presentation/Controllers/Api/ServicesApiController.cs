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
    public class ServicesApiController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServicesApiController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        // GET: api/ServicesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServices()
        {
            var services = await _serviceService.GetAllAsync();
            return Ok(services);
        }

        // GET: api/ServicesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetService(int id)
        {
            var service = await _serviceService.GetByIdAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        // POST: api/ServicesApi
        [HttpPost]
        public async Task<ActionResult<ServiceDto>> CreateService(CreateServiceDto serviceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdService = await _serviceService.CreateAsync(serviceDto);

            return CreatedAtAction(nameof(GetService), new { id = createdService.Id }, createdService);
        }

        // PUT: api/ServicesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, UpdateServiceDto serviceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedService = await _serviceService.UpdateAsync(id, serviceDto);

            if (updatedService == null)
            {
                return NotFound();
            }

            return Ok(updatedService);
        }

        // DELETE: api/ServicesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            try
            {
                var result = await _serviceService.DeleteAsync(id);

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