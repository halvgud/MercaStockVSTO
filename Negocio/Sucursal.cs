using System;
using Herramienta;
using Herramienta.Config;
using Datos;
using RestSharp;


namespace Negocio
{
  public  class Sucursal
    {
        public static void Consultar(Action<IRestResponse> callback)
        {
            try
            {
                var rest = new Rest(Externa.Api.UrlApi, 
                    Externa.Sucursal.Seleccionar, 
                    Method.POST, 
                    callback,
                    new{idGenerico =1}
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
