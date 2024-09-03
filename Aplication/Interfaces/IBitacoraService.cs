using Models;
using Models.Enums;
using System.Collections.Generic;
using System.Data;

namespace Aplication.Interfaces
{
    public interface IBitacoraService
    {
        int AltaBitacora(string email, Puesto tipoUsuario, string descripcion, Criticidad criticidad);
        int BajaBitacora(string fechaIni, string fechaFin);
        List<Bitacora> ListarEventos();
        DataTable ListarEventoBetween(string fecha_ini, string fecha_fin);
        DataTable ListarEventoBetweenUsuario(string fecha_ini, string fecha_fin, string nombre_usuario);
        DataTable ListarEventoUsuario(string nombre_usuario);
        DataTable ListarEventoBetweenCritic(string fecha_ini, string fecha_fin, string critic);
        DataTable ListarEventoCrit(string critic);
        DataTable ListarEventoCritUsu(string nombre_usuario, string crit);
        DataTable ListarEventoFechaUsuCrit(string fecha_ini, string fecha_fin, string nombre_usuario, string crit);
    }
}