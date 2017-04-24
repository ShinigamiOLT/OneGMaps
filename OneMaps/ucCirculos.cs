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
    public partial class ucCirculos : UserControl
    {
        Dictionary<string, List<string>> Principal;
        WebBrowser wbEarth;
        public ucCirculos(Dictionary<string, List<string>> dtP, WebBrowser navegador)
        {
            wbEarth = navegador;
            Principal = dtP;
            
            InitializeComponent();
            foreach (KeyValuePair<string, List<string>> row in dtP)
            {
                dgvVecinos.Rows.Add(true, "Eliminar",row.Key);
            }
        }

        private void ucCirculos_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void dgvVecinos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0: break;
                case 1: //Eliminar
                    string nombre = dgvVecinos[2, e.RowIndex].Value.ToString();
                    var lista = Principal[nombre];
                    foreach (string nam in lista)
                    {
                        wbEarth.Document.InvokeScript("EliminaMarcaCirculo", new object[] { nam });
                    }
                    wbEarth.Document.InvokeScript("RemovePMark", new object[] { nombre });
                    wbEarth.Document.InvokeScript("RemovePMark", new object[] { "Anillo" + nombre });
                    Principal.Remove(nombre);
                    dgvVecinos.Rows.RemoveAt(e.RowIndex);

                    break;
                case 2: break;
            }
        }
    }
}
