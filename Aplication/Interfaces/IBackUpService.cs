using Models;

namespace Aplication.Interfaces
{
    public interface IBackUpService
    {
        string RealizarBackup(string ruta, string nombre, Usuario usuario);
        string RealizarRestore(string ruta, Usuario usuario);
        bool CrearBaseDeDatos();
    }
}