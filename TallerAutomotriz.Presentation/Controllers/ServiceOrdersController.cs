using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using TallerAutomotriz.Application.DTOs;
using TallerAutomotriz.Application.Exceptions;
using TallerAutomotriz.Application.Interfaces;

namespace TallerAutomotriz.Presentation.Controllers
{
    public class ServiceOrdersController : Controller
    {
        private readonly IServiceOrderService _serviceOrderService;
        private readonly IVehicleService _vehicleService;
        private readonly IMechanicService _mechanicService;
        private readonly IServiceService _serviceService;

        public ServiceOrdersController(
            IServiceOrderService serviceOrderService,
            IVehicleService vehicleService,
            IMechanicService mechanicService,
            IServiceService serviceService)
        {
            _serviceOrderService = serviceOrderService;
            _vehicleService = vehicleService;
            _mechanicService = mechanicService;
            _serviceService = serviceService;
        }

        // GET: ServiceOrders
        public async Task<IActionResult> Index()
        {
            var serviceOrders = await _serviceOrderService.GetAllAsync();
            return View(serviceOrders);
        }

        // GET: ServiceOrders/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var serviceOrder = await _serviceOrderService.GetByIdAsync(id);

            if (serviceOrder == null)
            {
                return NotFound();
            }

            return View(serviceOrder);
        }

        // GET: ServiceOrders/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropDowns();
            return View();
        }

        // POST: ServiceOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateServiceOrderDto serviceOrderDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _serviceOrderService.CreateAsync(serviceOrderDto);
                    TempData["Success"] = "Orden de servicio creada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (BusinessRuleException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            await PopulateDropDowns(serviceOrderDto.VehicleId, serviceOrderDto.MechanicId);
            return View(serviceOrderDto);
        }

        // GET: ServiceOrders/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var serviceOrder = await _serviceOrderService.GetByIdAsync(id);

            if (serviceOrder == null)
            {
                return NotFound();
            }

            var updateDto = new UpdateServiceOrderDto
            {
                CompletionDate = serviceOrder.CompletionDate,
                Description = serviceOrder.Description,
                Status = serviceOrder.Status,
                VehicleId = serviceOrder.VehicleId,
                MechanicId = serviceOrder.MechanicId,
                Details = serviceOrder.Details
                    .Select(d => new CreateServiceOrderDetailDto
                    {
                        ServiceId = d.ServiceId,
                        Quantity = d.Quantity,
                        Notes = d.Notes
                    })
                    .ToList()
            };

            await PopulateDropDowns(serviceOrder.VehicleId, serviceOrder.MechanicId);
            return View(updateDto);
        }

        // POST: ServiceOrders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateServiceOrderDto serviceOrderDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedServiceOrder = await _serviceOrderService.UpdateAsync(id, serviceOrderDto);

                    if (updatedServiceOrder == null)
                    {
                        return NotFound();
                    }

                    TempData["Success"] = "Orden de servicio actualizada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (BusinessRuleException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            await PopulateDropDowns(serviceOrderDto.VehicleId, serviceOrderDto.MechanicId);
            return View(serviceOrderDto);
        }

        // GET: ServiceOrders/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var serviceOrder = await _serviceOrderService.GetByIdAsync(id);

            if (serviceOrder == null)
            {
                return NotFound();
            }

            return View(serviceOrder);
        }

        // POST: ServiceOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _serviceOrderService.DeleteAsync(id);
            TempData["Success"] = "Orden de servicio eliminada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropDowns(int? selectedVehicleId = null, int? selectedMechanicId = null)
        {
            var vehicles = await _vehicleService.GetAllAsync();
            var mechanics = await _mechanicService.GetAllAsync();
            var services = await _serviceService.GetAllAsync();

            ViewBag.Vehicles = new SelectList(
                vehicles.Select(vehicle => new
                {
                    vehicle.Id,
                    Label = $"{vehicle.LicensePlate} - {vehicle.Brand} {vehicle.Model}"
                }),
                "Id",
                "Label",
                selectedVehicleId);

            ViewBag.Mechanics = new SelectList(
                mechanics.Select(mechanic => new
                {
                    mechanic.Id,
                    Label = $"{mechanic.Name} {mechanic.LastName}".Trim()
                }),
                "Id",
                "Label",
                selectedMechanicId);

            ViewBag.Services = new SelectList(
                services,
                "Id",
                "Name");
        }
    }
}
