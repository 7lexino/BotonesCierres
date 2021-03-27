namespace BotonesCierres
{
    partial class frmReporteProduccion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReporteProduccion));
            this.tabReportes = new System.Windows.Forms.TabControl();
            this.tBotones = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbCargandoArchivo = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGenerarReporte = new System.Windows.Forms.Button();
            this.btnElegirArchivo = new System.Windows.Forms.Button();
            this.txtRutaArchivo = new System.Windows.Forms.TextBox();
            this.tEditar = new System.Windows.Forms.TabPage();
            this.btnEtiquetas = new System.Windows.Forms.Button();
            this.pbExportandoArchivo = new System.Windows.Forms.ProgressBar();
            this.btnActualizarTabla = new System.Windows.Forms.Button();
            this.btnExportarRep = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBlusas = new System.Windows.Forms.Button();
            this.btnFaldas = new System.Windows.Forms.Button();
            this.btnSacos = new System.Windows.Forms.Button();
            this.cbReportes = new System.Windows.Forms.ComboBox();
            this.dgVerReporte = new System.Windows.Forms.DataGridView();
            this.tabReportes.SuspendLayout();
            this.tBotones.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tEditar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVerReporte)).BeginInit();
            this.SuspendLayout();
            // 
            // tabReportes
            // 
            this.tabReportes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabReportes.Controls.Add(this.tBotones);
            this.tabReportes.Controls.Add(this.tEditar);
            this.tabReportes.Location = new System.Drawing.Point(12, 12);
            this.tabReportes.Name = "tabReportes";
            this.tabReportes.SelectedIndex = 0;
            this.tabReportes.Size = new System.Drawing.Size(1160, 588);
            this.tabReportes.TabIndex = 1;
            // 
            // tBotones
            // 
            this.tBotones.Controls.Add(this.groupBox1);
            this.tBotones.Location = new System.Drawing.Point(4, 22);
            this.tBotones.Name = "tBotones";
            this.tBotones.Padding = new System.Windows.Forms.Padding(3);
            this.tBotones.Size = new System.Drawing.Size(1152, 562);
            this.tBotones.TabIndex = 0;
            this.tBotones.Text = "Generar reporte";
            this.tBotones.UseVisualStyleBackColor = true;
            this.tBotones.Enter += new System.EventHandler(this.tBotones_Enter);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pbCargandoArchivo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnGenerarReporte);
            this.groupBox1.Controls.Add(this.btnElegirArchivo);
            this.groupBox1.Controls.Add(this.txtRutaArchivo);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(548, 276);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Generar";
            // 
            // pbCargandoArchivo
            // 
            this.pbCargandoArchivo.Location = new System.Drawing.Point(14, 236);
            this.pbCargandoArchivo.Maximum = 1000;
            this.pbCargandoArchivo.Name = "pbCargandoArchivo";
            this.pbCargandoArchivo.Size = new System.Drawing.Size(518, 23);
            this.pbCargandoArchivo.Step = 1;
            this.pbCargandoArchivo.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbCargandoArchivo.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Elegir archivo";
            // 
            // btnGenerarReporte
            // 
            this.btnGenerarReporte.BackColor = System.Drawing.Color.Transparent;
            this.btnGenerarReporte.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerarReporte.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnGenerarReporte.Location = new System.Drawing.Point(205, 104);
            this.btnGenerarReporte.Name = "btnGenerarReporte";
            this.btnGenerarReporte.Size = new System.Drawing.Size(128, 38);
            this.btnGenerarReporte.TabIndex = 7;
            this.btnGenerarReporte.Text = "Generar";
            this.btnGenerarReporte.UseVisualStyleBackColor = false;
            this.btnGenerarReporte.Click += new System.EventHandler(this.btnGenerarReporte_Click);
            // 
            // btnElegirArchivo
            // 
            this.btnElegirArchivo.BackColor = System.Drawing.Color.Transparent;
            this.btnElegirArchivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnElegirArchivo.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnElegirArchivo.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnElegirArchivo.Location = new System.Drawing.Point(141, 56);
            this.btnElegirArchivo.Name = "btnElegirArchivo";
            this.btnElegirArchivo.Size = new System.Drawing.Size(75, 20);
            this.btnElegirArchivo.TabIndex = 1;
            this.btnElegirArchivo.Text = "Elegir";
            this.btnElegirArchivo.UseVisualStyleBackColor = false;
            this.btnElegirArchivo.Click += new System.EventHandler(this.btnElegirArchivo_Click);
            // 
            // txtRutaArchivo
            // 
            this.txtRutaArchivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRutaArchivo.Location = new System.Drawing.Point(222, 56);
            this.txtRutaArchivo.Name = "txtRutaArchivo";
            this.txtRutaArchivo.ReadOnly = true;
            this.txtRutaArchivo.Size = new System.Drawing.Size(171, 20);
            this.txtRutaArchivo.TabIndex = 0;
            // 
            // tEditar
            // 
            this.tEditar.Controls.Add(this.btnEtiquetas);
            this.tEditar.Controls.Add(this.pbExportandoArchivo);
            this.tEditar.Controls.Add(this.btnActualizarTabla);
            this.tEditar.Controls.Add(this.btnExportarRep);
            this.tEditar.Controls.Add(this.label3);
            this.tEditar.Controls.Add(this.btnBlusas);
            this.tEditar.Controls.Add(this.btnFaldas);
            this.tEditar.Controls.Add(this.btnSacos);
            this.tEditar.Controls.Add(this.cbReportes);
            this.tEditar.Controls.Add(this.dgVerReporte);
            this.tEditar.Location = new System.Drawing.Point(4, 22);
            this.tEditar.Name = "tEditar";
            this.tEditar.Padding = new System.Windows.Forms.Padding(3);
            this.tEditar.Size = new System.Drawing.Size(1152, 562);
            this.tEditar.TabIndex = 1;
            this.tEditar.Text = "Editar reporte";
            this.tEditar.UseVisualStyleBackColor = true;
            this.tEditar.Enter += new System.EventHandler(this.tEditar_Enter);
            // 
            // btnEtiquetas
            // 
            this.btnEtiquetas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEtiquetas.BackColor = System.Drawing.Color.SteelBlue;
            this.btnEtiquetas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEtiquetas.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnEtiquetas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEtiquetas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEtiquetas.ForeColor = System.Drawing.Color.White;
            this.btnEtiquetas.Image = global::BotonesCierres.Properties.Resources.reporte_medio;
            this.btnEtiquetas.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEtiquetas.Location = new System.Drawing.Point(1022, 10);
            this.btnEtiquetas.Name = "btnEtiquetas";
            this.btnEtiquetas.Size = new System.Drawing.Size(61, 45);
            this.btnEtiquetas.TabIndex = 10;
            this.btnEtiquetas.Text = "Etiquetas";
            this.btnEtiquetas.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEtiquetas.UseVisualStyleBackColor = false;
            this.btnEtiquetas.Click += new System.EventHandler(this.btnEtiquetas_Click);
            // 
            // pbExportandoArchivo
            // 
            this.pbExportandoArchivo.Location = new System.Drawing.Point(276, 6);
            this.pbExportandoArchivo.Maximum = 1000;
            this.pbExportandoArchivo.Name = "pbExportandoArchivo";
            this.pbExportandoArchivo.Size = new System.Drawing.Size(328, 12);
            this.pbExportandoArchivo.TabIndex = 9;
            // 
            // btnActualizarTabla
            // 
            this.btnActualizarTabla.BackColor = System.Drawing.Color.Transparent;
            this.btnActualizarTabla.BackgroundImage = global::BotonesCierres.Properties.Resources.Actualizar;
            this.btnActualizarTabla.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnActualizarTabla.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizarTabla.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnActualizarTabla.Location = new System.Drawing.Point(574, 20);
            this.btnActualizarTabla.Name = "btnActualizarTabla";
            this.btnActualizarTabla.Size = new System.Drawing.Size(30, 30);
            this.btnActualizarTabla.TabIndex = 8;
            this.btnActualizarTabla.UseVisualStyleBackColor = false;
            this.btnActualizarTabla.Click += new System.EventHandler(this.btnActualizarTabla_Click);
            // 
            // btnExportarRep
            // 
            this.btnExportarRep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportarRep.BackColor = System.Drawing.Color.Green;
            this.btnExportarRep.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarRep.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnExportarRep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarRep.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarRep.ForeColor = System.Drawing.Color.White;
            this.btnExportarRep.Image = global::BotonesCierres.Properties.Resources.reporte_medio;
            this.btnExportarRep.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExportarRep.Location = new System.Drawing.Point(1089, 10);
            this.btnExportarRep.Name = "btnExportarRep";
            this.btnExportarRep.Size = new System.Drawing.Size(57, 45);
            this.btnExportarRep.TabIndex = 7;
            this.btnExportarRep.Text = "Exportar";
            this.btnExportarRep.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExportarRep.UseVisualStyleBackColor = false;
            this.btnExportarRep.Click += new System.EventHandler(this.btnExportarRep_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Selecciona un reporte";
            // 
            // btnBlusas
            // 
            this.btnBlusas.BackColor = System.Drawing.Color.Transparent;
            this.btnBlusas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBlusas.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnBlusas.Location = new System.Drawing.Point(493, 24);
            this.btnBlusas.Name = "btnBlusas";
            this.btnBlusas.Size = new System.Drawing.Size(75, 23);
            this.btnBlusas.TabIndex = 5;
            this.btnBlusas.Text = "Blusas";
            this.btnBlusas.UseVisualStyleBackColor = false;
            this.btnBlusas.Click += new System.EventHandler(this.btnBlusas_Click);
            // 
            // btnFaldas
            // 
            this.btnFaldas.BackColor = System.Drawing.Color.Transparent;
            this.btnFaldas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFaldas.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnFaldas.Location = new System.Drawing.Point(357, 24);
            this.btnFaldas.Name = "btnFaldas";
            this.btnFaldas.Size = new System.Drawing.Size(130, 23);
            this.btnFaldas.TabIndex = 3;
            this.btnFaldas.Text = "Faldas y pantalones";
            this.btnFaldas.UseVisualStyleBackColor = false;
            this.btnFaldas.Click += new System.EventHandler(this.btnFaldas_Click);
            // 
            // btnSacos
            // 
            this.btnSacos.BackColor = System.Drawing.Color.Transparent;
            this.btnSacos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSacos.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnSacos.Location = new System.Drawing.Point(276, 24);
            this.btnSacos.Name = "btnSacos";
            this.btnSacos.Size = new System.Drawing.Size(75, 23);
            this.btnSacos.TabIndex = 2;
            this.btnSacos.Text = "Sacos";
            this.btnSacos.UseVisualStyleBackColor = false;
            this.btnSacos.Click += new System.EventHandler(this.btnSacos_Click);
            // 
            // cbReportes
            // 
            this.cbReportes.BackColor = System.Drawing.Color.Gainsboro;
            this.cbReportes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbReportes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReportes.FormattingEnabled = true;
            this.cbReportes.Location = new System.Drawing.Point(6, 26);
            this.cbReportes.Name = "cbReportes";
            this.cbReportes.Size = new System.Drawing.Size(222, 21);
            this.cbReportes.TabIndex = 1;
            // 
            // dgVerReporte
            // 
            this.dgVerReporte.AllowUserToAddRows = false;
            this.dgVerReporte.AllowUserToDeleteRows = false;
            this.dgVerReporte.AllowUserToResizeRows = false;
            this.dgVerReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgVerReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVerReporte.Location = new System.Drawing.Point(6, 61);
            this.dgVerReporte.Name = "dgVerReporte";
            this.dgVerReporte.RowHeadersVisible = false;
            this.dgVerReporte.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgVerReporte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgVerReporte.Size = new System.Drawing.Size(1140, 495);
            this.dgVerReporte.TabIndex = 0;
            this.dgVerReporte.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgVerReporte_CellValueChanged);
            // 
            // frmReporteProduccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 612);
            this.Controls.Add(this.tabReportes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReporteProduccion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte producción :: Juguel";
            this.Load += new System.EventHandler(this.frmReporteProduccion_Load);
            this.tabReportes.ResumeLayout(false);
            this.tBotones.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tEditar.ResumeLayout(false);
            this.tEditar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVerReporte)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabReportes;
        private System.Windows.Forms.TabPage tBotones;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGenerarReporte;
        private System.Windows.Forms.Button btnElegirArchivo;
        private System.Windows.Forms.TextBox txtRutaArchivo;
        private System.Windows.Forms.TabPage tEditar;
        private System.Windows.Forms.ComboBox cbReportes;
        private System.Windows.Forms.DataGridView dgVerReporte;
        private System.Windows.Forms.Button btnBlusas;
        private System.Windows.Forms.Button btnFaldas;
        private System.Windows.Forms.Button btnSacos;
        private System.Windows.Forms.Button btnExportarRep;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnActualizarTabla;
        private System.Windows.Forms.ProgressBar pbCargandoArchivo;
        private System.Windows.Forms.ProgressBar pbExportandoArchivo;
        private System.Windows.Forms.Button btnEtiquetas;
    }
}