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
    public partial class ucMasters : UserControl
    {
        public List<DataGridViewRow> RowEdit;
        userFiltroElemento filtroTablas;
        userFiltroElemento filtroColumnas;
        Dictionary<string, DataTable> dicPrincipal;
        DataTable dtPrincipal;
        WebBrowser wbEarth;
        DevComponents.DotNetBar.Controls.DataGridViewX dgvPrincipal;
        public bool cerrado = false;

        public ucMasters(Dictionary<string, DataTable> dicPri, DevComponents.DotNetBar.Controls.DataGridViewX Burbujas, WebBrowser web, NotifyIcon noti, List<DataGridViewRow> Rowedit)
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
            List<ObjetoSelecionable> ji = (from x in dicPrincipal.Keys where x.Contains("BURBUJAS") select new ObjetoSelecionable(x.ToString().Split(':')[1])).ToList();
            filtroTablas = new userFiltroElemento(ref ji, 1);
            dgvPrincipal.CellClick += dgvPrincipal_CellClick;
            dgvPrincipal.DefaultValuesNeeded += dgvPrincipal_DefaultValuesNeeded;
            dgvPrincipal.RowsAdded += dgvPrincipal_RowsAdded;
            this.HandleDestroyed += ucAnotacion_HandleDestroyed;

            try { dgvPrincipal.Columns["Eliminar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader; }
            catch { }
        }

        void ucAnotacion_HandleDestroyed(object sender, EventArgs e)
        {
            dgvPrincipal.CellClick -= dgvPrincipal_CellClick;
            dgvPrincipal.DefaultValuesNeeded -= dgvPrincipal_DefaultValuesNeeded;
            dgvPrincipal.RowsAdded -= dgvPrincipal_RowsAdded;
            this.HandleDestroyed -= ucAnotacion_HandleDestroyed;
        }

        private void dgvPrincipal_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            double cf = 0;
            int val = 0;
            if (dgvPrincipal.RowCount > 1)
                val = (int)(from DataGridViewRow row in dgvPrincipal.Rows
                            where row.Cells["ID"].Value != null && row.Index < dgvPrincipal.RowCount && double.TryParse(row.Cells["ID"].Value.ToString(), out cf)
                            select cf).Max();
            else val = ID;
            dgvPrincipal[0, e.RowIndex].Value = true;
            dgvPrincipal[10, e.RowIndex].Value = " X ";
            dgvPrincipal["ID", e.RowIndex].Value = val + 1;
        }
        int id = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                bool unavez = true;
                foreach (DataGridViewRow row in RowEdit)
                {
                    string tabla = row.Cells[3].Value.ToString();
                    string nombre = row.Cells[4].Value.ToString();
                    string LAT = row.Cells[5].Value.ToString();
                    string LON = row.Cells[6].Value.ToString();
                    string Variable = row.Cells[7].Value.ToString();
                    double grosor = double.Parse(row.Cells[9].Value.ToString());
                    int _Id = int.Parse(row.Cells["ID"].Value.ToString());
                    var color = row.Cells[8].Style.BackColor;
                    double x = 0, y = 0, Radio = 0;
                    string valor1 = row.Cells[0].Value == null ? "false" : row.Cells[0].Value.ToString();
                    string valor2 = row.Cells[1].Value == null ? "false" : row.Cells[1].Value.ToString();
                    if (valor1 == "True")
                    {

                        DataTable dtAuz = dicPrincipal["BURBUJAS:" + tabla];
                        wbEarth.SuspendLayout();
                        Notificacion.Visible = true;
                        Notificacion.ShowBalloonTip(290000);

                        var colorA = String.Format("{0:X2}{1:X2}{2:X2}{3:X2}", color.A, color.B, color.G, color.R);
                        foreach (DataRow row1 in dtAuz.Rows)
                        {
                            if (double.TryParse(row1[LAT].ToString(), out x) && double.TryParse(row1[LON].ToString(), out y))
                            {
                                if (double.TryParse(row1[Variable].ToString(), out Radio) && Radio > 0)
                                {
                                    wbEarth.Document.InvokeScript("CreaCirculo", new object[] { row1[nombre].ToString() + Variable + "_" + _Id.ToString(), colorA, x, y, Radio / 111.111, grosor, row1[nombre].ToString() + ":" + Radio.ToString() });
                                    if (valor2 == "True")
                                        wbEarth.Document.InvokeScript("Etiqueta", new object[] { row1[nombre].ToString() + "_" + _Id.ToString(), x, y });
                                    else
                                        wbEarth.Document.InvokeScript("EliminaMarcaCirculo", new object[] { row1[nombre].ToString() + "_" + _Id.ToString() });
                                }

                            }
                        }
                        //if (valor2 == "false")
                        //    row.Cells["ID"].Value = ++_Id;
                        Notificacion.Visible = false;
                        cerrado = true;
                    }
                    else
                    {
                        eliminar(tabla, nombre, LAT, LON, Variable, _Id);
                        row.Cells["ID"].Value = ++_Id;
                    }
                    wbEarth.ResumeLayout();
                }
            }
            catch
            { }
        }

        private void dgvPrincipal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0: //Visible
                    break;

                case 1: break;
                case 2: break;
                case 3:
                    filtroTablas.EnForm(false);
                    if (filtroTablas.cerrado) 
                        return;
                    dgvPrincipal[e.ColumnIndex, e.RowIndex].Value = filtroTablas.Seleccionado.ToString();

                    dtPrincipal = dicPrincipal["BURBUJAS:"+filtroTablas.Seleccionado.ToString()];
                    List<ObjetoSelecionable> ji = (from DataColumn col in dtPrincipal.Columns select (new ObjetoSelecionable(col.Caption))).ToList();
                    filtroColumnas = new userFiltroElemento(ref ji, 1);
                break;

                case 8:
                    DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1 = (sender as DevComponents.DotNetBar.Controls.DataGridViewX);
                    
                        ColorDialog Colornuevo = new ColorDialog();
                        if (Colornuevo.ShowDialog() == DialogResult.OK)
                        {
                            var color = String.Format("{0:X2}{1:X2}{2:X2}{3:X2}", Colornuevo.Color.A, Colornuevo.Color.B, Colornuevo.Color.G, Colornuevo.Color.R);
                            dgvPrincipal[e.ColumnIndex, e.RowIndex].Style.BackColor = Colornuevo.Color;
                            dgvPrincipal[e.ColumnIndex, e.RowIndex].Tag = Colornuevo.Color.Name;
                            dgvPrincipal[e.ColumnIndex, e.RowIndex].Value = "";
                            dgvPrincipal[e.ColumnIndex, e.RowIndex].Selected = false;
                        }
                    
                    break;
                case 9: 
                    break;
                case 10: //eliminar
                    string nombre_ = dgvPrincipal[4, e.RowIndex].Value.ToString();
                    string tabla =dgvPrincipal[3, e.RowIndex].Value.ToString();
                    string LAT = dgvPrincipal[5, e.RowIndex].Value.ToString();
                    string LON = dgvPrincipal[6, e.RowIndex].Value.ToString();
                    string VAR = dgvPrincipal[7, e.RowIndex].Value.ToString();
                    int id_ = int.Parse(dgvPrincipal[2, e.RowIndex].Value.ToString());
                    eliminar(tabla, nombre_, LAT, LON, VAR, id_);
                    //dicPrincipal.Remove("BURBUJAS:" + tabla);
                    RowEdit.Remove(dgvPrincipal.Rows[e.RowIndex]);
                    return;

                default:
                    filtroColumnas.EnForm(false);
                    if (filtroColumnas.cerrado)
                    {
                        filtroColumnas.cerrado = false;
                        return;
                    }
                    string seleccionado = filtroColumnas.Seleccionado.ToString();
                    dgvPrincipal[e.ColumnIndex, e.RowIndex].Value = seleccionado;
                    break;
            }
            if (!RowEdit.Contains(dgvPrincipal.Rows[e.RowIndex]))
                RowEdit.Add(dgvPrincipal.Rows[e.RowIndex]);
        }

        int ID = 0;
        private void dgvPrincipal_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["Visible"].Value = true;
            double cf = 0;
            int val = 0;
            if (dgvPrincipal.RowCount > 1)
                val = (int)(from DataGridViewRow row in dgvPrincipal.Rows
                            where row.Cells["ID"].Value != null && row.Index < dgvPrincipal.RowCount && double.TryParse(row.Cells["ID"].Value.ToString(), out cf)
                            select cf).Max();
            else val = ID;
            e.Row.Cells["ID"].Value = val + 1;
            e.Row.Cells["Eliminar"].Value = " X ";
        }

        private void eliminar(string tabla, string Titulo, string Lat, string Lon, string Variable, int id)
        {
            //wbEarth.SuspendLayout();
            DataTable dtAuz = dicPrincipal["BURBUJAS:" + tabla];
            string titulo = Titulo;

            double x = 0, y = 0, Radio = 0;
            string ColX = Lat, ColY = Lon, variable = Variable;
            foreach (DataRow row in dtAuz.Rows)
            {
                if (double.TryParse(row[ColX].ToString(), out x) && double.TryParse(row[ColY].ToString(), out y))
                {
                    if (double.TryParse(row[variable].ToString(), out Radio))
                    {
                        wbEarth.Document.InvokeScript("EliminaMarcaCirculo", new object[] { row[titulo].ToString() + Variable + "_" + id.ToString() });
                        wbEarth.Document.InvokeScript("EliminaMarcaCirculo", new object[] { row[titulo].ToString() + "_" + id.ToString() });
                    }
                }
            }
            //wbEarth.ResumeLayout();
        }

        public NotifyIcon Notificacion { get; set; }
    }
}
