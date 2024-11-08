using System.Collections.Generic;
using System.Data;

namespace Data.Interfaces
{
    public interface IDigitoVerificadorDAO
    {
        List<string> Verificar_DV();
        bool Recalcular_DV();
        bool CalcularDVTabla(string tablaObjetivo);
        DataTable ObtenerTabla(string tabla);
    }
}
