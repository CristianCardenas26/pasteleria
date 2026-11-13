# Backend Pastelería (C# · Arquitectura Onion)

API REST en ASP.NET Core para una pastelería: clientes, productos y órdenes.

## Tecnologías

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core (base de datos en memoria, no hay que instalar nada)
- Swagger para probar los endpoints

## Arquitectura Onion

```
        ┌──────────────────────────────┐
        │  API (Controllers)           │
        │  ┌────────────────────────┐  │
        │  │ Infrastructure (EF,    │  │
        │  │ Repositories)          │  │
        │  │  ┌──────────────────┐  │  │
        │  │  │ Application      │  │  │
        │  │  │ (Services, DTOs) │  │  │
        │  │  │  ┌────────────┐  │  │  │
        │  │  │  │  Domain    │  │  │  │
        │  │  │  └────────────┘  │  │  │
        │  │  └──────────────────┘  │  │
        │  └────────────────────────┘  │
        └──────────────────────────────┘
```

Las dependencias van siempre hacia el centro:

| Proyecto | Contiene | Depende de |
|---|---|---|
| `Pasteleria.Domain` | Entidades (Cliente, Producto, Orden) e interfaces de repositorios | Nada |
| `Pasteleria.Application` | Servicios (lógica), interfaces de servicios y DTOs | Domain |
| `Pasteleria.Infrastructure` | DbContext de EF Core e implementación de los repositorios | Domain |
| `Pasteleria.API` | Controladores y `Program.cs` (registra todo) | Application, Infrastructure |

El Domain no conoce a nadie. Application usa las interfaces de los repositorios sin saber que
por debajo está Entity Framework; eso lo decide Infrastructure.

```
src/
├── Pasteleria.Domain/
│   ├── Entities/         Cliente.cs, Producto.cs, Orden.cs
│   └── Interfaces/       IClienteRepository.cs, IProductoRepository.cs, IOrdenRepository.cs
├── Pasteleria.Application/
│   ├── DTOs/             CrearClienteDto.cs, CrearOrdenDto.cs
│   ├── Interfaces/       IClienteService.cs, IProductoService.cs, IOrdenService.cs
│   └── Services/         ClienteService.cs, ProductoService.cs, OrdenService.cs
├── Pasteleria.Infrastructure/
│   ├── Data/             PasteleriaDbContext.cs (incluye datos de prueba)
│   └── Repositories/     ClienteRepository.cs, ProductoRepository.cs, OrdenRepository.cs
└── Pasteleria.API/
    ├── Controllers/      ClientesController.cs, ProductosController.cs, OrdenesController.cs
    └── Program.cs
```

## Cómo correrlo

Con Visual Studio: abrir `Pasteleria.sln`, poner `Pasteleria.API` como proyecto de inicio y dar Run.

Con la terminal:

```
dotnet run --project src/Pasteleria.API
```

Se abre Swagger en http://localhost:5000/swagger para probar todo.
Al iniciar se cargan 5 productos y 2 clientes de prueba. Los datos se borran al apagar la app.

## Endpoints

| Método | Ruta | Descripción |
|---|---|---|
| GET | `/api/productos` | Lista de productos |
| GET | `/api/clientes` | Lista de clientes |
| POST | `/api/clientes` | Crea un cliente (queda activo) |
| DELETE | `/api/clientes/{id}` | Elimina un cliente y sus órdenes |
| GET | `/api/ordenes` | Lista de órdenes |
| POST | `/api/ordenes` | Crea una orden |
| DELETE | `/api/ordenes/{id}` | Elimina una orden |

Ejemplos listos en `src/Pasteleria.API/Pasteleria.API.http`.

### Crear cliente

```json
POST /api/clientes
{
  "nombre": "Laura Martinez",
  "telefono": "3205551234",
  "email": "laura@correo.com"
}
```

### Crear orden

```json
POST /api/ordenes
{
  "clienteId": 1,
  "productoId": 1,
  "cantidad": 2
}
```

El total se calcula solo (precio × cantidad). No se puede crear una orden para un cliente
inactivo ni con cantidad 0.
