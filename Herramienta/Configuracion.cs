using System;
using System.Net.NetworkInformation;

namespace Herramienta
{
    namespace Config
    {
        public static class Log
        {
            public static class Interno
            {
                public static string Articulo { get; set; } = "articulo.log";
                public static string Categoria { get; set; } = "categoria.log";
                public static string Departamento { get; set; } = "departamento.log";
            }        
        }
        public static class Externa
        {

            public static class Api
            {
                public static string UrlApi { get; set; } = "http://localhost:8080/APIMercaStock/public/";
                //public static string UrlApi { get; set; } = "http://mercastock.mercatto.mx/API2/public/";
                public static string IdSucursal { get; set; } = Properties.Settings.Default.IdSucursal;
            }
            public static class Articulo
            {
                public static string Lista { get; set; } = Properties.Settings.Default.ArticuloLista;
                public static string IdArticulo { get; set; } = "";

                public static class Tipo
                {
                    public static string Seleccionar { get; set; } = Properties.Settings.Default.TipoSeleccionar;
                    public static string Guardar { get; set; } = Properties.Settings.Default.TipoGuardar;
                }

             
            }
           
      

            public static class Categoria
            {
                public static string Lista { get; set; } = Properties.Settings.Default.CategoriaLista;
            }



            public static class Departamento
            {
                public static string InventarioPorDepartamento { get; set; } = "inventario/reporte/cabecero";
                public static string DepartamentoDetalle { get; set; } = "inventario/reporte/detalle";
            }
            public static class AjusteInventario
            {
                public static string Consultar { get; set; } = "ajuste/seleccionar/todo";
                public static string Ajustar { get; set; } = "ajuste/insertar";
            }
            public static class Parametro
            {
                public static string UrlImportar { get; set; }
            }

            public static class Permiso
            {
                public static string Obtener { get; set; } = Properties.Settings.Default.PermisoObtener;
            }
        

            
            public static class Sucursal
            {
                public static string Seleccionar { get; set; } = "sucursal/seleccionar";
            }
        }
    }
}
