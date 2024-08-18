using System.Collections.Generic;
using System.Linq;

namespace Models.Composite
{
    public class Familia : Componente
    {
        private IList<Componente> _hijos;
        public Familia()
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

        public override void VaciarHijos()
        {
            _hijos = new List<Componente>();
        }

        public override void AgregarHijo(Componente componente)
        {
            _hijos.Add(componente);
        }

        public override void BorrarHijo(Componente componente)
        {
            _hijos.Remove(componente);
        }
    }
}
