using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;


namespace BotonesCierres
{
    public partial class frmNuevoCierre : Form
    {
        InventarioCierres Cierres = new InventarioCierres();

        public frmNuevoCierre()
        {
            InitializeComponent();
        }

        private void btnAgregarCierre_Click(object sender, EventArgs e)
        {
            bool esEntero;
            //Primero validamos que no estén vacíos los campos
            string color = this.txtColor.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(color))
            {
                Mensajes.Excepcion("Datos inválidos en el campo 'Color'");
                this.txtColor.Focus();
                this.txtColor.SelectAll();
                return;
            }
            string modelo = this.txtModelo.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(modelo))
            {
                Mensajes.Excepcion("Datos inválidos en el campo 'Modelo'");
                this.txtModelo.Focus();
                this.txtModelo.SelectAll();
                return;
            }
            esEntero = byte.TryParse(this.txtTalla.Text.Trim(), out byte talla); //Validamos que sea de su tipo
            if (esEntero == false)
            {
                Mensajes.Excepcion("Datos inválidos en el campo 'Talla'");
                this.txtTalla.Focus();
                this.txtTalla.SelectAll();
                return;
            }

            esEntero = int.TryParse(this.nCantidad.Value.ToString(), out int cantidad);
            if (esEntero == false)
            {
                Mensajes.Excepcion("Datos inválidos en el campo 'Cantidad inicial'");
                this.nCantidad.Focus();
                return;
            }

            //Añadimos el botón
            try
            {
                frmInventarioCierres wCierres = Funciones.TraerVentana<frmInventarioCierres>();
                Cierres.NuevoProducto(modelo, color, talla, cantidad);
                wCierres.LlenarColoresCierres();
                wCierres.ActualizarHistorial();
                Mensajes.Exito("Se ha añadido el nuevo cierre al inventario");
            }
            catch (ArgumentException err)
            {
                Mensajes.Excepcion(err.Message);
            }
            this.txtColor.Clear();
            this.txtModelo.Clear();
            this.txtTalla.Clear();
            this.nCantidad.Value = 0;
            this.txtColor.Focus();
        }
    }
}
