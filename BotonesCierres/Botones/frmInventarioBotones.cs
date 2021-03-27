using System;
using System.Data;
using System.Windows.Forms;


namespace BotonesCierres
{
    public partial class frmInventarioBotones : Form
    {
        public static DataSet ds; //Dataset para almacenar el resultado de la consulta
        public static string sql;
        public static InventarioBotones Botones = new InventarioBotones();

        public frmInventarioBotones()
        {
            InitializeComponent();
        }

        private void frmInventarioBotones_Load(object sender, EventArgs e)
        {
            LlenarEstilosBotones();
            ActualizarHistorial();
        }

        public void ActualizarHistorial()
        {
            ds = Botones.UltimosEventosLog(20);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return; //Salimos de la función si el resultado es 0 o null

            dgHistorial.DataSource = ds.Tables[0];
            dgHistorial.Columns[0].HeaderText = "Tipo";
            dgHistorial.Columns[1].HeaderText = "Fecha";
            dgHistorial.Columns[2].HeaderText = "Modelo";
            dgHistorial.Columns[3].HeaderText = "Color";
            dgHistorial.Columns[4].HeaderText = "Talla";
            dgHistorial.Columns[5].HeaderText = "Antes";
            dgHistorial.Columns[6].HeaderText = "Valor";
            dgHistorial.Columns[7].HeaderText = "Después";

            foreach (DataGridViewColumn col in dgHistorial.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgHistorial.RowHeadersVisible = false;
        }

        public void LlenarEstilosBotones()
        {
            
            cbEstilo.Items.Clear();
            //Hacemos una consulta a la DB para traer la info de estilos
            ds = Conexion.Ejecutar("SELECT DISTINCT(Modelo) FROM inv_botones ORDER BY Modelo ASC;");
            if (ds.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    cbEstilo.Items.Add(r["Modelo"]);
                }
            }
            else
            {
                cbEstilo.Enabled = false;
            }

            //Bloqueamos los combos de color y talla (ya que primero tienen que elegir el estilo del boton)
            cbColor.Enabled = false;
            cbTalla.Enabled = false;
            rbBolsas.PerformClick(); //Activamos la opción de bolsas
        }

        private void cbEstilo_SelectedValueChanged(object sender, EventArgs e)
        {
            cbColor.Items.Clear(); //Limpiamos el combobox color
            cbTalla.Items.Clear(); //Limpiamos el combobox tallas
            this.txtExistencia.Clear(); //Limpiamos la existencia

            cbColor.Enabled = false;
            cbTalla.Enabled = false;
            //Hacemos la consulta para buscar los colores del estilo seleccionado
            sql = string.Format("SELECT DISTINCT(Color) FROM inv_botones WHERE Modelo={0} ORDER BY Color ASC;", cbEstilo.Text);
            ds = Conexion.Ejecutar(sql);

            if (ds.Tables[0].Rows.Count != 0)
            {
                //Rellenamos el combobox en caso de que si haya registros
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    cbColor.Items.Add(r["Color"]);
                }
                cbColor.Enabled = true; //Activamos el combobox
            }
            else
            {
                cbColor.Enabled = false; //En caso de que no haya registros (imposible) lo desactivamos
            }
        }

        private void cbColor_SelectedValueChanged(object sender, EventArgs e)
        {
            cbTalla.Items.Clear(); //Limpiamos el combobox color
            this.txtExistencia.Clear();

            cbTalla.Enabled = false;

            try
            {
                //Hacemos la consulta para buscar los colores del estilo seleccionado
                sql = string.Format("SELECT DISTINCT(Talla) FROM inv_botones WHERE Modelo={0} AND Color='{1}' ORDER BY Talla ASC;", cbEstilo.Text, cbColor.Text);
                ds = Conexion.Ejecutar(sql);

                if (ds.Tables[0].Rows.Count != 0)
                {
                    //Rellenamos el combobox en caso de que si haya registros
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        cbTalla.Items.Add(r["Talla"]);
                    }
                    cbTalla.Enabled = true; //Activamos el combobox
                }
                else
                {
                    cbTalla.Enabled = false; //En caso de que no haya registros (imposible) lo desactivamos
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("ERROR: " + err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbTalla_SelectedValueChanged(object sender, EventArgs e)
        {
            this.txtExistencia.Text =  Botones.Existencia(short.Parse(cbEstilo.Text), cbColor.Text, byte.Parse(cbTalla.Text)).ToString();
        }

        private void rbBolsas_Click(object sender, EventArgs e)
        {
            this.lblBolsas.Visible = true;
            this.nBolsas.Visible = true;
            this.lblBotonesBolsas.Text = "Botones por bolsa";
        }

        private void rbUnidad_Click(object sender, EventArgs e)
        {
            this.lblBolsas.Visible = false;
            this.nBolsas.Visible = false;
            this.lblBotonesBolsas.Text = "Botones";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int cantidad;
            //Verificamos que no estén vacíos los campos de la información del botón
            if (string.IsNullOrEmpty(cbEstilo.Text)) return; //Validamos estilo
            if (string.IsNullOrEmpty(cbColor.Text)) return; //Validamos el color
            if (string.IsNullOrEmpty(cbTalla.Text)) return; //Validamos la talla
            if (this.nBotonesBolsas.Value == 0) return; //Validamos la cantidad de botones por bolsa

            short modelo = Int16.Parse(cbEstilo.Text.ToString());
            string color = cbColor.Text;
            byte talla = byte.Parse(cbTalla.Text.ToString());

            //Verificamos cuál opción está seleccionadda (Por bolsa o por unidad)
            if (rbBolsas.Checked) {
                if (this.nBolsas.Value == 0) return; //Validamos el numero de bolsas
                
                cantidad = decimal.ToInt32(this.nBolsas.Value) * decimal.ToInt32(this.nBotonesBolsas.Value);//Sumamos la anterior con la cantidad que se está agregando para actualizar el inventario
                Botones.AgregarUnidades(modelo,color,talla,cantidad); //Añadimos los botones al inventario
            }
            else
            {
                cantidad = decimal.ToInt32(this.nBotonesBolsas.Value); //Sumamos la anterior con la cantidad que se quiere añadir para actualizar el inventario
                Botones.AgregarUnidades(modelo, color, talla, cantidad); //Añadimos los botones al inventario
            }

            this.txtExistencia.Text = Botones.Existencia(modelo,color,talla).ToString(); //Actualizamos el inventario para mostrarlo en el formulario
            MessageBox.Show("Se han agregado " + cantidad + " botones al inventario");
            this.nBolsas.Value = 0;
            this.nBotonesBolsas.Value = 0;
        }

        private void btnDescontar_Click(object sender, EventArgs e)
        {
            int cantidad;
            //Verificamos que no estén vacíos los campos de la información del botón
            if (string.IsNullOrEmpty(cbEstilo.Text)) return; //Validamos estilo
            if (string.IsNullOrEmpty(cbColor.Text)) return; //Validamos el color
            if (string.IsNullOrEmpty(cbTalla.Text)) return; //Validamos la talla
            if (this.nBotonesBolsas.Value == 0) return; //Validamos la cantidad de botones por bolsa

            short modelo = Int16.Parse(cbEstilo.Text.ToString());
            string color = cbColor.Text;
            byte talla = byte.Parse(cbTalla.Text.ToString());

            //Verificamos cuál opción está seleccionadda (Por bolsa o por unidad)
            if (rbBolsas.Checked)
            {
                if (this.nBolsas.Value == 0) return; //Validamos el numero de bolsas
                
                cantidad = decimal.ToInt32(this.nBolsas.Value) * decimal.ToInt32(this.nBotonesBolsas.Value);//Sumamos la anterior con la cantidad que se está agregando para actualizar el inventario
                Botones.DescontarUnidades(modelo,color,talla,cantidad); //Añadimos los botones al inventario
            }
            else
            {
                cantidad = decimal.ToInt32(this.nBotonesBolsas.Value); //Sumamos la anterior con la cantidad que se quiere añadir para actualizar el inventario
                Botones.DescontarUnidades(modelo,color,talla,cantidad);
            }

            this.txtExistencia.Text = Botones.Existencia(modelo,color,talla).ToString(); //Actualizamos el inventario para mostrarlo en el formulario
            MessageBox.Show("Se han descontado " + cantidad + " botones al inventario");
            this.nBolsas.Value = 0;
            this.nBotonesBolsas.Value = 0;
        }

        private void btnNuevoBoton_Click(object sender, EventArgs e)
        {
            frmNuevoBoton wNuevoBoton = new frmNuevoBoton();
            wNuevoBoton.ShowDialog(this);
        }

        private void btnAbrirInventario_Click(object sender, EventArgs e)
        {
            frmPrincipal wPrincipal = Funciones.TraerVentana<frmPrincipal>();

            try
            {
                BotonesCierres.Botones.frmBotonesInv wInventarioBotones = Funciones.TraerVentana<BotonesCierres.Botones.frmBotonesInv>();
                wInventarioBotones.BringToFront();
            }
            catch (IndexOutOfRangeException ex)
            {
                BotonesCierres.Botones.frmBotonesInv wInventarioBotones = new BotonesCierres.Botones.frmBotonesInv();
                wInventarioBotones.MdiParent = wPrincipal;
                wInventarioBotones.Show();
                this.Tag = wInventarioBotones;
                wInventarioBotones.Show();
            }
        }
    }
}
