using Financiera.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace Financiera.Data
{
    public class ClienteRepositorio : IGeneric<Cliente>
    {

        private readonly string cadenaConexion;

        public ClienteRepositorio(IConfiguration config)
        {
            cadenaConexion = config["ConnectionStrings:BD"] ?? "";
        }

        public bool Actualizar(Cliente entidad)
        {
            bool exito = false;
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var command = new SqlCommand(
                    @"UPDATE Clientes SET Apellidos=@ape, Nombres=@nom,
                        Direccion=@dire, Telefono=@tel, Email=@email, TipoClienteID=@tipoclienteID
                      WHERE ID = @id", conexion))
                {
                    command.Parameters.AddWithValue("@ape", entidad.Apellido);
                    command.Parameters.AddWithValue("@nom", entidad.Nombre);
                    command.Parameters.AddWithValue("@dire", entidad.Direccion);
                    command.Parameters.AddWithValue("@tel", entidad.Telefono);
                    command.Parameters.AddWithValue("@email", entidad.Email);
                    command.Parameters.AddWithValue("@tipoclienteID", entidad.TipoclienteID);
                    exito = command.ExecuteNonQuery() > 0;
                }
            }
            return exito;
        }

        public bool Eliminar(int ID)
        {
            bool exito = false;
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var command = new SqlCommand(
                    @"DELETE FROM Clientes Where ID = @id", conexion))
                {
                    command.Parameters.AddWithValue("@id", ID);
                    exito = command.ExecuteNonQuery() > 0;
                }
            }
            return exito;
        }

        public List<Cliente> Listar()
        {
            var listado = new List<Cliente>();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("ListarClientes", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    using(SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            while (lector.Read())
                            {
                                listado.Add(new Cliente()
                                {
                                    ID = lector.GetInt32(0),
                                    Apellido = lector.GetString(1),
                                    Nombre = lector.GetString(2),
                                    Direccion = lector.GetString(3),
                                    Telefono = lector.GetString(4),
                                    Email = lector.GetString(5),
                                    TipoclienteID = lector.GetInt32(6),
                                    Activo = lector.GetBoolean(7)
                                });
                            }
                        }
                    }
                }
            }
            return listado;
        }

        public List<TipoCliente> ListarTipoCliente()
        {
            var tipoCliente = new List<TipoCliente>();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                string sql = "SELECT ID, Nombre FROM TiposCliente WHERE Activo = 1";
                using (var comando = new SqlCommand(sql, conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            tipoCliente.Add(new TipoCliente
                            {
                                ID = lector.GetInt32(0),
                                Nombre = lector.GetString(1)
                            });
                        }
                    }
                }
            }
            return tipoCliente;
        }

        public Cliente ObtenerPorID(int id)
        {
            Cliente cliente = null;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("ObtenerCliente", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            lector.Read();
                            cliente = new Cliente()
                            {
                                ID = lector.GetInt32(0),
                                Apellido = lector.GetString(1),
                                Nombre = lector.GetString(2),
                                Direccion = lector.GetString(3),
                                Telefono = lector.GetString(4),
                                Email = lector.GetString(5),
                                TipoclienteID = lector.GetInt32(6),
                                Activo = lector.GetBoolean(7)
                            };
                        }
                    }
                }
            }
            return cliente;
        }

        public int Registrar (Cliente entidad)
        {
            int nuevoID = 0;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("RegistrarCliente", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Apellidos", entidad.Apellido);
                    comando.Parameters.AddWithValue("@Nombres", entidad.Nombre);
                    comando.Parameters.AddWithValue("@Direccion", entidad.Direccion);
                    comando.Parameters.AddWithValue("@Telefono", entidad.Telefono);
                    comando.Parameters.AddWithValue("@Email", entidad.Email);
                    comando.Parameters.AddWithValue("@Tipo", entidad.TipoclienteID);
                    
                    nuevoID = Convert.ToInt32(comando.ExecuteScalar());
                }
            }
            return nuevoID;
        }
    }
}
