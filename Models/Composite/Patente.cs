using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Composite
{
    public class Patente : Componente
    {
        private IList<Componente> _hijos;

        public Patente()
        {
            _hijos = new List<Componente>();
        }

        public override IList<Componente> Hijos
        {
            get
            {
                return _hijos.ToArray();
            }
        }
        public override void AgregarHijo(Componente componente)
        {
            _hijos.Add(componente);
        }
        public override void VaciarHijos()
        {
        }
        public override void BorrarHijo(Componente componente)
        {
        }
    }
}
