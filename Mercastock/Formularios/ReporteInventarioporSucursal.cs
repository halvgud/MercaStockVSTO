using System;
using System.Drawing;
using System.Windows.Forms;
using Negocio;
using Herramienta;
using Entidad;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;


namespace Mercastock.Formularios
{
    public partial class ReporteInventarioporSucursal : Form
    {
        private List<DepartamentoDetalle> _listadetalle;
        public ReporteInventarioporSucursal(List<DepartamentoDetalle> listadetalle)
        {
            _listadetalle = listadetalle;
            InitializeComponent();
            Sucursal.Consultar(json => {
                Opcion.CargarComboBox(this, cbSucursales,json);
            });
        }

        private void btBuscar_Click(object sender, EventArgs e)
        {
            Departamento.Consultar(json =>
            {
                BeginInvoke((Action)(() =>
                {
                    dataGridView1.DataSource = Opcion.JsonaListaGenerica<DepartamentoCabecero2>(json);
                    dataGridView1.RowHeadersVisible = false;
                    dataGridView1.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.DefaultCellStyle.Font =new Font("Microsoft Sans Serif", 12);
                    var dataGridViewColumn = dataGridView1.Columns["nombre"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Width = 210;
                    var gridViewColumn = dataGridView1.Columns["total"];
                    if (gridViewColumn != null) gridViewColumn.Width=70;
                    var viewColumn = dataGridView1.Columns["bandera"];
                    if (viewColumn != null) viewColumn.Width = 90;
                    var column = dataGridView1.Columns["costo"];
                    if (column != null) column.Width = 110;
                    DataGridViewCellStyle style =
                           dataGridView1.ColumnHeadersDefaultCellStyle;
                    style.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
                    dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }));
               
            }, new ReporteGenerico
            {
                idSucursal = cbSucursales.SelectedValue.ToString(),
                fechaInicio = dtFechaIni.Value,
                fechaFin = dtFechaFin.Value,
                concepto = 2

            });
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var nombretipo = dataGridView1.Rows[e.RowIndex].Cells["nombre"].Value;
            // ReSharper disable once PossibleNullReferenceException
            if (nombretipo!=null || nombretipo.ToString()!="")
            { 
                Departamento.DetalleDepartamento(json =>
                {
                   _listadetalle=Opcion.JsonaListaGenerica<DepartamentoDetalle>(json);
                },
                new ReporteGenerico
                {
                    idSucursal = cbSucursales.SelectedValue.ToString(),
                    fechaInicio = dtFechaIni.Value,
                    fechaFin = dtFechaFin.Value,
                    fecha = dtFechaFin.Value,
                    busqueda =1,
                    idConcepto =2

                });
                
            }

            var addIn = Globals.ThisAddIn;
            addIn.DetalleDepartamento(_listadetalle);
        }
    }
}