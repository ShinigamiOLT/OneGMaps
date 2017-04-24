namespace Maps
{
    partial class fAritmetica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fAritmetica));
            this.rtbFormula = new System.Windows.Forms.RichTextBox();
            this.cmdDatosEventos = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbAuto = new System.Windows.Forms.ListBox();
            this.Notificacion = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnAyuda = new DevComponents.DotNetBar.ButtonX();
            this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
            this.cbFiltro = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // rtbFormula
            // 
            this.rtbFormula.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbFormula.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbFormula.Location = new System.Drawing.Point(74, 59);
            this.rtbFormula.Multiline = false;
            this.rtbFormula.Name = "rtbFormula";
            this.rtbFormula.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtbFormula.Size = new System.Drawing.Size(526, 23);
            this.rtbFormula.TabIndex = 2;
            this.rtbFormula.Text = "";
            this.rtbFormula.TextChanged += new System.EventHandler(this.rtbFormula_TextChanged);
            this.rtbFormula.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbFormula_KeyDown);
            this.rtbFormula.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtbFormula_KeyPress);
            // 
            // cmdDatosEventos
            // 
            this.cmdDatosEventos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDatosEventos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdDatosEventos.ForeColor = System.Drawing.Color.Black;
            this.cmdDatosEventos.Location = new System.Drawing.Point(474, 124);
            this.cmdDatosEventos.Name = "cmdDatosEventos";
            this.cmdDatosEventos.Size = new System.Drawing.Size(126, 33);
            this.cmdDatosEventos.TabIndex = 3;
            this.cmdDatosEventos.Text = "&Aplicar";
            this.cmdDatosEventos.UseVisualStyleBackColor = true;
            this.cmdDatosEventos.Click += new System.EventHandler(this.cmdDatosEventos_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 69;
            this.label1.Text = "Formulas";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(74, 34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(326, 20);
            this.textBox1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 26);
            this.label2.TabIndex = 71;
            this.label2.Text = "Nombre de la\r\ncolumna";
            // 
            // lbAuto
            // 
            this.lbAuto.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAuto.FormattingEnabled = true;
            this.lbAuto.ItemHeight = 15;
            this.lbAuto.Location = new System.Drawing.Point(77, 83);
            this.lbAuto.Name = "lbAuto";
            this.lbAuto.Size = new System.Drawing.Size(191, 79);
            this.lbAuto.TabIndex = 72;
            this.lbAuto.Visible = false;
            this.lbAuto.DoubleClick += new System.EventHandler(this.lbAuto_DoubleClick);
            this.lbAuto.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbAuto_MouseMove);
            // 
            // Notificacion
            // 
            this.Notificacion.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.Notificacion.BalloonTipText = "Se están procesando multiples operaciones";
            this.Notificacion.BalloonTipTitle = "Espere";
            this.Notificacion.Icon = ((System.Drawing.Icon)(resources.GetObject("Notificacion.Icon")));
            this.Notificacion.Text = "notifyIcon1";
            // 
            // btnAyuda
            // 
            this.btnAyuda.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAyuda.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAyuda.Image = global::Maps.Properties.Resources.deshacer;
            this.btnAyuda.Location = new System.Drawing.Point(7, 5);
            this.btnAyuda.Name = "btnAyuda";
            this.btnAyuda.Shape = new DevComponents.DotNetBar.EllipticalShapeDescriptor();
            this.btnAyuda.Size = new System.Drawing.Size(20, 20);
            this.btnAyuda.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAyuda.TabIndex = 74;
            // 
            // cbFiltro
            // 
            this.cbFiltro.AutoSize = true;
            this.cbFiltro.Location = new System.Drawing.Point(7, 142);
            this.cbFiltro.Name = "cbFiltro";
            this.cbFiltro.Size = new System.Drawing.Size(53, 17);
            this.cbFiltro.TabIndex = 75;
            this.cbFiltro.Text = "Filtros";
            this.cbFiltro.UseVisualStyleBackColor = true;
            // 
            // fAritmetica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 162);
            this.Controls.Add(this.cbFiltro);
            this.Controls.Add(this.btnAyuda);
            this.Controls.Add(this.lbAuto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdDatosEventos);
            this.Controls.Add(this.rtbFormula);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(627, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(627, 200);
            this.Name = "fAritmetica";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Formulas";
            this.Load += new System.EventHandler(this.fAritmetica_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbFormula;
        private System.Windows.Forms.Button cmdDatosEventos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbAuto;
        private System.Windows.Forms.NotifyIcon Notificacion;
        private DevComponents.DotNetBar.ButtonX btnAyuda;
        private DevComponents.DotNetBar.SuperTooltip superTooltip1;
        private System.Windows.Forms.CheckBox cbFiltro;
    }
}