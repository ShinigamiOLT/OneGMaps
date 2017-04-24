using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Maps
{
    public class CoreCalifica : Core_Calificar
    {
        private DataSet dsPrincipal;
        DataTable dtPrincipal;
        private userFiltroElemento selec;
       
        private Point Origen;
        private Point Muestra;
        private Dictionary<string, DataRow> Dicc;
        private List<ObjetoSelecionable> listObj;
        
        string columna = "";
        cETL Limpieza = new cETL();
        List<int> ListEncontrados = new List<int>();
        List<string> listCopia;
       private List<string> LReentradas;
        List<int> posCoinciden;
        List<int> posNoEncontradas;
       
        bool Ubicacion;
        
        string TableA;
        List<string> lTabla;
        

        public CoreCalifica()
        {
            dsPrincipal = new DataSet();
            dsCalifica = new DataSet();
            Diccionario = new Dictionary<string, Point>();
            ListaTablas = new List<DataTable>();
            cargaLista();
        }

        public CoreCalifica(DataSet dsTotal)
        {
            dsCalifica = new DataSet();
            dsCalifica = dsTotal;
            Diccionario = new Dictionary<string, Point>();
            ListaTablas = new List<DataTable>();
            cargaLista();
        }

        /// <summary>
        /// Llena El diccionario de "CoreCalifica" apartir de otro diccionario
        /// </summary>
        /// <param name="dicAux">Un diccionario con Key: string y Value: Point</param>
        public override void armaDiccionario(Dictionary<string, Point> dicAux)
        {
            foreach (KeyValuePair<string, Point> Par in dicAux)
            {
                AddDictionary(Par.Key, Par.Value);
            }
        }

        /// <summary>
        /// Para averiguar si la tabla ya esta agregada
        /// </summary>
        /// <param name="name">Nombre de la tabla</param>
        /// <returns>Verdadero si pertenece la tabla en caso contrario falso</returns>
        public override bool exiteTabla(string name)
        {
            return dsCalifica.Tables.Contains(name);
        }

        /// <summary>
        /// Agrega una tabla
        /// </summary>
        /// <param name="Tabla">Tabla que se agregara</param>
        public override void addTable(DataTable Tabla)
        {
            if (!dsCalifica.Tables.Contains(Tabla.TableName))
            {
                dsCalifica.Tables.Add(Tabla);
                ListaTablas.Add(Tabla);
            }
            else
            {
                updateTable(Tabla.TableName, Tabla);
                ListaTablas[ListaTablas.FindIndex(x => x.TableName == Tabla.TableName)] = Tabla;
            }
        }

        /// <summary>
        /// Elimina una tabla
        /// </summary>
        /// <param name="Tabla">Tabla que se eliminara</param>
        public override void removeTable(DataTable Tabla)
        {
            dsCalifica.Tables.Remove(Tabla);
            ListaTablas.Remove(Tabla);
        }

        /// <summary>
        /// Elimia una tabla apartir de su nombre
        /// </summary>
        /// <param name="ttabla">nombre de la tabla que va a eliminar</param>
        public void removeTable(string ttabla)
        {
            dsCalifica.Tables.Remove(ttabla);
            ListaTablas.Remove(ListaTablas[ListaTablas.FindIndex(x => x.TableName == ttabla)]);

        }

        /// <summary>
        /// Para saber cuales sera la lista de elementos seleccionables
        /// </summary>
        private void cargaLista()
        {
            foreach (DataTable table in dsCalifica.Tables)
                ListaTablas.Add(table);
        }

        /// <summary>
        /// Actualiza un elemento del DataSet
        /// </summary>
        /// <param name="key">Valor a actualizar</param>
        /// <param name="punto">Valor a actualizar</param>
        public override void UpdateDictionary(string key, Point punto)
        {
            if (Diccionario.Keys.Contains(key))
                Diccionario[key] = punto;
        }

        /// <summary>
        /// Elimina un elemento del diccionario
        /// </summary>
        /// <param name="key">Elemento que elimira</param>
        public override void RemoveDictionary(string key)
        {
            if (Diccionario.Keys.Contains(key))
                Diccionario.Remove(key);
        }

        /// <summary>
        /// Agrega un elemento al diccionario
        /// </summary>
        /// <param name="key">Elemento key de tipo string</param>
        /// <param name="punto">Elemento key de tipo Point</param>
        public override void AddDictionary(string key, Point punto)
        {
            if (!Diccionario.Keys.Contains(key))
                Diccionario.Add(key, punto);
        }

        /// <summary>
        /// Devuelve el elemento buscado
        /// </summary>
        /// <param name="key">Parametro que buscara</param>
        /// <returns></returns>
        public override Point ValueDictionary(string key)
        {
            if (Diccionario.Keys.Contains(key))
                return Diccionario[key];
            else return new Point(-1, -1);
        }

        /// <summary>
        /// Este metodo es que se utiliza para Homologar los nombres
        /// </summary>
        /// <param name="val">Tabla que se modificaran los nombres</param>
        public override void Colocar(DataTable val)
        {
            dsPrincipal = new DataSet();
            TableA = val.TableName;
            if (lTabla == null)
            {
                DataTable Tabla = this.ValueTable("Tabla_Inicial");
                lTabla = (from DataRow pozo in Tabla.Rows
                          orderby pozo["Pozo"].ToString() ascending
                          select pozo["Pozo"].ToString()).Distinct().ToList();
            }
            LReentradas = new List<string>();
            string nombre_real = "";
            LReentradas.Add("-R1");
            LReentradas.Add("-R2");
            LReentradas.Add("-R3");
            LReentradas.Add(" R1");
            LReentradas.Add(" R2");
            LReentradas.Add(" R3");
            LReentradas.Add(" re");
            LReentradas.Add(" Re");
            LReentradas.Add(" RE");
            LReentradas.Add("R1");
            LReentradas.Add("R2");
            LReentradas.Add("R3");
            LReentradas.Add("re");
            LReentradas.Add("Re");
            LReentradas.Add("RE");

            Ubicacion = false;
            //Notificacion.Visible = true;
            //Notificacion.ShowBalloonTip(5000000);

            ListEncontrados.Clear();
            string cadElegida;
            DataTable dtErrores = new DataTable();
            dtErrores.TableName = "Original";
            dtErrores.Columns.Add("Original " + TableA);
            dtErrores.Columns.Add("Asignado");
            dsPrincipal.Tables.Add(dtErrores);

            DataTable dtCoinciden = new DataTable();
            dtCoinciden.TableName = "Coinciden";
            dtCoinciden.Columns.Add("Original " + TableA);
            dtCoinciden.Columns.Add("Asignado");

            DataTable dtEncontrados = new DataTable();
            dtEncontrados.TableName = "Errores";
            dtEncontrados.Columns.Add("Original " + TableA);
            dtEncontrados.Columns.Add("Asignado");
            listCopia = new List<string>();
            listCopia.Clear();

            dsPrincipal.Tables.Add(dtCoinciden);
            dsPrincipal.Tables.Add(dtEncontrados);

            List<string> listaComparacion = new List<string>();
            posCoinciden = new List<int>();
            posNoEncontradas = new List<int>();

            dtPrincipal = val;

            columna = dtPrincipal.Columns[ValueDictionary(TableA).X].Caption;

            var varTempOriginal = (from DataRow row in dtPrincipal.Rows.Cast<DataRow>()
                                   select row[columna].ToString()).Distinct().ToList();

            var esto = (from string nombre in varTempOriginal
                        where nombre.Contains(" ") || nombre.Contains("-") && nombre != ""
                        select Limpieza.quitaAcentos((nombre.Replace('-', ' ').Split())[0])).Distinct().ToList();

            if (esto.Count == 0)
            {
                esto = (from string nombre in varTempOriginal
                        where nombre.Contains("-") && nombre != ""
                        select nombre.Split('-')[0]).Distinct().ToList();
            }


            List<string> grupo = new List<string>();
            List<string> grupos = new List<string>();
            List<string> alterno = new List<string>();

            if (esto.Count < 2)
            {

                grupo = (from string name in lTabla
                         where name.Contains(esto[0].ToUpper())
                         select name).Distinct().ToList();

                alterno = (from x in grupo
                           select x.Replace('-', ' ').Split(' ')[0]).Distinct().ToList();
            }
            else
            {
                DateTime inicio = DateTime.Now;
                grupo = (from x in lTabla
                         from y in esto
                         where x.Replace('-', ' ').Split(' ')[0] == y.ToUpper()
                         select x).Distinct().ToList();

                DateTime final = DateTime.Now;

                alterno = (from x in grupo
                           select x.Replace('-', ' ').Split(' ')[0]).Distinct().ToList();
            }

            bool quiteRee;
            string cualQuite = "";

            for (int i = 0; i < varTempOriginal.Count; i++)
            {
                nombre_real = varTempOriginal[i];
                string caden = Limpieza.quitaAcentos(varTempOriginal[i].Replace('-', ' '));
                if (caden != "")
                {
                    quiteRee = false;

                    for (int u = 0; u < LReentradas.Count; u++)
                    {
                        if (caden.Contains(LReentradas[u]))
                        {
                            caden = caden.Replace(LReentradas[u], "");
                            quiteRee = true;
                            cualQuite = LReentradas[u];
                            break;
                        }
                        else quiteRee = false;
                    }

                    var dfg = "";
                    DataRow rowEncontrados = dtEncontrados.NewRow();
                    dfg = grupo.SingleOrDefault(x => x.Replace('-', ' ') == caden.ToUpper());

                    if (dfg == null)
                    {
                        if (alterno.Contains(caden.ToUpper().Split()[0]))
                        {
                            bool HuboCoincidencia = false; 
                            for (int index = 0; index < grupo.Count; index++)
                            {
                                float y = CalculateSimilarity(caden.ToUpper(), grupo[index].Replace('-', ' '));
                                if (y >= 0.90)//aqui se guardan las palabras que posiblemente tienen coincidencias
                                {
                                    DataRow rowCoincide = dtCoinciden.NewRow();
                                    //noesta = false;
                                    rowCoincide[0] = nombre_real;
                                    rowCoincide[1] = grupo[index];
                                    dtCoinciden.Rows.Add(rowCoincide);
                                    posCoinciden.Add(i);
                                    HuboCoincidencia = true;
                                    break;
                                }
                            }
                            if (!HuboCoincidencia)
                            {
                                DataRow rowNoencontrado = dtErrores.NewRow();
                                rowNoencontrado[0] = nombre_real;
                                rowNoencontrado[1] = "(No Encontrado)";
                                posNoEncontradas.Add(i);
                                dtErrores.Rows.Add(rowNoencontrado);
                            }

                        }
                        else
                        {
                            DataRow rowNoencontrado = dtErrores.NewRow();
                            rowNoencontrado[0] = nombre_real;
                            rowNoencontrado[1] = "(No Encontrado)";
                            posNoEncontradas.Add(i);
                            dtErrores.Rows.Add(rowNoencontrado);
                        }

                    }
                    else//aqui se almacenan los que Se encuentran en la lista
                    {
                        ListEncontrados.Add(i);

                        //rowError[1] = dfg;

                        if (quiteRee)
                        {
                            rowEncontrados[0] = nombre_real;
                            rowEncontrados[1] = dfg + cualQuite;
                        }
                        else
                        {
                            rowEncontrados[0] = nombre_real;
                            rowEncontrados[1] = dfg;
                        }

                        dtEncontrados.Rows.Add(rowEncontrados);
                    }
                }
            }

            DataView vista = new DataView(dtErrores);
            dsPrincipal.Tables.Remove(dtErrores);
            dtErrores = vista.ToTable(true, "Original " + TableA, "Asignado");
            dsPrincipal.Tables.Add(dtErrores);
            
            //dsCalifica.Tables.Remove(Original);
            //dsCalifica.Tables.Add(Original);
            funcionQueHaceTodo();
            //Notificacion.Visible = false;

            //this.addTable(dtErrores);
        }

        /// <summary>
        /// Esta funcion llena las tablas con los: Errores, coincidencias y inexistentes
        /// </summary>
        void funcionQueHaceTodo()
        {
            UserNompozos nombres = new UserNompozos();
            nombres.Dock = DockStyle.Fill;
            nombres.tabNombres.SelectedIndexChanged += new EventHandler(tabNombres_SelectedIndexChanged);
            nombres.btnAplicar.Click += new EventHandler(btnAplicar_Click);
            nombres.dgvNombres.DataSource = dsPrincipal.Tables["Errores"];
            nombres.buttonX1.Visible = false;
            nombres.labelNombre.Text = "COLUMNA: " + columna + " DE LA TABLA " + TableA;
            varTexto = "Encontrados";

            nombres.dgvNombres.Columns[0].ReadOnly = true;

            panel4.Controls.Clear();
            panel4.Show();
            panel4.Size = nombres.Size;
            panel4.Controls.Add(nombres);
            Form uno = new Form();
            uno.Text = "Estandarización de Nombres";
            uno.Size = panel4.Size;
            uno.MinimizeBox = false;
            uno.MaximizeBox = false;
            uno.ShowInTaskbar = false;
            uno.StartPosition = FormStartPosition.CenterScreen;
            uno.Controls.Add(panel4);
            panel4.Dock = DockStyle.Fill;
            uno.ShowDialog();
        }

        void btnAplicar_Click(object sender, EventArgs e)
        {
            aplicar(varTexto, sender);
        }

        void tabNombres_SelectedIndexChanged(object sender, EventArgs e)
        {
            var text = (sender as TabControl).SelectedTab.Text;
            var dgvNombre = (sender as TabControl).SelectedTab.Controls[0] as DataGridView;
            switch (text)
            {
                case "Aplicar":
                    aplicar(varTexto, sender);
                    break;
                case "Similar":
                    varTexto = "Similar";
                    DataGridViewCheckBoxColumn Column1 = new DataGridViewCheckBoxColumn();
                    Column1.HeaderText = " Mantener Origen";
                    Column1.Name = "Column1";
                    dgvNombre.DataSource = dsPrincipal.Tables["Coinciden"];
                    if (!dgvNombre.Columns.Contains(Column1.Name))
                        dgvNombre.Columns.Insert(2, Column1);
                    
                    break;
                case "Existente":
                    varTexto = "Encontrados";
                    dgvNombre.DataSource = dsPrincipal.Tables["Errores"];
                    break;
                case "No Existente":
                    varTexto = "No Existente";
                    dgvNombre.DataSource = dsPrincipal.Tables["Original"];
                    break;
            }
            dgvNombre.Columns[0].ReadOnly = true;
        }

        Panel panel4 = new Panel();
        string varTexto = "";
        void Nombres_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var text = (sender as LinkLabel).Text;

            var dgvNombre = (((sender as LinkLabel).Parent.Parent as SplitContainer).Panel1.Controls[0] as SplitContainer).Panel2.Controls[0] as DataGridView;
            switch (text)
            {
                case "Aplicar":
                    aplicar(varTexto, sender);
                    break;
                case "Similar":
                    varTexto = "Similar";
                    dgvNombre.Columns[0].Visible = true;
                    dgvNombre.DataSource = dsPrincipal.Tables[" Coinciden"];
                    break;
                case "Existente":
                    varTexto = "Encontrados";
                    dgvNombre.Columns[0].Visible = false;
                    dgvNombre.DataSource = dsPrincipal.Tables[" Errores"];
                    break;
                case "No Existente":
                    dgvNombre.Columns[0].Visible = false;
                    varTexto = "No Existente";
                    dgvNombre.DataSource = dsPrincipal.Tables["Original"];
                    break;
            }
        }

        /// <summary>
        /// Este metodo aplica colos cambios que solicitas en la pantalla
        /// </summary>
        /// <param name="cad">Esta cadena es utilizada para saber en que pestaña te encuentras (Coincidencias, Errores, No encontrados)</param>
        /// <param name="sender">Que tabla es la que se le haran las modificaciones</param>
        void aplicar(string cad, object sender)
        {
            string nompozo = dtPrincipal.Columns[ValueDictionary(dtPrincipal.TableName).X].Caption;
            if (cad != "")
            {
                var objeto = ((((sender as Button).Parent.Parent as SplitContainer).Panel1.Controls[0] as SplitContainer).Panel2.Controls[0] as TabControl).SelectedTab.Controls[0] as DataGridView;
                switch (cad)
                {
                    case "Similar":
                        {

                            for (int i = 0; i < objeto.Rows.Count; i++)
                            {
                                var val = objeto[2, i].Value;
                                if (val == null) val = false;
                                DataRow[] arrayRows = null;
                                string cade = "'" + nompozo + "' = " + "'" + objeto[0, i].Value.ToString() + "'";
                                //if ((bool)val) arrayRows = dtPrincipal.Select("'" + nompozo + "' = " + "'" + objeto[0, i].Value.ToString() + "'");

                                 arrayRows = (from DataRow row in dtPrincipal.Rows
                                                  where  row[nompozo].ToString() == objeto[0, i].Value.ToString()
                                                  select row).ToArray();


                                foreach (DataRow row in arrayRows)
                                {
                                    if (!(bool)val)
                                        row[columna] = objeto[1, i].Value;
                                }
                            }


                        }
                        break;
                    case "Encontrados":
                        for (int i = 0; i < objeto.Rows.Count; i++)
                        {
                            
                            DataRow[] arrayRows = (from DataRow row in dtPrincipal.Rows
                                                   where row[nompozo].ToString() == objeto[0, i].Value.ToString()
                                                    select row).ToArray();
                            foreach (DataRow row in arrayRows)
                            {
                                row[columna] = objeto[1, i].Value;
                            }

                        }

                        break;

                    case "No Existente":
                        for (int i = 0; i < objeto.Rows.Count; i++)
                        {
                            DataRow[] arrayRows = (from DataRow row in dtPrincipal.Rows
                                                   where row[nompozo].ToString() == objeto[0, i].Value.ToString()
                                                   select row).ToArray();


                            foreach (DataRow row in arrayRows)
                            {
                                row[columna] = objeto[1, i].Value;
                            }
                        }

                        break;

                }

                DialogResult dialogResult = MessageBox.Show("El proceso ha terminado. ¿Deseas Aplicar los Cambios?", "Atención", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    dtPrincipal.AcceptChanges();
                    updateTable(dtPrincipal.TableName, dtPrincipal.Copy());
                    
                }
                else if (dialogResult == DialogResult.No)
                {
                    dtPrincipal.RejectChanges();
                }
            }
        }

        

        public float CalculateSimilarity(string s1, string s2)
        {
            if ((s1 == null) || (s2 == null)) return 0.0f;

            double we = 0;

            int t = LevenshteinDistance(s1, s2, out we);
            //int t1 = EditDistance(s1, s2);
            float maxLen = s1.Length;
            if (maxLen < s2.Length)
                maxLen = s2.Length;
            if (maxLen == 0.0F)
                return 1.0F;
            else return 1.0F - t / maxLen;
        }

        public int LevenshteinDistance(string s, string t, out double porcentaje)
        {
            porcentaje = 0;

            // d es una tabla con m+1 renglones y n+1 columnas
            int costo = 0;
            int m = s.Length;
            int n = t.Length;
            int[,] d = new int[m + 1, n + 1];

            // Verifica que exista algo que comparar
            if (n == 0) return m;
            if (m == 0) return n;

            // Llena la primera columna y la primera fila.
            for (int i = 0; i <= m; d[i, 0] = i++) ;
            for (int j = 0; j <= n; d[0, j] = j++) ;


            /// recorre la matriz llenando cada unos de los pesos.
            /// i columnas, j renglones
            for (int i = 1; i <= m; i++)
            {
                // recorre para j
                for (int j = 1; j <= n; j++)
                {
                    /// si son iguales en posiciones equidistantes el peso es 0
                    /// de lo contrario el peso suma a uno.
                    costo = (s[i - 1] == t[j - 1]) ? 0 : 1;
                    d[i, j] = System.Math.Min(System.Math.Min(d[i - 1, j] + 1,  //Eliminacion
                                  d[i, j - 1] + 1),                             //Inserccion 
                                  d[i - 1, j - 1] + costo);                     //Sustitucion
                }
            }

            /// Calculamos el porcentaje de cambios en la palabra.
            if (s.Length > t.Length)
                porcentaje = ((double)d[m, n] / (double)s.Length);
            else
                porcentaje = ((double)d[m, n] / (double)t.Length);
            return d[m, n];
        }
       
        /// <summary>
        /// Devuelve una tabla apartir de una busqueda
        /// </summary>
        /// <param name="nombre">Nombre de la tabla que retornara si esta se encuentra</param>
        /// <returns></returns>
        public override DataTable ValueTable(string nombre)
        {
            return dsCalifica.Tables[dsCalifica.Tables.IndexOf(nombre)];
        }
        
        public override void calificaCustom(string TableNameOrigen, string TableNameMaestra)
        {
            try
            {
                Origen = ValueDictionary(TableNameOrigen);
                Muestra = ValueDictionary(TableNameMaestra);

                DataTable nuevoDT = new DataTable();
                nuevoDT.TableName = "Nuevo";
                nuevoDT = ListaTablas.Find(a => a.TableName == TableNameOrigen);
                dsPrincipal.Tables.Add(nuevoDT);
                DataTable dt = new DataTable();
                dt.TableName = "Reparacion";
                dt = ListaTablas.Find(a => a.TableName == TableNameMaestra);
                Dicc = new Dictionary<string, DataRow>();
                listObj = new List<ObjetoSelecionable>();

                var listaSeleccionA = (from DataRow row in nuevoDT.Rows
                                       select row[Origen.X].ToString()).Distinct().ToList();

                List<string> listaRows = (from DataRow row1 in dt.Rows
                                          select row1[Muestra.X].ToString()).ToList();


                foreach (string cad in listaSeleccionA)
                {
                    try
                    {
                        int numBus = listaRows.FindIndex(m => m.Equals(cad));
                        Dicc.Add(cad, dt.Rows[numBus]);
                    }
                    catch
                    {
                        continue;
                    }
                }
                for (int i = 0; i < dt.Columns.Count; i++)
                {


                    listObj.Add(new ObjetoSelecionable(dt.Columns[i].Caption));
                }

                selec = new userFiltroElemento(ref listObj);
                selec.cmdAceptar.Click += new EventHandler(cmdAceptar_Click);
                selec.EnForm(true);
            }
            catch
            {

            }
        }

        
        void cmdAceptar_Click(object sender, EventArgs e)
        {
            LReentradas = new List<string>();

            LReentradas.Add("-R1");
            LReentradas.Add("-R2");
            LReentradas.Add("-R3");
            LReentradas.Add(" R1");
            LReentradas.Add(" R2");
            LReentradas.Add(" R3");
            LReentradas.Add("R1");
            LReentradas.Add("R2");
            LReentradas.Add("R3");
            LReentradas.Add("re");
            LReentradas.Add("Re");
            LReentradas.Add("RE");

            try
            {
                DataTable dtAuxB = dsPrincipal.Tables["Nuevo"];

                var ty = selec.ListaPozosTotales.FindAll(x => x.Estado).ToList();
                if (ty.Count > 0)
                {

                    int valI = dtAuxB.Columns.Count;
                    string[] cadena = new string[valI];

                    for (int i = 0; i < ty.Count; i++)
                        if (!dtAuxB.Columns.Contains(ty[i].ToString()))
                            dtAuxB.Columns.Add(ty[i].ToString());
                    string cualQuite = "";
                    for (int i = 0; i < dtAuxB.Rows.Count; i++)
                    {
                        string val = dtAuxB.Rows[i][Origen.X].ToString();

                        for (int u = 0; u < LReentradas.Count; u++)
                        {
                            if (val.Contains(LReentradas[u]))
                            {
                                val = val.Replace(LReentradas[u], "");
                                cualQuite = LReentradas[u];
                                break;
                            }
                        }

                        if (Dicc.Keys.Contains(val))
                        {
                            for (int j = valI, k = 0; k < ty.Count; j++, k++)
                            {
                                var tyu = Dicc[val][ty[k].ToString()];
                                dtAuxB.Rows[i][j] = Dicc[val][ty[k].ToString()];
                            }
                        }
                    }
                }
            }
            catch 
            { }
        }



        public override bool ExisteDictionary(string key)
        {
            return Diccionario.ContainsKey(key);
        }
    }

    public abstract class Core_Calificar
    {
        public Dictionary<String, Point> Diccionario;
        protected DataSet dsCalifica;
        public List<DataTable> ListaTablas;
        

        public Core_Calificar() {
            dsCalifica = new DataSet();
        }

        public abstract void Colocar(DataTable val);
        public abstract void addTable(DataTable Tabla);
        public abstract void removeTable(DataTable Tabla);
        public abstract DataTable ValueTable(string nombre);
        public void updateTable(string tableName, DataTable Tabla)
        {
            try
            {
                dsCalifica.Tables.Remove(tableName);
                dsCalifica.Tables.Add(Tabla);
                ListaTablas[ListaTablas.FindIndex(a => a.TableName == tableName)] = Tabla;
            }
            catch { }
        }

        

        public DataTable GuardaDiccionario()
        {


            DataTable Dic = new DataTable("DiccionarioActual");
            Dic.Rows.Clear();
            Dic.Columns.Add("Clave", Type.GetType("System.String"));

            Dic.Columns.Add("PuntoX", Type.GetType("System.Int32"));
            Dic.Columns.Add("PuntoY", Type.GetType("System.Int32"));
            DataRow Reglon;
            foreach (KeyValuePair<string, Point> Par in Diccionario)
            {
                Reglon = Dic.NewRow();
                Reglon[0] = Par.Key;
                Reglon[1] = Par.Value.X;
                Reglon[2] = Par.Value.Y;
                Dic.Rows.Add(Reglon);
            }

            return Dic;
        }




        public abstract void UpdateDictionary(string key, Point punto);
        public abstract bool exiteTabla(string name);
        public abstract void RemoveDictionary(string key);
        public abstract void AddDictionary(string key, Point punto);
        public abstract Point ValueDictionary(string key);
        public abstract void armaDiccionario(Dictionary<string, Point> dicAux);
        public abstract bool ExisteDictionary(string key);
        //Metodos para Calificar tablas
        public abstract void calificaCustom(string TableNameOrigen, string TableNameMaestra);

    }
}
