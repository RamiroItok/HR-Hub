using System.Data;

namespace Aplication.Interfaces
{
    public interface IDigitoVerificadorService
    {
        string VerificarDV();
        bool RecalcularDV();
        DataTable ObtenerTabla(string tabla);
    }
}