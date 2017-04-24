using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneGMaps
{
    public partial class Form1 : Form
    {
        OneMaps cMapas = new OneMaps();
        OneMapsDatos cdatos = new OneMapsDatos();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

       GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
       
            cMapas.CMaps = gMapControl1;
            cMapas.PosicionActual = new GMap.NET.PointLatLng(18.239373, -93.905608);
            dataGridView1.DataSource = cdatos.TablaInfo;
            dataGridView1.DataMember = cdatos.TablaInfo.Tables[0].TableName;
            cMapas.Posicion();
            cMapas.Carga();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            cMapas.Posicion();
            cMapas.Carga();
           
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            cMapas.Poligonos();
        }

        private void gMapControl1_OnPolygonClick(GMap.NET.WindowsForms.GMapPolygon item, MouseEventArgs e)
        {

            Console.WriteLine(String.Format("Polygon {0} with tag {1} was clicked",
                item.Name, item.Tag));
        }

        private void btnLinea_Click(object sender, EventArgs e)
        {
            cMapas.Rutas();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            cMapas.Puntos();
        }

        private void gMapControl1_OnMarkerClick(GMap.NET.WindowsForms.GMapMarker item, MouseEventArgs e)
        {
            Console.WriteLine(String.Format("Marker {0} was clicked.", item.Tag));
            cMapas.PosicionActual = item.Position;
            cMapas.Carga(9);
        }

        private void bntPozo_Click(object sender, EventArgs e)
        {
            cMapas.ColocaPozos(cdatos.DiccionarioPozos);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            cMapas.Carga(trackBar1.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
        Text =    cMapas.Limpiar(textBox1.Text);
        }
    }
}
