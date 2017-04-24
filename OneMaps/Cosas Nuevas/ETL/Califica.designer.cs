namespace Maps
{
    partial class CalificaTabla
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalificaTabla));
            this.cmbIzquierda = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbDerecha = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.explorerBar1 = new DevComponents.DotNetBar.ExplorerBar();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbB = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label6 = new System.Windows.Forms.Label();
            this.contenedorPrincipal = new DevComponents.DotNetBar.ExplorerBarGroupItem();
            this.labelItem4 = new DevComponents.DotNetBar.LabelItem();
            this.labelItem3 = new DevComponents.DotNetBar.LabelItem();
            this.controlContainerItem6 = new DevComponents.DotNetBar.ControlContainerItem();
            this.labelItem2 = new DevComponents.DotNetBar.LabelItem();
            this.btnSeleccion = new DevComponents.DotNetBar.ButtonItem();
            this.btnAgregar = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.controlContainerItem4 = new DevComponents.DotNetBar.ControlContainerItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.dgv_origen = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Notificacion = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.explorerBar1)).BeginInit();
            this.explorerBar1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_origen)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbIzquierda
            // 
            this.cmbIzquierda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbIzquierda.DisplayMember = "Text";
            this.cmbIzquierda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIzquierda.FormattingEnabled = true;
            this.cmbIzquierda.ItemHeight = 15;
            this.cmbIzquierda.Location = new System.Drawing.Point(113, 107);
            this.cmbIzquierda.Name = "cmbIzquierda";
            this.cmbIzquierda.Size = new System.Drawing.Size(127, 23);
            this.cmbIzquierda.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbIzquierda.TabIndex = 1;
            this.cmbIzquierda.SelectedIndexChanged += new System.EventHandler(this.cmbIzquierda_SelectedIndexChanged);
            // 
            // cmbDerecha
            // 
            this.cmbDerecha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDerecha.DisplayMember = "Text";
            this.cmbDerecha.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDerecha.FormattingEnabled = true;
            this.cmbDerecha.ItemHeight = 15;
            this.cmbDerecha.Location = new System.Drawing.Point(113, 164);
            this.cmbDerecha.Name = "cmbDerecha";
            this.cmbDerecha.Size = new System.Drawing.Size(127, 23);
            this.cmbDerecha.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbDerecha.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.label1.Location = new System.Drawing.Point(6, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 45);
            this.label1.TabIndex = 6;
            this.label1.Text = "Criterio de \r\ncomparación\r\nde: ";
            // 
            // explorerBar1
            // 
            this.explorerBar1.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.explorerBar1.BackColor = System.Drawing.SystemColors.Control;
            this.explorerBar1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            // 
            // 
            // 
            this.explorerBar1.BackStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(200)))), ((int)(((byte)(212)))));
            this.explorerBar1.BackStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.explorerBar1.BackStyle.BackColorGradientAngle = 90;
            this.explorerBar1.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBar1.ContentMargin = 1;
            this.explorerBar1.Controls.Add(this.panel4);
            this.explorerBar1.Cursor = System.Windows.Forms.Cursors.Default;
            this.explorerBar1.Dock = System.Windows.Forms.DockStyle.Left;
            this.explorerBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.explorerBar1.GroupImages = null;
            this.explorerBar1.Groups.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.contenedorPrincipal,
            this.labelItem1});
            this.explorerBar1.Images = null;
            this.explorerBar1.Location = new System.Drawing.Point(0, 0);
            this.explorerBar1.Name = "explorerBar1";
            this.explorerBar1.Size = new System.Drawing.Size(260, 601);
            this.explorerBar1.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.Blue;
            this.explorerBar1.TabIndex = 17;
            this.explorerBar1.Text = "explorerBar1";
            this.explorerBar1.ThemeAware = true;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.cmbIzquierda);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.cmbDerecha);
            this.panel4.Controls.Add(this.cbB);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label1);
            this.panel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(182)))), ((int)(((byte)(216)))));
            this.panel4.Location = new System.Drawing.Point(7, 54);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(246, 210);
            this.panel4.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.label7.Location = new System.Drawing.Point(6, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 15);
            this.label7.TabIndex = 10;
            this.label7.Text = "Selección de Criterios ";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.label5.Location = new System.Drawing.Point(6, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 45);
            this.label5.TabIndex = 7;
            this.label5.Text = "Criterio de \r\ncomparación\r\nde: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 15);
            this.label3.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.label4.Location = new System.Drawing.Point(6, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Para la Tabla:        ";
            // 
            // cbB
            // 
            this.cbB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbB.DisplayMember = "Text";
            this.cbB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbB.FormattingEnabled = true;
            this.cbB.ItemHeight = 15;
            this.cbB.Location = new System.Drawing.Point(113, 3);
            this.cbB.Name = "cbB";
            this.cbB.Size = new System.Drawing.Size(127, 23);
            this.cbB.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbB.TabIndex = 1;
            this.cbB.SelectedIndexChanged += new System.EventHandler(this.cbB_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.label6.Location = new System.Drawing.Point(6, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "De la Tabla";
            // 
            // contenedorPrincipal
            // 
            // 
            // 
            // 
            this.contenedorPrincipal.BackStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(223)))), ((int)(((byte)(247)))));
            this.contenedorPrincipal.BackStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.contenedorPrincipal.BackStyle.BorderBottomWidth = 1;
            this.contenedorPrincipal.BackStyle.BorderColor = System.Drawing.Color.White;
            this.contenedorPrincipal.BackStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.contenedorPrincipal.BackStyle.BorderLeftWidth = 1;
            this.contenedorPrincipal.BackStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.contenedorPrincipal.BackStyle.BorderRightWidth = 1;
            this.contenedorPrincipal.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.contenedorPrincipal.Cursor = System.Windows.Forms.Cursors.Default;
            this.contenedorPrincipal.ExpandButtonVisible = false;
            this.contenedorPrincipal.Expanded = true;
            this.contenedorPrincipal.Name = "contenedorPrincipal";
            this.contenedorPrincipal.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.Blue;
            this.contenedorPrincipal.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem4,
            this.labelItem3,
            this.controlContainerItem6,
            this.labelItem2,
            this.btnSeleccion,
            this.btnAgregar});
            this.contenedorPrincipal.Text = "Agregar Columnas";
            // 
            // 
            // 
            this.contenedorPrincipal.TitleHotStyle.BackColor = System.Drawing.Color.White;
            this.contenedorPrincipal.TitleHotStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(211)))), ((int)(((byte)(247)))));
            this.contenedorPrincipal.TitleHotStyle.CornerDiameter = 3;
            this.contenedorPrincipal.TitleHotStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.contenedorPrincipal.TitleHotStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.contenedorPrincipal.TitleHotStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.contenedorPrincipal.TitleHotStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.contenedorPrincipal.TitleStyle.BackColor = System.Drawing.Color.White;
            this.contenedorPrincipal.TitleStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(211)))), ((int)(((byte)(247)))));
            this.contenedorPrincipal.TitleStyle.CornerDiameter = 3;
            this.contenedorPrincipal.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.contenedorPrincipal.TitleStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.contenedorPrincipal.TitleStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.contenedorPrincipal.TitleStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            // 
            // labelItem4
            // 
            this.labelItem4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.labelItem4.Name = "labelItem4";
            this.labelItem4.Text = "   Selección de tablas";
            this.labelItem4.ThemeAware = true;
            // 
            // labelItem3
            // 
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.Text = " ";
            this.labelItem3.ThemeAware = true;
            // 
            // controlContainerItem6
            // 
            this.controlContainerItem6.AllowItemResize = true;
            this.controlContainerItem6.Control = this.panel4;
            this.controlContainerItem6.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem6.Name = "controlContainerItem6";
            this.controlContainerItem6.Text = "controlContainerItem6";
            this.controlContainerItem6.ThemeAware = true;
            // 
            // labelItem2
            // 
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.Text = " ";
            this.labelItem2.ThemeAware = true;
            // 
            // btnSeleccion
            // 
            this.btnSeleccion.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnSeleccion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSeleccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.btnSeleccion.HotFontUnderline = true;
            this.btnSeleccion.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.btnSeleccion.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.btnSeleccion.Name = "btnSeleccion";
            this.btnSeleccion.Text = "4.- Selección de Columnas";
            this.btnSeleccion.Click += new System.EventHandler(this.btnSeleccion_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.btnAgregar.HotFontUnderline = true;
            this.btnAgregar.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.btnAgregar.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Text = "5.- Agregar";
            this.btnAgregar.Click += new System.EventHandler(this.buttonItem1_Click_1);
            // 
            // labelItem1
            // 
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = " ";
            this.labelItem1.ThemeAware = true;
            // 
            // controlContainerItem4
            // 
            this.controlContainerItem4.AllowItemResize = true;
            this.controlContainerItem4.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem4.Name = "controlContainerItem4";
            this.controlContainerItem4.ThemeAware = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(260, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgv_origen);
            this.splitContainer1.Size = new System.Drawing.Size(869, 601);
            this.splitContainer1.SplitterDistance = 37;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // dgv_origen
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_origen.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_origen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_origen.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_origen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_origen.EnableHeadersVisualStyles = false;
            this.dgv_origen.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv_origen.Location = new System.Drawing.Point(0, 0);
            this.dgv_origen.Name = "dgv_origen";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_origen.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_origen.Size = new System.Drawing.Size(869, 563);
            this.dgv_origen.TabIndex = 1;
            // 
            // Notificacion
            // 
            this.Notificacion.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.Notificacion.BalloonTipText = "Se están procesando multiples operaciones";
            this.Notificacion.BalloonTipTitle = "Espere";
            this.Notificacion.Icon = ((System.Drawing.Icon)(resources.GetObject("Notificacion.Icon")));
            this.Notificacion.Text = "notifyIcon1";
            this.Notificacion.Visible = true;
            // 
            // CalificaTabla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 601);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.explorerBar1);
            this.DoubleBuffered = true;
            this.Name = "CalificaTabla";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Agregar Etiquetas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CalificaTabla_FormClosing);
            this.Load += new System.EventHandler(this.Comparativa_de_tabla_Load);
            ((System.ComponentModel.ISupportInitialize)(this.explorerBar1)).EndInit();
            this.explorerBar1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_origen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbIzquierda;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbDerecha;
        private System.Windows.Forms.Label label1;
        public DevComponents.DotNetBar.ExplorerBar explorerBar1;
        private DevComponents.DotNetBar.ExplorerBarGroupItem contenedorPrincipal;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem3;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbB;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem6;
        private DevComponents.DotNetBar.LabelItem labelItem3;
        private DevComponents.DotNetBar.LabelItem labelItem2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.ButtonItem btnSeleccion;
        private DevComponents.DotNetBar.ButtonItem btnAgregar;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.LabelItem labelItem4;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_origen;
        private System.Windows.Forms.NotifyIcon Notificacion;
        private System.Windows.Forms.Label label7;
    }
}