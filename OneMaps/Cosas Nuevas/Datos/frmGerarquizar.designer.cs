namespace Maps
{
    partial class frmGerarquizar
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.explorerBar1 = new DevComponents.DotNetBar.ExplorerBar();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboBoxEx2 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.dgv_Controles = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.cmdGraficar = new System.Windows.Forms.Button();
            this.explorerBarGroupItem1 = new DevComponents.DotNetBar.ExplorerBarGroupItem();
            this.labelItem6 = new DevComponents.DotNetBar.LabelItem();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.controlContainerItem1 = new DevComponents.DotNetBar.ControlContainerItem();
            this.labelItem2 = new DevComponents.DotNetBar.LabelItem();
            this.controlContainerItem2 = new DevComponents.DotNetBar.ControlContainerItem();
            this.labelItem3 = new DevComponents.DotNetBar.LabelItem();
            this.labelItem5 = new DevComponents.DotNetBar.LabelItem();
            this.controlContainerItem3 = new DevComponents.DotNetBar.ControlContainerItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem4 = new DevComponents.DotNetBar.LabelItem();
            this._contenedor_cmdcargar = new DevComponents.DotNetBar.ControlContainerItem();
            this.chOrigenes = new DevComponents.DotNetBar.CheckBoxItem();
            this.ch_sinCruz = new DevComponents.DotNetBar.CheckBoxItem();
            this.ch_automatico = new DevComponents.DotNetBar.CheckBoxItem();
            this.dgvSumatoria = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Grafica_Estado_Mecanico = new Gigasoft.ProEssentials.Pesgo();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.TabPrincipal = new DevComponents.DotNetBar.SuperTabControl();
            this.TabControlCriterio = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.TabCriterios = new DevComponents.DotNetBar.SuperTabItem();
            this.TabControlResultado = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.TabResultado = new DevComponents.DotNetBar.SuperTabItem();
            this.ColOpeTabla = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.explorerBar1)).BeginInit();
            this.explorerBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Controles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSumatoria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TabPrincipal)).BeginInit();
            this.TabPrincipal.SuspendLayout();
            this.TabControlCriterio.SuspendLayout();
            this.TabControlResultado.SuspendLayout();
            this.SuspendLayout();
            // 
            // explorerBar1
            // 
            this.explorerBar1.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.explorerBar1.BackColor = System.Drawing.SystemColors.Control;
            this.explorerBar1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            // 
            // 
            // 
            this.explorerBar1.BackStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(241)))), ((int)(((byte)(240)))));
            this.explorerBar1.BackStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(241)))), ((int)(((byte)(240)))));
            this.explorerBar1.BackStyle.BackColorGradientAngle = 90;
            this.explorerBar1.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBar1.BackStyle.TextColor = System.Drawing.SystemColors.Highlight;
            this.explorerBar1.ContentMargin = 1;
            this.explorerBar1.Controls.Add(this.comboBoxEx1);
            this.explorerBar1.Controls.Add(this.comboBoxEx2);
            this.explorerBar1.Controls.Add(this.dgv_Controles);
            this.explorerBar1.Controls.Add(this.cmdGraficar);
            this.explorerBar1.Cursor = System.Windows.Forms.Cursors.Default;
            this.explorerBar1.Dock = System.Windows.Forms.DockStyle.Left;
            this.explorerBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.explorerBar1.GroupImages = null;
            this.explorerBar1.Groups.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.explorerBarGroupItem1});
            this.explorerBar1.Images = null;
            this.explorerBar1.Location = new System.Drawing.Point(0, 0);
            this.explorerBar1.Name = "explorerBar1";
            this.explorerBar1.Size = new System.Drawing.Size(245, 782);
            this.explorerBar1.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.SystemColors;
            this.explorerBar1.TabIndex = 17;
            this.explorerBar1.Text = "explorerBar1";
            this.explorerBar1.ThemeAware = true;
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.DisplayMember = "Text";
            this.comboBoxEx1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ItemHeight = 15;
            this.comboBoxEx1.Location = new System.Drawing.Point(32, 54);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(180, 23);
            this.comboBoxEx1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx1.TabIndex = 4;
            this.comboBoxEx1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEx1_SelectedIndexChanged);
            // 
            // comboBoxEx2
            // 
            this.comboBoxEx2.DisplayMember = "Text";
            this.comboBoxEx2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEx2.FormattingEnabled = true;
            this.comboBoxEx2.ItemHeight = 15;
            this.comboBoxEx2.Location = new System.Drawing.Point(29, 93);
            this.comboBoxEx2.Name = "comboBoxEx2";
            this.comboBoxEx2.Size = new System.Drawing.Size(186, 23);
            this.comboBoxEx2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx2.TabIndex = 19;
            this.comboBoxEx2.SelectedIndexChanged += new System.EventHandler(this.comboBoxEx2_SelectedIndexChanged);
            // 
            // dgv_Controles
            // 
            this.dgv_Controles.AllowUserToAddRows = false;
            this.dgv_Controles.AllowUserToOrderColumns = true;
            this.dgv_Controles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Controles.BackgroundColor = System.Drawing.Color.White;
            this.dgv_Controles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Controles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Controles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Controles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColOpeTabla,
            this.ColNombre,
            this.colActivo});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Controles.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Controles.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.dgv_Controles.Location = new System.Drawing.Point(7, 146);
            this.dgv_Controles.Name = "dgv_Controles";
            this.dgv_Controles.RowHeadersVisible = false;
            this.dgv_Controles.Size = new System.Drawing.Size(231, 112);
            this.dgv_Controles.TabIndex = 52;
            this.dgv_Controles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dgv_Controles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dgv_Controles.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_Controles_DataError);
            // 
            // cmdGraficar
            // 
            this.cmdGraficar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdGraficar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdGraficar.Location = new System.Drawing.Point(60, 296);
            this.cmdGraficar.Name = "cmdGraficar";
            this.cmdGraficar.Size = new System.Drawing.Size(124, 23);
            this.cmdGraficar.TabIndex = 53;
            this.cmdGraficar.Text = "Graficar";
            this.cmdGraficar.UseVisualStyleBackColor = true;
            this.cmdGraficar.Click += new System.EventHandler(this.cmdGraficar_Click);
            // 
            // explorerBarGroupItem1
            // 
            // 
            // 
            // 
            this.explorerBarGroupItem1.BackStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(223)))), ((int)(((byte)(247)))));
            this.explorerBarGroupItem1.BackStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem1.BackStyle.BorderBottomWidth = 1;
            this.explorerBarGroupItem1.BackStyle.BorderColor = System.Drawing.Color.White;
            this.explorerBarGroupItem1.BackStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem1.BackStyle.BorderLeftWidth = 1;
            this.explorerBarGroupItem1.BackStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem1.BackStyle.BorderRightWidth = 1;
            this.explorerBarGroupItem1.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem1.CanCustomize = false;
            this.explorerBarGroupItem1.Cursor = System.Windows.Forms.Cursors.Default;
            this.explorerBarGroupItem1.ExpandButtonVisible = false;
            this.explorerBarGroupItem1.Expanded = true;
            this.explorerBarGroupItem1.Name = "explorerBarGroupItem1";
            this.explorerBarGroupItem1.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.SystemColors;
            this.explorerBarGroupItem1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem6,
            this.labelItem1,
            this.controlContainerItem1,
            this.labelItem2,
            this.controlContainerItem2,
            this.labelItem3,
            this.labelItem5,
            this.controlContainerItem3,
            this.buttonItem2,
            this.labelItem4,
            this._contenedor_cmdcargar,
            this.chOrigenes,
            this.ch_sinCruz,
            this.ch_automatico});
            this.explorerBarGroupItem1.Text = "Heterogeneizacion de Grupos";
            // 
            // 
            // 
            this.explorerBarGroupItem1.TitleHotStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem1.TitleHotStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(211)))), ((int)(((byte)(247)))));
            this.explorerBarGroupItem1.TitleHotStyle.CornerDiameter = 3;
            this.explorerBarGroupItem1.TitleHotStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem1.TitleHotStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem1.TitleHotStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem1.TitleHotStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.explorerBarGroupItem1.TitleStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem1.TitleStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(211)))), ((int)(((byte)(247)))));
            this.explorerBarGroupItem1.TitleStyle.CornerDiameter = 3;
            this.explorerBarGroupItem1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem1.TitleStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem1.TitleStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem1.TitleStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            // 
            // labelItem6
            // 
            this.labelItem6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.labelItem6.Height = 34;
            this.labelItem6.Name = "labelItem6";
            this.labelItem6.Text = " ";
            this.labelItem6.ThemeAware = true;
            // 
            // labelItem1
            // 
            this.labelItem1.ForeColor = System.Drawing.Color.Black;
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "        Seleccione Columna Ix";
            this.labelItem1.ThemeAware = true;
            // 
            // controlContainerItem1
            // 
            this.controlContainerItem1.AllowItemResize = false;
            this.controlContainerItem1.Control = this.comboBoxEx1;
            this.controlContainerItem1.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem1.Name = "controlContainerItem1";
            this.controlContainerItem1.Text = "controlContainerItem1";
            this.controlContainerItem1.ThemeAware = true;
            // 
            // labelItem2
            // 
            this.labelItem2.ForeColor = System.Drawing.Color.Black;
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.Text = "        Seleccione Columna Iy";
            this.labelItem2.ThemeAware = true;
            // 
            // controlContainerItem2
            // 
            this.controlContainerItem2.AllowItemResize = false;
            this.controlContainerItem2.Control = this.comboBoxEx2;
            this.controlContainerItem2.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem2.Name = "controlContainerItem2";
            this.controlContainerItem2.Text = "controlContainerItem2";
            this.controlContainerItem2.ThemeAware = true;
            // 
            // labelItem3
            // 
            this.labelItem3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.labelItem3.Height = 34;
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.Text = " ";
            this.labelItem3.ThemeAware = true;
            // 
            // labelItem5
            // 
            this.labelItem5.ForeColor = System.Drawing.Color.Black;
            this.labelItem5.Name = "labelItem5";
            this.labelItem5.Text = "Seleccion de Grupos";
            this.labelItem5.ThemeAware = true;
            // 
            // controlContainerItem3
            // 
            this.controlContainerItem3.AllowItemResize = true;
            this.controlContainerItem3.Control = this.dgv_Controles;
            this.controlContainerItem3.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem3.Name = "controlContainerItem3";
            this.controlContainerItem3.Text = "controlContainerItem3";
            this.controlContainerItem3.ThemeAware = true;
            // 
            // buttonItem2
            // 
            this.buttonItem2.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem2.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem2.HotFontBold = true;
            this.buttonItem2.HotFontUnderline = true;
            this.buttonItem2.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem2.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.PopupFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonItem2.SplitButton = true;
            this.buttonItem2.Stretch = true;
            this.buttonItem2.Text = "         Marcar /Desmarcar Todos";
            this.buttonItem2.Click += new System.EventHandler(this.buttonItem2_Click);
            // 
            // labelItem4
            // 
            this.labelItem4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.labelItem4.Height = 34;
            this.labelItem4.Name = "labelItem4";
            this.labelItem4.Text = " ";
            this.labelItem4.ThemeAware = true;
            // 
            // _contenedor_cmdcargar
            // 
            this._contenedor_cmdcargar.AllowItemResize = false;
            this._contenedor_cmdcargar.Control = this.cmdGraficar;
            this._contenedor_cmdcargar.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this._contenedor_cmdcargar.Name = "_contenedor_cmdcargar";
            this._contenedor_cmdcargar.Text = "controlContainerItem8";
            this._contenedor_cmdcargar.ThemeAware = true;
            // 
            // chOrigenes
            // 
            this.chOrigenes.Name = "chOrigenes";
            this.chOrigenes.Text = "Utilizar Origenes";
            this.chOrigenes.ThemeAware = true;
            // 
            // ch_sinCruz
            // 
            this.ch_sinCruz.Name = "ch_sinCruz";
            this.ch_sinCruz.Text = "Sin Ejes Promedio";
            this.ch_sinCruz.ThemeAware = true;
            // 
            // ch_automatico
            // 
            this.ch_automatico.Name = "ch_automatico";
            this.ch_automatico.Text = "Eje Automatico";
            this.ch_automatico.ThemeAware = true;
            // 
            // dgvSumatoria
            // 
            this.dgvSumatoria.AllowUserToAddRows = false;
            this.dgvSumatoria.AllowUserToOrderColumns = true;
            this.dgvSumatoria.BackgroundColor = System.Drawing.Color.White;
            this.dgvSumatoria.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSumatoria.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSumatoria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSumatoria.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSumatoria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSumatoria.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.dgvSumatoria.Location = new System.Drawing.Point(0, 0);
            this.dgvSumatoria.Name = "dgvSumatoria";
            this.dgvSumatoria.RowHeadersVisible = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSumatoria.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSumatoria.Size = new System.Drawing.Size(1039, 755);
            this.dgvSumatoria.TabIndex = 51;
            // 
            // Grafica_Estado_Mecanico
            // 
            this.Grafica_Estado_Mecanico.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grafica_Estado_Mecanico.Location = new System.Drawing.Point(0, 0);
            this.Grafica_Estado_Mecanico.Name = "Grafica_Estado_Mecanico";
            this.Grafica_Estado_Mecanico.Size = new System.Drawing.Size(1039, 782);
            this.Grafica_Estado_Mecanico.TabIndex = 13;
            this.Grafica_Estado_Mecanico.Text = "Grafica";
            // 
            // buttonItem3
            // 
            this.buttonItem3.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem3.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem3.HotFontBold = true;
            this.buttonItem3.HotFontUnderline = true;
            this.buttonItem3.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem3.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.PopupFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonItem3.SplitButton = true;
            this.buttonItem3.Stretch = true;
            this.buttonItem3.Text = "         Copiar al portapapeles";
            // 
            // TabPrincipal
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.TabPrincipal.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.TabPrincipal.ControlBox.MenuBox.Name = "";
            this.TabPrincipal.ControlBox.Name = "";
            this.TabPrincipal.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.TabPrincipal.ControlBox.MenuBox,
            this.TabPrincipal.ControlBox.CloseBox});
            this.TabPrincipal.Controls.Add(this.TabControlCriterio);
            this.TabPrincipal.Controls.Add(this.TabControlResultado);
            this.TabPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabPrincipal.Location = new System.Drawing.Point(245, 0);
            this.TabPrincipal.Name = "TabPrincipal";
            this.TabPrincipal.ReorderTabsEnabled = false;
            this.TabPrincipal.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.TabPrincipal.SelectedTabIndex = 0;
            this.TabPrincipal.Size = new System.Drawing.Size(1039, 782);
            this.TabPrincipal.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabPrincipal.TabIndex = 31;
            this.TabPrincipal.TabLayoutType = DevComponents.DotNetBar.eSuperTabLayoutType.MultiLine;
            this.TabPrincipal.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.TabCriterios,
            this.TabResultado});
            this.TabPrincipal.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.VisualStudio2008Dock;
            this.TabPrincipal.TabVerticalSpacing = 5;
            this.TabPrincipal.Text = "TabPrimario";
            this.TabPrincipal.TextAlignment = DevComponents.DotNetBar.eItemAlignment.Center;
            // 
            // TabControlCriterio
            // 
            this.TabControlCriterio.Controls.Add(this.dgvSumatoria);
            this.TabControlCriterio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControlCriterio.Location = new System.Drawing.Point(0, 27);
            this.TabControlCriterio.Name = "TabControlCriterio";
            this.TabControlCriterio.Size = new System.Drawing.Size(1039, 755);
            this.TabControlCriterio.TabIndex = 0;
            this.TabControlCriterio.TabItem = this.TabCriterios;
            // 
            // TabCriterios
            // 
            this.TabCriterios.AttachedControl = this.TabControlCriterio;
            this.TabCriterios.GlobalItem = false;
            this.TabCriterios.Name = "TabCriterios";
            this.TabCriterios.Text = "Tabla de resultado";
            this.TabCriterios.Tooltip = "Cruterios a aplicar";
            // 
            // TabControlResultado
            // 
            this.TabControlResultado.Controls.Add(this.Grafica_Estado_Mecanico);
            this.TabControlResultado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControlResultado.Location = new System.Drawing.Point(0, 0);
            this.TabControlResultado.Name = "TabControlResultado";
            this.TabControlResultado.Size = new System.Drawing.Size(1039, 782);
            this.TabControlResultado.TabIndex = 0;
            this.TabControlResultado.TabItem = this.TabResultado;
            // 
            // TabResultado
            // 
            this.TabResultado.AttachedControl = this.TabControlResultado;
            this.TabResultado.GlobalItem = false;
            this.TabResultado.Name = "TabResultado";
            this.TabResultado.Text = "Grafica";
            this.TabResultado.Tooltip = "Resultado de los criterios";
            // 
            // ColOpeTabla
            // 
            this.ColOpeTabla.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ColOpeTabla.HeaderText = "X";
            this.ColOpeTabla.Name = "ColOpeTabla";
            this.ColOpeTabla.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColOpeTabla.Width = 21;
            // 
            // ColNombre
            // 
            this.ColNombre.HeaderText = "Operacion";
            this.ColNombre.Name = "ColNombre";
            this.ColNombre.ReadOnly = true;
            this.ColNombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colActivo
            // 
            this.colActivo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colActivo.HeaderText = "Color";
            this.colActivo.Name = "colActivo";
            this.colActivo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colActivo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colActivo.Width = 42;
            // 
            // frmGerarquizar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 782);
            this.Controls.Add(this.TabPrincipal);
            this.Controls.Add(this.explorerBar1);
            this.Name = "frmGerarquizar";
            this.Text = "Heterogeneidad";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmGerarquizar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.explorerBar1)).EndInit();
            this.explorerBar1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Controles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSumatoria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TabPrincipal)).EndInit();
            this.TabPrincipal.ResumeLayout(false);
            this.TabControlCriterio.ResumeLayout(false);
            this.TabControlResultado.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public DevComponents.DotNetBar.ExplorerBar explorerBar1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx1;
        private DevComponents.DotNetBar.ExplorerBarGroupItem explorerBarGroupItem1;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx2;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem2;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem3;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        public DevComponents.DotNetBar.Controls.DataGridViewX dgvSumatoria;
        private DevComponents.DotNetBar.LabelItem labelItem2;
        public DevComponents.DotNetBar.Controls.DataGridViewX dgv_Controles;
        private Gigasoft.ProEssentials.Pesgo Grafica_Estado_Mecanico;
        private DevComponents.DotNetBar.LabelItem labelItem6;
        private DevComponents.DotNetBar.LabelItem labelItem3;
        private DevComponents.DotNetBar.LabelItem labelItem4;
        private DevComponents.DotNetBar.ControlContainerItem _contenedor_cmdcargar;
        private System.Windows.Forms.Button cmdGraficar;
        private DevComponents.DotNetBar.LabelItem labelItem5;
        private DevComponents.DotNetBar.SuperTabControl TabPrincipal;
        private DevComponents.DotNetBar.SuperTabControlPanel TabControlCriterio;
        private DevComponents.DotNetBar.SuperTabItem TabCriterios;
        private DevComponents.DotNetBar.SuperTabControlPanel TabControlResultado;
        private DevComponents.DotNetBar.SuperTabItem TabResultado;
        private DevComponents.DotNetBar.CheckBoxItem chOrigenes;
        private DevComponents.DotNetBar.CheckBoxItem ch_sinCruz;
        private DevComponents.DotNetBar.CheckBoxItem ch_automatico;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColOpeTabla;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActivo;
    }
}