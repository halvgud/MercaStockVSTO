using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class AjusteArticulo
    {
        public DateTime fechaSolicitud { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public Double Costo { get; set; }
        public Double ExistenciaEjecucion { get; set; }
        public Double ExistenciaRespuesta { get; set; }
        public Double Diferencia { get; set; }
        public Double CostoActual { get; set; }
        public Double Existencia { get; set; }
    }
}
