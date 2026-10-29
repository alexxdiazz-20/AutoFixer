using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TallerAutomotriz.Application.DTOs;
using TallerAutomotriz.Application.Exceptions;
using TallerAutomotriz.Application.Interfaces;

namespace TallerAutomotriz.Presentation.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            var services = await _serviceService.GetAllAsync();
            return View(services);
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var service = await _serviceService.GetByIdAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateServiceDto serviceDto)
        {
            if (ModelState.IsValid)
            {
                await _serviceService.CreateAsync(serviceDto);
                TempData["Success"] = "Servicio creado correctamente.";
                return RedirectToAction(nameof(Index));
            }

            return View(serviceDto);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var service = await _serviceService.GetByIdAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            var updateDto = new UpdateServiceDto
            {
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                EstimatedTimeInMinutes = service.EstimatedTimeInMinutes
            };

            return View(updateDto);
        }

        // POST: Services/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateServiceDto serviceDto)
        {
            if (ModelState.IsValid)
            {
                var updatedService = await _serviceService.UpdateAsync(id, serviceDto);

                if (updatedService == null)
                {
                    return NotFound();
                }

                TempData["Success"] = "Servicio actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }

            return View(serviceDto);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _serviceService.GetByIdAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _serviceService.DeleteAsync(id);
                TempData["Success"] = "Servicio eliminado correctamente.";
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