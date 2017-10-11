using System;

namespace Entidad
{
   public class Generico
    {
        public string Id { get; set; }
        public string idSucursal { get; set; }
        public string Nombre { get; set; }
    }
    public class ReporteGenerico
    {
        public string idSucursal { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public DateTime fechaIni { get; set; }
        public DateTime fecha { get; set; }
        public int concepto { get; set; }
        public  int departamento { get; set; }
        public int busqueda { get; set; }
        public int idConcepto { get; set; }
    }

}
