using Microsoft.Data.SqlClient;
using ProyectoWebApi.Models;

namespace ProyectoWebApi.Data
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<ProductoRepository> _logger;

        public ProductoRepository(IConfiguration configuration, ILogger<ProductoRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
               ?? throw new InvalidOperationException("Falta la connection string.");
            _logger = logger;
        }

        public IEnumerable<Producto> ObtenerTodos()
        {
            try
            {
                var productos = new List<Producto>();

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand(
                    "SELECT Id, Nombre, Precio, Stock FROM Productos", connection);

                connection.Open();
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    productos.Add(new Producto
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Precio = reader.GetDecimal(2),
                        Stock = reader.GetInt32(3)
                    });
                }

                return productos;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error de SQL al obtener todos los productos.");
                throw new DataAccessException("Error al acceder a la base de datos.", ex);
            }
        }

        public Producto? ObtenerPorId(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand(
                    "SELECT Id, Nombre, Precio, Stock FROM Productos WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                using var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Producto
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Precio = reader.GetDecimal(2),
                        Stock = reader.GetInt32(3)
                    };
                }

                return null;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error de SQL al obtener el producto {Id}.", id);
                throw new DataAccessException("Error al acceder a la base de datos.", ex);
            }
        }
    }
}