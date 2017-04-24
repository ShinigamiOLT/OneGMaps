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
    public partial class ucAnotacion : UserControl
    {
        public List<DataGridViewRow> RowEdit;
        userFiltroElemento filtroTablas;
        userFiltroElemento filtroColumnas;
        Dictionary<string, DataTable> dicPrincipal;
        DataTable dtPrincipal;
        WebBrowser wbEarth;
        cDiccionarios Metadatos;
        DevComponents.DotNetBar.Controls.DataGridViewX dgvPrincipal;
        public bool cerrado = false;
        public NotifyIcon Notificacion { get; set; }

        public ucAnotacion(Dictionary<string, DataTable> dicPri, DevComponents.DotNetBar.Controls.DataGridViewX Anota, WebBrowser web, NotifyIcon noti, cDiccionarios DicDatos, List<DataGridViewRow> Rowedit)
        {
            InitializeComponent();
            Metadatos = DicDatos;
            RowEdit = Rowedit;
            dicPrincipal = dicPri;

            dgvPrincipal = Anota;

            wbEarth = web;
            Notificacion = noti;

            dgvPrincipal.Dock = DockStyle.Fill;
            splitContainer1.Panel1.Controls.Add(dgvPrincipal);
            RowEdit.Clear();
            List<ObjetoSelecionable> ji = (from x in dicPrincipal.Keys where x.Contains("ANOTACIONES") select new ObjetoSelecionable(x.ToString().Split(':')[1])).ToList();
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
            dgvPrincipal[0, e.RowIndex].Value = true;
            dgvPrincipal[6, e.RowIndex].Value = " X ";
        }

        private void dgvPrincipal_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[0].Value = true;
            e.Row.Cells[6].Value = " X ";
        }

        private void dgvPrincipal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                filtroTablas.Actualiza((from x in dicPrincipal.Keys where x.Contains("ANOTACIONES") select new ObjetoSelecionable(x.ToString().Split(':')[1])).ToList(), 1);
                switch (e.ColumnIndex)
                {
                    case 0: //Visible
                        break;
                    case 1:
                        filtroTablas.EnForm(false);
                        if (filtroTablas.cerrado)
                        {
                            filtroColumnas.cerrado = false;
                            return;
                        }
                        dgvPrincipal[e.ColumnIndex, e.RowIndex].Value = filtroTablas.Seleccionado.ToString();

                        dtPrincipal = dicPrincipal["ANOTACIONES:" + filtroTablas.Seleccionado.ToString()];
                        List<ObjetoSelecionable> ji = (from DataColumn col in dtPrincipal.Columns select (new ObjetoSelecionable(col.Caption))).ToList();
                        filtroColumnas = new userFiltroElemento(ref ji, 1);
                        break;

                    case 2:
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
                    case 5: break;
                    case 6:
                        string Nom = dgvPrincipal[1, e.RowIndex].Value.ToString();
                        dgvPrincipal.Rows.RemoveAt(e.RowIndex);
                        wbEarth.Document.InvokeScript("RemoveLineString", new object[] { Nom });
                        RowEdit.Remove(dgvPrincipal.Rows[e.RowIndex]);
                        break;

                    default: 
                        if (filtroColumnas == null)
                        {
                            dtPrincipal = dicPrincipal["ANOTACIONES:" +dgvPrincipal[1,e.RowIndex].Value.ToString()];
                            List<ObjetoSelecionable> jis = (from DataColumn col in dtPrincipal.Columns select (new ObjetoSelecionable(col.Caption))).ToList();
                            filtroColumnas = new userFiltroElemento(ref jis, 1);
                        }
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
            catch
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in RowEdit)
                {
                    var color = row.Cells[2].Style.BackColor;
                    var colorA = String.Format("{0:X2}{1:X2}{2:X2}{3:X2}", color.A, color.B, color.G, color.R);
                    Notificacion.Visible = true;
                    Notificacion.ShowBalloonTip(290000);
                    string tabla = row.Cells[1].Value.ToString();
                    DataTable TABLA = dicPrincipal["ANOTACIONES:" + tabla];
                    string Nom = TABLA.TableName;
                    string alterno = "ANOTACION:" + Nom;
                    string LAT = row.Cells[3].Value.ToString();
                    string LON = row.Cells[4].Value.ToString();
                    string Zona = row.Cells[5].Value != null ? row.Cells[5].Value.ToString().ToUpper() : "";
                    if (row.Cells[0].Value.ToString() == "True")
                    {
                        Conversor cv = new Conversor();
                        object[] ContendorPrincipal = new object[7];
                        double x = 0, y = 0, Conve = 0;
                        bool nopase = true;
                        List<object> op1 = new List<object>();
                        List<object> op2 = new List<object>();
                        wbEarth.Document.InvokeScript("RemoveLineString", new object[] { Nom });
                        for (int i = 0; i < TABLA.Rows.Count; i++)
                        {
                            if (double.TryParse(TABLA.Rows[i][LAT].ToString(), out Conve) && double.TryParse(TABLA.Rows[i][LON].ToString(), out Conve))
                            {
                                if (Zona != "")
                                    cv.ToLatLon(double.Parse(TABLA.Rows[i][LAT].ToString()), double.Parse(TABLA.Rows[i][LON].ToString()), Zona, out x, out y);
                                else
                                {
                                    double.TryParse(TABLA.Rows[i][LAT].ToString(), out x);
                                    double.TryParse(TABLA.Rows[i][LON].ToString(), out y);
                                }
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
                                    ContendorPrincipal[4] = colorA;
                                    ContendorPrincipal[5] = 2;
                                    ContendorPrincipal[6] = "";
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
                            ContendorPrincipal[4] = colorA;
                            ContendorPrincipal[5] = 2;
                            ContendorPrincipal[6] = "";
                            wbEarth.Document.InvokeScript("CreaLinea", ContendorPrincipal);
                        }
                        wbEarth.Document.InvokeScript("Enfoca", new object[] { x, y });
                        wbEarth.Document.InvokeScript("AgregaLineaContenedor");

                        Notificacion.Visible = false;
                        //cerrado = true;
                        //(this.Parent as Form).Close();
                    }
                    else
                    {
                        wbEarth.Document.InvokeScript("RemoveLineString", new object[] { Nom });
                    }

                }

            }
            catch { }
        }
    }
        
}
