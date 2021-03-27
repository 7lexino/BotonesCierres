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
    public partial class frmCierresInv : Form
    {
        DataSet ds;
        
        public frmCierresInv()
        {
            InitializeComponent();
        }

        private void frmCierresInv_Load(object sender, EventArgs e)
        {
            string sql = "SELECT Color,Modelo,Talla,Inventario FROM inv_cierres;";
            ds = Conexion.Ejecutar(sql);
            dgvBotones.DataSource = ds.Tables[0];
        }
    }
}
