using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gigasoft.ProEssentials.Enums;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;

namespace Maps
{ 
    /// <summary>
    /// Componente que contendra el pesgo listo para dibujar
    /// </summary>
    class Char_Pego: GeneraColores
    {
        public Gigasoft.ProEssentials.Pego Pesgo1;
        public bool TipoFechaX;
        public bool TipoFechaY;
        bool simbolos = true;
        float Valnull = 0;
        public float Factor = 1;
        public int GA = 0;
        public int GB = 0;
        public Char_Pego()
        {
            Pesgo1 = new Gigasoft.ProEssentials.Pego();
          
            Pesgo1.PeData.NullDataValue = -999;
            TipoFechaX = false;
            TipoFechaY = false;
           
        }

        public void invertir(bool orientacion)
        {
            Pesgo1.PeGrid.Option.InvertedYAxis = orientacion;
        }

        public void configurar_hoja3()
        {
            Pesgo1.PeGrid.Option.InvertedYAxis = false;
            Pesgo1.PeGrid.Option.ShowXAxis = ShowAxis.All;
            Pesgo1.PeColor.GraphBackground = Color.White;
            Pesgo1.PeColor.GraphForeground = Color.Black;
            Pesgo1.PeColor.Desk = Color.LightGray;
            Pesgo1.PeConfigure.BorderTypes = TABorder.NoBorder;
            Pesgo1.PeColor.GridBands = false;


            Pesgo1.Refresh();
        }

        public Char_Pego(Gigasoft.ProEssentials.Pego Pesgo, bool carlos)
        {
            Pesgo1 = Pesgo;
            
            TipoFechaX = false;
            TipoFechaY = false;
        }
        /// <summary>
        /// Si tendra Zoom o no
        /// </summary>
        public void Zoom(bool isZoom,Gigasoft.ProEssentials.Pesgo Pesgo1)
        {
            if (isZoom)
            {
                Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.HorzAndVert;
                Pesgo1.PeUserInterface.Allow.ZoomStyle = ZoomStyle.Ro2Not;

            }
            else
            {
                Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.None;
                Pesgo1.PeUserInterface.Allow.ZoomStyle = ZoomStyle.FramedRect;
            }
            Pesgo1.Refresh();
        }
        public void ZoomH(bool isZoom,Gigasoft.ProEssentials.Pesgo Pesgo1)
        {
            if (isZoom)
            {
                Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.Horizontal;
                Pesgo1.PeUserInterface.Allow.ZoomStyle = ZoomStyle.Ro2Not;

            }
            else
            {
                Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.None;
                Pesgo1.PeUserInterface.Allow.ZoomStyle = ZoomStyle.FramedRect;
            }
            Pesgo1.Refresh();
        }
        public void ZoomV(bool isZoom, Gigasoft.ProEssentials.Pesgo Pesgo1)
        {
            if (isZoom)
            {
                Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.Vertical;
                Pesgo1.PeUserInterface.Allow.ZoomStyle = ZoomStyle.Ro2Not;

            }
            else
            {
                Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.None;
                Pesgo1.PeUserInterface.Allow.ZoomStyle = ZoomStyle.FramedRect;
            }
            Pesgo1.Refresh();
        }
        public void Zoom(bool isZoom)
        {
            if (isZoom)
            {
                // Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.Horizontal;
                //   Pesgo1.PeUserInterface.Allow.ZoomStyle = ZoomStyle.Ro2Not;

                Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.Vertical;
                Pesgo1.PeUserInterface.Scrollbar.ScrollingVertZoom = true;
            }
            else
            {
                Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.None;
                Pesgo1.PeUserInterface.Allow.ZoomStyle = ZoomStyle.FramedRect;
            }
            Pesgo1.Refresh();
        }
        public void ZoomV(bool isZoom)
        {
            if (isZoom)
            {
                // Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.Horizontal;
                //   Pesgo1.PeUserInterface.Allow.ZoomStyle = ZoomStyle.Ro2Not;

                Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.Vertical;
                Pesgo1.PeUserInterface.Scrollbar.ScrollingVertZoom = true;
            }
            else
            {
                Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.None;
                Pesgo1.PeUserInterface.Allow.ZoomStyle = ZoomStyle.FramedRect;
            }
            Pesgo1.Refresh();
        }
        public void ZoomH(bool isZoom)
        {
            if (isZoom)
            {
                // Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.Horizontal;
                //   Pesgo1.PeUserInterface.Allow.ZoomStyle = ZoomStyle.Ro2Not;

                Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.Horizontal;
                Pesgo1.PeUserInterface.Scrollbar.ScrollingVertZoom = true;
                Pesgo1.PePlot.ZoomWindow.Show = true;
            }
            else
            {
                Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.None;
                Pesgo1.PeUserInterface.Allow.ZoomStyle = ZoomStyle.FramedRect;
            }
            Pesgo1.Refresh();
        }
        public Char_Pego(Gigasoft.ProEssentials.Pego Pesgo)
        {
            Pesgo1 = Pesgo;
            Pesgo1.PeData.NullDataValue = -999;
            TipoFechaX = false;
            TipoFechaY = false;
           
           

        }
        /// <summary>
        /// Aqui es para apuntar a un nuevo pesgo o obtener el actual.
        /// </summary>
        public Gigasoft.ProEssentials.Pego PlotPesgo
        {
            get { return Pesgo1; }
            set { Pesgo1 = value; }
        }
        //crear una lista por default de colores;
       
       /// <summary>
       /// estableceremos propiedades basicas como el tipo de ploteo.
       /// </summary>

        public Gigasoft.ProEssentials.Enums.GraphPlottingMethod TipoGrafica
        {
            set { Pesgo1.PePlot.Method = value; }
            get { return Pesgo1.PePlot.Method; }
        }
        /// <summary>
        /// eje Y sera normal o financiero
        /// </summary>
        public SpecialScaling TipoescalaY
        {
            set { Pesgo1.PeGrid.Option.SpecialScalingY = value; }
            get { return Pesgo1.PeGrid.Option.SpecialScalingY; }
        }
        /// <summary>
        /// eje Y2 sera normal o financiero
        /// </summary>
        public SpecialScaling TipoescalaY2
        {
            set { Pesgo1.PeGrid.Option.SpecialScalingRY = value; }
            get { return Pesgo1.PeGrid.Option.SpecialScalingRY; }
        }

        public bool Simbolos
        {
            set { simbolos = value; }
            get { return simbolos; }
        }
        /// <summary>
        /// Establer el valor conla cual la grafica no pintara
        /// </summary>
        /// <param name="ValorNulo"></param>
        public void estableceNulo(float ValorNulo)
        {
            Pesgo1.PeData.Precision = DataPrecision.SixDecimals;
            Pesgo1.PeData.NullDataValue = ValorNulo;
            Valnull = ValorNulo;
        }
        /// <summary>
        /// si el eje x sera de tipo tiempo.
        /// </summary>
        public bool TipoTiempoX
        {
            set { Pesgo1.PeData.DateTimeMode = value; }
            get { return Pesgo1.PeData.DateTimeMode; }
        }
        /// <summary>
        /// Tamaño de los puntos mstrados.
        /// </summary>
        public PointSize TamPunto
        {
            set { Pesgo1.PePlot.PointSize = value; }
            get { return Pesgo1.PePlot.PointSize; }
        }
        /// <summary>
        /// Limpiando todos los valores
        /// </summary>
        public void LimpiaDatos() { 
         Pesgo1.PeData.X.Clear();
                Pesgo1.PeData.Y.Clear();
           
               
        }
        /// <summary>
        /// cantidad de serie de datos a graficar
        /// </summary>
        public int Series
        {
            set { Pesgo1.PeData.Subsets = value; }
            get { return Pesgo1.PeData.Subsets; }
        }
        /// <summary>
        /// puntos por serie.
        /// </summary>
        public int MaximoPuntos
        {
            set { Pesgo1.PeData.Points = value; }
            get { return Pesgo1.PeData.Points; }
        }
        
           public bool Invertir_eje_Y
        {
            set {  Pesgo1.PeGrid.Option.InvertedYAxis  = value; }
            get { return  Pesgo1.PeGrid.Option.InvertedYAxis ; }
        }
        public bool Indistintos
        {
            set { Pesgo1.PeData.SubsetByPoint = value; }
            get { return Pesgo1.PeData.SubsetByPoint; }
        }
        int colactual = 0;
       
        public string Nombre
        {
            set { Pesgo1.PeString.MainTitle = value; }
            get { return  Pesgo1.PeString.MainTitle; }
        }
        public string Subtitulo
        {
            set { Pesgo1.PeString.SubTitle = value; }
            get { return Pesgo1.PeString.SubTitle; }
        }
         public float Factor_Inversion
        {
            set { Factor = value; }
            get { return Factor; }
        }
        
        /// <summary>
        /// Este trata de colocar las serie cada una en cada eje
        /// </summary>
        /// <remarks>aun esta mal no grafica como debiera</remarks>
       public  void Plot_X_Serie_Mismo_Eje( DataTable Tabla,  string Tipo, int X, int Y, int Serie)
        {
          
            Pesgo1.PeUserInterface.Scrollbar.MouseDraggingX = true;
            Pesgo1.PeUserInterface.Scrollbar.MouseDraggingY = true;
            Pesgo1.PeData.AutoScaleData = false;// para quitar la m

            // Enable Bar Glass Effect //
            Pesgo1.PePlot.Option.BarGlassEffect = false;

            // Enable Plotting style gradient and bevel features //
            Pesgo1.PePlot.Option.AreaGradientStyle = PlotGradientStyle.RadialBottomRight;
            Pesgo1.PePlot.Option.AreaBevelStyle = BevelStyle.MediumSmooth;
            Pesgo1.PePlot.Option.SplineGradientStyle = PlotGradientStyle.RadialBottomRight;
            Pesgo1.PePlot.Option.SplineBevelStyle = SplineBevelStyle.MediumSmooth;

            // Prepare images in memory //
            Pesgo1.PeConfigure.PrepareImages = true;

            // Pass Data //
            //One_Registro_Presion.VistaPrevia Vp = new One_Registro_Presion.VistaPrevia(Tabla);
            //Vp.ShowDialog();
            try
            {
                   Coloca_Datos_PorSerie( Serie, Tabla, X, Y);
                    
               
            }
            catch { }
            // Set DataShadows to show 3D //
            Pesgo1.PePlot.DataShadows = DataShadows.None;

            // Enable ZoomWindow //

            //            Pesgo1.PePlot.ZoomWindow.Show = true;

            Pesgo1.PeUserInterface.Allow.FocalRect = false;
           // Pesgo1.PePlot.Method = SGraphPlottingMethod.PointsPlusLine;
            Pesgo1.PeGrid.LineControl = GridLineControl.Both;
            Pesgo1.PeGrid.Style = GridStyle.Dot;
           

            Pesgo1.PeLegend.SimplePoint = false;
            Pesgo1.PeLegend.SimpleLine = false;
            Pesgo1.PeLegend.Style = LegendStyle.OneLineInsideAxis;
            Pesgo1.PeGrid.Option.MultiAxisStyle = MultiAxisStyle.SeparateAxes;

            Pesgo1.PePlot.Option.GradientBars = 8;
            Pesgo1.PeConfigure.TextShadows = TextShadows.BoldText;
            Pesgo1.PeFont.MainTitle.Bold = true;
            Pesgo1.PeFont.SizeTitleCntl = 0.72f;
            Pesgo1.PeFont.FontSize = FontSize.Medium;
            Pesgo1.PeFont.SubTitle.Bold = true;
            Pesgo1.PeFont.Label.Bold = true;
            Pesgo1.PePlot.Option.LineShadows = true;
            Pesgo1.PeFont.FontSize = FontSize.Large;
            Pesgo1.PeUserInterface.Scrollbar.ScrollingVertZoom = true;
            Pesgo1.PeData.Precision = DataPrecision.OneDecimal;

            // Various other features //
            Pesgo1.PeFont.Fixed = true;
            Pesgo1.PeColor.BitmapGradientMode = true;
            Pesgo1.PeColor.QuickStyle = QuickStyle.MediumNoBorder;

            // Improves Metafile Export //
            Pesgo1.PeSpecial.DpiX = 600;
            Pesgo1.PeSpecial.DpiY = 600;

            Pesgo1.PeConfigure.RenderEngine = RenderEngine.GdiPlus;
            Pesgo1.PeConfigure.AntiAliasGraphics = true;
            Pesgo1.PeConfigure.AntiAliasText = true;

            Pesgo1.PeColor.BitmapGradientMode = true;
            Pesgo1.PeColor.QuickStyle = QuickStyle.LightLine;
            Pesgo1.PePlot.MarkDataPoints = true;
            
            Pesgo1.PeLegend.Show = true;
            Pesgo1.Visible = true;

        }


       public void configurar_hoja2(Gigasoft.ProEssentials.Pego p1)
       {
          // p1.PeGrid.Option.InvertedYAxis = true;
           p1.PeGrid.Option.ShowXAxis = ShowAxis.All;
           p1.PeColor.GraphBackground = Color.White;
           p1.PeColor.GraphForeground = Color.Black;
           p1.PeColor.Desk = Color.LightGray;
           p1.PeConfigure.BorderTypes = TABorder.NoBorder;
           p1.PeColor.GridBands = false;
           

           p1.Refresh();
       }
        
       public void configurar_hoja(Gigasoft.ProEssentials.Pego p1)
       {
           p1.PeGrid.Option.InvertedYAxis = true;
           p1.PeGrid.Option.ShowXAxis = ShowAxis.Empty;
           p1.PeColor.GraphBackground = Color.White;
           p1.PeColor.GraphForeground = Color.Black;
           p1.PeColor.Desk = Color.LightGray;
           p1.PeConfigure.BorderTypes = TABorder.NoBorder;
           p1.PeColor.GridBands = false;


           p1.Refresh();
       }
       public void configurar_hoja2()
       {
           Pesgo1.PeGrid.Option.InvertedYAxis = Invertir_eje_Y;
           Pesgo1.PeGrid.Option.ShowXAxis = ShowAxis.All;
           Pesgo1.PeColor.GraphBackground = Color.White;
           Pesgo1.PeColor.GraphForeground = Color.Black;
           Pesgo1.PeColor.Desk = Color.White;
           Pesgo1.PeConfigure.BorderTypes = TABorder.NoBorder;
           Pesgo1.PeColor.GridBands = false;
           Pesgo1.PeConfigure.BorderTypes = TABorder.SingleLine;
           // Pesgo1.PeGrid.Style = GridStyle.OnePixel;
           Pesgo1.Refresh();
       }

       PointType pt = 0;
        void Coloca_Datos_PorSerie( int Lineas,  DataTable Actual, int X, int Y)
        {
            try
            {

                

                Pesgo1.PeData.Points =MaximoPuntos;

                if (Simbolos)
                {
                    if (pt == PointType.UpTriangleSolid)
                        pt = 0;
                }
                else
                {
                    pt = PointType.DotSolid;
                }
                for (int s = Lineas; s < Lineas + 1; s++)
                {
                    try
                    {
                        Pesgo1.PeString.SubsetLabels[s] = Actual.Columns[Y].ColumnName + Actual.TableName;
                        Pesgo1.PeString.XAxisLabel = Actual.Columns[X].ColumnName;
                        Pesgo1.PeString.YAxisLabel = Actual.Columns[Y].ColumnName +"  "+ Actual.TableName; 
                        Pesgo1.PePlot.SubsetPointTypes[s] = pt;
                        if(Actual.Rows.Count >0)
                        for (int p = 0; p < Actual.Rows.Count && p<MaximoPuntos; p++)
                        {
                            try
                            {
                                Pesgo1.PeString.PointLabels[p] = Actual.Rows[p][X].ToString();

                            }
                            catch { }
                            //aqui saco las Y;
                            try
                            {
                              
                                Pesgo1.PeColor.PointColors[s, p] =  ColoresR();
                   
                                if (Actual.Rows[p][Y] != null && Convert.ToString(Actual.Rows[p][Y]) != "")
                                {
                                    if (!TipoFechaY)
                                    {
                                        Pesgo1.PeData.Y[s, p] =Factor* Convert.ToSingle(Actual.Rows[p][Y]);

                                    }
                                    else
                                    {
                                        Pesgo1.PeData.Y[s, p] = Convert.ToSingle(Convert.ToDateTime(Actual.Rows[p][Y]).ToOADate());

                                    }
                                }
                                else
                                {
                                    Pesgo1.PeData.Y[s, p] =Valnull;
                                }


                            }
                            catch
                            {
                                Pesgo1.PeData.Y[s, p] = Valnull;
                            }
                            pt++;
                        }


                        Pesgo1.PeLegend.SubsetLineTypes[s] = LineType.Dot;
                       
                        
                        //aqui se agregan los puntos mas grandes que no tenga yo dato
                        if(Actual.Rows.Count >0)
                        for (int p = Actual.Rows.Count; p < MaximoPuntos; p++)
                        {
                            
                             try
                            {
                                if (Actual.Rows[Actual.Rows.Count - 1][Y] != null && Convert.ToString(Actual.Rows[Actual.Rows.Count - 1][Y]) != "")
                                {
                                    if (!TipoFechaY)
                                    {
                                        Pesgo1.PeData.Y[s, p] =Valnull;

                                    }
                                    else
                                    {
                                        Pesgo1.PeData.Y[s, p] = Convert.ToSingle(Convert.ToDateTime(Actual.Rows[Actual.Rows.Count - 1][Y]).ToOADate());

                                    }


                                }
                                else
                                {
                                    Pesgo1.PeData.Y[s, p] = Valnull;
                                }


                            }
                             catch (Exception ex)
                             {
                                 System.Windows.Forms.MessageBox.Show(ex.Message);
                                 Pesgo1.PeData.Y[s, p] = Valnull;
                            }
                        }//del for para llenar vacios

                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message+ "  "+ ex.StackTrace);
                       throw (ex);
                    }
                }//for para toda la serie
            }
            catch { }

        }

      public  void Plot_Serie_X_Eje( DataTable Tabla_datos1,int X,int Y,int Serie)
        {
            try
            {
                Coloca_Datos_PorSerie(Serie, Tabla_datos1, X, Y);
                 Pesgo1.PeColor.SubsetColors[0] = ColoresR();
                // Create 4 separate axes and then overlap in two groups //
                Pesgo1.PeGrid.MultiAxesSubsets[0] = 1; // 1 subset on first axis
                Pesgo1.PeGrid.MultiAxesSubsets[1] = 1; // 1 subset on second axis
                Pesgo1.PeGrid.OverlapMultiAxes[0] = 2; // overlap first two axes in one group
                Pesgo1.PeGrid.OverlapMultiAxes[1] = 2; // overlap second two axes in one group
                Pesgo1.PeUserInterface.Allow.MultiAxesSizing = false;

                // Match axis color and label to subset label //
                Pesgo1.PeGrid.WorkingAxis = 0;
              //  Pesgo1.PeColor.YAxis = Uno;
                Pesgo1.PeString.YAxisLabel = Tabla_datos1.Columns[1].ColumnName;
                Pesgo1.PePlot.Method = GraphPlottingMethod.PointsPlusLine;
                Pesgo1.PeLegend.SubsetLineTypes[0] = LineType.DashDot;


                Pesgo1.PeGrid.WorkingAxis = 1;
               // Pesgo1.PeColor.YAxis = Dos;
               // Pesgo1.PeString.YAxisLabel = Tabla_datos2.Columns[1].ColumnName;
                Pesgo1.PePlot.Method = GraphPlottingMethod.PointsPlusLine;
                Pesgo1.PeLegend.SubsetLineTypes[1] = LineType.DashDot;
               

                Pesgo1.PeGrid.WorkingAxis = 0;

                // Set Various Other Properties ///
                Pesgo1.PeFont.FontSize = FontSize.Large;
                Pesgo1.PePlot.MarkDataPoints = true;


                // Set Various Other Properties ///
                Pesgo1.PeColor.BitmapGradientMode = true;
                Pesgo1.PeColor.QuickStyle = QuickStyle.LightLine;
                Pesgo1.PePlot.MarkDataPoints = true;
                Pesgo1.PePlot.PointSize = PointSize.Small;
                Pesgo1.PeLegend.Show = true;

                Pesgo1.PeGrid.LineControl = GridLineControl.Both;
                Pesgo1.PeConfigure.BorderTypes = TABorder.Inset;
                Pesgo1.PeUserInterface.Menu.ShowLegend = MenuControl.Show;
                Pesgo1.PeUserInterface.Menu.LegendLocation = MenuControl.Show;
                Pesgo1.PeLegend.Style = LegendStyle.OneLineInsideAxis;



            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }



        }

      /// <summary>
      /// Trata de colocar en grupo de 2 serie por cada eje Y
      /// </summary>
      /// <remarks>Funciona apartir de 3 series.</remarks>
      public void Pinta_PorGrupos(bool sentido)
      {
          Pesgo1.PePlot.MethodII = GraphPlottingMethodII.PointsPlusSpline;
          try
          {
              // Create 4 separate axes and then overlap in two groups //
              Pesgo1.PeGrid.MultiAxesSubsets[0] = GA; // 1 subset on first axis
              Pesgo1.PeGrid.MultiAxesSubsets[1] = GB; // 1 subset on second axis

              if (sentido)
              {
                  Pesgo1.PeGrid.OverlapMultiAxes[0] = 2; // overlap first two axes in one group
                  Pesgo1.PeGrid.OverlapMultiAxes[1] = 1; // overlap second two axes in one group
              }
              else {
                  Pesgo1.PeGrid.OverlapMultiAxes[0] = 1; // overlap first two axes in one group
                  Pesgo1.PeGrid.OverlapMultiAxes[1] = 2; // overlap second two axes in one group
              
                }
              
              Pesgo1.PeUserInterface.Allow.MultiAxesSizing = true;

              // Match axis color and label to subset label //
              Pesgo1.PeGrid.WorkingAxis = 0;
              Pesgo1.PeColor.YAxis = Color.Black;// Pesgo1.PeColor.SubsetColors[0]; 
              string texto = "";
              if (GA == 1)
                  for (int i = 0; i < GA; i++)
                  {
                      texto += Pesgo1.PeString.SubsetLabels[i] + "  ";
                  }
              Pesgo1.PeString.YAxisLabel = texto;//Pesgo1.PeString.SubsetLabels[0]+"    "+  Pesgo1.PeString.SubsetLabels[1];
              GraphPlottingMethod MX = TipoGrafica;
              Pesgo1.PePlot.Method = TipoGrafica;
              Pesgo1.PeLegend.SubsetLineTypes[0] = LineType.DashDot;


              Pesgo1.PeGrid.WorkingAxis = 1;
              Pesgo1.PeColor.YAxis = Color.Black;// Pesgo1.PeColor.SubsetColors[2];

              texto = "";
              if (GB == 1)
                  for (int i = GA; i < GA + GB; i++)
                  {
                      texto += Pesgo1.PeString.SubsetLabels[i] + "  ";
                  }
              Pesgo1.PeString.YAxisLabel = texto; //Pesgo1.PeString.SubsetLabels[2];// Tabla_datos1.Columns[1].ColumnName;;//Tabla_datos2.Columns[1].ColumnName;
              Pesgo1.PePlot.Method = MX;
              Pesgo1.PeLegend.SubsetLineTypes[1] = LineType.DashDot;



              Pesgo1.PeGrid.WorkingAxis = 0;

              // Set Various Other Properties ///
              Pesgo1.PeFont.FontSize = FontSize.Large;
              Pesgo1.PePlot.MarkDataPoints = true;


              // Set Various Other Properties ///
              Pesgo1.PeColor.BitmapGradientMode = true;
              Pesgo1.PeColor.QuickStyle = QuickStyle.LightLine;
              Pesgo1.PePlot.MarkDataPoints = true;
              Pesgo1.PePlot.PointSize = PointSize.Small;
              Pesgo1.PeLegend.Show = true;

              Pesgo1.PeGrid.LineControl = GridLineControl.Both;
              Pesgo1.PeConfigure.BorderTypes = TABorder.SingleLine;
              Pesgo1.PeUserInterface.Menu.ShowLegend = MenuControl.Show;
              Pesgo1.PeUserInterface.Menu.LegendLocation = MenuControl.Show;
              Pesgo1.PeLegend.Style = LegendStyle.OneLineInsideAxis;
              Pesgo1.PeString.TextShadows = TextShadows.NoShadows;



          }
          catch (Exception ex) { MessageBox.Show(ex.Message); }



      }
      public void Pinta_Tabla_en_Grafica(int columna_para_eje_x, int columna_para_eje_y, DataGridView Tabla)
      {
          for (int i = 0; i < MaximoPuntos; i++)
          {
              Pesgo1.PeData.X[0, i] = (float)(Tabla[columna_para_eje_x, i].Value);
              Pesgo1.PeData.Y[0, i] = (float)(Tabla[columna_para_eje_y, i].Value);
          }
          Pesgo1.Refresh();

      }


     
     

      public void Establecer_min_max_del_eje_Y(double Minimo, double Maximo)
      {

          Pesgo1.PeGrid.Configure.ManualScaleControlY = ManualScaleControl.MinMax;
          Pesgo1.PeGrid.Configure.ManualMinY = Minimo;
          Pesgo1.PeGrid.Configure.ManualMaxY = Maximo;


      }

      public void Pinta_Tabla_en_Grafica(string nombre_columna_para_eje_x, string nombre_columna_para_eje_y, DataGridView Tabla)
      {

          try
          {
              for (int i = 0; i < MaximoPuntos; i++)
              {
                  //si el eje es invertidoo
                  if (Pesgo1.PeGrid.Option.InvertedYAxis == true)
                  {
                      Pesgo1.PeData.X[0, i] = (float)Convert.ToDouble(Tabla[nombre_columna_para_eje_x, i].Value);
                      Pesgo1.PeData.Y[0, i] = (-1) * ((float)Convert.ToDouble(Tabla[nombre_columna_para_eje_y, i].Value));

                  }//si no lo es se grafica normal
                  else
                  {
                      Pesgo1.PeData.X[0, i] = (float)Convert.ToDouble(Tabla[nombre_columna_para_eje_x, i].Value);
                      Pesgo1.PeData.Y[0, i] = (float)Convert.ToDouble(Tabla[nombre_columna_para_eje_y, i].Value);

                  }
              }



              Pesgo1.Refresh();
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + ex.StackTrace);
          }
      }


      public void Plot_X_Serie_Mismo_Eje2(DataTable Tabla, string Tipo, int X, int Y, int Serie, int Inicio, int Fin)
      {
          Pesgo1.PeUserInterface.Scrollbar.MouseDraggingX = true;
          Pesgo1.PeUserInterface.Scrollbar.MouseDraggingY = true;
          Pesgo1.PeData.AutoScaleData = false;// para quitar la m

          //// Enable Bar Glass Effect //
          //Pesgo1.PePlot.Option.BarGlassEffect = false;

          //// Enable Plotting style gradient and bevel features //
          //Pesgo1.PePlot.Option.AreaGradientStyle = PlotGradientStyle.RadialBottomRight;
          //Pesgo1.PePlot.Option.AreaBevelStyle = BevelStyle.MediumSmooth;
          //Pesgo1.PePlot.Option.SplineGradientStyle = PlotGradientStyle.RadialBottomRight;
          //Pesgo1.PePlot.Option.SplineBevelStyle = SplineBevelStyle.MediumSmooth;

          // Prepare images in memory //
          Pesgo1.PeConfigure.PrepareImages = true;

          // Pass Data //
          //One_Registro_Presion.VistaPrevia Vp = new One_Registro_Presion.VistaPrevia(Tabla);
          //Vp.ShowDialog();
          try
          {
              Coloca_Datos_PorSerie2(Serie, Tabla, X, Y, Inicio, Fin);


          }
          catch { }

          configurar_hoja2(Pesgo1);
          try
          {
              Pesgo1.PeLegend.Show = false;
              Pesgo1.PeAnnotation.InFront = false;
              Pesgo1.PeAnnotation.Line.TextSize = 110;
              Pesgo1.PeAnnotation.Show = true;
              Pesgo1.PeAnnotation.Line.YAxis[0] = 0;
              Pesgo1.PeAnnotation.Line.YAxisType[0] = LineAnnotationType.Dot;
              Pesgo1.PeAnnotation.Line.YAxisColor[0] = Color.FromArgb(0, 0, 198);
              Pesgo1.PeAnnotation.Line.YAxisText[0] = "";
              Pesgo1.PeAnnotation.Line.YAxisAxis[0] = 0;
              Pesgo1.PeAnnotation.Line.YAxisInFront[0] = AnnotationInFront.InFront;



              Pesgo1.PeAnnotation.Line.XAxis[0] = 0;
              Pesgo1.PeAnnotation.Line.XAxisType[0] = LineAnnotationType.Dot;
              Pesgo1.PeAnnotation.Line.XAxisColor[0] = Color.FromArgb(0, 0, 198);
              Pesgo1.PeAnnotation.Line.XAxisText[0] = "";
              Pesgo1.PeAnnotation.Line.XAxisInFront[0] = AnnotationInFront.InFront;
          }
          catch { }


      }

      public void Plot_X_Serie_Mismo_Eje2(DataGridView Tabla, string Tipo, int X, int Y, int Serie, int Inicio, int Fin)
      {
          Pesgo1.PeUserInterface.Scrollbar.MouseDraggingX = true;
          Pesgo1.PeUserInterface.Scrollbar.MouseDraggingY = true;
          Pesgo1.PeData.AutoScaleData = false;// para quitar la m

          //// Enable Bar Glass Effect //
          //Pesgo1.PePlot.Option.BarGlassEffect = false;

          //// Enable Plotting style gradient and bevel features //
          //Pesgo1.PePlot.Option.AreaGradientStyle = PlotGradientStyle.RadialBottomRight;
          //Pesgo1.PePlot.Option.AreaBevelStyle = BevelStyle.MediumSmooth;
          //Pesgo1.PePlot.Option.SplineGradientStyle = PlotGradientStyle.RadialBottomRight;
          //Pesgo1.PePlot.Option.SplineBevelStyle = SplineBevelStyle.MediumSmooth;

          // Prepare images in memory //
          Pesgo1.PeConfigure.PrepareImages = true;

          // Pass Data //
          //One_Registro_Presion.VistaPrevia Vp = new One_Registro_Presion.VistaPrevia(Tabla);
          //Vp.ShowDialog();
          try
          {
              Coloca_Datos_PorSerie2(Serie, Tabla, X, Y, Inicio, Fin);


          }
          catch { }

          configurar_hoja2(Pesgo1);
          try
          {
              Pesgo1.PeLegend.Show = false;
              Pesgo1.PeAnnotation.InFront = false;
              Pesgo1.PeAnnotation.Line.TextSize = 110;
              Pesgo1.PeAnnotation.Show = true;
              Pesgo1.PeAnnotation.Line.YAxis[0] = 0;
              Pesgo1.PeAnnotation.Line.YAxisType[0] = LineAnnotationType.Dot;
              Pesgo1.PeAnnotation.Line.YAxisColor[0] = Color.FromArgb(0, 0, 198);
              Pesgo1.PeAnnotation.Line.YAxisText[0] = "";
              Pesgo1.PeAnnotation.Line.YAxisAxis[0] = 0;
              Pesgo1.PeAnnotation.Line.YAxisInFront[0] = AnnotationInFront.InFront;



              Pesgo1.PeAnnotation.Line.XAxis[0] = 0;
              Pesgo1.PeAnnotation.Line.XAxisType[0] = LineAnnotationType.Dot;
              Pesgo1.PeAnnotation.Line.XAxisColor[0] = Color.FromArgb(0, 0, 198);
              Pesgo1.PeAnnotation.Line.XAxisText[0] = "";
              Pesgo1.PeAnnotation.Line.XAxisInFront[0] = AnnotationInFront.InFront;
          }
          catch { }


      }
        //DataGridView Tabla

      void Coloca_Datos_PorSerie2(int Lineas, DataTable Actual, int X, int Y, int Inicio, int Fin)
      {
          try
          {

              Pesgo1.PeData.Points = MaximoPuntos;

              if (Simbolos)
              {
                  if (pt == PointType.UpTriangleSolid)
                      pt = 0;
              }
              else
              {
                  pt = PointType.DotSolid;
              }
              for (int s = Lineas; s < Lineas + 1; s++)
              {
                  try
                  {
                      Pesgo1.PeString.SubsetLabels[s] = Actual.Columns[Y].ColumnName + Actual.TableName;
                      Pesgo1.PeString.XAxisLabel = Actual.Columns[X].ColumnName;
                      Pesgo1.PeString.YAxisLabel = Actual.Columns[Y].ColumnName + "  " + Actual.TableName;
                      Pesgo1.PePlot.SubsetPointTypes[s] = pt;
                      if (Actual.Rows.Count > 0)
                          for (int p = 0, ovi = Inicio; ovi < Fin; p++, ovi++)
                          {
                              //saco las X
                              try
                              {
                                  if (!TipoTiempoX)
                                  {
                                      Pesgo1.PeData.X[s, p] = Convert.ToSingle(Actual.Rows[p + Inicio][X]);

                                  }
                                  else
                                  {
                                      Pesgo1.PeData.X[s, p] = (float)Convert.ToDateTime(Actual.Rows[p + Inicio][X]).ToOADate();

                                  }
                              }
                              catch
                              {
                                  Pesgo1.PeData.X[s, p] = Valnull;
                              }
                              // if(Inicio==0)
                              //     Pesgo1.PeData.Z[s, p] = 1;
                              // else

                              // Pesgo1.PeData.Z[s, p] = 100;// 0.5f * Inicio;
                              ////
                              // //aqui saco las Y;
                              try
                              {
                                  if (Actual.Rows[p][Y] != null && Convert.ToString(Actual.Rows[p + Inicio][Y]) != "")
                                  {
                                      if (!TipoFechaY)
                                      {
                                          Pesgo1.PeData.Y[s, p] = Factor * Convert.ToSingle(Actual.Rows[p + Inicio][Y]);

                                      }
                                      else
                                      {
                                          Pesgo1.PeData.Y[s, p] = Convert.ToSingle(Convert.ToDateTime(Actual.Rows[p + Inicio][Y]).ToOADate());

                                      }
                                  }
                                  else
                                  {
                                      Pesgo1.PeData.Y[s, p] = Valnull;
                                  }


                              }
                              catch
                              {
                                  Pesgo1.PeData.Y[s, p] = Valnull;
                              }

                              pt++;
                          }


                      Pesgo1.PeLegend.SubsetLineTypes[s] = LineType.Dot;
                      try
                      {
                          Pesgo1.PeColor.SubsetColors[s] = ColoresR();
                      }
                      catch { Pesgo1.PeColor.SubsetColors[s] = Color.Red; }
                      //aqui se agregan los puntos mas grandes que no tenga yo dato
                      if (Actual.Rows.Count > 0)
                          for (int p = Actual.Rows.Count; p < MaximoPuntos; p++)
                          {
                              //saco las X
                              try
                              {
                                  if (!TipoTiempoX)
                                  {
                                      Pesgo1.PeData.X[s, p] = Valnull;

                                  }
                                  else
                                  {
                                      Pesgo1.PeData.X[s, p] = (float)Convert.ToDateTime(Actual.Rows[Actual.Rows.Count - 1][X]).ToOADate();

                                  }
                              }
                              catch (Exception ex)
                              {
                                  System.Windows.Forms.MessageBox.Show(ex.Message);
                                  Pesgo1.PeData.X[s, p] = Valnull;
                              }
                              try
                              {
                                  if (Actual.Rows[Actual.Rows.Count - 1][Y] != null && Convert.ToString(Actual.Rows[Actual.Rows.Count - 1][Y]) != "")
                                  {
                                      if (!TipoFechaY)
                                      {
                                          Pesgo1.PeData.Y[s, p] = Valnull;

                                      }
                                      else
                                      {
                                          Pesgo1.PeData.Y[s, p] = Convert.ToSingle(Convert.ToDateTime(Actual.Rows[Actual.Rows.Count - 1][Y]).ToOADate());

                                      }


                                  }
                                  else
                                  {
                                      Pesgo1.PeData.Y[s, p] = Valnull;
                                  }


                              }
                              catch (Exception ex)
                              {
                                  System.Windows.Forms.MessageBox.Show(ex.Message);
                                  Pesgo1.PeData.Y[s, p] = Valnull;
                              }
                          }//del for para llenar vacios

                  }
                  catch (Exception ex)
                  {
                      System.Windows.Forms.MessageBox.Show(ex.Message + "  " + ex.StackTrace);
                      throw (ex);
                  }
              }//for para toda la serie
          }
          catch { }

      }

       void Coloca_Datos_PorSerie2(int Lineas, DataGridView Actual, int X, int Y, int Inicio, int Fin)
      {
          try
          {

              Pesgo1.PeData.Points = MaximoPuntos;

              if (Simbolos)
              {
                  if (pt == PointType.UpTriangleSolid)
                      pt = 0;
              }
              else
              {
                  pt = PointType.DotSolid;
              }
              for (int s = Lineas; s < Lineas + 1; s++)
              {
                  try
                  {
                      Pesgo1.PeString.SubsetLabels[s] = Actual.Columns[Y].Name + Actual.Name;
                      Pesgo1.PeString.XAxisLabel = Actual.Columns[X].Name;
                      Pesgo1.PeString.YAxisLabel = Actual.Columns[Y].Name + "  " + Actual.Name;
                      Pesgo1.PePlot.SubsetPointTypes[s] = pt;
                      if (Actual.Rows.Count > 0)
                          for (int p = 0, ovi = Inicio; ovi < Fin; p++, ovi++)
                          {
                              //saco las X
                              try
                              {
                                  if (!TipoTiempoX)
                                  {
                                      Pesgo1.PeData.X[s, p] = Convert.ToSingle(Actual.Rows[p + Inicio].Cells[X]);

                                  }
                                  else
                                  {
                                      Pesgo1.PeData.X[s, p] = (float)Convert.ToDateTime(Actual.Rows[p + Inicio].Cells[X]).ToOADate();

                                  }
                              }
                              catch
                              {
                                  Pesgo1.PeData.X[s, p] = Valnull;
                              }
                              // if(Inicio==0)
                              //     Pesgo1.PeData.Z[s, p] = 1;
                              // else

                              // Pesgo1.PeData.Z[s, p] = 100;// 0.5f * Inicio;
                              ////
                              // //aqui saco las Y;
                              try
                              {
                                  if (Actual.Rows[p].Cells[Y] != null && Convert.ToString(Actual.Rows[p + Inicio].Cells[Y]) != "")
                                  {
                                      if (!TipoFechaY)
                                      {
                                          Pesgo1.PeData.Y[s, p] = Factor * Convert.ToSingle(Actual.Rows[p + Inicio].Cells[Y]);

                                      }
                                      else
                                      {
                                          Pesgo1.PeData.Y[s, p] = Convert.ToSingle(Convert.ToDateTime(Actual.Rows[p + Inicio].Cells[Y]).ToOADate());

                                      }
                                  }
                                  else
                                  {
                                      Pesgo1.PeData.Y[s, p] = Valnull;
                                  }


                              }
                              catch
                              {
                                  Pesgo1.PeData.Y[s, p] = Valnull;
                              }

                              pt++;
                          }


                      Pesgo1.PeLegend.SubsetLineTypes[s] = LineType.Dot;
                      try
                      {
                          Pesgo1.PeColor.SubsetColors[s] = ColoresR();
                      }
                      catch { Pesgo1.PeColor.SubsetColors[s] = Color.Red; }
                      //aqui se agregan los puntos mas grandes que no tenga yo dato
                      if (Actual.Rows.Count > 0)
                          for (int p = Actual.Rows.Count; p < MaximoPuntos; p++)
                          {
                              //saco las X
                              try
                              {
                                  if (!TipoTiempoX)
                                  {
                                      Pesgo1.PeData.X[s, p] = Valnull;

                                  }
                                  else
                                  {
                                      Pesgo1.PeData.X[s, p] = (float)Convert.ToDateTime(Actual.Rows[Actual.Rows.Count - 1].Cells[X]).ToOADate();

                                  }
                              }
                              catch (Exception ex)
                              {
                                  System.Windows.Forms.MessageBox.Show(ex.Message);
                                  Pesgo1.PeData.X[s, p] = Valnull;
                              }
                              try
                              {
                                  if (Actual.Rows[Actual.Rows.Count - 1].Cells[Y] != null && Convert.ToString(Actual.Rows[Actual.Rows.Count - 1].Cells[Y]) != "")
                                  {
                                      if (!TipoFechaY)
                                      {
                                          Pesgo1.PeData.Y[s, p] = Valnull;

                                      }
                                      else
                                      {
                                          Pesgo1.PeData.Y[s, p] = Convert.ToSingle(Convert.ToDateTime(Actual.Rows[Actual.Rows.Count - 1].Cells[Y]).ToOADate());

                                      }


                                  }
                                  else
                                  {
                                      Pesgo1.PeData.Y[s, p] = Valnull;
                                  }


                              }
                              catch (Exception ex)
                              {
                                  System.Windows.Forms.MessageBox.Show(ex.Message);
                                  Pesgo1.PeData.Y[s, p] = Valnull;
                              }
                          }//del for para llenar vacios

                  }
                  catch (Exception ex)
                  {
                      System.Windows.Forms.MessageBox.Show(ex.Message + "  " + ex.StackTrace);
                      throw (ex);
                  }
              }//for para toda la serie
          }
          catch { }

      }


    }
}


