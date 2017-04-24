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
    public partial class ucArea : UserControl
    {
        public List<DataGridViewRow> RowEdit;
        userFiltroElemento filtroTablas;
        userFiltroElemento filtroColumnas;
        Dictionary<string, DataTable> dicPrincipal;
        DataTable dtPrincipal;
        WebBrowser wbEarth;
        DevComponents.DotNetBar.Controls.DataGridViewX dgvPrincipal;
        public bool cerrado = false;

        public ucArea(Dictionary<string, DataTable> dicPri, DevComponents.DotNetBar.Controls.DataGridViewX Burbujas, WebBrowser web, NotifyIcon noti, List<DataGridViewRow> Rowedit)
        {

            InitializeComponent();
            RowEdit = Rowedit;
            dicPrincipal = dicPri;

            dgvPrincipal = Burbujas;

            wbEarth = web;
            Notificacion = noti;

            dgvPrincipal.Dock = DockStyle.Fill;
            splitContainer1.Panel1.Controls.Add(dgvPrincipal);
            RowEdit.Clear();
                dgvPrincipal.Columns["Eliminar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                List<ObjetoSelecionable> ji = (from x in dicPrincipal.Keys where x.Contains("AREA") select new ObjetoSelecionable(x.ToString().Split(':')[1])).ToList();
                filtroTablas = new userFiltroElemento(ref ji, 1);
                dgvPrincipal.CellClick += dgvPrincipal_CellClick;
                dgvPrincipal.DefaultValuesNeeded += dgvPrincipal_DefaultValuesNeeded;
                dgvPrincipal.RowsAdded += dgvPrincipal_RowsAdded;
                this.HandleDestroyed += ucAnotacion_HandleDestroyed;
            
            try { dgvPrincipal.Columns["Eliminar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader; }
            catch { }
            
        }

        private void ucAnotacion_HandleDestroyed(object sender, EventArgs e)
        {
            dgvPrincipal.CellClick -= dgvPrincipal_CellClick;
            dgvPrincipal.DefaultValuesNeeded -= dgvPrincipal_DefaultValuesNeeded;
            dgvPrincipal.RowsAdded -= dgvPrincipal_RowsAdded;
            this.HandleDestroyed -= ucAnotacion_HandleDestroyed;
        }

        bool filaLLena()
        {
            return dgvPrincipal[1, 0].Value != null ? false : true;
        }
        bool verdadero = true;
        private void dgvPrincipal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0: //Visible 
                    if (verdadero)
                    {
                        verdadero = false;

                    }
                    else
                    {
                        verdadero = true;
                    }
                    break;

                case 1:
                    filtroTablas.EnForm(false);
                    if (filtroTablas.cerrado)
                        return;
                    dgvPrincipal[e.ColumnIndex, e.RowIndex].Value = filtroTablas.Seleccionado.ToString();

                    dtPrincipal = dicPrincipal["AREAS:" + filtroTablas.Seleccionado.ToString()];
                    List<ObjetoSelecionable> ji = (from DataColumn col in dtPrincipal.Columns select (new ObjetoSelecionable(col.Caption))).ToList();
                    filtroColumnas = new userFiltroElemento(ref ji, 1);
                    break;
                case 8:
                    if (filtroColumnas == null)
                    {
                        dtPrincipal = dicPrincipal["AREA:" + dgvPrincipal[1, e.RowIndex].Value.ToString()];
                        contenedor conten = new contenedor();
                        DevComponents.DotNetBar.Controls.DataGridViewX dt = new DevComponents.DotNetBar.Controls.DataGridViewX();
                        dt.DataSource = dtPrincipal;
                        ucMaestro maestro = new ucMaestro(dt);
                        conten.Controls.Clear();
                        conten.Controls.Add(maestro);
                        conten.Show(this);
                    }
                    break;
                case 9: //eliminar

                    string cad =dgvPrincipal[1, e.RowIndex].Value.ToString(); RowEdit.Remove(dgvPrincipal.Rows[e.RowIndex]);
                    wbEarth.Document.InvokeScript("RemoveLineString", new object[] { cad });dgvPrincipal.Rows.RemoveAt(e.RowIndex);
                    //dicPrincipal.Remove("AREA:" + cad);

                    return;


                default:
                    string nomb = dgvPrincipal.Columns[e.ColumnIndex].Name;
                    if (nomb == "Color")
                    {
                        ColorDialog Colornuevo1 = new ColorDialog();
                        if (Colornuevo1.ShowDialog() == DialogResult.OK)
                        {
                            var color = String.Format("{0:X2}{1:X2}{2:X2}{3:X2}", Colornuevo1.Color.A, Colornuevo1.Color.B, Colornuevo1.Color.G, Colornuevo1.Color.R);
                            dgvPrincipal[e.ColumnIndex, e.RowIndex].Style.BackColor = Colornuevo1.Color;
                            dgvPrincipal[e.ColumnIndex, e.RowIndex].Tag = Colornuevo1.Color.Name;
                            dgvPrincipal[e.ColumnIndex, e.RowIndex].Value = "";
                            dgvPrincipal[e.ColumnIndex, e.RowIndex].Selected = false;
                        }
                    }
                    break;
            }
            if (!RowEdit.Contains(dgvPrincipal.Rows[e.RowIndex]))
                RowEdit.Add(dgvPrincipal.Rows[e.RowIndex]);
        }

        private void dgvPrincipal_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dgvPrincipal[0, e.RowIndex].Value = true;
            dgvPrincipal.Rows[e.RowIndex].Cells["Eliminar"].Value = "X";
        }

        private void dgvPrincipal_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["Visible"].Value = true;
            e.Row.Cells["Eliminar"].Value = "X";
        }

        private void fnAreas_Load(object sender, EventArgs e)
        {

        }

        public NotifyIcon Notificacion { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in RowEdit)
            {
                string NomP = row.Cells[1].Value.ToString();
                wbEarth.Document.InvokeScript("RemoveLineString", new object[] { NomP });
                if ((bool)row.Cells[0].Value)
                {
                    dtPrincipal = dicPrincipal["AREA:" + NomP];
                    var color = row.Cells[7].Style.BackColor;
                    string descri ="Area: "+ row.Cells[3].Value.ToString() + " KM2, Hectareas: " + row.Cells[5].Value.ToString() + " Ha, Perimetro: " + row.Cells[3].Value.ToString()+"KMS";
                    var colorA = String.Format("{0:X2}{1:X2}{2:X2}{3:X2}", color.A, color.B, color.G, color.R);
                    List<object> op1 = new List<object>();
                    List<object> op2 = new List<object>();
                    double grosor = 2;
                    for (int i = 0; i < dtPrincipal.Rows.Count; i++)
                    {
                        op1.Add(double.Parse(dtPrincipal.Rows[i]["LATITUD"].ToString()));
                        op2.Add(double.Parse(dtPrincipal.Rows[i]["LONGITUD"].ToString()));
                    }
                    wbEarth.Document.InvokeScript("CreaLinea", new object[] { op1.ToArray(), op2.ToArray(), "", NomP, colorA, grosor, descri });
                }
                else wbEarth.Document.InvokeScript("RemoveLineString", new object[] { NomP });
            }
        }
    }
}
