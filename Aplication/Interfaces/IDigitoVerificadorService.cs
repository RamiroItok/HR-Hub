using System.Collections.Generic;
using System.Data;

namespace Aplication.Interfaces
{
    public interface IDigitoVerificadorService
    {
        List<string> VerificarDV();
        bool RecalcularDV();
        bool CalcularDVTabla(string tabla);
        DataTable ObtenerTabla(string tabla);
    }
}