using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maps
{
    public partial class ucMaestro : UserControl
    {
        public bool cerrado = false;
        GeneraColores Generando;
        public ucMaestro(DevComponents.DotNetBar.Controls.DataGridViewX dt)
        {
            InitializeComponent(); 
            Generando = new GeneraColores();
            splitContainer1.Panel1.Controls.Add(dt);
            dt.Dock = DockStyle.Fill;
            try { dt.Columns["Eliminar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader; }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cerrado = true;
            (this.Parent as Form).Close();
        }

       
    }
}
