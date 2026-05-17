# Manual de Configuración de Proyecto Blazor Server con .NET 8, Arquitectura Limpia, EF Core (SQLite) y Docker

Este manual describe cómo configurar una aplicación Blazor Server con .NET 8, siguiendo una arquitectura limpia, utilizando Entity Framework Core con SQLite para el acceso a datos (code-first) y preparando el proyecto para Docker.

## 1. Estructura de la Solución y Proyectos

La solución `VentasPro` se organizará en las siguientes capas, cada una en su propio proyecto (biblioteca de clases o aplicación web):

*   **VentasPro.sln**: Archivo de solución que agrupa todos los proyectos.
*   **VentasPro.App** (Capa de Presentación): Proyecto Blazor Server. Contendrá la interfaz de usuario y la lógica de presentación.
*   **VentasPro.Domain** (Capa de Dominio): Biblioteca de clases que contiene las entidades de negocio, interfaces de repositorio, servicios de dominio y lógica de negocio pura. Es el "núcleo" de la aplicación y no debe tener dependencias de otras capas.
*   **VentasPro.Application** (Capa de Aplicación): Biblioteca de clases que contendrá la lógica de la aplicación, como casos de uso (use cases), comandos y queries (CQRS), DTOs y servicios de aplicación. Orquestra la interacción entre el dominio y la infraestructura.
*   **VentasPro.Infrastructure** (Capa de Infraestructura): Biblioteca de clases que implementa los contratos definidos en el dominio y la aplicación. Contendrá la implementación de repositorios (con EF Core), servicios externos, configuración de base de datos y otras preocupaciones técnicas.

## 2. Creación de la Solución y Proyectos

Abra una terminal en la ubicación donde desea crear su proyecto (por ejemplo, `C:\Users\nicos92\Desktop\VentasPro`) y ejecute los siguientes comandos:

1.  **Crear la solución:**
    ```bash
    dotnet new sln -n VentasPro
    ```

2.  **Configurar la versión del SDK de .NET (opcional pero recomendado para consistencia):**
    ```bash
    dotnet new globaljson --sdk-version 8.0.421
    ```

3.  **Crear el proyecto Blazor Server (Capa de Presentación):**
    ```bash
    dotnet new blazor -int Server -n VentasPro.App -o VentasPro.App
    dotnet sln add VentasPro.App/VentasPro.App.csproj
    ```

3.  **Crear la biblioteca de clases para el Dominio:**
    ```bash
    dotnet new classlib -n VentasPro.Domain -o VentasPro.Domain
    dotnet sln add VentasPro.Domain/VentasPro.Domain.csproj
    ```

4.  **Crear la biblioteca de clases para la Aplicación:**
    ```bash
    dotnet new classlib -n VentasPro.Application -o VentasPro.Application
    dotnet sln add VentasPro.Application/VentasPro.Application.csproj
    ```

5.  **Crear la biblioteca de clases para la Infraestructura:**
    ```bash
    dotnet new classlib -n VentasPro.Infrastructure -o VentasPro.Infrastructure
    dotnet sln add VentasPro.Infrastructure/VentasPro.Infrastructure.csproj
    ```

## 3. Referencias entre Proyectos

Establezca las referencias necesarias para que las capas puedan comunicarse (siempre en la dirección correcta, de fuera hacia dentro):

1.  **`VentasPro.App` referencia a `VentasPro.Application`:**
    ```bash
    dotnet add VentasPro.App/VentasPro.App.csproj reference VentasPro.Application/VentasPro.Application.csproj
    ```

2.  **`VentasPro.Application` referencia a `VentasPro.Domain`:**
    ```bash
    dotnet add VentasPro.Application/VentasPro.Application.csproj reference VentasPro.Domain/VentasPro.Domain.csproj
    ```

3.  **`VentasPro.Infrastructure` referencia a `VentasPro.Application` y `VentasPro.Domain`:**
    ```bash
    dotnet add VentasPro.Infrastructure/VentasPro.Infrastructure.csproj reference VentasPro.Application/VentasPro.Application.csproj
    dotnet add VentasPro.Infrastructure/VentasPro.Infrastructure.csproj reference VentasPro.Domain/VentasPro.Domain.csproj
    ```

## 4. Organización de Carpetas (Arquitectura Limpia)

Aquí se sugiere una estructura de carpetas dentro de cada proyecto para mantener la organización:

*   **VentasPro.App**
    *   `Components/`: Componentes Blazor (páginas, layouts, componentes compartidos).
    *   `Data/`: Posibles modelos de vista o servicios de UI.
    *   `Program.cs`: Configuración inicial y registro de servicios.
    *   `App.razor`, `_Host.cshtml`, etc.

*   **VentasPro.Domain**
    *   `Entities/`: Clases que representan el modelo de negocio (ej. `Producto`, `Cliente`, `Venta`).
    *   `ValueObjects/`: Clases inmutables que representan conceptos con valor propio (ej. `Direccion`, `Email`).
    *   `Enums/`: Enumeraciones de dominio.
    *   `Exceptions/`: Excepciones personalizadas del dominio.
    *   `Services/`: Interfaces para servicios de dominio.
    *   `Repositories/`: Interfaces para repositorios de datos (ej. `IProductoRepository`).

*   **VentasPro.Application**
    *   `Features/`: Organizado por funcionalidades (ej. `Productos/Commands/CrearProducto`, `Productos/Queries/ObtenerProductos`).
        *   `Commands/`: Clases para operaciones de escritura que modifican el estado.
        *   `Queries/`: Clases para operaciones de lectura que no modifican el estado.
        *   `DTOs/`: Objetos de Transferencia de Datos.
        *   `Handlers/`: Implementaciones de la lógica de comandos y queries (usando librerías como MediatR).
    *   `Interfaces/`: Interfaces para servicios de aplicación (ej. `IUnitOfWork`).
    *   `Services/`: Implementaciones de servicios de aplicación.

*   **VentasPro.Infrastructure**
    *   `Data/`:
        *   `DbContext/`: Clases de contexto de Entity Framework (ej. `ApplicationDbContext`).
        *   `Configurations/`: Configuraciones de entidades para EF Core (ej. `ProductoConfiguration`).
        *   `Migrations/`: Carpeta para las migraciones de EF Core.
    *   `Repositories/`: Implementaciones de los repositorios definidos en `VentasPro.Domain` (ej. `ProductoRepository`).
    *   `Services/`: Implementaciones de servicios externos o de infraestructura.
    *   `Persistence/`: Configuración de la base de datos y registro de servicios.

## 5. Configuración de Entity Framework Core con SQLite (Code-First)

### 5.1. Instalación de Paquetes Nuget

En el proyecto `VentasPro.Infrastructure`, instale los siguientes paquetes de Nuget. Abra la terminal en la carpeta `VentasPro.Infrastructure` y ejecute:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SQLite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

### 5.2. Definición de Entidades (VentasPro.Domain)

Cree sus clases de entidad en la carpeta `VentasPro.Domain/Entities`.

**Ejemplo: `Producto.cs`**
```VentasPro.Domain/Entities/Producto.cs
public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
}
```

### 5.3. Creación del DbContext (VentasPro.Infrastructure)

Cree su `DbContext` en la carpeta `VentasPro.Infrastructure/Data/DbContext`.

**Ejemplo: `ApplicationDbContext.cs`**
```VentasPro.Infrastructure/Data/DbContext/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using VentasPro.Domain.Entities;

namespace VentasPro.Infrastructure.Data.DbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Aquí puede configurar sus entidades, por ejemplo:
            // modelBuilder.Entity<Producto>().Property(p => p.Precio).HasColumnType("decimal(18,2)");
        }
    }
}
```

### 5.4. Configuración de la Conexión en `Program.cs` (VentasPro.App)

En el proyecto `VentasPro.App`, edite `Program.cs` para registrar su `DbContext` y configurar la cadena de conexión.

```VentasPro.App/Program.cs
// ... otras using ...
using Microsoft.EntityFrameworkCore;
using VentasPro.Infrastructure.Data.DbContext; // Asegúrese de tener esta referencia

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configuración de Entity Framework Core con SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Inicializar y aplicar migraciones al iniciar la aplicación (opcional, para entornos de desarrollo)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
```

### 5.5. Configuración de `appsettings.json` (VentasPro.App)

Añada la cadena de conexión a su `appsettings.json` en `VentasPro.App`.

```VentasPro.App/appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=VentasPro.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```
Esto creará un archivo `VentasPro.db` en la raíz del proyecto `VentasPro.App`.

### 5.6. Migraciones de Entity Framework Core

Para crear las migraciones y actualizar la base de datos, navegue a la carpeta de su proyecto de **infraestructura** (`VentasPro.Infrastructure`) en la terminal y ejecute los siguientes comandos:

```bash
# Asegúrese de que el proyecto de startup sea VentasPro.App
dotnet ef migrations add InitialCreate --project VentasPro.Infrastructure --startup-project VentasPro.App
dotnet ef database update --project VentasPro.Infrastructure --startup-project VentasPro.App
```
*   `InitialCreate`: Es el nombre de la migración, puede elegir otro.
*   `--project VentasPro.Infrastructure`: Indica que las migraciones y el `DbContext` están en este proyecto.
*   `--startup-project VentasPro.App`: Indica cuál es el proyecto de inicio que tiene la configuración del `DbContext` (cadena de conexión, etc.).

## 6. Integración con Blazor Server

Ahora que tiene EF Core configurado, puede inyectar `ApplicationDbContext` en sus componentes Blazor o servicios de aplicación para interactuar con la base de datos.

**Ejemplo: Un servicio en `VentasPro.Application` que usa el repositorio y el contexto.**

Primero, defina la interfaz del repositorio en `VentasPro.Domain/Repositories/IProductoRepository.cs`:
```VentasPro.Domain/Repositories/IProductoRepository.cs
using VentasPro.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VentasPro.Domain.Repositories
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<Producto> GetByIdAsync(int id);
        Task AddAsync(Producto producto);
        Task UpdateAsync(Producto producto);
        Task DeleteAsync(int id);
    }
}
```

Luego, implemente el repositorio en `VentasPro.Infrastructure/Repositories/ProductoRepository.cs`:
```VentasPro.Infrastructure/Repositories/ProductoRepository.cs
using Microsoft.EntityFrameworkCore;
using VentasPro.Domain.Entities;
using VentasPro.Domain.Repositories;
using VentasPro.Infrastructure.Data.DbContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VentasPro.Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto> GetByIdAsync(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task AddAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Producto producto)
        {
            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
```

Registre el repositorio en `Program.cs` de `VentasPro.App`:
```VentasPro.App/Program.cs
// ...
using VentasPro.Domain.Repositories;
using VentasPro.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ... Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar el repositorio
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

var app = builder.Build();
// ...
```

Ahora, puede inyectar `IProductoRepository` en sus componentes o servicios de Blazor.

**Ejemplo: Un componente Blazor en `VentasPro.App`**
```VentasPro.App/Components/Pages/Productos.razor
@page "/productos"
@inject VentasPro.Domain.Repositories.IProductoRepository ProductoRepository

<h3>Lista de Productos</h3>

@if (productos == null)
{
    <p><em>Cargando productos...</em></p>
}
else
{
    <table class="table">
        <thead>
            <tr>
                <th>ID</th>
                <th>Nombre</th>
                <th>Descripción</th>
                <th>Precio</th>
                <th>Stock</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var producto in productos)
            {
                <tr>
                    <td>@producto.Id</td>
                    <td>@producto.Nombre</td>
                    <td>@producto.Descripcion</td>
                    <td>@producto.Precio.ToString("C")</td>
                    <td>@producto.Stock</td>
                </tr>
            }
        </tbody>
    </table>
}

@code {
    private IEnumerable<VentasPro.Domain.Entities.Producto> productos;

    protected override async Task OnInitializedAsync()
    {
        productos = await ProductoRepository.GetAllAsync();
    }
}
```

## 7. Configuración de Docker

Para contenerizar su aplicación Blazor Server con .NET 8, necesitará un `Dockerfile`, un archivo `.dockerignore` y un `docker-compose.yml`.

### 7.1. Dockerfile

Cree un archivo llamado `Dockerfile` en la raíz de su solución (`C:\Users\nicos92\Desktop\VentasPro`).

```Dockerfile
# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["VentasPro.App/VentasPro.App.csproj", "VentasPro.App/"]
COPY ["VentasPro.Application/VentasPro.Application.csproj", "VentasPro.Application/"]
COPY ["VentasPro.Domain/VentasPro.Domain.csproj", "VentasPro.Domain/"]
COPY ["VentasPro.Infrastructure/VentasPro.Infrastructure.csproj", "VentasPro.Infrastructure/"]
RUN dotnet restore "VentasPro.App/VentasPro.App.csproj"

COPY . .
WORKDIR "/src/VentasPro.App"
RUN dotnet build "VentasPro.App.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "VentasPro.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the official .NET ASP.NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VentasPro.App.dll"]
```

### 7.2. .dockerignore

Cree un archivo llamado `.dockerignore` en la raíz de su solución (`C:\Users\nicos92\Desktop\VentasPro`). Este archivo es similar a `.gitignore` y evita que archivos innecesarios se copien a la imagen Docker, reduciendo su tamaño y el tiempo de construcción.

```.dockerignore
**/.classpath
**/.dockerignore
**/.env
**/.git
**/.gitignore
**/.project
**/.settings
**/.vs
**/.vscode
**/*.*proj.user
**/*.dbmdl
**/*.jfm
**/bin
**/obj
**/node_modules
**/*.db
```
**Nota:** He incluido `**/*.db` para que el archivo SQLite de la base de datos no se copie en la imagen final si está en el contexto de construcción. Si desea que la base de datos sea parte de la imagen, elimine esta línea, aunque lo más común es que la base de datos se genere en tiempo de ejecución o se monte como un volumen persistente.

### 7.3. docker-compose.yml

Cree un archivo llamado `docker-compose.yml` en la raíz de su solución (`C:\Users\nicos92\Desktop\VentasPro`). Este archivo le permitirá definir y ejecutar su aplicación Blazor Server como un servicio.

```docker-compose.yml
version: '3.8'

services:
  ventaspro_app:
    container_name: ventaspro_app_container
    build:
      context: .
      dockerfile: Dockerfile
    ports:
      - "8080:8080" # Mapea el puerto 8080 del host al puerto 8080 del contenedor
    environment:
      - ASPNETCORE_URLS=http://+:8080 # La aplicación escucha en el puerto 8080 dentro del contenedor
      - ConnectionStrings__DefaultConnection=Data Source=/app/VentasPro.db # Ruta a la DB dentro del contenedor
    volumes:
      - ./data:/app # Monta un volumen para persistir la base de datos SQLite
    restart: always
```
**Consideraciones para Docker Compose:**
*   **Puerto:** La aplicación Blazor se configurará para escuchar en el puerto 8080 dentro del contenedor (`ASPNETCORE_URLS=http://+:8080`). El `ports` mapea el puerto 8080 del host al 8080 del contenedor.
*   **Volumen para SQLite:** El volumen `./data:/app` montará una carpeta `data` en la raíz de su solución en el host a la carpeta `/app` dentro del contenedor. Esto es crucial para que el archivo `VentasPro.db` sea persistente y no se pierda cada vez que se reinicie o elimine el contenedor. La cadena de conexión en el `appsettings.json` dentro del contenedor apuntará a este volumen.
*   **Cadena de Conexión en Docker:** Notará que en `docker-compose.yml` se sobrescribe la cadena de conexión (`ConnectionStrings__DefaultConnection=Data Source=/app/VentasPro.db`). Esto es para asegurar que la aplicación dentro del contenedor busque el archivo `VentasPro.db` en la ubicación correcta (que es el directorio de trabajo del contenedor, donde también se montará el volumen `data`).

### 7.4. Construir y Ejecutar con Docker Compose

Desde la raíz de su solución en la terminal, puede construir y ejecutar su aplicación Dockerizada:

```bash
docker-compose build
docker-compose up -d
```
*   `docker-compose build`: Construye la imagen Docker de su aplicación.
*   `docker-compose up -d`: Inicia el contenedor en segundo plano.

Su aplicación estará accesible en `http://localhost:8080`.

Para detener y remover el contenedor:
```bash
docker-compose down
```

## Conclusión

Con este manual, ha configurado su aplicación Blazor Server con una arquitectura limpia, acceso a datos con EF Core y SQLite, y la ha preparado para ser desplegada con Docker. Recuerde que este es un punto de partida, y a medida que su aplicación crezca, podría considerar patrones más avanzados como CQRS completo, MediatR, y servicios de infraestructura más complejos.
