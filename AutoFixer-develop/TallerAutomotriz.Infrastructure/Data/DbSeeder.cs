using Microsoft.EntityFrameworkCore;
using TallerAutomotriz.Domain.Entities;

namespace TallerAutomotriz.Infrastructure.Data
{
    /// <summary>
    /// Carga datos de demostración cuando la base de datos está vacía.
    /// Si ya existen clientes, no hace nada (no toca datos existentes).
    /// </summary>
    public static class DbSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (await context.Customers.AnyAsync())
                return;

            var now = DateTime.UtcNow;
            var today = DateTime.Today;

            // ---------- Clientes ----------
            var juan = new Customer { Name = "Juan", LastName = "Pérez", Email = "juan.perez@correo.com", Phone = "809-555-0101", Address = "Av. Winston Churchill #45, Santo Domingo", CreatedAt = now };
            var maria = new Customer { Name = "María", LastName = "Rodríguez", Email = "maria.rodriguez@correo.com", Phone = "829-555-0102", Address = "C/ El Conde #12, Zona Colonial", CreatedAt = now };
            var carlos = new Customer { Name = "Carlos", LastName = "Gómez", Email = "carlos.gomez@correo.com", Phone = "849-555-0103", Address = "Av. 27 de Febrero #210, Santo Domingo", CreatedAt = now };
            var ana = new Customer { Name = "Ana", LastName = "Martínez", Email = "ana.martinez@correo.com", Phone = "809-555-0104", Address = "C/ Duarte #8, Santiago", CreatedAt = now };
            var luis = new Customer { Name = "Luis", LastName = "Fernández", Email = "luis.fernandez@correo.com", Phone = "829-555-0105", Address = "Av. Independencia #77, Santo Domingo", CreatedAt = now };
            var paola = new Customer { Name = "Paola", LastName = "Santana", Email = "paola.santana@correo.com", Phone = "809-555-0106", Address = "C/ Las Américas #3, Boca Chica", CreatedAt = now };

            // ---------- Vehículos ----------
            var corolla = new Vehicle { LicensePlate = "A123456", Brand = "Toyota", Model = "Corolla", Year = 2018, Color = "Blanco", VIN = "2T1BURHE5JC012345", Customer = juan, CreatedAt = now };
            var hilux = new Vehicle { LicensePlate = "L234567", Brand = "Toyota", Model = "Hilux", Year = 2021, Color = "Gris", VIN = "MR0FA3CD1M0023456", Customer = juan, CreatedAt = now };
            var civic = new Vehicle { LicensePlate = "A345678", Brand = "Honda", Model = "Civic", Year = 2019, Color = "Negro", VIN = "2HGFC2F69KH034567", Customer = maria, CreatedAt = now };
            var tucson = new Vehicle { LicensePlate = "G456789", Brand = "Hyundai", Model = "Tucson", Year = 2020, Color = "Azul", VIN = "KM8J33A46LU045678", Customer = carlos, CreatedAt = now };
            var sportage = new Vehicle { LicensePlate = "G567890", Brand = "Kia", Model = "Sportage", Year = 2017, Color = "Rojo", VIN = "KNDPM3AC7H7056789", Customer = ana, CreatedAt = now };
            var crv = new Vehicle { LicensePlate = "G678901", Brand = "Honda", Model = "CR-V", Year = 2022, Color = "Plateado", VIN = "7FARW2H85NE067890", Customer = luis, CreatedAt = now };
            var sentra = new Vehicle { LicensePlate = "A789012", Brand = "Nissan", Model = "Sentra", Year = 2016, Color = "Blanco", VIN = "3N1AB7AP8GY078901", Customer = paola, CreatedAt = now };

            // ---------- Mecánicos ----------
            var pedro = new Mechanic { Name = "Pedro", LastName = "Almonte", Specialization = "Motor y transmisión", Phone = "809-555-0201", Email = "pedro.almonte@autofixer.com", HireDate = new DateTime(2019, 3, 15), CreatedAt = now };
            var ramon = new Mechanic { Name = "Ramón", LastName = "Castillo", Specialization = "Frenos y suspensión", Phone = "829-555-0202", Email = "ramon.castillo@autofixer.com", HireDate = new DateTime(2020, 7, 1), CreatedAt = now };
            var jose = new Mechanic { Name = "José", LastName = "Medina", Specialization = "Electricidad y diagnóstico", Phone = "849-555-0203", Email = "jose.medina@autofixer.com", HireDate = new DateTime(2021, 1, 10), CreatedAt = now };
            var miguel = new Mechanic { Name = "Miguel", LastName = "Reyes", Specialization = "Aire acondicionado", Phone = "809-555-0204", Email = "miguel.reyes@autofixer.com", HireDate = new DateTime(2022, 9, 5), CreatedAt = now };

            // ---------- Servicios ----------
            var aceite = new Service { Name = "Cambio de aceite y filtro", Description = "Aceite sintético 5W-30 y filtro nuevo", Price = 2500m, EstimatedTimeInMinutes = 45, CreatedAt = now };
            var alineacion = new Service { Name = "Alineación y balanceo", Description = "Alineación computarizada y balanceo de 4 gomas", Price = 1800m, EstimatedTimeInMinutes = 60, CreatedAt = now };
            var frenos = new Service { Name = "Cambio de pastillas de freno", Description = "Pastillas delanteras y revisión de discos", Price = 3500m, EstimatedTimeInMinutes = 90, CreatedAt = now };
            var diagnostico = new Service { Name = "Diagnóstico computarizado", Description = "Escaneo OBD-II y reporte de fallas", Price = 1500m, EstimatedTimeInMinutes = 30, CreatedAt = now };
            var bateria = new Service { Name = "Cambio de batería", Description = "Batería nueva con instalación y prueba de alternador", Price = 6500m, EstimatedTimeInMinutes = 30, CreatedAt = now };
            var aire = new Service { Name = "Mantenimiento de aire acondicionado", Description = "Limpieza, revisión de fugas y recarga de gas", Price = 4000m, EstimatedTimeInMinutes = 120, CreatedAt = now };
            var afinado = new Service { Name = "Afinado de motor", Description = "Bujías, filtro de aire y limpieza de inyectores", Price = 5500m, EstimatedTimeInMinutes = 150, CreatedAt = now };
            var correa = new Service { Name = "Cambio de correa de distribución", Description = "Correa, tensor y bomba de agua", Price = 9000m, EstimatedTimeInMinutes = 240, CreatedAt = now };

            // ---------- Órdenes de servicio ----------
            var orders = new List<ServiceOrder>
            {
                Order(corolla, pedro, today.AddDays(-20), ServiceOrderStatus.Completed, today.AddDays(-19),
                    "Mantenimiento de los 60,000 km", now,
                    (aceite, 1, "Aceite sintético"), (afinado, 1, "")),
                Order(civic, ramon, today.AddDays(-14), ServiceOrderStatus.Completed, today.AddDays(-14),
                    "Ruido al frenar", now,
                    (frenos, 1, "Discos en buen estado"), (alineacion, 1, "")),
                Order(tucson, jose, today.AddDays(-9), ServiceOrderStatus.Completed, today.AddDays(-8),
                    "No enciende en las mañanas", now,
                    (diagnostico, 1, ""), (bateria, 1, "Batería anterior con 4 años de uso")),
                Order(sportage, miguel, today.AddDays(-6), ServiceOrderStatus.Cancelled, null,
                    "Aire acondicionado no enfría", now,
                    (aire, 1, "Cliente decidió no hacer el trabajo")),
                Order(crv, pedro, today.AddDays(-3), ServiceOrderStatus.InProgress, null,
                    "Cambio de correa preventivo", now,
                    (correa, 1, "Esperando bomba de agua"), (aceite, 1, "")),
                Order(hilux, ramon, today.AddDays(-2), ServiceOrderStatus.InProgress, null,
                    "Vibración en el volante", now,
                    (alineacion, 1, ""), (diagnostico, 1, "")),
                Order(sentra, jose, today.AddDays(-1), ServiceOrderStatus.Pending, null,
                    "Luz de check engine encendida", now,
                    (diagnostico, 1, "")),
                Order(corolla, miguel, today, ServiceOrderStatus.Pending, null,
                    "Revisión de aire acondicionado", now,
                    (aire, 1, ""), (aceite, 1, "")),
            };

            context.AddRange(juan, maria, carlos, ana, luis, paola);
            context.AddRange(corolla, hilux, civic, tucson, sportage, crv, sentra);
            context.AddRange(pedro, ramon, jose, miguel);
            context.AddRange(aceite, alineacion, frenos, diagnostico, bateria, aire, afinado, correa);
            context.AddRange(orders);

            await context.SaveChangesAsync();
        }

        private static ServiceOrder Order(
            Vehicle vehicle,
            Mechanic mechanic,
            DateTime orderDate,
            ServiceOrderStatus status,
            DateTime? completionDate,
            string description,
            DateTime createdAt,
            params (Service Service, int Quantity, string Notes)[] items)
        {
            var details = items.Select(i => new ServiceOrderDetail
            {
                Service = i.Service,
                Quantity = i.Quantity,
                UnitPrice = i.Service.Price,
                Subtotal = i.Service.Price * i.Quantity,
                Notes = i.Notes,
                CreatedAt = createdAt
            }).ToList();

            return new ServiceOrder
            {
                Vehicle = vehicle,
                Mechanic = mechanic,
                OrderDate = orderDate,
                CompletionDate = completionDate,
                Description = description,
                Status = status,
                TotalAmount = details.Sum(d => d.Subtotal),
                Details = details,
                CreatedAt = createdAt
            };
        }
    }
}
