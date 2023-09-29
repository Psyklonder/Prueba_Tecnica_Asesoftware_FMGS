using System;
using System.Collections.Generic;

namespace Prueba_Tecnica_Asesoftware_FMGS.DA.Entities
{
    public partial class turnos
    {
        public long id_turno { get; set; }
        public int id_servicio { get; set; }
        public DateTime fecha_turno { get; set; }
        public TimeSpan hora_inicio { get; set; }
        public TimeSpan hora_fin { get; set; }
        public bool estado { get; set; }

        public virtual servicios id_servicioNavigation { get; set; } = null!;
    }
}
