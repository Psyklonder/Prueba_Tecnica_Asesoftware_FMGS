using System;
using System.Collections.Generic;

namespace Prueba_Tecnica_Asesoftware_FMGS.DA.Entities
{
    public partial class servicios
    {
        public servicios()
        {
            turnos = new HashSet<turnos>();
        }

        public int id_servicio { get; set; }
        public int id_comercio { get; set; }
        public string nom_servicio { get; set; } = null!;
        public TimeSpan hora_apertura { get; set; }
        public TimeSpan hora_cierre { get; set; }
        public int duracion { get; set; }

        public virtual comercios id_comercioNavigation { get; set; } = null!;
        public virtual ICollection<turnos> turnos { get; set; }
    }
}
