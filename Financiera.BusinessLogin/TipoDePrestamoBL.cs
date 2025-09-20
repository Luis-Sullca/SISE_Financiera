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
    public class TipoDePrestamoBL
    {
        private readonly TipoPrestamoRepositorio tipodePrestamoBD;

        public TipoDePrestamoBL(IConfiguration config)
        {
            tipodePrestamoBD = new TipoPrestamoRepositorio(config);
        }

        public List<TipoPrestamo> Listar()
        {
            return tipodePrestamoBD.Listar();
        }

        public TipoPrestamo ObtenerPorID(int id)
        {
            return tipodePrestamoBD.ObtenerPorID(id);
        }
    }
}
