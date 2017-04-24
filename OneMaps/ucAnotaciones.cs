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
    public partial class ucAnotaciones : UserControl
    {
        public bool cerrado = false;
        Dictionary<string, DataTable> dicPrincipal;
        WebBrowser wbEarth;
        cDiccionarios Metadatos;
        NotifyIcon Notificacion;
        public ucAnotaciones(Dictionary<string, DataTable> dicPri, WebBrowser web, cDiccionarios DicDatos, NotifyIcon Noti)
        {
            dicPrincipal = dicPri;
            InitializeComponent();
            wbEarth = web;
            Metadatos = DicDatos;
            Notificacion = Noti;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Notificacion.Visible = true;
            Notificacion.ShowBalloonTip(290000);
            DataTable TABLA = dicPrincipal["ANOTACIONES:"+cbTabla.SelectedItem.ToString()];
            string Nom = TABLA.TableName;
            string alterno = "ANOTACION:" + Nom;
            string LAT = cbX.SelectedItem.ToString();
            string LON = cbY.SelectedItem.ToString();
            string Zona = cbNum.SelectedItem.ToString() + " " + cbLetra.SelectedItem.ToString();
            Conversor cv = new Conversor();
            object[] ContendorPrincipal = new object[4];
            double x = 0, y = 0, Conve = 0;
            bool nopase = true;
            List<object> op1 = new List<object>();
            List<object> op2 = new List<object>();

            if (!Metadatos.ContainsDictionary(alterno))
            {
                List<string> zAux = new List<string>();
                zAux.Add("ANOTACION");
                zAux.Add(LAT);
                zAux.Add(LON);
                zAux.Add(Zona.Split(' ')[0]);
                zAux.Add(Zona.Split(' ')[1]);
                Metadatos.addDictionary(alterno, zAux);
            }

            for (int i = 0; i < TABLA.Rows.Count; i++)
            {
                if (double.TryParse(TABLA.Rows[i][LAT].ToString(), out Conve) && double.TryParse(TABLA.Rows[i][LON].ToString(), out Conve))
                {
                    cv.ToLatLon(double.Parse(TABLA.Rows[i][LAT].ToString()), double.Parse(TABLA.Rows[i][LON].ToString()), Zona, out x, out y);
                    op1.Add(x);
                    op2.Add(y);
                }
                else
                {
                    if (op1.Count > 0 && op2.Count > 0)
                    {
                        nopase = false;
                        ContendorPrincipal[0] = op1.ToArray();
                        ContendorPrincipal[1] = op2.ToArray();
                        ContendorPrincipal[2] = "";
                        ContendorPrincipal[3] = Nom;
                        wbEarth.Document.InvokeScript("CreaLinea", ContendorPrincipal);
                        op1.Clear();
                        op2.Clear();
                    }
                    else nopase = true;
                }

            }
            if (nopase)
            {
                ContendorPrincipal[0] = op1.ToArray();
                ContendorPrincipal[1] = op2.ToArray();
                ContendorPrincipal[2] = "";
                ContendorPrincipal[3] = Nom;
                wbEarth.Document.InvokeScript("CreaLinea", ContendorPrincipal);
            }
            wbEarth.Document.InvokeScript("Enfoca", new object[] { x, y });
            wbEarth.Document.InvokeScript("AgregaLineaContenedor");

            Notificacion.Visible = false;
            cerrado = true;
            (this.Parent as Form).Close();
        }

        private void cbTabla_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nom = cbTabla.SelectedItem.ToString();
            string alterno = "ANOTACION:" + nom;
            if (dicPrincipal.ContainsKey("ANOTACIONES:" + nom))
            {
                DataTable tabla = dicPrincipal["ANOTACIONES:" + nom];
                List<string> nombre = (from DataColumn row in tabla.Columns
                                       select row.Caption).ToList();
                cbX.DataSource = nombre;
                cbY.DataSource = nombre.ToList();
            }
            if (Metadatos.ContainsDictionary(alterno) && Metadatos.queTipoEs(alterno) == "ANOTACION")
            {
                cbX.Text = Metadatos.valueDictionary(alterno)[1];
                cbY.Text = Metadatos.valueDictionary(alterno)[2];
                cbNum.Text = Metadatos.valueDictionary(alterno)[3];
                cbLetra.Text = Metadatos.valueDictionary(alterno)[4];
            }
        }

        private void ucAnotaciones_Load(object sender, EventArgs e)
        {
            List<string> ji = (from x in dicPrincipal.Keys where x.ToString().Contains("ANOTACIONES")  select x.ToString().Split(':')[1]).ToList();
            cbTabla.DataSource = ji.ToList();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            wbEarth.Document.InvokeScript("RemoveLineString", new object[] { cbTabla.SelectedItem.ToString() });
            dicPrincipal.Remove(cbTabla.SelectedItem.ToString());
        }
    }
}
