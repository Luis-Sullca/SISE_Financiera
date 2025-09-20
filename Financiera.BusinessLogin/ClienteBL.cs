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
    public class ClienteBL
    {
        private readonly ClienteRepositorio clienteBD;

        public ClienteBL(IConfiguration config)
        {
            clienteBD = new ClienteRepositorio(config);
        }

        public List<Cliente> Listar()
        {
            return clienteBD.Listar();
        }

        public List<TipoCliente> ListarTipoCliente()
        {
            return clienteBD.ListarTipoCliente();
        }

        public int Registrar(Cliente cliente)
        {
            return clienteBD.Registrar(cliente);
        }

        public Cliente ObtenerPorID(int id)
        {
            return clienteBD.ObtenerPorID(id);
        }

        public bool Eliminar(int id)
        {
            return clienteBD.Eliminar(id);
        }

        public bool Actualizar(Cliente entidad)
        {
            return clienteBD.Actualizar(entidad);
        }
    }
}
