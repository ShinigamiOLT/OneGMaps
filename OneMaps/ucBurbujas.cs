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
    public partial class ucBurbujas : UserControl
    {
        public bool cerrado = false;
        Dictionary<string, DataTable> dicPrincipal;
        WebBrowser wbEarth;
        cDiccionarios Metadatos;
        NotifyIcon Notificacion;
        public ucBurbujas(Dictionary<string, DataTable> dicPri, WebBrowser web, cDiccionarios DicDatos, NotifyIcon noti)
        {
            dicPrincipal = dicPri;
            Metadatos = DicDatos;
            InitializeComponent();
            wbEarth = web;
            Notificacion = noti;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                btnEliminar_Click(null, null);
                Notificacion.Visible = true;
                Notificacion.ShowBalloonTip(290000);
                var color = String.Format("{0:X2}{1:X2}{2:X2}{3:X2}", textBox1.BackColor.A, textBox1.BackColor.B, textBox1.BackColor.G, textBox1.BackColor.R);
                string ColX = cbX.Text;
                string ColY = cbY.Text;
                string nombre = cbTabla.SelectedItem.ToString();
                DataTable dtAuz = dicPrincipal["BURBUJAS:" + nombre];
                double x = 0, y = 0, Radio = 0;
                string titulo = cbTitulo.SelectedItem.ToString();
                string variable = cbVariable.SelectedItem.ToString();

                {
                    List<string> zAux = new List<string>();
                    zAux.Add("BURBUJA");
                    zAux.Add(titulo);
                    zAux.Add(ColX);
                    zAux.Add(ColY);
                    zAux.Add(variable);
                    zAux.Add(textBox1.BackColor.Name);
                    Metadatos.addDictionary("BURBUJA:" + nombre, zAux);
                }

                foreach (DataRow row in dtAuz.Rows)
                {
                    if (double.TryParse(row[ColX].ToString(), out x) && double.TryParse(row[ColY].ToString(), out y))
                    {
                        if (double.TryParse(row[variable].ToString(), out Radio))
                        {
                            //wbEarth.Document.InvokeScript("EliminaMarcaCirculo", new object[] { row[titulo].ToString() });
                            wbEarth.Document.InvokeScript("CreaCirculo", new object[] { row[titulo].ToString()+variable, color, x, y, Radio / 111.111 });
                        }
                    }
                }
                Notificacion.Visible = false;
                cerrado = true;
                (this.Parent as Form).Close();
            }
            catch 
            { }
        }

        private void ucBurbujas_Load(object sender, EventArgs e)
        {
            List<string> ji = (from x in dicPrincipal.Keys where x.Contains("BURBUJAS") select x.ToString().Split(':')[1]).ToList();
            cbTabla.DataSource = ji.ToList();

            
        }

        private void cbTabla_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string nom = cbTabla.SelectedItem.ToString();
                string alterno = "BURBUJA:" + nom;
                if (dicPrincipal.ContainsKey("BURBUJAS:" + nom))
                {
                    DataTable tabla = dicPrincipal["BURBUJAS:" + nom];
                    List<string> nombre = (from DataColumn row in tabla.Columns
                                           select row.Caption).ToList();
                    cbX.DataSource = nombre;
                    cbY.DataSource = nombre.ToList();
                    cbTitulo.DataSource = nombre.ToList();
                    cbVariable.DataSource = nombre.ToList();
                }
                if (Metadatos.ContainsDictionary(alterno) && Metadatos.queTipoEs(alterno) == "BURBUJA")
                {
                    cbTitulo.Text = Metadatos.valueDictionary(alterno)[1];
                    cbX.Text = Metadatos.valueDictionary(alterno)[2];
                    cbY.Text = Metadatos.valueDictionary(alterno)[3];
                    cbVariable.Text = Metadatos.valueDictionary(alterno)[4];
                    textBox1.BackColor = Color.FromName(Metadatos.valueDictionary(alterno)[5]);
                }
            }
            catch { }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string nombre = cbTabla.Text;
            DataTable dtAuz = dicPrincipal["BURBUJAS:" + nombre];
            string titulo = cbTitulo.SelectedItem.ToString();

            double x = 0, y = 0, Radio = 0;
            string ColX = cbX.Text, ColY = cbY.Text, variable = cbVariable.Text;
            foreach (DataRow row in dtAuz.Rows)
            {
                if (double.TryParse(row[ColX].ToString(), out x) && double.TryParse(row[ColY].ToString(), out y))
                {
                    if (double.TryParse(row[variable].ToString(), out Radio))
                    {
                        wbEarth.Document.InvokeScript("EliminaCirculo", new object[] { row[titulo].ToString() + variable });
                    }
                }
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
                ColorDialog Colornuevo = new ColorDialog();
                if (Colornuevo.ShowDialog() == DialogResult.OK)
                    textBox1.BackColor = Colornuevo.Color;
                    
        }
    }
}
