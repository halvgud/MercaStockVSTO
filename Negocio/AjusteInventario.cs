using System;
using Herramienta;
using Herramienta.Config;
using Datos;
using RestSharp;

namespace Negocio
{
   public class AjusteInventario
    {
        public static void Consultar(Action<IRestResponse> callback, object parametros)
        {
            try
            {
                var rest = new Rest(Externa.Api.UrlApi,
                   Externa.AjusteInventario.Consultar,
                    Method.POST,
                    callback,
                    parametros
                    );
            }
            catch (Exception e)
            {
                Opcion.Log(Log.Interno.Categoria, "EXCEPCION: " + e.Message);
                // callback("CONTINUAR");
            }
        }
        public static void Ajustar(Action<IRestResponse> callback, object parametros)
        {
            try
            {
                var rest = new Rest(Externa.Api.UrlApi,
                   Externa.AjusteInventario.Ajustar,
                    Method.POST,
                    callback,
                    parametros
                    );
            }
            catch (Exception e)
            {
                Opcion.Log(Log.Interno.Categoria, "EXCEPCION: " + e.Message);
                // callback("CONTINUAR");
            }
        }
    }
}
