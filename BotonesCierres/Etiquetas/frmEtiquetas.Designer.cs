namespace BotonesCierres
{
    partial class frmEtiquetas
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
            this.cbLotes = new System.Windows.Forms.ComboBox();
            this.rbBotones = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbBlusas = new System.Windows.Forms.RadioButton();
            this.rbFaldasPantalones = new System.Windows.Forms.RadioButton();
            this.rbSacos = new System.Windows.Forms.RadioButton();
            this.rbCierres = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.imgCargando = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCargando)).BeginInit();
            this.SuspendLayout();
            // 
            // cbLotes
            // 
            this.cbLotes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLotes.FormattingEnabled = true;
            this.cbLotes.Location = new System.Drawing.Point(12, 28);
            this.cbLotes.Name = "cbLotes";
            this.cbLotes.Size = new System.Drawing.Size(235, 21);
            this.cbLotes.TabIndex = 0;
            // 
            // rbBotones
            // 
            this.rbBotones.AutoSize = true;
            this.rbBotones.Checked = true;
            this.rbBotones.Location = new System.Drawing.Point(49, 187);
            this.rbBotones.Name = "rbBotones";
            this.rbBotones.Size = new System.Drawing.Size(64, 17);
            this.rbBotones.TabIndex = 1;
            this.rbBotones.TabStop = true;
            this.rbBotones.Text = "Botones";
            this.rbBotones.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbBlusas);
            this.groupBox1.Controls.Add(this.rbFaldasPantalones);
            this.groupBox1.Controls.Add(this.rbSacos);
            this.groupBox1.Location = new System.Drawing.Point(12, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(235, 112);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Prendas";
            // 
            // rbBlusas
            // 
            this.rbBlusas.AutoSize = true;
            this.rbBlusas.Location = new System.Drawing.Point(18, 74);
            this.rbBlusas.Name = "rbBlusas";
            this.rbBlusas.Size = new System.Drawing.Size(56, 17);
            this.rbBlusas.TabIndex = 4;
            this.rbBlusas.Text = "Blusas";
            this.rbBlusas.UseVisualStyleBackColor = true;
            // 
            // rbFaldasPantalones
            // 
            this.rbFaldasPantalones.AutoSize = true;
            this.rbFaldasPantalones.Location = new System.Drawing.Point(18, 51);
            this.rbFaldasPantalones.Name = "rbFaldasPantalones";
            this.rbFaldasPantalones.Size = new System.Drawing.Size(119, 17);
            this.rbFaldasPantalones.TabIndex = 3;
            this.rbFaldasPantalones.Text = "Faldas y pantalones";
            this.rbFaldasPantalones.UseVisualStyleBackColor = true;
            // 
            // rbSacos
            // 
            this.rbSacos.AutoSize = true;
            this.rbSacos.Checked = true;
            this.rbSacos.Location = new System.Drawing.Point(18, 28);
            this.rbSacos.Name = "rbSacos";
            this.rbSacos.Size = new System.Drawing.Size(55, 17);
            this.rbSacos.TabIndex = 2;
            this.rbSacos.TabStop = true;
            this.rbSacos.Text = "Sacos";
            this.rbSacos.UseVisualStyleBackColor = true;
            // 
            // rbCierres
            // 
            this.rbCierres.AutoSize = true;
            this.rbCierres.Location = new System.Drawing.Point(148, 187);
            this.rbCierres.Name = "rbCierres";
            this.rbCierres.Size = new System.Drawing.Size(57, 17);
            this.rbCierres.TabIndex = 5;
            this.rbCierres.Text = "Cierres";
            this.rbCierres.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Selecciona el lote";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(12, 219);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(235, 29);
            this.btnImprimir.TabIndex = 7;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // imgCargando
            // 
            this.imgCargando.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgCargando.Image = global::BotonesCierres.Properties.Resources.cargando;
            this.imgCargando.Location = new System.Drawing.Point(12, 12);
            this.imgCargando.Name = "imgCargando";
            this.imgCargando.Size = new System.Drawing.Size(235, 236);
            this.imgCargando.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imgCargando.TabIndex = 5;
            this.imgCargando.TabStop = false;
            this.imgCargando.Visible = false;
            // 
            // frmEtiquetas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 259);
            this.Controls.Add(this.imgCargando);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbCierres);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rbBotones);
            this.Controls.Add(this.cbLotes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEtiquetas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Etiquetas :: Juguel";
            this.Load += new System.EventHandler(this.frmEtiquetas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCargando)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLotes;
        private System.Windows.Forms.RadioButton rbBotones;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCierres;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.RadioButton rbBlusas;
        private System.Windows.Forms.RadioButton rbFaldasPantalones;
        private System.Windows.Forms.RadioButton rbSacos;
        private System.Windows.Forms.PictureBox imgCargando;
    }
}