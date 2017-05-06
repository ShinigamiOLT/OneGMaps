using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Maps
{
    class OneMaps
    {
        public GMap.NET.WindowsForms.GMapControl CMaps
        {
            set; get;
        }


        public PointLatLng PosicionActual
        {
            get { return CMaps.Position; }
            set { CMaps.Position = value; }
        }
        GMap.NET.WindowsForms.GMapOverlay MarcaGlobal;
        public GMapMarker UltimoMarcador;
        public OneMaps(GMap.NET.WindowsForms.GMapControl CMaps_)
        {
            CMaps = CMaps_;
            if (CMaps != null)
            {
                PosicionActual = new GMap.NET.PointLatLng(18.239373, -93.905608);
                UltimoMarcador = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(PosicionActual,
                    GMap.NET.WindowsForms.Markers.GMarkerGoogleType.yellow_pushpin);
                Carga();
                MarcaGlobal = new GMap.NET.WindowsForms.GMapOverlay("Global");
                CMaps.Overlays.Add(MarcaGlobal);
            }
            ListaCirculo = new List<MCirculo>();
        }
        public List<MCirculo> ListaCirculo;
        public void Carga()
        {
            CMaps.DragButton = MouseButtons.Left;
            CMaps.CanDragMap = true;
            CMaps.MapProvider = GMapProviders.GoogleMap;
            // CMaps.Position = new PointLatLng(18.239373, -93.905608);
            CMaps.MinZoom = 0;
            CMaps.MaxZoom = 24;
            CMaps.Zoom = 10;
            CMaps.AutoScroll = true;
            CMaps.Refresh();

        }
        public void Carga(int zoom)
        {
            CMaps.DragButton = MouseButtons.Left;
            CMaps.CanDragMap = true;
            CMaps.MapProvider = GMapProviders.GoogleMap;

            CMaps.MinZoom = 0;
            CMaps.MaxZoom = 24;
            CMaps.Zoom = zoom;
            CMaps.AutoScroll = true;



        }

        public void Posicion()
        {
            CMaps.Position = new PointLatLng(17.988276, -93.071124);
        }

        public void Poligonos()
        {


            GMapOverlay polygons = new GMapOverlay("polygons");
            List<PointLatLng> points = new List<PointLatLng>();
            points.Add(new PointLatLng(17.988276, -93.071124));
            points.Add(new PointLatLng(17.985761, -93.073145));
            points.Add(new PointLatLng(17.98291, -93.064669));
            points.Add(new PointLatLng(17.98562, -93.062781));
            GMapPolygon polygon = new GMapPolygon(points, "Jardin des Tuileries");

            PosicionActual = points[0];

            polygons.Polygons.Add(polygon);



            CMaps.Overlays.Add(polygons);

            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon.Stroke = new Pen(Color.Red, 1);


        }

        public void Poligonos(List<PointLatLng> Puntos,string ID)
        {


            GMapOverlay polygons = new GMapOverlay("Circulos");

            GMapPolygon polygon = new GMapPolygon(Puntos, ID);

            PosicionActual = Puntos[0];
           

            polygons.Polygons.Add(polygon);

           

            CMaps.Overlays.Add(polygons);

            polygon.Fill = new SolidBrush(Color.FromArgb(10, Color.Green));
            polygon.Stroke = new Pen(Color.Red, 1);


        }

        public void Rutas()
        {
            GMapOverlay routes = new GMapOverlay("routes");
            List<PointLatLng> points = new List<PointLatLng>();
            points.Add(new PointLatLng(17.988276, -93.071124));
            points.Add(new PointLatLng(17.985761, -93.073145));
            points.Add(new PointLatLng(17.98291, -93.064669));
            GMapRoute route = new GMapRoute(points, "A walk in the park");
            route.Stroke = new Pen(Color.Green, 3);
            routes.Routes.Add(route);
            CMaps.Overlays.Add(routes);

            PosicionActual = points[0];

        }
        public void Rutas(List<PointLatLng> points)
        {
            GMapOverlay routes = new GMapOverlay("routes");

            GMapRoute route = new GMapRoute(points, "A walk in the park");
            route.Stroke = new Pen(Color.Green, 15);
            routes.Routes.Add(route);
            CMaps.Overlays.Add(routes);
            

        }

        public void Puntos()
        {
            CMaps.MapProvider = GoogleMapProvider.Instance;// GMap.NET.MapProviders.BingHybridMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            CMaps.SetPositionByKeywords("Samaria 95");
            CMaps.ShowCenter = false;

            GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
            GMap.NET.WindowsForms.GMapMarker marker =
                new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                    new GMap.NET.PointLatLng(17.988276, -93.071124),
                    GMap.NET.WindowsForms.Markers.GMarkerGoogleType.yellow_pushpin);


            marker.ToolTipText = "Samaria\n95";
            marker.Tag = marker.ToolTipText;

            marker.ToolTip.Fill = Brushes.Transparent;
            marker.ToolTip.Foreground = Brushes.Blue;
            marker.ToolTip.Stroke = Pens.Black;
            marker.ToolTip.TextPadding = new Size(20, 20);

            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            markers.Markers.Add(marker);
            CMaps.Overlays.Add(markers);

            PosicionActual = marker.Position;

        }
        public GMapMarker Puntos(Tuple<string, double, double> Tupla, bool isGlobal, GMap.NET.WindowsForms.Markers.GMarkerGoogleType Pin)
        {
            //   GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            //  CMaps.SetPositionByKeywords(Tupla.Item1);
            CMaps.ShowCenter = false;

            GMap.NET.WindowsForms.GMapMarker marker =
               new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                   new GMap.NET.PointLatLng(Tupla.Item2, Tupla.Item3), Pin);


            marker.ToolTipText = Tupla.Item1;
            marker.Tag = marker.ToolTipText;

            marker.ToolTip.Fill = Brushes.Transparent;
            marker.ToolTip.Foreground = Brushes.Blue;
            marker.ToolTip.Stroke = Pens.Black;
            marker.ToolTip.TextPadding = new Size(20, 20);

            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;


            if (isGlobal)
            {
                MarcaGlobal.Markers.Add(marker);
                UltimoMarcador = marker;

            }
            return marker;

        }

        private void gmap_OnPolygonClick(GMapPolygon item, MouseEventArgs e)
        {
            Console.WriteLine(String.Format("Polygon {0} with tag {1} was clicked",
                item.Name, item.Tag));
        }
        private void btCargarGPX_Click()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckPathExists = true;
            dlg.CheckFileExists = false;
            dlg.AddExtension = true;
            dlg.DefaultExt = "gpx";
            dlg.ValidateNames = true;
            dlg.Title = "Cargar archivo GPX";
            dlg.Filter = "Archivos GPX (*.gpx)|*.gpx";
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string gpx = File.ReadAllText(dlg.FileName);

                    gpxType r = CMaps.Manager.DeserializeGPX(gpx);
                    if (r != null)
                    {
                        if (r.trk.Length > 0)
                        {
                            GMapOverlay routes = new GMapOverlay();
                            foreach (var trk in r.trk)
                            {
                                List<PointLatLng> points = new List<PointLatLng>();

                                foreach (var seg in trk.trkseg)
                                {
                                    foreach (var p in seg.trkpt)
                                    {
                                        points.Add(new PointLatLng((double)p.lat,
                                           (double)p.lon));
                                    }
                                }

                                GMapRoute rt = new GMapRoute(points, string.Empty);
                                {
                                    rt.Stroke = new Pen(Color.FromArgb(144, Color.Red));
                                    rt.Stroke.Width = 5;
                                    rt.Stroke.DashStyle =
                                        System.Drawing.Drawing2D.DashStyle.Solid;
                                }

                                routes.Routes.Add(rt);
                                routes.IsVisibile = true;

                            }
                            CMaps.Overlays.Add(routes);
                            CMaps.ZoomAndCenterRoutes(null);

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al importar GPX: " +
                        ex.Message, "Error importando GPX",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        public void ColocaPozos(Dictionary<string, Tuple<string, double, double>> Diccionario)
        {

            GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
            foreach (KeyValuePair<string, Tuple<string, double, double>> Tupla in Diccionario)
            {
                GMapMarker marker = Puntos(Tupla.Value, false, GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue);
                markers.Markers.Add(marker);
            }


            CMaps.Overlays.Add(markers);
            CMaps.Refresh();
        }
        public void Limpiar()
        {
            var elemn = CMaps.Overlays.ToList();

            foreach (GMapOverlay elemento in elemn)
            {
                //for(int i=0; i< 5 && i<elemento.Markers.Count; i++)
                //   elemento.Markers.RemoveAt(0);
                elemento.Markers.Clear();
                CMaps.Refresh();

            }
        }

        public string Limpiar(string Nombre)
        {
            if (Nombre.Length < 1)
                return "Sin coincidencia";
            var elementos = CMaps.Overlays.ToList();
            int Numero = 0;
            foreach (GMapOverlay elemento in elementos)
            {
                //for(int i=0; i< 5 && i<elemento.Markers.Count; i++)
                //   elemento.Markers.RemoveAt(0);
                // elemento.Markers.Clear();

                List<GMapMarker> lista = elemento.Markers.Where(x => x.ToolTipText.Contains(Nombre)).ToList();
                foreach (GMapMarker marcador in lista)
                {
                    elemento.Markers.Remove(marcador);
                    Numero++;
                }

                CMaps.Refresh();

            }

            return "Eliminado: " + Numero;
        }
        public string Buscar(string Nombre)
        {
            if (Nombre.Length < 1)
                return "Sin coincidencia";
            var elementos = CMaps.Overlays.ToList();
            int Numero = 0;
            foreach (GMapOverlay elemento in elementos)
            {
                //for(int i=0; i< 5 && i<elemento.Markers.Count; i++)
                //   elemento.Markers.RemoveAt(0);
                // elemento.Markers.Clear();

                List<GMapMarker> lista = elemento.Markers.Where(x => x.ToolTipText.Contains(Nombre)).ToList();
                foreach (GMapMarker marcador in lista)
                {
                    PosicionActual = marcador.Position;
                    CMaps.ShowCenter = true;
                    // CMaps.Zoom = 12;
                    Numero++;
                }

                CMaps.Refresh();

            }

            return "Encontrado: " + Numero;
        }

        public void Circulo(PointLatLng PuntoInicial, double d, string ID)
        {
            var num = ListaCirculo.Where(x => x.ID == ID);

            MCirculo circuloactual;
            if (num.Count() > 0)
            {
                circuloactual = num.ElementAt(0);

            }
            else
            {
                circuloactual = new MCirculo(PuntoInicial, ID,CMaps);
                ListaCirculo.Add( circuloactual);

            }
            circuloactual.Radio = d;
            circuloactual.CirculoFormula();
          //  Poligonos(circuloactual.ListaPuntos,ID);
            
        }
        public void Circulo(double d, string ID)
        {
            var num = ListaCirculo.Where(x => x.ID == ID);

            MCirculo circuloactual;
            if (num.Count() > 0)
            {
                circuloactual = num.ElementAt(0);


                circuloactual.Radio = d;
                circuloactual.CirculoFormula();
               // Poligonos(circuloactual.ListaPuntos, ID);
            }

        }
        private void CirculoFormula(PointLatLng PuntoInicial, double d)
        {
            double LatI =Radianes( PuntoInicial.Lat);
            double LonI =Radianes( PuntoInicial.Lng);

            var R = 6371;// 6371e3;//metros
            double brng = 90;//angulo
            double radio = d / R;

            List<PointLatLng> LPuntos = new List<PointLatLng>();
            List<PointLatLng> Diametro = new List<PointLatLng>();


            for (int i = 0; i <= 360; i++)
            {
                brng = Radianes( i);

               

                var LatF = Math.Asin(Math.Sin(LatI) * Math.Cos(radio) +
                        Math.Cos(LatI) * Math.Sin(radio) * Math.Cos(brng));

               double uno = Math.Sin(brng) * Math.Sin(radio) * Math.Cos(LatI);
                double dos = Math.Cos(radio) - Math.Sin(LatI) * Math.Sin(LatF);
                double tres = Math.Atan2 ( uno,dos );
                double LonF = LonI + tres;
               
              //  var LonF = LonI + Math.Atan2(Math.Cos(radio) - Math.Sin(LatI) * Math.Sin(LatF), Math.Sin(brng) * Math.Sin(radio) * Math.Cos(LatI));
              //  LonF = cuatro;

                LPuntos.Add(new PointLatLng( AGrados( LatF), AGrados( LonF)));
              //  Puntos(new Tuple<string, double, double>(i + ": ", AGrados(LatF), AGrados(LonF)), true, GMap.NET.WindowsForms.Markers.GMarkerGoogleType.yellow_pushpin);
              
            }
            Poligonos(LPuntos,"0");

            /*
            var fraccion = d / R;
            var lan1 = Radianes(LatI);
            var lon1= Radianes( LonI);
            var angulo = Radianes(brng);
            var deltaLat = fraccion * Math.Cos(angulo);
            var LatFinal = lon1 + deltaLat;
            // check for some daft bugger going past the pole, normalise latitude if so
            if (Math.Abs(LatFinal) > Math.PI / 2) LatFinal = LatFinal > 0 ? Math.PI - LatFinal : -Math.PI - LatFinal;
            var X3 = Math.Log(Math.Tan(LatFinal / 2 + Math.PI / 4) / Math.Tan(lan1 / 2 + Math.PI / 4));
            var q = Math.Abs(X3) > 10e-12 ? deltaLat / X3 : Math.Cos(lon1); // E-W course becomes ill-conditioned with 0/0
            var deltalon = fraccion * Math.Sin(angulo) / q;
            var lonfinal = lon1 + deltalon;

         Puntos.Add ( new PointLatLng (AGrados( LatFinal), (AGrados( lonfinal) + 540) % 360 - 180)); // normalise to −180..+180°

            */
            //     Rutas(Puntos);
        }
        /**
 * Returns the destination point having travelled along a rhumb line from ‘this’ point the given
 * distance on the  given bearing.
 *
 * @param   {number} distance - Distance travelled, in same units as earth radius (default: metres).
 * @param   {number} bearing - Bearing in degrees from north.
 * @param   {number} [radius=6371e3] - (Mean) radius of earth (defaults to radius in metres).
 * @returns {LatLon} Destination point.
 *
 * @example
 *     var p1 = new LatLon(51.127, 1.338);
 *     var p2 = p1.rhumbDestinationPoint(40300, 116.7); // 50.9642°N, 001.8530°E
 * /
        LatLon.prototype.rhumbDestinationPoint = function(distance, bearing, radius)
        {
            radius = (radius === undefined) ? 6371e3 : Number(radius);

            var δ = Number(distance) / radius; // angular distance in radians
            var φ1 = this.lat.toRadians(), λ1 = this.lon.toRadians();
            var θ = Number(bearing).toRadians();

            var Δφ = δ * Math.cos(θ);
            var φ2 = φ1 + Δφ;

            // check for some daft bugger going past the pole, normalise latitude if so
            if (Math.abs(φ2) > Math.PI / 2) φ2 = φ2 > 0 ? Math.PI - φ2 : -Math.PI - φ2;

            var Δψ = Math.log(Math.tan(φ2 / 2 + Math.PI / 4) / Math.tan(φ1 / 2 + Math.PI / 4));
            var q = Math.abs(Δψ) > 10e-12 ? Δφ / Δψ : Math.cos(φ1); // E-W course becomes ill-conditioned with 0/0

            var Δλ = δ * Math.sin(θ) / q;
            var λ2 = λ1 + Δλ;

            return new LatLon(φ2.toDegrees(), (λ2.toDegrees() + 540) % 360 - 180); // normalise to −180..+180°
        };*/

        double Radianes(double grados)
        {
            double radianes = 0;
            return (radianes = grados * (Math.PI / 180));
        }
        double AGrados(double Radianes)
        {
            double grados = 0;
        //    Radianes =  grados * (Math.PI / 180);
          return (grados= Radianes / (Math.PI / 180));

        }
       

        void CalculaPerimetros(IList<PointLatLng> coords)
        {


            // Add all coordinates to a list, converting them to meters:
            IList<PointLatLng> points = new List<PointLatLng>();
            foreach (PointLatLng coord in coords)
            {
                PointLatLng p = new PointLatLng(
                  coord.Lng * (System.Math.PI * 6378137 / 180),
                  coord.Lat * (System.Math.PI * 6378137 / 180)
                );
                points.Add(p);
            }
            // Add point 0 to the end again:
            points.Add(points[0]);

            // Calculate polygon area (in square meters):
            var area = System.Math.Abs(points.Take(points.Count - 1)
              .Select((p, i) => (points[i + 1].Lat - p.Lat) * (points[i + 1].Lng + p.Lng))
              .Sum() / 2);
        }
    }


    public class MCirculo
    {
       public  string ID { get; set; }
        public PointLatLng PuntoInicial { get; set; }
        public double Radio { get; set; }

        double RadioTierra { set; get; }

        public List<PointLatLng> ListaPuntos { get { return LPuntos; } set { LPuntos = value; } }

        public GMap.NET.WindowsForms.GMapControl CMaps { set; get; }

        List<PointLatLng> LPuntos;



        public MCirculo(PointLatLng Localizacion,string clave, GMapControl  GMapControl_)
        {
            CMaps = GMapControl_;
            ID = clave;
            PuntoInicial = Localizacion;
            RadioTierra = 6371;//en Kilometros 6371e3;//metros
            Radio = 0;
            LPuntos = new List<PointLatLng>();
          

        }

        public void CirculoFormula()
        {
            double LatI = ARadianes(PuntoInicial.Lat);
            double LonI = ARadianes(PuntoInicial.Lng);

          
            double brng = 90;//angulo
            double radio = Radio / RadioTierra;

            LPuntos.Clear();//Limpiando la lista

            for (int i = 0; i <= 360; i++)
            {
                brng = ARadianes(i);



                var LatF = Math.Asin(Math.Sin(LatI) * Math.Cos(radio) +
                        Math.Cos(LatI) * Math.Sin(radio) * Math.Cos(brng));

                double uno = Math.Sin(brng) * Math.Sin(radio) * Math.Cos(LatI);
                double dos = Math.Cos(radio) - Math.Sin(LatI) * Math.Sin(LatF);
                double LonF = LonI +  Math.Atan2(uno, dos);


                LPuntos.Add(new PointLatLng(AGrados(LatF), AGrados(LonF)));
               

            }
            //  Poligonos(LPuntos);
            Poligonos();

          
        }

        public void Poligonos()
        {



           var lista = CMaps.Overlays.Where(x => x.Id == "Circulos");
            GMapOverlay polygons = new GMapOverlay("Circulos");
            if (lista.Count() > 0)
            {
                polygons = lista.ElementAt(0);
            }

            // GMapOverlay polygons = new GMapOverlay("Circulos");


            var buscapoligono = polygons.Polygons.Where(x => x.Name == ID);

            GMapPolygon polygon = new GMapPolygon(ListaPuntos, ID);
            if (buscapoligono.Count() > 0)
            {
             polygons.Polygons.Remove(   buscapoligono.ElementAt(0));

            } 

          

            


            polygons.Polygons.Add(polygon);



            CMaps.Overlays.Add(polygons);

            polygon.Fill = new SolidBrush(Color.FromArgb(10, Color.Green));
            polygon.Stroke = new Pen(Color.Red, 1);


        }


        double ARadianes(double grados)
        {
        
            return (grados * (Math.PI / 180));
        }
        double AGrados(double Radianes)
        {
           
            return (Radianes / (Math.PI / 180));

        }
    }

}
