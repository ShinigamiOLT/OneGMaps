using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maps
{
    public partial class fnSeleccionAreas : Form
    {
        DataTable dtPrincipal;
        Dictionary<string, DataTable> dicPricipal;
        DevComponents.DotNetBar.Controls.DataGridViewX dgvArea;
        WebBrowser webB;
        public fnSeleccionAreas(DataTable area, Dictionary<string, DataTable> dicPri, DevComponents.DotNetBar.Controls.DataGridViewX dgvAreas, WebBrowser web)
        {
            InitializeComponent();
            webB = web;
            dtPrincipal = area;
            dicPricipal = dicPri;
            dgvArea = dgvAreas;
            dataGridView1.DataSource = dtPrincipal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
                try
                {
                    if (dtPrincipal.Rows.Count > 0)
                    {
                        double peri = 0;
                        double ddd = 0;
                        double fff = 0;

                        var promedioX = (from DataRow row in dtPrincipal.Rows
                                         where double.TryParse(row["LATITUD"].ToString(), out ddd)
                                         select ddd).ToList().Average();

                        var promedioY = (from DataRow row in dtPrincipal.Rows
                                         where double.TryParse(row["LONGITUD"].ToString(), out fff)
                                         select fff).ToList().Average();

                        double[] cp = new double[] { promedioX, promedioY };
                        //var area = CalculoArea(dtPrincipal, "LATITUD", "LONGITUD", cp, out peri);
                        var area2 = CalculoAreaGaus(dtPrincipal, "LATITUD", "LONGITUD", cp, out peri);
                        area2 = area2 / 1000000;

                        DataTable copia = dtPrincipal.Copy();
                        copia.TableName = textBox1.Text;
                        string nombre = "AREA:" + textBox1.Text;
                        if (!dicPricipal.Values.Contains(copia)) dicPricipal.Add(nombre, copia);
                        else dicPricipal[nombre] = copia;
                        dgvArea.Rows.Add(false, textBox1.Text, area2 * 1000, Math.Round(area2, 4), area2 * 247.11, area2 / 0.01, peri, "", "", " X ");
                        webB.Document.InvokeScript("eliminaArea");
                    }
                    this.Close();
                    webB.Document.InvokeScript("EstablecePoligono", new object[] { false });
                }
                catch
                { }
            else
            {
                MessageBox.Show("Ingresa un nombre al area a calcular");
                return;
            }
            
        }

        private double CalculoAreaGaus(DataTable tabla, string Lat, string Lon, double[] PuntoCentral, out double peri)
        {
            double aux = 0;
            double a1 = 0, b1 = 0, Base;
            Conversor cvb = new Conversor();
            double x1 = 0, y1 = 0, x2 = 0, y2 = 0, area = 0;
            try
            {
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    if (double.TryParse(tabla.Rows[i][Lat].ToString(), out x1) && double.TryParse(tabla.Rows[i][Lon].ToString(), out y1))
                    {
                        
                        if (i + 1 == tabla.Rows.Count)
                        {

                            x2 = double.Parse(tabla.Rows[0][Lat].ToString());
                            y2 = double.Parse(tabla.Rows[0][Lon].ToString());
                            Base = distancia(x1, x2, y1, y2) * 111.111;//base
                            var cad = cvb.ConvertToUtmString(x2, y2);
                            x2 = cad[0];
                            y2 = cad[1];
                        }
                        else
                        {

                            x2 = double.Parse(tabla.Rows[i + 1][Lat].ToString());
                            y2 = double.Parse(tabla.Rows[i + 1][Lon].ToString());
                            Base = distancia(x1, x2, y1, y2) * 111.111;//base
                            var cad = cvb.ConvertToUtmString(x2, y2);
                            x2 = cad[0];
                            y2 = cad[1];
                        }

                        var cad1 = cvb.ConvertToUtmString(x1, y1);
                        x1 = cad1[0];
                        y1 = cad1[1];
                        aux += Base;
                        a1 += x1 * Math.Abs(y2);
                        b1 -= Math.Abs(y1) * x2;

                    }
                }
                area = 0.5f * Math.Abs(a1 + b1);
            }
            catch
            { }
            peri = aux;
            return area;
        }


        private double CalculoArea(DataTable tabla, string Lat, string Lon, double[] PuntoCentral, out double perimetro)
        {
            double aux = 0;
            double d1 = 0, d2 = 0, Base = 0, x1 = 0, y1 = 0, x2 = 0, y2 = 0, area = 0;
            try
            {
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    if (double.TryParse(tabla.Rows[i][Lat].ToString(), out x1) && double.TryParse(tabla.Rows[i][Lon].ToString(), out y1))
                    {

                        if (i + 1 == tabla.Rows.Count)
                        {
                            x2 = double.Parse(tabla.Rows[0][Lat].ToString());
                            y2 = double.Parse(tabla.Rows[0][Lon].ToString());
                        }
                        else
                        {
                            x2 = double.Parse(tabla.Rows[i + 1][Lat].ToString());
                            y2 = double.Parse(tabla.Rows[i + 1][Lon].ToString());
                        }

                        d1 = distancia(x1, PuntoCentral[0], y1, PuntoCentral[1]) * 111.111f;
                        d2 = distancia(x2, PuntoCentral[0], y2, PuntoCentral[1]) * 111.111f;
                        Base = distancia(x1, x2, y1, y2) * 111.111;//base
                        double semiperimetro = (d1 + d2 + Base) / 2;

                        aux += Base;
                        area += Math.Sqrt(semiperimetro * ((semiperimetro - d1) * (semiperimetro - d2) * (semiperimetro - Base)));
                    }
                }
            }
            catch
            { }
            perimetro = aux;
            return area;
        }

        double distancia(double x1, double x2, double y1, double y2)
        { return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)); }

        private void button2_Click(object sender, EventArgs e)
        {
            webB.Document.InvokeScript("QuitaUltimoElemento");
            dataGridView1.Rows.RemoveAt(dataGridView1.RowCount-1);
        }

        private void fnSeleccionAreas_FormClosed(object sender, FormClosedEventArgs e)
        {
            webB.Document.InvokeScript("EstablecePoligono", new object[] { false });
            webB.Document.InvokeScript("eliminaArea");
        }
    }
}
