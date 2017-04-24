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
    public partial class ucMarcas : UserControl
    {
        public bool cerrado = false;
        Dictionary<string, DataTable> dicPrincipal;
        WebBrowser wbEarth;
        public ucMarcas(Dictionary<string, DataTable> dicPri, WebBrowser web)
        {
            dicPrincipal = dicPri;
            InitializeComponent();
            wbEarth = web;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ColX = cbX.SelectedItem.ToString();
            string ColY = cbY.SelectedItem.ToString();
            string nombre = cbTabla.SelectedItem.ToString();
            string tiulo = cbTitulo.SelectedItem.ToString();
            DataTable dtAuz = dicPrincipal["ANOTACIONES:" + nombre];
            double x = 0, y =0;
            foreach (DataRow row in dtAuz.Rows)
            {
                if (double.TryParse(row[ColX].ToString(), out x) && double.TryParse(row[ColY].ToString(), out y))
                    wbEarth.Document.InvokeScript("CreaMarca", new object[] { row[tiulo].ToString(), "", x, y });
            }
            cerrado = true;
            (this.Parent as Form).Close();
        }

        private void ucMarcas_Load(object sender, EventArgs e)
        {
            List<string> ji = (from x in dicPrincipal.Keys where x.Contains("MARCAS") select x.ToString().Split(':')[1]).ToList();
            cbTabla.DataSource = ji.ToList();
        }

        private void cbTabla_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nom = cbTabla.SelectedItem.ToString();
            if (dicPrincipal.ContainsKey("ANOTACIONES:" + nom))
            {
                DataTable tabla = dicPrincipal["ANOTACIONES:" + nom];
                List<string> nombre = (from DataColumn row in tabla.Columns
                                       select row.Caption).ToList();
                cbX.DataSource = nombre;
                cbY.DataSource = nombre.ToList();
                cbTitulo.DataSource = nombre.ToList();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string nombre = cbTabla.SelectedItem.ToString();
            DataTable dtAuz = dicPrincipal["ANOTACIONES:" + nombre];
            string tiulo = cbTitulo.SelectedItem.ToString();
            foreach (DataRow row in dtAuz.Rows)
            {
                wbEarth.Document.InvokeScript("EliminaMarcaCirculo", new object[] { row[tiulo].ToString() });
            }
        }
    }
}
