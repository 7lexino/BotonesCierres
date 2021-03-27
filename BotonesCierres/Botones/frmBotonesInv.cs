using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BotonesCierres.Botones
{
    public partial class frmBotonesInv : Form
    {
        DataSet ds;
        
        public frmBotonesInv()
        {
            InitializeComponent();
        }

        private void frmBotonesInv_Load(object sender, EventArgs e)
        {
            string sql = "SELECT Modelo,Color,Talla,Inventario FROM inv_botones;";
            ds = Conexion.Ejecutar(sql);
            dgvBotones.DataSource = ds.Tables[0];
        }
    }
}
