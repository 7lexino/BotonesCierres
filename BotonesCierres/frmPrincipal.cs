using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Data.OleDb;


namespace BotonesCierres
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnBotones_Click(object sender, EventArgs e)
        {
            try
            {
                frmInventarioBotones wBotones = Funciones.TraerVentana<frmInventarioBotones>();
                wBotones.BringToFront();
            }
            catch (IndexOutOfRangeException ex)
            {
                frmInventarioBotones wBotones = new frmInventarioBotones();
                wBotones.MdiParent = this;
                wBotones.Show();
                this.Tag = wBotones;
                wBotones.Show();
            }

            //frmInventarioBotones wInventarioBotones = this.Controls.OfType<frmInventarioBotones>().FirstOrDefault();

            //if (wInventarioBotones != null)
            //{
            //    //Si la instancia esta minimizada la dejamos en su estado normal
            //    if (wInventarioBotones.WindowState == FormWindowState.Minimized)
            //    {
            //        wInventarioBotones.WindowState = FormWindowState.Normal;
            //    }
            //    //Posicionamos la ventana
            //    wInventarioBotones.Left = paContenedorMenu.Width + 20;
            //    wInventarioBotones.Top = 20;
            //    wInventarioBotones.BringToFront();
            //    return;
            //}

            ////Se abre el form
            //wInventarioBotones = new frmInventarioBotones();
            //wInventarioBotones.TopLevel = false;
            //this.Controls.Add(wInventarioBotones);
            //this.Tag = wInventarioBotones;
            ////Posicionamos la ventana
            //wInventarioBotones.Left = paContenedorMenu.Width + 20;
            //wInventarioBotones.Top = 20;
            //wInventarioBotones.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnReportesProduccion_Click(object sender, EventArgs e)
        {
            try
            {
                frmReporteProduccion wReportes = Funciones.TraerVentana<frmReporteProduccion>();
                wReportes.BringToFront();
            }
            catch (IndexOutOfRangeException ex)
            {
                frmReporteProduccion wReportes = new frmReporteProduccion();
                wReportes.MdiParent = this;
                wReportes.Show();
                this.Tag = wReportes;
                wReportes.Show();
            }
        }

        private void btnEtiquetas_Click(object sender, EventArgs e)
        {
            try
            {
                frmEtiquetas wEtiquetas = Funciones.TraerVentana<frmEtiquetas>();
                wEtiquetas.BringToFront();
            }
            catch (IndexOutOfRangeException ex)
            {
                frmEtiquetas wEtiquetas = new frmEtiquetas();
                wEtiquetas.MdiParent = this;
                wEtiquetas.Show();
                this.Tag = wEtiquetas;
                wEtiquetas.Show();
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Usuario.CerrarSesion(lblUsuario.Text);
            
        }

        private void btnCierres_Click(object sender, EventArgs e)
        {
            try
            {
                frmInventarioCierres wCierres = Funciones.TraerVentana<frmInventarioCierres>();
                wCierres.BringToFront();

            }
            catch (IndexOutOfRangeException ex)
            {
                frmInventarioCierres wCierres = new frmInventarioCierres();
                wCierres.MdiParent = this;
                wCierres.Show();
                this.Tag = wCierres;
                wCierres.Show();
            }
        }
    }
}
