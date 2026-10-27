using Microsoft.AspNetCore.Mvc;
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
            var productos = _productoService.ObtenerTodos();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var producto = _productoService.ObtenerPorId(id);

            if (producto == null)
                return NotFound();

            return Ok(producto);
        }
    }
}