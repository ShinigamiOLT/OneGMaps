namespace Maps
{
    partial class SoloNombre
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
            this.txtxName = new System.Windows.Forms.TextBox();
            this.cmdGuardarfiltro = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtxName
            // 
            this.txtxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtxName.Location = new System.Drawing.Point(12, 22);
            this.txtxName.Name = "txtxName";
            this.txtxName.Size = new System.Drawing.Size(180, 22);
            this.txtxName.TabIndex = 0;
            // 
            // cmdGuardarfiltro
            // 
            this.cmdGuardarfiltro.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdGuardarfiltro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdGuardarfiltro.Location = new System.Drawing.Point(213, 21);
            this.cmdGuardarfiltro.Name = "cmdGuardarfiltro";
            this.cmdGuardarfiltro.Size = new System.Drawing.Size(97, 24);
            this.cmdGuardarfiltro.TabIndex = 5;
            this.cmdGuardarfiltro.Text = "Guardar";
            this.cmdGuardarfiltro.Click += new System.EventHandler(this.cmdGuardarfiltro_Click);
            // 
            // SoloNombre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 67);
            this.Controls.Add(this.cmdGuardarfiltro);
            this.Controls.Add(this.txtxName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SoloNombre";
            this.Text = "Inserte el Nombre de la Tabla";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdGuardarfiltro;
        public System.Windows.Forms.TextBox txtxName;
    }
}