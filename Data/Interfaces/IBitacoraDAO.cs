using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IBitacoraDAO
    {
        int RegistrarBitacora(Bitacora bitacora);
        bool BajaBitacora(string fechaIni, string fechaFin);
    }
}