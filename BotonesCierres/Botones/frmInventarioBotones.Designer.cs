namespace BotonesCierres
{
    partial class frmInventarioBotones
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
            this.cbEstilo = new System.Windows.Forms.ComboBox();
            this.cbColor = new System.Windows.Forms.ComboBox();
            this.cbTalla = new System.Windows.Forms.ComboBox();
            this.dgHistorial = new System.Windows.Forms.DataGridView();
            this.gbLogBotones = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtExistencia = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAbrirInventario = new System.Windows.Forms.Button();
            this.btnNuevoBoton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnImportar = new System.Windows.Forms.Button();
            this.nBolsas = new System.Windows.Forms.NumericUpDown();
            this.nBotonesBolsas = new System.Windows.Forms.NumericUpDown();
            this.lblBotonesBolsas = new System.Windows.Forms.Label();
            this.lblBolsas = new System.Windows.Forms.Label();
            this.rbBolsas = new System.Windows.Forms.RadioButton();
            this.btnDescontar = new System.Windows.Forms.Button();
            this.rbUnidad = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgHistorial)).BeginInit();
            this.gbLogBotones.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nBolsas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nBotonesBolsas)).BeginInit();
            this.SuspendLayout();
            // 
            // cbEstilo
            // 
            this.cbEstilo.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cbEstilo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbEstilo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstilo.FormattingEnabled = true;
            this.cbEstilo.Location = new System.Drawing.Point(6, 42);
            this.cbEstilo.Name = "cbEstilo";
            this.cbEstilo.Size = new System.Drawing.Size(121, 21);
            this.cbEstilo.TabIndex = 1;
            this.cbEstilo.SelectedValueChanged += new System.EventHandler(this.cbEstilo_SelectedValueChanged);
            // 
            // cbColor
            // 
            this.cbColor.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cbColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColor.FormattingEnabled = true;
            this.cbColor.Location = new System.Drawing.Point(159, 42);
            this.cbColor.Name = "cbColor";
            this.cbColor.Size = new System.Drawing.Size(121, 21);
            this.cbColor.TabIndex = 2;
            this.cbColor.SelectedValueChanged += new System.EventHandler(this.cbColor_SelectedValueChanged);
            // 
            // cbTalla
            // 
            this.cbTalla.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cbTalla.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbTalla.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTalla.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbTalla.FormattingEnabled = true;
            this.cbTalla.Location = new System.Drawing.Point(311, 42);
            this.cbTalla.Name = "cbTalla";
            this.cbTalla.Size = new System.Drawing.Size(121, 21);
            this.cbTalla.TabIndex = 3;
            this.cbTalla.SelectedValueChanged += new System.EventHandler(this.cbTalla_SelectedValueChanged);
            // 
            // dgHistorial
            // 
            this.dgHistorial.AllowUserToAddRows = false;
            this.dgHistorial.AllowUserToDeleteRows = false;
            this.dgHistorial.AllowUserToResizeRows = false;
            this.dgHistorial.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgHistorial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgHistorial.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgHistorial.Location = new System.Drawing.Point(3, 16);
            this.dgHistorial.Name = "dgHistorial";
            this.dgHistorial.ReadOnly = true;
            this.dgHistorial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgHistorial.Size = new System.Drawing.Size(674, 185);
            this.dgHistorial.TabIndex = 4;
            // 
            // gbLogBotones
            // 
            this.gbLogBotones.Controls.Add(this.dgHistorial);
            this.gbLogBotones.Location = new System.Drawing.Point(12, 245);
            this.gbLogBotones.Name = "gbLogBotones";
            this.gbLogBotones.Size = new System.Drawing.Size(680, 204);
            this.gbLogBotones.TabIndex = 5;
            this.gbLogBotones.TabStop = false;
            this.gbLogBotones.Text = "Historial";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Modelo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Color";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(308, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Talla";
            // 
            // txtExistencia
            // 
            this.txtExistencia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExistencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExistencia.ForeColor = System.Drawing.Color.Red;
            this.txtExistencia.Location = new System.Drawing.Point(562, 43);
            this.txtExistencia.Name = "txtExistencia";
            this.txtExistencia.Size = new System.Drawing.Size(112, 22);
            this.txtExistencia.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(559, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Existencia";
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.Transparent;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnAgregar.Location = new System.Drawing.Point(273, 22);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(134, 31);
            this.btnAgregar.TabIndex = 11;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAbrirInventario);
            this.groupBox1.Controls.Add(this.btnNuevoBoton);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cbEstilo);
            this.groupBox1.Controls.Add(this.cbTalla);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtExistencia);
            this.groupBox1.Controls.Add(this.cbColor);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(680, 227);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inventario de botones";
            // 
            // btnAbrirInventario
            // 
            this.btnAbrirInventario.BackColor = System.Drawing.Color.Transparent;
            this.btnAbrirInventario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbrirInventario.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnAbrirInventario.Location = new System.Drawing.Point(577, 148);
            this.btnAbrirInventario.Name = "btnAbrirInventario";
            this.btnAbrirInventario.Size = new System.Drawing.Size(97, 31);
            this.btnAbrirInventario.TabIndex = 24;
            this.btnAbrirInventario.Text = "Ver inventario";
            this.btnAbrirInventario.UseVisualStyleBackColor = false;
            this.btnAbrirInventario.Click += new System.EventHandler(this.btnAbrirInventario_Click);
            // 
            // btnNuevoBoton
            // 
            this.btnNuevoBoton.BackColor = System.Drawing.Color.Transparent;
            this.btnNuevoBoton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoBoton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnNuevoBoton.Location = new System.Drawing.Point(577, 185);
            this.btnNuevoBoton.Name = "btnNuevoBoton";
            this.btnNuevoBoton.Size = new System.Drawing.Size(97, 31);
            this.btnNuevoBoton.TabIndex = 23;
            this.btnNuevoBoton.Text = "Nuevo";
            this.btnNuevoBoton.UseVisualStyleBackColor = false;
            this.btnNuevoBoton.Click += new System.EventHandler(this.btnNuevoBoton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnImportar);
            this.groupBox2.Controls.Add(this.nBolsas);
            this.groupBox2.Controls.Add(this.nBotonesBolsas);
            this.groupBox2.Controls.Add(this.lblBotonesBolsas);
            this.groupBox2.Controls.Add(this.lblBolsas);
            this.groupBox2.Controls.Add(this.rbBolsas);
            this.groupBox2.Controls.Add(this.btnDescontar);
            this.groupBox2.Controls.Add(this.btnAgregar);
            this.groupBox2.Controls.Add(this.rbUnidad);
            this.groupBox2.Location = new System.Drawing.Point(7, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(425, 137);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Altas / Bajas";
            // 
            // btnImportar
            // 
            this.btnImportar.BackColor = System.Drawing.Color.Transparent;
            this.btnImportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportar.Enabled = false;
            this.btnImportar.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnImportar.Location = new System.Drawing.Point(273, 96);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(134, 31);
            this.btnImportar.TabIndex = 22;
            this.btnImportar.Text = "Importar";
            this.btnImportar.UseVisualStyleBackColor = false;
            // 
            // nBolsas
            // 
            this.nBolsas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nBolsas.Location = new System.Drawing.Point(18, 56);
            this.nBolsas.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nBolsas.Name = "nBolsas";
            this.nBolsas.Size = new System.Drawing.Size(89, 20);
            this.nBolsas.TabIndex = 21;
            this.nBolsas.ThousandsSeparator = true;
            // 
            // nBotonesBolsas
            // 
            this.nBotonesBolsas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nBotonesBolsas.Location = new System.Drawing.Point(18, 108);
            this.nBotonesBolsas.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nBotonesBolsas.Name = "nBotonesBolsas";
            this.nBotonesBolsas.Size = new System.Drawing.Size(89, 20);
            this.nBotonesBolsas.TabIndex = 20;
            this.nBotonesBolsas.ThousandsSeparator = true;
            // 
            // lblBotonesBolsas
            // 
            this.lblBotonesBolsas.AutoSize = true;
            this.lblBotonesBolsas.Location = new System.Drawing.Point(15, 92);
            this.lblBotonesBolsas.Name = "lblBotonesBolsas";
            this.lblBotonesBolsas.Size = new System.Drawing.Size(92, 13);
            this.lblBotonesBolsas.TabIndex = 17;
            this.lblBotonesBolsas.Text = "Botones por bolsa";
            // 
            // lblBolsas
            // 
            this.lblBolsas.AutoSize = true;
            this.lblBolsas.Location = new System.Drawing.Point(15, 40);
            this.lblBolsas.Name = "lblBolsas";
            this.lblBolsas.Size = new System.Drawing.Size(38, 13);
            this.lblBolsas.TabIndex = 16;
            this.lblBolsas.Text = "Bolsas";
            // 
            // rbBolsas
            // 
            this.rbBolsas.AutoSize = true;
            this.rbBolsas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbBolsas.Location = new System.Drawing.Point(165, 73);
            this.rbBolsas.Name = "rbBolsas";
            this.rbBolsas.Size = new System.Drawing.Size(55, 17);
            this.rbBolsas.TabIndex = 14;
            this.rbBolsas.TabStop = true;
            this.rbBolsas.Text = "Bolsas";
            this.rbBolsas.UseVisualStyleBackColor = true;
            this.rbBolsas.Click += new System.EventHandler(this.rbBolsas_Click);
            // 
            // btnDescontar
            // 
            this.btnDescontar.BackColor = System.Drawing.Color.Transparent;
            this.btnDescontar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDescontar.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnDescontar.Location = new System.Drawing.Point(273, 59);
            this.btnDescontar.Name = "btnDescontar";
            this.btnDescontar.Size = new System.Drawing.Size(134, 31);
            this.btnDescontar.TabIndex = 12;
            this.btnDescontar.Text = "Descontar";
            this.btnDescontar.UseVisualStyleBackColor = false;
            this.btnDescontar.Click += new System.EventHandler(this.btnDescontar_Click);
            // 
            // rbUnidad
            // 
            this.rbUnidad.AutoSize = true;
            this.rbUnidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbUnidad.Location = new System.Drawing.Point(165, 96);
            this.rbUnidad.Name = "rbUnidad";
            this.rbUnidad.Size = new System.Drawing.Size(58, 17);
            this.rbUnidad.TabIndex = 13;
            this.rbUnidad.TabStop = true;
            this.rbUnidad.Text = "Unidad";
            this.rbUnidad.UseVisualStyleBackColor = true;
            this.rbUnidad.Click += new System.EventHandler(this.rbUnidad_Click);
            // 
            // frmInventarioBotones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 461);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbLogBotones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(720, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(720, 500);
            this.Name = "frmInventarioBotones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventario botones :: Juguel";
            this.Load += new System.EventHandler(this.frmInventarioBotones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgHistorial)).EndInit();
            this.gbLogBotones.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nBolsas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nBotonesBolsas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbLogBotones;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtExistencia;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.NumericUpDown nBolsas;
        private System.Windows.Forms.NumericUpDown nBotonesBolsas;
        private System.Windows.Forms.Label lblBotonesBolsas;
        private System.Windows.Forms.Label lblBolsas;
        private System.Windows.Forms.Button btnDescontar;
        private System.Windows.Forms.RadioButton rbUnidad;
        private System.Windows.Forms.Button btnNuevoBoton;
        public System.Windows.Forms.ComboBox cbEstilo;
        public System.Windows.Forms.ComboBox cbColor;
        public System.Windows.Forms.ComboBox cbTalla;
        public System.Windows.Forms.DataGridView dgHistorial;
        public System.Windows.Forms.RadioButton rbBolsas;
        private System.Windows.Forms.Button btnAbrirInventario;
    }
}