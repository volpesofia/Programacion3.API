using Microsoft.AspNetCore.Mvc;
using ProyectoWebApi.Data;
using ProyectoWebApi.Services;

namespace ProyectoWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var productos = _productoService.ObtenerTodos();
                return Ok(productos);
            }
            catch (DataAccessException)
            {
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var producto = _productoService.ObtenerPorId(id);

                if (producto == null)
                    return NotFound();

                return Ok(producto);
            }
            catch (DataAccessException)
            {
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}