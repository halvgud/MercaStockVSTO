﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;


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