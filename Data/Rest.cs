using RestSharp;
using Herramienta;
using System;
using System.Net;
namespace Datos
{
    public class Rest
    {
        public RestClient Cliente;
        public RestRequest Peticion;
        public Rest(string urlApi, string urlMetodo, Method tipo, Action<IRestResponse> callback)
        {
            RestBody(urlApi, urlMetodo, tipo, callback, null);

        }
        public Rest(string urlApi, string urlMetodo, Method tipo, Action<IRestResponse> callback,object parameters)
        {
            RestBody(urlApi, urlMetodo, tipo, callback, parameters);
        }
        private void RestBody(string urlApi, string urlMetodo, Method tipo, Action<IRestResponse> callback, object parameters)
        {
            Cliente = new RestClient(urlApi);
            Peticion = new RestRequest(urlMetodo, tipo);
            Peticion.AddJsonBody(parameters);
            Peticion.AddHeader(Constantes.Http.ObtenerTipoDeContenido,
                    Constantes.Http.TipoDeContenido.Json);
            Peticion.AddHeader(Constantes.Http.Autenticacion, "d93a1e0929c63a72d437ab44bcddcd60");
            Cliente.ExecuteAsync(Peticion, response =>
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        callback(response);
                        break;
                    default:
                        callback(null);
                        break;
                }
            });
        }
    }
}
