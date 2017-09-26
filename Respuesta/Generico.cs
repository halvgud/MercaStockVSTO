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
        public DateTime fechaIni { get; set; }
        public DateTime fechaFin { get; set; }
    }
}
