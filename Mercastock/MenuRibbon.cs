using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Mercastock;
using Microsoft.Office.Interop.Excel;
//using testVSTO2.Herramienta;
using Office = Microsoft.Office.Core;
using Mercastock.Formularios;
//using testVSTO2.Respuesta;

// TODO:  Siga estos pasos para habilitar el elemento (XML) de la cinta de opciones:

// 1: Copie el siguiente bloque de código en la clase ThisAddin, ThisWorkbook o ThisDocument.

//  protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
//  {
//      return new Ribbon1();
//  }

// 2. Cree métodos de devolución de llamada en el área "Devolución de llamadas de la cinta de opciones" de esta clase para controlar acciones del usuario,
//    como hacer clic en un botón. Nota: si ha exportado esta cinta de opciones desde el diseñador de la cinta de opciones,
//    mueva el código de los controladores de eventos a los métodos de devolución de llamada y modifique el código para que funcione con el
//    modelo de programación de extensibilidad de la cinta de opciones (RibbonX).

// 3. Asigne atributos a las etiquetas de control del archivo XML de la cinta de opciones para identificar los métodos de devolución de llamada apropiados en el código.  

// Para obtener más información, vea la documentación XML de la cinta de opciones en la Ayuda de Visual Studio Tools para Office.


namespace Mercastock
{
    [ComVisible(true)]
    public class MenuRibbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI _ribbon;

        #region Miembros de IRibbonExtensibility

        public string GetCustomUI(string ribbonId)
        {
            return GetResourceText("MercaStock.MenuRibbon.xml");
        }

        #endregion

        #region Devoluciones de llamada de la cinta de opciones
        //Cree aquí métodos de devolución de llamada. Para obtener más información sobre los métodos de devolución de llamada, visite http://go.microsoft.com/fwlink/?LinkID=271226.

        public void Ribbon_Load(Office.IRibbonUI ribbonUi)
        {
            _ribbon = ribbonUi;
           
        }

        readonly Dictionary<string, bool> _listaBools = new Dictionary<string, bool>();
        public void ReporteInventarioporSucursal(Object control)
        {
            ReporteInventarioporSucursal rdc = new ReporteInventarioporSucursal();
            rdc.Show();
        }

        public void AjustarInventario(Office.IRibbonControl control)
        {
            Negocio.AjusteInventario.Consultar(json =>
            {
                Globals.ThisAddIn.AjustarInventario(json);
            },new {
                idSucursal=2,
                fecha=DateTime.Now
            });
            
        }




        public bool BuscarPermiso(Office.IRibbonControl control)
        {
            return _listaBools[control.Id];
        }

        public void SetearPermiso(bool isEnabled, string id)
        {
            _listaBools[id] = isEnabled;
            _ribbon.Invalidate();
        }
        public void AbrirConfiguracion(Object control)
        {

            _ribbon.Invalidate();
        }

        #endregion

        #region Aplicaciones auxiliares

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            foreach (string t in resourceNames)
            {
                if (string.Compare(resourceName, t, StringComparison.OrdinalIgnoreCase) != 0) continue;
                using (var resourceReader = new StreamReader(asm.GetManifestResourceStream(t)))
                {
                    return resourceReader.ReadToEnd();
                }
            }
            return null;
        }

        #endregion

        public void AbrirRecetario(Object control)
        {
           //hisAddIn.AgregaraLista.Visible = true;
        }
    }
}
