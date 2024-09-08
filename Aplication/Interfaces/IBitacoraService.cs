using Models;
using Models.Enums;
using System.Collections.Generic;

namespace Aplication.Interfaces
{
    public interface IBitacoraService
    {
        int AltaBitacora(string email, Puesto tipoUsuario, string descripcion, Criticidad criticidad);
        int BajaBitacora(string fechaIni, string fechaFin);
        List<Bitacora> ListarEventos();
    }
}