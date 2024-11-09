using Models;
using Models.Enums;
using System.Collections.Generic;

namespace Aplication.Interfaces
{
    public interface IBitacoraService
    {
        int AltaBitacora(string email, Puesto? tipoUsuario, string descripcion, Criticidad criticidad);
        void BajaBitacora(List<Bitacora> listaBitacora, Usuario userSession);
        List<Bitacora> ListarEventos();
    }
}