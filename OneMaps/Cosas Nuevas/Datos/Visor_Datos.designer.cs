namespace Maps
{
    partial class Visor_Datos
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
            this.dgv_tabla_grafica = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Barra_Menu = new DevComponents.DotNetBar.RibbonBar();
            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
            this.cmdAddRows = new DevComponents.DotNetBar.ButtonItem();
            this.cmdDelRow = new DevComponents.DotNetBar.ButtonItem();
            this.cmdRowDel = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer12 = new DevComponents.DotNetBar.ItemContainer();
            this.buttonPaste = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer14 = new DevComponents.DotNetBar.ItemContainer();
            this.buttonItem47 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem67 = new DevComponents.DotNetBar.ButtonItem();
            this.cmd_Filtrar = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer2 = new DevComponents.DotNetBar.ItemContainer();
            this.textBox1 = new DevComponents.DotNetBar.TextBoxItem();
            this.cmdSearch = new DevComponents.DotNetBar.ButtonItem();
            this.cmdVerOcultar = new DevComponents.DotNetBar.ButtonItem();
            this.cmdReemplazarPorColumna = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tabla_grafica)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_tabla_grafica
            // 
            this.dgv_tabla_grafica.BackgroundColor = System.Drawing.Color.White;
            this.dgv_tabla_grafica.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_tabla_grafica.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_tabla_grafica.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_tabla_grafica.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_tabla_grafica.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_tabla_grafica.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(229)))), ((int)(((byte)(239)))));
            this.dgv_tabla_grafica.Location = new System.Drawing.Point(0, 54);
            this.dgv_tabla_grafica.Name = "dgv_tabla_grafica";
            this.dgv_tabla_grafica.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_tabla_grafica.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_tabla_grafica.Size = new System.Drawing.Size(1151, 375);
            this.dgv_tabla_grafica.TabIndex = 11;
            this.dgv_tabla_grafica.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_tabla_grafica_CellClick);
            // 
            // Barra_Menu
            // 
            this.Barra_Menu.AutoOverflowEnabled = false;
            this.Barra_Menu.AutoScroll = true;
            // 
            // 
            // 
            this.Barra_Menu.BackgroundMouseOverStyle.Class = "";
            this.Barra_Menu.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.Barra_Menu.BackgroundStyle.Class = "";
            this.Barra_Menu.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Barra_Menu.CanCustomize = false;
            this.Barra_Menu.ContainerControlProcessDialogKey = true;
            this.Barra_Menu.Dock = System.Windows.Forms.DockStyle.Top;
            this.Barra_Menu.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer1,
            this.itemContainer12,
            this.itemContainer14,
            this.itemContainer2,
            this.cmdVerOcultar,
            this.cmdReemplazarPorColumna,
            this.buttonItem6});
            this.Barra_Menu.ItemSpacing = 20;
            this.Barra_Menu.Location = new System.Drawing.Point(0, 0);
            this.Barra_Menu.Name = "Barra_Menu";
            this.Barra_Menu.Size = new System.Drawing.Size(1151, 54);
            this.Barra_Menu.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Barra_Menu.TabIndex = 12;
            // 
            // 
            // 
            this.Barra_Menu.TitleStyle.Class = "";
            this.Barra_Menu.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.Barra_Menu.TitleStyleMouseOver.Class = "";
            this.Barra_Menu.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Barra_Menu.TitleVisible = false;
            // 
            // itemContainer1
            // 
            // 
            // 
            // 
            this.itemContainer1.BackgroundStyle.Class = "";
            this.itemContainer1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.cmdAddRows,
            this.cmdDelRow,
            this.cmdRowDel});
            // 
            // cmdAddRows
            // 
            //this.cmdAddRows.Image = global::Mapas.Properties.Resources.Icono_3;
            this.cmdAddRows.Name = "cmdAddRows";
            this.cmdAddRows.Text = "cmdFilaadd";
            this.cmdAddRows.Tooltip = "Agregar Fila";
            this.cmdAddRows.Click += new System.EventHandler(this.cmdAddRows_Click);
            // 
            // cmdDelRow
            // 
            this.cmdDelRow.Image = global::Maps.Properties.Resources.eliminar_fila;
            this.cmdDelRow.Name = "cmdDelRow";
            this.cmdDelRow.Text = "cmdFilaDelete";
            this.cmdDelRow.Tooltip = "Borrar reglones";
            this.cmdDelRow.Click += new System.EventHandler(this.cmdDelRow_Click);
            // 
            // cmdRowDel
            // 
            this.cmdRowDel.Image = global::Maps.Properties.Resources.borrar_tabla;
            this.cmdRowDel.Name = "cmdRowDel";
            this.cmdRowDel.Text = "cmdFilaDelete";
            this.cmdRowDel.Tooltip = "Borrar Tabla";
            this.cmdRowDel.Click += new System.EventHandler(this.cmdRowDel_Click);
            // 
            // itemContainer12
            // 
            // 
            // 
            // 
            this.itemContainer12.BackgroundStyle.Class = "";
            this.itemContainer12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer12.Name = "itemContainer12";
            this.itemContainer12.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonPaste});
            // 
            // buttonPaste
            // 
            this.buttonPaste.Image = global::Maps.Properties.Resources.Pegar;
            this.buttonPaste.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonPaste.Name = "buttonPaste";
            this.buttonPaste.SplitButton = true;
            this.buttonPaste.Tooltip = "Pegar del Portapapeles";
            // 
            // itemContainer14
            // 
            // 
            // 
            // 
            this.itemContainer14.BackgroundStyle.Class = "";
            this.itemContainer14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer14.Name = "itemContainer14";
            this.itemContainer14.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem47,
            this.buttonItem67,
            this.cmd_Filtrar});
            // 
            // buttonItem47
            // 
            this.buttonItem47.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem47.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem47.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem47.Image = global::Maps.Properties.Resources.Busqueda_avanzada;
            this.buttonItem47.Name = "buttonItem47";
            this.buttonItem47.Tooltip = "Revision Pdf, Visualiza la informacion del renglos de donde viene su origen, en l" +
    "as hojas del Pdf";
            this.buttonItem47.Click += new System.EventHandler(this.buttonItem47_Click);
            // 
            // buttonItem67
            // 
            this.buttonItem67.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem67.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem67.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            //this.buttonItem67.Image = global::Mapas.Properties.Resources.clasificar;
            this.buttonItem67.Name = "buttonItem67";
            this.buttonItem67.Tooltip = "Clasificar Informacion";
            this.buttonItem67.Click += new System.EventHandler(this.buttonItem67_Click);
            // 
            // cmd_Filtrar
            // 
            this.cmd_Filtrar.AutoCheckOnClick = true;
            this.cmd_Filtrar.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.cmd_Filtrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.cmd_Filtrar.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.cmd_Filtrar.Image = global::Maps.Properties.Resources.Filtrar_archivo;
            this.cmd_Filtrar.Name = "cmd_Filtrar";
            this.cmd_Filtrar.Tooltip = "Filtrado, filtrado por columnas";
            this.cmd_Filtrar.Click += new System.EventHandler(this.cmd_Filtrar_Click);
            // 
            // itemContainer2
            // 
            // 
            // 
            // 
            this.itemContainer2.BackgroundStyle.Class = "";
            this.itemContainer2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer2.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
            this.itemContainer2.Name = "itemContainer2";
            this.itemContainer2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.textBox1,
            this.cmdSearch});
            // 
            // textBox1
            // 
            this.textBox1.Name = "textBox1";
            this.textBox1.TextBoxWidth = 100;
            this.textBox1.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // cmdSearch
            // 
            this.cmdSearch.Image = global::Maps.Properties.Resources.buscar_info;
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Text = "Buscar";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // cmdVerOcultar
            // 
            this.cmdVerOcultar.Image = global::Maps.Properties.Resources.Visualizar_tabla;
            this.cmdVerOcultar.Name = "cmdVerOcultar";
            this.cmdVerOcultar.SubItemsExpandWidth = 14;
            this.cmdVerOcultar.Text = "Mostrar/Ocultar Columnas";
            this.cmdVerOcultar.Tooltip = "Mostrar/Ocultar Columnas";
            this.cmdVerOcultar.Click += new System.EventHandler(this.cmdVerOcultar_Click);
            // 
            // cmdReemplazarPorColumna
            // 
            this.cmdReemplazarPorColumna.Image = global::Maps.Properties.Resources.Reemplazar;
            this.cmdReemplazarPorColumna.Name = "cmdReemplazarPorColumna";
            this.cmdReemplazarPorColumna.SubItemsExpandWidth = 14;
            this.cmdReemplazarPorColumna.Text = "Reemplazar";
            this.cmdReemplazarPorColumna.Tooltip = "Tabla de Reemplazo";
            this.cmdReemplazarPorColumna.Click += new System.EventHandler(this.cmdReemplazarPorColumna_Click);
            // 
            // buttonItem6
            // 
            this.buttonItem6.Image = global::Maps.Properties.Resources.deshacer;
            this.buttonItem6.Name = "buttonItem6";
            this.buttonItem6.SubItemsExpandWidth = 14;
            this.buttonItem6.Text = "Relleno";
            this.buttonItem6.Tooltip = "Ventana de relleno";
            this.buttonItem6.Click += new System.EventHandler(this.buttonItem6_Click);
            // 
            // Visor_Datos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1151, 429);
            this.Controls.Add(this.dgv_tabla_grafica);
            this.Controls.Add(this.Barra_Menu);
            this.KeyPreview = true;
            this.Name = "Visor_Datos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Visor_Datos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Visor_Datos_FormClosing);
            this.Load += new System.EventHandler(this.Visor_Datos_Load);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Visor_Datos_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tabla_grafica)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgv_tabla_grafica;
        private DevComponents.DotNetBar.RibbonBar Barra_Menu;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;
        private DevComponents.DotNetBar.ButtonItem cmdAddRows;
        private DevComponents.DotNetBar.ButtonItem cmdDelRow;
        private DevComponents.DotNetBar.ButtonItem cmdRowDel;
        private DevComponents.DotNetBar.ItemContainer itemContainer12;
        private DevComponents.DotNetBar.ButtonItem buttonPaste;
        private DevComponents.DotNetBar.ItemContainer itemContainer14;
        private DevComponents.DotNetBar.ButtonItem buttonItem67;
        private DevComponents.DotNetBar.ButtonItem cmd_Filtrar;
        private DevComponents.DotNetBar.ItemContainer itemContainer2;
        private DevComponents.DotNetBar.TextBoxItem textBox1;
        private DevComponents.DotNetBar.ButtonItem cmdSearch;
        private DevComponents.DotNetBar.ButtonItem cmdVerOcultar;
        private DevComponents.DotNetBar.ButtonItem cmdReemplazarPorColumna;
        private DevComponents.DotNetBar.ButtonItem buttonItem6;
        private DevComponents.DotNetBar.ButtonItem buttonItem47;
    }
}