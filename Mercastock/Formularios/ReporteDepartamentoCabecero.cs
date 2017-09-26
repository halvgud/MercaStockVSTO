using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Negocio;
using Herramienta;
using Entidad;

namespace Mercastock.Formularios
{
    public partial class ReporteDepartamentoCabecero : Form
    {
        public ReporteDepartamentoCabecero()
        {
            InitializeComponent();
            Sucursal.Consultar(json => {
                Opcion.CargarComboBox(this, cbSucursales, json);
            });
        }

        private void btBuscar_Click(object sender, EventArgs e)
        {
            Departamento.Consultar(json =>
            {
                dataGridView1.DataSource = Opcion.JsonaListaGenerica<object>(json);
            }, new ReporteGenerico
            {
                idSucursal = cbSucursales.SelectedValue.ToString(),
                fechaIni = dtFechaIni.Value,
                fechaFin = dtFechaFin.Value
            });
        }
    }
}