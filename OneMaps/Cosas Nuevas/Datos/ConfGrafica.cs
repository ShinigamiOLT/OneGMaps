using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gigasoft.ProEssentials.Enums;

namespace Maps
{
    public partial class ConfGrafica : Form
    {
        private DataTable Lista_Datos = new DataTable();
        private List<Core_Clasificador.ObjetoTablas> ListaFinal;
        bool estado_activo = true;
        int Clave = 0; // calve 0 barra , riesgo 1 
        Tablaescala[] escalas;

        public ConfGrafica(List<Core_Clasificador.ObjetoTablas> ListaFinal, int Clave_)
        {
            Clave = Clave_;
            // TODO: Complete member initialization
            this.ListaFinal = ListaFinal;
            InitializeComponent();
            if (ListaFinal.Count > 0)
            {
                Core_Clasificador.ObjetoTablas obj = ListaFinal[0];

                foreach (DataColumn col in obj.Tabla_unidad.Columns)
                {
                    cmbVariable.Items.Add(col.ColumnName);
                    cmbVarX.Items.Add(col.ColumnName);
                    cmbVarY.Items.Add(col.ColumnName);

                }
                SoloreglonesGrupo();
                string[] Operaciones_Permitidas = new string[] { "SUM", "CONT", "PRO", "MIN", "MAX", "DESV" };
                cmbOperacion.Items.AddRange(Operaciones_Permitidas);
                CmbOpeX.Items.AddRange(Operaciones_Permitidas);
                CmbOpeY.Items.AddRange(Operaciones_Permitidas);
            }

        }
        void SoloreglonesGrupo()
        {

            if (ListaFinal.Count > 0)
            {

                GeneraColores genera = new GeneraColores();

                foreach (Core_Clasificador.ObjetoTablas obj in ListaFinal)
                {

                    try
                    {  //  obj.imprimeValores(dgvSumatoria);
                        dataGridView1.Rows.Add(true, obj.Cadena, "");

                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Tag = obj;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Style.BackColor = genera.ColoresR();
                    }
                    catch { }
                }
                CambiaTamanio(dataGridView1, 15);

            }


        }
        void CambiaTamanio(DataGridView dgv, int max)
        {
            try
            {
                //if (dgv.Rows.Count > 0)
                {

                    int Largo = (dgv.RowCount + 2) * (dgv.RowTemplate.Height) + 5;
                    if (dgv.RowCount >= max)
                    {
                        Largo = (max + 1) * (dgv.RowTemplate.Height) + 5;
                    }
                    dgv.Size = new Size(dgv.Size.Width, Largo);
                    explorerBarGroupItem1.Refresh();
                    explorerBarGroupItem1.RecalcSize();

                    explorerBar1.Update();
                    explorerBar1.Refresh();

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ConfGrafica_Load(object sender, EventArgs e)
        {

            escalas = new Tablaescala[2];
            escalas[0].Auto = escalas[1].Auto = true;
            
            
            if (Clave == 0)
            {
                Pego1.Visible = _control_barra.Visible = true;
                Pesgo1.Visible = _contro_ovi.Visible = false;
                EventoPego pes = new EventoPego(Pego1, new NotifyIcon());
                Pego1.Dock = DockStyle.Fill;
            }
            else
            {
                Pego1.Visible = _control_barra.Visible = false;

                Pesgo1.Visible = _contro_ovi.Visible = true;

                EventoPesgo pesgo = new EventoPesgo(Pesgo1, new NotifyIcon());
                Pesgo1.Dock = DockStyle.Fill;

            }



            CambiaTamanio(dataGridView1, 5);

            explorerBar1.Refresh();
            explorerBarGroupItem1.Refresh();

        }

        private void cmdGraficar_Click(object sender, EventArgs e)
        {
            if (Clave == 0)
            {
                if (cmbVariable.SelectedItem != null && cmbOperacion.SelectedItem != null)
                {
                    Lista_Datos.Columns.Clear();
                    Lista_Datos.Columns.Add(cmbOperacion.Text, Type.GetType("System.String"));
                    Lista_Datos.Columns.Add("Datos", Type.GetType("System.Double"));
                    Lista_Datos.Clear();

                    DataRow Reglon;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {


                        Core_Clasificador.ObjetoTablas obj = (Core_Clasificador.ObjetoTablas)row.Cells[1].Tag;
                        bool estado = (bool)row.Cells[0].Value;
                        if (estado)
                        {
                            try
                            {
                                DataTable Tablas_Resumen = obj.Tabla_ValoresCalculo;
                                for (int i = 0; i < Tablas_Resumen.Rows.Count; i++)
                                {
                                    if (Tablas_Resumen.Rows[i]["Operacion_"].ToString().Contains(cmbOperacion.Text))
                                    {
                                        Reglon = Lista_Datos.NewRow();
                                        Reglon[0] = obj.Cadena;
                                        Reglon[1] = Tablas_Resumen.Rows[i][cmbVariable.Text].ToString();
                                        Lista_Datos.Rows.Add(Reglon);
                                    }
                                }
                            }
                            catch { }
                        }

                    }

                    //foreach (Core_Clasificador.ObjetoTablas obj in ListaFinal)
                    //{
                    //    DataTable Tablas_Resumen = obj.Tabla_ValoresCalculo;
                    //    for (int i = 0; i < Tablas_Resumen.Rows.Count - 1; i++)
                    //    {
                    //        if (Tablas_Resumen.Rows[i]["Operacion_"].ToString().Contains(cmbOperacion.Text))
                    //        {
                    //            Reglon = Lista_Datos.NewRow();
                    //            Reglon[0] = cmbOperacion.Text + "_" + obj.Cadena;
                    //            Reglon[1] = Tablas_Resumen.Rows[i][cmbVariable.Text].ToString();
                    //            Lista_Datos.Rows.Add(Reglon);
                    //        }
                    //    }
                    //}
                    //despues de haber obtenido los datos.
                    if (rbacendente.Checked)

                        Lista_Datos.DefaultView.Sort = "Datos ASC";
                    if (rbdecendente.Checked)
                        Lista_Datos.DefaultView.Sort = "Datos DESC";
                    Lista_Datos = Lista_Datos.DefaultView.ToTable();
                    //grafica l = new grafica(dt);
                    //l.ShowDialog();

                    Char_Pego Ploter = new Char_Pego(Pego1);
                    Ploter.Series = 1;
                    Ploter.MaximoPuntos = Lista_Datos.Rows.Count;
                    Ploter.Simbolos = false;
                    if (Lista_Datos.Rows.Count == 0)
                    {
                        Pego1.Visible = false;
                        return;
                    }
                    Pego1.Visible = true;
                    if (Lista_Datos.Rows.Count > 1)
                    {
                        Ploter.Colores.Clear();
                        //   Ploter.CargaPorDefault();
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {


                            bool estado = (bool)row.Cells[0].Value;
                            if (estado)
                            {
                                try
                                {
                                    Ploter.Colores.Add(row.Cells[2].Style.BackColor);

                                }
                                catch { }
                            }

                        }
                        //  Ploter.Colores.Clear();
                        //  Ploter.CargaPorDefault();
                    }
                    else
                    {
                        Ploter.Colores.Clear();
                        Ploter.CargaPorDefault();
                    }

                    Ploter.Factor = 1;
                    Ploter.TamPunto = PointSize.Large;
                    Ploter.Plot_X_Serie_Mismo_Eje(Lista_Datos, "", 0, 1, 0);
                    Pego1.PeString.YAxisLabel = Lista_Datos.Columns[1].ColumnName;

                    //Pego1.PePlot.AutoStatSubsets[0] = (int)Gigasoft.ProEssentials.Enums.AutoStatSubsets.AveragePerPoint;
                    //Pego1.PePlot.AutoStatSubsets[1] = (int)Gigasoft.ProEssentials.Enums.AutoStatSubsets.MaxPerPoint;

                    Pego1.PeLegend.Style = LegendStyle.OneLineInsideAxis;

                    if (rbEjeX.Checked)
                        Pego1.PePlot.Method = GraphPlottingMethod.Bar;
                    if (rbEjeY.Checked)
                    {
                        Pego1.PePlot.Method = GraphPlottingMethod.HorizontalStackedBar;
                        Pego1.PePlot.MarkDataPoints = true;
                        //Pego1.PePlot.Method = GraphPlottingMethod.HorizontalStackedBar;
                        //Pego1.PeLegend.Style = LegendStyle.OneLine;
                        //Pego1.PeLegend.Location = LegendLocation.Top;
                        //Pego1.PeConfigure.ImageAdjustBottom = 50;
                        //Pego1.PeString.YAxisLabel = " ";
                        //Pego1.PeColor.QuickStyle = QuickStyle.LightLine;
                        //Pego1.PePlot.DataShadows = DataShadows.ThreeDimensional;

                        // Control which subsets to show //
                        // Value [1 - 9] show subset, subsets with a value of 9 plot before values of 1
                        // Value [0] hides subset
                        //      Pego1.PeData.SubsetsToShow[0] = 1;
                    }

                    Pego1.PePlot.Allow.StackedData = true;
                    Ploter.configurar_hoja2();
                    Pego1.PePlot.DataShadows = DataShadows.None;
                    Ploter.Nombre = "";
                    Ploter.Subtitulo = " ";

                    Ploter.PlotPesgo.PeString.XAxisLabel = "";

                    Ploter.PlotPesgo.PeString.YAxisLabel = cmbVariable.SelectedItem.ToString() + " " + cmbOperacion.SelectedItem.ToString();
                    // Generally call ReinitializeResetImage at end **'
                    Pego1.PeFunction.ReinitializeResetImage();
                    Pego1.Refresh();
                }
            }

            else
            {
                //aqui es para la clave 1
                //comenzemos por el proceso de validacion



                List<DataTable> dtSeries = new List<DataTable>();

                if (cmbVarX.SelectedItem != null && CmbOpeX.SelectedItem != null && cmbVarY.SelectedItem != null && CmbOpeY.SelectedItem != null)
                {
                    //aqui ya tenemos las variables;

                    string VarX = cmbVarX.SelectedItem.ToString();
                    string VarY = cmbVarY.SelectedItem.ToString();

                    string OpenX = CmbOpeX.SelectedItem.ToString();
                    string OpenY = CmbOpeY.SelectedItem.ToString();
                    Lista_Datos.Columns.Clear();
                    Lista_Datos.Columns.Add(VarX + OpenX + "X", Type.GetType("System.Double"));
                    Lista_Datos.Columns.Add(VarY + OpenY + "Y", Type.GetType("System.Double"));
                    Lista_Datos.Columns.Add("Nombre", Type.GetType("System.String"));

                    Lista_Datos.Clear();
                    //ahora saquemos los datos
                    DataRow Reglon;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {


                        Core_Clasificador.ObjetoTablas obj = (Core_Clasificador.ObjetoTablas)row.Cells[1].Tag;
                        bool estado = (bool)row.Cells[0].Value;
                        if (estado)
                        {
                            try
                            {
                                DataTable Tablas_Resumen = obj.Tabla_ValoresCalculo;
                                DataTable temporal = Lista_Datos.Clone();
                                Reglon = temporal.NewRow();
                                Reglon[2] = obj.Cadena;
                                for (int i = 0; i < Tablas_Resumen.Rows.Count; i++)
                                {

                                    if (Tablas_Resumen.Rows[i]["Operacion_"].ToString().Contains(OpenX))
                                    {


                                        Reglon[0] = Tablas_Resumen.Rows[i][VarX].ToString();

                                    }
                                    if (Tablas_Resumen.Rows[i]["Operacion_"].ToString().Contains(OpenY))
                                    {

                                        Reglon[1] = Tablas_Resumen.Rows[i][VarY].ToString();

                                    }



                                }
                                temporal.Rows.Add(Reglon);
                                dtSeries.Add(temporal);

                                Lista_Datos.Merge(temporal);
                            }
                            catch { }
                        }

                        //aqui viene el dibujo
                    }
                    if (dtSeries.Count > 0)
                    {

                        Char_Pro visualizador = new Char_Pro(Pesgo1);


                        visualizador.MaximoPuntos = 1;
                        visualizador.Series = dtSeries.Count;
                        int serie = 0;
                        visualizador.Factor = 1;
                        visualizador.TamPunto = PointSize.Small;
                       if(chMicro.Checked ) visualizador.TamPunto= PointSize.Micro;
                       if (chSmall.Checked) visualizador.TamPunto = PointSize.Small;
                       if (chMedio .Checked) visualizador.TamPunto = PointSize.Medium;
                        visualizador.CargaPorDefault();
                        visualizador.TipoGrafica = SGraphPlottingMethod.Point;
                        visualizador.TipoFechaX = false;
                        foreach (DataTable tabla in dtSeries)
                        {
                            visualizador.Plot_Serie_X_EjeV2(tabla, 0, 1, serie);
                            serie++;
                        }

                        Pesgo1.PeString.YAxisLabel = VarY ;

                        Pesgo1.PeString.XAxisLabel = VarX ;
                        //Pego1.PePlot.AutoStatSubsets[0] = (int)Gigasoft.ProEssentials.Enums.AutoStatSubsets.AveragePerPoint;
                        //Pego1.PePlot.AutoStatSubsets[1] = (int)Gigasoft.ProEssentials.Enums.AutoStatSubsets.MaxPerPoint;

                        Pesgo1.PeLegend.Style = LegendStyle.OneLineInsideAxis;
                        
                        visualizador.configurar_hoja2();
                        visualizador.PlotPesgo.PeConfigure.BorderTypes = TABorder.SingleLine;
                        visualizador.Nombre = "";
                        visualizador.Subtitulo = "";
                        if (visualizador.PlotPesgo.Tag is DataTable)
                            ((DataTable)visualizador.PlotPesgo.Tag).Dispose();

                        visualizador.PlotPesgo.Tag = Lista_Datos;

                        visualizador.PlotPesgo.Refresh();
                       
                    }


                }
                else
                {

                    MessageBox.Show("Faltan uno o mas parametros!!!");
                }

            }
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                row.Cells[0].Value = !estado_activo;
            }
            estado_activo = !estado_activo;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                ColorDialog Colornuevo = new ColorDialog();
                if (Colornuevo.ShowDialog() == DialogResult.OK)
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Colornuevo.Color;

                }
            }
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            

        }


    }
}
