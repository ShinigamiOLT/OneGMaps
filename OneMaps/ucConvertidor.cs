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
    public partial class ucConvertidor : UserControl
    {
        userFiltroElemento filtroTablas;
        DataTable dtTabla;
        Conversor cv;
        public ucConvertidor(DataTable dt)
        {
            InitializeComponent();
            dtTabla = dt;
            dataGridViewX1.Rows.Add();
            cv = new Conversor();
            dgv_origen.DataSource = dtTabla;
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var t = (from DataColumn row in dtTabla.Columns select new ObjetoSelecionable(row.Caption)).ToList();
            filtroTablas = new userFiltroElemento(ref t, 1);
            filtroTablas.EnForm(false);
            dataGridViewX1[e.ColumnIndex, e.RowIndex].Value = filtroTablas.Seleccionado.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridViewX1.Rows[0];
            string lat = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : "";
            string lon = row.Cells[1].Value != null ? row.Cells[1].Value.ToString() : "";
            double x = 0, y = 0;
            if (lat != "" && lon != "")
            {
                dtTabla.Columns.Add("LATITUD", typeof(double));
                dtTabla.Columns.Add("LONGITUD", typeof(double));
                foreach (DataRow filas in dtTabla.Rows)
                {
                    string a = filas[lat].ToString();
                    string b = filas[lon].ToString();
                    x = cv.degToDec(a);
                    y = cv.degToDec(b);
                    if (x > 0)
                    {
                        filas["LATITUD"] = x;
                        filas["LONGITUD"] = y;
                    }
                }
            }
            else
            {
            }

            double[] pc = new double[] { 16.349784474421885, -94.413560064348829 };
            //double[] pc = new double[] { 18.0002466, -92.96831393 };
           var ttt =  CalculoAreaGaus(dtTabla, "LATITUD", "LONGITUD", pc);
        }

        private double CalculoAreaGaus(DataTable tabla, string Lat, string Lon, double[] PuntoCentral)
        {
            double aux = 0;
            double a1 = 0, b1 = 0;
            Conversor cvb = new Conversor();
            double d1 = 0, d2 = 0, Base = 0, x1 = 0, y1 = 0, x2 = 0, y2 = 0, area = 0;
            try
            {
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    if (double.TryParse(tabla.Rows[i][Lat].ToString(), out x1) && double.TryParse(tabla.Rows[i][Lon].ToString(), out y1))
                    {
                        var cad1 = cvb.ConvertToUtmString(x1, y1);
                        x1 = cad1[0];
                        y1 = cad1[1];
                        if (i + 1 == tabla.Rows.Count)
                        {

                            x2 = double.Parse(tabla.Rows[0][Lat].ToString());
                            y2 = double.Parse(tabla.Rows[0][Lon].ToString());
                            var cad = cvb.ConvertToUtmString(x2, y2);
                            x2 = cad[0];
                            y2 = cad[1];
                        }
                        else
                        {
                            x2 = double.Parse(tabla.Rows[i + 1][Lat].ToString());
                            y2 = double.Parse(tabla.Rows[i + 1][Lon].ToString());
                            var cad = cvb.ConvertToUtmString(x2, y2);
                            x2 = cad[0];
                            y2 = cad[1];
                        }

                        Base = distancia(x1, x2, y1, y2) * 111.111;//base
                        aux += Base;
                        a1 += x1 * Math.Abs(y2);
                        b1 -= Math.Abs(y1) * x2;

                    }
                }
                area = 0.5f * Math.Abs(a1 + b1);
            }
            catch
            { }
            return area/10000;
        }


        private double CalculoArea(DataTable tabla, string Lat, string Lon, double[] PuntoCentral)
        {
            double aux = 0;
            double d1 = 0, d2 = 0, Base = 0, x1 = 0, y1 = 0, x2 = 0, y2 = 0, area = 0;
            try
            {
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    if (double.TryParse(tabla.Rows[i][Lat].ToString(), out x1) && double.TryParse(tabla.Rows[i][Lon].ToString(), out y1))
                    {

                        if (i + 1 == tabla.Rows.Count - 1)
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
            return area;
        }

        double distancia(double x1, double x2, double y1, double y2)
        { return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)); }
    }
}
