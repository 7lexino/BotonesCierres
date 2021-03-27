namespace BotonesCierres
{
    partial class frmNuevoCierre
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
            this.nCantidad = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTalla = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAgregarCierre = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nCantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // nCantidad
            // 
            this.nCantidad.BackColor = System.Drawing.SystemColors.Window;
            this.nCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nCantidad.Location = new System.Drawing.Point(120, 122);
            this.nCantidad.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nCantidad.Name = "nCantidad";
            this.nCantidad.Size = new System.Drawing.Size(110, 20);
            this.nCantidad.TabIndex = 20;
            this.nCantidad.ThousandsSeparator = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(117, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Cantidad inicial";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Añadir nuevo cierre";
            // 
            // txtTalla
            // 
            this.txtTalla.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTalla.Location = new System.Drawing.Point(14, 121);
            this.txtTalla.Name = "txtTalla";
            this.txtTalla.Size = new System.Drawing.Size(63, 20);
            this.txtTalla.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Talla";
            // 
            // txtModelo
            // 
            this.txtModelo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtModelo.Location = new System.Drawing.Point(137, 65);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.Size = new System.Drawing.Size(93, 20);
            this.txtModelo.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(134, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Modelo";
            // 
            // txtColor
            // 
            this.txtColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtColor.Location = new System.Drawing.Point(14, 65);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(89, 20);
            this.txtColor.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Color";
            // 
            // btnAgregarCierre
            // 
            this.btnAgregarCierre.BackColor = System.Drawing.Color.Transparent;
            this.btnAgregarCierre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarCierre.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnAgregarCierre.Location = new System.Drawing.Point(14, 159);
            this.btnAgregarCierre.Name = "btnAgregarCierre";
            this.btnAgregarCierre.Size = new System.Drawing.Size(216, 23);
            this.btnAgregarCierre.TabIndex = 11;
            this.btnAgregarCierre.Text = "Agregar al inventario";
            this.btnAgregarCierre.UseVisualStyleBackColor = false;
            this.btnAgregarCierre.Click += new System.EventHandler(this.btnAgregarCierre_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(79, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "cm.";
            // 
            // frmNuevoCierre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 193);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nCantidad);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTalla);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtModelo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAgregarCierre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmNuevoCierre";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cierre nuevo";
            ((System.ComponentModel.ISupportInitialize)(this.nCantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nCantidad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTalla;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAgregarCierre;
        private System.Windows.Forms.Label label6;
    }
}