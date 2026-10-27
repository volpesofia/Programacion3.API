using ProyectoWebApi.Models;

namespace ProyectoWebApi.Data
{
    public interface IProductoRepository
    {
        IEnumerable<Producto> ObtenerTodos();
        Producto? ObtenerPorId(int id);
    }
}
