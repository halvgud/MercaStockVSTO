using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
   public class DepartamentoCabecero
    {
        public string Departamento { get; set; }
        public double TotaldeProductoInventariado { get; set; }
        public double TotaldeProductoSinAjuste { get; set; }
        public double Acertado { get; set; }
        public double Fallado { get; set; }
        public double Restante { get; set; }
        public double Perdida { get; set; }
        public double InventarioAcertado { get; set; }
    }
    public class DepartamentoCabecero2
    {
        public string nombre { get; set; }
        public double total { get; set; }
        public double totalAcertado { get; set; }
        public double totalFallado { get; set; }
        public double totalRestante { get; set; }
        public double costo { get; set; }
        public double bandera { get; set; }
        public double detalle { get; set; }
    }
    public class DepartamentoDetalle
    {
        public string fechaSolicitud { get; set; }
        public string clave { get; set; }
        public string descripcion { get; set; }
        public double existenciaSolicitud { get; set; }
        public double existenciaEjecucion { get; set; }
        public double existenciaRespuesta { get; set; }
        public double costo { get; set; }
        public double bandera { get; set; }
        public double porcetaje { get; set; }
    }
}
