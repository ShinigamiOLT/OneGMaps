namespace Maps
{
    partial class userFiltroElemento
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBuscar = new System.Windows.Forms.ComboBox();
            this.Marcador = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.Lista_eventos = new System.Windows.Forms.CheckedListBox();
            this.Lista_Unica = new System.Windows.Forms.ListBox();
            this.panel_user_actual = new System.Windows.Forms.Panel();
            this.Panel_Elementos = new System.Windows.Forms.Panel();
            this.panel_aceptar = new System.Windows.Forms.Panel();
            this.cmdAceptar = new System.Windows.Forms.Button();
            this.PanelTipo = new System.Windows.Forms.Panel();
            this.cmdfechas = new System.Windows.Forms.Button();
            this.cmdTipos = new System.Windows.Forms.Button();
            this.cmdValor = new System.Windows.Forms.Button();
            this.panel_user_actual.SuspendLayout();
            this.Panel_Elementos.SuspendLayout();
            this.panel_aceptar.SuspendLayout();
            this.PanelTipo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Buscar:";
            // 
            // cmbBuscar
            // 
            this.cmbBuscar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbBuscar.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbBuscar.FormattingEnabled = true;
            this.cmbBuscar.Location = new System.Drawing.Point(63, 3);
            this.cmbBuscar.MaximumSize = new System.Drawing.Size(180, 0);
            this.cmbBuscar.MinimumSize = new System.Drawing.Size(90, 0);
            this.cmbBuscar.Name = "cmbBuscar";
            this.cmbBuscar.Size = new System.Drawing.Size(180, 21);
            this.cmbBuscar.TabIndex = 14;
            this.cmbBuscar.TextChanged += new System.EventHandler(this.cmbBuscar_TextChanged);
            // 
            // Marcador
            // 
            this.Marcador.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Marcador.AutoSize = true;
            // 
            // 
            // 
            this.Marcador.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Marcador.Location = new System.Drawing.Point(53, 492);
            this.Marcador.Name = "Marcador";
            this.Marcador.Size = new System.Drawing.Size(150, 15);
            this.Marcador.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Marcador.TabIndex = 12;
            this.Marcador.Text = "Marcar/Desmarcar Todos.";
            this.Marcador.CheckedChanged += new System.EventHandler(this.Marcador_CheckedChanged);
            // 
            // Lista_eventos
            // 
            this.Lista_eventos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Lista_eventos.CheckOnClick = true;
            this.Lista_eventos.FormattingEnabled = true;
            this.Lista_eventos.Location = new System.Drawing.Point(5, 30);
            this.Lista_eventos.Name = "Lista_eventos";
            this.Lista_eventos.Size = new System.Drawing.Size(247, 454);
            this.Lista_eventos.TabIndex = 11;
            this.Lista_eventos.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.Lista_eventos_ItemCheck);
            this.Lista_eventos.SelectedIndexChanged += new System.EventHandler(this.Lista_eventos_SelectedIndexChanged);
            // 
            // Lista_Unica
            // 
            this.Lista_Unica.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Lista_Unica.FormattingEnabled = true;
            this.Lista_Unica.Location = new System.Drawing.Point(4, 30);
            this.Lista_Unica.Name = "Lista_Unica";
            this.Lista_Unica.Size = new System.Drawing.Size(248, 459);
            this.Lista_Unica.TabIndex = 16;
            this.Lista_Unica.SelectedIndexChanged += new System.EventHandler(this.Lista_Unica_SelectedIndexChanged);
            // 
            // panel_user_actual
            // 
            this.panel_user_actual.Controls.Add(this.Panel_Elementos);
            this.panel_user_actual.Controls.Add(this.panel_aceptar);
            this.panel_user_actual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_user_actual.Location = new System.Drawing.Point(0, 29);
            this.panel_user_actual.Name = "panel_user_actual";
            this.panel_user_actual.Size = new System.Drawing.Size(256, 549);
            this.panel_user_actual.TabIndex = 17;
            // 
            // Panel_Elementos
            // 
            this.Panel_Elementos.Controls.Add(this.cmbBuscar);
            this.Panel_Elementos.Controls.Add(this.Marcador);
            this.Panel_Elementos.Controls.Add(this.label1);
            this.Panel_Elementos.Controls.Add(this.Lista_Unica);
            this.Panel_Elementos.Controls.Add(this.Lista_eventos);
            this.Panel_Elementos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Elementos.Location = new System.Drawing.Point(0, 0);
            this.Panel_Elementos.Name = "Panel_Elementos";
            this.Panel_Elementos.Size = new System.Drawing.Size(256, 513);
            this.Panel_Elementos.TabIndex = 18;
            // 
            // panel_aceptar
            // 
            this.panel_aceptar.Controls.Add(this.cmdAceptar);
            this.panel_aceptar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_aceptar.Location = new System.Drawing.Point(0, 513);
            this.panel_aceptar.Name = "panel_aceptar";
            this.panel_aceptar.Size = new System.Drawing.Size(256, 36);
            this.panel_aceptar.TabIndex = 19;
            // 
            // cmdAceptar
            // 
            this.cmdAceptar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdAceptar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmdAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdAceptar.Location = new System.Drawing.Point(73, 6);
            this.cmdAceptar.Name = "cmdAceptar";
            this.cmdAceptar.Size = new System.Drawing.Size(111, 24);
            this.cmdAceptar.TabIndex = 3;
            this.cmdAceptar.Text = "&Aceptar";
            this.cmdAceptar.Click += new System.EventHandler(this.boton_Click);
            // 
            // PanelTipo
            // 
            this.PanelTipo.Controls.Add(this.cmdfechas);
            this.PanelTipo.Controls.Add(this.cmdTipos);
            this.PanelTipo.Controls.Add(this.cmdValor);
            this.PanelTipo.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTipo.Location = new System.Drawing.Point(0, 0);
            this.PanelTipo.Name = "PanelTipo";
            this.PanelTipo.Size = new System.Drawing.Size(256, 29);
            this.PanelTipo.TabIndex = 17;
            this.PanelTipo.Visible = false;
            // 
            // cmdfechas
            // 
            this.cmdfechas.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdfechas.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmdfechas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdfechas.Location = new System.Drawing.Point(176, 2);
            this.cmdfechas.Name = "cmdfechas";
            this.cmdfechas.Size = new System.Drawing.Size(71, 24);
            this.cmdfechas.TabIndex = 4;
            this.cmdfechas.Text = "Fecha";
            this.cmdfechas.Click += new System.EventHandler(this.cmdfechas_Click);
            // 
            // cmdTipos
            // 
            this.cmdTipos.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdTipos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmdTipos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdTipos.Location = new System.Drawing.Point(9, 3);
            this.cmdTipos.Name = "cmdTipos";
            this.cmdTipos.Size = new System.Drawing.Size(71, 24);
            this.cmdTipos.TabIndex = 3;
            this.cmdTipos.Text = "Tipo";
            this.cmdTipos.Click += new System.EventHandler(this.cmdTipos_Click);
            // 
            // cmdValor
            // 
            this.cmdValor.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdValor.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmdValor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdValor.Location = new System.Drawing.Point(93, 3);
            this.cmdValor.Name = "cmdValor";
            this.cmdValor.Size = new System.Drawing.Size(71, 24);
            this.cmdValor.TabIndex = 2;
            this.cmdValor.Text = "Valor";
            this.cmdValor.Click += new System.EventHandler(this.cmdValor_Click);
            // 
            // userFiltroElemento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_user_actual);
            this.Controls.Add(this.PanelTipo);
            this.Name = "userFiltroElemento";
            this.Size = new System.Drawing.Size(256, 578);
            this.Load += new System.EventHandler(this.userFiltroElemento_Load);
            this.panel_user_actual.ResumeLayout(false);
            this.Panel_Elementos.ResumeLayout(false);
            this.Panel_Elementos.PerformLayout();
            this.panel_aceptar.ResumeLayout(false);
            this.PanelTipo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.CheckBoxX Marcador;
        public System.Windows.Forms.ComboBox cmbBuscar;
        public System.Windows.Forms.CheckedListBox Lista_eventos;
        private System.Windows.Forms.Panel panel_user_actual;
        public System.Windows.Forms.Button cmdfechas;
        public System.Windows.Forms.Button cmdTipos;
        public System.Windows.Forms.Button cmdValor;
        public System.Windows.Forms.Panel PanelTipo;
        public System.Windows.Forms.Panel panel_aceptar;
        public System.Windows.Forms.Button cmdAceptar;
        public System.Windows.Forms.Panel Panel_Elementos;
        public System.Windows.Forms.ListBox Lista_Unica;
    }
}
