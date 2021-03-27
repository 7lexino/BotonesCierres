namespace BotonesCierres
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.paContenedorMenu = new System.Windows.Forms.Panel();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.btnEtiquetas = new System.Windows.Forms.Button();
            this.btnReportesProduccion = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnCierres = new System.Windows.Forms.Button();
            this.btnBotones = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.paContenedorMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // paContenedorMenu
            // 
            this.paContenedorMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(63)))), ((int)(((byte)(117)))));
            this.paContenedorMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paContenedorMenu.Controls.Add(this.btnCerrarSesion);
            this.paContenedorMenu.Controls.Add(this.btnEtiquetas);
            this.paContenedorMenu.Controls.Add(this.btnReportesProduccion);
            this.paContenedorMenu.Controls.Add(this.btnSalir);
            this.paContenedorMenu.Controls.Add(this.btnCierres);
            this.paContenedorMenu.Controls.Add(this.btnBotones);
            this.paContenedorMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.paContenedorMenu.Location = new System.Drawing.Point(0, 0);
            this.paContenedorMenu.Name = "paContenedorMenu";
            this.paContenedorMenu.Size = new System.Drawing.Size(176, 512);
            this.paContenedorMenu.TabIndex = 0;
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCerrarSesion.BackColor = System.Drawing.Color.Orange;
            this.btnCerrarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarSesion.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.Black;
            this.btnCerrarSesion.Location = new System.Drawing.Point(12, 437);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(149, 28);
            this.btnCerrarSesion.TabIndex = 7;
            this.btnCerrarSesion.Text = "Cerrar sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // btnEtiquetas
            // 
            this.btnEtiquetas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(63)))), ((int)(((byte)(117)))));
            this.btnEtiquetas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEtiquetas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEtiquetas.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(105)))), ((int)(((byte)(161)))));
            this.btnEtiquetas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEtiquetas.ForeColor = System.Drawing.Color.White;
            this.btnEtiquetas.Image = global::BotonesCierres.Properties.Resources.etiquetas_mini;
            this.btnEtiquetas.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEtiquetas.Location = new System.Drawing.Point(0, 149);
            this.btnEtiquetas.Name = "btnEtiquetas";
            this.btnEtiquetas.Size = new System.Drawing.Size(174, 50);
            this.btnEtiquetas.TabIndex = 6;
            this.btnEtiquetas.Text = "Etiquetas";
            this.btnEtiquetas.UseVisualStyleBackColor = false;
            this.btnEtiquetas.Click += new System.EventHandler(this.btnEtiquetas_Click);
            // 
            // btnReportesProduccion
            // 
            this.btnReportesProduccion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(63)))), ((int)(((byte)(117)))));
            this.btnReportesProduccion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReportesProduccion.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReportesProduccion.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(105)))), ((int)(((byte)(161)))));
            this.btnReportesProduccion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportesProduccion.ForeColor = System.Drawing.Color.White;
            this.btnReportesProduccion.Image = global::BotonesCierres.Properties.Resources.reporte_mini;
            this.btnReportesProduccion.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReportesProduccion.Location = new System.Drawing.Point(0, 99);
            this.btnReportesProduccion.Name = "btnReportesProduccion";
            this.btnReportesProduccion.Size = new System.Drawing.Size(174, 50);
            this.btnReportesProduccion.TabIndex = 5;
            this.btnReportesProduccion.Text = "Reportes producción";
            this.btnReportesProduccion.UseVisualStyleBackColor = false;
            this.btnReportesProduccion.Click += new System.EventHandler(this.btnReportesProduccion_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSalir.BackColor = System.Drawing.Color.DarkRed;
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(12, 471);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(149, 28);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnCierres
            // 
            this.btnCierres.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(63)))), ((int)(((byte)(117)))));
            this.btnCierres.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCierres.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCierres.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(105)))), ((int)(((byte)(161)))));
            this.btnCierres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCierres.ForeColor = System.Drawing.Color.White;
            this.btnCierres.Image = global::BotonesCierres.Properties.Resources.cierre_mini;
            this.btnCierres.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCierres.Location = new System.Drawing.Point(0, 49);
            this.btnCierres.Name = "btnCierres";
            this.btnCierres.Size = new System.Drawing.Size(174, 50);
            this.btnCierres.TabIndex = 2;
            this.btnCierres.Text = "Cierres";
            this.btnCierres.UseVisualStyleBackColor = false;
            this.btnCierres.Click += new System.EventHandler(this.btnCierres_Click);
            // 
            // btnBotones
            // 
            this.btnBotones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(63)))), ((int)(((byte)(117)))));
            this.btnBotones.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBotones.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBotones.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(105)))), ((int)(((byte)(161)))));
            this.btnBotones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBotones.ForeColor = System.Drawing.Color.White;
            this.btnBotones.Image = global::BotonesCierres.Properties.Resources.boton_mini;
            this.btnBotones.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBotones.Location = new System.Drawing.Point(0, 0);
            this.btnBotones.Name = "btnBotones";
            this.btnBotones.Size = new System.Drawing.Size(174, 49);
            this.btnBotones.TabIndex = 1;
            this.btnBotones.Text = "Botones";
            this.btnBotones.UseVisualStyleBackColor = false;
            this.btnBotones.Click += new System.EventHandler(this.btnBotones_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.lblUsuario);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(176, 491);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(808, 21);
            this.panel1.TabIndex = 1;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(762, 5);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(43, 13);
            this.lblUsuario.TabIndex = 2;
            this.lblUsuario.Text = "Usuario";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(984, 512);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.paContenedorMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MinimumSize = new System.Drawing.Size(1000, 550);
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Botones y cierres :: Juguel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPrincipal_FormClosing);
            this.paContenedorMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCierres;
        private System.Windows.Forms.Panel paContenedorMenu;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnReportesProduccion;
        private System.Windows.Forms.Button btnBotones;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEtiquetas;
        private System.Windows.Forms.Button btnCerrarSesion;
        public System.Windows.Forms.Label lblUsuario;
    }
}

