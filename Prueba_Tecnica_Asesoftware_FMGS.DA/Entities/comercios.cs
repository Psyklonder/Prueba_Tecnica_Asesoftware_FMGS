using System;
using System.Collections.Generic;

namespace Prueba_Tecnica_Asesoftware_FMGS.DA.Entities
{
    public partial class comercios
    {
        public comercios()
        {
            servicios = new HashSet<servicios>();
        }

        public int id_comercio { get; set; }
        public string nom_comercio { get; set; } = null!;
        public int aforo_maximo { get; set; }

        public virtual ICollection<servicios> servicios { get; set; }
    }
}
