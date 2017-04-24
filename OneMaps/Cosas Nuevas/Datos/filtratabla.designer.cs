namespace Maps
{
    partial class filtratabla
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ElementoFiltro = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Column1 = new DevComponents.DotNetBar.Controls.DataGridViewTextBoxDropDownColumn();
            this.splitElementos = new System.Windows.Forms.SplitContainer();
            this.panel_controles = new System.Windows.Forms.Panel();
            this.cmdGuardarfiltro = new System.Windows.Forms.Button();
            this.PanelDerecho = new System.Windows.Forms.TableLayoutPanel();
            this.userFiltroElemento1 = new Maps.userFiltroElemento();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdAplicarFiltro = new System.Windows.Forms.Button();
            this.PanelTipo = new System.Windows.Forms.Panel();
            this.buttonX3 = new System.Windows.Forms.Button();
            this.cmdTipos = new System.Windows.Forms.Button();
            this.cmdValor = new System.Windows.Forms.Button();
            this.ejemplo = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Divisor_primario = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.ElementoFiltro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitElementos)).BeginInit();
            this.splitElementos.Panel1.SuspendLayout();
            this.splitElementos.Panel2.SuspendLayout();
            this.splitElementos.SuspendLayout();
            this.panel_controles.SuspendLayout();
            this.PanelDerecho.SuspendLayout();
            this.panel2.SuspendLayout();
            this.PanelTipo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ejemplo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Divisor_primario)).BeginInit();
            this.Divisor_primario.Panel2.SuspendLayout();
            this.Divisor_primario.SuspendLayout();
            this.SuspendLayout();
            // 
            // ElementoFiltro
            // 
            this.ElementoFiltro.AllowUserToAddRows = false;
            this.ElementoFiltro.AllowUserToDeleteRows = false;
            this.ElementoFiltro.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ElementoFiltro.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ElementoFiltro.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ElementoFiltro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ElementoFiltro.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ElementoFiltro.DefaultCellStyle = dataGridViewCellStyle2;
            this.ElementoFiltro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ElementoFiltro.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.ElementoFiltro.Location = new System.Drawing.Point(0, 0);
            this.ElementoFiltro.Name = "ElementoFiltro";
            this.ElementoFiltro.ReadOnly = true;
            this.ElementoFiltro.RowHeadersVisible = false;
            this.ElementoFiltro.Size = new System.Drawing.Size(159, 672);
            this.ElementoFiltro.TabIndex = 0;
            this.ElementoFiltro.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellClick);
            this.ElementoFiltro.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ElementoFiltro_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.Column1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.Column1.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.Column1.BackgroundStyle.Class = "DataGridViewIpAddressBorder";
            this.Column1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Column1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.Column1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Column1.HeaderText = "Col Variable";
            this.Column1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Column1.Name = "Column1";
            this.Column1.PasswordChar = '\0';
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Text = "";
            // 
            // splitElementos
            // 
            this.splitElementos.Dock = System.Windows.Forms.DockStyle.Left;
            this.splitElementos.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitElementos.Location = new System.Drawing.Point(0, 0);
            this.splitElementos.Name = "splitElementos";
            // 
            // splitElementos.Panel1
            // 
            this.splitElementos.Panel1.Controls.Add(this.ElementoFiltro);
            this.splitElementos.Panel1.Controls.Add(this.panel_controles);
            // 
            // splitElementos.Panel2
            // 
            this.splitElementos.Panel2.Controls.Add(this.PanelDerecho);
            this.splitElementos.Size = new System.Drawing.Size(428, 745);
            this.splitElementos.SplitterDistance = 159;
            this.splitElementos.TabIndex = 3;
            // 
            // panel_controles
            // 
            this.panel_controles.Controls.Add(this.cmdGuardarfiltro);
            this.panel_controles.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_controles.Location = new System.Drawing.Point(0, 672);
            this.panel_controles.Name = "panel_controles";
            this.panel_controles.Size = new System.Drawing.Size(159, 73);
            this.panel_controles.TabIndex = 0;
            // 
            // cmdGuardarfiltro
            // 
            this.cmdGuardarfiltro.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdGuardarfiltro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdGuardarfiltro.Location = new System.Drawing.Point(27, 24);
            this.cmdGuardarfiltro.Name = "cmdGuardarfiltro";
            this.cmdGuardarfiltro.Size = new System.Drawing.Size(105, 24);
            this.cmdGuardarfiltro.TabIndex = 4;
            this.cmdGuardarfiltro.Text = "Guardar Filtro";
            this.cmdGuardarfiltro.Click += new System.EventHandler(this.button1_Click);
            // 
            // PanelDerecho
            // 
            this.PanelDerecho.AutoScroll = true;
            this.PanelDerecho.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PanelDerecho.ColumnCount = 1;
            this.PanelDerecho.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelDerecho.Controls.Add(this.userFiltroElemento1, 0, 1);
            this.PanelDerecho.Controls.Add(this.panel2, 0, 2);
            this.PanelDerecho.Controls.Add(this.PanelTipo, 0, 0);
            this.PanelDerecho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelDerecho.Location = new System.Drawing.Point(0, 0);
            this.PanelDerecho.Name = "PanelDerecho";
            this.PanelDerecho.RowCount = 3;
            this.PanelDerecho.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.PanelDerecho.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelDerecho.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.PanelDerecho.Size = new System.Drawing.Size(265, 745);
            this.PanelDerecho.TabIndex = 0;
            // 
            // userFiltroElemento1
            // 
            this.userFiltroElemento1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userFiltroElemento1.AutoSize = true;
            this.userFiltroElemento1.Location = new System.Drawing.Point(3, 38);
            this.userFiltroElemento1.Name = "userFiltroElemento1";
            this.userFiltroElemento1.Size = new System.Drawing.Size(259, 628);
            this.userFiltroElemento1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel2.Controls.Add(this.cmdAplicarFiltro);
            this.panel2.Location = new System.Drawing.Point(3, 672);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(259, 70);
            this.panel2.TabIndex = 5;
            // 
            // cmdAplicarFiltro
            // 
            this.cmdAplicarFiltro.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdAplicarFiltro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdAplicarFiltro.Location = new System.Drawing.Point(77, 23);
            this.cmdAplicarFiltro.Name = "cmdAplicarFiltro";
            this.cmdAplicarFiltro.Size = new System.Drawing.Size(105, 24);
            this.cmdAplicarFiltro.TabIndex = 3;
            this.cmdAplicarFiltro.Text = "Aplicar";
            this.cmdAplicarFiltro.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // PanelTipo
            // 
            this.PanelTipo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PanelTipo.Controls.Add(this.buttonX3);
            this.PanelTipo.Controls.Add(this.cmdTipos);
            this.PanelTipo.Controls.Add(this.cmdValor);
            this.PanelTipo.Location = new System.Drawing.Point(4, 3);
            this.PanelTipo.Name = "PanelTipo";
            this.PanelTipo.Size = new System.Drawing.Size(257, 29);
            this.PanelTipo.TabIndex = 4;
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonX3.Location = new System.Drawing.Point(182, 2);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(74, 24);
            this.buttonX3.TabIndex = 4;
            this.buttonX3.Text = "Fecha";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // cmdTipos
            // 
            this.cmdTipos.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdTipos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdTipos.Location = new System.Drawing.Point(0, 3);
            this.cmdTipos.Name = "cmdTipos";
            this.cmdTipos.Size = new System.Drawing.Size(74, 24);
            this.cmdTipos.TabIndex = 3;
            this.cmdTipos.Text = "Tipo";
            this.cmdTipos.Click += new System.EventHandler(this.cmdTipos_Click);
            // 
            // cmdValor
            // 
            this.cmdValor.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdValor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdValor.Location = new System.Drawing.Point(91, 3);
            this.cmdValor.Name = "cmdValor";
            this.cmdValor.Size = new System.Drawing.Size(74, 24);
            this.cmdValor.TabIndex = 2;
            this.cmdValor.Text = "Valor";
            this.cmdValor.Click += new System.EventHandler(this.cmdValor_Click);
            // 
            // ejemplo
            // 
            this.ejemplo.BackgroundColor = System.Drawing.SystemColors.Control;
            this.ejemplo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ejemplo.DefaultCellStyle = dataGridViewCellStyle3;
            this.ejemplo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ejemplo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.ejemplo.Location = new System.Drawing.Point(428, 0);
            this.ejemplo.Name = "ejemplo";
            this.ejemplo.Size = new System.Drawing.Size(664, 745);
            this.ejemplo.TabIndex = 4;
            // 
            // Divisor_primario
            // 
            this.Divisor_primario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Divisor_primario.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.Divisor_primario.Location = new System.Drawing.Point(0, 0);
            this.Divisor_primario.Name = "Divisor_primario";
            this.Divisor_primario.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Divisor_primario.Panel1
            // 
            this.Divisor_primario.Panel1.AutoScroll = true;
            this.Divisor_primario.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // Divisor_primario.Panel2
            // 
            this.Divisor_primario.Panel2.Controls.Add(this.ejemplo);
            this.Divisor_primario.Panel2.Controls.Add(this.splitElementos);
            this.Divisor_primario.Size = new System.Drawing.Size(1092, 774);
            this.Divisor_primario.SplitterDistance = 25;
            this.Divisor_primario.SplitterIncrement = 30;
            this.Divisor_primario.TabIndex = 5;
            // 
            // filtratabla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 774);
            this.Controls.Add(this.Divisor_primario);
            this.Name = "filtratabla";
            this.Text = "filtratabla";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.filtratabla_FormClosing);
            this.Load += new System.EventHandler(this.filtratabla_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ElementoFiltro)).EndInit();
            this.splitElementos.Panel1.ResumeLayout(false);
            this.splitElementos.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitElementos)).EndInit();
            this.splitElementos.ResumeLayout(false);
            this.panel_controles.ResumeLayout(false);
            this.PanelDerecho.ResumeLayout(false);
            this.PanelDerecho.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.PanelTipo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ejemplo)).EndInit();
            this.Divisor_primario.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Divisor_primario)).EndInit();
            this.Divisor_primario.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX ElementoFiltro;
        private System.Windows.Forms.SplitContainer splitElementos;
        private System.Windows.Forms.TableLayoutPanel PanelDerecho;
        private System.Windows.Forms.Panel PanelTipo;
        private DevComponents.DotNetBar.Controls.DataGridViewX ejemplo;
        private System.Windows.Forms.Panel panel2;
        private userFiltroElemento userFiltroElemento1;
        private DevComponents.DotNetBar.Controls.DataGridViewTextBoxDropDownColumn Column1;
        private System.Windows.Forms.SplitContainer Divisor_primario;
        private System.Windows.Forms.Button buttonX3;
        private System.Windows.Forms.Button cmdTipos;
        private System.Windows.Forms.Button cmdValor;
        private System.Windows.Forms.Button cmdAplicarFiltro;
        private System.Windows.Forms.Panel panel_controles;
        private System.Windows.Forms.Button cmdGuardarfiltro;
    }
}