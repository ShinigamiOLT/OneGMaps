using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevComponents.DotNetBar;

namespace Maps
{
    public partial class CalificaTabla : Form
    {
        List<DataTable> listaTablas;
        public CalificaTabla(List<DataTable> Lista_tablas)
        {
            listaTablas = Lista_tablas;
            InitializeComponent();
        }

        DataTable dtAuxiliar;
        public CalificaTabla(DataTable dt, List<DataTable> Lista_tablas)
        {
            listaTablas = Lista_tablas;
            //dtAuxiliar = dt;
            InitializeComponent();
            label2.Visible = true;
            label2.Text = "TABLA: " + dt.TableName;
            label4.Text += ": " + dt.TableName.ToUpper();
            foreach (DataColumn dato in dt.Columns)
                cmbIzquierda.Items.Add(dato.Caption);
        }

        public CalificaTabla(List<DataTable> dgvList, CoreCalifica vamos)
        {
        }


        List<string> lTabla;
        DataTable dtYUP;
        public CalificaTabla(DataTable dt, List<string> lTabla, List<DataTable> Lista_tablas, CoreCalifica NucleoCalificador)
        {
            InitializeComponent();
            dgv_origen.DataSource = dt;
            this.NucleoCalificador = NucleoCalificador;
            this.lTabla = lTabla;
            listaTablas = Lista_tablas;
            //dtAuxiliar = dt;

            label2.Visible = true;
            label2.Text = "TABLA: " + dt.TableName;
            label4.Text += ": " + dt.TableName.ToUpper();
            label5.Text += dt.TableName;
            foreach (DataColumn dato in dt.Columns)
                cmbIzquierda.Items.Add(dato.Caption);
        }

        private void Comparativa_de_tabla_Load(object sender, EventArgs e)
        {
            string nombre = (dgv_origen.DataSource as DataTable).TableName;
            listObj = new List<ObjetoSelecionable>();
            for (int i = 0; i < listaTablas.Count; i++)
            {
                if (listaTablas[i].TableName != nombre)
                    cbB.Items.Add(listaTablas[i].TableName);
            }
            panel4.Invalidate();
        }

        private void labelItem7_Click(object sender, EventArgs e)
        {

        }



        private void cbB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtSeleccion = listaTablas[listaTablas.FindIndex(x => x.TableName == cbB.SelectedItem.ToString())].Copy();
                var listaA = (from DataColumn col in dtSeleccion.Columns
                              select col.Caption).ToList();
                cmbDerecha.DataSource = listaA;
                label1.Text += dtSeleccion;
            }
            catch
            {
            }
        }

        bool yaPase = false;
        List<string> listaSeleccionA;
        private void cmbIzquierda_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        Dictionary<string, DataRow> Diccionario;
        List<ObjetoSelecionable> listObj;
        userFiltroElemento selec;
        private void cmdcargaD_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreTabla = cbB.SelectedItem.ToString();
                DataTable dt = NucleoCalificador.ListaTablas[NucleoCalificador.ListaTablas.FindIndex(x => x.TableName == nombreTabla)];
                Diccionario = new Dictionary<string, DataRow>();
                listObj = new List<ObjetoSelecionable>();

                listaSeleccionA = listaSeleccionA.Distinct().ToList();

                List<string> listaRows = (from DataRow row1 in dt.Rows
                                          select row1[cmbDerecha.Items[cmbDerecha.SelectedIndex].ToString()].ToString()).ToList();
                foreach (string cad in listaSeleccionA)
                {
                    try
                    {
                        int numBus = listaRows.FindIndex(m => m.Equals(cad));
                        DataRow rowQ = dt.Rows[numBus];
                        Diccionario.Add(cad, rowQ);
                    }
                    catch
                    {
                        continue;
                    }
                }

            }
            catch
            {
            }
        }

        List<string> LReentradas;


        public DataTable dtNuevo;
        private List<DataTable> dgvList;
        private CoreCalifica vamos;
        private void CalificaTabla_FormClosing(object sender, FormClosingEventArgs e)
        {
        }


        void funcionPrincipal()
        {
            if (Diccionario.Keys.Count > 1)
            {
                LReentradas = new List<string>();

                LReentradas.Add("-R1");
                LReentradas.Add("-R2");
                LReentradas.Add("-R3");
                LReentradas.Add(" R1");
                LReentradas.Add(" R2");
                LReentradas.Add(" R3");
                LReentradas.Add("R1");
                LReentradas.Add("R2");
                LReentradas.Add("R3");
                LReentradas.Add("re");
                LReentradas.Add("Re");
                LReentradas.Add("RE");

                try
                {
                    DataTable dtAuxB = ((DataTable)dgv_origen.DataSource);

                    var ty = selec.ListaPozosTotales.FindAll(x => x.Estado == true).ToList();
                    if (ty.Count > 0)
                    {

                        int valI = dtAuxB.Columns.Count;
                        string[] cadena = new string[valI];

                        for (int i = 0; i < ty.Count; i++)
                            if (!dtAuxB.Columns.Contains(ty[i].ToString()))
                                dtAuxB.Columns.Add(ty[i].ToString());

                        bool quiteRee;
                        string cualQuite = "";
                        Notificacion.ShowBalloonTip(2900000);
                        for (int i = 0; i < dtAuxB.Rows.Count; i++)
                        {
                            string val = dtAuxB.Rows[i][cmbIzquierda.Items[cmbIzquierda.SelectedIndex].ToString()].ToString();

                            for (int u = 0; u < LReentradas.Count; u++)
                            {
                                if (val.Contains(LReentradas[u]))
                                {
                                    val = val.Replace(LReentradas[u], "");
                                    quiteRee = true;
                                    cualQuite = LReentradas[u];
                                    break;
                                }
                                else quiteRee = false;
                            }

                            if (Diccionario.Keys.Contains(val))
                            {
                                for (int j = valI, k = 0; k < ty.Count; j++, k++)
                                {
                                    var tyu = Diccionario[val][ty[k].ToString()];
                                    dtAuxB.Rows[i][j] = Diccionario[val][ty[k].ToString()];
                                }
                            }
                        }
                        dgv_origen.DataSource = dtAuxB;
                        dtNuevo = dtAuxB;
                        Notificacion.Visible = false;
                    }
                }
                catch { }
            }
            else MessageBox.Show("Sin cocidencias");
        }




        FolderBrowserDialog SelecionaCarpeta;
        Constantes_Conf config = new Constantes_Conf();
        private DataTable dtConf;
        private CoreCalifica NucleoCalificador;

        private void buttonItem1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Notificacion.Visible = true;
                Notificacion.ShowBalloonTip(2900000);
                listaSeleccionA = new List<string>();
                var quesesto = cmbIzquierda.Items[cmbIzquierda.SelectedIndex].ToString();

                listaSeleccionA = (from DataRow row in (dgv_origen.DataSource as DataTable).Rows
                                   select row[cmbIzquierda.Items[cmbIzquierda.SelectedIndex].ToString()].ToString()).Distinct().ToList();

                DataTable nuevoDT = (DataTable)dgv_origen.DataSource;
                //DataTable dtAuxB = (DataTable)dgv_origen.DataSource;

                string nombreTabla = cbB.SelectedItem.ToString();
                DataTable dt = NucleoCalificador.ListaTablas[NucleoCalificador.ListaTablas.FindIndex(x => x.TableName == nombreTabla)];
                Diccionario = new Dictionary<string, DataRow>();
                listObj = new List<ObjetoSelecionable>();

                //listaSeleccionA = listaSeleccionA.Distinct().ToList();
                var queestootro = cmbDerecha.Items[cmbDerecha.SelectedIndex].ToString();

                List<string> listaRows = (from DataRow row1 in dt.Rows
                                          select row1[cmbDerecha.Items[cmbDerecha.SelectedIndex].ToString()].ToString()).ToList();

                Notificacion.ShowBalloonTip(2900000);
                foreach (string cad in listaSeleccionA)
                {
                    try
                    {
                        int numBus = listaRows.FindIndex(m => m.Equals(cad));
                        DataRow rowQ = dt.Rows[numBus];
                        Diccionario.Add(cad, rowQ);
                    }
                    catch
                    {
                        continue;
                    }
                }
                Notificacion.ShowBalloonTip(2900000);
                funcionPrincipal();
            }
            catch
            {
            }
        }

        private void btnSeleccion_Click(object sender, EventArgs e)
        {
            DataTable dtSeleccion = listaTablas[listaTablas.FindIndex(x => x.TableName == cbB.SelectedItem.ToString())];
            listObj.Clear();
            for (int i = 0; i < dtSeleccion.Columns.Count; i++)
            {
                listObj.Add(new ObjetoSelecionable(dtSeleccion.Columns[i].Caption));
            }

            selec = new userFiltroElemento(ref listObj);
            selec.EnForm(true);
        }

    }

    
}
