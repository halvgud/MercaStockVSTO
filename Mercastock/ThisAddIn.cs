using System;
using System.Collections.Generic;
using System.Linq;
using Entidad;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using Negocio;
using Herramienta;
using RestSharp;
using System.IO;
using System.Reflection;




namespace Mercastock
{
    /*
     Namespaces,clases.- UpperCamelCase
     Funciones publicas .- 
     */
    public partial class ThisAddIn
    {
        public class Objeto
        {
            public String Nombre { get; set; }

        }
        Excel.Worksheet _sheet1;
        private List<Objeto> _objeto;


        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            _objeto = new List<Objeto> {
            new Objeto { Nombre = "ReporteInventario"},
            new Objeto { Nombre= "ReporteInventarioImprimir" }
            };
            Application.WorkbookActivate +=
           Application_ActiveWorkbookChanges;
            Application.WorkbookDeactivate += Application_ActiveWorkbookChanges;
            Globals.ThisAddIn.Application.SheetSelectionChange += activeSheet_SelectionChange;
            _sheet1 = (Excel.Worksheet)Application.ActiveSheet;
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
        }
        void Application_ActiveWorkbookChanges(Excel.Workbook wb)
        {
            // TODO: Active Workbook has changed. Ribbon should be updated.    
            wb.Unprotect();
        }
        void activeSheet_SelectionChange(object sh, Excel.Range target)
        {
            _sheet1 = (Excel.Worksheet)sh;
            if (target.Row != 1 && (_objeto.FirstOrDefault(x => x.Nombre == _sheet1.Name) != null))
            {
                try
                {
                    _sheet1.Unprotect();
                    Globals.ThisAddIn.Application.Cells.Locked = false;
                    //BloquearRango(_rowCount);
                    _sheet1.Protect(AllowSorting: true, AllowFiltering: true);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                _sheet1.Unprotect();
            }
        }
        private int _rowCount;
        private Excel.Worksheet _reporte;
        public Excel.Worksheet InicializarExcelConTemplate(string nombreHoja)
        {
            try
            {
                _sheet1 = (Excel.Worksheet)Application.ActiveSheet;
                _sheet1.Unprotect();
                var found = Application.Sheets.Cast<Excel.Worksheet>().Any(sheet => sheet.Name == nombreHoja);
                var awa = Application.Workbooks.Application;//nueva app
                if (!found)
                {
                    var ows = Application.Worksheets[1];// excel actual
                    var sPath = Path.GetTempFileName(); // archivo temporal
                    File.WriteAllBytes(sPath, Properties.Resources.FICHA_RECETA);//se copia el template
                    var oTemplate = Application.Workbooks.Add(sPath); //path del template temporal  
                    var worksheet = oTemplate.Worksheets[nombreHoja] as Excel.Worksheet;//descripcion del template
                    worksheet?.Copy(After: ows); oTemplate.Close(false, missing, missing);
                    File.Delete(sPath);
                }
                _reporte = awa.Worksheets[nombreHoja] as Excel.Worksheet;//descripcion de la hoja actual   
                _reporte?.Activate();
            }
            catch (Exception e)
            {
              //  MessageBox.Show(e.Message);
            }
            return _reporte;
        }
       public void AjustarInventario(IRestResponse restResponse)
        {
            Application.ScreenUpdating = false;
            var rrg = Opcion.JsonaListaGenerica<AjusteArticulo>(restResponse);
            _rowCount = (rrg.Count + 1);
            _reporte = InicializarExcelConTemplate("AjusteInventario"); //TODO traer de la base de datos 
            var list = new List<string> { "P-1", "P-.5", "PD", "1", "4", "DESCONOCIDO" };//TODO traer de la base de datos
            var flatList = string.Join(",", list.ToArray());
            if (_reporte != null)
            {
                try
                {
                    Application.Cells.Locked = false;
                    var oRng = _reporte.Range["A1", "I" + (rrg.Count + 1)];
                    oRng.Cells.AutoFilter(1, Type.Missing, Excel.XlAutoFilterOperator.xlAnd, Type.Missing, true);
                    _sheet1.Protect(AllowSorting: true, AllowFiltering: true, UserInterfaceOnly: true);
                    _reporte.Range["A" + 2 + ":T" + _rowCount].Value2 = InicializarLista(rrg);
                    Application.ScreenUpdating = true;
                    
                }
                catch (Exception e)
                {
                  //  MessageBox.Show(e.Message);
                }
            }
            else
            {
              //  MessageBox.Show(@"No se encontraron resultados con la busqueda indicada");
            }

        }
        private static object[,] InicializarLista(List<AjusteArticulo> rrg)
        {
            var lista = new object[rrg.Count, 20];
            for (var x = 0; x < rrg.Count; x++)
            {
                lista[x, 0] = rrg[x].fechaSolicitud + "";
                lista[x, 1] = rrg[x].Clave;
                lista[x, 2] = rrg[x].Descripcion;
                lista[x, 3] = rrg[x].CostoActual;
                lista[x, 4] = rrg[x].ExistenciaEjecucion;
                lista[x, 5] = rrg[x].ExistenciaRespuesta;
                lista[x, 6] = rrg[x].Diferencia;
                lista[x, 7] = rrg[x].CostoActual;
                lista[x, 8] = rrg[x].Existencia;
               
            }
            return lista;
        }
        public void DetalleDepartamento(List<DepartamentoDetalle> listaDetalleCabecero)
        {
            
        }
        #region Código generado por VSTO

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return new MenuRibbon();
        }

        #endregion
    }
}
