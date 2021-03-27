using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;


namespace BotonesCierres
{
    public partial class frmInventarioCierres : Form
    {
        public static DataSet ds; //Dataset para almacenar el resultado de la consulta
        public static string sql;
        public static InventarioCierres Cierres = new InventarioCierres();

        public frmInventarioCierres()
        {
            InitializeComponent();
        }

        private void frmInventarioCierres_Load(object sender, EventArgs e)
        {
            LlenarColoresCierres();
            ActualizarHistorial();
        }

        public void LlenarColoresCierres()
        {
            this.cbColor.Items.Clear();
            //Hacemos una consulta a la DB para traer la info de estilos
            ds = Conexion.Ejecutar("SELECT DISTINCT(Color) FROM inv_cierres ORDER BY Color ASC;");
            if (ds.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    this.cbColor.Items.Add(r["Color"]);
                }
            }
            else
            {
                this.cbColor.Enabled = false;
            }

            //Bloqueamos los combos de color y talla (ya que primero tienen que elegir el estilo del boton)
            this.cbModelo.Enabled = false;
            this.cbTalla.Enabled = false;

            this.rbBolsas.PerformClick(); //Activamos la opción de bolsas
        }

        public void ActualizarHistorial()
        {
            ds = Cierres.UltimosEventosLog(20);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return; //Salimos de la función si nos devuelve 0

            dgHistorial.DataSource = ds.Tables[0];
            dgHistorial.Columns[0].HeaderText = "Tipo";
            dgHistorial.Columns[1].HeaderText = "Fecha";
            dgHistorial.Columns[2].HeaderText = "Color";
            dgHistorial.Columns[3].HeaderText = "Modelo";
            dgHistorial.Columns[4].HeaderText = "Talla";
            dgHistorial.Columns[5].HeaderText = "Antes";
            dgHistorial.Columns[6].HeaderText = "Valor";
            dgHistorial.Columns[7].HeaderText = "Después";

            foreach (DataGridViewColumn col in this.dgHistorial.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            this.dgHistorial.RowHeadersVisible = false;
        }

        private void cbColor_SelectedValueChanged(object sender, EventArgs e)
        {
            this.cbModelo.Items.Clear(); //Limpiamos el combobox modelo
            this.cbTalla.Items.Clear(); //Limpiamos el combobox tallas
            this.txtExistencia.Clear(); //Limpiamos la existencia

            this.cbModelo.Enabled = false;
            this.cbTalla.Enabled = false;
            //Hacemos la consulta para buscar los modelos del color seleccionado
            sql = string.Format("SELECT DISTINCT(Modelo) FROM inv_cierres WHERE Color='{0}' ORDER BY Modelo ASC;", this.cbColor.Text);
            ds = Conexion.Ejecutar(sql);

            if (ds.Tables[0].Rows.Count != 0)
            {
                //Rellenamos el combobox en caso de que si haya registros
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    this.cbModelo.Items.Add(r["Modelo"]);
                }
                this.cbModelo.Enabled = true; //Activamos el combobox
            }
            else
            {
                this.cbModelo.Enabled = false; //En caso de que no haya registros (imposible) lo desactivamos
            }
        }

        private void cbModelo_SelectedValueChanged(object sender, EventArgs e)
        {
            this.cbTalla.Items.Clear(); //Limpiamos el combobox color
            this.txtExistencia.Clear();

            this.cbTalla.Enabled = false;
            //Hacemos la consulta para buscar las tallas del modelo seleccionado
            sql = string.Format("SELECT DISTINCT(Talla) FROM inv_cierres WHERE Color='{0}' AND Modelo='{1}' ORDER BY Talla ASC;", this.cbColor.Text, this.cbModelo.Text);
            ds = Conexion.Ejecutar(sql);

            if (ds.Tables[0].Rows.Count != 0)
            {
                //Rellenamos el combobox en caso de que si haya registros
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    this.cbTalla.Items.Add(r["Talla"]);
                }
                this.cbTalla.Enabled = true; //Activamos el combobox
            }
            else
            {
                this.cbTalla.Enabled = false; //En caso de que no haya registros (imposible) lo desactivamos
            }
        }

        private void cbTalla_SelectedValueChanged(object sender, EventArgs e)
        {
            this.txtExistencia.Text = Cierres.Existencia(this.cbModelo.Text, this.cbColor.Text, byte.Parse(this.cbTalla.Text)).ToString();
        }

        private void rbBolsas_Click(object sender, EventArgs e)
        {
            this.lblBolsas.Visible = true;
            this.nBolsas.Visible = true;
            this.lblCierresBolsas.Text = "Cierres por bolsa";
        }

        private void rbUnidad_Click(object sender, EventArgs e)
        {
            this.lblBolsas.Visible = false;
            this.nBolsas.Visible = false;
            this.lblCierresBolsas.Text = "Cierres";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int cantidad;
            //Verificamos que no estén vacíos los campos de la información del botón
            if (string.IsNullOrEmpty(cbColor.Text)) return; //Validamos estilo
            if (string.IsNullOrEmpty(cbModelo.Text)) return; //Validamos el color
            if (string.IsNullOrEmpty(cbTalla.Text)) return; //Validamos la talla
            if (this.nCierresBolsas.Value == 0) return; //Validamos la cantidad de botones por bolsa

            string modelo = this.cbModelo.Text;
            string color = this.cbColor.Text;
            byte talla = byte.Parse(this.cbTalla.Text.ToString());

            //Verificamos cuál opción está seleccionadda (Por bolsa o por unidad)
            if (this.rbBolsas.Checked)
            {
                if (this.nBolsas.Value == 0) return; //Validamos el numero de bolsas
                
                cantidad = decimal.ToInt32(this.nBolsas.Value) * decimal.ToInt32(this.nCierresBolsas.Value);//Sumamos la anterior con la cantidad que se está agregando para actualizar el inventario
                Cierres.AgregarUnidades(modelo, color, talla, cantidad); //Añadimos los botones al inventario
            }
            else
            {
                cantidad = decimal.ToInt32(this.nCierresBolsas.Value); //Sumamos la anterior con la cantidad que se quiere añadir para actualizar el inventario
                Cierres.AgregarUnidades(modelo, color, talla, cantidad);
            }

            this.txtExistencia.Text = Cierres.Existencia(modelo,color,talla).ToString(); //Actualizamos el inventario para mostrarlo en el formulario
            MessageBox.Show("Se han agregado " + cantidad + " cierres al inventario");
            this.nBolsas.Value = 0;
            this.nCierresBolsas.Value = 0;
        }

        private void btnDescontar_Click(object sender, EventArgs e)
        {
            int cantidad;
            //Verificamos que no estén vacíos los campos de la información del botón
            if (string.IsNullOrEmpty(this.cbColor.Text)) return; //Validamos estilo
            if (string.IsNullOrEmpty(this.cbModelo.Text)) return; //Validamos el color
            if (string.IsNullOrEmpty(this.cbTalla.Text)) return; //Validamos la talla
            if (this.nCierresBolsas.Value == 0) return; //Validamos la cantidad de botones por bolsa

            string modelo = this.cbModelo.Text;
            string color = this.cbColor.Text;
            byte talla = byte.Parse(this.cbTalla.Text.ToString());

            //Verificamos cuál opción está seleccionadda (Por bolsa o por unidad)
            if (this.rbBolsas.Checked)
            {
                if (this.nBolsas.Value == 0) return; //Validamos el numero de bolsas
                
                cantidad = decimal.ToInt32(this.nBolsas.Value) * decimal.ToInt32(this.nCierresBolsas.Value);//Sumamos la anterior con la cantidad que se está agregando para actualizar el inventario
                Cierres.DescontarUnidades(modelo,color,talla,cantidad); //Añadimos los botones al inventario
            }
            else
            {
                cantidad = decimal.ToInt32(this.nCierresBolsas.Value); //Sumamos la anterior con la cantidad que se quiere añadir para actualizar el inventario
                Cierres.DescontarUnidades(modelo,color,talla,cantidad);
            }

            this.txtExistencia.Text = Cierres.Existencia(modelo,color,talla).ToString(); //Actualizamos el inventario para mostrarlo en el formulario
            MessageBox.Show("Se han descontado " + cantidad + " cierres al inventario");
            this.nBolsas.Value = 0;
            this.nCierresBolsas.Value = 0;
        }

        private void btnNuevoCierre_Click(object sender, EventArgs e)
        {
            frmNuevoCierre wNuevoCierre = new frmNuevoCierre();
            wNuevoCierre.ShowDialog(this);
        }

        private void btnAbrirInventario_Click(object sender, EventArgs e)
        {
            frmPrincipal wPrincipal = Funciones.TraerVentana<frmPrincipal>();

            try
            {
                BotonesCierres.Botones.frmCierresInv wInventarioCierres = Funciones.TraerVentana<BotonesCierres.Botones.frmCierresInv>();
                wInventarioCierres.BringToFront();
            }
            catch (IndexOutOfRangeException ex)
            {
                BotonesCierres.Botones.frmCierresInv wInventarioCierres = new BotonesCierres.Botones.frmCierresInv();
                wInventarioCierres.MdiParent = wPrincipal;
                wInventarioCierres.Show();
                this.Tag = wInventarioCierres;
                wInventarioCierres.Show();
            }
        }
    }
}
