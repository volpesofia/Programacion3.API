# Programacion3.API

Web API desarrollada con **ASP.NET Core (.NET 8)** y **C#** para la materia Programación 3. Expone el listado de productos almacenados en una base de datos **SQL Server**, accediendo a los datos con **ADO.NET** y organizando el código en capas.

## Arquitectura

```
Cliente (Swagger / Postman)
        │
        ▼
   Controller      → recibe la solicitud HTTP y devuelve la respuesta
        │
        ▼
    Service        → lógica de la aplicación
        │
        ▼
   Repository      → acceso a datos con ADO.NET (SqlConnection, SqlCommand, SqlDataReader)
        │
        ▼
   SQL Server
```

| Carpeta | Contenido |
|---|---|
| `Controllers/` | `ProductosController` |
| `Services/` | `IProductoService`, `ProductoService` |
| `Data/` | `IProductoRepository`, `ProductoRepository`, `AccesoDatosException` |
| `Models/` | `Producto` |

Las dependencias se registran en `Program.cs` con inyección de dependencias (`AddScoped`).

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 (con la carga de trabajo *ASP.NET y desarrollo web*)
- SQL Server Express (instancia `.\SQLEXPRESS`)
- SQL Server Management Studio (SSMS) u otra herramienta para ejecutar scripts SQL

## Cómo ejecutar el proyecto

### 1. Clonar el repositorio

```bash
git clone https://github.com/volpesofia/Programacion3.API.git
```

### 2. Crear la base de datos

1. Abrir SSMS y conectarse a `.\SQLEXPRESS`.
2. Abrir el archivo `Database/Programacion3.sql`.
3. Ejecutarlo completo. El script:
   - crea la base `Programacion3`,
   - crea la tabla `Productos` (`Id`, `Nombre`, `Precio`, `Stock`),
   - inserta 5 productos de prueba.

### 3. Configurar la connection string

Revisar `ProyectoWebApi/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=Programacion3;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Si tu instancia de SQL Server tiene otro nombre, reemplazá `.\\SQLEXPRESS` por el tuyo.

### 4. Ejecutar la API

1. Abrir `ProyectoWebApi.sln` en Visual Studio.
2. Presionar **F5** (o el botón verde **https**).
3. Se abre el navegador con Swagger. El puerto lo indica la consola y puede variar.

## Endpoints

| Método | Ruta | Descripción | Respuestas |
|---|---|---|---|
| GET | `/api/productos` | Devuelve todos los productos | 200, 500 |
| GET | `/api/productos/{id}` | Devuelve un producto por su Id | 200, 400, 404, 500 |

### Ejemplos de respuesta

`GET /api/productos/1` → **200 OK**

```json
{
  "id": 1,
  "nombre": "Notebook Lenovo",
  "precio": 850000.00,
  "stock": 10
}
```

`GET /api/productos/9999` → **404 Not Found**

```json
{ "mensaje": "No existe un producto con id 9999." }
```

`GET /api/productos/0` → **400 Bad Request**

```json
{ "mensaje": "El id debe ser mayor a cero." }
```

## Manejo de errores

- Un producto inexistente devuelve **404**, porque es una situación válida y no una excepción.
- Un `id` inválido (menor o igual a cero) devuelve **400**.
- Si falla el acceso a la base de datos (por ejemplo, SQL Server detenido), el Repository captura la `SqlException`, registra el detalle técnico únicamente en el log del servidor y lanza una `AccesoDatosException`. El Controller responde **500** con un mensaje genérico, sin exponer la connection string ni detalles internos:

```json
{ "mensaje": "Ocurrió un error al consultar los productos. Intente nuevamente más tarde." }
```

- Los objetos `SqlConnection`, `SqlCommand` y `SqlDataReader` se liberan con `using`, incluso cuando ocurre una excepción.

## Pruebas realizadas

Las capturas se encuentran en la carpeta `Capturas/`:

1. `GET /api/productos` → 200 con los 5 productos.
2. `GET /api/productos/1` → 200 con el producto.
3. `GET /api/productos/9999` → 404.
4. `GET /api/productos` con SQL Server detenido → 500 con mensaje genérico.

## Autora

Sofia Volpe — Tecnicatura Superior en Análisis de Sistemas e Informática, Instituto Nra. Sra. De Las Nieves.
