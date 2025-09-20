using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Financiera.Model
{
    public class Prestamo
    {
        public int ID { get; set; }
        public DateTime Fecha { get; set; }
        [DisplayName("Fecha Deposito")]
        public DateTime FechaDeposito { get; set; }
        [DisplayName("Cliente")]
        public int ClienteID { get; set; }
        [DisplayName("Tipo de Prestamo")]
        public int TipoPrestamoID { get; set; }
        public string Moneda { get; set; }
        public decimal Importe { get; set; }
        public int Plazo { get; set; }
        public decimal Tasa { get; set; }
        public string Estado { get; set; }

        public Prestamo()
        {
            Fecha = DateTime.Now;
            FechaDeposito = DateTime.Now.AddDays(7);
            Estado = "P";
        }
    }
}
