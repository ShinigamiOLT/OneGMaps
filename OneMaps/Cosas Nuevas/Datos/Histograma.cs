using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gigasoft.ProEssentials.Enums;
using Onestatistics;

namespace Maps
{
    public partial class Histograma : Form
    {

        private List<Core_Clasificador.ObjetoTablas> ListaFinal;
        List<Color> ColoresDisponibles;
        int indexColor = 0;
        Color colorgrafica;
        public Control Primario_eliminar;
        NotifyIcon Notificacion;
        public Histograma()
        {
            InitializeComponent();
        }
        void CreaColores()
        {
            ColoresDisponibles = new List<Color>();
            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                Color temp = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));

                while (ColoresDisponibles.Contains(temp))
                {
                    temp = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
                }
                ColoresDisponibles.Add(temp);
            }
        }

        public Histograma(List<Core_Clasificador.ObjetoTablas> ListaFinal, NotifyIcon noti)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.ListaFinal = ListaFinal;
            Notificacion = noti;
            CreaColores();
        }



        Color Colores()
        {
            if (indexColor < 100)
                return ColoresDisponibles[indexColor++];

            indexColor = 0;
            return ColoresDisponibles[indexColor];
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

        private void cmdgera_Click(object sender, EventArgs e)
        {
          
        }

        private void Histograma_Load(object sender, EventArgs e)
        {

            //antes de pintar cargamos las columas en los combos para idexar
            if (ListaFinal.Count > 0)
            {
                Core_Clasificador.ObjetoTablas obj = ListaFinal[0];
                cmbVariable.Items.Clear();

                foreach (DataColumn col in obj.Tabla_unidad.Columns)
                {
                    cmbVariable.Items.Add(col.ColumnName);
                }
            }

            CreaColores();
            foreach (Core_Clasificador.ObjetoTablas obj in ListaFinal)
            {

                // obj.imprimeValores(dgvSumatoria);
                dgvControlClases.Rows.Add(true, obj.Cadena, "");


                dgvControlClases.Rows[dgvControlClases.Rows.Count - 1].Cells[1].Value = 8;
                dgvControlClases.Rows[dgvControlClases.Rows.Count - 1].Cells[1].Tag = Colores();
                dgvControlClases.Rows[dgvControlClases.Rows.Count - 1].Cells[2].Value = obj.Cadena;
                dgvControlClases.Rows[dgvControlClases.Rows.Count - 1].Cells[2].Tag = obj;


            }
            CambiaTamanio(dgvControlClases, 10);


            contiene_boton.Control = dgvGraficas_generadas;
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            estado_activo = !estado_activo;
            foreach (DataGridViewRow row in dgvControlClases.Rows)
            {

                row.Cells[0].Value = estado_activo;
            }
        }


        public bool estado_activo = true;

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //cuando se termina de editar volvemos a graficar
            if (e.ColumnIndex == 1)
            {
                int Valor = Convert.ToInt32(dgvControlClases[1, e.RowIndex].Value);
                //visualizar la marcada
                bool estado = (bool)dgvControlClases[0, e.RowIndex].Value;
                if (estado)
                {
                    Pinta_reglon(dgvControlClases.Rows[e.RowIndex], Valor);
                }
                  ActualizaTamanios();
            }
        }

        void Pinta_reglon(DataGridViewRow Reglon, int Valor)
        {

            try
            {
                if (cmbVariable.SelectedItem != null)
                {
                    Notificacion.ShowBalloonTip(500, "Informacion - " + Application.ProductName, "Espere... generando histograma para " + Valor.ToString() + " Clases", ToolTipIcon.Warning);

                    Core_Clasificador.ObjetoTablas obj = (Core_Clasificador.ObjetoTablas)Reglon.Cells[2].Tag;
                    colorgrafica = (Color)Reglon.Cells[1].Tag;
                    //aqui mandaremos a hacer las tablas de frecuencua
                    DescriptiveStatistics n = new DescriptiveStatistics();
                    n.Actualiza(obj.Tabla_unidad, cmbVariable.Text);

                   

                    Gigasoft.ProEssentials.Pesgo Nueva = InsertaGraficaEnTabladeFrecuencia(n,Valor);
                    ColocaSlider(Nueva);


                    Nueva.PeString.MainTitle = obj.Cadena;
                    Nueva.PeString.SubTitle = cmbVariable.Text;

                    DataGridViewRow reglon = new DataGridViewRow();
                    reglon.CreateCells(dgvGraficas_generadas);
                    reglon.Cells[0].Value = true;
                    reglon.Cells[1].Value = Nueva.PeString.MainTitle;

                    reglon.Cells[1].Tag = Nueva;
                    dgvGraficas_generadas.Rows.Add(reglon);
                    //aqui agrego la grafica.
                   

                }
                else
                {
                    MessageBox.Show("Debe de seleccionar una variable para realizar el histograma!!!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch { }



        }
        public void ColocaSlider(Gigasoft.ProEssentials.Pesgo Nueva)
        {
            Area_desplegado2.FlowDirection = FlowDirection.TopDown;
            if (Area_desplegado2.Controls.Count > 0)
                Area_desplegado2.FlowDirection = FlowDirection.BottomUp;
            Control papaC = Area_desplegado2.Parent;
            Area_desplegado2.Visible = false;
            //Nueva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left)
            //  Nueva.Size = new Size((int)(Split.Panel2.Size.Width * 0.98), (int)(Split.Panel2.Size.Height * 0.46));
            Point TamnioActual = new Point((int)(papaC.Size.Width * 0.98) - 5, (int)(papaC.Size.Height * 0.49));


            // panel2.Size = new Size(Split.Panel2.Size.Width, (int)(Split.Panel2.Size.Height * 0.46) * (Area_desplegado.RowCount+1));
            //antes de meter el ultimo creados los pondremos en una pila



            //Area_desplegado2.Size = new System.Drawing.Size(TamnioActual.X + 10, ((Area_desplegado2.Controls.Count+1) * TamnioActual.Y) + 10);


            // Area_desplegado.Size = new Size(Area_desplegado.Size.Width, Nueva.Size.Height * (Area_desplegado.RowCount + 1));
            Area_desplegado2.Controls.Add(Nueva);
            // Area_desplegado.Controls.Add(Nueva, 0, Area_desplegado.RowCount); 
            Nueva.Size = new Size(TamnioActual);
            Nueva.Anchor = (
                (System.Windows.Forms.AnchorStyles)
                (System.Windows.Forms.AnchorStyles.Right));//| System.Windows.Forms.AnchorStyles.Left)                 );



            Area_desplegado2.Refresh();
            Area_desplegado2.Visible = true;

            Area_desplegado2.ScrollControlIntoView(Nueva);

            Nueva.PeColor.Text = Color.Black;


        }

        void ActualizaTamanios()
        {

            Control papaC = Area_desplegado2.Parent;
            Area_desplegado2.Visible = false;
            // Point TamnioActual = new Point((int)(panel2.Size.Width * 0.98) - 15, (int)(panel2.Size.Height * 0.48));

            Point TamnioActual = new Point((int)(papaC.Size.Width * 0.98) - 5, (int)(papaC.Size.Height * 0.49));
            //  panel2.Size = new System.Drawing.Size(TamnioActual.X + 10, ((Area_desplegado.Controls.Count) * TamnioActual.Y) + 10);
            Area_desplegado2.FlowDirection = FlowDirection.TopDown;
            if (Area_desplegado2.Controls.Count > 1)
                Area_desplegado2.FlowDirection = FlowDirection.BottomUp;

            foreach (Control control1 in Area_desplegado2.Controls)
            {
                //control1.Dock = DockStyle.;
                control1.Size = new Size(TamnioActual);


            }
            Area_desplegado2.Visible = true;
            explorerBar1.Size = new System.Drawing.Size(245, 782);

            explorerBarGroupItem1.RecalcSize();
            explorerBar1.Refresh();
        }



        Gigasoft.ProEssentials.Pesgo InsertaGraficaEnTabladeFrecuencia(Onestatistics.DescriptiveStatistics Tabla,int val)
        {


            Gigasoft.ProEssentials.Pesgo Pego1 = new Gigasoft.ProEssentials.Pesgo();
            Ovidio(ConfigurandoHistograma(Tabla.CalculaTabladeFrecuencia2(val)), Pego1,Tabla.MediaAritmetica());

            Pego1.Tag = Tabla.CalculaTabladeFrecuencia2(val);
            EventoPesgo pes = new EventoPesgo(Pego1, new NotifyIcon());
            Pego1.Size = new Size(275, 195);

            Pego1.Dock = DockStyle.Fill;

            /*La parte de abajo va despues*/
            return Pego1;

        }
        private void Grafica_Usuario_Click(object sender, EventArgs e)
        {
            try
            {
                //  dgv_tabla_grafica.AutoGenerateColumns=false;
                Gigasoft.ProEssentials.Pesgo G = (Gigasoft.ProEssentials.Pesgo)sender;
                Visor_Datos visor = new Visor_Datos();

                visor.Text = G.PeString.MainTitle;
                visor.EstableceDatos((DataTable)G.Tag);
                visor.NoOrdenable();
                visor.EstableceFromato();
                visor.ModificaTamanio();
                visor.ShowDialog();



                //aqui 

            }
            catch { }
        }




        void Ovidio(List<DataTable> Lista_Datos, Gigasoft.ProEssentials.Pesgo Pego1, double Media)
        {
            {



                Maps.Char_Pro Ploter = new Char_Pro(Pego1);
                Ploter.Series = Lista_Datos.Count;
                Ploter.MaximoPuntos = 4;
                Ploter.Simbolos = false;

                Ploter.Colores.Clear();
                //   Ploter.Colores.AddRange(ColoresDisponibles);
                // Ploter.CargaPorDefault();
                if (colorgrafica != null)
                    Ploter.Colores.Add(colorgrafica);
                else
                    Ploter.Colores.Add(Color.Green);

                Ploter.Factor = 1;
                Ploter.TamPunto = PointSize.Large;

                Pego1.PePlot.Method = SGraphPlottingMethod.Area;
                for (int i = 0; i < Lista_Datos.Count; i++)
                {
                    Ploter.Plot_Serie_X_EjeV2(Lista_Datos[i], 0, 1, i);
                    Ploter.PlotPesgo.PeString.SubsetLabels[i] = (i + 1).ToString() + ".- " + Lista_Datos[i].TableName;
                    if (chAshurado.Checked)
                    {
                        Ploter.PlotPesgo.PePlot.SubsetHatch[i] = HatchType.DiagonalCross;
                        Ploter.PlotPesgo.PeColor.HatchBackColor = Color.White;
                    }
                }
                Pego1.PePlot.Method = SGraphPlottingMethod.Area;
                Ploter.PlotPesgo.PeString.YAxisLabel = "Frecuencia";
                Ploter.PlotPesgo.PeString.XAxisLabel = "Rango de Clases";
                {    //aqui va la media, la linea
                    Ploter.PlotPesgo.PeAnnotation.InFront = false;
                    Ploter.PlotPesgo.PeAnnotation.Show = true;
                    Ploter.PlotPesgo.PeAnnotation.Line.TextSize = 80;// Convert.ToInt32(txtFont.Text);


                    Ploter.PlotPesgo.PeFont.GraphAnnotationTextSize = 100;



                    // Give user ability to show or hide annotations //
                    Ploter.PlotPesgo.PeUserInterface.Menu.AnnotationControl = true;


                    Ploter.PlotPesgo.PeAnnotation.ShowAllTableAnnotations = true;

                    Ploter.PlotPesgo.PeAnnotation.Line.XAxis[0] = Math.Round( Media,3);//Tablas_A_Graficar[0].Rows[0][Xcol]).ToOADate();
                    Ploter.PlotPesgo.PeAnnotation.Line.XAxisType[0] = LineAnnotationType.ThinSolid;
                    Ploter.PlotPesgo.PeAnnotation.Line.XAxisColor[0] = Color.Black;//.FromArgb(198, 0, 198);                     

                    Ploter.PlotPesgo.PeAnnotation.Line.XAxisText[0] = "|t" + "Media: " + Math.Round(Media, 3).ToString();

                    Ploter.PlotPesgo.PeAnnotation.Line.XAxis[1] = Media;//Tablas_A_Graficar[0].Rows[0][Xcol]).ToOADate();
                    ////PlotPesgo.PeAnnotation.Line.XAxisType[ncon] = LineAnnotationType.ThinSolid;
                    Ploter.PlotPesgo.PeAnnotation.Line.XAxisColor[1] = Color.FromArgb(198, 0, 198);


                    Ploter.PlotPesgo.PeAnnotation.Line.XAxisText[1] = " ";


                }
                Ploter.Nombre = "Distribucion de Datos";
                Ploter.Subtitulo = " ";
                Pego1.PeLegend.Style = LegendStyle.OneLine;
                Pego1.PeLegend.Location = LegendLocation.Right;
                Pego1.PeColor.BitmapGradientMode = false;

                Pego1.PePlot.MarkDataPoints = false;
                Ploter.configurar_hoja2();
                Ploter.PlotPesgo.PeUserInterface.Menu.Help = MenuControl.Hide;

                Ploter.PlotPesgo.PeUserInterface.Menu.CustomMenuText[0] = "|";
                Ploter.PlotPesgo.PeUserInterface.Menu.CustomMenuText[1] = "1.- Ver Tabla de Datos";
                Ploter.PlotPesgo.PeUserInterface.Menu.CustomMenuText[2] = "|";

                // Generally call ReinitializeResetImage at end **'
                Pego1.PeFunction.ReinitializeResetImage();
                Pego1.Refresh();
            }
        }


        List<DataTable> ConfigurandoHistograma(DataTable Datos)
        {
            List<DataTable> Lista = new List<DataTable>();


            foreach (DataRow row in Datos.Rows)
            {
                DataTable Ejemplo = new DataTable(Math.Round(Convert.ToDouble(row[0]), 1).ToString() + " - " + Math.Round(Convert.ToDouble(row[1]), 1).ToString());
                Ejemplo.Columns.Add("X");
                Ejemplo.Columns.Add("Y");
                DataRow reglon;


                double[,] puntos = new double[,]{  {Convert.ToDouble(row[0]),0}, 
                                                  {Convert.ToDouble(row[0]),Convert.ToDouble(row[2]) },
                                                   {Convert.ToDouble(row[1]),Convert.ToDouble(row[2]) },
                                                    {Convert.ToDouble(row[1]),0 }
 
                
                };
                for (int i = 0; i < 4; i++)
                {
                    reglon = Ejemplo.NewRow();
                    reglon[0] = puntos[i, 0];
                    reglon[1] = puntos[i, 1];


                    Ejemplo.Rows.Add(reglon);
                }


                Lista.Add(Ejemplo.Copy());
            }

            return Lista;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //si el click es el la 4 boton

            if (e.ColumnIndex == 3)
            {
                int Valor = Convert.ToInt32(dgvControlClases[1, e.RowIndex].Value);
                //visualizar la marcada
                bool estado = (bool)dgvControlClases[0, e.RowIndex].Value;
                if (estado)
                {
                    Core_Clasificador.ObjetoTablas obj = (Core_Clasificador.ObjetoTablas)dgvControlClases[2, e.RowIndex].Tag;
                    try
                    {

                        //aqui mandaremos a hacer las tablas de frecuencua
                        DescriptiveStatistics n = new DescriptiveStatistics();
                        n.Actualiza(obj.Tabla_unidad, cmbVariable.Text);

                        Master = new Frecuencias("Ejemplo", n.CalculaTabladeFrecuencia2(Valor), Valor);

                        explorerBarGroupItem1.Visible = false;
                        explorerBar1.Refresh();

                    }
                    catch { }
                }
            }
        }
        Onestatistics.Frecuencias Master;

        private void cmdEliminaG_Click(object sender, EventArgs e)
        {
            BorarTablasGeneradas();
        }


        void BorarTablasGeneradas()
        {

            for (int i = dgvGraficas_generadas.Rows.Count - 1; i > -1; i--)
            {

                Primario_eliminar = dgvGraficas_generadas[1, i].Tag as Control;
                panel2.ScrollControlIntoView(Primario_eliminar);
                Primario_eliminar.BackColor = Color.Gray;



                //una vez que tengo el control elimino la grafica del area a eliminar
                Area_desplegado2.Controls.Remove(Primario_eliminar);
                //   Area_desplegado.Size = new Size(Area_desplegado.Size.Width, Primario_eliminar.Size.Height * (Area_desplegado.Controls.Count));
                Primario_eliminar.Dispose();


                dgvGraficas_generadas.Rows.RemoveAt(i);


            }
            CambiaTamanio(dgvGraficas_generadas, 5);


            ActualizaTamanios();
        }
        private void dgvGraficas_generadas_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }
        private void dgvGraficas_generadas_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvGraficas_generadas.IsCurrentCellDirty)
            {
                dgvGraficas_generadas.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void dgvGraficas_generadas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    try
                    {
                        bool valor = !(bool)dgvGraficas_generadas[0, e.RowIndex].Value;
                        Primario_eliminar = dgvGraficas_generadas[1, e.RowIndex].Tag as Control;

                        //   Primario_eliminar.Visible = valor;
                        if (valor)
                        {
                            Area_desplegado2.Controls.Remove(Primario_eliminar);



                        }

                        else
                        {

                            Area_desplegado2.Controls.Add(Primario_eliminar);
                        }
                        ActualizaTamanios();


                    }
                    catch { }
                }
                if (e.ColumnIndex == 3)
                {
                    bool valor = (bool)dgvGraficas_generadas[3, e.RowIndex].Value;
                    Gigasoft.ProEssentials.Pesgo Pes = (Gigasoft.ProEssentials.Pesgo)dgvGraficas_generadas[1, e.RowIndex].Tag;

                    Pes.PeAnnotation.Show = valor;
                    Pes.Refresh();

                }

            }
        }


        private void dgvGraficas_generadas_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {

                if (e.ColumnIndex == 2)
                {
                    Primario_eliminar = dgvGraficas_generadas[1, e.RowIndex].Tag as Control;
                    panel2.ScrollControlIntoView(Primario_eliminar);
                    Primario_eliminar.BackColor = Color.Gray;

                    //  if (MessageBox.Show("Esta seguro de Elimnar la Grafica", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {

                        //una vez que tengo el control elimino la grafica del area a eliminar
                        Area_desplegado2.Controls.Remove(Primario_eliminar);
                        //Area_desplegado.RowCount = Area_desplegado.Controls.Count;
                        //Area_desplegado.Size = new Size(Area_desplegado.Size.Width, Primario_eliminar.Size.Height * (Area_desplegado.Controls.Count));
                        Primario_eliminar.Dispose();


                        dgvGraficas_generadas.Rows.RemoveAt(e.RowIndex);
                        //  ActualizaTamanios();
                        CambiaTamanio(dgvGraficas_generadas, 5);

                    }
                    //else
                    //{
                    //    Primario_eliminar.BackColor = Color.White;
                    //}


                }
                if (e.ColumnIndex == 1)
                {
                    try
                    {
                        Primario_eliminar = dgvGraficas_generadas[1, e.RowIndex].Tag as Control;
                        panel2.ScrollControlIntoView(Primario_eliminar);
                    }
                    catch { }

                }
                try
                {
                    int Largo = (dgvGraficas_generadas.RowCount + 2) * (dgvGraficas_generadas.Rows[0].Cells[0].Size.Height) + 5;
                    dgvGraficas_generadas.Size = new Size(dgvGraficas_generadas.Size.Width, Largo);
                    control_dgv_echas.Size = new System.Drawing.Size((Point)dgvGraficas_generadas.Size);
                    explorerBarGroupItem1.Refresh();
                    control_dgv_echas.Refresh();
                    explorerBar1.Invalidate();
                    explorerBar1.Refresh();
                }
                catch { }
            }

        }

        private void Split_SizeChanged(object sender, EventArgs e)
        {

        }

        private void Area_desplegado_ControlRemoved(object sender, ControlEventArgs e)
        {

        }



        private void panel2_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                ActualizaTamanios();
            }
            catch { }
        }

        private void cmbVariable_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvControlClases.Focus();
        }

        private void cmdGraficas_Click(object sender, EventArgs e)
        {

        }

        private void cmdGraficar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvControlClases.IsCurrentCellDirty)
                    dgvControlClases.CommitEdit(DataGridViewDataErrorContexts.Commit);
                if (cmbVariable.SelectedItem != null)
                {

                    foreach (DataGridViewRow row in dgvControlClases.Rows)
                    {
                        bool estado = (bool)row.Cells[0].Value;
                        if (estado)
                        {
                            int Valor = Convert.ToInt32(row.Cells[1].Value);
                            Pinta_reglon(row, Valor);

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Debe de seleccionar una variable para realizar el histograma!!!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch { }
            ActualizaTamanios();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            BorarTablasGeneradas();
        }
       
    }
}
