using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Text.RegularExpressions;
using System.Threading;
using DevComponents.DotNetBar.Controls;
using One_Produccion.Editar_Tabla;

namespace Maps
{
    public partial class one_form_Produccion_OrigenDatos : Form
    {
        private One_Core_Produccion Core_produccion;
        DataTable Tabladatos;//tabla diccionario
        DataTable dtFormaciones, dfListaNombresPozos;
        List<DataTable> Lista_tablas;

        List<Color> Colores = new List<Color>();

        NotifyIcon Notificacion;
        int ID = 0; 

        public one_form_Produccion_OrigenDatos(int id, One_Core_Produccion Core_produccion, NotifyIcon Noti)
        {
            InitializeComponent();
            this.Core_produccion = Core_produccion;
            Notificacion = Noti;
            Colores.Add(Color.Red);
            Colores.Add(Color.Blue);
            Colores.Add(Color.Magenta);
            Colores.Add(Color.DarkGreen);
            Colores.Add(Color.OrangeRed);
            ID = id;

        }
        //para cualquier dataset 
        public one_form_Produccion_OrigenDatos(int id, DataSet Datos_gene, NotifyIcon Noti)
        {
            InitializeComponent();
            dgvList = new List<DataTable>();
            lTabla = new List<string>();
            // TODO: Complete member initialization
            Notificacion = Noti;
            Datos_genericos = Datos_gene;
            Colores.Add(Color.Red);
            Colores.Add(Color.Blue);
            Colores.Add(Color.Magenta);
            Colores.Add(Color.DarkGreen);
            Colores.Add(Color.OrangeRed);
            ID = id;
            Core_produccion = new One_Core_Produccion();

        }
        
       
        private void show_chkBox(int Columna)
        {
            Rectangle rect = dgvEventos.GetCellDisplayRectangle(Columna, -1, true);
            // set checkbox header to center of header cell. +1 pixel to position 
            rect.Y = 3;
            rect.X = rect.Location.X + (rect.Width / 8);
            CheckBox checkboxHeader = new CheckBox();
            checkboxHeader.Tag = Columna;
            checkboxHeader.Name = "checkboxHeader";
            //datagridview[0, 0].ToolTipText = "sdfsdf";
            checkboxHeader.Size = new Size(18, 18);
            checkboxHeader.Location = rect.Location;
            checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);
            dgvEventos.Controls.Add(checkboxHeader);
        }

        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox headerBox = ((CheckBox)dgvEventos.Controls.Find("checkboxHeader", true)[0]);
            int index = Convert.ToInt32(headerBox.Tag);
            for (int i = 0; i < dgvEventos.RowCount; i++)
            {
                dgvEventos.Rows[i].Cells[index].Value = headerBox.Checked;
            }
        }

        private void one_form_Produccion_OrigenDatos_Load(object sender, EventArgs e)
        {
            Lista_tablas = new List<DataTable>();
            foreach (DataTable tabla in Datos_genericos.Tables)
            {
                CreaPestanias(tabla, false);
                dgvList.Add(tabla);
                lTabla.Add(tabla.TableName);
            }

            System.Threading.Thread hilo1 = new System.Threading.Thread(new System.Threading.ThreadStart(p1));
            hilo1.Start();
            System.Threading.Thread hilo2 = new System.Threading.Thread(new System.Threading.ThreadStart(p2));
            hilo2.Start();
            NucleoCalificador = new CoreCalifica(Datos_genericos);
        }
        
        void ColocandoCheck()
        {

            var tipo = TabPrincipal.Controls.OfType<DevComponents.DotNetBar.SuperTabControlPanel>().AsEnumerable();

            foreach (DevComponents.DotNetBar.SuperTabControlPanel Panels in tipo)
            {
                //   DevComponents.DotNetBar.SuperTabControlPanel tabs = (DevComponents.DotNetBar.SuperTabControlPanel)Panels.AttachedControl;
                try
                {
                    DataGridView dgv = (DataGridView)Panels.Controls[0];
                    //sacamos los dgv 
                    EventosDGv edg = (EventosDGv)dgv.Tag;
                    edg.Especial();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }
        void CargaPanelCore()
        {
            Lista_tablas = new List<DataTable>();

            switch (ID)
            {
                case 0:
                   // Core_produccion.Unidad.OrdenaPorPozo();
                    Core_produccion.Unidad.DtEventos.Columns[0].DefaultValue = true;
                    CreaPestanias(Core_produccion.Unidad.DtEventos, false);



                    foreach (DataTable tabla in Core_produccion.Unidad.dtTablas_Informacion.Tables)
                    {
                        //    if (tabla.Namespace != "Diccionario" && tabla.Namespace != "PlantillasTabla")
                        CreaPestanias(tabla, true);
                    }
                    grupoedicion.Visible = false;
                    Text = "Datos Produccion - File " + Core_produccion.Unidad.File;
                    break;
                case 1:

                    
                case 2:
                    

                    
                case 99:

                    foreach (DataTable tabla in Datos_genericos.Tables)
                    {
                        if (tabla.Columns.Count > 1)
                            CreaPestanias(tabla, true);
                    }
                    grupoedicion.Visible = false;
                    Text = "Datos de Declinacion";

                    break;
            }




        }
        void CreaPestanias(DataTable datatable, bool cerrar)
        {

            if (ExisteTabla(datatable.TableName))
                return;
            Lista_tablas.Add(datatable);
            DevComponents.DotNetBar.SuperTabControlPanel TabPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
            DevComponents.DotNetBar.Controls.DataGridViewX dgv = new DevComponents.DotNetBar.Controls.DataGridViewX();
            DevComponents.DotNetBar.SuperTabItem TabInicial = new DevComponents.DotNetBar.SuperTabItem();


            TabPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(dgv)).BeginInit();

            // TabInicial
            // 
            TabInicial.AttachedControl = TabPanel1;
            TabInicial.GlobalItem = false;
            TabInicial.Name = datatable.TableName;
            TabInicial.Text = "superTabItem1";
            //  
            TabPanel1.Controls.Add(dgv);
            TabPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            TabPanel1.Location = new System.Drawing.Point(0, 25);
            TabPanel1.Name = "TabPanel1";
            TabPanel1.Size = new System.Drawing.Size(1060, 444);
            TabPanel1.TabIndex = 0;
            TabPanel1.TabItem = TabInicial;
            // 
            // dgv
            // 
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            //dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            dgv.DefaultCellStyle = dataGridViewCellStyle1;
            dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            dgv.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            dgv.Location = new System.Drawing.Point(0, 0);
            dgv.Name = "dgv";
            dgv.Size = new System.Drawing.Size(1060, 444);
            dgv.TabIndex = 0;
            dgv.DataSource = datatable;
            TabInicial.Text = datatable.TableName;
            TabInicial.CloseButtonVisible = cerrar;
            dgv.AllowUserToAddRows = true;

            //

            TabPrincipal.Controls.Add(TabPanel1);
            TabPrincipal.Tabs.Add(TabInicial);
            TabPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(dgv)).EndInit();
            // dgv.KeyDown += new System.Windows.Forms.KeyEventHandler(Encabezados_KeyDown);
            dgv.AutoSize = true;


            dgv.KeyDown += new System.Windows.Forms.KeyEventHandler(Tabla_CamposFiltro_KeyDown);
            dgv.KeyUp += new KeyEventHandler(dgv_KeyUp);
            EventosDGv dg = new EventosDGv(dgv, Notificacion);
            // dgv.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(dgvorden_ColumnHeaderMouseClick);
            dgv.Tag = dg;
            dg.IsOrigen = true;


            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Programmatic;// = DataGridViewAutoSizeColumnMode.Fill;
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //  MessageBox.Show(col.Name +" - "+ col.CellType.Name.ToString()+" -"+ col.CellTemplate.ToString());


            }


            switch (ID)
            {
                case 0:
                    if (datatable.TableName == "Tabla_Eventos")
                    {
                        foreach (DataGridViewColumn col in dgv.Columns)
                        {
                            //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        }
                        dgvEventos = dgv;
                        //dgv.Columns["Visible"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                        //dgv.Columns["Visible"].SortMode = DataGridViewColumnSortMode.NotSortable;

                    }
                    break;
                case 99:
                    if (dgv.Columns.Count > 1)
                        dgv.Columns[0].DefaultCellStyle.Format = "F3";

                    break;
            }
        }

        bool ExisteTabla(string name)
        {
            return (TabPrincipal.Tabs.Contains(name));

        }

        void dgv_KeyUp(object sender, KeyEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if (e.Control == true && (e.KeyValue == 187))//|| e.KeyValue == 187))
            {
                InsertaReglon();
            }

        }

        void InsertaReglon()
        {
            DataGridView dgv = new DataGridView();

            try
            {
                dgv = (DataGridView)TabPrincipal.SelectedPanel.Controls[0];

                DataTable t = (DataTable)dgv.DataSource;
                DataRow reglon = t.NewRow();
                t.Rows.InsertAt(reglon, dgv.CurrentCell.RowIndex);
                // dgv.Rows.Insert(, 1);
            }
            catch (Exception ex)
            {


            }
        }

        private void dgvorden_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            //  MessageBox.Show( dataGridViewX1.SortOrder.ToString()+ "xxx"+e.RowIndex.ToString());
            DataGridView dgv = (DataGridView)sender;

            try
            {


                DataTable t = (DataTable)dgv.DataSource;
                string Columna = dgv.Columns[e.ColumnIndex].Name;


                DataGridViewColumn newCol = dgv.Columns[e.ColumnIndex];
                ListSortDirection direction;

                if (newCol != null)
                {
                    // Sort the same column again, reversing the SortOrder.
                    if (newCol.HeaderCell.SortGlyphDirection == SortOrder.Ascending)
                    {
                        direction = ListSortDirection.Descending;
                        Ordena(t, Columna, direction);
                        newCol.HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                    else
                    {
                        // Sort a new column and remove the old SortGlyph.
                        direction = ListSortDirection.Ascending;
                        // newCol.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                        Ordena(t, Columna, direction);
                        newCol.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }

                }



                //    newCol.HeaderCell.SortGlyphDirection =            direction == ListSortDirection.Ascending ?            SortOrder.Ascending : SortOrder.Descending;

            }
            catch (Exception ex)
            {


            }


        }


        internal void Ordena(DataTable Tabla_unidad, string campo, ListSortDirection sortOrder)
        {

            //DataRow reglonTemp;
            //aqui es la para lo de los decimales.
            MessageBox.Show("Comenzamos" + Tabla_unidad.Rows.Count.ToString());
            Visor_Datos vd = new Visor_Datos();
            try
            {
                if (Tabla_unidad.Columns.Contains(campo))
                {
                    Notificacion.ShowBalloonTip(500, "Informacion - " + Application.ProductName, "Comenzando la ordenación. \n Puede tardar varios minutos", ToolTipIcon.Warning);
                    //se supone qeu podrian haber campos que no son numericos. asi que lo que haremos primero sera sacar en una tablita los que son letras
                    DataTable Letras = Tabla_unidad.Clone();
                    DataTable Numeros = Tabla_unidad.Clone();
                    DataTable Tiempo = Tabla_unidad.Clone();
                    DataTable VaciosoNulos = Tabla_unidad.Clone();
                    DataTable Basura = Tabla_unidad.Clone();
                    double dtemp = 0;
                    DateTime tiempo = DateTime.Now;

                    List<DataRow> ListaNueva = (from row in Tabla_unidad.AsEnumerable() where (row[campo] == null || row[campo].ToString().Trim() == "" || row[campo].ToString().Trim() == " ") select row).ToList();


                    foreach (DataRow row in ListaNueva)
                    {
                        //mover los Nulos
                        VaciosoNulos.ImportRow(row);
                        Tabla_unidad.Rows.Remove(row);


                    }

                    //List<DataRow> RowVacios = (from row in Tabla_unidad.AsEnumerable().Where(p => p.Field<string>(campo) != null)
                    //                           where (row[campo].ToString().Trim() == "")
                    //                           select row).ToList();

                    ///*  foreach (DataRow row in RowVacios)
                    //  {
                    //      VaciosoNulos.ImportRow(row);
                    //      //Tabla_unidad.Rows.Remove(row);

                    //  }*/
                    //foreach (DataRow row in RowVacios)
                    //{
                    //    VaciosoNulos.ImportRow(row);
                    //    Tabla_unidad.Rows.Remove(row);

                    //}

                    //aqui vemos las fraciones
                    IEnumerable<DataRow> Rowfraccion = (from row in Tabla_unidad.AsEnumerable().Where(p => p.Field<string>(campo) != null)
                                                        where (IsFraccion(row[campo].ToString()))
                                                        select row).ToList();

                    foreach (DataRow row in Rowfraccion)
                    {
                        //convertir las Fracciones
                        // Numeros.Rows.Add(row);
                        row[campo] = Fraccion(row[campo].ToString());

                    }
                    //aqui van los numeros
                    List<DataRow> RowNumero = (from row in Tabla_unidad.AsEnumerable().Where(p => p.Field<string>(campo) != null)
                                               where (IsNumeric(row[campo].ToString()) && double.TryParse(row[campo].ToString(), out dtemp))
                                               select row).ToList();

                    foreach (DataRow row in RowNumero)
                    {
                        // Numeros.Rows.Add(row);
                        //mover los numeros
                        Numeros.ImportRow(row);
                        Tabla_unidad.Rows.Remove(row);

                    }


                    //aqui van las fechas
                    List<DataRow> Rowfechas = (from row in Tabla_unidad.AsEnumerable().Where(p => p.Field<string>(campo) != null)
                                               where (DateTime.TryParse(row[campo].ToString(), out tiempo))
                                               select row).ToList();

                    foreach (DataRow row in Rowfechas)
                    {
                        // Numeros.Rows.Add(row);
                        //mover los numeros
                        Tiempo.ImportRow(row);
                        Tabla_unidad.Rows.Remove(row);

                    }


                    //aqui van los letras
                    List<DataRow> RowLetras = (from row in Tabla_unidad.AsEnumerable().Where(p => p.Field<string>(campo) != null).Where(p => p.Field<string>(campo) != "")

                                               where (!IsNumeric(row[campo].ToString()))
                                               select row).ToList();

                    foreach (DataRow row in RowLetras)
                    {
                        // 
                        //Letras.Rows.Add(row);
                        //mover las letras.
                        Letras.ImportRow(row);
                        Tabla_unidad.Rows.Remove(row);
                    }

                    //ahora movemos cualquier tipo de basura que haya quedado en la tabla

                    Basura = Tabla_unidad.Copy();

                    Tabla_unidad.Rows.Clear();
                    //vemaos si letra y mas no son lo mismo.
                    //Tabla_unidad = Numeros.Copy();


                    //  catch (Exception ex) { MessageBox.Show("Error cuando sacamos datos"); }


                    if (sortOrder == ListSortDirection.Descending)
                    {

                        try
                        {

                            // where (double.TryParse(row[campo].ToString(), out dtemp))
                            IEnumerable<DataRow> CampoInicial = from row in Numeros.AsEnumerable().Where(p => p.Field<string>(campo) != null)

                                                                orderby Convert.ToDouble(row[campo].ToString()) descending
                                                                select row;

                            if (CampoInicial.Count() > 0)
                                Tabla_unidad.Merge(CampoInicial.CopyToDataTable());

                            //tiempo
                            IEnumerable<DataRow> Campofecha = from row in Tiempo.AsEnumerable().Where(p => p.Field<string>(campo) != null)
                                                              orderby DateTime.Parse(row[campo].ToString()) descending
                                                              select row;
                            if (Campofecha.Count() > 0)
                                Tabla_unidad.Merge(Campofecha.CopyToDataTable());



                            //letras

                            IEnumerable<DataRow> CampoLetra = from row in Letras.AsEnumerable().Where(p => p.Field<string>(campo) != null) orderby row.Field<string>(campo) descending select row;

                            if (CampoLetra.Count() > 0)
                                Tabla_unidad.Merge(CampoLetra.CopyToDataTable());


                        }
                        catch (Exception ex) { MessageBox.Show("ordenacion decendente"); }

                    }
                    //para decendente
                    else
                    {
                        try
                        {
                            //where (double.TryParse(row[campo].ToString(), out dtemp))

                            IEnumerable<DataRow> CampoInicial = from row in Numeros.AsEnumerable().Where(p => p.Field<string>(campo) != null)

                                                                orderby Convert.ToDouble(row[campo].ToString()) ascending
                                                                select row;

                            if (CampoInicial.Count() > 0)
                                Tabla_unidad.Merge(CampoInicial.CopyToDataTable());

                            //tiempo
                            IEnumerable<DataRow> Campofecha = from row in Tiempo.AsEnumerable().Where(p => p.Field<string>(campo) != null)
                                                              orderby DateTime.Parse(row[campo].ToString()) ascending
                                                              select row;
                            if (Campofecha.Count() > 0)
                                Tabla_unidad.Merge(Campofecha.CopyToDataTable());

                            //numero
                            IEnumerable<DataRow> CampoLetra = from row in Letras.AsEnumerable().Where(p => p.Field<string>(campo) != null)
                                                              orderby row.Field<string>(campo) ascending
                                                              select row;

                            if (CampoLetra.Count() > 0)
                                Tabla_unidad.Merge(CampoLetra.CopyToDataTable());



                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show("ordenacion acendente");
                        }
                    }
                    //agreguemos los vacios nulos y la basura
                    Tabla_unidad.Merge(VaciosoNulos);
                    Tabla_unidad.Merge(Basura);
                    Notificacion.ShowBalloonTip(500, "Informacion - " + Application.ProductName, "Se termino de ordenar", ToolTipIcon.Info);

                }//de las columnas
            }
            catch (SystemException er)
            {
                MessageBox.Show(er.Message);
            }
            MessageBox.Show("Terminamos" + Tabla_unidad.Rows.Count.ToString());

        }//de la clase
        private bool IsNumeric(string number)
        {

            if (number.Trim().Length < 1)
                return false;
            Regex pattern = new Regex(@"^(\d*)([.]*)(\d)(\d*)$");//(@"^[-]?[\d](.\d)?$");//("[^0-9][.]?[^0-9]");
            MatchCollection temp = pattern.Matches(number.Trim());

            if (temp.Count == 1)
                return true;
            return false;

            //  return pattern.IsMatch(number.Trim());
        }
        private bool IsFraccion(string number)
        {

            if (number.Trim().Length < 1)
                return false;
            Regex pattern = new Regex(@"^(-)?(\d)(\d)*[\' ']*(/)[\' ']*(\d)(\d)*$");//(@"^[-]?[\d](.\d)?$");//("[^0-9][.]?[^0-9]");
            MatchCollection temp = pattern.Matches(number.Trim());
            //if (temp.Count == 0)
            //    txtSalida.Text += "\n" + "Error";
            //for (int i = 0; i < temp.Count; i++)
            //{
            //    txtSalida.Text += "\n(" + i.ToString() + ")" + temp[i].Value;
            //}
            if (temp.Count == 1)
                return true;
            return false;

            //  return pattern.IsMatch(number.Trim());
        }
        double Fraccion(string frac)
        {
            double a = 0, b = 0;
            try
            {
                string[] Valor = frac.Split('/');
                a = Convert.ToDouble(Valor[0]);
                b = Convert.ToDouble(Valor[1]);
            }
            catch { }
            if (b == 0)
                return b;
            return Math.Round(a / b, 3);
        }
        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {

        }
        private void Tabla_CamposFiltro_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;
                if (e.KeyValue == 8 || e.KeyValue == 46)
                {
                    DataGridViewSelectedCellCollection Selec = dgv.SelectedCells;
                    for (int i = 0; i < Selec.Count; i++)
                    {
                        Selec[i].Value = null;
                        Selec[i].Tag = null;
                        Selec[i].Style.BackColor = Color.White;
                    }

                }


            }
            catch { }
        }
        DataGridView dgvEventos;

        private void Encabezados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 86 && e.Control)
            {




                Pegar_Datgridview((DataGridView)sender);//Actual);
            }
        }
        void Pegar_Datgridview(DataGridView dgv)
        {
            try
            {

                DataTable Temp = new DataTable();
                char[] rowSplitter = new char[] { '\n', '\r' };
                char[] columnSplitter = new char[] { '\t', '\n' };
                //obtengo el texto desde clipboard
                IDataObject dataInClipboard = Clipboard.GetDataObject();
                String stringInClipboard = Convert.ToString(dataInClipboard.GetData(DataFormats.Text));
                //'split it into lines
                String[] filas_en_clipboard = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries);
                //'get the row and column of selected cell in grid 
                int r, c;
                try
                {
                    r = dgv.SelectedCells[0].RowIndex;
                    c = dgv.SelectedCells[0].ColumnIndex;

                }
                catch
                {
                    r = 0; c = 0;
                }
                for (int iCol = 0; iCol < filas_en_clipboard[0].Split(columnSplitter).Length; iCol++)
                {
                    //'assign cell value, only if it within columns of the grid
                    Temp.Columns.Add();
                }
                DataRow Reglon;
                for (int iRow = 0; iRow < filas_en_clipboard.Length; iRow++)
                {
                    //split row into cell values
                    Reglon = Temp.NewRow();
                    //split row into cell values
                    String[] valuesInRow = filas_en_clipboard[iRow].Split(columnSplitter);
                    for (int iCol = 0; iCol < valuesInRow.Length; iCol++)
                    {
                        //     // 'assign cell value, only if it within columns of the grid

                        Reglon[iCol] = valuesInRow[iCol];
                    }
                    Temp.Rows.Add(Reglon);

                }


                if (Temp.Rows.Count > 0)
                {
                    // Notificacion.ShowBalloonTip(2000, "Informacion - " + Application.ProductName, "Espere... leyendo valores del portapapeles. \n Puede tardar varios minutos", ToolTipIcon.Warning);
                    DateTime Inicio = DateTime.Now;

                    if (dgv.Rows.Count < (r + Temp.Rows.Count))
                    {

                        //   dgv.Rows.Add(r + Temp.Rows.Count - dgv.Rows.Count + 1);
                        DataTable dt = (DataTable)dgv.DataSource;
                        int Max = r + Temp.Rows.Count - dgv.Rows.Count + 1;
                        for (int k = 0; k < Max; k++)
                        {
                            Reglon = dt.NewRow();
                            dt.Rows.Add(Reglon);

                        }
                    }
                    //     dgv.Rows.Add(r + Temp.Rows.Count - dgv.Rows.Count + 1);

                    //MessageBox.Show("cargare: " +Temp.Columns.Count.ToString());

                    if (c <= 1)
                    {
                        try
                        {
                            for (int i = 0; i < Temp.Rows.Count; i++)
                            {
                                //  dataGridView1.Rows.Add();
                                for (int j = 0; j < Temp.Columns.Count; j++)
                                {
                                    if ((j + c - 1) < dgv.Columns.Count - 1)
                                    {
                                        // rehacer_contenedor.Push(new Rehacer_class(i + r, j + c, dgv[j + c, i + r].Value, 1));

                                        dgv[j + c, i + r].Value = Temp.Rows[i][j];
                                    }

                                }
                                //   Tabla_Condensada[0, i + r].Value = false;
                            }
                        }
                        catch (Exception Ex) { MessageBoxEx.Show(Ex.Message); }
                    }
                    else
                    {
                        try
                        {
                            for (int i = 0; i < Temp.Rows.Count; i++)
                            {
                                // dgv.Rows.Add();
                                for (int j = 0; j < Temp.Columns.Count; j++)
                                {
                                    if (j + c - 1 < dgv.Columns.Count - 1)
                                    {
                                        //    rehacer_contenedor.Push(new Rehacer_class(i + r, j + c, dgv[j + c, i + r].Value, 1));
                                        dgv[j + c, i + r].Value = Temp.Rows[i][j];
                                    }
                                }
                                //   Tabla_Condensada[0, i + r].Value = false;
                            }
                        }
                        catch (Exception Ex) { MessageBoxEx.Show(Ex.Message); }
                    }
                    DateTime Fin = DateTime.Now;
                    // dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    //  Notificacion.ShowBalloonTip(500, "Informacion - " + Application.ProductName, "Listo... Se agrego " + Temp.Rows.Count.ToString() + " registros \n  En " + (Fin - Inicio).ToString(), ToolTipIcon.Info);

                }

            }
            catch (Exception error) { MessageBox.Show(error.Message); }
        }


        string File = "";
        void CargaDiccionario()
        {
        }

        int indice = 0;
        bool Remplazavalores(string Palabra, List<string> Clave, string Final, int Cont)
        {



            System.Text.RegularExpressions.Regex r;
            r = new System.Text.RegularExpressions.Regex(Clave[Cont].ToLower());
            // (@"\s*\d*/"); //(@"^[-+]?([0-9]{0,4})([a-l/]{1})\d+(\.\d{2})?$");
            //(@"([E][.][I]([.]*))*(\s[1](\s)*([.])([.]*)(\s*)(\d{2}))");
            MatchCollection mc = r.Matches(Palabra.ToLower());
            if (mc.Count > 0)
            {
                // Palabra = mc[0].Result("$0");
                if (Cont + 1 < Clave.Count)
                    return Remplazavalores(Palabra, Clave, Final, Cont + 1);
                return true;
            }

            //if (Palabra.ToLower().Contains(Clave.ToLower()))
            //    return Final;

            return false;

        }
        Color intColor()
        {
            indice++;
            if (indice >= Colores.Count)
            {
                indice = 0;

            }
            return Colores[indice];

        }
        void CambiaValores()
        {
            //aqui Aplicaremos A todas las Palabras que usaremos convertir
            string CadenaOriginal = "";
            string CadenaReemplazo = "";
            indice = 0; 
            char[] sep = new char[] { ' ', '\t' };
            char[] separapalabras = new char[] { ',' };
            foreach (DataGridViewRow reglon in dgvEventos.Rows)
            {
                // Final.Text = Remplazavalores(Origen.Text, Palabras, ClaveR.Text,0);
                try
                {
                    reglon.Cells[4].Value = " ";
                    reglon.Cells[4].Style.ForeColor = Color.Black;
                }
                catch { }
            }

            try
            {
                Notificacion.ShowBalloonTip(3000, "Información", "Comenzando con el etiquetado", ToolTipIcon.Info);
                foreach (DataRow Lineas in Tabladatos.Rows)
                {
                    if (Lineas[0] != null && Lineas[1] != null)
                    {
                        CadenaOriginal = Lineas[0].ToString();
                        CadenaReemplazo = Lineas[1].ToString();


                       
                        List<string> PorFrases = CadenaOriginal.Split(separapalabras).ToList();

                        List<List<string>> Busqueda = new List<List<string>>();
                        foreach (string frases in PorFrases)
                        {
                            Busqueda.Add(frases.Split(sep).ToList());
                        }


                        ////
                        Color _Color = intColor();
                        Notificacion.ShowBalloonTip(500, "Información", "Comenzando con el etiquetado: " + CadenaReemplazo, ToolTipIcon.Info);
                        if (dgvEventos.Rows.Count > 1)
                            foreach (DataGridViewRow reglon in dgvEventos.Rows)
                            {
                                if (reglon.Cells[3].Value != null)
                                {
                                    // Final.Text = Remplazavalores(Origen.Text, Palabras, ClaveR.Text,0);
                                    try
                                    {
                                        //if (Remplazavalores(reglon.Cells[2].Value.ToString(), Palabras, ClaveR.Text, 0) || Remplazavalores(reglon.Cells[2].Value.ToString(), Palabras2, ClaveR.Text, 0))


                                        bool Existe = false, quizas = false;
                                        string Subcadena = "";
                                        try
                                        {
                                            Subcadena = reglon.Cells[3].Value.ToString();
                                        }
                                        catch
                                        { }
                                        foreach (List<string> Palabritas in Busqueda)
                                        {

                                            Existe = Remplazavalores(Subcadena, Palabritas, CadenaReemplazo, 0);
                                            if (Existe)
                                            {
                                                quizas = true;
                                            }
                                        }


                                        if (quizas)
                                        {
                                            reglon.Cells[4].Value = CadenaReemplazo;
                                            reglon.Cells[4].Style.ForeColor = _Color;
                                        }

                                    }

                                    catch
                                    { }
                                }
                            }//del row

                    }//del if
                }//por linea del diccionario
                Notificacion.ShowBalloonTip(3000, "Información", "se termino de etiquetar", ToolTipIcon.Info);



            }
            catch (Exception falla)
            {
                MessageBox.Show(falla.Message);
            }



        }



       

        private void buttonItem2_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem1_Click_1(object sender, EventArgs e)
        {
            DataGridView dgv;

            try
            {
                dgv = (DataGridView)TabPrincipal.SelectedPanel.Controls[0];

                DataTable t = (DataTable)dgv.DataSource;
                if (dgv.Rows.Count > 0)
                    if (MessageBox.Show("¿Esta realmente seguro de eliminar toda la Tabla?\n\n Esta accion eliminara " + dgv.Rows.Count.ToString() + " Registros!!!", "ALERTA -- " + t.TableName, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                    {

                        t.Rows.Clear();

                    }
                // dgv.Rows.Insert(, 1);
            }
            catch (Exception ex)
            {


            }


        }

        private void buttonItem2_Click_1(object sender, EventArgs e)
        {
            InsertaReglon();
        }

        

        private void buttonItem63_Click(object sender, EventArgs e)
        {
            //Regex_Dic.Form1 Dic = new Regex_Dic.Form1();
            //Dic.StartPosition = FormStartPosition.CenterScreen;
         
        }
        void CreaGenericoDatos()
        {
        }
        void EliminaIguales()
        {
           
}
        void GuardaGenerico()
        {
        }



        private void cmdEventosetiqueta_Click(object sender, EventArgs e)
        {
            Visor_Datos vd = new Visor_Datos();
            File = @"c:\Diccionario.onedic";

            if (System.IO.File.Exists(File))
            {
                DataTable dataset = new DataTable("DiccionarioOnepro");
                dataset.ReadXml(File);
                vd.EstableceDatos(dataset);
                vd.Rapida();
            }

        }

        private void cmdBuscaDelta_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dtConf = ((TabPrincipal.SelectedPanel.Controls[0] as DataGridView).DataSource as DataTable);
                var listy = (from DataColumn col in dtConf.Columns
                             select col.Caption.ToUpper()).ToList();

                fAritmetica fn = new fAritmetica(listy, (TabPrincipal.SelectedPanel.Controls[0] as DataGridView).DataSource as DataTable);
                fn.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Debe de exitir una Tabla");
            }
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            Buscar();
        }
        void Buscar()
        {
            try
            {
                DataGridView dgv_actual;


                dgv_actual = (DataGridView)TabPrincipal.SelectedPanel.Controls[0];
                int col_selec = dgv_actual.CurrentCell.ColumnIndex;
                string Texto = textBox1.Text;
                foreach (DataGridViewRow reglon in dgv_actual.Rows)
                {
                    if (reglon.Cells[col_selec].Value != null)
                    {
                        if (reglon.Cells[col_selec].Value.ToString().ToLower().Contains(Texto.ToLower()))
                        {
                            dgv_actual.FirstDisplayedScrollingRowIndex = reglon.Index;
                            dgv_actual.Rows[reglon.Index].Selected = true;
                            break;
                        }
                    }

                }

            }
            catch { }
        }


        private void cmdEditarTabla_Click(object sender, EventArgs e)
        {
            DataGridView dgv;

            try
            {
                dgv = (DataGridView)TabPrincipal.SelectedPanel.Controls[0];

                DataTable t = (DataTable)dgv.DataSource;

                Editor_de_Columnas fmreditor = new Editor_de_Columnas(t, dgv);
                fmreditor.EditarDel = fmreditor.EditarNew = fmreditor.RowEdit = true;

                fmreditor.Show(this);
                switch (ID)
                {
                    case 0:
                        Core_produccion.Unidad.LeerTablas();
                        break;
                    case 1:
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void cmd_Filtrar_Click(object sender, EventArgs e)
        {
            //aqui mandamos a filtar los valores.
            cmd_Filtrar.Checked = true;
            DataGridView dgv;

            try
            {
                dgv = (DataGridView)TabPrincipal.SelectedPanel.Controls[0];
                DataTable t = (DataTable)dgv.DataSource;
                filtratabla filtro;
                switch (ID)
                {
                    case 0: filtro = new filtratabla(t, Notificacion, Core_produccion);
                        break;
                   
                    default:
                        filtro = new filtratabla(t, Notificacion);
                        break;
                }
                if (filtro != null)
                    filtro.ShowDialog();
                t.AcceptChanges();
                CargaPanelCore();
                //  UsandoAutoFiltro usafiltro = new UsandoAutoFiltro( t);
                //  usafiltro.ShowDialog(this);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

        }



        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBox1.Text.Length > 0)
                Buscar();

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter && textBox1.Text.Length > 0)
                Buscar();
        }

        private void buttonPaste_Click(object sender, EventArgs e)
        {
            DataGridView dgv;

            try
            {
                dgv = (DataGridView)TabPrincipal.SelectedPanel.Controls[0];



                Pegar_Datgridview(dgv);//Actual);
            }
            catch (Exception ex)
            {


            }

        }

        private void cmdDelRow_Click(object sender, EventArgs e)
        {
            RemoverReglon();
        }
        void RemoverReglon()
        {
            DataGridView dgv = new DataGridView();
            try
            {
                dgv = (DataGridView)TabPrincipal.SelectedPanel.Controls[0];

                DataGridViewSelectedRowCollection r = dgv.SelectedRows;
                if (r.Count > 0)
                {
                    if (MessageBox.Show("¿Esta seguro de eliminar los reglones selecionados?", "Alerta !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {

                        Notificacion.ShowBalloonTip(500, "Informacion -" + Application.ProductName, "Eliminando reglones espere!!!", ToolTipIcon.Warning);
                        foreach (DataGridViewRow reglon in r)
                        {
                            try
                            {
                                dgv.Rows.Remove(reglon);
                            }
                            catch { }
                        }
                        Notificacion.ShowBalloonTip(500, "Informacion -" + Application.ProductName, "Reglones eliminados!!!", ToolTipIcon.Info);
                    }
                }

            }
            catch { }
        }

        private void one_form_Produccion_OrigenDatos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //if (MessageBox.Show(" Seguro de guardar los cambios ?", "Alerta!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                //    Core_produccion.Unidad.GuardaCambios();
                //else
                //    Core_produccion.Unidad.NoGuardaCambios();
                switch (ID)
                {
                    case 0:
                        //  Core_produccion.Unidad.OrdenaPorPozo();
                        GuardaGenerico();
                        break;
                    case 1:
                        break;
                }
                //if (hilo != null)
                //{
                //    if (hilo.IsAlive) hilo.Join();
                //    NucleoCalificador.removeTable("Tabla_Inicial");
                //}
                GC.Collect();
            }
            catch { }
        }

        

        private void TabPrincipal_GetTabCloseBounds(object sender, SuperTabGetTabCloseBoundsEventArgs e)
        {

        }

        private void TabPrincipal_TabItemClose(object sender, SuperTabStripTabItemCloseEventArgs e)
        {
            DevComponents.DotNetBar.SuperTabItem mitab = (DevComponents.DotNetBar.SuperTabItem)e.Tab;
            if (Core_produccion != null)
            {
                if (!mitab.CloseButtonVisible)
                {
                    e.Cancel = true;
                    return;
                }
                if (e.Tab.Name == "Tabla_Eventos")
                {
                    e.Cancel = true;
                    return;
                }
            }
            if (MessageBox.Show("Esta seguro de eliminar la Tabla: " + e.Tab.Text + "\nEsta accion NO se Puede DESHACER!!!", "Confimacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.OK)
            {
                switch (ID)
                {
                    case 0:
                        Core_produccion.Unidad.dtTablas_Informacion.Tables.Remove(e.Tab.Text);
                        Core_produccion.Unidad.Diccionario.Remove(e.Tab.Text);
                        Core_produccion.Unidad.LeerTablas();
                        break;
                   
                    case 99:

                        Datos_genericos.Tables.Remove(e.Tab.Text);
                        if (Datos_genericos.Tables.Count == 0)
                            Barra_Menu.Enabled = false;
                        

                        break;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
        public class MyDataRowComparer : IEqualityComparer<DataRow>
        {
            public bool Equals(DataRow x, DataRow y)
            {
                //   for (int i = 0; i < x.Table.Columns.Count; i++)
                for (int i = 0; i < 2; i++)
                {
                    if (x[i].ToString().ToUpper().Trim() != y[i].ToString().ToUpper().Trim())
                    {
                        return false;
                    }

                }
                return true;
            }

            public int GetHashCode(DataRow obj)
            {
                return obj.ToString().GetHashCode();
            }
        }
        private void cmdLoad_Click(object sender, EventArgs e)
        {
            //aqui cargaremos una tabla Nueva.
            //aqui mandaremos a  editar
            DataGridView dgv;

            try
            {
                dgv = (DataGridView)TabPrincipal.SelectedPanel.Controls[0];

                DataTable t = (DataTable)dgv.DataSource;
                DataTable Copia = t.Clone();
                DataTable temp = t.Clone();
                MyDataRowComparer mycom = new MyDataRowComparer();
                if (t != null)
                {
                    Visor_Datos fmreditor = new Visor_Datos();//
                    fmreditor.EstableceDatos(Copia);
                    fmreditor.Rapida(true);

                    Notificacion.ShowBalloonTip(1000, "Espere mientras que se validan los datos", "Informacion", ToolTipIcon.Info);

                    switch (ID)
                    {
                        case 0:
                            // t.BeginLoadData();
                            //cuando tenga la copia
                            Point punto = new Point(1, 2); // 1.- pozo 2.- fecha
                            bool actualizar = false;
                            if (t.TableName != "Tabla_Eventos")
                            {
                                punto = Core_produccion.Unidad.Diccionario[t.TableName];
                                //   t.PrimaryKey = new DataColumn[] { t.Columns[punto.X], t.Columns[punto.Y], t.Columns[2] };
                                actualizar = true;
                            }
                            //
                            var agrupados = from grupo in Copia.AsEnumerable() group grupo by grupo.Field<string>(punto.X);

                            foreach (var ungrupo in agrupados)
                            {
                                //como ya tenemos un pozo ahora intentaremos 
                                string Pozo_Buscar = ungrupo.Key.ToUpper();
                                //mandemos a buscarlo aver si esta ahi.
                                IEnumerable<DataRow> exiteposo = from pozo in t.AsEnumerable() where (pozo[punto.X].ToString().ToUpper() == Pozo_Buscar) select pozo;
                                if (exiteposo.Count() > 0)
                                {
                                    // MessageBox.Show("si existe"+ Pozo_Buscar);
                                    //como existe ahora trataremos de hacer los cruces
                                    IEnumerable<DataRow> inter = ungrupo.Except(exiteposo, mycom);
                                    if (inter.Count() > 0)
                                    {
                                        temp.Merge(inter.CopyToDataTable());
                                    }
                                }
                                else
                                {

                                    temp.Merge(ungrupo.CopyToDataTable());
                                }
                            }

                            //fmreditor.EstableceDatos(temp);
                            //fmreditor.Rapida(true);


                            //  //intercepcion
                            //IEnumerable<DataRow> list = Copia.AsEnumerable().Except( t.AsEnumerable(),mycom );
                            //try
                            //{
                            //    temp = list.CopyToDataTable();
                            //}
                            //catch { }



                            //despues de ver que los diferentes los agregamos al final

                            Notificacion.ShowBalloonTip(1000, "Se agregaron nuevos registros: " + temp.Rows.Count.ToString(), "Informacion", ToolTipIcon.Info);

                            t.Merge(temp);



                            break;
                        case 1:

                            break;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }


        private void cmdVerOcultar_Click(object sender, EventArgs e)
        {
            DataGridView dgv;

            try
            {
                dgv = (DataGridView)TabPrincipal.SelectedPanel.Controls[0];
                DataTable t = (DataTable)dgv.DataSource;
                //vamos a crear una lista
                List<ObjetoSelecionable> Lista1 = new List<ObjetoSelecionable>();
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    Lista1.Add(new ObjetoSelecionable(col, col.Visible));
                }
                userFiltroElemento userfiltro = new userFiltroElemento(ref Lista1);
                userfiltro.Lista_eventos.Sorted = false;
                userfiltro.EnForm(true);
                bool estaVisible = true;
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (!col.Visible)
                    {
                        estaVisible = false;
                        break;
                    }
                }

                cmdVerOcultar.Checked = !estaVisible;

                //foreach (ObjetoSelecionable l in Lista1)
                //{
                //    dgv.Columns[l.ToString()].Visible = l.Estado;
                //}
                //  UsandoAutoFiltro usafiltro = new UsandoAutoFiltro( t);
                //  usafiltro.ShowDialog(this);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }
        private void TabPrincipal_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            switch (ID)
            {
                case 0:
                    if (TabPrincipal.SelectedTabIndex == 0)
                    {
                        grupoedicion.Visible = false;
                        grupoProduccion.Visible = false;
                    }
                    else
                    {
                        grupoedicion.Visible = true;
                        grupoProduccion.Visible = true;
                    }
                    Barra_Menu.Refresh();
                    break;
            }
            DataGridView dgv;

            try
            {
                dgv = (DataGridView)TabPrincipal.SelectedPanel.Controls[0];

                //caundo se esta cambiando de pantalla.
                bool estaVisible = true;
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (!col.Visible)
                    {
                        estaVisible = false;
                        break;
                    }
                }

                cmdVerOcultar.Checked = !estaVisible;
            }
            catch { }

        }


        private DataTable CreaRangoFormaciones()
        {

            DataTable dtxFechasxFormacion = dtFormaciones.Clone();
            if (!dtxFechasxFormacion.Columns.Contains("Intervalo")) 
            dtxFechasxFormacion.Columns.Add("Intervalo");

            Visor_Datos vd = new Visor_Datos();
            //hagamos grupos
            var engrupos = from tipoPozo in dtFormaciones.AsEnumerable() where (tipoPozo[0] != null) group tipoPozo by (tipoPozo[0]);
            foreach (var ungrupo in engrupos)
            {
                //veamos que hay.
                //ahora por fromacion.
                var porformacion = from tipoPozo in ungrupo group tipoPozo by (tipoPozo["Formacion"]);
                foreach (var unformacion in porformacion)
                {
                    //en esta tabla buscaremos scar una nueva
                    //saquemos la fecha minima y la maxima
                    DataTable dtfechas = unformacion.CopyToDataTable();
                    if (!dtfechas.Columns.Contains("Intervalo")) 
                    dtfechas.Columns.Add("Intervalo");
                    string Inter = "";

                    foreach (DataRow re in dtfechas.Rows)
                    {
                        re["Intervalo"] = re["Formacion"].ToString() + ": " + re["Cima"].ToString() + "-" + re["Base"].ToString() + ", ";
                        Inter += re["Formacion"].ToString() + ": " + re["Cima"].ToString() + "-" + re["Base"].ToString() + ", ";

                    }
                    //vd.EstableceDatos(dtfechas);
                    //vd.Rapida(true);
                    //DateTime[] fechasInicio = MaxMin(dtfechas,4);
                    //DateTime[] fechasFin = MaxMin(dtfechas, 5);

                    //    DataRow drReg = dtFormacionPozo.NewRow();
                    //    drReg["Pozo"] = dtfechas.Rows[0]["Pozo"];
                    //    drReg["Formacion"] = dtfechas.Rows[0]["Formacion"];
                    //    drReg["Inicio"] = fechasInicio[0];
                    //    drReg["Fin"] = fechasFin[1];
                    //    drReg["Intervalo"] = Inter;
                    //    dtFormacionPozo.Rows.Add( drReg);
                    dtxFechasxFormacion.Merge(dtfechas);
                    NormalizaFecha(dtxFechasxFormacion, 4);
                    NormalizaFecha(dtxFechasxFormacion, 5);
                    dtfechas.Dispose();

                }
            }

            //  var agrupados = from grupo in CampoInicial group grupo by grupo.Field<string>(x);

            return dtxFechasxFormacion;
        }
        void NormalizaFecha(DataTable t, int col)
        {
            DateTime temp = DateTime.Now;
            foreach (DataRow reg in t.Rows)
            {
                if (!DateTime.TryParse(reg[col].ToString(), out temp))
                {
                    reg[col] = DateTime.Now;
                }

            }
        }
        DateTime[] MaxMin(DataTable t, int col)
        {
            //fecha Maximo minimo
            DateTime[] fechas = new DateTime[2];
            DateTime temp = DateTime.Now;
            foreach (DataRow reg in t.Rows)
            {
                if (!DateTime.TryParse(reg[col].ToString(), out temp))
                {
                    reg[col] = DateTime.Now;
                }

            }

            var max = (from valor in t.AsEnumerable().Where(p => p.Field<string>(col) != null)
                       select Convert.ToDateTime(valor[col].ToString())).Max();

            var min = (from valor in t.AsEnumerable().Where(p => p.Field<string>(col) != null)
                       select Convert.ToDateTime(valor[col].ToString())).Min();
            fechas[0] = min;
            fechas[1] = max;

            return fechas;
        }

       

        void CargaFormaciones()
        {

        }




       



        private void buttonItem47_Click(object sender, EventArgs e)
        {
            DataGridView dgv;

            try
            {
                dgv = (DataGridView)TabPrincipal.SelectedPanel.Controls[0];
                DataTable t = (DataTable)dgv.DataSource;
                if (t != null)
                {
                    BusquedadeDatos bd = new BusquedadeDatos(t);
                    bd.ShowDialog();

                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }

        private void buttonItem7_Click(object sender, EventArgs e)
        {
            DataGridView dgv;

            try
            {
                dgv = (DataGridView)TabPrincipal.SelectedPanel.Controls[0];
                DataTable t = (DataTable)dgv.DataSource;
                if (t != null)
                {

                    SaveFileDialog salvar = new SaveFileDialog();
                    salvar.DefaultExt = ".xls";
                    salvar.Filter = "Hoja de Excel (*.xls) | *.xls";
                    if (salvar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        ExportaAExcelDLL.GuardaTablaenExcel Nuevo = new ExportaAExcelDLL.GuardaTablaenExcel();
                        Nuevo.DataTableToExcelSave(t, salvar.FileName, "Tabla de Clasificacion");
                    }

                    MessageBox.Show("Exportado a Excel completado!!! ", "Confimación");
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }
        public List<string> RetornaPozos()
        {
            List<string> combopozo = new List<string>();
            try
            {

                //aqui rellenaremos pero 


                DataTable dtTabla_Medicion = dfListaNombresPozos;
                dtTabla_Medicion.AcceptChanges();
                if (dtTabla_Medicion.Namespace != "PlantillasTabla")
                {

                    int x = 1;
                    IEnumerable<string> CampoInicial =
                     (from pozo in dtTabla_Medicion.AsEnumerable().Where(p => !string.IsNullOrWhiteSpace(p.Field<string>(x))) select pozo[x].ToString().ToUpper()).Distinct();
                    //Visor_Datos vd = new Visor_Datos();
                    //vd.EstableceDatos(dtTabla_Medicion);
                    //vd.Rapida(false);


                    foreach (string p in CampoInicial)
                    {
                        if (!combopozo.Contains(p))
                            combopozo.Add(p);

                    }
                }



            }
            catch { }
            return combopozo;
        }
        


        private void cmdEtiquetaFormacions_Click(object sender, EventArgs e)
        {
        }
        private void cmdEtiquetaEvento_Click(object sender, EventArgs e)
        {
          
        }

        private void cmdPosicionesXY_Click(object sender, EventArgs e)
        {
           
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            DataGridView dgv;

            try
            {
                dgv = (DataGridView)TabPrincipal.SelectedPanel.Controls[0];
                DataTable t = (DataTable)dgv.DataSource;
               //ahora invoquemos la tabla de comparar. 
                if (Lista_tablas.Count > 0)
                {
                    Comparativa_de_tabla compa = new Comparativa_de_tabla(Notificacion, t, Lista_tablas);
                    compa.ShowDialog();
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }

        private void buttonItem2_Click_2(object sender, EventArgs e)
        {
             try
            {//aqui haremos lo de heterogenizar pero solo para las marcadas

                //hagamos la sub lista

                   DataGridView dgv;

          
                dgv = (DataGridView)TabPrincipal.SelectedPanel.Controls[0];
                DataTable t = (DataTable)dgv.DataSource;
                if (t.Rows.Count > 0)
                {
                    List<Core_Clasificador.ObjetoTablas> ListaParaHisto = new List<Core_Clasificador.ObjetoTablas>();

                    Core_Clasificador.ObjetoTablas objetonuevo = new Core_Clasificador.ObjetoTablas();
                    objetonuevo.Tabla_unidad = t.Copy();

                    ListaParaHisto.Add(objetonuevo);


                    if (ListaParaHisto.Count > 0)
                    {

                        frmGerarquizar fmrgera = new frmGerarquizar(ListaParaHisto,Notificacion);


                        fmrgera.Show();
                    }
                }
               


                else { MessageBox.Show("Sin tabla para heterogenizar o sin reglones"); }
            }
            catch (Exception Error) { MessageBox.Show(Error.Message); }
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            //aqui Buscaremos Remplazar el datagridview las palabras que necesito.

            if (ID == 0)
            {
                // CargaDiccionario(false);
                if ((DataGridView)TabPrincipal.SelectedPanel.Controls[0] == dgvEventos)
                    CambiaValores();
                else
                    MessageBox.Show("Esta tabla no se puede clasificar en Eventos");
            }
            else
            {
                MessageBox.Show("Esta tabla no se puede clasificar en Eventos");
            }
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            // File = @"c:\Formaciones.onefor";
            //  CargaFormaciones();
            //Visor_Datos vd = new Visor_Datos();
            //vd.EstableceDatos(dtFormaciones);
            //vd.Rapida(true);
            ////despues de eso hay que tratar de arrglaer losnombres.
            // userReemplazar2 reem = new userReemplazar2(dtFormaciones, 0);
            //pediremos los pozos .
            // AutoCompleteStringCollection x = new AutoCompleteStringCollection();
            // x.AddRange(Core_produccion.Unidad.RetornaPozos().ToArray());
            // reem.AplicaOrigen(x);

            // reem.EnForm();
            //cremos una tabla por pozo/formacion  -inciio fin



            DataGridView dgv;
            dgv = (DataGridView)TabPrincipal.SelectedPanel.Controls[0];
            DataTable t = (DataTable)dgv.DataSource;

            if (t == null) return;
            if (t.Rows.Count == 0) return;
            try
            {
                Notificacion.ShowBalloonTip(500, "Informacion - " + Application.ProductName, "Comenzando a clasificar la formacion", ToolTipIcon.Info);
                //para no hacer de mas preguntemos si hay datos o no
                DataTable dtrangofromacion = CreaRangoFormaciones();
                // vd.EstableceDatos(dtrangofromacion);
                // vd.Rapida(true);

                //cmo ya tenemos estos valores ahora intentaremos sacar el dato.

                //vamos a crear una lista
                if (t != null)
                {
                    //creamos Una Columna para poner la formacion y la fecha
                    if (!t.Columns.Contains("Formacion"))
                        t.Columns.Add("Formacion");
                    if (!t.Columns.Contains("Intervalo"))
                        t.Columns.Add("Intervalo");
                    foreach (DataRow x1 in t.Rows)
                    {
                        x1["Formacion"] = "";
                        x1["Intervalo"] = "";
                    }
                    //depues de buscar la formacion 
                    Point punto = new Point(-1, -1);
                    switch (ID)
                    {
                        case 0: punto = Core_produccion.Unidad.PosicionDiccionario(t.TableName);
                            break;
                        case 1:
                            punto = new Point(0, 1);
                            break;


                    }
                    if (punto.X != -1)
                    {
                        foreach (DataRow x1 in t.Rows)
                        {
                            if (x1[punto.X] != null)
                                x1[punto.X] = origenCore.EliminaAcentos(x1[punto.X].ToString());
                        }
                        DataTable pozosactuales = origenCore.DistintosDeUnaColumna(punto.X, t);
                        //vd.EstableceDatos(pozosactuales);
                        //vd.Rapida(true);
                        foreach (DataRow row in pozosactuales.Rows)
                        {
                            //obtenemos el nombre 
                            string _pozo = row[punto.X].ToString().ToUpper();
                            //de este pozo saquemos de ambos tablas aver en cual existe
                            IEnumerable<DataRow> reglonesTabla = from reg in t.AsEnumerable()
                                                                 where (reg[punto.X].ToString().ToUpper() == _pozo)
                                                                 select reg;
                            IEnumerable<DataRow> reglonesFormacion = from reg in dtrangofromacion.AsEnumerable()
                                                                     where (reg["Pozo"].ToString().ToUpper() == _pozo)
                                                                     select reg;

                            foreach (DataRow regform in reglonesFormacion)
                            {
                                //sauqemos las fechas
                                DateTime Inicio = Convert.ToDateTime(regform[4].ToString());
                                DateTime fin = Convert.ToDateTime(regform[5].ToString());
                                //saquemos todas la fechas de ese rango.

                                var reglonesafectados = from dentro in reglonesTabla
                                                        where (Convert.ToDateTime(dentro[punto.Y].ToString()) >= Inicio && Convert.ToDateTime(dentro[punto.Y].ToString()) <= fin)
                                                        select dentro;
                                //ahora veremos que es lo que fue afectado
                                foreach (DataRow rowfinales in reglonesafectados)
                                {
                                    string S1 = rowfinales["Formacion"].ToString();
                                    string S2 = regform["Formacion"].ToString();
                                    if (!S1.Contains(S2))
                                    {
                                        rowfinales["Formacion"] += regform["Formacion"].ToString() + ", ";

                                    }
                                    rowfinales["Intervalo"] += regform["Intervalo"].ToString();
                                }


                            }
                        }

                    }

                }
                Notificacion.ShowBalloonTip(500, "Informacion - " + Application.ProductName, "Se termino de clasificar", ToolTipIcon.Info);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void buttonItem8_Click(object sender, EventArgs e)
        {
          
        }

        

        

        private void cmdSap_Click(object sender, EventArgs e)
        {

        }

        

        public void EnForm(UserControl user_)
        {


            Form fm = new Form();
            
            fm.MaximizeBox = true;
            fm.MinimizeBox = true;
            fm.Controls.Add(user_);
            fm.StartPosition = FormStartPosition.CenterParent;

            //  CreaBoton(fm);

         
            fm.ShowInTaskbar = false;
            fm.ShowIcon = false;
            user_.Dock = DockStyle.Fill;

            fm.WindowState = FormWindowState.Maximized;
            fm.ShowDialog();

        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
          
        }
        

        private void cmdOrdenar_Click(object sender, EventArgs e)
        {
            if (Core_produccion != null)
            {
                DataGridView dgv;

                try
                {
                    dgv = (DataGridView)TabPrincipal.SelectedPanel.Controls[0];

                    DataTable t = (DataTable)dgv.DataSource;
                    if (dgv.Rows.Count > 0)
                        Core_produccion.Unidad.OrdenaPorPozo(t);
                }
                catch
                {

                }
            }
        }


        public DataSet Datos_genericos { get; set; }

        private void buttonItem5_Click(object sender, EventArgs e)
        {

        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
            
            DataTable dtConf = ((TabPrincipal.SelectedPanel.Controls[0] as DataGridView).DataSource as DataTable);
            CalificaTabla caTabla = new CalificaTabla(dtConf, lTabla, dgvList, NucleoCalificador);
            caTabla.WindowState = FormWindowState.Maximized;
            caTabla.ShowDialog(this);
            if (caTabla.dtNuevo != null && caTabla.dtNuevo.Rows.Count > 0)
                ((TabPrincipal.SelectedPanel.Controls[0] as DataGridView)).DataSource = caTabla.dtNuevo;
        }

        private void p1()
        {
            DataSet set = new DataSet();
            set.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "LocalizacionPozo.OneClass");
            dgvList.Add(set.Tables[0]);
            lTabla.Add(set.Tables[0].TableName);
            NucleoCalificador.addTable(set.Tables[0].Copy());
        }
        private void p2()
        {
            DataSet set = new DataSet();
            set.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "LocalizacionMexico.OneED");
            dgvList.Add(set.Tables[0]);
            lTabla.Add(set.Tables[0].TableName);
            NucleoCalificador.addTable(set.Tables[0].Copy());
        }

        public List<string> lTabla { get; set; }
        public List<DataTable> dgvList { get; set; }
        public CoreCalifica NucleoCalificador { get; set; }

        private void btnRadio_Click(object sender, EventArgs e)
        {
            DataTable dtConf = ((TabPrincipal.SelectedPanel.Controls[0] as DataGridView).DataSource as DataTable);
            Maps.Aritmetica.ucRadio rDIO = new Aritmetica.ucRadio(dtConf);
            rDIO.Dock = DockStyle.Fill;
            contenedor cm = new contenedor();
            cm.Controls.Add(rDIO);
            cm.Size = new System.Drawing.Size(295, 150);
            cm.ShowDialog();
        }

        private void buttonItem3_Click_1(object sender, EventArgs e)
        {
            DataTable dtConf = ((TabPrincipal.SelectedPanel.Controls[0] as DataGridView).DataSource as DataTable);

            ucConvertidor rDIO = new ucConvertidor(dtConf);
            rDIO.Dock = DockStyle.Fill;
            contenedor cm = new contenedor();
            cm.Controls.Add(rDIO);
            cm.Size = new System.Drawing.Size(1000, 650);
            cm.ShowDialog();
        }
    
    }//clase
}//objeto
