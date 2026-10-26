using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using TallerAutomotriz.Application.DTOs;
using TallerAutomotriz.Application.Interfaces;

namespace TallerAutomotriz.Presentation.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly ICustomerService _customerService;

        public VehiclesController(IVehicleService vehicleService, ICustomerService customerService)
        {
            _vehicleService = vehicleService;
            _customerService = customerService;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var vehicles = await _vehicleService.GetAllAsync();
            return View(vehicles);
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public async Task<IActionResult> Create()
        {
            await PopulateCustomersDropDown();
            return View();
        }

        // POST: Vehicles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVehicleDto vehicleDto)
        {
            if (ModelState.IsValid)
            {
                await _vehicleService.CreateAsync(vehicleDto);
                return RedirectToAction(nameof(Index));
            }

            await PopulateCustomersDropDown(vehicleDto.CustomerId);
            return View(vehicleDto);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            var updateDto = new UpdateVehicleDto
            {
                LicensePlate = vehicle.LicensePlate,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Color = vehicle.Color,
                VIN = vehicle.VIN,
                CustomerId = vehicle.CustomerId
            };

            await PopulateCustomersDropDown(vehicle.CustomerId);
            return View(updateDto);
        }

        // POST: Vehicles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateVehicleDto vehicleDto)
        {
            if (ModelState.IsValid)
            {
                var updatedVehicle = await _vehicleService.UpdateAsync(id, vehicleDto);

                if (updatedVehicle == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            await PopulateCustomersDropDown(vehicleDto.CustomerId);
            return View(vehicleDto);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehicleService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateCustomersDropDown(int? selectedCustomerId = null)
        {
            var customers = await _customerService.GetAllAsync();
            ViewBag.Customers = new SelectList(
                customers,
                "Id",
                "Name",
                selectedCustomerId);
        }
    }
}