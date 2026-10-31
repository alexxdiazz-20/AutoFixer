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
    public class MechanicsApiController : ControllerBase
    {
        private readonly IMechanicService _mechanicService;

        public MechanicsApiController(IMechanicService mechanicService)
        {
            _mechanicService = mechanicService;
        }

        // GET: api/MechanicsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MechanicDto>>> GetMechanics()
        {
            var mechanics = await _mechanicService.GetAllAsync();
            return Ok(mechanics);
        }

        // GET: api/MechanicsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MechanicDto>> GetMechanic(int id)
        {
            var mechanic = await _mechanicService.GetByIdAsync(id);

            if (mechanic == null)
            {
                return NotFound();
            }

            return Ok(mechanic);
        }

        // POST: api/MechanicsApi
        [HttpPost]
        public async Task<ActionResult<MechanicDto>> CreateMechanic(CreateMechanicDto mechanicDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdMechanic = await _mechanicService.CreateAsync(mechanicDto);

            return CreatedAtAction(nameof(GetMechanic), new { id = createdMechanic.Id }, createdMechanic);
        }

        // PUT: api/MechanicsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMechanic(int id, UpdateMechanicDto mechanicDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedMechanic = await _mechanicService.UpdateAsync(id, mechanicDto);

            if (updatedMechanic == null)
            {
                return NotFound();
            }

            return Ok(updatedMechanic);
        }

        // DELETE: api/MechanicsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMechanic(int id)
        {
            try
            {
                var result = await _mechanicService.DeleteAsync(id);

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