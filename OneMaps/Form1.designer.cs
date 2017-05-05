namespace Maps
{
    public partial class Form1
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dgvDistancia = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCiudades = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel_final = new DevComponents.DotNetBar.ExpandablePanel();
            this.explorerBar1 = new DevComponents.DotNetBar.ExplorerBar();
            this.dgvInfoGeneral = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ebgArchivo = new DevComponents.DotNetBar.ExplorerBarGroupItem();
            this.btnAbrir = new DevComponents.DotNetBar.ButtonItem();
            this.btnNuevo = new DevComponents.DotNetBar.ButtonItem();
            this.btnCargaMax = new DevComponents.DotNetBar.ButtonItem();
            this.btnGuardarTabla = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem7 = new DevComponents.DotNetBar.ButtonItem();
            this.explorerBarGroupItem1 = new DevComponents.DotNetBar.ExplorerBarGroupItem();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.btnCiudades = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem2 = new DevComponents.DotNetBar.LabelItem();
            this.controlContainerItem2 = new DevComponents.DotNetBar.ControlContainerItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.btnConfVecinos = new DevComponents.DotNetBar.ButtonItem();
            this.explorerBarGroupItem2 = new DevComponents.DotNetBar.ExplorerBarGroupItem();
            this.btnPseleccionado = new DevComponents.DotNetBar.ButtonItem();
            this.btnNuevoPunto = new DevComponents.DotNetBar.ButtonItem();
            this.btnTablaCirculos = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.explorerBarGroupItem3 = new DevComponents.DotNetBar.ExplorerBarGroupItem();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
            this.explorerBarGroupItem4 = new DevComponents.DotNetBar.ExplorerBarGroupItem();
            this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
            this.btnMarcas = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.btnAreas = new DevComponents.DotNetBar.ButtonItem();
            this.explorerBarGroupItem5 = new DevComponents.DotNetBar.ExplorerBarGroupItem();
            this.btnburbujas = new DevComponents.DotNetBar.ButtonItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.cbiAnotaciones = new DevComponents.Editors.ComboItem();
            this.cbiVecinos = new DevComponents.Editors.ComboItem();
            this.cbiAreas = new DevComponents.Editors.ComboItem();
            this.cbiNada = new DevComponents.Editors.ComboItem();
            this.button6 = new System.Windows.Forms.Button();
            this.Notificacion = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.bntPozo = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.btnLinea = new System.Windows.Forms.Button();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.btnPoligono = new System.Windows.Forms.Button();
            this.btnCargar = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistancia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCiudades)).BeginInit();
            this.panel_final.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.explorerBar1)).BeginInit();
            this.explorerBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfoGeneral)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(122, 55);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.dgvDistancia);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvCiudades);
            this.splitContainer1.Size = new System.Drawing.Size(570, 440);
            this.splitContainer1.SplitterDistance = 231;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Location = new System.Drawing.Point(10, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(115, 120);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Menu";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Medir Distancia";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Crea una marca";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 94);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(101, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "Tabla de Datos";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 67);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Eliminar Marcas";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dgvDistancia
            // 
            this.dgvDistancia.AllowUserToAddRows = false;
            this.dgvDistancia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDistancia.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dgvDistancia.Location = new System.Drawing.Point(4, 12);
            this.dgvDistancia.Name = "dgvDistancia";
            this.dgvDistancia.RowHeadersVisible = false;
            this.dgvDistancia.Size = new System.Drawing.Size(225, 70);
            this.dgvDistancia.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "PuntoA";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "PuntoB";
            this.Column2.Name = "Column2";
            // 
            // dgvCiudades
            // 
            this.dgvCiudades.AllowUserToAddRows = false;
            this.dgvCiudades.AllowUserToDeleteRows = false;
            this.dgvCiudades.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCiudades.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvCiudades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCiudades.ColumnHeadersVisible = false;
            this.dgvCiudades.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCiudades.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvCiudades.EnableHeadersVisualStyles = false;
            this.dgvCiudades.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvCiudades.Location = new System.Drawing.Point(47, 12);
            this.dgvCiudades.Name = "dgvCiudades";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCiudades.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvCiudades.RowHeadersVisible = false;
            this.dgvCiudades.Size = new System.Drawing.Size(240, 135);
            this.dgvCiudades.TabIndex = 70;
            this.dgvCiudades.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCiudades_CellContentClick);
            this.dgvCiudades.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvCiudades_EditingControlShowing);
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn13.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn14.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn7.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn8.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn9.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn10.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panel_final
            // 
            this.panel_final.AutoScroll = true;
            this.panel_final.CanvasColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel_final.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.RightToLeft;
            this.panel_final.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.panel_final.Controls.Add(this.explorerBar1);
            this.panel_final.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_final.ExpandButtonVisible = false;
            this.panel_final.ExpandOnTitleClick = true;
            this.panel_final.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_final.Location = new System.Drawing.Point(0, 0);
            this.panel_final.Name = "panel_final";
            this.panel_final.RightToLeftLayout = true;
            this.panel_final.Size = new System.Drawing.Size(258, 774);
            this.panel_final.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panel_final.Style.BackColor1.Color = System.Drawing.Color.Black;
            this.panel_final.Style.BackColor2.Color = System.Drawing.Color.Gainsboro;
            this.panel_final.Style.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Tile;
            this.panel_final.Style.BorderColor.Color = System.Drawing.Color.Black;
            this.panel_final.Style.ForeColor.Color = System.Drawing.Color.Black;
            this.panel_final.Style.GradientAngle = 90;
            this.panel_final.Style.WordWrap = true;
            this.panel_final.StyleMouseDown.BackColor1.Color = System.Drawing.Color.Gainsboro;
            this.panel_final.StyleMouseDown.BackColor2.Color = System.Drawing.Color.Gainsboro;
            this.panel_final.StyleMouseDown.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panel_final.StyleMouseOver.BackColor1.Color = System.Drawing.Color.Gainsboro;
            this.panel_final.StyleMouseOver.BackColor2.Color = System.Drawing.Color.Gainsboro;
            this.panel_final.TabIndex = 68;
            this.panel_final.TitleStyle.BackColor1.Color = System.Drawing.Color.DarkGray;
            this.panel_final.TitleStyle.BackColor2.Color = System.Drawing.Color.DarkGray;
            this.panel_final.TitleStyle.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Center;
            this.panel_final.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.panel_final.TitleStyle.BorderColor.Color = System.Drawing.Color.Gray;
            this.panel_final.TitleStyle.ForeColor.Color = System.Drawing.Color.Black;
            this.panel_final.TitleStyle.GradientAngle = 90;
            this.panel_final.TitleStyle.MarginLeft = 6;
            this.panel_final.TitleStyleMouseDown.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel_final.TitleStyleMouseDown.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel_final.TitleStyleMouseDown.ForeColor.Color = System.Drawing.Color.Black;
            this.panel_final.TitleStyleMouseOver.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel_final.TitleStyleMouseOver.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel_final.TitleStyleMouseOver.ForeColor.Color = System.Drawing.Color.Black;
            this.panel_final.TitleStyleMouseOver.UseMnemonic = false;
            this.panel_final.TitleText = "                                  Menú";
            // 
            // explorerBar1
            // 
            this.explorerBar1.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.explorerBar1.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.explorerBar1.BackStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(200)))), ((int)(((byte)(212)))));
            this.explorerBar1.BackStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.explorerBar1.BackStyle.BackColorGradientAngle = 90;
            this.explorerBar1.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBar1.ColorScheme.BarBackground = System.Drawing.SystemColors.Info;
            this.explorerBar1.ColorScheme.DockSiteBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.explorerBar1.ColorScheme.DockSiteBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.explorerBar1.ContentMargin = 1;
            this.explorerBar1.Controls.Add(this.dgvInfoGeneral);
            this.explorerBar1.Cursor = System.Windows.Forms.Cursors.Default;
            this.explorerBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.explorerBar1.GroupImages = null;
            this.explorerBar1.Groups.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ebgArchivo,
            this.explorerBarGroupItem1,
            this.explorerBarGroupItem2,
            this.explorerBarGroupItem3,
            this.explorerBarGroupItem4,
            this.explorerBarGroupItem5});
            this.explorerBar1.GroupSpacing = 1;
            this.explorerBar1.Images = null;
            this.explorerBar1.Location = new System.Drawing.Point(0, 26);
            this.explorerBar1.Name = "explorerBar1";
            this.explorerBar1.Size = new System.Drawing.Size(258, 748);
            this.explorerBar1.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.Silver;
            this.explorerBar1.TabIndex = 1;
            this.explorerBar1.Text = "Fracciones";
            this.explorerBar1.ThemeAware = true;
            // 
            // dgvInfoGeneral
            // 
            this.dgvInfoGeneral.AllowUserToAddRows = false;
            this.dgvInfoGeneral.AllowUserToDeleteRows = false;
            this.dgvInfoGeneral.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInfoGeneral.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvInfoGeneral.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInfoGeneral.ColumnHeadersVisible = false;
            this.dgvInfoGeneral.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvInfoGeneral.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvInfoGeneral.EnableHeadersVisualStyles = false;
            this.dgvInfoGeneral.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvInfoGeneral.Location = new System.Drawing.Point(1, 191);
            this.dgvInfoGeneral.Name = "dgvInfoGeneral";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInfoGeneral.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvInfoGeneral.RowHeadersVisible = false;
            this.dgvInfoGeneral.Size = new System.Drawing.Size(240, 487);
            this.dgvInfoGeneral.TabIndex = 69;
            this.dgvInfoGeneral.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvInfoGeneral_EditingControlShowing);
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn11.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn12.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ebgArchivo
            // 
            // 
            // 
            // 
            this.ebgArchivo.BackStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(245)))));
            this.ebgArchivo.BackStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ebgArchivo.BackStyle.BorderBottomWidth = 1;
            this.ebgArchivo.BackStyle.BorderColor = System.Drawing.Color.White;
            this.ebgArchivo.BackStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ebgArchivo.BackStyle.BorderLeftWidth = 1;
            this.ebgArchivo.BackStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ebgArchivo.BackStyle.BorderRightWidth = 1;
            this.ebgArchivo.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ebgArchivo.Cursor = System.Windows.Forms.Cursors.Default;
            this.ebgArchivo.Expanded = true;
            this.ebgArchivo.Name = "ebgArchivo";
            this.ebgArchivo.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.Silver;
            this.ebgArchivo.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnAbrir,
            this.btnNuevo,
            this.btnCargaMax,
            this.btnGuardarTabla,
            this.buttonItem7});
            this.ebgArchivo.Text = "Archivo";
            // 
            // 
            // 
            this.ebgArchivo.TitleHotStyle.BackColor = System.Drawing.Color.White;
            this.ebgArchivo.TitleHotStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(215)))), ((int)(((byte)(224)))));
            this.ebgArchivo.TitleHotStyle.CornerDiameter = 3;
            this.ebgArchivo.TitleHotStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ebgArchivo.TitleHotStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.ebgArchivo.TitleHotStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.ebgArchivo.TitleHotStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            // 
            // 
            // 
            this.ebgArchivo.TitleStyle.BackColor = System.Drawing.Color.White;
            this.ebgArchivo.TitleStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(215)))), ((int)(((byte)(224)))));
            this.ebgArchivo.TitleStyle.CornerDiameter = 3;
            this.ebgArchivo.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ebgArchivo.TitleStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.ebgArchivo.TitleStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.ebgArchivo.TitleStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            // 
            // btnAbrir
            // 
            this.btnAbrir.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAbrir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbrir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.btnAbrir.HotFontUnderline = true;
            this.btnAbrir.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.btnAbrir.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Text = "      Abrir";
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.btnNuevo.HotFontUnderline = true;
            this.btnNuevo.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.btnNuevo.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Text = "      Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnCargaMax
            // 
            this.btnCargaMax.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnCargaMax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCargaMax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.btnCargaMax.HotFontUnderline = true;
            this.btnCargaMax.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.btnCargaMax.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.btnCargaMax.Name = "btnCargaMax";
            this.btnCargaMax.Text = "      Cargar Datos";
            this.btnCargaMax.Click += new System.EventHandler(this.btnCargaMax_Click);
            // 
            // btnGuardarTabla
            // 
            this.btnGuardarTabla.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnGuardarTabla.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarTabla.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.btnGuardarTabla.HotFontUnderline = true;
            this.btnGuardarTabla.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.btnGuardarTabla.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.btnGuardarTabla.Name = "btnGuardarTabla";
            this.btnGuardarTabla.Text = "      Guardar";
            this.btnGuardarTabla.Click += new System.EventHandler(this.btnGuardarTabla_Click);
            // 
            // buttonItem7
            // 
            this.buttonItem7.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.buttonItem7.HotFontUnderline = true;
            this.buttonItem7.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.buttonItem7.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem7.Name = "buttonItem7";
            this.buttonItem7.Text = "New Button";
            this.buttonItem7.Click += new System.EventHandler(this.buttonItem7_Click);
            // 
            // explorerBarGroupItem1
            // 
            // 
            // 
            // 
            this.explorerBarGroupItem1.BackStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(245)))));
            this.explorerBarGroupItem1.BackStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem1.BackStyle.BorderBottomWidth = 1;
            this.explorerBarGroupItem1.BackStyle.BorderColor = System.Drawing.Color.White;
            this.explorerBarGroupItem1.BackStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem1.BackStyle.BorderLeftWidth = 1;
            this.explorerBarGroupItem1.BackStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem1.BackStyle.BorderRightWidth = 1;
            this.explorerBarGroupItem1.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem1.Cursor = System.Windows.Forms.Cursors.Default;
            this.explorerBarGroupItem1.Name = "explorerBarGroupItem1";
            this.explorerBarGroupItem1.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.Silver;
            this.explorerBarGroupItem1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem1,
            this.btnCiudades,
            this.labelItem2,
            this.controlContainerItem2,
            this.buttonItem2,
            this.btnConfVecinos});
            this.explorerBarGroupItem1.Text = "Información de localización";
            // 
            // 
            // 
            this.explorerBarGroupItem1.TitleHotStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem1.TitleHotStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(215)))), ((int)(((byte)(224)))));
            this.explorerBarGroupItem1.TitleHotStyle.CornerDiameter = 3;
            this.explorerBarGroupItem1.TitleHotStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem1.TitleHotStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem1.TitleHotStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem1.TitleHotStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            // 
            // 
            // 
            this.explorerBarGroupItem1.TitleStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem1.TitleStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(215)))), ((int)(((byte)(224)))));
            this.explorerBarGroupItem1.TitleStyle.CornerDiameter = 3;
            this.explorerBarGroupItem1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem1.TitleStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem1.TitleStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem1.TitleStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            // 
            // labelItem1
            // 
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = " ";
            this.labelItem1.ThemeAware = true;
            // 
            // btnCiudades
            // 
            this.btnCiudades.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnCiudades.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCiudades.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.btnCiudades.HotFontUnderline = true;
            this.btnCiudades.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.btnCiudades.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.btnCiudades.Name = "btnCiudades";
            this.btnCiudades.Text = "Busqueda Ciudades";
            this.btnCiudades.Click += new System.EventHandler(this.btnCiudades_Click);
            // 
            // labelItem2
            // 
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.Text = " ";
            this.labelItem2.ThemeAware = true;
            // 
            // controlContainerItem2
            // 
            this.controlContainerItem2.AllowItemResize = false;
            this.controlContainerItem2.Control = this.dgvInfoGeneral;
            this.controlContainerItem2.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem2.Name = "controlContainerItem2";
            this.controlContainerItem2.ThemeAware = true;
            // 
            // buttonItem2
            // 
            this.buttonItem2.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.buttonItem2.HotFontUnderline = true;
            this.buttonItem2.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.buttonItem2.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = "       Pozos Vecinos";
            this.buttonItem2.Click += new System.EventHandler(this.buttonItem2_Click);
            // 
            // btnConfVecinos
            // 
            this.btnConfVecinos.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnConfVecinos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfVecinos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.btnConfVecinos.HotFontUnderline = true;
            this.btnConfVecinos.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.btnConfVecinos.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.btnConfVecinos.Name = "btnConfVecinos";
            this.btnConfVecinos.Text = "       Configuración";
            this.btnConfVecinos.Click += new System.EventHandler(this.buttonItem3_Click_2);
            // 
            // explorerBarGroupItem2
            // 
            // 
            // 
            // 
            this.explorerBarGroupItem2.BackStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(245)))));
            this.explorerBarGroupItem2.BackStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem2.BackStyle.BorderBottomWidth = 1;
            this.explorerBarGroupItem2.BackStyle.BorderColor = System.Drawing.Color.White;
            this.explorerBarGroupItem2.BackStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem2.BackStyle.BorderLeftWidth = 1;
            this.explorerBarGroupItem2.BackStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem2.BackStyle.BorderRightWidth = 1;
            this.explorerBarGroupItem2.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem2.Cursor = System.Windows.Forms.Cursors.Default;
            this.explorerBarGroupItem2.Expanded = true;
            this.explorerBarGroupItem2.Name = "explorerBarGroupItem2";
            this.explorerBarGroupItem2.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.Silver;
            this.explorerBarGroupItem2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnPseleccionado,
            this.btnNuevoPunto,
            this.btnTablaCirculos,
            this.buttonItem1});
            this.explorerBarGroupItem2.Text = "Areas";
            // 
            // 
            // 
            this.explorerBarGroupItem2.TitleHotStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem2.TitleHotStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(215)))), ((int)(((byte)(224)))));
            this.explorerBarGroupItem2.TitleHotStyle.CornerDiameter = 3;
            this.explorerBarGroupItem2.TitleHotStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem2.TitleHotStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem2.TitleHotStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem2.TitleHotStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            // 
            // 
            // 
            this.explorerBarGroupItem2.TitleStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem2.TitleStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(215)))), ((int)(((byte)(224)))));
            this.explorerBarGroupItem2.TitleStyle.CornerDiameter = 3;
            this.explorerBarGroupItem2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem2.TitleStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem2.TitleStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem2.TitleStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            // 
            // btnPseleccionado
            // 
            this.btnPseleccionado.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnPseleccionado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPseleccionado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.btnPseleccionado.HotFontUnderline = true;
            this.btnPseleccionado.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.btnPseleccionado.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.btnPseleccionado.Name = "btnPseleccionado";
            this.btnPseleccionado.Text = "       Pozo seleccionado";
            this.btnPseleccionado.Click += new System.EventHandler(this.btnPseleccionado_Click);
            // 
            // btnNuevoPunto
            // 
            this.btnNuevoPunto.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnNuevoPunto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoPunto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.btnNuevoPunto.HotFontUnderline = true;
            this.btnNuevoPunto.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.btnNuevoPunto.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.btnNuevoPunto.Name = "btnNuevoPunto";
            this.btnNuevoPunto.Text = "       Nuevo Punto";
            this.btnNuevoPunto.Click += new System.EventHandler(this.btnPseleccionado_Click);
            // 
            // btnTablaCirculos
            // 
            this.btnTablaCirculos.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnTablaCirculos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTablaCirculos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.btnTablaCirculos.HotFontUnderline = true;
            this.btnTablaCirculos.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.btnTablaCirculos.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.btnTablaCirculos.Name = "btnTablaCirculos";
            this.btnTablaCirculos.Text = "       Tabla Areas";
            this.btnTablaCirculos.Click += new System.EventHandler(this.btnTablaCirculos_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.buttonItem1.HotFontUnderline = true;
            this.buttonItem1.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.buttonItem1.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "       Distancia";
            this.buttonItem1.Click += new System.EventHandler(this.button2_Click);
            // 
            // explorerBarGroupItem3
            // 
            // 
            // 
            // 
            this.explorerBarGroupItem3.BackStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(245)))));
            this.explorerBarGroupItem3.BackStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem3.BackStyle.BorderBottomWidth = 1;
            this.explorerBarGroupItem3.BackStyle.BorderColor = System.Drawing.Color.White;
            this.explorerBarGroupItem3.BackStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem3.BackStyle.BorderLeftWidth = 1;
            this.explorerBarGroupItem3.BackStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem3.BackStyle.BorderRightWidth = 1;
            this.explorerBarGroupItem3.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem3.Cursor = System.Windows.Forms.Cursors.Default;
            this.explorerBarGroupItem3.Expanded = true;
            this.explorerBarGroupItem3.Name = "explorerBarGroupItem3";
            this.explorerBarGroupItem3.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.Silver;
            this.explorerBarGroupItem3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem4,
            this.buttonItem6});
            this.explorerBarGroupItem3.Text = "Opciones Mapa";
            // 
            // 
            // 
            this.explorerBarGroupItem3.TitleHotStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem3.TitleHotStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(215)))), ((int)(((byte)(224)))));
            this.explorerBarGroupItem3.TitleHotStyle.CornerDiameter = 3;
            this.explorerBarGroupItem3.TitleHotStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem3.TitleHotStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem3.TitleHotStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem3.TitleHotStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            // 
            // 
            // 
            this.explorerBarGroupItem3.TitleStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem3.TitleStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(215)))), ((int)(((byte)(224)))));
            this.explorerBarGroupItem3.TitleStyle.CornerDiameter = 3;
            this.explorerBarGroupItem3.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem3.TitleStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem3.TitleStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem3.TitleStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            // 
            // buttonItem4
            // 
            this.buttonItem4.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.buttonItem4.HotFontUnderline = true;
            this.buttonItem4.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.buttonItem4.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.Text = "       Capturar pantalla";
            this.buttonItem4.Click += new System.EventHandler(this.buttonItem4_Click_1);
            // 
            // buttonItem6
            // 
            this.buttonItem6.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.buttonItem6.HotFontUnderline = true;
            this.buttonItem6.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.buttonItem6.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem6.Name = "buttonItem6";
            this.buttonItem6.Text = "       Etiquetas";
            this.buttonItem6.Click += new System.EventHandler(this.buttonItem6_Click);
            // 
            // explorerBarGroupItem4
            // 
            // 
            // 
            // 
            this.explorerBarGroupItem4.BackStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(245)))));
            this.explorerBarGroupItem4.BackStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem4.BackStyle.BorderBottomWidth = 1;
            this.explorerBarGroupItem4.BackStyle.BorderColor = System.Drawing.Color.White;
            this.explorerBarGroupItem4.BackStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem4.BackStyle.BorderLeftWidth = 1;
            this.explorerBarGroupItem4.BackStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem4.BackStyle.BorderRightWidth = 1;
            this.explorerBarGroupItem4.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem4.Cursor = System.Windows.Forms.Cursors.Default;
            this.explorerBarGroupItem4.Expanded = true;
            this.explorerBarGroupItem4.Name = "explorerBarGroupItem4";
            this.explorerBarGroupItem4.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.Silver;
            this.explorerBarGroupItem4.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem5,
            this.btnMarcas,
            this.buttonItem3,
            this.btnAreas});
            this.explorerBarGroupItem4.Text = "Anotaciones";
            // 
            // 
            // 
            this.explorerBarGroupItem4.TitleHotStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem4.TitleHotStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(215)))), ((int)(((byte)(224)))));
            this.explorerBarGroupItem4.TitleHotStyle.CornerDiameter = 3;
            this.explorerBarGroupItem4.TitleHotStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem4.TitleHotStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem4.TitleHotStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem4.TitleHotStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            // 
            // 
            // 
            this.explorerBarGroupItem4.TitleStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem4.TitleStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(215)))), ((int)(((byte)(224)))));
            this.explorerBarGroupItem4.TitleStyle.CornerDiameter = 3;
            this.explorerBarGroupItem4.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem4.TitleStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem4.TitleStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem4.TitleStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            // 
            // buttonItem5
            // 
            this.buttonItem5.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.buttonItem5.HotFontUnderline = true;
            this.buttonItem5.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.buttonItem5.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem5.Name = "buttonItem5";
            this.buttonItem5.Text = "       Cargar";
            this.buttonItem5.Click += new System.EventHandler(this.buttonItem5_Click);
            // 
            // btnMarcas
            // 
            this.btnMarcas.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnMarcas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMarcas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.btnMarcas.HotFontUnderline = true;
            this.btnMarcas.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.btnMarcas.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.btnMarcas.Name = "btnMarcas";
            this.btnMarcas.Text = "       Marcas";
            this.btnMarcas.Click += new System.EventHandler(this.btnMarcas_Click);
            // 
            // buttonItem3
            // 
            this.buttonItem3.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.buttonItem3.HotFontUnderline = true;
            this.buttonItem3.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.buttonItem3.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Text = "       Calcular Area";
            this.buttonItem3.Click += new System.EventHandler(this.buttonItem3_Click_3);
            // 
            // btnAreas
            // 
            this.btnAreas.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAreas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAreas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.btnAreas.HotFontUnderline = true;
            this.btnAreas.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.btnAreas.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.btnAreas.Name = "btnAreas";
            this.btnAreas.Text = "       Areas";
            this.btnAreas.Click += new System.EventHandler(this.btnAreas_Click);
            // 
            // explorerBarGroupItem5
            // 
            // 
            // 
            // 
            this.explorerBarGroupItem5.BackStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(241)))), ((int)(((byte)(245)))));
            this.explorerBarGroupItem5.BackStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem5.BackStyle.BorderBottomWidth = 1;
            this.explorerBarGroupItem5.BackStyle.BorderColor = System.Drawing.Color.White;
            this.explorerBarGroupItem5.BackStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem5.BackStyle.BorderLeftWidth = 1;
            this.explorerBarGroupItem5.BackStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem5.BackStyle.BorderRightWidth = 1;
            this.explorerBarGroupItem5.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem5.Cursor = System.Windows.Forms.Cursors.Default;
            this.explorerBarGroupItem5.Expanded = true;
            this.explorerBarGroupItem5.Name = "explorerBarGroupItem5";
            this.explorerBarGroupItem5.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.Silver;
            this.explorerBarGroupItem5.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnburbujas});
            this.explorerBarGroupItem5.Text = "Burbujas";
            // 
            // 
            // 
            this.explorerBarGroupItem5.TitleHotStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem5.TitleHotStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(215)))), ((int)(((byte)(224)))));
            this.explorerBarGroupItem5.TitleHotStyle.CornerDiameter = 3;
            this.explorerBarGroupItem5.TitleHotStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem5.TitleHotStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem5.TitleHotStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem5.TitleHotStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            // 
            // 
            // 
            this.explorerBarGroupItem5.TitleStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem5.TitleStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(215)))), ((int)(((byte)(224)))));
            this.explorerBarGroupItem5.TitleStyle.CornerDiameter = 3;
            this.explorerBarGroupItem5.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem5.TitleStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem5.TitleStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem5.TitleStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            // 
            // btnburbujas
            // 
            this.btnburbujas.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnburbujas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnburbujas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.btnburbujas.HotFontUnderline = true;
            this.btnburbujas.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.btnburbujas.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.btnburbujas.Name = "btnburbujas";
            this.btnburbujas.Text = "       Cargar";
            this.btnburbujas.Click += new System.EventHandler(this.btnburbujas_Click_1);
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "1 KM";
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "5 KMS";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "10 KMS";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "15 KMS";
            // 
            // cbiAnotaciones
            // 
            this.cbiAnotaciones.Text = "Anotaciones";
            // 
            // cbiVecinos
            // 
            this.cbiVecinos.Text = "Pozos Vecinos";
            // 
            // cbiAreas
            // 
            this.cbiAreas.Text = "Areas";
            // 
            // cbiNada
            // 
            this.cbiNada.Text = "Ocultar";
            // 
            // button6
            // 
            this.button6.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button6.Location = new System.Drawing.Point(255, 0);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(116, 28);
            this.button6.TabIndex = 69;
            this.button6.Text = "  Datos";
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Notificacion
            // 
            this.Notificacion.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.Notificacion.BalloonTipText = "Espera se esta dibujando: ";
            this.Notificacion.BalloonTipTitle = "Espere";
            this.Notificacion.Icon = ((System.Drawing.Icon)(resources.GetObject("Notificacion.Icon")));
            this.Notificacion.Text = "Espere";
            this.Notificacion.Visible = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(258, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(633, 774);
            this.tabControl1.TabIndex = 70;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button9);
            this.tabPage1.Controls.Add(this.button8);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.button5);
            this.tabPage1.Controls.Add(this.trackBar1);
            this.tabPage1.Controls.Add(this.bntPozo);
            this.tabPage1.Controls.Add(this.button7);
            this.tabPage1.Controls.Add(this.btnLinea);
            this.tabPage1.Controls.Add(this.gMapControl1);
            this.tabPage1.Controls.Add(this.btnPoligono);
            this.tabPage1.Controls.Add(this.btnCargar);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(625, 748);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button9.Location = new System.Drawing.Point(547, 232);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 11;
            this.button9.Text = "Actualiza Circulos";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.Location = new System.Drawing.Point(547, 203);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 10;
            this.button8.Text = "Buscar";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(548, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(71, 20);
            this.textBox1.TabIndex = 9;
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(547, 174);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 8;
            this.button5.Text = "Pozos";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.Location = new System.Drawing.Point(572, 502);
            this.trackBar1.Maximum = 24;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 240);
            this.trackBar1.TabIndex = 7;
            this.trackBar1.Value = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // bntPozo
            // 
            this.bntPozo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bntPozo.Location = new System.Drawing.Point(547, 145);
            this.bntPozo.Name = "bntPozo";
            this.bntPozo.Size = new System.Drawing.Size(75, 23);
            this.bntPozo.TabIndex = 6;
            this.bntPozo.Text = "Pozos";
            this.bntPozo.UseVisualStyleBackColor = true;
            this.bntPozo.Click += new System.EventHandler(this.bntPozo_Click);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.Location = new System.Drawing.Point(547, 116);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 5;
            this.button7.Text = "Punto";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // btnLinea
            // 
            this.btnLinea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLinea.Location = new System.Drawing.Point(544, 87);
            this.btnLinea.Name = "btnLinea";
            this.btnLinea.Size = new System.Drawing.Size(75, 23);
            this.btnLinea.TabIndex = 4;
            this.btnLinea.Text = "Linea";
            this.btnLinea.UseVisualStyleBackColor = true;
            this.btnLinea.Click += new System.EventHandler(this.btnLinea_Click);
            // 
            // gMapControl1
            // 
            this.gMapControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemmory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(3, 3);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(538, 742);
            this.gMapControl1.TabIndex = 0;
            this.gMapControl1.Zoom = 0D;
            this.gMapControl1.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gMapControl1_OnMarkerClick);
            this.gMapControl1.OnMapZoomChanged += new GMap.NET.MapZoomChanged(this.gMapControl1_OnMapZoomChanged);
            this.gMapControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gMapControl1_MouseClick);
            // 
            // btnPoligono
            // 
            this.btnPoligono.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPoligono.Location = new System.Drawing.Point(544, 58);
            this.btnPoligono.Name = "btnPoligono";
            this.btnPoligono.Size = new System.Drawing.Size(75, 23);
            this.btnPoligono.TabIndex = 3;
            this.btnPoligono.Text = "Poligono";
            this.btnPoligono.UseVisualStyleBackColor = true;
            this.btnPoligono.Click += new System.EventHandler(this.btnPoligono_Click);
            // 
            // btnCargar
            // 
            this.btnCargar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCargar.Location = new System.Drawing.Point(544, 3);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(75, 23);
            this.btnCargar.TabIndex = 1;
            this.btnCargar.Text = "Cargar";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(625, 748);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(619, 742);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(625, 748);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 774);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.panel_final);
            this.Name = "Form1";
            this.Text = "Mapas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistancia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCiudades)).EndInit();
            this.panel_final.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.explorerBar1)).EndInit();
            this.explorerBar1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfoGeneral)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgvDistancia;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DevComponents.DotNetBar.ExpandablePanel panel_final;
        private DevComponents.DotNetBar.ExplorerBar explorerBar1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvInfoGeneral;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DevComponents.DotNetBar.ExplorerBarGroupItem ebgArchivo;
        private DevComponents.DotNetBar.ButtonItem btnNuevo;
        private DevComponents.DotNetBar.ButtonItem btnCargaMax;
        private DevComponents.DotNetBar.ExplorerBarGroupItem explorerBarGroupItem1;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem2;
        private DevComponents.DotNetBar.ExplorerBarGroupItem explorerBarGroupItem2;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private System.Windows.Forms.Button button6;
        private DevComponents.DotNetBar.ExplorerBarGroupItem explorerBarGroupItem3;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.Editors.ComboItem cbiAnotaciones;
        private DevComponents.Editors.ComboItem cbiVecinos;
        private DevComponents.Editors.ComboItem cbiAreas;
        private DevComponents.Editors.ComboItem cbiNada;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem btnTablaCirculos;
        private System.Windows.Forms.NotifyIcon Notificacion;
        private DevComponents.DotNetBar.ButtonItem btnPseleccionado;
        private DevComponents.DotNetBar.ButtonItem btnNuevoPunto;
        private DevComponents.DotNetBar.ExplorerBarGroupItem explorerBarGroupItem4;
        private DevComponents.DotNetBar.ButtonItem buttonItem5;
        private DevComponents.DotNetBar.ExplorerBarGroupItem explorerBarGroupItem5;
        private DevComponents.DotNetBar.ButtonItem btnConfVecinos;
        private DevComponents.DotNetBar.ButtonItem btnCiudades;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvCiudades;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private DevComponents.DotNetBar.ButtonItem btnMarcas;
        private DevComponents.DotNetBar.ButtonItem btnGuardarTabla;
        private DevComponents.DotNetBar.ButtonItem btnburbujas;
        private DevComponents.DotNetBar.ButtonItem btnAbrir;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private DevComponents.DotNetBar.ButtonItem btnAreas;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.LabelItem labelItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
        private DevComponents.DotNetBar.ButtonItem buttonItem6;
        private DevComponents.DotNetBar.ButtonItem buttonItem7;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button bntPozo;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button btnLinea;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private System.Windows.Forms.Button btnPoligono;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
    }
}

