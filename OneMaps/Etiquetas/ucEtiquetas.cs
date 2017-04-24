using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Maps.Etiquetas
{
    public partial class ucEtiquetas : UserControl
    {
        cEtiqueta Etiqueta;
        Dictionary<string, DataTable> DicPrincipal;
        public ucEtiquetas(Dictionary<string,DataTable> Dic)
        {
            InitializeComponent();
            DicPrincipal= Dic;
            Etiqueta = new cEtiqueta(DicPrincipal);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Etiqueta.CambiaTabla(comboBox1.Text);
        }
    }
}
