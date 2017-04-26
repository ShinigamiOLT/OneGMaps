using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Maps
{
    public partial class Form1 : Form
    {
        OneMaps cMapas;
        OneMapsDatos cdatos = new OneMapsDatos();
        public int columna;
        private string Url1 = AppDomain.CurrentDomain.BaseDirectory + "pluginhost.html";
        private string Url2 = AppDomain.CurrentDomain.BaseDirectory + "MapasBurbujas.html";
        Dictionary<string, DataTable> dicPrincipal;
        public List<DataGridViewRow> RowEdit { get; set; }
        Dictionary<string, List<string>> dicCirculos;
        Dictionary<string, List<ObjetoSelecionable>> dicVecinos;
        DataTable TablaAreas;
        DataTable TablaAnotaciones;
        DevComponents.DotNetBar.Controls.DataGridViewX TablaAnotacion;
        DevComponents.DotNetBar.Controls.DataGridViewX TablaNuevopunto;
        DevComponents.DotNetBar.Controls.DataGridViewX TablaPozosVecinos;
        DevComponents.DotNetBar.Controls.DataGridViewX TablaPuntoSeleccionado;
        DevComponents.DotNetBar.Controls.DataGridViewX TablaBurbujas;
        DevComponents.DotNetBar.Controls.DataGridViewX TAreas;
        bool SoloUnaVez = true;
        userFiltroElemento filtro;
        DataTable dtPrincipal;
        DataTable dtPrincipalCiudades;
        List<string> Nombres;
        bool cerrado = true;
        System.Threading.Thread p1;
        System.Threading.Thread p2;
        cDiccionarios Metadatos;

        public Form1()
        {

            Metadatos = new cDiccionarios();
            InitializeComponent();
            cMapas = new OneMaps(gMapControl1);
            RowEdit = new List<DataGridViewRow>();
            TAreas = new DevComponents.DotNetBar.Controls.DataGridViewX();
            dicVecinos = new Dictionary<string, List<ObjetoSelecionable>>();
            TablaPozosVecinos = new DevComponents.DotNetBar.Controls.DataGridViewX();
            TablaAreas = new DataTable();
            dicPrincipal = new Dictionary<string, DataTable>();
            dicCirculos = new Dictionary<string, List<string>>();
            TablaAnotacion = new DevComponents.DotNetBar.Controls.DataGridViewX();
            TablaPuntoSeleccionado = new DevComponents.DotNetBar.Controls.DataGridViewX();
            TablaNuevopunto = new DevComponents.DotNetBar.Controls.DataGridViewX();
            TablaBurbujas = new DevComponents.DotNetBar.Controls.DataGridViewX();
            TablaNuevopunto.CellClick += new DataGridViewCellEventHandler(dataGridViewX1_CellClick);
            TablaNuevopunto.CellBeginEdit += new DataGridViewCellCancelEventHandler(dt_CellBeginEdit);
            TablaPuntoSeleccionado.CellClick += new DataGridViewCellEventHandler(dataGridViewX1_CellClick);
            TablaPuntoSeleccionado.CellBeginEdit += new DataGridViewCellCancelEventHandler(dt_CellBeginEdit);
            TablaPozosVecinos.CellClick += new DataGridViewCellEventHandler(dataGridViewX1_CellClick);
            TablaPozosVecinos.CellBeginEdit += new DataGridViewCellCancelEventHandler(dt_CellBeginEdit);
            cCargaDatos carga = new cCargaDatos(TablaPozosVecinos, TablaNuevopunto, TablaPuntoSeleccionado, TablaAreas, TablaBurbujas, TablaAnotacion, TAreas);
            p1 = new System.Threading.Thread(new System.Threading.ThreadStart(cargaTabla));
            p1.Start();
            p2 = new System.Threading.Thread(new System.Threading.ThreadStart(cargaTabla1));
            p2.Start();
            dtArea.TableName = "AREA";
            dtArea.Columns.Add("LATITUD");
            dtArea.Columns.Add("LONGITUD");
            dtArea.Columns.Add("ALTITUD");
           
        }

        private void cargaTabla1()
        {
            DataSet set = new DataSet();
            set.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "LocalizacionMexico.OneED");
            dtPrincipalCiudades = set.Tables[0];
        }

        private void cargaTabla()
        {
            DataSet set = new DataSet();
            set.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "LocalizacionPozo.OneClass");
            dtPrincipal = set.Tables[0];

            Nombres = (from DataRow row in dtPrincipal.Rows
                       select QuitaAcentos(row["Pozo"].ToString()).ToUpper()).ToList();

        }


        string QuitaAcentos(string texto)
        {
            string textoNormalizado = texto.Normalize(NormalizationForm.FormD);
            Regex reg = new Regex("[^a-zA-Z0-9-_ ]");
            return reg.Replace(textoNormalizado, "");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvDistancia.Rows.Add(2);
           lisG = new List<DataGridViewRow>();

            dgvInfoGeneral.Rows.Add(22);
            dgvInfoGeneral[0, 0].Selected = false;
            dgvInfoGeneral[0, 0].Value = "Uwi";
            dgvInfoGeneral[0, 1].Value = "Nombre";
            dgvInfoGeneral[0, 2].Value = "Alias";
            dgvInfoGeneral[0, 3].Value = "Campo";
            dgvInfoGeneral[0, 4].Value = "Cuenca";
            dgvInfoGeneral[0, 5].Value = "Ultima Formacion";
            dgvInfoGeneral[0, 6].Value = "Activo";
            dgvInfoGeneral[0, 7].Value = "Municipio";
            dgvInfoGeneral[0, 8].Value = "Estado";
            dgvInfoGeneral[0, 9].Value = "Fuente";
            dgvInfoGeneral[0, 10].Value = "Ubicación";
            dgvInfoGeneral[0, 11].Value = "Dirección";
            dgvInfoGeneral[0, 12].Value = "Plataforma - Equipo";
            dgvInfoGeneral[0, 13].Value = "Coordenadas";
            dgvInfoGeneral[0, 14].Value = "Tipo";
            dgvInfoGeneral[0, 15].Value = "Latitud";
            dgvInfoGeneral[0, 16].Value = "Longitud";
            dgvInfoGeneral[0, 17].Value = "Fuente";
            dgvInfoGeneral[0, 18].Value = "Tipo";
            dgvInfoGeneral[0, 19].Value = "Zona";
            dgvInfoGeneral[0, 20].Value = "X";
            dgvInfoGeneral[0, 21].Value = "Y";
            dgvInfoGeneral.Focus();
            dgvInfoGeneral[1, 1].Selected = true;

            dgvCiudades.Rows.Add(6);
            dgvCiudades[0, 0].Value = "Estado";
            dgvCiudades[0, 1].Value = "Municipio";
            dgvCiudades[0, 2].Value = "Colonia";
            dgvCiudades[0, 3].Value = "Latitud";
            dgvCiudades[0, 4].Value = "Longitud";
            dgvCiudades[0, 5].Value = "Población";


            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;

            cMapas.CMaps = gMapControl1;
            cMapas.PosicionActual = new GMap.NET.PointLatLng(18.239373, -93.905608);
            dataGridView1.DataSource = cdatos.TablaInfo;
            dataGridView1.DataMember = cdatos.TablaInfo.Tables[0].TableName;
            cMapas.Posicion();
            cMapas.Carga();
        }

        private void button1_Click(object sender, EventArgs e)
        {
       }

        /// <summary>
        /// Esta funcion se invoca desde el script, con la propiedad de window.external
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        public void CreaDistancia(double lat, double lon) 
        {
            if (columna > 1)
            {
                columna = 0;
              //  wbEarth.Document.InvokeScript("RemovePlaceMark");
              //  wbEarth.Document.InvokeScript("RemovePMark", new object[] { "La distancia es:" });

                dgvDistancia.Rows.Clear();
                dgvDistancia.Rows.Add(2);
            }
            else
            {
                dgvDistancia[0, columna].Value = lat;
                dgvDistancia[1, columna++].Value = lon;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                object[] arraryA = new object[2];
                object[] arraryB = new object[2];

                object[] Contenedor = new object[7];

                arraryA[0] = double.Parse(dgvDistancia[0, 0].Value.ToString());
                arraryA[1] = double.Parse(dgvDistancia[0, 1].Value.ToString());
                arraryB[0] = double.Parse(dgvDistancia[1, 0].Value.ToString());
                arraryB[1] = double.Parse(dgvDistancia[1, 1].Value.ToString());

                Contenedor[0] = arraryA;
                Contenedor[1] = arraryB;
                Contenedor[2] = "Distancia";
                Contenedor[3] = "";
                Contenedor[4] = "";
                Contenedor[5] = 2;
                Contenedor[6] = "";
               // wbEarth.Document.InvokeScript("CreaLinea", Contenedor);
                var distanc = distancia((double)arraryA[0], (double)arraryA[1], (double)arraryB[0], (double)arraryB[1]);

                object a = (double.Parse(dgvDistancia[0, 0].Value.ToString()) + double.Parse(dgvDistancia[0, 1].Value.ToString())) / 2;
                object b = (double.Parse(dgvDistancia[1, 0].Value.ToString()) + double.Parse(dgvDistancia[1, 1].Value.ToString())) / 2;
                object nom = "La distancia es: " + (Math.Round(distanc * 111.111, 3)).ToString() + " Km";
             //   wbEarth.Document.InvokeScript("CreaMarcaRoja", new object[] { nom, a, b });
            }
            catch
            {

            }
        }

        double distancia(double x1, double x2, double y1, double y2)
        { return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)); }

        private void button3_Click(object sender, EventArgs e)
        {
}

        private void button4_Click(object sender, EventArgs e)
        {

        }


       

        bool SoloUnaVezCiudades = true;
        int indexRow = 0;
        private void dgvCiudades_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (SoloUnaVezCiudades)
            {
                filtro = new userFiltroElemento();
                filtro.HandleDestroyed += filtro_HandleDestroyed;
                filtro.Lista_Unica.SelectedIndexChanged += otro;
                TextBox txt = e.Control as TextBox;
                txt.KeyDown += txt_KeyDownCiudades;

            } indexRow = dgvCiudades.CurrentRow.Index;
        }

        List<string> municipios;
        List<string> colonias;
        private void otro(object sender, EventArgs e)
        {
            dgvCiudades.EndEdit();
            var selec = filtro.Seleccionado.ToString();
            switch (indexRow)
            {
                case 0:
                    dgvCiudades[1, indexRow].Value = selec;
                    dgvCiudades.Refresh();
                    municipios = (from DataRow row in dtPrincipalCiudades.Rows
                                  where row["NOM_ENT"].ToString().ToUpper() == selec
                                  select row["NOM_MUN"].ToString()).Distinct().ToList();
                    break;
                case 1:
                    dgvCiudades[1, indexRow].Value = selec;
                    string estado=dgvCiudades[1, 0].Value.ToString();
                    colonias = (from DataRow row in dtPrincipalCiudades.Rows
                                where row["NOM_MUN"].ToString().ToUpper() == selec && row["NOM_ENT"].ToString().ToUpper() == estado
                                select row["NOM_LOC"].ToString()).Distinct().ToList();

                    var mu6 = (from DataRow row in dtPrincipalCiudades.Rows
                               where row["NOM_MUN"].ToString().ToUpper() == selec
                               select row).ToList();

                    dgvCiudades[1, indexRow + 2].Value = mu6[0]["LAT_DEC"];
                    dgvCiudades[1, indexRow + 3].Value = mu6[0]["LON_DEC"];
                    dgvCiudades[1, indexRow + 4].Value = mu6[0]["Z2"];

                  //  wbEarth.Document.InvokeScript("CreaMarca", new object[] { selec, mu6[0]["LAT_DEC"], mu6[0]["LON_DEC"] });
                    string Nombre =selec;
                    double X = Convert.ToDouble(mu6[0]["LAT_DEC"]);
                    double Y = Convert.ToDouble(mu6[0]["LON_DEC"]);
                    cMapas.PosicionActual = (cMapas.Puntos(new Tuple<string, double, double>(Nombre, X, Y), true, GMap.NET.WindowsForms.Markers.GMarkerGoogleType.red_pushpin)).Position;


                    break;

                case 2:
                    
                    dgvCiudades[1, indexRow].Value = selec;
                    var ttt = (from DataRow row in dtPrincipalCiudades.Rows
                               where row["NOM_LOC"].ToString().ToUpper() == selec
                               select row).ToList();
                    dgvCiudades[1, indexRow + 1].Value = ttt[0]["LAT_DEC"];
                    dgvCiudades[1, indexRow + 2].Value = ttt[0]["LON_DEC"];
                    dgvCiudades[1, indexRow + 3].Value = ttt[0]["Z2"];


                  //  wbEarth.Document.InvokeScript("CreaMarca", new object[] { selec, "", ttt[0]["LAT_DEC"], ttt[0]["LON_DEC"] });
                    string Nombre_ = selec;
                    double X_ = Convert.ToDouble(ttt[0]["LAT_DEC"]);
                    double Y_ = Convert.ToDouble(ttt[0]["LON_DEC"]);
                    cMapas.PosicionActual = (cMapas.Puntos(new Tuple<string, double, double>(Nombre_, X_, Y_), true, GMap.NET.WindowsForms.Markers.GMarkerGoogleType.red_pushpin)).Position;

                    break;
            }
        }

        private void dgvCiudades_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        void txt_KeyDownCiudades(object sender, KeyEventArgs e)
        {
            while (p1.IsAlive) ;
            string texto = "";
            if (dgvCiudades.CurrentCell.ColumnIndex == 1)
            {
                switch (dgvCiudades.CurrentCell.RowIndex)
                {
                    case 0: dgvCiudades.CommitEdit(DataGridViewDataErrorContexts.Parsing);
                        if (dgvCiudades[1, 0].Value != null)
                        {
                            texto = dgvCiudades[1, 0].Value.ToString().ToUpper() + (e.KeyData != Keys.Back ? ((char)e.KeyValue).ToString().ToUpper() : "");
                            List<string> aunNombres = new List<string>();
                            aunNombres.Add("Aguascalientes"); aunNombres.Add("Baja California"); aunNombres.Add("Baja California Sur"); aunNombres.Add("Campeche"); aunNombres.Add("Chiapas");
                            aunNombres.Add("Chihuahua"); aunNombres.Add("Coahuila de Zaragoza"); aunNombres.Add("Colima");
                            aunNombres.Add("Distrito Federal"); aunNombres.Add("Durango"); aunNombres.Add("Guanajuato"); aunNombres.Add("Guerrero"); aunNombres.Add("Hidalgo"); aunNombres.Add("Jalisco"); aunNombres.Add("México");
                            aunNombres.Add("Michoacán de Ocampo"); aunNombres.Add("Morelos"); aunNombres.Add("Nayarit"); aunNombres.Add("Nuevo León"); aunNombres.Add("Oaxaca"); aunNombres.Add("Puebla"); aunNombres.Add("Querétaro");
                            aunNombres.Add("Quintana Roo"); aunNombres.Add("San Luis Potosí"); aunNombres.Add("Sinaloa"); aunNombres.Add("Sonora"); aunNombres.Add("Tabasco"); aunNombres.Add("Tamaulipas"); aunNombres.Add("Tlaxcala");
                            aunNombres.Add("Veracruz de Ignacio de la Llave"); aunNombres.Add("Yucatán"); aunNombres.Add("Zacatecas");

                            List<ObjetoSelecionable> AuxNombres = (from name in aunNombres
                                                                   where Regex.IsMatch(name.ToUpper(), @"^" + texto, RegexOptions.None)
                                                                   select new ObjetoSelecionable(name)).ToList();
                            if (AuxNombres.Count == 0) return;

                            if (cerrado)
                            {
                                filtro = new userFiltroElemento(ref AuxNombres, 1);
                                filtro.HandleDestroyed += filtro_HandleDestroyed;
                                filtro.Lista_Unica.SelectedIndexChanged += otro; filtro.Actualiza(AuxNombres, 1);
                                filtro.EnForm(true);
                                (sender as TextBox).Focus();
                                cerrado = false;
                            }
                            else filtro.Actualiza(AuxNombres, 1);
                            SoloUnaVezCiudades = false;
                        }
                        break;

                    case 1:
                        dgvCiudades.CommitEdit(DataGridViewDataErrorContexts.Parsing);
                        if (dgvCiudades[1, indexRow].Value == null) return;

                        string nomT = dgvCiudades[1, indexRow].Value.ToString();
                        texto = dgvCiudades[1, indexRow].Value.ToString().ToUpper() + (e.KeyData != Keys.Back ? ((char)e.KeyValue).ToString().ToUpper() : "");

                        List<ObjetoSelecionable> Municipios = (from name in municipios
                                                               where Regex.IsMatch(name.ToUpper(), @"^" + texto, RegexOptions.None)
                                                               select new ObjetoSelecionable(name)).ToList();
                        if (Municipios.Count == 0) { return; }

                        if (cerrado)
                        {
                            filtro = new userFiltroElemento(ref Municipios, 1);
                            filtro.HandleDestroyed += filtro_HandleDestroyed;
                            filtro.Lista_Unica.SelectedIndexChanged += otro; filtro.Actualiza(Municipios, 1);
                            filtro.EnForm(true);
                            (sender as TextBox).Focus();
                            cerrado = false;
                        }
                        else filtro.Actualiza(Municipios, 1);
                        SoloUnaVezCiudades = false;
                        break;
                    case 2:

                        dgvCiudades.CommitEdit(DataGridViewDataErrorContexts.Parsing);
                        if (dgvCiudades[1, indexRow].Value == null) return;
                        string nomCol = dgvCiudades[1, indexRow].Value.ToString();
                        texto = dgvCiudades[1, indexRow].Value.ToString().ToUpper() + (e.KeyData != Keys.Back ? ((char)e.KeyValue).ToString().ToUpper() : "");

                        List<ObjetoSelecionable> Colonias = (from name in colonias
                                                             where Regex.IsMatch(name.ToUpper(), @"^" + texto, RegexOptions.None)
                                                             select new ObjetoSelecionable(name)).ToList();
                        if (Colonias.Count == 0) { return; }

                        if (cerrado)
                        {
                            filtro = new userFiltroElemento(ref Colonias, 1);
                            filtro.HandleDestroyed += filtro_HandleDestroyed;
                            filtro.Lista_Unica.SelectedIndexChanged += otro; filtro.Actualiza(Colonias, 1);
                            filtro.EnForm(true);
                            (sender as TextBox).Focus();
                            cerrado = false;
                        }
                        else filtro.Actualiza(Colonias, 1);
                        SoloUnaVezCiudades = false;

                        break;
                }
            }
        }

        private void dgvInfoGeneral_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (SoloUnaVez)
            {
                filtro = new userFiltroElemento();
                filtro.HandleDestroyed += filtro_HandleDestroyed;
                filtro.Lista_Unica.SelectedIndexChanged += Lista_Unica_SelectedIndexChanged;
                TextBox txt = e.Control as TextBox;
                txt.KeyDown += txt_KeyDown;
            }
        }

        void txt_KeyDown(object sender, KeyEventArgs e)
        {
            while (p1.IsAlive) ;
            string texto = "";
            if (dgvInfoGeneral.CurrentCell.ColumnIndex == 1 && dgvInfoGeneral.CurrentCell.RowIndex == 1)
            {

                dgvInfoGeneral.CommitEdit(DataGridViewDataErrorContexts.Parsing);
                if (dgvInfoGeneral[1, 1].Value != null)
                {
                    texto = dgvInfoGeneral[1, 1].Value.ToString().ToUpper() + (e.KeyData != Keys.Back ? ((char)e.KeyValue).ToString().ToUpper() : "");
                    List<ObjetoSelecionable> AuxNombres = (from name in Nombres
                                                           where Regex.IsMatch(name, @"^" + texto, RegexOptions.None)
                                                           select new ObjetoSelecionable(name)).ToList();

                    if (cerrado)
                    {
                        filtro = new userFiltroElemento(ref AuxNombres, 1);
                        filtro.HandleDestroyed += filtro_HandleDestroyed;
                        filtro.Lista_Unica.SelectedIndexChanged += Lista_Unica_SelectedIndexChanged; filtro.Actualiza(AuxNombres, 1);
                        filtro.EnForm(true);
                        (sender as TextBox).Focus();
                        cerrado = false;
                    }
                    else filtro.Actualiza(AuxNombres, 1);
                    SoloUnaVez = false;
                }

            }
        }

        void Lista_Unica_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selec = filtro.Seleccionado.ToString();
            int index = Nombres.FindIndex(x => x == selec);
            llenaDatos(dtPrincipal.Rows[index], true);
        }

        void filtro_HandleDestroyed(object sender, EventArgs e)
        { dgvInfoGeneral.EndEdit(); dgvInfoGeneral[1, 1].Value = ""; cerrado = true; }

        void llenaDatos(DataRow Row, bool flag)
        {
            TablaPuntoSeleccionado.Rows.Add(true,TablaPuntoSeleccionado.RowCount.ToString(), Row["Pozo"], "", Row["LAT"], Row["LON"], 0);
            dgvInfoGeneral[1, 0].Value = Row["Uwi"];
            dgvInfoGeneral[1, 1].Value = Row["Pozo"];
            dgvInfoGeneral[1, 2].Value = Row["Alias"];
            dgvInfoGeneral[1, 3].Value = Row["Campo"];
            dgvInfoGeneral[1, 4].Value = "";
            dgvInfoGeneral[1, 5].Value = Row["Ultima Formacion Perforada"];
            dgvInfoGeneral[1, 6].Value = Row["Activo"];
            dgvInfoGeneral[1, 7].Value = "";
            dgvInfoGeneral[1, 8].Value = "";
            dgvInfoGeneral[1, 9].Value = "";
            dgvInfoGeneral[1, 10].Value = Row["Ubicacion"];
            dgvInfoGeneral[1, 11].Value = "";
            dgvInfoGeneral[1, 12].Value = Row["Numero de equipo"];
            dgvInfoGeneral[1, 13].Value = "";
            dgvInfoGeneral[1, 14].Value = "GEODESICAS";
            dgvInfoGeneral[1, 15].Value = Row["LAT"];
            dgvInfoGeneral[1, 16].Value = Row["LON"];
            dgvInfoGeneral[1, 17].Value = "Fuente";
            dgvInfoGeneral[1, 18].Value = "UTM";
            dgvInfoGeneral[1, 19].Value = Row["Zona"];
            dgvInfoGeneral[1, 20].Value = Row["Coord. X"];
            dgvInfoGeneral[1, 21].Value = Row["Coord. Y"];
            double X = 0, Y = 0;
            if (flag)
                if (double.TryParse(Row["LAT"].ToString(), out X) && double.TryParse(Row["LON"].ToString(), out Y))
                {
                    string Nombre = Row["Pozo"].ToString();
                    string Apellido = Row["Campo"].ToString();

                   // wbEarth.Document.InvokeScript("CreaMarca", new object[] { Nombre, X, Y });

                   cMapas.PosicionActual=(  cMapas.Puntos(new Tuple<string, double, double>(Nombre,X,Y),true,GMap.NET.WindowsForms.Markers.GMarkerGoogleType.red_pushpin)).Position;
                    cMapas.CirculoFormula(cMapas.PosicionActual,Convert.ToDouble(textBox1.Text)  );// ("Hola",Color.Red,cMapas.UltimoMarcador.Position, 5000 /111.111f, 1,"") ;//(cMapas.UltimoMarcador,5000);
                  //  cMapas.CMaps.Zoom = 12;
                }
        }

        public void agregarAreaRadio(string nomb, double valX, double valY, double radio, string id)
        {
            List<string> circuloC = new List<string>();
            double AuxRadio = radio / 111.111f;
            try
            {
                double xTabla = 0;
                double yTabla = 0;
                foreach (DataRow row in dtPrincipal.Rows)
                    if (double.TryParse(row["LAT"].ToString(), out xTabla) && double.TryParse(row["LON"].ToString(), out yTabla))
                        if (distancia(valX, xTabla, valY, yTabla) <= AuxRadio)
                        {
                          //  wbEarth.Document.InvokeScript("CreaMarcaAnillo", new object[] { row["Pozo"].ToString() + id, "", xTabla, yTabla });
                            circuloC.Add(row["Pozo"].ToString()+id); TablaAreas.Rows.Add(nomb, row["Pozo"].ToString(), xTabla, yTabla);
                        }
                if (!dicCirculos.ContainsKey(nomb))
                    dicCirculos.Add(nomb , circuloC);
                else
                {
                    dicCirculos[nomb] = dicCirculos[nomb].Concat(circuloC).ToList();
                }
            }
            catch
            { }
        }

     
        private void btnCargaMax_Click(object sender, EventArgs e)
        {
            List<ObjetoSelecionable> Seleccion = new List<ObjetoSelecionable>();
            Seleccion.Add(new ObjetoSelecionable("Anotaciones"));
            Seleccion.Add(new ObjetoSelecionable("Burbujas"));
            Seleccion.Add(new ObjetoSelecionable("Marcas"));

            userFiltroElemento seleccion = new userFiltroElemento(ref Seleccion, 1);
            seleccion.EnForm(false);

            if (seleccion.cerrado) return;

            var se = seleccion.Seleccionado.ToString();
            
            Notificacion.Visible = true;
            Load_txt Datos = new Load_txt(dicPrincipal, se);
            Datos.ShowDialog();
            Notificacion.Visible = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
         //   wbEarth.Refresh();
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            if (dgvInfoGeneral[1, 1].Value != null)
            {
                Notificacion.Visible = true;
                Notificacion.BalloonTipText = "Espere, se estan dibujando: Pozos vecinos";
                Notificacion.ShowBalloonTip(29000);
                string NomPadre = dgvInfoGeneral[1, 1].Value.ToString().ToUpper();
                string texto = dgvInfoGeneral[1, 1].Value.ToString().ToUpper().Split('-')[0];
                List<ObjetoSelecionable> AuxNombres = (from name in Nombres
                                                       where Regex.IsMatch(name, @"^" + texto, RegexOptions.None)
                                                       select new ObjetoSelecionable(name)).ToList();


                filtro = new userFiltroElemento(ref AuxNombres, 0);
                filtro.cmbBuscar.Text = texto;
                filtro.EnForm(false);
                if (filtro.cerrado) return;

                if (sender is TextBox)
                    (sender as TextBox).Focus();


                var nuevalista = filtro.ListaPozosTotales.FindAll(x => x.Estado);
                if (!dicVecinos.ContainsKey(NomPadre))
                {
                    dicVecinos.Add(NomPadre, nuevalista);
                    TablaPozosVecinos.Rows.Add(true, NomPadre,"", " X ");
                }
                else dicVecinos[NomPadre] = nuevalista;
                foreach (ObjetoSelecionable selec in nuevalista)
                {
                    var sele = selec.ToString();
                    int index = Nombres.FindIndex(x => x == sele);
                    var Row = dtPrincipal.Rows[index];
                    try
                    {
                        string Nombre = Row["Pozo"].ToString();
                        double X = Convert.ToDouble(Row["LAT"]);
                        double Y = Convert.ToDouble(Row["LON"]);
                      //  wbEarth.Document.InvokeScript("CreaMarca", new object[] { Row["Pozo"], Row["LAT"], Row["LON"] });

                        cMapas.Puntos(new Tuple<string, double, double>(Nombre, X, Y), true, GMap.NET.WindowsForms.Markers.GMarkerGoogleType.yellow_pushpin);
                    }
                  catch
                        { }
                   

                }
                cMapas.PosicionActual = cMapas.UltimoMarcador.Position;
              //  cMapas.CMaps.Zoom = 18;
                Notificacion.Visible = false;

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                foreach (KeyValuePair<string, DataTable> dic in dicPrincipal)
                {
                    ds.Tables.Add(dic.Value);
                }

                one_form_Produccion_OrigenDatos ventana = new one_form_Produccion_OrigenDatos(99, ds, new NotifyIcon());
                ventana.ShowDialog();
                ds.Tables.Clear();
                ds.Dispose();
            }
            catch
            {
            }
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            string valor = dgvInfoGeneral[1, 1].Value.ToString().ToUpper().Split('-')[0];
       //     wbEarth.Document.InvokeScript("RemovePMark", new object[] { valor });
        }

       

        private void btnTablaCirculos_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(TablaAreas);
            one_form_Produccion_OrigenDatos ventana = new one_form_Produccion_OrigenDatos(99, ds, new NotifyIcon());
            ventana.ShowDialog();
        }

      

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            try
            {
                var y = dtPrincipal.Select("Campo = 'RIO NUEVO'").CopyToDataTable();
                double element = 0;
                double maxi = (from DataRow row in y.Rows
                               where double.TryParse(row["Elevacion KB(m)"].ToString(), out element)
                               select element).ToList().Max();

                y.Columns.Add("Tamano Burbuja");
                foreach (DataRow row in y.Rows)
                {
                    if (double.TryParse(row["Elevacion KB(m)"].ToString(), out element))
                    {
                        double burbuja = element / maxi * 0.5;
                        row["Tamano Burbuja"] = burbuja;
                     //   wbEarth.Document.InvokeScript("creaCirculos", new object[] { row["Pozo"].ToString(), double.Parse(row["LAT"].ToString()), double.Parse(row["LON"].ToString()), burbuja / 111.111f });
                    }
                }

            }
            catch
            { }
        }

        contenedor contendor = new contenedor();
        double radioAnterior = 0;
        private void btnPseleccionado_Click(object sender, EventArgs e)
        {
            //if (dgvInfoGeneral[1, 1].Value == null) return;
            RowEdit.Clear();
            bool cerrado;
            ucMaestro aux;
            if (dicCirculos.Count < 0) return;
            if (sender.ToString().Contains("Pozo seleccionado")) aux = new ucMaestro(TablaPuntoSeleccionado);
            else aux = new ucMaestro(TablaNuevopunto);

            aux.Dock = DockStyle.Fill;
            contendor.Controls.Clear();
            contendor.Controls.Add(aux);
            contendor.ShowDialog();
            if (contendor.cerrado) return;
            cerrado = aux.cerrado;
            try
            {
                foreach (DataGridViewRow auxRow in RowEdit)
                {
                    string NomPadre = auxRow.Cells["Nombre"].Value.ToString().ToUpper();
                    if (cerrado)
                    {
                        double radio = double.Parse(auxRow.Cells["Radio (KM)"].Value.ToString());
                        string Color1 = auxRow.Cells[3].Tag.ToString();
                        double x = double.Parse(auxRow.Cells["LAT"].Value.ToString());
                        double y = double.Parse(auxRow.Cells["LON"].Value.ToString());
                        string id = "_"+auxRow.Cells["ID"].Value.ToString();
                        if (dicCirculos.Count > 0)
                        {
                            elimina(NomPadre + id);
                            auxRow.Cells["ID"].Value = int.Parse(auxRow.Cells["ID"].Value.ToString()) + 1;
                            id = "_" + auxRow.Cells["ID"].Value.ToString();
                        }
                        string visible = auxRow.Cells["Visible"].Value.ToString();
                        if (visible == "True")
                        {
                          //  wbEarth.Document.InvokeScript("CreaMarcaRoja", new object[] { NomPadre + id, x, y });
                          //  wbEarth.Document.InvokeScript("CreaCirculo", new object[] { "Anillo" + NomPadre + id, Color1, x, y, radio / 111.111, 2 });
                            agregarAreaRadio(NomPadre + id, x, y, radio, id);

                        }
                        else if (dicCirculos.Count > 0)
                        {
                            string nombre = auxRow.Cells["Nombre"].Value.ToString().ToUpper();
                            //elimina(nombre);

                            if (dicCirculos.Keys.Contains(nombre))
                            {
                                var lista = dicCirculos[nombre];
                                foreach (string nam in lista)
                                {
                                   // wbEarth.Document.InvokeScript("EliminaMarcaCirculo", new object[] { nam });
                                }
                                dicCirculos.Remove(nombre);
                            }
                        }
                    }
                }
            }
            catch 
            { }
        }

        void elimina(string NomPadre)
        {
            if (dicCirculos.ContainsKey(NomPadre))
            {
                var lista = dicCirculos[NomPadre];
                foreach (string nam in lista)
                {
                 //   wbEarth.Document.InvokeScript("EliminaMarcaCirculo", new object[] { nam });
                }

                dicCirculos.Remove(NomPadre);
            }
          //  wbEarth.Document.InvokeScript("EliminaMarcaCirculo", new object[] { "Anillo" + NomPadre });
           // wbEarth.Document.InvokeScript("EliminaMarcaCirculo", new object[] { NomPadre });
            radioAnterior++;
        }

        void dt_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1 = (sender as DevComponents.DotNetBar.Controls.DataGridViewX);
            if (!RowEdit.Contains(dataGridViewX1.Rows[e.RowIndex]))
                RowEdit.Add(dataGridViewX1.Rows[e.RowIndex]);
        }

        void eliminaMarcas(string name)
        {
            foreach (ObjetoSelecionable onj in dicVecinos[name].FindAll(x => !x.Estado))
            {
                string cad = onj.ToString();
               // wbEarth.Document.InvokeScript("EliminaMarcaCirculo", new object[] { cad });
            }
        }

        void agregaMarcas(List<ObjetoSelecionable> listaAgregar)
        {
            foreach (ObjetoSelecionable selec in listaAgregar)
            {
                var sele = selec.ToString();
                int index = Nombres.FindIndex(x => x == sele);
                var Row = dtPrincipal.Rows[index];
                try
                {
                  //  wbEarth.Document.InvokeScript("CreaMarca", new object[] { Row["Pozo"], Row["LAT"], Row["LON"] });
                    string Nombre = Row["Pozo"].ToString();
                    double X = Convert.ToDouble(Row["LAT"]);
                    double Y = Convert.ToDouble(Row["LON"]);

                    cMapas.PosicionActual = (cMapas.Puntos(new Tuple<string, double, double>(Nombre, X, Y), true, GMap.NET.WindowsForms.Markers.GMarkerGoogleType.red_pushpin)).Position;

                }
                catch { }
                }
        }
            

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1 = (sender as DevComponents.DotNetBar.Controls.DataGridViewX);
                if (!RowEdit.Contains(dataGridViewX1.Rows[e.RowIndex]))
                    RowEdit.Add(dataGridViewX1.Rows[e.RowIndex]);
                if (e.ColumnIndex == 2)
                {
                    string texto = dataGridViewX1.Columns[e.ColumnIndex].Name;
                    if (texto == "Pozos Vecinos")
                    {
                        string name = dataGridViewX1[1, e.RowIndex].Value.ToString();
                        filtro.Actualiza(dicVecinos[name], 0);
                        filtro.EnForm(false);

                        if (filtro.cerrado)
                        {
                            filtro.cerrado = false;
                            return;
                        }
                        //eliminaMarcas(name);
                        //agregaMarcas(filtro.ListaPozosTotales.FindAll(x => x.Estado));

                    }
                    else if(texto == "Color")
                    {
                        ColorDialog Colornuevo = new ColorDialog();
                        if (Colornuevo.ShowDialog() == DialogResult.OK)
                        {
                            var color = String.Format("{0:X2}{1:X2}{2:X2}{3:X2}", Colornuevo.Color.A, Colornuevo.Color.B, Colornuevo.Color.G, Colornuevo.Color.R);
                            dataGridViewX1[e.ColumnIndex, e.RowIndex].Style.BackColor = Colornuevo.Color;
                            dataGridViewX1[e.ColumnIndex, e.RowIndex].Tag = color;
                            dataGridViewX1[e.ColumnIndex, e.RowIndex].Value = "";
                            dataGridViewX1[e.ColumnIndex, e.RowIndex].Selected = false;
                        }
                    }
                }
                else if (e.ColumnIndex == 3)
                {
                    string texto = dataGridViewX1.Columns[e.ColumnIndex].Name;

                    if (texto == "Eliminar")
                    {
                        string name = dataGridViewX1[1, e.RowIndex].Value.ToString();
                        var nuevalista = dicVecinos[name].FindAll(x => x.Estado);

                        foreach (ObjetoSelecionable selec in dicVecinos[name])
                        {
                            var sele = selec.ToString();
                          //  wbEarth.Document.InvokeScript("EliminaMarcaCirculo", new object[] { sele });
                        }
                        //wbEarth.Document.InvokeScript("EliminaMarcaCirculo", new object[] { name });
                        dicVecinos.Remove(name);
                        RowEdit.Remove(dataGridViewX1.Rows[e.RowIndex]);
                        dataGridViewX1.Rows.RemoveAt(e.RowIndex);
                    }
                    else if (texto == "Color")
                    {
                        ColorDialog Colornuevo = new ColorDialog();
                        if (Colornuevo.ShowDialog() == DialogResult.OK)
                        {
                            var color = String.Format("{0:X2}{1:X2}{2:X2}{3:X2}", Colornuevo.Color.A, Colornuevo.Color.B, Colornuevo.Color.G, Colornuevo.Color.R);
                            dataGridViewX1[e.ColumnIndex, e.RowIndex].Style.BackColor = Colornuevo.Color;
                            dataGridViewX1[e.ColumnIndex, e.RowIndex].Tag = color;
                            dataGridViewX1[e.ColumnIndex, e.RowIndex].Value = "";
                            dataGridViewX1[e.ColumnIndex, e.RowIndex].Selected = false;
                        }
                    }
                    
                }
                else if (dataGridViewX1.Columns[e.ColumnIndex].Name == "Eliminar")
                {
                    string name = dataGridViewX1[1, e.RowIndex].Value.ToString().ToUpper();
                    if (dicCirculos.Count > 0 && dicCirculos.Keys.Contains(name))
                    {

                        foreach (string selec in dicCirculos[name])
                        {
                         //   wbEarth.Document.InvokeScript("EliminaMarcaCirculo", new object[] { selec });
                        }
                      //  wbEarth.Document.InvokeScript("EliminaMarcaCirculo", new object[] { "Anillo" + name });
                      //  wbEarth.Document.InvokeScript("EliminaMarcaCirculo", new object[] { name });
                        dicVecinos.Remove(name);
                        RowEdit.Remove(dataGridViewX1.Rows[e.RowIndex]);
                        dataGridViewX1.Rows.RemoveAt(e.RowIndex);
                    }
                }
                
            }
                
            catch 
            { }
        }



        private void buttonItem3_Click_2(object sender, EventArgs e)
        {
            try
            {
                ucMaestro pozos = new ucMaestro(TablaPozosVecinos);
                pozos.Dock = DockStyle.Fill;
                contendor.Controls.Clear();
                contendor.Controls.Add(pozos);
                contendor.ShowDialog();

                foreach (DataGridViewRow roll in RowEdit)
                {
                    string visible = roll.Cells["Visible"].Value.ToString();
                    if (visible == "True")
                    {
                        string nombre = roll.Cells["Nombre"].Value.ToString();
                        var nuevalista = dicVecinos[nombre].FindAll(x => x.Estado);

                        foreach (ObjetoSelecionable selec in dicVecinos[nombre].FindAll(x => !x.Estado))
                        {
                            var sele = selec.ToString();
                          //  wbEarth.Document.InvokeScript("EliminaMarcaCirculo", new object[] { sele });
                        }

                        foreach (ObjetoSelecionable selec in nuevalista)
                        {
                            var sele = selec.ToString();
                            int index = Nombres.FindIndex(x => x == sele);
                            var Row = dtPrincipal.Rows[index];
                          //  wbEarth.Document.InvokeScript("CreaMarca", new object[] { Row["Pozo"], Row["LAT"], Row["LON"] });
                            string Nombre = Row["Pozo"].ToString();
                            double X= Convert.ToDouble(Row["LAT"]);
                            double Y = Convert.ToDouble(Row["LAT"]);
                            cMapas.PosicionActual = (cMapas.Puntos(new Tuple<string, double, double>(Nombre, X, Y), true, GMap.NET.WindowsForms.Markers.GMarkerGoogleType.red_pushpin)).Position;

                        }

                    }
                    else
                    {
                        string nombre = roll.Cells["Nombre"].Value.ToString();
                        var nuevalista = dicVecinos[nombre];
                        foreach (ObjetoSelecionable selec in nuevalista)
                        {
                          //  wbEarth.Document.InvokeScript("EliminaMarcaCirculo", new object[] { selec.ToString() });
                        }
                    }
                }
            }
            catch
            { }
        }

        List<DataGridViewRow> lisG;
        private void buttonItem5_Click(object sender, EventArgs e)
        {
            //contendor.Size = new Size(310, 185);
            //ucAnotaciones anotacion = new ucAnotaciones(dicPrincipal, wbEarth, Metadatos,Notificacion);
            //anotacion.Dock = DockStyle.Fill;
            //contendor.Controls.Clear();
            //contendor.Controls.Add(anotacion);
            //contendor.ShowDialog();
            //contendor.Size = new Size(665, 300);
            ucAnotacion ANOTA = new ucAnotacion(dicPrincipal, TablaAnotacion, new WebBrowser(), Notificacion, Metadatos, lisG);
            ANOTA.Dock = DockStyle.Fill;
            contendor.Controls.Clear();
            contendor.Controls.Add(ANOTA);
            contendor.Size = new Size(800, 270);
            contendor.ShowDialog();

        }

        DevComponents.DotNetBar.Controls.DataGridViewX dgvAux;

        private void btnCiudades_Click(object sender, EventArgs e)
        {
            if (btnCiudades.Text == "Busqueda Ciudades")
            {
                dgvAux = (DevComponents.DotNetBar.Controls.DataGridViewX)controlContainerItem2.Control;
                controlContainerItem2.Control = dgvCiudades;
                btnCiudades.Text = "Busqueda Pozos";
            }
            else
            {
                DevComponents.DotNetBar.Controls.DataGridViewX uax = (DevComponents.DotNetBar.Controls.DataGridViewX)controlContainerItem2.Control;
                controlContainerItem2.Control = dgvAux;
                dgvAux = uax;
                btnCiudades.Text = "Busqueda Ciudades";
            }
            panel_final.Refresh();
        }

        private void btnMarcas_Click(object sender, EventArgs e)
        {
            contendor.Size = new Size(310, 185);
            ucMarcas MArcas = new ucMarcas(dicPrincipal,new WebBrowser());
            MArcas.Dock = DockStyle.Fill;
            contendor.Controls.Clear();
            contendor.Controls.Add(MArcas);
            contendor.ShowDialog();
            contendor.Size = new Size(665, 300);
           
        }

        private void btnGuardarTabla_Click(object sender, EventArgs e)
        {

            if (dicPrincipal.Count > 0)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "OneMaps files (*.OneMaps)|*.OneMaps";
                saveDialog.FilterIndex = 2;
                saveDialog.RestoreDirectory = true;
                saveDialog.InitialDirectory = "C:\\User\\";
                saveDialog.FileName = "OneMaps File";
                saveDialog.Title = "OneMaps Export";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    Notificacion.Visible = true;
                    Notificacion.ShowBalloonTip(5000);
                    DataSet set = new DataSet();
                    foreach (KeyValuePair<string, DataTable> valor in dicPrincipal)
                    {
                        valor.Value.TableName = valor.Key;
                        set.Tables.Add(valor.Value);
                    }

                    DataTable dtBurbuja = new DataTable("Diccionario");
                    dtBurbuja.Columns.Add("Nombre", typeof(string));
                    dtBurbuja.Columns.Add("Tipo", typeof(string));
                    dtBurbuja.Columns.Add("Titulo", typeof(string));
                    dtBurbuja.Columns.Add("X", typeof(string));
                    dtBurbuja.Columns.Add("Y", typeof(string));
                    dtBurbuja.Columns.Add("Variable", typeof(string));
                    dtBurbuja.Columns.Add("Color", typeof(string));


                    foreach (KeyValuePair<string, List<string>> punto in Metadatos.Diccionario)
                    { 
                        punto.Value.Insert(0, punto.Key);
                        object[] ok = new object[punto.Value.Count];
                        for (int i = 0; i < ok.Length; ok[i] = punto.Value[i], i++) ;
                        dtBurbuja.Rows.Add(ok);
                    }
                    set.Tables.Add(dtBurbuja);


                    set.WriteXml(saveDialog.FileName, System.Data.XmlWriteMode.WriteSchema);

                    Notificacion.Visible = false;
                }
            }
        }

        List<DataGridViewRow> RowEdit1 = new List<DataGridViewRow>();
        private void btnburbujas_Click_1(object sender, EventArgs e)
        {
            if (dicPrincipal.Count > 0)
            {
                ucMasters master = new ucMasters(dicPrincipal, TablaBurbujas, new WebBrowser(), Notificacion, RowEdit1);
                master.Dock = DockStyle.Fill;
                contendor.Controls.Clear();
                contendor.Controls.Add(master);
                contendor.Size = new Size(1000, 270);
                contendor.ShowDialog();
            }
        }

        DataTable dtArea = new DataTable();
        public void CalularArea(double LAT, double LON, double ALT)
        {
            dtArea.Rows.Add(LAT, LON, ALT);
        }

        private void buttonItem3_Click_3(object sender, EventArgs e)
        {
            try
            {
                dtArea.Rows.Clear();
                fnSeleccionAreas seleccionAreas = new fnSeleccionAreas(dtArea, dicPrincipal, TAreas,new WebBrowser());
               // wbEarth.Document.InvokeScript("EstablecePoligono", new object[] { true });
                seleccionAreas.Show(this);
            }
            catch
            { }
        }

        

        public void cambiaTitulo(string nombre)
        { //this.Text = (float.Parse(nombre)/1000f).ToString(); 
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog AbreArchivo = new OpenFileDialog();

                AbreArchivo.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                AbreArchivo.Filter = "OneMaps files (*.OneMaps)|*.OneMaps";
                if (AbreArchivo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string x = AbreArchivo.FileName.Split('.')[1].ToUpper();
                    Notificacion.Visible = true;
                    Notificacion.ShowBalloonTip(5000);
                    this.Refresh();
                    switch (x)
                    {

                        case "ONEMAPS": abreXmlEtl(AbreArchivo.FileName);

                            break;

                        default: MessageBox.Show("Actualmente No se puede Abrir el archivo, Puede ser una versión Obsoleta. Puedes cargarlo con \"Importar Datos\"");
                            break;



                    }
                }
                Notificacion.Visible = false;
            }
            catch { MessageBox.Show("Actualmente No se puede Abrir el archivo, Puede ser una versión Obsoleta. Puedes cargarlo con \"Importar Datos\""); }
        }

        public void abreXmlEtl(string cadena)
        {
            try
            {
                DataSet dset = new DataSet();
                dset.ReadXml(cadena);
                DataTable dtMeta = dset.Tables["Diccionario"];
                foreach (DataRow row in dtMeta.Rows)
                {
                    List<string> ldlfldsflsdf = new List<string>();
                    var xValor = row.ItemArray;
                    for(int i = 1; i < xValor.Length; i++)
                        ldlfldsflsdf.Add(xValor[i].ToString());
                    Metadatos.addDictionary(row[0].ToString(), ldlfldsflsdf);
                }

                foreach (DataTable dt in dset.Tables)
                {
                    string nombreTabla = dt.TableName;
                    if (!dicPrincipal.ContainsKey(nombreTabla) )
                    {
                        if (nombreTabla != "Diccionario")
                        {
                            dt.TableName = dt.TableName.Split(':')[1];
                            dicPrincipal.Add(nombreTabla, dt);
                        }
                    }
                }
                dset.Tables.Clear();
                dset.Dispose();
            }
            catch
            { }

        }

        private void btnAreas_Click(object sender, EventArgs e)
        {
            try
            {
                if (dicPrincipal.Values.Count > 0)
                {
                    ucArea areas = new ucArea(dicPrincipal, TAreas, new WebBrowser(), Notificacion, RowEdit);
                    areas.Dock = DockStyle.Fill;
                    contendor.Controls.Clear();
                    contendor.Controls.Add(areas);
                    contendor.Size = new Size(1000, 270);
                    contendor.ShowDialog();
                }
            }
            catch
            { }
        }

        private void buttonItem4_Click_1(object sender, EventArgs e)
        {
            Notificacion.ShowBalloonTip(700, "Selecciona", "Selecciona el Area a Pintar", ToolTipIcon.Info);
            System.Threading.Thread.Sleep(150);
            CapturaPantallaII.frmFondo _frmFondo = new CapturaPantallaII.frmFondo();
            if (_frmFondo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PictureBox Temp = new PictureBox();
                
                MetodosUtiles Utiles = new MetodosUtiles();
                if (Clipboard.ContainsImage())
                {
                    Temp.Image = Clipboard.GetImage();
                    Bitmap imagen = new Bitmap(Temp.Image);
                    var titotp = "C:\\" + DateTime.Today.ToShortDateString().Replace("/","-") + ".png";
                    imagen.Save(titotp, System.Drawing.Imaging.ImageFormat.Png);
                    string direccion = Environment.SpecialFolder.Desktop + "\\Mapas";
                    if (Directory.Exists(direccion))
                        imagen.Save(direccion+"\\Screenshot.png", System.Drawing.Imaging.ImageFormat.Png);
                    else
                    {
                        Directory.CreateDirectory(direccion);
                        imagen.Save(direccion + "\\Screenshot.png", System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
            }
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            Maps.Etiquetas.ucEtiquetas Etiquetas  = new Maps.Etiquetas.ucEtiquetas(dicPrincipal);
            Form fn = new Form();
            fn.Size = new Size(Etiquetas.Width+20,Etiquetas.Height+60);
            fn.Controls.Add(Etiquetas);
            fn.Show(this);
        }

        private void buttonItem7_Click(object sender, EventArgs e)
        {
         
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            cMapas.Posicion();
            cMapas.Carga();
        }

        private void btnPoligono_Click(object sender, EventArgs e)
        {
            cMapas.Poligonos();
        }

        private void btnLinea_Click(object sender, EventArgs e)
        {
            cMapas.Rutas();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cMapas.Puntos();
        }

        private void bntPozo_Click(object sender, EventArgs e)
        {
            cMapas.ColocaPozos(cdatos.DiccionarioPozos);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Text = cMapas.Limpiar(textBox1.Text);
        }

        private void gMapControl1_OnMarkerClick(GMap.NET.WindowsForms.GMapMarker item, MouseEventArgs e)
        {
            Console.WriteLine(String.Format("Marker {0} was clicked.", item.Tag));
            cMapas.PosicionActual = item.Position;
        //    cMapas.Carga(9);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            cMapas.Carga(trackBar1.Value);
        }

        private void gMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void gMapControl1_OnMapZoomChanged()
        {
            trackBar1.Value = (int)cMapas.CMaps.Zoom;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            cMapas.CMaps.ShowCenter = true;
            cMapas.Buscar(textBox1.Text);
        }
    }
}
