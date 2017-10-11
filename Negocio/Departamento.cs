using System;
using Herramienta;
using Herramienta.Config;
using Datos;
using RestSharp;

namespace Negocio
{
    public class Departamento
    {
        public static void Consultar(Action<IRestResponse> callback,object parametros)

        {
            try
            {
                var rest = new Rest(Externa.Api.UrlApi,
                    Externa.Departamento.InventarioPorDepartamento,
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
        public static void DetalleDepartamento(Action<IRestResponse> callback, object parametros)

        {
            try
            {
                var rest = new Rest(Externa.Api.UrlApi,
                    Externa.Departamento.DepartamentoDetalle,
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
