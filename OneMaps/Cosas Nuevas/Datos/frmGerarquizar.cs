using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Maps;
using Gigasoft.ProEssentials.Enums;

namespace Maps
{
    public partial class frmGerarquizar : Form
    {
        private DataTable dtIndezax,Temporales;
        private List< Maps.Core_Clasificador.ObjetoTablas> ListaFinal;
        bool estado_activo = false;
        NotifyIcon notificacion;
        GeneraColores Colores_;
       

        public frmGerarquizar(DataTable dtIndezax, NotifyIcon noti)
        {
            // TODO: Complete member initialization
            this.dtIndezax = dtIndezax;
            InitializeComponent();

            notificacion = noti;
            dgvSumatoria.DataSource = dtIndezax;
            //aquicargaremos el combo con las columnas a heterogenizar.
            foreach (DataColumn col in dtIndezax.Columns)
                comboBoxEx1.Items.Add(col.ColumnName);
           
        }

        public frmGerarquizar(List<Maps.Core_Clasificador.ObjetoTablas>ListaFinal,NotifyIcon noti)
        {
            InitializeComponent();
            notificacion= noti;
            // TODO: Complete member initialization
            this.ListaFinal = ListaFinal;
            foreach (Maps.Core_Clasificador.ObjetoTablas p in ListaFinal)
            {
                p.Calculos();
                if (string.IsNullOrWhiteSpace(p.Cadena))
                    p.Cadena = p.Tabla_unidad.TableName;
            }

           
        }
       

        private void frmGerarquizar_Load(object sender, EventArgs e)
        {
            //antes de pintar cargamos las columas en los combos para idexar
            Colores_= new GeneraColores();
            if (ListaFinal.Count > 0)
            {
                Core_Clasificador.ObjetoTablas obj = ListaFinal[0];
                comboBoxEx1.Items.Clear();
                comboBoxEx2.Items.Clear();
                foreach (DataColumn col in obj.Tabla_unidad.Columns)
                {
                    comboBoxEx1.Items.Add(col.ColumnName);
                    comboBoxEx2.Items.Add(col.ColumnName);
                }
               // SoloreglonesGrupo();
                CargaLineas();
            }  
            //Temporales = ListaFinal[0].Tabla_unidad.Clone();
  
         
           // PintaNuevamente("IX", "IY");
        }
        void CargaLineas()
        {
            foreach (Core_Clasificador.ObjetoTablas row in ListaFinal)
            {
                DataGridViewRow dgvrow= new DataGridViewRow();
                dgvrow.CreateCells(dgv_Controles);

               dgvrow.Cells[0].Value= true;
                dgvrow.Cells[1].Value= row.Cadena;

               dgvrow.Cells[1].Tag= row;
               dgvrow.Cells[2].Style.BackColor=Colores_.ColoresR();

               dgv_Controles.Rows.Add(dgvrow);

              
            }
            CambiaTamanio(dgv_Controles, 15);
    }
        void PintaNuevamente(string X, string Y)
        {
            if (ListaFinal.Count > 0)
            {
                //hay almenos una tabla

                //dataGridView1.Rows.Clear();
                //dgvSumatoria.Columns.Clear();

              
               
                foreach (Core_Clasificador.ObjetoTablas obj in ListaFinal)
                {
                    obj.AgregaIndex(X, Y);
                    obj.CalcularIndexacion();
                }
 Core_Clasificador.ObjetoTablas ob = ListaFinal[0];
 Temporales = ob.Esquema().Clone();

                //foreach (DataColumn on in ob.Tabla_unidad.Columns)
                //{
                //    dgvSumatoria.Columns.Add(on.ColumnName, on.ColumnName);
                //}

              
            }
        }

        void SoloreglonesGrupo()
        { 
             if (ListaFinal.Count > 0)
            {
                if (Temporales == null)
                    Temporales = ListaFinal[0].Esquema().Clone();
                else
                {
                    Temporales.Dispose();
                    Temporales = ListaFinal[0].Esquema().Clone();
                }
                 Temporales.Clear();
          
                //hay almenos una tabla

                dgv_Controles.Rows.Clear();
                dgvSumatoria.Columns.Clear();

                dgvSumatoria.DataSource = Temporales;
                 int num=0;
                foreach (Core_Clasificador.ObjetoTablas obj in ListaFinal)
                {
                    num++;
                    try
                    {   //  obj.imprimeValores(dgvSumatoria);
                        Temporales.Merge(obj.Tabla_unidad, true, MissingSchemaAction.Ignore);
                      //  dgv_Controles.Rows.Add(true, obj.Cadena, "");
                     notificacion.ShowBalloonTip(1000, "Imprimiendo Tabla!!!", "Heterogenizar: "+ num, ToolTipIcon.Info);
          
                       // dgv_Controles.Rows[dgv_Controles.Rows.Count - 1].Cells[1].Tag = obj;
                       // dgv_Controles.Rows[dgv_Controles.Rows.Count - 1].Cells[2].Style.BackColor = ColoresDisponibles[dgv_Controles.Rows.Count - 1];
                    }
                    catch { 
                    
                    }
                }
                CambiaTamanio(dgv_Controles,15);

            }
         
        
        }
        void SoloreglonesGrupo1_()
        {
            if (ListaFinal.Count > 0)
            {
                if (Temporales == null)
                    Temporales = ListaFinal[0].Esquema().Clone();
                else
                {
                    Temporales.Dispose();
                    Temporales = ListaFinal[0].Esquema().Clone();
                }
                Temporales.Clear();

                //hay almenos una tabla

                dgvSumatoria.Columns.Clear();

                dgvSumatoria.DataSource = Temporales;
                int num = 0;
                 foreach (DataGridViewRow row in dgv_Controles.Rows)
                {


                    Core_Clasificador.ObjetoTablas obj = (Core_Clasificador.ObjetoTablas)row.Cells[1].Tag;
                    bool estado = (bool)row.Cells[0].Value;
                    if (estado)
                    {
                        num++;
                        try
                        {   //  obj.imprimeValores(dgvSumatoria);
                            Temporales.Merge(obj.Tabla_unidad, true, MissingSchemaAction.Ignore);
                            notificacion.ShowBalloonTip(1000, "Imprimiendo Tabla!!!", "Heterogenizar: " + num, ToolTipIcon.Info);

                           
                        }
                        catch
                        {

                        }
                    }
                }
                CambiaTamanio(dgv_Controles, 15);

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
        //void CambiaTamanio(DataGridView dgv)
        //{
        //    try
        //    {
        //        //if (dgv.Rows.Count > 0)
        //        {

        //            int Largo = (dgv.RowCount + 2) * (dgv.RowTemplate.Height) + 5;
        //            dgv.Size = new Size(dgv.Size.Width, Largo);
        //            explorerBarGroupItem1.Refresh();
        //            explorerBarGroupItem1.RecalcSize();

        //            explorerBar1.Update();
        //            explorerBar1.Refresh();

        //        }
        //    }
        //    catch { }
        //}


        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void btCargar_Click(object sender, EventArgs e)
        {
            //antes de inicicambiar el nombre bucaremos  e indexaremos
           
           
        }

        private void dgvSumatoria_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
           
            foreach (DataGridViewRow row in dgv_Controles.Rows)
            {

                row.Cells[0].Value = !estado_activo;
            } 
            estado_activo = !estado_activo;
        }
      void Graficar_Resultado()
        {

            if (dgv_Controles.IsCurrentCellDirty)
                dgv_Controles.CommitEdit(DataGridViewDataErrorContexts.Commit);

          TabPrincipal.SelectedTabIndex = 1;
            if (comboBoxEx1.SelectedItem != null && comboBoxEx2.SelectedItem != null)
            {
                //aqui graficaremos la tabla de index.
                DataTable tabla = new DataTable();
                tabla.Columns.Add(comboBoxEx1.Text);
                tabla.Columns.Add(comboBoxEx2.Text);
                tabla.Columns.Add("Criterio");
                int maximopunto = 0;
                int Elementos = 0;

                foreach (DataGridViewRow row in dgv_Controles.Rows)
                {


                    Core_Clasificador.ObjetoTablas obj = (Core_Clasificador.ObjetoTablas)row.Cells[1].Tag;
                    bool estado = (bool)row.Cells[0].Value;
                    if (estado)
                    {
                        try
                        {
                            obj.Agrega(tabla);

                            int valTabla = obj.Tabla_unidad.Rows.Count;
                            if (valTabla > maximopunto)
                                maximopunto = valTabla;
                            Elementos++;
                        }
                        catch { }
                    }

                }

                //-------->

                // if(  chpunto.Checked)

                //if(chotro.Checked)
                //    plo.PlotPesgo.PePlot.Option.MaximumPointSize = MinimumPointSize.MediumSmall;
                Grafica_Estado_Mecanico.PeFunction.Reset();

                EventoPesgo even = new EventoPesgo(Grafica_Estado_Mecanico, new NotifyIcon());
                if (Elementos > 0)
                {
                    Maps.Char_Pro plo = new Char_Pro(Grafica_Estado_Mecanico);
                    plo.Zoom(Zoom.ZoomA);
                    plo.Series = Elementos;
                    plo.estableceNulo(-999.99f);
                    plo.TamPunto = PointSize.Medium;
                    plo.PlotPesgo.PePlot.Option.MaximumPointSize = MinimumPointSize.MediumLarge;

                    plo.MaximoPuntos = maximopunto;
                    plo.TipoTiempoX = false;
                    plo.Simbolos = false;


                    plo.Colores.Clear();
                    int SeriesCargadas = 0;
                    List<double> ListaX = new List<double>();
                    List<double> ListaY = new List<double>();

                    List<string> Lista_Nombre = new List<string>();
                    //aqui 
                    int cont = 0;
                    //  Visor_Datos vd = new Visor_Datos();
                    foreach (DataGridViewRow row in dgv_Controles.Rows)
                    {
                        bool estado = (bool)row.Cells[0].Value;
                        if (estado)
                        {
                            Core_Clasificador.ObjetoTablas obj = (Core_Clasificador.ObjetoTablas)row.Cells[1].Tag;
                            try
                            {


                                plo.TipoGrafica = SGraphPlottingMethod.Point;
                                plo.Colores.Add(row.Cells[2].Style.BackColor);
                                DataTable t = obj.RegresaT(chOrigenes.Checked);


                                ListaX.AddRange(MaxMin(t, 0).ToArray());
                                ListaY.AddRange(MaxMin(t, 1).ToArray());
                                plo.Plot_Serie_X_EjeV2(t, 0, 1, SeriesCargadas);
                                t.Dispose();

                                plo.PlotPesgo.PeString.SubsetLabels[SeriesCargadas] = obj.Cadena;
                                if (row.Cells[1].Value != null)
                                {
                                    if (row.Cells[1].Value.ToString().Trim() != "")
                                    {
                                        plo.PlotPesgo.PeString.SubsetLabels[SeriesCargadas] = row.Cells[1].Value.ToString().Trim();
                                    }
                                }

                                SeriesCargadas++;

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        cont++;

                    }
                    plo.CargaPorDefault();

                    //una vez que tenemos  los datos 

                    var paraX = ListaX.AsEnumerable().Max(p => Math.Abs(p));

                    double paraX4 = paraX;// +(paraX / 10);

                    var paraY = ListaY.AsEnumerable().Max(p => Math.Abs(p));

                    double paraY4 = paraY;// +(paraY / 10);
                    if (!ch_automatico.Checked)
                    {
                        plo.EstableceMinMaxX(-(paraX4 - 1), paraX4 + 1);
                        plo.EstableceMinMaxY(-(paraY4 - 1), paraY4 + 1);
                    }
                    //  MessageBox.Show(paraX.ToString());




                    plo.configurar_hoja2();
                    plo.Nombre = "Grafica";

                    plo.Subtitulo = "";
                    if(!ch_sinCruz.Checked)
                    ColocaEventos(plo.PlotPesgo);
                    if (plo.PlotPesgo.Tag != null)
                    {
                        DataTable ob = (DataTable)plo.PlotPesgo.Tag;
                        ob.Dispose();
                        plo.PlotPesgo.Tag = null;
                    }
                    plo.PlotPesgo.Tag = tabla;
                    plo.PlotPesgo.PeFunction.ReinitializeResetImage();
                    plo.PlotPesgo.Refresh();

                    //-------->
                }//si hay alemnos uno seleccionado

            }
            else
            {
                MessageBox.Show("Deberia de Marcar las variables a indexar !!!","Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
      MyPoint MaxMin(DataTable t, int col)
      {
          double max = 0, min = 0;
          var ani = from valor in t.AsEnumerable().Where(p => !string.IsNullOrWhiteSpace(p.Field<string>(col)))
                    select Convert.ToDouble(valor.Field<string>(col));

          if (ani.Any())
          {
              max = ani.Max();

          }

          var otro = from valor in t.AsEnumerable().Where(p => !string.IsNullOrWhiteSpace(p.Field<string>(col)))
                     select Convert.ToDouble(valor.Field<string>(col));
          if (otro.Any())
          {
              min = otro.Max();

          }
          return new MyPoint(max, min);
      }
        void ColocaEventos(Gigasoft.ProEssentials.Pesgo PlotPesgo)
        {


            PlotPesgo.PePlot.MarkDataPoints = false;

            PlotPesgo.PeAnnotation.InFront = true;
            PlotPesgo.PeAnnotation.Line.TextSize = 80;// Convert.ToInt32(txtFont.Text);
            PlotPesgo.PeAnnotation.Show = true;

            PlotPesgo.PeFont.GraphAnnotationTextSize = 100;



            // Give user ability to show or hide annotations //
            PlotPesgo.PeUserInterface.Menu.AnnotationControl = true;

            PlotPesgo.PeAnnotation.Show = true;
            PlotPesgo.PeAnnotation.ShowAllTableAnnotations = true;

            PlotPesgo.PeFont.FontSize = FontSize.Large;

            PlotPesgo.PeUserInterface.Menu.AnnotationControl = true;
            int ncon = 0;
            //texto
            /*
                PlotPesgo.PeAnnotation.Line.XAxis[ncon] = Convert.ToDateTime(reglon[0]).ToOADate();//Tablas_A_Graficar[0].Rows[0][Xcol]).ToOADate();
                PlotPesgo.PeAnnotation.Line.XAxisType[ncon] = LineAnnotationType.ThinSolid;
                PlotPesgo.PeAnnotation.Line.XAxisColor[ncon] = Color.Black;//.FromArgb(198, 0, 198);                     

                PlotPesgo.PeAnnotation.Line.XAxisText[ncon] = "|t" + reglon[2].ToString();
                ncon++;

            */
            //linea
            PlotPesgo.PeAnnotation.Line.XAxis[ncon] = 1;//Tablas_A_Graficar[0].Rows[0][Xcol]).ToOADate();
            ////PlotPesgo.PeAnnotation.Line.XAxisType[ncon] = LineAnnotationType.ThinSolid;
            PlotPesgo.PeAnnotation.Line.XAxisColor[ncon] = Color.Black;//.FromArgb(198, 0, 198);


            PlotPesgo.PeAnnotation.Line.XAxisText[ncon] = " ";
            //para la Y
            PlotPesgo.PeAnnotation.Line.YAxis[ncon] = 1;//Tablas_A_Graficar[0].Rows[0][Xcol]).ToOADate();
            ////PlotPesgo.PeAnnotation.Line.XAxisType[ncon] = LineAnnotationType.ThinSolid;
            PlotPesgo.PeAnnotation.Line.YAxisColor[ncon] = Color.Black;//.FromArgb(198, 0, 198);


            PlotPesgo.PeAnnotation.Line.YAxisText[ncon] = " ";




        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                ColorDialog Colornuevo = new ColorDialog();
                if (Colornuevo.ShowDialog() == DialogResult.OK)
                {
                    dgv_Controles[e.ColumnIndex, e.RowIndex].Style.BackColor = Colornuevo.Color;

                }
            }
        }

        private void comboBoxEx2_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void cmdGraficar_Click(object sender, EventArgs e)
        {
            if (comboBoxEx1.Text != comboBoxEx2.Text)
            {

              PintaNuevamente("I" + comboBoxEx1.Text, "I" + comboBoxEx2.Text);
                SoloreglonesGrupo1_();
                Graficar_Resultado();
            }
            else
            {
                MessageBox.Show("Error!!! ambas variables Son iguales");
            }
        }

        private void dgv_Controles_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.ColumnIndex + " --- " + e.RowIndex+" --- "+ e.Context);
        }

       

       

    }
}
