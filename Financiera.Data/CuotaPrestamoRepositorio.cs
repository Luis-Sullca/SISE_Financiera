using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financiera.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Financiera.Data
{
    public class CuotaPrestamoRepositorio
    {
        public readonly string cadenaConexion;

        public CuotaPrestamoRepositorio(IConfiguration config)
        {
            cadenaConexion = config["ConnectionStrings:BD"] ?? "";
        }

        public List<CuotaPrestamo> Listar()
        {
            var listado = new List<CuotaPrestamo>();

            return listado;
        }

        public bool Registrar(CuotaPrestamo entidad)
        {
            bool exito = false;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("RegistrarCuotas", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@prestamo", entidad.PrestamoID);
                    comando.Parameters.AddWithValue("@numero", entidad.NumeroCuota);
                    comando.Parameters.AddWithValue("@importe", entidad.Importe);
                    comando.Parameters.AddWithValue("@importeInteres", entidad.ImporteInteres);
                    comando.Parameters.AddWithValue("@fechaPago", entidad.FechaPago);
                    exito = comando.ExecuteNonQuery() > 0;
                }
            }
            return exito;
        }
    }
}
