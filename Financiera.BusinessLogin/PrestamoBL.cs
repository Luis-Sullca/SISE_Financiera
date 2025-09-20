using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financiera.Data;
using Financiera.Model;
using Microsoft.Extensions.Configuration;

namespace Financiera.BusinessLogin
{
    public class PrestamoBL
    {
        private readonly PrestamoRepositorio prestamoBD;
        private readonly CuotaPrestamoRepositorio cuotaBD;
        private readonly ClienteRepositorio clienteBD;
        private readonly TipoClienteRepositorio tipoClienteBD;

        public PrestamoBL(IConfiguration config)
        {
            prestamoBD = new PrestamoRepositorio(config);
            cuotaBD = new CuotaPrestamoRepositorio(config);
            clienteBD = new ClienteRepositorio(config);
            tipoClienteBD = new TipoClienteRepositorio(config);
        }

        public List<Prestamo> Listar()
        {
            return prestamoBD.Listar();
        }

        public int Registrar(Prestamo prestamo)
        {

            // Crear excepciones personalizadas
            // El plazo minimo debe ser de 6 meses
            if (prestamo.Plazo < 6)
            {
                // Propaga / lanza 
                throw new Exception("El plazo mínimo es de 6 meses");
            }

            // Buscar al cliente
            Cliente cliente = clienteBD.ObtenerPorID(prestamo.ClienteID);
            if (cliente == null)
            {
                throw new Exception("El cliente no existe");
            }

            // Buscar al tipo de cliente
            TipoCliente tipoCliente = tipoClienteBD.ObtenerPorID(prestamo.ClienteID);
            if (tipoCliente == null)
            {
                throw new Exception("El tipo de cliente no existe");
            }


            // Si el cliente es individual, el plazo minimo para un prestamo es de 24 meses
            if (tipoCliente.Nombre.Contains("INDIVIDUAL") && prestamo.Plazo < 24)
            {
                throw new Exception("El plazo minimo para el tipo de cliente asociado es de 24 meses");
            }

            //TODO
            // Si el cliente es coorporativo y el prestamo es el tipo Mi Negocio, entonces se le asigna un 3% menos adicional

            int nuevoID = prestamoBD.Registrar(prestamo);
            decimal cuotaMensual = prestamo.Importe / prestamo.Plazo;
            decimal porcentajeInteres = prestamo.Tasa / 100;
            decimal importeInteres = cuotaMensual * porcentajeInteres;
            CuotaPrestamo cuota;
            for (int idx = 0; idx <= prestamo.Plazo; idx++)
            {
                cuota = new CuotaPrestamo()
                {
                    PrestamoID = nuevoID,
                    NumeroCuota = idx,
                    Importe = importeInteres,
                    Estado = "P",
                    FechaPago = prestamo.FechaDeposito.AddMonths(idx)
                };

                cuotaBD.Registrar(cuota);
            }
            return nuevoID;
        }

        public Prestamo ObtenerPorID(int id)
        {
            return prestamoBD.ObtenerPorID(id);
        }


    }
}
