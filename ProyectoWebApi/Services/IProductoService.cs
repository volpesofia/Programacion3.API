using ProyectoWebApi.Models;

namespace ProyectoWebApi.Services
{
    public interface IProductoService
    {
        IEnumerable<Producto> ObtenerTodos();
        Producto? ObtenerPorId(int id);
    }
}
