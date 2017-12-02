using System;
using System.Drawing;
using System.Windows.Forms;
using Negocio;
using Herramienta;
using Entidad;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;


namespace Mercastock.Formularios
{
    public partial class ReporteInventarioporSucursal : Form
    {
        private List<DepartamentoDetalle> _listadetalle;
        public ReporteInventarioporSucursal()
        {
            _listadetalle =new List<DepartamentoDetalle>();
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
                    var filaprimera = new BindingList<DepartamentoCabecero>();
                    filaprimera.AddNew();
                    var list = new List<DepartamentoCabecero>(Opcion.JsonaListaGenerica<DepartamentoCabecero>(json));
                    list.AddRange((filaprimera));
                    dataGridView1.DataSource = list;
                    dataGridView1.RowHeadersVisible = false;
                    dataGridView1.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.DefaultCellStyle.Font =new Font("Microsoft Sans Serif", 11);
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

                    label6.Text = cbSucursales.Text;
                    dataGridView1.AutoResizeColumns();
                    dataGridView1.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.AllCells;
                    //var porcentaje = new DataGridViewTextBoxColumn
                    //{
                    //    Name = "%",
                    //    HeaderText = @"%",
                    //};
                    //dataGridView1.Columns.Insert(9, porcentaje);
                    var buttonColumn = new DataGridViewButtonColumn
                    {
                        Name = "Detalle",
                        HeaderText = @"Detalle",
                        Text = "Detalle",
                        UseColumnTextForButtonValue = true
                    };
                    dataGridView1.Columns.Insert(8, buttonColumn);
                    dataGridView1.Columns[8].DefaultCellStyle.BackColor = Color.Aqua;
                 

                    double suminve = 0;
                    double sumajus = 0;
                    double sumacer = 0;
                    double sumfall = 0;
                    double sumperd = 0;
                    double sumres = 0;
                    for (var x = 0; x < dataGridView1.RowCount; x++)
                    {
                        dataGridView1.Rows[x].Height = 35;
                        dataGridView1.Rows[x].Cells[8].Style.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                        var porciento =Convert.ToDouble(dataGridView1.Rows[x].Cells[7].Value) ;
                        if (porciento<=0.00)
                        {
                            dataGridView1.Rows[x].Cells[7].Style.BackColor = Color.Red;
                        }
                        if (porciento >= 0.01 && porciento <= 0.10)
                        {
                            dataGridView1.Rows[x].Cells[7].Style.BackColor = Color.DarkRed;
                        }
                        if (porciento >= 0.11 && porciento <= 0.20)
                        {
                            dataGridView1.Rows[x].Cells[7].Style.BackColor = Color.Firebrick;
                        }
                        if (porciento >= 0.21 && porciento <= 0.30)
                        {
                            dataGridView1.Rows[x].Cells[7].Style.BackColor = Color.OrangeRed;
                        }
                        if (porciento >= 0.31 && porciento <= 0.40)
                        {
                            dataGridView1.Rows[x].Cells[7].Style.BackColor = Color.DarkOrange;
                        }
                        if (porciento >= 0.41 && porciento <= 0.50)
                        {
                            dataGridView1.Rows[x].Cells[7].Style.BackColor = Color.DarkGoldenrod;
                        }
                        if (porciento >= 0.51 && porciento <= 0.60)
                        {
                            dataGridView1.Rows[x].Cells[7].Style.BackColor = Color.Goldenrod;
                        }
                        if (porciento >= 0.61 && porciento <= 0.70)
                        {
                            dataGridView1.Rows[x].Cells[7].Style.BackColor = Color.GreenYellow;
                        }
                        if (porciento >= 0.71 && porciento <= 0.80)
                        {
                            dataGridView1.Rows[x].Cells[7].Style.BackColor = Color.LawnGreen;
                        }
                        if (porciento >= 0.81 && porciento <= 0.90)
                        {
                            dataGridView1.Rows[x].Cells[7].Style.BackColor = Color.OliveDrab;
                        }
                        if (porciento >= 0.91 && porciento <= 1.00)
                        {
                            dataGridView1.Rows[x].Cells[7].Style.BackColor = Color.Olive;
                        }
                        var inve = Convert.ToDouble(dataGridView1.Rows[x].Cells["ArticulosInventariados"].Value);
                        suminve += (inve);
                        var ajus = Convert.ToDouble(dataGridView1.Rows[x].Cells["ArticulosSinAjuste"].Value);
                        sumajus += (ajus);
                        var acer = Convert.ToDouble(dataGridView1.Rows[x].Cells["Acertado"].Value);
                        sumacer += (acer);
                        var fall = Convert.ToDouble(dataGridView1.Rows[x].Cells["Fallado"].Value);
                        sumfall += (fall);
                        var res = Convert.ToDouble(dataGridView1.Rows[x].Cells["Restante"].Value);
                        sumres += (res);
                        var perd = Convert.ToDouble(dataGridView1.Rows[x].Cells["Perdida"].Value);
                        sumperd += (perd);
                    }
                    dataGridView1.Rows[dataGridView1.RowCount-1].Cells["Departamento"].Value ="TOTAL INVENTARIO:";
                    dataGridView1.Rows[dataGridView1.RowCount-1].Cells[1].Value =suminve.ToString(CultureInfo.InvariantCulture);
                    dataGridView1.Rows[dataGridView1.RowCount-1].Cells[2].Value = sumajus.ToString(CultureInfo.InvariantCulture);
                    dataGridView1.Rows[dataGridView1.RowCount-1].Cells[3].Value = sumacer.ToString(CultureInfo.InvariantCulture);
                    dataGridView1.Rows[dataGridView1.RowCount-1].Cells[4].Value = sumfall.ToString(CultureInfo.InvariantCulture);
                    dataGridView1.Rows[dataGridView1.RowCount-1].Cells[5].Value = sumres.ToString(CultureInfo.InvariantCulture);
                    dataGridView1.Rows[dataGridView1.RowCount-1].Cells[6].Value =sumperd.ToString(CultureInfo.InvariantCulture);
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[7].Value =0;
                    //dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[6].Value =;
                    dataGridView1.Rows[dataGridView1.RowCount-1].Cells[0].Style.BackColor = Color.Gray;
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].Style.BackColor = Color.Gray;
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[2].Style.BackColor = Color.Gray;
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[3].Style.BackColor = Color.Gray;
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[4].Style.BackColor = Color.Gray;
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[5].Style.BackColor = Color.Gray;
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[6].Style.BackColor = Color.Gray;
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[7].Style.BackColor = Color.Gray;
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[8].Style.BackColor = Color.Gray;
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
            var boton= dataGridView1.Rows[e.RowIndex].Cells["Detalle"].Selected;
            var nombretipo = dataGridView1.Rows[e.RowIndex].Cells["Departamento"].Value;
            // ReSharper disable once PossibleNullReferenceException
            if (boton && nombretipo!=null && nombretipo.ToString()!="TOTAL INVENTARIO:")
            { 
                Departamento.DetalleDepartamento(json =>
                {
                   _listadetalle=Opcion.JsonaListaGenerica<DepartamentoDetalle>(json);
                    var addIn = Globals.ThisAddIn;
                    addIn.DetalleDepartamento(_listadetalle);
                },
                new ReporteGenerico
                {
                    idSucursal = cbSucursales.SelectedValue.ToString(),
                    fechaInicio = dtFechaIni.Value,
                    fechaFin = dtFechaFin.Value,
                    //fecha = dtFechaFin.Value,
                    busqueda = nombretipo.ToString(),
                    idConcepto =2
                });   
            }
        }
        private void ReporteInventarioporSucursal_Load(object sender, EventArgs e)
        {
            dtFechaIni.Value = dtFechaFin.Value.AddDays(-30);
        }
    }
}