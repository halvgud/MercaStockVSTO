using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class AjusteArticulo
    {
        public double idInventario { get; set; }
        public DateTime fechaSolicitud { get; set; } //1
        public string Clave { get; set; }  //2
        public string Descripcion { get; set; } //3
        public Double Costo { get; set; } //4
        public Double ExistenciaEjecucion { get; set; } //5
        public Double ExistenciaRespuesta { get; set; } //6
        public Double Diferencia { get; set; } //7
        public Double CostoActual { get; set; }  //9
        public Double Existencia { get; set; } //10
        public string Ajustar { get; set; }//11
        public Double idUsuario { get; set; }
    }
}
