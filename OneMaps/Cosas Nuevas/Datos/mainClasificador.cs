using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;
using System.Text.RegularExpressions;

namespace Maps
{
    public partial class mainClasificador : Form
    {
        List<ObjetoSelecionable> NombreColumnas = new List<ObjetoSelecionable>();
        List<DataTable> Clasificados_inicial = new List<DataTable>();
        List<Core_Clasificador.ObjetoTablas> ListaFinal = new List<Core_Clasificador.ObjetoTablas>();
        Core_Clasificador Origendatos;
        NotifyIcon Notificacion;
        List<Point> LIndice = new List<Point>();
        Color[] DobleColor = new Color[] { Color.Silver, Color.LightYellow };
        ObjetoSelecionable miobj;
      

        DataTable Temporales, dtOperacion, dtResumen;


        public mainClasificador(NotifyIcon Noti)
        {
            Notificacion = Noti;
            InitializeComponent();
            Origendatos = new Core_Clasificador();



        }
        public mainClasificador(DataSet temp, string Name, NotifyIcon Noti)
        {
            Notificacion = Noti;
            InitializeComponent();
            Origendatos = new Core_Clasificador(temp, Name);




        }
        public mainClasificador(DataTable Temp, NotifyIcon Noti)
        {
            Notificacion = Noti;
            InitializeComponent();
            Origendatos = new Core_Clasificador(Temp);

            LeerXML1();




        }
        public mainClasificador(DataTable Temp, NotifyIcon Noti,ObjetoSelecionable MiObjeto)
        {
            Notificacion = Noti;
            InitializeComponent();
            Origendatos = new Core_Clasificador(Temp);
            miobj = MiObjeto;
            LeerXML1();




        }

        
        //este solo dice que opcion es la marcada
        int Opcionesexternas()
        {
            int Linea = 0;
            foreach (DataRow row in dtOperacion.Rows)
            { Linea++;
                if (Convert.ToBoolean(row[2]))
                    return (Linea-1);
               
            }
          

            return Linea;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            /** subrecargando opciones de el checked de la lisat de columnas)
             */
            UserCriterio.Lista_eventos.ItemCheck += new ItemCheckEventHandler(Lista_ItemCheck);
            UserCriterio.panel_aceptar.Visible = false;
            //*************************************

            string[] Operaciones_Permitidas = new string[] { "SUMAR", "CONTAR", "PROMEDIO", "MINIMO", "MAXIMO", "DESVEST" };






            dtOperacion = new DataTable("Operaciones");
            dtOperacion.Columns.Add("Marca", Type.GetType("System.Boolean"));
            dtOperacion.Columns.Add("Operacion");
            dtOperacion.Columns.Add("OpeTabla", Type.GetType("System.Boolean"));
            foreach (string cadena in Operaciones_Permitidas)
            {
                DataRow row = dtOperacion.NewRow();
                row[0] = true;
                row[1] = cadena;
                row[2] = false;
                //
                dtOperacion.Rows.Add(row);

            }
            dgv_opreaciones.AutoGenerateColumns = false;
            dgv_opreaciones.DataSource = dtOperacion;
            colActivo.DataPropertyName = dtOperacion.Columns[0].ColumnName;
            ColNombre.DataPropertyName = dtOperacion.Columns[1].ColumnName;
            ColOpeTabla.DataPropertyName = dtOperacion.Columns[2].ColumnName;
            CambiaTamanio(dgv_opreaciones, 6);
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

        System.Data.DataTable GuardaDatos(DataGridView GridTabla)
        {
            System.Data.DataTable Tabla = new System.Data.DataTable();
            try
            {
                for (int j = 0; j < GridTabla.Columns.Count; j++)
                {
                    Tabla.Columns.Add(GridTabla.Columns[j].HeaderText);

                }
            }
            catch { }

            DataRow Reglon;
            try
            {
                for (int i = 0; i < GridTabla.Rows.Count - 1; i++)
                {
                    Reglon = Tabla.NewRow();
                    for (int j = 0; j < GridTabla.Columns.Count; j++)
                    {
                        if (GridTabla[j, i].Value != null)
                            Reglon[j] = GridTabla[j, i].Value;
                        else
                            Reglon[j] = 0;

                    }
                    Tabla.Rows.Add(Reglon);
                }
            }
            catch { }
            return Tabla;
        }
        System.Data.DataTable GuardaDatos_sinocultos(DataGridView GridTabla)
        {
            System.Data.DataTable Tabla = new System.Data.DataTable();
            try
            {
                for (int j = 0; j < GridTabla.Columns.Count; j++)
                {
                    if (GridTabla.Columns[j].Visible)
                        Tabla.Columns.Add(GridTabla.Columns[j].HeaderText);

                }
            }
            catch { }

            DataRow Reglon;
            try
            {
                for (int i = 0; i < GridTabla.Rows.Count - 1; i++)
                {

                    if (GridTabla.Rows[i].Visible)
                    {
                        Reglon = Tabla.NewRow();
                        for (int j = 0; j < GridTabla.Columns.Count; j++)
                        {
                            if (GridTabla.Columns[j].Visible)
                            {
                                if (GridTabla[j, i].Value != null)
                                    Reglon[j] = GridTabla[j, i].Value;
                                // else
                                //   Reglon[j] = "";
                            }

                        }
                        Tabla.Rows.Add(Reglon);
                    }
                }
            }
            catch { }
            Tabla.TableName = GridTabla.Name;
            return Tabla;
        }

        void LeerXML1()
        {
            try
            {
                this.Text = Application.ProductName + " V." + Application.ProductVersion;

                DataSet Tablas = new DataSet("REGISTRO_1");
                // Tablas.ReadXmlSchema(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SchemeD_registro.OnePre");
                // if (Origendatos.Origen.Tabla!=null)
                {
                    NombreColumnas = new List<ObjetoSelecionable>();
                    ComboExterno.Items.Clear();
                    cmbInterno.Items.Clear();
                    foreach (DataColumn col in Origendatos.Origen.TablaActual().Columns)
                    {
                        NombreColumnas.Add(new ObjetoSelecionable(col.ColumnName, false));

                    }
                    cmbInterno.Items.AddRange(NombreColumnas.ToArray());
                    ComboExterno.Items.AddRange(NombreColumnas.ToArray());
                    UserCriterio.Lista_eventos.Sorted = false;
                    UserCriterio.NuevaLista(ref NombreColumnas);

                    this.Text = Application.ProductName + " V. " + Application.ProductVersion + " " + Origendatos.Origen.Tabla.TableName;


                }
            }
            catch (Exception Ex) { MessageBoxEx.Show(Ex.Message); }

        }


     


        
        


        private double DesviacionStadar(List<double> ValoresObtenidos, double Promedio)
        {
            double Sumatoria = 0;

            for (int i = 0; i < ValoresObtenidos.Count; i++)
            {
                Sumatoria += Math.Pow(ValoresObtenidos[i] - Promedio, 2);
            }
            if (ValoresObtenidos.Count == 1)
                return 0;

            return Math.Sqrt(Sumatoria / (ValoresObtenidos.Count - 1));
        }

        //aqui esta funcion quita o eliminara tablas que son propias de la seleccion

        void ColocaElimina(string Clave)
        {
            //primero Busquemos si existe esa clave.
            int num = 0;
            foreach (Control control in FlowCriterios.Controls)
            {
                if (control.Tag.ToString() == Clave)
                    num++;
            }
            if (num == 0)//que no hay
            {
                //creamos un user control para ver que se peude meter.
                List<ObjetoSelecionable> obj = new List<ObjetoSelecionable>();
                foreach (string Objeto in Origendatos.Origen.DistintosDeUnaColumna(Clave).ToArray())
                {
                    obj.Add(new ObjetoSelecionable(Objeto));
                }
                userFiltroElemento elementos = new userFiltroElemento(ref obj);
                //elementos.PanelTipo.Visible = true;
                elementos.panel_aceptar.Visible = false;
                elementos.Tag = Clave;

                elementos.Size = new System.Drawing.Size(UserCriterio.Size.Width, UserCriterio.Size.Height);
                FlowCriterios.Controls.Add(elementos);
                //  elementos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)   ))                 ;

            }

            else
            {
                Control temp = null;
                foreach (Control control in FlowCriterios.Controls)
                {
                    if (control.Tag.ToString() == Clave)
                        temp = control;
                }
                if (temp != null)
                {
                    FlowCriterios.Controls.Remove(temp);
                    temp.Dispose();
                }
            }
        }
        void AplicaCalificacion()
        {
            //try
            //{
            ////    Visor_Datos vd = new Visor_Datos();
            //    //foreach (List<DataRow> dt in tabla_ahora)
            //    //{
            //    //    vd.EstableceDatos(dt.CopyToDataTable());
            //    //    vd.Rapida(false);
            //    //recoramos los valores
            //    if (Temporales != null)
            //    {
            //        Temporales.Dispose();


            //        dtResumen.Dispose();
            //    }
            //    Temporales = Origendatos.Origen.Tabla.Clone();

            //    int ix = 0;
            //    foreach (DataColumn col in Temporales.Columns)
            //    {
            //        col.SetOrdinal(ix);
            //        ix++;
            //    }
            //    //Temporales.Columns.Add("Criterio");
            //    // Temporales.Columns["Criterio"].SetOrdinal(0);
            //    Temporales.Columns.Add("Operacion_");
            //    Temporales.Columns["Operacion_"].SetOrdinal(0);
            //    dtResumen = Temporales.Clone();
            //    List<List<DataRow>> tabla_ahora = new List<List<DataRow>>();
            //    tabla_ahora.Add(Origendatos.Origen.Tabla.AsEnumerable().ToList());
            //    List<List<DataRow>> contenedorTemp = new List<List<DataRow>>();
            //    //mantenagmos un apuntador para ver que tenesmos jajajaj
            //    List<List<DataRow>> aux = tabla_ahora;
            //    int indicecriterio = 0;
            //    bool entro = false;

            //    List<string> lbCriterios = new List<string>();
            //    foreach (Control con in FlowCriterios.Controls)//criterio por criterio
            //    {
            //        string campo_clasificar = con.Tag.ToString();
            //        Notificacion.ShowBalloonTip(500, "Clasificando por "+campo_clasificar+ " !!!", "Espere...", ToolTipIcon.Info);
            //        userFiltroElemento userfil = (userFiltroElemento)con;


            //        //aqui es para los de elemntos 
            //        switch (userfil.ControlActual)
            //        {
            //            case 0:
            //                List<ObjetoSelecionable> temp = ObjetosMarcados(userfil.ListaPozosTotales);
            //                Temporales.Columns[con.Tag.ToString()].SetOrdinal(indicecriterio + 1);
            //                indicecriterio++;
            //                if (temp.Count == 0)
            //                {
            //                    entro = false;
            //                    break;
            //                }
            //                foreach (List<DataRow> ListaSub in tabla_ahora)//tabla por tabla
            //                {


            //                    foreach (ObjetoSelecionable obj in temp)//criterio elemento
            //                    {

            //                        List<DataRow> elementos = BuscaLinq(campo_clasificar, obj, ListaSub).ToList();
            //                        if (elementos.Count > 0)
            //                            contenedorTemp.Add(elementos);

            //                    }

            //                    //vemaos que tenemos 
            //                }
            //                entro = true;
            //                lbCriterios.Add(campo_clasificar);
            //                break;
            //            case 1:
            //                //para valores
            //                Temporales.Columns[con.Tag.ToString()].SetOrdinal(indicecriterio + 1);
            //                indicecriterio++;
            //                string Sub = "";
            //                foreach (List<DataRow> ListaSub in tabla_ahora)//tabla por tabla
            //                {

            //                    foreach (ClasificacionOne.InterevalosCondiciones inter in userfil.ListaIntervalos)
            //                    {

            //                        List<DataRow> elementos = ValoresLinq(inter, campo_clasificar, ListaSub).ToList();

            //                        if (elementos.Count > 0)
            //                            contenedorTemp.Add(elementos);
            //                    }
            //                }
            //                entro = true;

            //                foreach (ClasificacionOne.InterevalosCondiciones inter in userfil.ListaIntervalos)
            //                {
            //                    Sub += " " + inter.ToString();
            //                }
            //                lbCriterios.Add("_" + campo_clasificar + Sub);
            //                break;

            //            case 2:

            //                Temporales.Columns[con.Tag.ToString()].SetOrdinal(indicecriterio + 1);
            //                indicecriterio++;
            //                foreach (List<DataRow> ListaSub in tabla_ahora)//tabla por tabla
            //                {
            //                    List<DataRow> elementos = ListaSub;
            //                    foreach (ClasificacionOne.InterevalosFecha inter in userfil.ListaFechas)
            //                    {
            //                        elementos = FechasLinq(inter, campo_clasificar, elementos).ToList();

            //                    }
            //                    if (elementos.Count > 0)
            //                        contenedorTemp.Add(elementos);

            //                }

            //                entro = true;
            //                break;
            //            default: entro = false;
            //                break;

            //        }
            //        if (entro)
            //        {
            //            tabla_ahora.Clear();
            //            //aqui haremos el cambio
            //            aux = tabla_ahora;
            //            tabla_ahora = contenedorTemp;
            //            contenedorTemp = aux;
            //        }

            //    }


            //    //   Visor_Datos vd = new Visor_Datos();
            //    //foreach (List<DataRow> dt in tabla_ahora)
            //    //{
            //    //    vd.EstableceDatos(dt.CopyToDataTable());
            //    //    vd.Rapida(false);
            //    //}

            //    //depues de clasificar haremos el objeto
            //    if (tabla_ahora.Count > 0)
            //    {

            //        ListaFinal.Clear();


            //        foreach (List<DataRow> objtab in tabla_ahora)
            //        {
            //            try
            //            {
            //                Core_Clasificador.ObjetoTablas unidad = new Core_Clasificador.ObjetoTablas();
            //                unidad.Tabla_unidad = objtab.CopyToDataTable();
            //                unidad.ColocaCampos(lbCriterios);
            //                unidad.Calculos();
            //                if (cmbInterno.Text != "")
            //                {
            //                    if (rbInternoAcendente.Checked)
            //                        unidad.Ordena(cmbInterno.Text, true);
            //                    if (rbInternoDesc.Checked)
            //                        unidad.Ordena(cmbInterno.Text, false);

            //                    Notificacion.ShowBalloonTip(500, "Aplicando Operaciones: " + unidad.Cadena + "!!!", "Generacion Finalizada", ToolTipIcon.Info);

            //                }
            //                ListaFinal.Add(unidad);
            //            }
            //            catch (Exception Error) { MessageBox.Show(Error.Message); }
            //        }


            //        //vemaos que ordena
            //        MostrarOrdenarResultadov2();


            //    }

            //    TabPrincipal.SelectedTabIndex = 1;
            //    Tabla_Clasificada.DataSource = null;
            //    Tabla_Clasificada.DataSource = Temporales;
            //    //depues diremos que no se ordene.
            //    foreach (DataGridViewColumn columnas in Tabla_Clasificada.Columns)
            //    {
            //        columnas.SortMode = DataGridViewColumnSortMode.NotSortable;
            //        if (columnas.Name == "Operacion_")
            //        {
            //            columnas.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //        }
            //        columnas.DefaultCellStyle.Format = "F4";
            //    }
            //    Tabla_Clasificada.Refresh();
            //    TabPrincipal.SelectedTabIndex = 2;
            //    dgvResumen.DataSource = null;
            //    dgvResumen.DataSource = dtResumen;
            //    //depues diremos que no se ordene.
            //    foreach (DataGridViewColumn columnas in dgvResumen.Columns)
            //    {
            //        columnas.SortMode = DataGridViewColumnSortMode.NotSortable;
            //        columnas.DefaultCellStyle.Format = "F4";
            //        if (columnas.Name == "Operacion_")
            //        {
            //            columnas.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //        }

            //    }
            //    dgvResumen.Refresh();




            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}


        }
        List<ObjetoSelecionable> ObjetosMarcados(List<ObjetoSelecionable> lis)
        {
            var Objetos = from a in lis.AsEnumerable().Where(p => p.Estado) select a;
            if (Objetos.Count() > 0)
                return Objetos.ToList();
            return new List<ObjetoSelecionable>();
        }

        IEnumerable<DataRow> BuscaLinq(string Campo, ObjetoSelecionable Valores, IEnumerable<DataRow> rows_linq)
        {

            //MessageBox.Show("esto es lo que recibimos");
            //global.EstableceDatos(rows_linq.CopyToDataTable());
            //global.Rapida(true);
            string val = Valores.ToString().ToUpper();
            var query = from order in rows_linq.Where(p => p != null) where (order[Campo].ToString().ToUpper() ==val ) select order;

            return query;

        }


        
        IEnumerable<DataRow> ValoresLinq(Maps.InterevalosCondiciones interevalosCondiciones, string Campo, List<DataRow> reglon)
        {

            var CampoInicial = from order in reglon where A_valor(order[Campo].ToString(), interevalosCondiciones.Operador, interevalosCondiciones.Cantidad) select order;

            return CampoInicial;

        }
        bool A_valor(string cad, string Operador, double Cantidad)
        {
            double val = 0;
            cad = cad.Replace(",", string.Empty);
            if (double.TryParse(cad, out val))
            {
                switch (Operador)
                {
                    case "<":
                        return (val < Cantidad);



                    case ">=":
                        return (val >= Cantidad);

                    case "=":
                        return (val == Cantidad);

                    case "!=":
                        return (val != Cantidad);

                }


            }
            return false;
        }
        IEnumerable<DataRow> FechasLinq(Maps.InterevalosFecha interevalosCondiciones, string Campo, IEnumerable<DataRow> reglon)
        {



            reglon = from order in reglon where (A_fecha(order[Campo].ToString(), interevalosCondiciones.Operador, interevalosCondiciones.Fecha)) select order;


            return reglon;

        }
        bool A_fecha(string cad, string Operador, DateTime Fecha)
        {
            DateTime val = DateTime.Now;
            cad = cad.Replace(",", string.Empty);
            if (DateTime.TryParse(cad, out val))
            {
                switch (Operador)
                {
                    case "<":
                        return (val < Fecha);


                    case ">=":
                        return (val >= Fecha);

                    case "=":
                        return (val == Fecha);

                    case "!=":
                        return (val != Fecha);

                }


            }
            return false;
        }

        private void Lista_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //se supone que aqui acaba de cambiar un nuevo elemnto
            string criterio = (UserCriterio.Lista_eventos.Items[e.Index] as ItemsPersonal).ToString();
            ColocaElimina(criterio);

        }



      
        void PintaIndices()
        {
            int ix = 0;
            foreach (Point clave in LIndice)
            {
             
                for (int i = clave.X; i < clave.Y; i++)
                {
                    Tabla_Clasificada.Rows[i+ix].Cells["Operacion_"].Style.ForeColor = Color.Red;
                    Tabla_Clasificada.Rows[i+ix].DefaultCellStyle.BackColor = Color.Silver;
                }
                ix += clave.Y;
            }
            ix = 0;
            indice = 0; 
            for (int i = 0; i < dgvResumen.Rows.Count; i++)
            {
                
                ix++;
                dgvResumen.Rows[i].Cells["Operacion_"].Style.ForeColor = Color.Red;
                dgvResumen.Rows[i].DefaultCellStyle.BackColor = DobleColor[indice];
                if (ix > 5)
                {
                    ix = 0;
                    if (indice == 0)
                        indice = 1;
                    else
                    {
                        indice = 0;
                    }
                }

            }
          //pintemos ahora el resumen 
        }
       


        //private void buttonItem10_Click(object sender, EventArgs e)
        //{
        //    SaveFileDialog SALVAR = new SaveFileDialog();
        //    SALVAR.AddExtension = true;
        //    SALVAR.Filter = "Tipo One Registro (*.OneClass) | *.OneClass";
        //    if (SALVAR.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        DataSet Tablas = new DataSet("REGISTRO_1");
        //        Tablas.Tables.Clear();
        //        Tablas.Tables.Add(GuardaDatos_sinocultos(Tabla_Inicial));

        //        Tablas.WriteXml(SALVAR.FileName);

        //    }
        //}







        private void MostrarOrdenarResultadov2()
        {

            LIndice.Clear();
            bool Par = false;
            Notificacion.ShowBalloonTip(1000, "Imprimiendo Tabla!!!", "Espere mientras que se genera toda las tabla de Resultado\nPuede Tardar varios minutos..", ToolTipIcon.Info);
            if (ComboExterno.SelectedItem != null)
            {
                //antes de imprimir aqui haremos una pequeña lista de maximos e indices;
                DataTable Indexado = new DataTable();
                Indexado.Columns.Add("Maximo", Type.GetType("System.Double"));
                Indexado.Columns.Add("Indice", Type.GetType("System.Int32"));
                DataRow reglon;
                int PorMaximo = Opcionesexternas();

                for (int i = 0; i < ListaFinal.Count; i++)
                {
                    reglon = Indexado.NewRow();
                    reglon[0] = ListaFinal[i].Maximo(ComboExterno.SelectedItem.ToString(), PorMaximo);
                    reglon[1] = i;
                    Indexado.Rows.Add(reglon);
                }
                //ahora que ya tengo los indices los ordenare

                if (rbexternoAscendente.Checked)

                    Indexado.DefaultView.Sort = "Maximo ASC";
                if (rbexternodesc.Checked)
                    Indexado.DefaultView.Sort = "Maximo DESC";

                //One_Registro_Presion.VistaPrevia Vp = new One_Registro_Presion.VistaPrevia(Indexado);
                //Vp.ShowDialog();

                //aqui deberia de 


                for (int i = 0; i < Indexado.Rows.Count; i++)
                {
                    try
                    {

                        //  ListaFinal[i].imprimeValores();
                        int val = Convert.ToInt32(Indexado.DefaultView[i].Row[1]);
                        // ListaFinal[val].imprimeValores(Tabla_Clasificada);
                        // ListaFinal[val].Resumen(Tablas_Resumen, Par);

                        Core_Clasificador.ObjetoTablas tab = ListaFinal[val];


                        Temporales.Merge(tab.Tabla_unidad);
                        Temporales.Merge(tab.Tabla_ValoresCalculo);
                        dtResumen.Merge(tab.Resumen());
                        LIndice.Add(new Point(tab.Tabla_unidad.Rows.Count, tab.Tabla_unidad.Rows.Count + 6));
                        Par = !Par;
                    }
                    catch (Exception Error) { MessageBox.Show(Error.Message); }
                }



            }
            else
            {
               //int guardemos los 
               
                foreach (Core_Clasificador.ObjetoTablas tab in ListaFinal)
                {
                    //ascamos cada Tabla y la agregamos a un concentrado

                    Temporales.Merge(tab.Tabla_unidad);
                    Temporales.Merge(tab.Tabla_ValoresCalculo);
                    dtResumen.Merge(tab.Resumen());
                    LIndice.Add( new Point(  tab.Tabla_unidad.Rows.Count, tab.Tabla_unidad.Rows.Count+6));
                }

            }


            Notificacion.ShowBalloonTip(500, "Clasificacion terminada!!!", "Generacion Finalizada", ToolTipIcon.Info);


        }

 //aqui se supone que pintaremos solo la seccion de datos que jhay en la columna operaciones.

        void PintaOperaciones()
        {
            try
            {
                if (SegundoPlano.IsBusy)
                    SegundoPlano.CancelAsync();
                SegundoPlano.RunWorkerAsync();
            }
            catch (Exception Error) { MessageBox.Show(Error.Message); }
        }






        private void buttonItem16_Click(object sender, EventArgs e)
        {

            SaveFileDialog salvar = new SaveFileDialog();
            salvar.DefaultExt = ".xls";
            salvar.Filter = "Hoja de Excel (*.xls) | *.xls";
            if (Temporales.Columns.Count > 0)
                if (salvar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ExportaAExcelDLL.GuardaTablaenExcel Nuevo = new ExportaAExcelDLL.GuardaTablaenExcel();
                    DataTable[] ListaT = new DataTable[2];

                    ListaT[1] = (Temporales);
                    //   ListaT[0] = (GuardaDatos_sinocultos(Tabla_Clasificada));
                    Notificacion.ShowBalloonTip(5000, "Exportando tablas a Excel!!!", "Espere mientras que se genera toda las tabla de Resultado\nPuede Tardar varios minutos.. Se Expotara la Tabla Clasificacion y Resumen", ToolTipIcon.Info);

                    Nuevo.DataTableToExcel(ListaT, salvar.FileName, 4);
                    Notificacion.ShowBalloonTip(500, "Exportando Completa!!!", " Se Expotaron la Tabla Clasificacion y Resumen", ToolTipIcon.Info);



                }
        }



        private void buttonItem63_Click(object sender, EventArgs e)
        {
            SaveFileDialog salvar = new SaveFileDialog();
            salvar.DefaultExt = ".xls";
            salvar.Filter = "Hoja de Excel (*.xls) | *.xls";
            if (salvar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ExportaAExcelDLL.GuardaTablaenExcel Nuevo = new ExportaAExcelDLL.GuardaTablaenExcel();
                Nuevo.DataTableToExcelSave(Temporales, salvar.FileName, "Tabla de Clasificacion");
            }
        }








        private void buttonItem4_Click(object sender, EventArgs e)
        {

            ////se supone que Voy a Graficar por ello sacare los datos del resumen
            //try
            //{
               


            //    ConfGrafica cg = new ConfGrafica(ListaFinal,0);
            //   cg.ShowDialog();


            //}
            //catch { }
        }

      


     
        private void buttonItem11_Click_1(object sender, EventArgs e)
        {
           

        }

        private void Pestania_Maestra_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem12_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (ListaFinal .Count > 0)
                {

                    frmGerarquizar fmrgera = new frmGerarquizar(ListaFinal,Notificacion);


                    fmrgera.Show();
                }


                else { MessageBox.Show("No hay ninguna Tabla clasificada que se se pueda Heterogenizar"); }
            }
            catch (Exception Error) { MessageBox.Show(Error.Message); }
        }
       

        private void SegundoPlano_DoWork(object sender, DoWorkEventArgs e)
        {
            //pintemos bloques.

            int tam = Tabla_Clasificada.Rows.Count/100;

            for (int i = 0; i < Tabla_Clasificada.Rows.Count; i++)
            {
                try
                {
                    if (Tabla_Clasificada.Rows[i].Cells["Operacion_"].Value != null && Tabla_Clasificada.Rows[i].Cells["Operacion_"].Value.ToString().StartsWith("."))
                    {
                        //SegundoPlano.ReportProgress(i, i);
                        
                        Tabla_Clasificada.Rows[i].Cells["Operacion_"].Style.ForeColor = Color.Red;
                        Tabla_Clasificada.Rows[i].DefaultCellStyle.BackColor = Color.Silver;
                    }
                }
                catch (Exception Error) { MessageBox.Show(Error.Message); }
            }
        }

        private void SegundoPlano_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int valor = (int)e.UserState;
            Tabla_Clasificada.Rows[valor].Cells["Operacion_"].Style.ForeColor = Color.Red;
            Tabla_Clasificada.Rows[valor].DefaultCellStyle.BackColor = Color.Silver;
        }
        int indice = 0;


       
        private void cmdHistograma_Click(object sender, EventArgs e)
        {
           
            if ( ListaFinal.Count > 0)
            {
                Histograma histograma = new Histograma(ListaFinal, Notificacion);
                histograma.ShowDialog();
            }
        }

        private void FlowCriterios_SizeChanged(object sender, EventArgs e)
        {
            foreach (Control elementos in FlowCriterios.Controls)
                elementos.Size = new System.Drawing.Size(UserCriterio.Size.Width, UserCriterio.Size.Height - 10);
        }


        private void dgv_opreaciones_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv_opreaciones.IsCurrentCellDirty)
                dgv_opreaciones.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgv_opreaciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bool estado = false;
            switch (e.ColumnIndex)
            {
                case 1:
                    try
                    {
                        estado = Convert.ToBoolean(dtOperacion.Rows[e.RowIndex][0]);
                        if (estado)
                        {
                            dtOperacion.Rows[e.RowIndex][2] = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                case 2:
                    //aqui es para el ordenamiento
                    for (int i = 0; i < dtOperacion.Rows.Count; i++)
                    {
                        try
                        {
                            if (i != e.RowIndex && Convert.ToBoolean(dtOperacion.Rows[e.RowIndex][0]))
                            {
                                dtOperacion.Rows[i][2] = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    break;

            }
        }

        

        private void SegundoPlano_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

      

       

        public bool estado_activo = true;

        private void cmdSelecCriterio_Click(object sender, EventArgs e)
        {
         TabPrincipal.SelectedPanel=   TabControlCriterio;
        }

        private void cmdGraficar_Click(object sender, EventArgs e)
        {
            try
            {
                //generaMiClasificacion
                TabPrincipal.SelectedTabIndex=2;
                LIndice.Clear();
                AplicaCalificacion();
                //*/////
                //----> ColocaNombre();
                //Lo primero que haremos es tener un datatable con todos los datos


                //aguardando en la nueva Tabla.
       
                //pintemos los valores que sabemos que tenemos que mosstrar.
                PintaIndices();
                
            }
            catch (Exception Error) { MessageBox.Show(Error.Message); }
            // PintaOperaciones();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListaFinal.Count > 0)
                {

                    frm_generadorCurvas fmrgera = new frm_generadorCurvas(ListaFinal, Notificacion);


                    fmrgera.Show();
                }


                else { MessageBox.Show("No hay ninguna Tabla clasificada que se se pueda Heterogenizar"); }
            }
            catch (Exception Error) { MessageBox.Show(Error.Message); }
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            SaveFileDialog SALVAR = new SaveFileDialog();
            SALVAR.AddExtension = true;
            SALVAR.Filter = "XML (*.xml) | *.xml";

            if (SALVAR.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                dtResumen.WriteXml(SALVAR.FileName, XmlWriteMode.WriteSchema);

            }

            if (miobj != null)
                if (miobj.objeto is One_Core_Produccion)
                {
                    One_Core_Produccion CoreProduccion = miobj.objeto as One_Core_Produccion;

                    //para producciio.
                    int cont = 1;
                    string Nombre = "tab";
                    do
                    {
                        while (CoreProduccion.Unidad.dtTablas_Informacion.Tables.Contains(Temporales.TableName + "_F" + cont.ToString()))
                        {
                            cont++;
                        }
                        Nombre = Temporales.TableName + "_F" + cont.ToString();
                        SoloNombre soloname = new SoloNombre(Nombre);
                        soloname.ShowDialog();
                        Nombre = soloname.txtxName.Text;
                        //como ya cambio el nombre volvamos a ver si no esta ocupado.
                        if (CoreProduccion.Unidad.dtTablas_Informacion.Tables.Contains(Nombre))
                            MessageBox.Show("El nombre para esta tabla ya existe!!! escriba otra");
                    }
                    while (CoreProduccion.Unidad.dtTablas_Informacion.Tables.Contains(Nombre));

                    DataTable dt = dtResumen.Copy();
                    dt.TableName = Nombre.ToUpper();
                    Point pos = CoreProduccion.Unidad.PosicionDiccionario(dtResumen.TableName);
                    if (pos.X != -1)
                    {

                        //como tenemos unas posiciones x. busquemos que esten en la tabla.
                        Point nuevopunto = new Point(0, 2);
                        if (dtResumen.TableName != CoreProduccion.Unidad.DtEventos.TableName)
                        {
                            nuevopunto.X = dt.Columns[CoreProduccion.Unidad.dtTablas_Informacion.Tables[dtResumen.TableName].Columns[pos.X].ColumnName].Ordinal;
                            nuevopunto.Y = dt.Columns[CoreProduccion.Unidad.dtTablas_Informacion.Tables[dtResumen.TableName].Columns[pos.Y].ColumnName].Ordinal;
                        }
                        else
                        {
                            nuevopunto.X = dt.Columns[CoreProduccion.Unidad.DtEventos.Columns[pos.X].ColumnName].Ordinal;
                            nuevopunto.Y = dt.Columns[CoreProduccion.Unidad.DtEventos.Columns[pos.Y].ColumnName].Ordinal;
                        }
                        CoreProduccion.Unidad.dtTablas_Informacion.Tables.Add(dt);
                        CoreProduccion.Unidad.Diccionario.Add(dt.TableName, nuevopunto);
                        CoreProduccion.Unidad.LeerTablas();

                    }
                    MessageBox.Show("Tabla de Resultado Guardado Correctamente");


                }
    
        }

        private void cmdRiesgo_Click(object sender, EventArgs e)
        {

            ConfGrafica cg = new ConfGrafica(ListaFinal,1);
            cg.ShowDialog();

        }

     



    }//clas

}//namespace

