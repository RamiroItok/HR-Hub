using Models;
using System.Data;

namespace Data.Interfaces
{
    public interface IBitacoraDAO
    {
        int RegistrarBitacora(Bitacora bitacora);
        void BajaBitacora(int id);
        DataSet ListarEventos();
    }
}