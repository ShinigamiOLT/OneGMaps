using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

namespace Maps
{

    public partial class Load_txt : Form
    {
        Las archivo;
        DataTable Final= new DataTable("AltaF");
        DataTable Final_2;
        List<ObjetoSelecionable> MisColumnas = new List<ObjetoSelecionable>();
        List<string> FilesL;
        public NotifyIcon Notificacion;
        string Files = "";
        string _Clave;
        OpenData MyOpen = new OpenData();
       DataSet  dtTemporal;

       Dictionary<string, DataTable> Diccionario;
       public Load_txt(Dictionary<string, DataTable> Dic, string PanelMostrar)
       {
           _Clave = PanelMostrar;
           // TODO: Complete member initialization 
           InitializeComponent();
           FilesL = new List<string>();
           Diccionario = Dic;
           Final_2 = new DataTable();
           dtTemporal = new DataSet();
           this.Text = PanelMostrar;
       }
        public Load_txt(Dictionary <string, DataTable> Dic )
        {
            // TODO: Complete member initialization 
            InitializeComponent();
            FilesL = new List<string>();
            Diccionario = Dic;
            Final_2 = new DataTable();
            dtTemporal = new DataSet();
        }

      
        void GrupoTXT(bool band)
        {
            explorerBarGroupItem1.Refresh();
            chEspacio.Visible = band;
            chPunto.Visible = band;
            chSalto.Visible = band;
            chTab.Visible = band;
            chComa.Visible = band;
            cmdCargar.Visible = band;
            labelItem3.Visible = band;
          
        }

        private void Load_txt_Load(object sender, EventArgs e)
        {
            try
            {
                //cbLatitud.SelectedIndex = 0;
                dgvVisualizacion.DataSource = Final;
                EventosDGv even = new EventosDGv(dgvVisualizacion, new NotifyIcon());
                Notificacion = new NotifyIcon();
            }
            catch { }
        }
        void Columnas(int tamanio)
        {
            if (Final.Columns.Count < tamanio)
            {
                for (int i = Final.Columns.Count; i < tamanio; i++)
                {
                    DataColumn columnas = new DataColumn();
                    columnas.ColumnName = "Columna" + i.ToString();
                    Final.Columns.Add(columnas);
                    DataGridViewRow reglon = new DataGridViewRow();
                    reglon.CreateCells(dgvVariables);
                    ObjetoSelecionable neuvo = new ObjetoSelecionable(columnas);
                    reglon.Cells[0].Value = neuvo;
                    reglon.Cells[0].Tag = neuvo;
                    dgvVariables.Rows.Add(reglon);
                    MisColumnas.Add(neuvo);
                }

            }

        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvVariables[e.ColumnIndex, e.RowIndex].Value != null && dgvVariables[e.ColumnIndex, e.RowIndex].Tag != null)
            {
                List<string> lista = (from DataGridViewColumn valor in dgvVisualizacion.Columns
                                      select valor.HeaderText).ToList();

                ObjetoSelecionable objeto = dgvVariables[e.ColumnIndex, e.RowIndex].Tag as ObjetoSelecionable;
                DataTable dt = ((DataColumn)objeto.objeto).Table;
                if (dt.Columns.Contains(dgvVariables[e.ColumnIndex, e.RowIndex].Value.ToString()))
                    dgvVariables[e.ColumnIndex, e.RowIndex].Value = ((DataColumn)objeto.objeto).ColumnName;
                else

                    ((DataColumn)objeto.objeto).ColumnName = dgvVariables[e.ColumnIndex, e.RowIndex].Value.ToString();
                //combo 1

                ColocaElemntos();
            }

        }

        void Carga_Valores()
        {
            MisColumnas.Clear();
            GrupoTXT(true);

            Final.Clear();
            Final.Rows.Clear();
            Final.Columns.Clear();


            dgvVariables.Rows.Clear();
            // StreamReader leer = new StreamReader(Files);
            string[] Informcionacion = File.ReadAllLines(Files, Encoding.Default);
            //
            List<char> Caracteres = new List<char>();
            if (chSalto.Checked)
                Caracteres.Add('\n');
            if (chTab.Checked)
                Caracteres.Add('\t');
            if (chEspacio.Checked)
                Caracteres.Add(' ');
            if (chComa.Checked)
                Caracteres.Add(',');
            if (chPunto.Checked)
                Caracteres.Add('.');
            if (Caracteres.Count > 0)
            {
                char[] separadores = Caracteres.ToArray();
                foreach (string Linea in Informcionacion)
                {
                    string[] lines = Linea.Split(separadores, StringSplitOptions.RemoveEmptyEntries);
                    if (lines.Length > 0)
                    {
                        Columnas(lines.Count());
                        DataRow reglon = Final.NewRow();
                        reglon.ItemArray = lines;

                        Final.Rows.Add(reglon);

                    }
                }
            }
            else
            {
                Columnas(1);
                CambiaTamanio(dgvVariables, 5);
                dgvVisualizacion.Rows.Add(Informcionacion);


            }
            ColocaElemntos();
            this.Text="Filas" + Final.Rows.Count.ToString();

           


        }
        void ColocaElemntos()
        {
        }

        private void ColFecha_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        void Carga()
        {   
        }

        private void cmdCargar_Click(object sender, EventArgs e)
        {
            if (Files.Length > 0)
            {
                Carga_Valores();
            }
        }



        public void Abrir_Las(string Direccion)
        {
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(Direccion, System.Text.Encoding.Default);
                string texto;
                texto = sr.ReadToEnd();
                sr.Close();


                archivo = new Las(texto);
                dgvVisualizacion.Columns.Clear();
                //  dgvVisualizacion.DataSource = archivo.Dt_Tabla_datos_5;
                Final = archivo.Dt_Tabla_datos_5.Copy();
                //Visor_Datos vd = new Visor_Datos();
                //vd.EstableceDatos(archivo.dt_CURVE_INFORMATION_BLOCK_2_4);
                //vd.Rapida(true);
                ConfiguraTabla();
            }
            catch (SystemException er)
            {

                MessageBox.Show(er.Message);
            }

            this.Text = "Filas" + Final.Rows.Count.ToString();
        }
        private void ConfiguraTabla()
        {
            if (dgvVariables.Columns.Count == 1)
            {
                DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
                columna.HeaderText = "Descripcion";
                columna.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                columna.SortMode = DataGridViewColumnSortMode.NotSortable;
                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvVariables.Columns.Add(columna);
            }

            dgvVariables.Rows.Clear();
            int i = 0;
            MisColumnas.Clear();
            foreach (DataColumn col in Final.Columns)
            {
                DataGridViewRow reglon = new DataGridViewRow();
                reglon.CreateCells(dgvVariables);
                ObjetoSelecionable neuvo = new ObjetoSelecionable(col);
                reglon.Cells[0].Value = neuvo;
                reglon.Cells[0].Tag = neuvo;
                reglon.Cells[1].Value = archivo.dt_CURVE_INFORMATION_BLOCK_2_4.Rows[i][3].ToString();
                i++;
                dgvVariables.Rows.Add(reglon);
                MisColumnas.Add(neuvo);

            }

            ColocaElemntos();

            CambiaTamanio(dgvVariables, 8);

        }
        void CambiaTamanio(DataGridView dgv, int max)
        {
            try
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
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void chSinfecha_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxItem2_Click(object sender, EventArgs e)
        {
            try
            {
                controlContainerItem3.Visible = true;
                OpenFileDialog Abrir = new OpenFileDialog();
                Abrir.Filter = "Todos los Archivos (*.*) | *.*";
                Abrir.FilterIndex = 1;

                if (Abrir.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Files = Abrir.FileName;
                    dgvVisualizacion.DataSource = null;
                    //aqui va para e texto normal
                    if (Abrir.SafeFileName.Substring(Abrir.SafeFileName.LastIndexOf('.')).ToUpper() != ".LAS")

                        Carga_Valores();
                    else
                    {
                        //aqui va para el las
                        Abrir_Las(Files);
                    }
                }
                dgvVisualizacion.DataSource = Final;
            }
            catch { }
        }

        private void checkBoxItem1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog SelecionaCarpeta= new FolderBrowserDialog();
            Constantes_Conf config = new Constantes_Conf();
                // SelecionaCarpeta
            // 
            SelecionaCarpeta.Description = "Ruta de los archivos a cargar (*.OnePro)";
            SelecionaCarpeta.ShowNewFolderButton = false;
            // 
             


                SelecionaCarpeta.ShowNewFolderButton = false;

                SelecionaCarpeta.SelectedPath = config.Carga;

                SelecionaCarpeta.ShowDialog();


                MyOpen.DirectoryWorking = SelecionaCarpeta.SelectedPath + "\\";

                MyOpen.Filtro = ".las";
                MyOpen.LoadArchive();

                MyOpen.Show();
                //despues de haber marcado la carpeta de trabajo mandamos a visualizar los archivos que contiene

                

                    try
                    {
                        Notificacion.ShowBalloonTip(500, "Informacion - " + Application.ProductName, "Espere... leyendo valores del Archivo. \n Puede tardar varios minutos", ToolTipIcon.Warning);

           

                        Notificacion.ShowBalloonTip(100, "Informacion - " + Application.ProductName, "Espere... leyendo " + MyOpen.Archivo, ToolTipIcon.Warning);

                        //mandemos a guardar los files a un nuevo archivo
                        FilesL.Clear();
                        foreach (ObjetoSelecionable obj in MyOpen.Lista)
                        {
                            if (obj.Estado)
                            FilesL.Add(    obj.ToString());
                        }
                        


           
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                   

                    System.GC.Collect();

                    // Pestania_Principal.SelectedTabIndex = 1;

               
                   
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void CargaPLT()
        {
            //if (FilesL.Count == 0)
            //    MessageBox.Show("No ha seleccionado ningun archivo o no existe!!", "Error!!!");
            //else
            //{

            //    //carguemos el monton de datos
            //    chSinfecha.Checked = true;
            //    foreach (string cad in FilesL)
            //    {
            //        txtNombreTabla.Text = cad;
            //        Abrir_Las(MyOpen.DirectoryWorking + "\\" + cad);
            //        Carga();
            //        Notificacion.ShowBalloonTip(1000, "Informacion - " + Application.ProductName, "Carga Completa!!! " + cad, ToolTipIcon.Warning);

            //    }
            //    MessageBox.Show("Se termino de cargar los archivos marcados");
            //    Close();
            //}
        }
        private void cmdcargaD_Click(object sender, EventArgs e)
        {
            if (textBoxItem1.Text != "")
            {
                Final.AcceptChanges();
                Final.TableName = textBoxItem1.Text;
                if (!Diccionario.ContainsKey(_Clave + ":" + textBoxItem1.Text.ToUpper()))
                    Diccionario.Add(_Clave + ":" + textBoxItem1.Text.ToUpper(), Final);
                else MessageBox.Show("Nombre Ya Existe");
                Close();
            }
        }



        private void chxml_Click(object sender, EventArgs e)
        {
            //aqui mandemos a abir un archivo
            controlContainerItem4.Visible = true;

            OpenFileDialog Abre = new OpenFileDialog();


            Abre.Filter = "Todos los Archivos (*.*) | *.*";
            if (Abre.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Notificacion.ShowBalloonTip(2000, "Trabajando.", "Espere mientras que se cargan los registros", ToolTipIcon.Warning);

                try
                {
                    dtTemporal.ReadXml(Abre.FileName);


                    Notificacion.ShowBalloonTip(500, "Informacion - " + Application.ProductName, "Listo... Carga completada", ToolTipIcon.Info);

                    cmbListatabla.Items.Clear();

                    foreach (DataTable dt in dtTemporal.Tables)
                    {
                        cmbListatabla.Items.Add(dt.TableName);
                    }
                    this.Text = Application.ProductName + " - File: " + Abre.FileName;

                }
                catch
                {
                    MessageBox.Show("Este archivo no  puede ser cargado.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }

        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            

        }
        private void LeeArchivoExcel(string Ruta)
        {

            cmbListatabla.Items.Clear();
            //En tiempo de ejecucion se crea el tipo de base de datos que se abrira   

            try
            {
                DataTable libro;
                OleDbConnection conexion = new OleDbConnection();// factory.CreateConnection();
                // String cadena = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Ruta + ";Extended Properties= Excel 8.0;";
                string cadena = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Ruta + ";Extended Properties=\"Excel 12.0;HDR=YES\"";
                conexion.ConnectionString = cadena;
                conexion.Open();
                libro = conexion.GetSchema("Tables");
                //CNumeroHoja.Items.Clear();
                List<string> ListaHojas = new List<string>();
                foreach (DataRow dr in libro.Rows)
                {
                    string hoja = dr["TABLE_NAME"].ToString();
                    //las siguientes líneas "limpian" el nombre de caracteres especiales
                    try
                    {
                        if (!hoja.Contains("Print_Area") && !hoja.ToLower().Contains("grafic") && !hoja.Contains("Grafic"))
                        {
                            hoja = hoja.Substring(0, hoja.LastIndexOf("$"));
                            if (hoja.IndexOf("'") == 0)
                                hoja = hoja.Remove(0, 1);
                            //   CNumeroHoja.Items.Add(hoja.ToString());
                            // HojasActuales.Items.Add(hoja.ToString());
                         //   ListaHojas.Add(hoja.ToString().ToLower());
                            if (!cmbListatabla.Items.Contains(hoja.ToString().ToLower()))
                            {
                                cmbListatabla.Items.Add(hoja.ToString().ToLower());
                            }
                           
                        }

                    }
                    catch { }
                }
                conexion.Close();
                //Si hay hojas k leer marcamos la primera como leida.
               
            }
            catch (Exception Ex)
            {
                // MessageBox .Show(Ex.Message);
                //  Errores +="\n No se cargo Hoja: "+Hojas
            }


        }
        DataTable RegresaHoja(string Hoja, string Archivo)
        {
            DataTable Temp = new DataTable();
            DataSet MIDATA = new DataSet();
            try
            {
                OleDbConnection objConn = new OleDbConnection();
                OleDbCommand objCmd = new OleDbCommand();
                OleDbDataAdapter objDa = new OleDbDataAdapter();
                //personalizar

                // Leer el path del archivo Excel que tiene los datos del cliente

                // Si el archivo existe...

                // Atencion: Esta es la cadena de conexion (apta para archivos xlsx). La misma lee el archivo especificado en el path
                objConn.ConnectionString = ("Provider=Microsoft.ACE.OLEDB.12.0;" +
                ("Data Source=" + (Archivo + ";Extended Properties=\"Excel 12.0;HDR=YES\"")));
                objConn.Open();
                objCmd.CommandText = "SELECT * FROM [" + Hoja + "$]";
                objCmd.Connection = objConn;
                objDa.SelectCommand = objCmd;

                // Llenar el DataSet
                objDa.Fill(MIDATA);

                //cerrar la conexion
                objConn.Close();
                Temp = MIDATA.Tables[0];

                //

            }
            catch (Exception ex)
            {
                //dtsPlantas = null;
            }



            return Temp.Copy();
        }

        private void chExcel_Click(object sender, EventArgs e)
        {
            //aqui mandemos a abir un archivo
            OpenFileDialog Abre = new OpenFileDialog();

            controlContainerItem4.Visible = true;
            Abre.Filter = "Archivos Excel (*.xls*) | *.xls*";
            if (Abre.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                Notificacion.ShowBalloonTip(2000, "Trabajando.", "Espere mientras que se cargan los registros", ToolTipIcon.Warning);

                try
                {
                    LeeArchivoExcel(Abre.FileName);
                    Files = Abre.FileName;
                    Notificacion.ShowBalloonTip(500, "Informacion - " + Application.ProductName, "Listo... Carga completada", ToolTipIcon.Info);

                   
                    this.Text = Application.ProductName + " - File: " + Abre.FileName;
                }
                catch
                {
                    MessageBox.Show("Este archivo no  puede ser cargado.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }
        //aqui es para el caso de los xml y dataset;

        void CargaTablaColumnas()
        {
            MisColumnas.Clear();
            dgvVariables.Rows.Clear();
            if (dgvVariables.Columns.Count > 1)
            {
                dgvVariables.Columns.RemoveAt(dgvVariables.Columns.Count - 1);
            }

            //ahora lo que haremos es cargar las columnas 

            foreach(DataColumn col in Final.Columns )
            {

                DataGridViewRow reglon = new DataGridViewRow();
                reglon.CreateCells(dgvVariables);
                ObjetoSelecionable neuvo = new ObjetoSelecionable(col);
                reglon.Cells[0].Value = neuvo;
                reglon.Cells[0].Tag = neuvo;
                dgvVariables.Rows.Add(reglon);
                MisColumnas.Add(neuvo);
            }
            CambiaTamanio(dgvVariables, 8);
        
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //saquemos el texto y veamos el valor
            controlContainerItem3.Visible = true;
            if (cmbListatabla.SelectedItem != null)
            {
                string cad = "";
                cad = cmbListatabla.SelectedItem.ToString();
                if (chxml.Checked)
                {
                    Final = dtTemporal.Tables[cad].Copy();
                }
                if (chExcel.Checked)
                {
                    Final = RegresaHoja(cad, Files);

                }


                dgvVisualizacion.DataSource = Final;
               
                //  visor.EstableceDatos(dtTemporal.Tables[cad]);
                //  this.Controls.Add(visor);
                // visor.Dock = DockStyle.Fill;
                CargaTablaColumnas();
                ColocaElemntos();
            }
            
        }
    }

    
      class Constantes_Conf
    {
        public string Ruta_Trabajo = "";
        string Ruta_Trabajo_Carga = "";
        string Ruta_Trabajo_Perfil = "";
        string Ruta_Trabajo_Produccion = "";

        string Ruta_Trabajo_Jera = "";
        string Ruta_Trabajo_Exporta = "";
        string Ruta_Trabajo_Estado;

        public Constantes_Conf()
        {
            if (System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Onepro_Suite\").Exists)
                Ruta_Trabajo = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Onepro_Suite\";
            else
                Ruta_Trabajo = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            carga();
        }
        //aqui va para la carga
        void carga()
        {
            if (System.IO.Directory.CreateDirectory(Ruta_Trabajo + "\\Carga\\").Exists)

                Ruta_Trabajo_Carga = Ruta_Trabajo + @"Carga";
            else
                Ruta_Trabajo_Carga = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }
        public string Carga
        {
            get { carga(); return Ruta_Trabajo_Carga; }
        }

        //aqui va para la salida perfil
        void SalidaPerfil()
        {
            if (System.IO.Directory.CreateDirectory(Ruta_Trabajo + "\\Perfil\\").Exists)

                Ruta_Trabajo_Perfil = Ruta_Trabajo + @"Perfil";
            else
                Ruta_Trabajo_Perfil = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        public string Perfil
        {
            get { SalidaPerfil(); return Ruta_Trabajo_Perfil; }

        }

        //aqui va para la salida Estado Mecanico
        void SalidaEstado()
        {
            if (System.IO.Directory.CreateDirectory(Ruta_Trabajo + "\\EstadoMecanico\\").Exists)

                Ruta_Trabajo_Estado = Ruta_Trabajo + @"EstadoMecanico";
            else
                Ruta_Trabajo_Estado = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        public string Estado
        {
            get { SalidaEstado(); return Ruta_Trabajo_Estado; }

        }




        //aqui va para la salida de Produccion
        void SalidaProduccion()
        {
            if (System.IO.Directory.CreateDirectory(Ruta_Trabajo + "\\Produccion\\").Exists)

                Ruta_Trabajo_Produccion = Ruta_Trabajo + @"Produccion";
            else
                Ruta_Trabajo_Produccion = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        public string Produccion
        {
            get { SalidaProduccion(); return Ruta_Trabajo_Produccion; }
        }
        //aqui va para la salida de Exportar
        void SalidaExpo()
        {
            if (System.IO.Directory.CreateDirectory(Ruta_Trabajo + "\\Exportar\\").Exists)

                Ruta_Trabajo_Exporta = Ruta_Trabajo + @"Exportar";
            else
                Ruta_Trabajo_Exporta = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        public string Exportar
        {
            get { SalidaExpo(); return Ruta_Trabajo_Exporta; }
        }
        //jera
        //aqui va para la salida de Exportar
        void SalidaJera()
        {
            if (System.IO.Directory.CreateDirectory(Ruta_Trabajo + "\\Jera\\").Exists)

                Ruta_Trabajo_Jera = Ruta_Trabajo + @"Jera";
            else
                Ruta_Trabajo_Jera = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        public string Jera
        {
            get { SalidaJera(); return Ruta_Trabajo_Jera; }
        }

        void cargaConfiguara()
        {

        }
    }
    
}

