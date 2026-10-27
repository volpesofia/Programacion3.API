using Microsoft.Data.SqlClient;
using ProyectoWebApi.Models;

namespace ProyectoWebApi.Data
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly string _connectionString;
        public ProductoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
               ?? throw new InvalidOperationException("Falta la connection string.");
        }
        public IEnumerable<Producto> ObtenerTodos()
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

        public Producto? ObtenerPorId(int id)
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
    }
}
    

