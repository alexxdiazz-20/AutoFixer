using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TallerAutomotriz.Application.DTOs;
using TallerAutomotriz.Application.Exceptions;
using TallerAutomotriz.Application.Interfaces;

namespace TallerAutomotriz.Presentation.Controllers
{
    public class MechanicsController : Controller
    {
        private readonly IMechanicService _mechanicService;

        public MechanicsController(IMechanicService mechanicService)
        {
            _mechanicService = mechanicService;
        }

        // GET: Mechanics
        public async Task<IActionResult> Index()
        {
            var mechanics = await _mechanicService.GetAllAsync();
            return View(mechanics);
        }

        // GET: Mechanics/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var mechanic = await _mechanicService.GetByIdAsync(id);

            if (mechanic == null)
            {
                return NotFound();
            }

            return View(mechanic);
        }

        // GET: Mechanics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mechanics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMechanicDto mechanicDto)
        {
            if (ModelState.IsValid)
            {
                await _mechanicService.CreateAsync(mechanicDto);
                TempData["Success"] = "Mecánico registrado correctamente.";
                return RedirectToAction(nameof(Index));
            }

            return View(mechanicDto);
        }

        // GET: Mechanics/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var mechanic = await _mechanicService.GetByIdAsync(id);

            if (mechanic == null)
            {
                return NotFound();
            }

            var updateDto = new UpdateMechanicDto
            {
                Name = mechanic.Name,
                LastName = mechanic.LastName,
                Specialization = mechanic.Specialization,
                Phone = mechanic.Phone,
                Email = mechanic.Email,
                HireDate = mechanic.HireDate
            };

            return View(updateDto);
        }

        // POST: Mechanics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateMechanicDto mechanicDto)
        {
            if (ModelState.IsValid)
            {
                var updatedMechanic = await _mechanicService.UpdateAsync(id, mechanicDto);

                if (updatedMechanic == null)
                {
                    return NotFound();
                }

                TempData["Success"] = "Mecánico actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }

            return View(mechanicDto);
        }

        // GET: Mechanics/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var mechanic = await _mechanicService.GetByIdAsync(id);

            if (mechanic == null)
            {
                return NotFound();
            }

            return View(mechanic);
        }

        // POST: Mechanics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _mechanicService.DeleteAsync(id);
                TempData["Success"] = "Mecánico eliminado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (BusinessRuleException ex)
            {
                // Se queda en la pantalla de eliminar mostrando por qué no se pudo
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Delete), new { id });
            }
        }
    }
}