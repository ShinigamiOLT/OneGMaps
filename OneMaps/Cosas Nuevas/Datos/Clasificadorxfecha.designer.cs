namespace Maps
{
    partial class Clasificadorxfecha
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.dgv_inicio = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Operadores = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Valores = new DevComponents.DotNetBar.Controls.DataGridViewMaskedTextBoxAdvColumn();
            this.dgv_fin = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.operador2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn1 = new DevComponents.DotNetBar.Controls.DataGridViewMaskedTextBoxAdvColumn();
            this.checkBoxX1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cmdAplicar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_inicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_fin)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.Color.Black;
            this.labelX1.Location = new System.Drawing.Point(80, 7);
            this.labelX1.Name = "labelX1";
            this.labelX1.SingleLineColor = System.Drawing.Color.Black;
            this.labelX1.Size = new System.Drawing.Size(110, 15);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Clasificar por Fechas";
            // 
            // dgv_inicio
            // 
            this.dgv_inicio.AllowUserToAddRows = false;
            this.dgv_inicio.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dgv_inicio.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_inicio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_inicio.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Operadores,
            this.Valores});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_inicio.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_inicio.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.dgv_inicio.Location = new System.Drawing.Point(17, 35);
            this.dgv_inicio.Name = "dgv_inicio";
            this.dgv_inicio.Size = new System.Drawing.Size(237, 44);
            this.dgv_inicio.TabIndex = 2;
            this.dgv_inicio.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellEndEdit);
            // 
            // Operadores
            // 
            this.Operadores.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Operadores.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.Operadores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Operadores.HeaderText = "Operador";
            this.Operadores.Name = "Operadores";
            this.Operadores.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Operadores.Width = 57;
            // 
            // Valores
            // 
            this.Valores.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.Valores.BackgroundStyle.Class = "DataGridViewBorder";
            this.Valores.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Valores.Culture = new System.Globalization.CultureInfo("es-MX");
            this.Valores.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Valores.HeaderText = "Valor";
            this.Valores.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Valores.Mask = "99/99/9999";
            this.Valores.Name = "Valores";
            this.Valores.PasswordChar = '\0';
            this.Valores.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Valores.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Valores.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Valores.Text = "  /  /";
            // 
            // dgv_fin
            // 
            this.dgv_fin.AllowUserToAddRows = false;
            this.dgv_fin.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dgv_fin.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_fin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_fin.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operador2,
            this.dataGridViewTextBoxColumn1});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_fin.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_fin.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.dgv_fin.Location = new System.Drawing.Point(17, 114);
            this.dgv_fin.Name = "dgv_fin";
            this.dgv_fin.Size = new System.Drawing.Size(237, 49);
            this.dgv_fin.TabIndex = 3;
            this.dgv_fin.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX2_CellEndEdit);
            // 
            // operador2
            // 
            this.operador2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.operador2.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.operador2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.operador2.HeaderText = "Operador";
            this.operador2.Name = "operador2";
            this.operador2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.operador2.Width = 57;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.dataGridViewTextBoxColumn1.BackgroundStyle.Class = "DataGridViewBorder";
            this.dataGridViewTextBoxColumn1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dataGridViewTextBoxColumn1.Culture = new System.Globalization.CultureInfo("es-MX");
            this.dataGridViewTextBoxColumn1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridViewTextBoxColumn1.HeaderText = "Valor";
            this.dataGridViewTextBoxColumn1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dataGridViewTextBoxColumn1.Mask = "99/99/9999";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.PasswordChar = '\0';
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Text = "  /  /";
            // 
            // checkBoxX1
            // 
            this.checkBoxX1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // 
            // 
            this.checkBoxX1.BackgroundStyle.Class = "";
            this.checkBoxX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxX1.Location = new System.Drawing.Point(117, 85);
            this.checkBoxX1.Name = "checkBoxX1";
            this.checkBoxX1.Size = new System.Drawing.Size(57, 23);
            this.checkBoxX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX1.TabIndex = 4;
            this.checkBoxX1.Text = "Entre";
            this.checkBoxX1.CheckedChanged += new System.EventHandler(this.checkBoxX1_CheckedChanged);
            // 
            // labelX2
            // 
            this.labelX2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.Color.Black;
            this.labelX2.Location = new System.Drawing.Point(13, 173);
            this.labelX2.Name = "labelX2";
            this.labelX2.SingleLineColor = System.Drawing.Color.Black;
            this.labelX2.Size = new System.Drawing.Size(245, 37);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "Clasificar por Fechas";
            this.labelX2.Click += new System.EventHandler(this.labelX2_Click);
            // 
            // cmdAplicar
            // 
            this.cmdAplicar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdAplicar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmdAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdAplicar.Location = new System.Drawing.Point(98, 221);
            this.cmdAplicar.Name = "cmdAplicar";
            this.cmdAplicar.Size = new System.Drawing.Size(75, 23);
            this.cmdAplicar.TabIndex = 7;
            this.cmdAplicar.Text = "&Aplicar";
            this.cmdAplicar.Click += new System.EventHandler(this.cmdAplicar_Click);
            // 
            // Clasificadorxfecha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdAplicar);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.checkBoxX1);
            this.Controls.Add(this.dgv_fin);
            this.Controls.Add(this.dgv_inicio);
            this.Controls.Add(this.labelX1);
            this.Name = "Clasificadorxfecha";
            this.Size = new System.Drawing.Size(271, 257);
            this.Load += new System.EventHandler(this.Clasificadorxfecha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_inicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_fin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_inicio;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_fin;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.DataGridViewComboBoxColumn Operadores;
        private DevComponents.DotNetBar.Controls.DataGridViewMaskedTextBoxAdvColumn Valores;
        private System.Windows.Forms.DataGridViewComboBoxColumn operador2;
        private DevComponents.DotNetBar.Controls.DataGridViewMaskedTextBoxAdvColumn dataGridViewTextBoxColumn1;
        public System.Windows.Forms.Button cmdAplicar;
    }
}