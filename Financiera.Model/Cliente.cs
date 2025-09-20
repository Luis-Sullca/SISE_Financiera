namespace Financiera.Model
{
    public class Cliente
    {
        public int ID { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int TipoclienteID { get; set; }
        public bool Activo { get; set; }

        public string NombreCompleto
        {
            get { return $"{Apellido}, {Nombre}"; }
        }
    }
}
