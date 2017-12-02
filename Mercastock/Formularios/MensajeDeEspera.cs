using System;
using System.Windows.Forms;
using RestSharp;
using System.Drawing;
using System.Collections.Generic;

namespace Mercastock.Formularios
{
    public partial class MensajeDeEspera : Form
    {
        public MensajeDeEspera()
        {
            InitializeComponent();
            this.BackColor = Color.Red;
            this.TransparencyKey = Color.Red;
        }
        public MensajeDeEspera(Action<Action<IRestResponse>> accionInicial, Action<IRestResponse> accionFinal) {
            accionInicial(accionFinal);
            Close();
        }
        public MensajeDeEspera(Action<Action<IRestResponse>, object> accionInicial,object parametros,Action<IRestResponse> accionFinal)
        {
            var addIn = Globals.ThisAddIn;
            //addIn.
            InitializeComponent();
            BackColor = Color.Red;
            TransparencyKey = Color.Red;
            accionInicial(x=> {
                switch (x.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        accionFinal(x);
                        BeginInvoke((MethodInvoker)(Close));
                        break;
                    case System.Net.HttpStatusCode.BadRequest:
                        BeginInvoke((MethodInvoker)(Close));
                        MessageBox.Show(x.Content);
                        break;
                    default:
                        BeginInvoke((MethodInvoker)(Close));
                        MessageBox.Show("Error crítico en la transacción");break;
                }
                
               

            },parametros);
            
        }
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void FormMain_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void MensajeDeEspera_FormClosing(object sender, FormClosingEventArgs e)
        {
            MenuRibbon._listaBools["btAjustarInventario"] = true;
        }
    }
}
