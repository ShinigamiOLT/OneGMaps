using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maps.Aritmetica
{
    public partial class ucRadio : UserControl
    {
        DataTable dtPrincipal;
        public ucRadio(DataTable dt)
        {
            dtPrincipal = dt;
            InitializeComponent();

            comboBox1.DataSource = (from DataColumn col in dtPrincipal.Columns
                                    select col.ColumnName).ToList();
            comboBox2.DataSource = (from DataColumn col in dtPrincipal.Columns
                                    select col.ColumnName).ToList();
        }

        private void ucRadio_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double espesor = double.Parse(textBox1.Text);
                string columna = comboBox1.Text;
                string nombre = comboBox2.SelectedItem.ToString();
                if (!dtPrincipal.Columns.Contains(columna)) dtPrincipal.Columns.Add(columna);
                double valor = 0;
                foreach (DataRow row in dtPrincipal.Rows)
                {
                    if (double.TryParse(row[nombre].ToString(), out valor))
                    {
                        valor = Math.Sqrt(valor / (Math.PI * espesor));
                        row[columna] = valor;
                    }
                }
            }
            catch
            { }
        }
    }
}
