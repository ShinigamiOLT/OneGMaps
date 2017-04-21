using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET.MapProviders;
using GMap.NET;
using System.Windows.Forms;
using System.IO;
using GMap.NET.WindowsForms;
using System.Drawing;

namespace OneGMaps
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

        public OneMaps()
        {
            if (CMaps != null)
            {
                PosicionActual = new GMap.NET.PointLatLng(18.239373, -93.905608);
                Carga();
            }
        }

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

       public  void Puntos()
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
        public GMapMarker Puntos(Tuple<string ,double,double> Tupla)
        {
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            CMaps.SetPositionByKeywords(Tupla.Item1);
            CMaps.ShowCenter = false;

             GMap.NET.WindowsForms.GMapMarker marker =
                new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                    new GMap.NET.PointLatLng(Tupla.Item2,Tupla.Item3),
                    GMap.NET.WindowsForms.Markers.GMarkerGoogleType.yellow_pushpin);


            marker.ToolTipText = Tupla.Item1;
            marker.Tag = marker.ToolTipText;

            marker.ToolTip.Fill = Brushes.Transparent;
            marker.ToolTip.Foreground = Brushes.Blue;
            marker.ToolTip.Stroke = Pens.Black;
            marker.ToolTip.TextPadding = new Size(20, 20);

            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
           

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

        public void ColocaPozos( Dictionary<string,Tuple<string,double,double>> Diccionario)
        {

            GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
            foreach (KeyValuePair<string, Tuple<string,double,double>> Tupla in Diccionario)
            {
                GMapMarker marker = Puntos(Tupla.Value);
                markers.Markers.Add(marker);
            }

            
            CMaps.Overlays.Add(markers);
        }
        public void Limpiar()
        {
          var elemn =  CMaps.Overlays.ToList();

            foreach (GMapOverlay elemento in elemn)
            {
               //for(int i=0; i< 5 && i<elemento.Markers.Count; i++)
                 //   elemento.Markers.RemoveAt(0);
                CMaps.Refresh();
                
            }
        }
    }
}
