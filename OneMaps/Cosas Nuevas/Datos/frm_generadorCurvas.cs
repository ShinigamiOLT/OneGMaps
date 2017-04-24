using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Onestatistics;
using Gigasoft.ProEssentials.Enums;
using OneDistribuciones;

namespace Maps
{
    public partial class frm_generadorCurvas : Form
    {
        private List<Maps.Core_Clasificador.ObjetoTablas> ListaFinal;
       
        public Control Primario_eliminar;
        NotifyIcon Notificacion;
        public bool estado_activo = true;
        GeneraColores ColoresList;
         List<ObjetoSelecionable> listaObjetos = new List<Maps.ObjetoSelecionable>();

        public frm_generadorCurvas(List<Maps.Core_Clasificador.ObjetoTablas> ListaFinal, NotifyIcon noti)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.ListaFinal = ListaFinal;
            Notificacion = noti;
            ColoresList = new GeneraColores();
            MuestraSelecionDistribuciones(listaObjetos);
        }

        private void frm_generadorCurvas_Load(object sender, EventArgs e)
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

          
            foreach (Core_Clasificador.ObjetoTablas obj in ListaFinal)
            {

                // obj.imprimeValores(dgvSumatoria);
                dgvControlClases.Rows.Add(true, obj.Cadena, "");


                dgvControlClases.Rows[dgvControlClases.Rows.Count - 1].Cells[1].Value = formula(obj.Tabla_unidad.Rows.Count);
                dgvControlClases.Rows[dgvControlClases.Rows.Count - 1].Cells[1].Tag = ColoresList.ColoresR();
                dgvControlClases.Rows[dgvControlClases.Rows.Count - 1].Cells[2].Value = obj.Cadena;
                dgvControlClases.Rows[dgvControlClases.Rows.Count - 1].Cells[2].Tag = obj;


            }
            CambiaTamanio(dgvControlClases, 10);


            contiene_boton.Control = dgvGraficas_generadas;
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

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            estado_activo = !estado_activo;
            foreach (DataGridViewRow row in dgvControlClases.Rows)
            {

                row.Cells[0].Value = estado_activo;
            }
        }
        string Titulo_e = "";
        string Subtitulo = "";
        int Valor;
        private void cmdGraficar_Click(object sender, EventArgs e)
        {
            if (cmbVariable.Text == "")
                return;


            Maps.userFiltroElemento selecUltima = new Maps.userFiltroElemento(ref listaObjetos);
            selecUltima.EnForm(true);
            var marcados = from a in listaObjetos where (a.Estado) select a;

            if (dgvControlClases.IsCurrentCellDirty)
                dgvControlClases.CommitEdit(DataGridViewDataErrorContexts.Commit);

            foreach (DataGridViewRow Reglon in dgvControlClases.Rows)
            {
                bool estado = (bool)Reglon.Cells[0].Value;
                 Valor = Convert.ToInt32(Reglon.Cells[1].Value);
                 if (estado)
                 {
                     Core_Clasificador.ObjetoTablas obj = (Core_Clasificador.ObjetoTablas)Reglon.Cells[2].Tag;


                     DataTable Tabla = obj.Tabla_unidad;
                     string Campo = cmbVariable.Text;

                     DescriptiveStatistics cEstadistica = new DescriptiveStatistics();
                     cEstadistica.Actualiza(Tabla, Campo);
                     Curvas curva = new Curvas();
                     cEstadistica.Actualiza(Tabla, Campo);
                     

                     foreach (ObjetoSelecionable obj_ in marcados)
                     {

                         Titulo_e= obj.Cadena;
                         Subtitulo = obj_.objeto.ToString();
                         switch ( obj_.objeto.ToString())
                         {
                             case "Distribución Normal":
                                 HistorialSlider(curva.PdfNormal(), "Distribución Normal", cEstadistica);
                                 break;
                             case "Distribución Weibull":
                                 HistorialSlider(curva.PdfWeibull(), "Distribución Weibull", cEstadistica);
                                 break;
                             case "Distribución Log-Normal":
                                 HistorialSlider(curva.PdfLogNormal(), "Distribución Log-Normal", cEstadistica);
                                 break;
                             case "Distribución Exponencial":
                                 HistorialSlider(curva.PdfExponencial(), "Distribución Exponencial", cEstadistica);
                                 break;
                             case "Distribución Gamma":
                                 HistorialSlider(curva.PdfGamma(), "Distribución Gamma", cEstadistica);
                                 break;
                             case "Distribución Valores Ext.":

                                 HistorialSlider(curva.PdfValoresExtremosTipo_1(), "Distribución Valores Ext.", cEstadistica);
                                 break;
                             case "Distribución Uniforme":
                                 HistorialSlider(curva.PdfUniforme(), "Distribución Uniforme", cEstadistica);
                                 break;
                             case "Distribución  Doble Exp.":
                                 HistorialSlider(curva.PdfDoubleExponencial(), "Distribución  Doble Exp.", cEstadistica);
                                 break;

                         }

                     }
                 }
                


            }
        }
        void MuestraSelecionDistribuciones(List<Maps.ObjetoSelecionable> listaObjetos)
        {

            listaObjetos.Add(new Maps.ObjetoSelecionable("Distribución Normal"));
           // listaObjetos.Add(new One_LD.ObjetoSelecionable("Distribución Weibull"));
            listaObjetos.Add(new Maps.ObjetoSelecionable("Distribución Log-Normal"));
            listaObjetos.Add(new Maps.ObjetoSelecionable("Distribución Exponencial"));
            listaObjetos.Add(new Maps.ObjetoSelecionable("Distribución Gamma"));
            listaObjetos.Add(new Maps.ObjetoSelecionable("Distribución Valores Ext."));
            listaObjetos.Add(new Maps.ObjetoSelecionable("Distribución Uniforme"));
           // listaObjetos.Add(new One_LD.ObjetoSelecionable("Distribución Poisson"));
           // listaObjetos.Add(new One_LD.ObjetoSelecionable("Distribución Binomial"));
         //   listaObjetos.Add(new One_LD.ObjetoSelecionable("Distribución Pareto"));
            listaObjetos.Add(new Maps.ObjetoSelecionable("Distribución  Doble Exp."));


           


        }

        public void HistorialSlider(DataTable Curva, string NomCurva, DescriptiveStatistics n)
        {
            

            Gigasoft.ProEssentials.Pesgo Pego2 = new Gigasoft.ProEssentials.Pesgo();




            Pego2 = InsertaGraficaEnTabladeFrecuencia(n.TabalaFrecuenciaMini(Valor), true, Curva);

           
            
            Pego2.Tag = Curva.Copy();
            EventoPesgo ep1 = new EventoPesgo(Pego2, Notificacion);
            ColocaSlider(Pego2);
            


            Pego2.PeString.SubTitle =Subtitulo;

            Pego2.PeLegend.Show = chAshurado.Checked;
            Pego2.PeGrid.WorkingAxis = 0;
            Pego2.PeString.YAxisLabel = "";

            DataGridViewRow reglon = new DataGridViewRow();
            reglon.CreateCells(dgvGraficas_generadas);
            reglon.Cells[0].Value = true;
            reglon.Cells[1].Value = Pego2.PeString.MainTitle;

            reglon.Cells[1].Tag = Pego2;
            dgvGraficas_generadas.Rows.Add(reglon);
           
           
           
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


        public Gigasoft.ProEssentials.Pesgo InsertaGraficaEnTabladeFrecuencia(DataTable Tabla, bool PintaGrafica, DataTable DT)
        {
            Gigasoft.ProEssentials.Pesgo Pego1 = new Gigasoft.ProEssentials.Pesgo();
            //en esta`pintara el histogtrama mas la curva
            Ovidio(ConfigurandoHistograma(Tabla), Pego1, DT, PintaGrafica);
            Pego1.Tag = Tabla;
            Pego1.Size = new Size(275, 195);
            Pego1.PeString.MainTitle = Titulo_e;
            Pego1.Dock = DockStyle.Fill;
            return Pego1;

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

        void Ovidio(List<DataTable> Lista_Datos, Gigasoft.ProEssentials.Pesgo Pego1, DataTable dtCurva, bool PintaCurva)
        {
            if (Lista_Datos.Count <= 0)
                throw (new Exception("Este intervalo de tiempo no contienen datos !!!"));

            List<Maps.Graficacion_Serie> Lista = new List<Maps.Graficacion_Serie>();

            Graficacion_Serie lx = new Graficacion_Serie();
            lx.Origen = Lista_Datos[0].TableName;
            lx.Variable = Lista_Datos[0].Columns[0].ColumnName;
            lx.NombrePozo = "";
            lx.Eje = "X1";
            Lista.Add(lx);

            for (int i = 0; i < Lista_Datos.Count; i++)
            {
                Graficacion_Serie ly = new Graficacion_Serie();
                ly.Origen = Lista_Datos[i].TableName;
                ly.Variable = Lista_Datos[i].Columns[0].ColumnName;
                ly.Eje = "Y1";
                ly.ColorGrafica = Color.Blue;
                ly.TipoGrafica = SGraphPlottingMethods.Area;
                ly.Informacion = Lista_Datos[i];
                Lista.Add(ly);
            }

            //Aqui mandemos la curva
            if (PintaCurva)
            {
                Maps.Graficacion_Serie ly1 = new Maps.Graficacion_Serie();
                ly1.Origen = dtCurva.TableName;
                ly1.Variable = dtCurva.Columns[0].ColumnName;
                ly1.Eje = "Y2";
                ly1.ColorGrafica = Color.DarkSlateBlue;
                ly1.TipoGrafica = SGraphPlottingMethods.Point;
                ly1.Informacion = dtCurva;
                Lista.Add(ly1);
            }

            Char_Pro Ploter = new Char_Pro(Pego1);
            Ploter.Series = Lista_Datos.Count;
            Ploter.Simbolos = false;
            Ploter.PlotPesgo.PeUserInterface.Menu.AnnotationControl = true;
            Ploter.PlotPesgo.PeString.XAxisLabel = "Rango de Clases";
            {    //aqui va la media, la linea
                Ploter.PlotPesgo.PeAnnotation.InFront = true;
                Ploter.PlotPesgo.PeAnnotation.Show = true;
                Ploter.PlotPesgo.PeAnnotation.Line.TextSize = 80;
                Ploter.PlotPesgo.PeFont.GraphAnnotationTextSize = 100;
                Ploter.PlotPesgo.PeUserInterface.Menu.AnnotationControl = true;
                Ploter.PlotPesgo.PeAnnotation.ShowAllTableAnnotations = true;

            }

            Graficando_Plantilla(new Maps.MiPlantilla(Lista), TamanioCirculos.MedioSmall, false, Pego1);

            Pego1.PeLegend.Style = LegendStyle.OneLine;
            Pego1.PeLegend.Location = LegendLocation.Right;
            Pego1.PeColor.BitmapGradientMode = false;
            //Pego1.PeString.

            Pego1.PePlot.MarkDataPoints = false;
            Pego1.PeFunction.ReinitializeResetImage();
            Pego1.Refresh();
        }

        Gigasoft.ProEssentials.Pesgo Graficando_Plantilla(MiPlantilla MiPlan, TamanioCirculos tam, bool IsFecha, Gigasoft.ProEssentials.Pesgo Nueva)
        {
            //aqui mandare sacar todas las Tablas.
            if (MiPlan != null)
            {
                try
                {
                    //Seccionde Variables Reutlizadas.

                    bool Existe_X = false;

                    Graficacion_Serie SerieX = new Graficacion_Serie();
                    List<Graficacion_Serie> Serie = new List<Graficacion_Serie>();
                    //mejor volveremos a crear todo desde cero.
                    //tretaemos de localizar el eje X.

                    foreach (Graficacion_Serie se in MiPlan.Serie)
                    {
                        if (se.Eje == "X1")
                        {

                            SerieX = se;
                            Existe_X = true;
                            break;
                        }
                    }
                    if (Existe_X)
                    {
                        //como ya tengo mi variable X ahora lo que tengo que hacer es crear las series a graficar. Y1--- hasta la YN.

                        foreach (Graficacion_Serie se in MiPlan.Serie)
                        {

                            if (se.Eje != "X1")
                            {
                                //  se.Informacion = tabla;

                                Serie.Add(se);


                            }

                        }


                        Char_Pro plo = new Char_Pro(Nueva);
                        //  plo.Pego1.PeCustomMenu += new Gigasoft.ProEssentials.Pesgo.CustomMenuEventHandler(Pego1_PeCustomMenu);
                        plo.Zoom(Zoom.ZoomA);
                        plo.Series = MiPlan.Serie.Count - 1; //EjeY[0].Count + EjeY[1].Count + EjeY[2].Count + EjeY[3].Count + EjeY[4].Count; ;//Ycol1.Count + Ycol2.Count; //
                        plo.estableceNulo(-999.99f);
                        plo.TamPunto = PointSize.Small;

                        switch (tam)
                        {
                            case TamanioCirculos.chico:
                                plo.TamPunto = PointSize.Small;
                                break;
                            case TamanioCirculos.mediano:
                                break;
                            case TamanioCirculos.micro:
                                plo.TamPunto = PointSize.Micro;
                                break;
                            case TamanioCirculos.largo:
                                plo.PlotPesgo.PePlot.Option.MaximumPointSize = MinimumPointSize.MediumLarge;
                                break;
                            case TamanioCirculos.MedioSmall:
                                plo.PlotPesgo.PePlot.Option.MaximumPointSize = MinimumPointSize.MediumSmall;
                                break;
                        }


                        int max_p = 0;

                        foreach (Graficacion_Serie se in Serie)
                        {
                            if (se.Informacion.Rows.Count > max_p)
                                max_p = se.Informacion.Rows.Count;
                        }
                        plo.MaximoPuntos = max_p;
                        if (IsFecha)
                            plo.TipoTiempoX = true;
                        else
                            plo.TipoTiempoX = false;
                        plo.Simbolos = false;


                        plo.Colores.Clear();
                        int SeriesCargadas = 0;
                        string Names = "";
                        List<string> Lista_Nombre = new List<string>();
                        //aqui 
                        int[] Tamanio_Y = new int[] { 0, 0, 0, 0, 0 };
                        for (int n = 0; n < 5; n++)
                        {
                            Names = "";
                            foreach (Graficacion_Serie se in Serie)
                            {
                                if (plo.Campos[n + 1] == se.Eje)
                                {
                                    plo.TipoGrafica = SGraphPlottingMethod.Point;
                                    plo.PlotPesgo.PePlot.Methods[SeriesCargadas] = se.TipoGrafica;
                                    if (se.TipoGrafica == SGraphPlottingMethods.Area)
                                    {
                                        plo.PlotPesgo.PePlot.SubsetHatch[SeriesCargadas] = HatchType.BDiagonal;
                                        plo.PlotPesgo.PeColor.HatchBackColor = Color.White;
                                    }
                                    plo.Colores.Add(se.ColorGrafica);
                                    plo.Plot_Serie_X_EjeV2(se.Informacion, 0, 1, SeriesCargadas);
                                    Names += plo.PlotPesgo.PeString.SubsetLabels[SeriesCargadas] = se.ToString();
                                    SeriesCargadas++;
                                    Tamanio_Y[n]++;
                                }
                            }
                            Names = "F R E C U E N C I A\n\n";
                            if (Names.Length > 0)
                                Lista_Nombre.Add(Names);
                        }

                        plo.CargaPorDefault();


                        int Numero_grupos = 0;

                        for (int cont = 0; cont < 5; cont++)
                        {
                            if (Tamanio_Y[cont] != 0)
                            {
                                plo.PlotPesgo.PeGrid.MultiAxesSubsets[Numero_grupos] = Tamanio_Y[cont];
                                Numero_grupos++;
                            }
                        }
                        plo.PlotPesgo.PeGrid.OverlapMultiAxes[0] = Numero_grupos;

                        for (int h = 0; h < Numero_grupos; h++)
                        {
                            plo.PlotPesgo.PeGrid.WorkingAxis = h;
                            plo.PlotPesgo.PeString.YAxisLabel = Lista_Nombre[h];
                        }
                        plo.PlotPesgo.PeString.XAxisLabel = cmbVariable.Text;
                        plo.configurar_hoja2();



                        plo.Nombre = SerieX.NombrePozo;
                        //plo.Subtitulo = "Graficas de Chan";
                    }



                    else
                    {


                        MessageBox.Show("Falta Selecionar el Eje X", "ERROR");


                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }


            }
            return Nueva;
        }
        int formula(int tam)
        {
            return Convert.ToInt32(   1 + 3.33 * Math.Log10(tam));
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                ActualizaTamanios();
            }
            catch { }
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
                    panel1.ScrollControlIntoView(Primario_eliminar);
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
                        panel1.ScrollControlIntoView(Primario_eliminar);
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

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            BorarTablasGeneradas();
        }
        void BorarTablasGeneradas()
        {

            for (int i = dgvGraficas_generadas.Rows.Count - 1; i > -1; i--)
            {

                Primario_eliminar = dgvGraficas_generadas[1, i].Tag as Control;
                panel1.ScrollControlIntoView(Primario_eliminar);
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
        


        

    }
}
