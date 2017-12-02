using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Office = Microsoft.Office.Core;
using Mercastock.Formularios;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Linq.Expressions;


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
            
            _listaBools = new ObservableDictionary<string, bool>
            {
                { "btAjustarInventario", true },
                { "btReporteInventarioPorDepartamento", true },
                { "btReporteComparativo", true },
                { "btReporteGernerarInventario", true },
                { "subtAjustarRojo", true },
                { "subtReconteoVerde", true },
                { "subtProductoAlertaAzul", true }
            };
            _listaBools.OnAdd += new EventHandler(l_OnAdd);
            _listaBools.CollectionChanged +=l_OnAdd;
          //  _listaBools.Add("btAjustarInventario", false);

        }

        /**/
        public class ObservableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, INotifyCollectionChanged, INotifyPropertyChanged
        {
            public ObservableDictionary() : base() { }
            public ObservableDictionary(int capacity) : base(capacity) { }
            public ObservableDictionary(IEqualityComparer<TKey> comparer) : base(comparer) { }
            public ObservableDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary) { }
            public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer) { }
            public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer) { }

            public event NotifyCollectionChangedEventHandler CollectionChanged;
            public event PropertyChangedEventHandler PropertyChanged;

            public  TValue this[TKey key]
            {
                get
                {
                    return base[key];
                }
                set
                {
                    TValue oldValue;
                    bool exist = base.TryGetValue(key, out oldValue);
                    var oldItem = new KeyValuePair<TKey, TValue>(key, oldValue);
                    base[key] = value;
                    var newItem = new KeyValuePair<TKey, TValue>(key, value);
                    if (exist)
                    {
                        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItem, oldItem, base.Keys.ToList().IndexOf(key)));
                    }
                    else
                    {
                        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItem, base.Keys.ToList().IndexOf(key)));
                        this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
                    }
                }
            }
            public event EventHandler OnAdd;

            public  void Add(TKey key, TValue value)
            {
                if (null != OnAdd)
                {
                    OnAdd(this, null);
                }
                if (!base.ContainsKey(key))
                {
                    var item = new KeyValuePair<TKey, TValue>(key, value);
                    base.Add(key, value);
                    this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, base.Keys.ToList().IndexOf(key)));
                    this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
                }
            }

            public new bool Remove(TKey key)
            {
                TValue value;
                if (base.TryGetValue(key, out value))
                {
                    var item = new KeyValuePair<TKey, TValue>(key, base[key]);
                    bool result = base.Remove(key);
                    this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, base.Keys.ToList().IndexOf(key)));
                    this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
                    return result;
                }
                return false;
            }

            public new void Clear()
            {
                base.Clear();
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
            }

            protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
            {
                if (this.CollectionChanged != null)
                {
                    this.CollectionChanged(this, e);
                }
            }

            protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, e);
                }
            }
        } 

        void l_OnAdd(object sender, EventArgs e)
        {
            _ribbon.Invalidate();
        }
        void l_OnAdd(object sender, NotifyCollectionChangedEventArgs e)
        {
            _ribbon.Invalidate();
        }
        /**/



        public static ObservableDictionary<string, bool> _listaBools;
        public void ReporteInventarioporSucursal(Object control)
        {
            ReporteInventarioporSucursal rdc = new ReporteInventarioporSucursal();
            rdc.Show();
        }

        public void AjustarInventario(Office.IRibbonControl control)
        {
            _listaBools["btAjustarInventario"] = false;
            MensajeDeEspera msj = new MensajeDeEspera(
            Negocio.AjusteInventario.Consultar, new
            {
                idSucursal = 2,
                fecha = DateTime.Now
            }, Globals.ThisAddIn.AjustarInventario);
            msj.Show();     
        }
        public void AjusteRojos(Office.IRibbonControl control)
        {
            object[,] array = ThisAddIn._reporte.Range[
                         "A" + 2 + ":K" + Globals.ThisAddIn.Application.ActiveSheet.Cells.Find("*", Missing.Value,
                             Missing.Value, Missing.Value, XlSearchOrder.xlByRows,
                            XlSearchDirection.xlPrevious, false, Missing.Value,
                             Missing.Value)
                             .Row].Value2;
            List<Entidad.AjusteArticulo> output = new List<Entidad.AjusteArticulo>();
            output = Enumerable.Range(1, array.GetLength(0))
                 .Select(idx => new Entidad.AjusteArticulo
                 {
                     idInventario = (double)array[idx,1],
                     fechaSolicitud = (DateTime.Parse((array[idx, 2]).ToString())),
                     Clave = (string)array[idx, 3],
                     Descripcion = (string)array[idx, 4],
                     Costo = (double)array[idx, 5],
                     ExistenciaEjecucion = (double)array[idx, 6],
                     ExistenciaRespuesta = (double)array[idx,7],
                     Diferencia = (double)array[idx, 8],
                     CostoActual = (double)array[idx, 9],
                     Existencia = (double)array[idx, 10],
                     Ajustar = (string)((string)array[idx, 11]==null?"": (string)array[idx, 11]),
                     idUsuario=100
                 }).ToList().Where(x=>x.Ajustar=="AJUSTAR").ToList();
            /**/
            _listaBools["btAjustarInventario"] = false;
            MensajeDeEspera msj = new MensajeDeEspera(
                        Negocio.AjusteInventario.Ajustar, output, x => {
                            MessageBox.Show(@"Ajuste realizado con exito");
                        });
            msj.Show();
        }




        public bool BuscarPermiso(Office.IRibbonControl control)
        {
            return _listaBools[control.Id];
        }
        public bool BuscarVisibilidad(Office.IRibbonControl control)
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
