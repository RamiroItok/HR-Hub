using System.Data;

namespace Data.Interfaces
{
    public interface IDigitoVerificadorDAO
    {
        string Verificar_DV();
        bool Recalcular_DV();
        DataTable ObtenerTabla(string tabla);
    }
}
