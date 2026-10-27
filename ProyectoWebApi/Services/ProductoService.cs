using ProyectoWebApi.Data;
using ProyectoWebApi.Models;

namespace ProyectoWebApi.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Producto> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }

        public Producto? ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }
    }
}




