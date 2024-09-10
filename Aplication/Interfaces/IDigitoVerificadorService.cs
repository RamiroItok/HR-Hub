using System.Data;

namespace Aplication.Interfaces
{
    public interface IDigitoVerificadorService
    {
        string VerificarDV();
        bool RecalcularDV();
        bool CalcularDVTabla(string tabla);
        DataTable ObtenerTabla(string tabla);
    }
}