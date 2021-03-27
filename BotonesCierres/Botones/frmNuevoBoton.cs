using System;
using System.Collections;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;


namespace BotonesCierres
{
    public partial class frmNuevoBoton : Form
    {
        InventarioBotones Botones = new InventarioBotones(); //Creamos el objeto tipo InventarioBotones

        public frmNuevoBoton()
        {
            InitializeComponent();
        }

        private void btnAgregarBoton_Click(object sender, EventArgs e)
        {
            bool esEntero;
            //Validamos que sean numeros de sus respectivos tipos y los guardamos
            esEntero = short.TryParse(this.txtEstilo.Text.Trim(), out short modelo);
            //Ahora validamos que no estén vavíos y que la comprobación haya sido verdadera
            if (esEntero == false)//Ahora validamos que no estén vavíos y que la comprobación haya sido verdadera
            {
                MessageBox.Show("Datos inválidos en el campo 'Modelo'");
                this.txtEstilo.Focus();
                this.txtEstilo.SelectAll();
                return;
            }
            //Validamos que sean numeros de sus respectivos tipos y los guardamos
            string color = this.txtColor.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(color))//Ahora validamos que no estén vavíos y que la comprobación haya sido verdadera
            {
                MessageBox.Show("Datos inválidos en el campo 'Color'");
                this.txtColor.Focus();
                this.txtColor.SelectAll();
                return;
            }
            //Validamos que sean numeros de sus respectivos tipos y los guardamos
            esEntero = byte.TryParse(this.txtTalla.Text.Trim(),out byte talla);
            if (esEntero == false)//Ahora validamos que no estén vavíos y que la comprobación haya sido verdadera
            {
                MessageBox.Show("Datos inválidos en el campo 'Talla'");
                this.txtTalla.Focus();
                this.txtTalla.SelectAll();
                return;
            }
            //Validamos que sean numeros de sus respectivos tipos y los guardamos
            esEntero = int.TryParse(this.nCantidad.Value.ToString(), out int cantidad);
            if (esEntero==false)//Ahora validamos que no estén vavíos y que la comprobación haya sido verdadera
            {
                MessageBox.Show("Datos inválidos en el campo 'Cantidad inicial'");
                this.nCantidad.Focus();
                return;
            }

            //Añadimos el botón
            try
            {
                //Consigue instancia de ventana de frmInventarioBotones
                frmInventarioBotones frmInventarios = Funciones.TraerVentana<frmInventarioBotones>();
                Botones.NuevoProducto(modelo, color, talla, cantidad);
                frmInventarios.LlenarEstilosBotones();
                frmInventarios.ActualizarHistorial();
                MessageBox.Show("Se ha añadido el nuevo botón al inventario", "Botón añadido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.txtEstilo.Clear();
            this.txtColor.Clear();
            this.txtTalla.Clear();
            this.nCantidad.Value = 0;
            this.txtEstilo.Focus();
        }
    }
}
