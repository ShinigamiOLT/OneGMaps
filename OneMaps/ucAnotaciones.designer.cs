namespace Maps
{
    partial class ucAnotaciones
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbTabla = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbX = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbY = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbNum = new System.Windows.Forms.ComboBox();
            this.cbLetra = new System.Windows.Forms.ComboBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbTabla
            // 
            this.cbTabla.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTabla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbTabla.FormattingEnabled = true;
            this.cbTabla.Location = new System.Drawing.Point(99, 10);
            this.cbTabla.Name = "cbTabla";
            this.cbTabla.Size = new System.Drawing.Size(184, 21);
            this.cbTabla.TabIndex = 0;
            this.cbTabla.SelectedIndexChanged += new System.EventHandler(this.cbTabla_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(208, 118);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Aplicar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Selecciona Tabla";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Coordenada X";
            // 
            // cbX
            // 
            this.cbX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbX.FormattingEnabled = true;
            this.cbX.Location = new System.Drawing.Point(99, 37);
            this.cbX.Name = "cbX";
            this.cbX.Size = new System.Drawing.Size(184, 21);
            this.cbX.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Coordenada Y";
            // 
            // cbY
            // 
            this.cbY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbY.FormattingEnabled = true;
            this.cbY.Location = new System.Drawing.Point(99, 64);
            this.cbY.Name = "cbY";
            this.cbY.Size = new System.Drawing.Size(184, 21);
            this.cbY.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Zona UTM";
            // 
            // cbNum
            // 
            this.cbNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbNum.FormattingEnabled = true;
            this.cbNum.Items.AddRange(new object[] {
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.cbNum.Location = new System.Drawing.Point(99, 91);
            this.cbNum.Name = "cbNum";
            this.cbNum.Size = new System.Drawing.Size(86, 21);
            this.cbNum.TabIndex = 8;
            // 
            // cbLetra
            // 
            this.cbLetra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLetra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbLetra.FormattingEnabled = true;
            this.cbLetra.Items.AddRange(new object[] {
            "Q",
            "P",
            "R"});
            this.cbLetra.Location = new System.Drawing.Point(197, 91);
            this.cbLetra.Name = "cbLetra";
            this.cbLetra.Size = new System.Drawing.Size(86, 21);
            this.cbLetra.TabIndex = 9;
            // 
            // btnEliminar
            // 
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Location = new System.Drawing.Point(127, 118);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 10;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // ucAnotaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.cbLetra);
            this.Controls.Add(this.cbNum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbY);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbTabla);
            this.Name = "ucAnotaciones";
            this.Size = new System.Drawing.Size(285, 147);
            this.Load += new System.EventHandler(this.ucAnotaciones_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbTabla;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbNum;
        private System.Windows.Forms.ComboBox cbLetra;
        private System.Windows.Forms.Button btnEliminar;
    }
}
