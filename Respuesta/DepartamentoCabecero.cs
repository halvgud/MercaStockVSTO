using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace Entidad
{
    
    public class DepartamentoCabecero
    {
        public string Departamento { get; set; }
        //public string UltimoInventario { get; set; }
        public double ArticulosInventariados { get; set; }
        public double ArticulosSinAjuste { get; set; }
        public double Acertado { get; set; }
        public double Fallado { get; set; }
        public double Restante { get; set; }
        public double Perdida { get; set; }
        [JsonProperty("%")]
        public double Porcentaje { get; set; }
    }

    public class DepartamentoDetalle
    {
        public string Departamento { get; set; }
        public string UltimoAjuste { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public double CantidadActualenSistema { get; set; }
        public double CantidadSisalMomentodelInventario { get; set; }
        public double CantidadFisica { get; set; }
        public double Diferencia { get; set; }
        public double Perdida { get; set; }
        public double InventarioAcertado { get; set; }
    }
}
