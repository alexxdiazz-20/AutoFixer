# AutoFixer — Gestión de Taller Automotriz

Sistema web para administrar un taller mecánico: clientes, vehículos, catálogo de servicios, mecánicos y órdenes de servicio con cálculo automático del total.

## Stack

- **ASP.NET Core MVC (.NET 8)** + API REST documentada con **Swagger**
- **Entity Framework Core 9** con **SQL Server**
- **AutoMapper** para DTOs
- **Bootstrap 5** en las vistas

## Arquitectura

```
TallerAutomotriz.Domain          → Entidades (Customer, Vehicle, Service, Mechanic, ServiceOrder, ServiceOrderDetail)
TallerAutomotriz.Application     → DTOs, servicios de negocio, perfiles de AutoMapper
TallerAutomotriz.Infrastructure  → DbContext, migraciones, repositorios, Unit of Work
TallerAutomotriz.Presentation    → Controladores MVC, controladores API, vistas Razor
```

## Cómo correrlo

### Requisitos
- Visual Studio 2022 (17.8+) con la carga de trabajo **ASP.NET y desarrollo web**
- .NET 8 SDK
- SQL Server Express (instancia `SQLEXPRESS`) o LocalDB

### Pasos
1. Clona el repo:
   ```bash
   git clone https://github.com/alexxdiazz-20/AutoFixer.git
   ```
2. Abre `AutoFixer.sln` en Visual Studio.
3. Revisa el connection string en `TallerAutomotriz.Presentation/appsettings.json`. Por defecto apunta a `.\SQLEXPRESS`.
   Si usas otra instancia, **no edites ese archivo**; mejor cambia el tuyo local con *User Secrets*
   (clic derecho en `TallerAutomotriz.Presentation` → *Manage User Secrets*):
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=TallerAutomotrizDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
     }
   }
   ```
4. Pon `TallerAutomotriz.Presentation` como proyecto de inicio y dale a ▶.
   La base de datos se crea sola al arrancar (se aplican las migraciones).
5. Swagger: `https://localhost:7213/swagger`

## Flujo de trabajo del equipo

Las tareas se gestionan en **Jira** (proyecto `AF`). Cada cambio se liga a su issue:

- **Ramas:** `AF-<número>-descripcion-corta` → ej. `AF-5-validar-orden-sin-servicios`
- **Commits:** [Conventional Commits](https://www.conventionalcommits.org/es/) con la clave al inicio
  → `AF-5 fix: evitar error 500 al crear orden sin servicios`
- **Pull Requests:** título con la clave del issue; al menos **una aprobación** de otro miembro antes de hacer merge a `master`.
- **Nunca** hacer push directo a `master`.

### Definition of Done
- [ ] Cumple todos los criterios de aceptación del issue
- [ ] Compila sin errores y la app corre localmente
- [ ] Probado manualmente (y con prueba unitaria si aplica)
- [ ] PR revisado y aprobado por otro miembro
- [ ] Issue movido a **Done** en Jira

## Dashboard de inicio (AF-14)

La página de inicio muestra órdenes pendientes y en progreso de todos los meses, órdenes completadas e ingresos del mes actual, y las cinco órdenes más recientes por fecha de orden (desempate por ID). Se conservan los accesos a los módulos.

Los cálculos están en `DashboardService`, dentro de Application. El mes sigue la fecha local del servidor. Las métricas mensuales usan `CompletionDate`: incluyen el primer instante del mes y excluyen el primer instante del siguiente; una orden completada sin fecha de finalización no se suma al mes.

Para ejecutar las pruebas del dashboard:

```bash
dotnet test AutoFixer.sln
```
