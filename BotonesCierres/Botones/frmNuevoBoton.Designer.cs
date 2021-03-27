namespace BotonesCierres
{
    partial class frmNuevoBoton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNuevoBoton));
            this.btnAgregarBoton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEstilo = new System.Windows.Forms.TextBox();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTalla = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nCantidad = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nCantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAgregarBoton
            // 
            this.btnAgregarBoton.BackColor = System.Drawing.Color.Transparent;
            this.btnAgregarBoton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarBoton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnAgregarBoton.Location = new System.Drawing.Point(12, 157);
            this.btnAgregarBoton.Name = "btnAgregarBoton";
            this.btnAgregarBoton.Size = new System.Drawing.Size(216, 23);
            this.btnAgregarBoton.TabIndex = 0;
            this.btnAgregarBoton.Text = "Agregar al inventario";
            this.btnAgregarBoton.UseVisualStyleBackColor = false;
            this.btnAgregarBoton.Click += new System.EventHandler(this.btnAgregarBoton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Modelo";
            // 
            // txtEstilo
            // 
            this.txtEstilo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEstilo.Location = new System.Drawing.Point(12, 63);
            this.txtEstilo.Name = "txtEstilo";
            this.txtEstilo.Size = new System.Drawing.Size(65, 20);
            this.txtEstilo.TabIndex = 1;
            // 
            // txtColor
            // 
            this.txtColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtColor.Location = new System.Drawing.Point(118, 63);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(110, 20);
            this.txtColor.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Color";
            // 
            // txtTalla
            // 
            this.txtTalla.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTalla.Location = new System.Drawing.Point(12, 119);
            this.txtTalla.Name = "txtTalla";
            this.txtTalla.Size = new System.Drawing.Size(65, 20);
            this.txtTalla.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Talla";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Añadir nuevo botón";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(115, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Cantidad inicial";
            // 
            // nCantidad
            // 
            this.nCantidad.BackColor = System.Drawing.SystemColors.Window;
            this.nCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nCantidad.Location = new System.Drawing.Point(118, 120);
            this.nCantidad.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nCantidad.Name = "nCantidad";
            this.nCantidad.Size = new System.Drawing.Size(110, 20);
            this.nCantidad.TabIndex = 10;
            this.nCantidad.ThousandsSeparator = true;
            // 
            // frmNuevoBoton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 197);
            this.Controls.Add(this.nCantidad);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTalla);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEstilo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAgregarBoton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(256, 231);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(256, 231);
            this.Name = "frmNuevoBoton";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Botón nuevo";
            ((System.ComponentModel.ISupportInitialize)(this.nCantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAgregarBoton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEstilo;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTalla;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nCantidad;
    }
}