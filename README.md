# VentasPro

Sistema de ventas desarrollado con Blazor Server, .NET 8 y Arquitectura Limpia (Clean Architecture).

## Arquitectura

El proyecto sigue los principios de Clean Architecture con las siguientes capas:

```
┌─────────────────────────────────────┐
│         VentasPro.App               │  ← Presentación (Blazor Server)
│  Pages, Components, Layouts         │
├─────────────────────────────────────┤
│       VentasPro.Application          │  ← Casos de Uso, DTOs, Commands
│  Features, Services, Interfaces     │
├─────────────────────────────────────┤
│        VentasPro.Domain              │  ← Entidades, Reglas de Negocio
│  Entities, Enums, Repositories      │
├─────────────────────────────────────┤
│       VentasPro.Infrastructure      │  ← Implementaciones, EF Core
│  DbContext, Repositories, Configs    │
└─────────────────────────────────────┘
```

### Regla de Dependencias

Las dependencias siempre apuntan hacia adentro:
- `App` → `Application` → `Domain`
- `Infrastructure` → `Application` + `Domain`

## Estructura del Proyecto

```
VentasPro/
├── VentasPro.sln
│
├── VentasPro.Domain/
│   ├── Entities/           # Producto, Cliente, Venta, etc.
│   ├── Enums/              # EstadoVenta, MetodoPago
│   ├── Exceptions/         # Excepciones personalizadas
│   ├── Repositories/       # Interfaces IRepository<T>
│   └── ValueObjects/       # Objetos de valor
│
├── VentasPro.Application/
│   └── Features/
│       ├── Productos/
│       │   ├── Commands/   # Create, Update
│       │   ├── Queries/    # GetById, GetAll
│       │   └── DTOs/       # ProductoDto
│       └── Clientes/
│           ├── Commands/
│           ├── Queries/
│           └── DTOs/
│
├── VentasPro.Infrastructure/
│   ├── Data/
│   │   ├── DbContext/     # ApplicationDbContext
│   │   └── Configurations/ # Entity Configurations
│   ├── Repositories/       # Implementaciones
│   └── Services/
│
└── VentasPro.App/
    ├── Components/
    │   ├── Pages/          # Producto, Cliente, Home
    │   └── Layout/         # NavMenu, MainLayout
    ├── Program.cs
    └── appsettings.json
```

## Entidades

| Entidad | Descripción |
|---------|-------------|
| **Producto** | Productos del catálogo con precio y stock |
| **Categoria** | Categorías para organizar productos |
| **Cliente** | Clientes registrados en el sistema |
| **Venta** | Encabezado de venta con totales |
| **DetalleVenta** | Líneas de detalle de cada venta |

## Requisitos

- .NET 8.0 SDK
- SQLite (incluido en .NET)

## Configuración

### 1. Restaurar paquetes
```bash
dotnet restore
```

### 2. Aplicar migraciones
```bash
dotnet ef database update --project VentasPro.Infrastructure --startup-project VentasPro.App
```

### 3. Ejecutar la aplicación
```bash
cd VentasPro.App
dotnet run
```

La aplicación estará disponible en `https://localhost:5000` o `http://localhost:5001`.

## Base de Datos

La base de datos SQLite se crea automáticamente en `VentasPro.App/VentasPro.db` al ejecutar la aplicación.

### Migraciones

Crear una nueva migración:
```bash
dotnet ef migrations add <NombreMigracion> --project VentasPro.Infrastructure --startup-project VentasPro.App --output-dir Data/Migrations
```

## Funcionalidades

- [x] Gestión de Productos (CRUD)
- [x] Gestión de Clientes (CRUD)
- [x] Base de datos SQLite con EF Core
- [x] Arquitectura Limpia implementada
- [ ] Registro de Ventas
- [ ] Dashboard con estadísticas
- [ ] Autenticación y autorización
- [ ] Preparación para Docker

## Docker

El proyecto incluye configuración para Docker:

```bash
docker-compose up -d
```

## Tecnologías

- **.NET 8.0** - Framework principal
- **Blazor Server** - Framework de UI
- **Entity Framework Core 8.0** - ORM
- **SQLite** - Base de datos
- **Clean Architecture** - Patrón de arquitectura

## Licencia

MIT