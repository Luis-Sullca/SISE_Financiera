using Financiera.Model;

namespace Financiera.Data
{
    public interface IGeneric<T> where T : class
    {
        List<T> Listar();
        T ObtenerPorID (int ID);
        int Registrar(T entidad);
        bool Eliminar(int ID);
        bool Actualizar(T entidad);
    }
}
