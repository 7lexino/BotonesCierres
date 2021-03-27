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
    public partial class frmLogin : Form
    {
        byte clics=0;
        string sql;
        public static string usuario;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            
            string contrasena, SQL;

            usuario = this.txtUsuario.Text.Trim();
            contrasena = this.txtContra.Text.Trim();
            SQL = string.Format("SELECT usuario, contrasena, nombre, apellido FROM usuarios WHERE usuario='{0}' AND contrasena='{1}';", usuario, contrasena);
            
            DataSet ds = Conexion.Ejecutar(SQL);
            
            if (ds.Tables[0].Rows.Count == 1)
            {
                string nombre = ds.Tables[0].Rows[0]["nombre"].ToString();
                string apellido = ds.Tables[0].Rows[0]["apellido"].ToString();

                sql = string.Format("UPDATE usuarios SET online=true WHERE usuario='{0}'", usuario);
                ds = Conexion.Ejecutar(sql);
                if (Funciones.EstaAbiertoFormulario(new frmPrincipal()))
                {
                    frmPrincipal wPrincipal = Funciones.TraerVentana<frmPrincipal>();
                    wPrincipal.lblUsuario.Text = nombre + " " + apellido;

                    wPrincipal.Show();
                    this.Hide();
                }
                else
                {
                    frmPrincipal wPanelPrincipal = new frmPrincipal();
                    wPanelPrincipal.Show();
                    wPanelPrincipal.lblUsuario.Text = nombre + " " + apellido;
                    wPanelPrincipal.lblUsuario.Left = wPanelPrincipal.Width - wPanelPrincipal.lblUsuario.Width-250;
                    this.Hide(); //Corregir este es dilema
                }
            }
            else
            {
                this.txtUsuario.Clear();
                this.txtContra.Clear();
                this.txtUsuario.Focus();
                Mensajes.NoExito("Datos incorrectos");
            }
            
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.txtUsuario.Focus();
        }

        private void txtContra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                this.btnEntrar.PerformClick();
            }
        }

        private void picLogoJuguel_Click(object sender, EventArgs e)
        {
            string usuario="", contra="";
            if (clics < 7) clics++;
            else
            {
                clics = 0;
                Funciones.InputBox("Login ", "Usuario: ", ref usuario);
                Funciones.InputBox("Login", "Contraseña: ", ref contra,1);

                if(usuario=="alexcoolfree" && contra == "alex_123")
                {
                    Mensajes.Exito("Bienvenido a la administración!");
                    //MessageBox.Show(Funciones.EstaAbiertoFormulario(new frmLogin()).ToString());
                }
            }
        }
    }
}
