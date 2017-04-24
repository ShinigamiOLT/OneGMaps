using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using Gigasoft.ProEssentials.Enums;
using System.Drawing;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using One_Produccion;

namespace Maps
{
    class EventosDGvED
    {
        DataGridView dgv;
        NotifyIcon Notificacion;
        bool IsDatasource = false;


        DataTable dt;
        public EventosDGvED(DataGridView dgv, NotifyIcon noti)
        {
            this.dgv = dgv;
            this.Notificacion = noti;
            Funciones();
            dt = ((DataTable)dgv.DataSource).Copy();
        }


        public bool IsOrigen
        {
            set { IsDatasource = value; }
            get { return IsDatasource; }

        }

        public void dgv_prescom_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        public void Funciones()
        {
            dgv.KeyPress += new KeyPressEventHandler(dgv_prescom_KeyPress);
            dgv.KeyDown += new KeyEventHandler(dgv_KeyDown);
        }


        string s ="";
        string[] lines;
        int iRow = 0;
        int iCol = 0;
        int total = 0;
        int count;
        void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.V) || (e.Shift && e.KeyCode == Keys.Insert))
            {
                try
                {
                    Notificacion.Visible = true;
                    Notificacion.BalloonTipIcon = ToolTipIcon.Info;
                    Notificacion.BalloonTipText = "Se estan copiando multiples datos";
                    //Notificacion.Icon = global::Mapas.Properties.Resources.hola;
                    Notificacion.BalloonTipTitle = "Espere";
                    Notificacion.ShowBalloonTip(29000000);

                    s = Clipboard.GetText();
                    lines = s.Split('\n');
                     iRow = dgv.CurrentCell.RowIndex;
                    iCol = dgv.CurrentCell.ColumnIndex;
                    count = lines.Count();

                    copiaDGV();

                    Notificacion.Visible = false;
                    
                }
                catch (FormatException)
                {
                    MessageBox.Show("The data you pasted is in the wrong format for the cell");
                    return;
                }

            }
            

            
        }
        
        void insertRows()
        {
            double renglones = count;
            double dgvRenglones = dgv.Rows.Count;
            double diferencia = dgvRenglones - renglones;

            if (diferencia < 0)
            {
                Notificacion.ShowBalloonTip(1);
                for (int i = 0; i < Math.Abs(diferencia); i++)
                    dt.Rows.Add();

            }
            diferencia = dgv.Rows.Count;
            
        }

        string[] sCells;
        int t;
        void copiaDGV()
        {
            Notificacion.ShowBalloonTip(1);
            int tam  = lines[0].Split('\t', '\r').Length;
            //for (int k = 0; k < tam; k++) dt.Columns.Add();
            foreach (string line in lines)
            {
                dt.Rows.Add();
                int coli = dt.Rows.Count;
                if (iRow < dt.Rows.Count && line.Length > 0)
                {
                    sCells = line.Split('\t', '\r');
                    total = sCells.Length;
                    t = sCells.GetLength(0) - 1;

                    

                        for (int i = 0; i < t; ++i)
                        {
                            if (iCol + i < this.dgv.ColumnCount)
                            {
                                if (sCells[i] != "")
                                    dt.Rows[iRow][iCol + i] = sCells[i].ToString();
                            }
                            else break;
                        }
                    iRow++;
                }
                else
                { break; }
               
            }
            if (iCol + total > dt.Columns.Count) total = dt.Columns.Count;
            else total = (iCol + total)-1;

            if (iRow > 2)
                dgv.DataSource = dt;

            dgv.Tag += iCol.ToString() + " " + total.ToString();
        }

        public void Paste()
        {
            if (IsOrigen)
            {
                Pegar_Datgridview_dt(dgv);
            }
            else
                Pegar_Datgridview(dgv);//Actual);
        }
        void InsertaReglon()
        {
            try
            {
                if (IsDatasource)
                {

                    try
                    {


                        DataTable t = (DataTable)dgv.DataSource;
                        DataRow reglon = t.NewRow();
                        t.Rows.InsertAt(reglon, dgv.CurrentCell.RowIndex);
                        // dgv.Rows.Insert(, 1);
                    }
                    catch
                    {


                    }
                }
                else
                {

                    dgv.Rows.Insert(dgv.CurrentCell.RowIndex);

                }
            }
            catch { }
        }
        void DesactivaOrdenamiento(DataGridView dgv)
        {

            for (int I = 0; I < dgv.Columns.Count; I++)
            {
                dgv.Columns[I].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        void Pegar_Datgridview(DataGridView dgv)
        {
            try
            {

                DataTable Temp = new DataTable();
                char[] rowSplitter = new char[] { '\n', '\r' };
                char[] columnSplitter = new char[] { '\t' };
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
                    Notificacion.ShowBalloonTip(2000, "Informacion - " + Application.ProductName, "Espere... leyendo valores del portapapeles. \n Puede tardar varios minutos", ToolTipIcon.Warning);
                    DateTime Inicio = DateTime.Now;

                    if (dgv.Rows.Count < (r + Temp.Rows.Count))
                        dgv.Rows.Add(r + Temp.Rows.Count - dgv.Rows.Count + 1);

                    //MessageBox.Show("cargare: " +Temp.Columns.Count.ToString());

                    // if (c <= 1)
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
                        catch (Exception Ex) { MessageBox.Show(Ex.Message); }
                    }
                    /* else
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
                         catch (Exception Ex) { MessageBox.Show(Ex.Message); }
                     }
                     * */
                    DateTime Fin = DateTime.Now;
                    // dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    Notificacion.ShowBalloonTip(500, "Informacion - " + Application.ProductName, "Listo... Se agrego " + Temp.Rows.Count.ToString() + " registros \n  En " + (Fin - Inicio).ToString(), ToolTipIcon.Info);
                }

            }
            catch { }
        }

        void Pegar_Datgridview_dt(DataGridView dgv)
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
                 DataTable dt_1 = (DataTable)dgv.DataSource;
                 int Columnbas_Acopiar = filas_en_clipboard[0].Split(columnSplitter).Length;

                 int aux = dt_1.Columns.Count;
                 for (int Col = dt_1.Columns.Count - c; Col <Columnbas_Acopiar ; Col++)
                 {
                     dt_1.Columns.Add("Col" + aux++);
                 }

                 dt_1.Columns.Add("Col" + aux++);

                for (int iCol = 0; iCol < filas_en_clipboard[0].Split(columnSplitter).Length && iCol < dgv.Columns.Count - c; iCol++)
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
                    for (int iCol = 0; iCol < valuesInRow.Length && iCol < dgv.Columns.Count + c; iCol++)
                    {
                        //     // 'assign cell value, only if it within columns of the grid

                        Reglon[iCol] = valuesInRow[iCol];
                    }
                    Temp.Rows.Add(Reglon);

                }

                dgv.SuspendLayout();
                if (Temp.Rows.Count > 0)
                {
                    Notificacion.ShowBalloonTip(2000, "Informacion - " + Application.ProductName, "Espere... leyendo valores del portapapeles. \n Puede tardar varios minutos", ToolTipIcon.Warning);
                    DateTime Inicio = DateTime.Now;
                    DataTable dt = (DataTable)dgv.DataSource;
                    dgv.DataSource = null;
                    if (dt.Rows.Count < (r + Temp.Rows.Count))
                    {

                        //   dgv.Rows.Add(r + Temp.Rows.Count - dgv.Rows.Count + 1);

                        int Max = r + Temp.Rows.Count - dt.Rows.Count;
                        for (int k = 0; k < Max; k++)
                        {
                            Reglon = dt.NewRow();
                            dt.Rows.Add(Reglon);

                        }
                    }
                    //     dgv.Rows.Add(r + Temp.Rows.Count - dgv.Rows.Count + 1);

                    //MessageBox.Show("cargare: " +Temp.Columns.Count.ToString());

                    //  if (c <= 1)
                    {
                        try
                        {

                            for (int i = 0; i < Temp.Rows.Count; i++)
                            {
                                //  dataGridView1.Rows.Add();
                                for (int j = 0; j < Temp.Columns.Count; j++)
                                {
                                    if ((j + c - 1) < dt.Columns.Count - 1)
                                    {
                                        dt.Rows[i + r][j + c] = Temp.Rows[i][j];

                                    }

                                }

                            }
                        }
                        catch (Exception Ex) { MessageBox.Show(Ex.Message); }
                    }
                    /* else
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
                         catch (Exception Ex) { MessageBox.Show(Ex.Message); }
                     }
                        */
                    dgv.DataSource = dt;
                    DateTime Fin = DateTime.Now;
                    // dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    Notificacion.ShowBalloonTip(500, "Informacion - " + Application.ProductName, "Listo... Se agrego " + Temp.Rows.Count.ToString() + " registros \n  En " + (Fin - Inicio).ToString(), ToolTipIcon.Info);

                }
                dgv.ResumeLayout(false);

            }
            catch (Exception error) { MessageBox.Show(error.Message); }
        }
    }

    class EventosDGv
    {
        DataGridView dgv;
        NotifyIcon Notificacion;
        bool IsDatasource = false;
        List<int> IndicesNomanejables;
        public int NDecimales=0;



        public EventosDGv(DataGridView dgv, NotifyIcon noti)
        {
            this.dgv = dgv;
            this.Notificacion = noti;
            dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            IndicesNomanejables = new List<int>();
            Funciones();
            IsOrden = true;
        }

        public EventosDGv(DataGridView dgv, NotifyIcon noti,bool orden)
        {
            this.dgv = dgv;
            this.Notificacion = noti;
            dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            IndicesNomanejables = new List<int>();
            Funciones();
            IsOrden = orden;
        }
        public bool IsOrigen
        {
            set { IsDatasource = value; }
            get { return IsDatasource; }

        }
        public bool IsOrden
        {
            get;
            set;
        }

        public void dgv_prescom_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        public void Funciones()
        {
            dgv.KeyPress += new KeyPressEventHandler(dgv_prescom_KeyPress);
            dgv.KeyDown += new KeyEventHandler(dgv_KeyDown);
            dgv.ColumnHeaderMouseClick -= null;
            dgv.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(dgvorden_ColumnHeaderMouseClick);
            dgv.CurrentCellDirtyStateChanged += new EventHandler(dgv_CurrentCellDirtyStateChanged);
            dgv.CellErrorTextChanged += new DataGridViewCellEventHandler(dgv_CellErrorTextChanged);
            dgv.DataError += new DataGridViewDataErrorEventHandler(dgv_DataError);
            dgv.RowErrorTextChanged += new DataGridViewRowEventHandler(dgv_RowErrorTextChanged);
            dgv.DataSourceChanged += new EventHandler(dgv_DataSourceChanged);
                foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Programmatic;// = DataGridViewAutoSizeColumnMode.Fill;
                if (col.HeaderText == "Visible" || col.Name == "Visible" ||  col.ValueType == typeof(bool))
                {
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                    
                }
                if (col.ValueType == typeof(DateTime))
                { 
                col.DefaultCellStyle.Format = "d";
                col.DefaultCellStyle.BackColor = Color.LightYellow;
                
                }
                if (col.ValueType == typeof(double))
                {
                    col.DefaultCellStyle.Format = "F3";
                    col.DefaultCellStyle.BackColor = Color.LightGray;

                }
            }  
           
        }
        public void NumeroDecimales_(bool Sentido)
        { if (Sentido)
                        NDecimales++;
                    else
                        NDecimales--;
                    if (NDecimales < 0)
                        NDecimales = 0;
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Programmatic;// = DataGridViewAutoSizeColumnMode.Fill;
                if (col.HeaderText == "Visible" || col.Name == "Visible" || col.ValueType == typeof(bool))
                {
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

                }
                if (col.ValueType == typeof(DateTime))
                {
                    col.DefaultCellStyle.Format = "d";
                    col.DefaultCellStyle.BackColor = Color.LightYellow;

                }
                if (col.ValueType == typeof(double))
                {
                   
                    col.DefaultCellStyle.Format = "F"+ NDecimales.ToString();
                    col.DefaultCellStyle.BackColor = Color.LightGray;

                }
            }
        }
           

   
      public  void CambiaTipo1(string NombreCol, DataTable  objetoDataTable,string ValorMiembro)
        {// Referenciamos la segunda columna del control
            // DataGridView
            //
            if (dgv.Columns.Contains(NombreCol) && objetoDataTable!=null   )
            {
                DataGridViewColumn col = dgv.Columns[NombreCol];
                int index = col.Index;
                // Creamos una columna tipo DataGridViewComboBoxColumn
                //
                DataGridViewComboBoxColumn comboColumn = new DataGridViewComboBoxColumn();

                // Configuramos la columna tipo ComboBox donde especificaremos
                // el objeto DataTable que utilizaremos para rellenar los datos
                //
                comboColumn.HeaderText = col.HeaderText;
                comboColumn.DropDownWidth = 160;
                comboColumn.Width = col.Width;
                comboColumn.DataSource = objetoDataTable;
                comboColumn.FlatStyle = FlatStyle.Flat;
                comboColumn.DisplayMember = ValorMiembro;
                comboColumn.ValueMember = ValorMiembro;

                // Eliminamos la columna tipo DataGridViewTextBoxColumn
                //
                dgv.Columns.Remove(col);

                // Insertamos la nueva columna en la segunda posicion
                //
                dgv.Columns.Insert(index, comboColumn);
            }

        }
       

        void dgv_RowErrorTextChanged(object sender, DataGridViewRowEventArgs e)
        {
            throw new NotImplementedException();
        } 

        void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DBNull.Value;
        }

        

        void dgv_CellErrorTextChanged(object sender, DataGridViewCellEventArgs e)
        {
          //  throw new NotImplementedException();
        }

        void dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                int colIndex = dgv.CurrentCell.ColumnIndex;
                if (IndicesNomanejables.Contains(colIndex))
                    if (dgv.IsCurrentCellDirty)
                    {
                        dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        dgv.EndEdit();
                    }
            }
            catch { }
        }
        private void dgvPlantillas_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
          
        }

        public void Especial()
        {
            try
            {
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.CellType.Name.Contains("CheckBox"))
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                       // col.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                        col.Width = 30;
                        show_chkBox(col.Index);
                        IndicesNomanejables.Add(col.Index);
                    }

                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        private void show_chkBox(int Columna)
        {
            Rectangle rect = dgv.GetCellDisplayRectangle(Columna, -1, true);
            // set checkbox header to center of header cell. +1 pixel to position 
            rect.Y = 3;
            rect.X = rect.Location.X + (rect.Width / 8);
            CheckBox checkboxHeader = new CheckBox();
            checkboxHeader.Tag = Columna;
            checkboxHeader.Name = "checkboxHeader"+Columna.ToString();
            //datagridview[0, 0].ToolTipText = "sdfsdf";
            checkboxHeader.Size = new Size(18, 18);
            checkboxHeader.Text = dgv.Columns[Columna].Name;
            checkboxHeader.Location = rect.Location;
            checkboxHeader.AutoSize = true;
            checkboxHeader.BackColor=  System.Drawing.Color.Transparent;
            checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);
            dgv.Controls.Add(checkboxHeader);
            dgv.Columns[Columna].HeaderText = "";
            dgv.Columns[Columna].Width = checkboxHeader.Width + 18;
            dgv.Columns[Columna].MinimumWidth = checkboxHeader.Width + 18;
            dgv.Columns[Columna].Tag = checkboxHeader;
        }


        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox headerBox = (CheckBox)sender; // ((CheckBox)dgv.Controls.Find("checkboxHeader", true)[0]);
            //  MessageBox.Show(headerBox.Name);
            int index = Convert.ToInt32(headerBox.Tag);

            if (IsOrigen)
            {

                DataTable t = (DataTable)dgv.DataSource;
                foreach (DataRow reglon in t.Rows)
                {
                    reglon[index] = headerBox.Checked;
                }

            }
            else
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    dgv.Rows[i].Cells[index].Selected = headerBox.Checked;
                    dgv.Rows[i].Cells[index].Value = headerBox.Checked;
                    if (dgv.IsCurrentCellDirty)
                        dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    dgv.EndEdit();
                    dgv.Rows[i].Cells[index].Selected = headerBox.Checked;
                    dgv.EndEdit();
                }
            }
            if (dgv.IsCurrentCellDirty)
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            dgv.EndEdit();
            dgv.Refresh();
        }
        void dgv_DataSourceChanged(object sender, EventArgs e)
        {
          //  dgv.ColumnHeaderMouseClick -= null;
          //dgv.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(dgvorden_ColumnHeaderMouseClick);
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Programmatic;// = DataGridViewAutoSizeColumnMode.Fill;
                if (col.HeaderText == "Visible" || col.Name == "Visible")
                {
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

                }
                if (col.ValueType == typeof(DateTime))
                {
                    col.DefaultCellStyle.Format = "d";
                    col.DefaultCellStyle.BackColor = Color.LightYellow;

                }
                if (col.ValueType == typeof(double))
                {
                    col.DefaultCellStyle.Format = "F3";
                    col.DefaultCellStyle.BackColor = Color.LightGray;

                }
            }  
           
        }

        void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyValue == 8 || e.KeyValue == 46)&& dgv.AllowUserToDeleteRows) 
                {

                  
                    DataGridViewSelectedCellCollection Selec = dgv.SelectedCells;
                    for (int i = 0; i < Selec.Count; i++)

                        Selec[i].Value = DBNull.Value;
                }
                if (e.KeyValue == 86 && e.Control)
                {
                   Paste();

                }
               

                if (e.Control == true && (e.KeyValue == 187))//|| e.KeyValue == 187))
                {
                    InsertaReglon();
                }


            }
            catch { }
        }
        public void Paste()
        {
            if (IsOrigen)
            {
                Pegar_Datgridview_dt(dgv);
            }
            else
                Pegar_Datgridview(dgv);//Actual);
        }
        void InsertaReglon()
        {
            try
            {
                if (IsDatasource)
                {

                    try
                    {


                        DataTable t = (DataTable)dgv.DataSource;
                        DataRow reglon = t.NewRow();
                        t.Rows.InsertAt(reglon, dgv.CurrentCell.RowIndex);
                        // dgv.Rows.Insert(, 1);
                    }
                    catch 
                    {


                    }
                }
                else
                {

                    dgv.Rows.Insert(dgv.CurrentCell.RowIndex);

                }
            }
            catch { }
        }
        void Pegar_Datgridview(DataGridView dgv)
        {
            try
            {

                DataTable Temp = new DataTable();
                char[] rowSplitter = new char[] { '\n', '\r' };
                char[] columnSplitter = new char[] { '\t' };
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
                    Notificacion.ShowBalloonTip(2000, "Informacion - " + Application.ProductName, "Espere... leyendo valores del portapapeles. \n Puede tardar varios minutos", ToolTipIcon.Warning);
                    DateTime Inicio = DateTime.Now;

                    if (dgv.Rows.Count < (r + Temp.Rows.Count))
                        dgv.Rows.Add(r + Temp.Rows.Count - dgv.Rows.Count + 1);

                    //MessageBox.Show("cargare: " +Temp.Columns.Count.ToString());

                    // if (c <= 1)
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
                        catch (Exception Ex) { MessageBox.Show(Ex.Message); }
                    }
                    /* else
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
                         catch (Exception Ex) { MessageBox.Show(Ex.Message); }
                     }
                     * */
                    DateTime Fin = DateTime.Now;
                    // dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    Notificacion.ShowBalloonTip(500, "Informacion - " + Application.ProductName, "Listo... Se agrego " + Temp.Rows.Count.ToString() + " registros \n  En " + (Fin - Inicio).ToString(), ToolTipIcon.Info);
                }

            }
            catch { }
        }

        void Pegar_Datgridview_dt(DataGridView dgv)
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
                for (int iCol = 0; iCol < filas_en_clipboard[0].Split(columnSplitter).Length && iCol < dgv.Columns.Count + c; iCol++)
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
                    for (int iCol = 0; iCol < valuesInRow.Length && iCol < dgv.Columns.Count + c; iCol++)
                    {
                        //     // 'assign cell value, only if it within columns of the grid

                        Reglon[iCol] = valuesInRow[iCol];
                    }
                    Temp.Rows.Add(Reglon);

                }

                dgv.SuspendLayout();
                if (Temp.Rows.Count > 0)
                {
                    Notificacion.ShowBalloonTip(2000, "Informacion - " + Application.ProductName, "Espere... leyendo valores del portapapeles. \n Puede tardar varios minutos", ToolTipIcon.Warning);
                    DateTime Inicio = DateTime.Now;
                    DataTable dt = (DataTable)dgv.DataSource;
                    dgv.DataSource = null;
                    if (dt.Rows.Count < (r + Temp.Rows.Count))
                    {

                        //   dgv.Rows.Add(r + Temp.Rows.Count - dgv.Rows.Count + 1);

                        int Max = r + Temp.Rows.Count - dt.Rows.Count;
                        for (int k = 0; k < Max; k++)
                        {
                            Reglon = dt.NewRow();
                            dt.Rows.Add(Reglon);

                        }
                    }
                    //     dgv.Rows.Add(r + Temp.Rows.Count - dgv.Rows.Count + 1);

                    //MessageBox.Show("cargare: " +Temp.Columns.Count.ToString());

                    //  if (c <= 1)
                    {
                        try
                        {

                            for (int i = 0; i < Temp.Rows.Count; i++)
                            {
                                //  dataGridView1.Rows.Add();
                                for (int j = 0; j < Temp.Columns.Count; j++)
                                {
                                    if ((j + c - 1) < dt.Columns.Count - 1)
                                    {
                                        dt.Rows[i + r][j + c] = Temp.Rows[i][j];

                                    }

                                }

                            }
                        }
                        catch (Exception Ex) { MessageBox.Show(Ex.Message); }
                    }
                    /* else
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
                         catch (Exception Ex) { MessageBox.Show(Ex.Message); }
                     }
                        */
                    dgv.DataSource = dt;
                    DateTime Fin = DateTime.Now;
                    // dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    Notificacion.ShowBalloonTip(500, "Informacion - " + Application.ProductName, "Listo... Se agrego " + Temp.Rows.Count.ToString() + " registros \n  En " + (Fin - Inicio).ToString(), ToolTipIcon.Info);

                }
                dgv.ResumeLayout(false);

            }
            catch (Exception error) { MessageBox.Show(error.Message); }
        }
        private void dgvorden_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            //  MessageBox.Show( dataGridViewX1.SortOrder.ToString()+ "xxx"+e.RowIndex.ToString());
         //   DataGridView dgv = (DataGridView)sender;

            try
            {
               

                if (!IsOrden)
                    return;
                DataTable t = (DataTable)dgv.DataSource;
                string Columna = dgv.Columns[e.ColumnIndex].Name;
                if (Columna == "Visible")
                    return;

                DataGridViewColumn newCol = dgv.Columns[e.ColumnIndex];
                newCol.SortMode = DataGridViewColumnSortMode.Programmatic;
                newCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
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
            catch 
            {


            }


        }


        internal void Ordena(DataTable Tabla_unidad, string campo, System.ComponentModel.ListSortDirection sortOrder)
        {
            if (Tabla_unidad == null)
                return;
            //DataRow reglonTemp;
            //aqui es la para lo de los decimales.
         //   MessageBox.Show("Comenzamos" + Tabla_unidad.Rows.Count.ToString());
            Visor_Datos vd = new Visor_Datos();
            try
            {
                if (Tabla_unidad.Columns.Contains(campo))
                {
                    Notificacion.ShowBalloonTip(500, "Informacion - " + Application.ProductName, "Comenzando la ordenacion. \n Puede tardar varios minutos \nreglones:" + Tabla_unidad.Rows.Count.ToString(), ToolTipIcon.Warning);
                    //se supone qeu podrian haber campos que no son numericos. asi que lo que haremos primero sera sacar en una tablita los que son letras
                    DataTable Letras = Tabla_unidad.Clone();
                    DataTable Numeros = Tabla_unidad.Clone();
                    DataTable Tiempo = Tabla_unidad.Clone();
                    DataTable VaciosoNulos = Tabla_unidad.Clone();
                    DataTable Basura = Tabla_unidad.Clone();
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
                    //aqui van los numeros && double.TryParse(row[campo].ToString(), out dtemp)
                    List<DataRow> RowNumero = (from row in Tabla_unidad.AsEnumerable().Where(p => p.Field<string>(campo) != null)
                                               where (IsNumeric(row[campo].ToString()) )
                                               select row).ToList();

                    foreach (DataRow row in RowNumero)
                    {
                        // Numeros.Rows.Add(row);
                        //mover los numeros
                        row[campo] = Numero(row[campo].ToString());
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

                                               where (!IsNumeric( row[campo].ToString()))
                                               select row).ToList();
                    int i = 0;
                    foreach (DataRow row in RowLetras)
                    {
                        // 
                        //Letras.Rows.Add(row);
                        //mover las letras.
                        Letras.ImportRow(row);
                        Tabla_unidad.Rows.Remove(row);
                        i++;
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
                        catch (Exception ex) { MessageBox.Show("ordenacion decendente"+ex.Message); }

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

                        catch 
                        {
                            MessageBox.Show("ordenacion acendente");
                        }
                    }
                    //agreguemos los vacios nulos y la basura
                    Tabla_unidad.Merge(VaciosoNulos);
                    Tabla_unidad.Merge(Basura);
                    Notificacion.ShowBalloonTip(500, "Informacion - " + Application.ProductName, "Se termino de ordenar \nreglones:"+ Tabla_unidad.Rows.Count.ToString(), ToolTipIcon.Info);
                    //aqui liberamos la memoria 
                    Letras.Dispose();
                    Numeros.Dispose();
                    Tiempo.Dispose();
                    VaciosoNulos.Dispose();
                   Basura .Dispose();
                }//de las columnas
            }
            catch (SystemException er)
            {
                MessageBox.Show(er.Message);
            }
            //MessageBox.Show("Terminamos" + Tabla_unidad.Rows.Count.ToString());

        }//de la clase
        private bool IsNumeric(string number)
        {
            

             number = number.Replace(',',' ');
            while(number.Contains(' '))
            {
                number = number.Remove(number.IndexOf(' '), 1);
             }
            if (number.Trim().Length < 1)
                return false;
            Regex pattern = new Regex(@"^(\d*)([.]*)(\d)(\d*)$");//(@"^[-]?[\d](.\d)?$");//("[^0-9][.]?[^0-9]");
           
            MatchCollection temp = pattern.Matches(number.Trim());

            if (temp.Count == 1)
                return true;
            return false;

            //  return pattern.IsMatch(number.Trim());
        }
        private double Numero(string number)
        {
            number = number.Replace(',', ' ');
            while (number.Contains(' '))
            {
                number = number.Remove(number.IndexOf(' '), 1);
            }
            try {
                return Convert.ToDouble(number); 
            }
            catch { }
            return 0;
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
       


    }
    class EventoPesgo
    {
        Gigasoft.ProEssentials.Pesgo Pesgo1;
        NotifyIcon Notificacion;
        int ko = 0;

        public int Alto;
        public int Ancho;
        public EventoPesgo(Gigasoft.ProEssentials.Pesgo pesgo_menu, NotifyIcon noti)
        {
            Pesgo1 = pesgo_menu;

            Notificacion = noti;

            Crea();
            OpcionesSubmenu();
            Pesgo1.PeCustomMenu += new Gigasoft.ProEssentials.Pesgo.CustomMenuEventHandler(Pesgo1_PeCustomMenu);
            Pesgo1.KeyUp += new KeyEventHandler(Pesgo1_KeyUp);
            Pesgo1.PeDataHotSpot += new Gigasoft.ProEssentials.Pesgo.DataHotSpotEventHandler(Pego1_PeDataHotSpot);
            Pesgo1.PeCursorMoved += new Gigasoft.ProEssentials.Pesgo.CursorMovedEventHandler(Pego1_PeCursorMoved);
            Pesgo1.PeTableAnnotation += new Gigasoft.ProEssentials.Pesgo.TableAnnotationEventHandler(Pesgo1_PeTableAnnotation);
            Ancho = 725;
            Alto = 350;
            CreaTabla();

        }

        void Pesgo1_PeTableAnnotation(object sender, Gigasoft.ProEssentials.EventArg.TableAnnotationEventArgs e)
        {

            Int32 i;
            if (e.WorkingTable == 0)
            {  // zero represents first table annotation
                Pesgo1.PeGrid.Zoom.Mode = false;

                // Change color of selected table item //			
                for (i = 2; i < Pesgo1.PeAnnotation.Table.Rows; i++)
                    Pesgo1.PeAnnotation.Table.Color[i, 0] = Color.FromArgb(142, 142, 142);
                Pesgo1.PeAnnotation.Table.Color[e.RowIndex, e.ColumnIndex] = Color.FromArgb(198, 0, 0);

                String szSym;
                szSym = Pesgo1.PeAnnotation.Table.Text[e.RowIndex, e.ColumnIndex];
                szSym = szSym.Trim();



                Pesgo1.PeFunction.Reinitialize();
                Pesgo1.PeFunction.ResetImage(0, 0);
            }
        }

        void Pesgo1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Pesgo1.PeFunction.UndoZoom();

            }
            if (e.KeyData == Keys.Delete)
            {
                // se supone que se borrar.
                Control papa = (Control)Pesgo1.Parent;

                papa.Controls.Remove(Pesgo1);
                Pesgo1.Dispose();
                papa.Refresh();

            }
        }



        void OpcionesSubmenu()
        {
            Pesgo1.PeUserInterface.Menu.CustomMenuText[0] = "|";
            Pesgo1.PeUserInterface.Menu.CustomMenuText[1] = "1.- Ver Tabla de Datos";
            Pesgo1.PeUserInterface.Menu.CustomMenuText[2] = "2.- Editar Titulos";
            Pesgo1.PeUserInterface.Menu.CustomMenuText[3] = "3.- Copiar Imagen | Copiar a *.JPEG | Copiar a *.Png  | Copiar a *.meta | Copiar a *.bmp | Copiar a *.emf | Asistente";
            Pesgo1.PeUserInterface.Menu.CustomMenuText[4] = "4.- Copiar Grafica";
            Pesgo1.PeUserInterface.Menu.CustomMenuText[5] = "5.- Zoom | Zoom H | Zoom V| Zoom Area";
            Pesgo1.PeUserInterface.Menu.CustomMenuText[6] = "6.- Anotaciones";
            Pesgo1.PeUserInterface.Menu.CustomMenuText[7] = "7.- Leyenda Posicion | Arriba | Abajo| Derecha | Izquierda | InsideAxis | OneLine| TwoLine | InsideOverLap";
            Pesgo1.PeUserInterface.Menu.CustomMenuText[8] = "8.- Declinacion";
            Pesgo1.PeUserInterface.Menu.CustomMenuText[9] = "9.- Activar Tabla";
            Pesgo1.PeUserInterface.Menu.CustomMenuText[10] = "|";
        }
        private void Pesgo1_PeCustomMenu(object sender, Gigasoft.ProEssentials.EventArg.CustomMenuEventArgs e)
        {
            if (e.MenuIndex == 1)
            {
                try
                {
                    //  dgv_tabla_grafica.AutoGenerateColumns=false;
                    Gigasoft.ProEssentials.Pesgo G = (Gigasoft.ProEssentials.Pesgo)sender;
                    Visor_Datos visor = new Visor_Datos();

                    visor.Text = G.PeString.MainTitle;
                    visor.EstableceDatos((DataTable)G.Tag);
                    visor.NoOrdenable();
                    visor.EstableceFromato();
                    //visor.ModificaTamanio();
                    //visor.ShowDialog();
                    visor.Rapida(false);



                    //aqui 

                }
                catch { }
            }
            if (e.MenuIndex == 2)
            {
                //Aqui editaremos las columnas
                //pirmero a obtener todas las columnas antes de culaquien cosa
                Gigasoft.ProEssentials.Pesgo G = (Gigasoft.ProEssentials.Pesgo)sender;
                Visor_Datos visor = new Visor_Datos();
                DataTable t = new DataTable();
                t.Columns.Add("Etiqueta");
                t.Columns[0].ReadOnly = true;
                t.Columns.Add("Valor");
                t.Columns.Add("Color");
                //recoremos toda la tabla.
                int Inicio = 0;

                DataRow titulo = t.NewRow();
                titulo[0] = "Titulo";
                titulo[1] = G.PeString.MainTitle;
                t.Rows.Add(titulo);
                DataRow Subtitulo = t.NewRow();
                Subtitulo[0] = "Subtitulo";
                Subtitulo[1] = G.PeString.SubTitle;
                t.Rows.Add(Subtitulo);

                Inicio = 2;

                for (int i = 0; i < G.PeData.Subsets; i++)
                {
                    DataRow reglon = t.NewRow();
                    reglon[0] = "Variable";
                    reglon[1] = G.PeString.SubsetLabels[i];
                    reglon[2] = G.PeColor.SubsetColors[i].ToArgb();
                    t.Rows.Add(reglon);


                }
                //sumamos el eje X

                for (int i = 0; i < 1; i++)
                {
                    DataRow reglon = t.NewRow();
                    reglon[0] = "Eje X";
                    reglon[1] = G.PeString.XAxisLabel;
                    t.Rows.Add(reglon);


                }
                //subamos los ejes Y.
                int ejey = 0;
                for (int i = 0; i < 5; i++)
                {
                    G.PeGrid.WorkingAxis = i;
                    if (!G.PeString.YAxisLabel.Contains("Y Axis #"))
                    {
                        ejey++;
                        DataRow reglon = t.NewRow();
                        reglon[0] = "Eje Y" + ejey.ToString();
                        reglon[1] = G.PeString.YAxisLabel;
                        t.Rows.Add(reglon);

                    }
                }

                visor.Text = G.PeString.MainTitle;
                visor.EstableceDatos(t,true);
                visor.NoOrdenable();
                visor.ModificaTamanio();
                visor.Add(false);
                visor.ShowDialog();
                //si se modifico ahora veremos que paso
                if (t.Rows.Count > 0)
                {
                    //subamos el titulo y el subtitulo


                    G.PeString.MainTitle = t.Rows[0][1].ToString();
                    G.PeString.SubTitle = t.Rows[1][1].ToString();



                    //optenemos las variables
                    for (int i = 0; i < G.PeData.Subsets; i++)
                    {

                        G.PeString.SubsetLabels[i] = t.Rows[i + Inicio][1].ToString();

                        //como aqui es por subset tambien trataremos de sacar el color
                        G.PeColor.SubsetColors[i] = Color.FromArgb(Convert.ToInt32(t.Rows[i + Inicio][2]));

                    }

                    //Guardamos el eje X
                    G.PeString.XAxisLabel = t.Rows[G.PeData.Subsets + Inicio][1].ToString();
                    //optenemos el eje y
                    for (int i = 0; i < ejey; i++)
                    {
                        G.PeGrid.WorkingAxis = i;


                        G.PeString.YAxisLabel = t.Rows[i + G.PeData.Subsets + 1 + Inicio][1].ToString(); ;

                    }
                }
                G.Refresh();
                G.PeFunction.ReinitializeResetImage();
            }

            if (e.MenuIndex == 3)
            {
                Gigasoft.ProEssentials.Pesgo G = (Gigasoft.ProEssentials.Pesgo)sender;
                switch (e.SubmenuIndex)
                {
                    case 1:


                        G.PeFunction.Image.ExportImageDpi = 180;//dpi
                        G.PeFunction.Image.ExportImageLargeFont = false; //font
                        G.PeFunction.Image.JpegToClipboard(Ancho, Alto);
                        Notificacion.ShowBalloonTip(2000, "Informacion - " + Application.ProductName, "Copiado al portapapeles jpeg " + G.PeString.MainTitle, ToolTipIcon.Warning);
                        break;

                    case 2:
                        //Gigasoft.ProEssentials.Pesgo G = (Gigasoft.ProEssentials.Pesgo)sender;
                        G.PeFunction.Image.ExportImageDpi = 180;//dpi
                        G.PeFunction.Image.ExportImageLargeFont = false; //font
                        G.PeFunction.Image.PngToClipboard(Ancho, Alto);
                        Notificacion.ShowBalloonTip(2000, "Informacion - " + Application.ProductName, "Copiado al portapapeles png" + G.PeString.MainTitle, ToolTipIcon.Warning);
                        break;
                    case 3:
                        //  Gigasoft.ProEssentials.Pesgo G = (Gigasoft.ProEssentials.Pesgo)sender;
                        G.PeFunction.Image.ExportImageDpi = 180;//dpi
                        G.PeFunction.Image.ExportImageLargeFont = false; //font
                        G.PeFunction.Image.MetafileToClipboard(Ancho, Alto);

                        Notificacion.ShowBalloonTip(2000, "Informacion - " + Application.ProductName, "Copiado al portapapeles Meta" + G.PeString.MainTitle, ToolTipIcon.Warning);
                        break;
                    case 4:
                        // Gigasoft.ProEssentials.Pesgo G = (Gigasoft.ProEssentials.Pesgo)sender;
                        G.PeFunction.Image.ExportImageDpi = 180;//dpi
                        G.PeFunction.Image.ExportImageLargeFont = false; //font
                        G.PeFunction.Image.BitmapToClipboard(Ancho, Alto);
                        Notificacion.ShowBalloonTip(2000, "Informacion - " + Application.ProductName, "Copiado al portapapeles Bitmap" + G.PeString.MainTitle, ToolTipIcon.Warning);
                        break;
                    case 5:
                        // Gigasoft.ProEssentials.Pesgo G = (Gigasoft.ProEssentials.Pesgo)sender;
                        G.PeFunction.Image.ExportImageDpi = 180;//dpi
                        G.PeFunction.Image.ExportImageLargeFont = false; //font
                        G.PeFunction.Image.BitmapToClipboard(Ancho, Alto);
                        Notificacion.ShowBalloonTip(2000, "Informacion - " + Application.ProductName, "Copiado al portapapeles efm" + G.PeString.MainTitle, ToolTipIcon.Warning);
                        break;
                    case 6:
                        //List<Control> Controles = new List<Control>();
                        //Controles.Add(G);
                        //ExportaGraficas Exporta = new ExportaGraficas(ref Controles);
                        //Exporta.ShowDialog();
                        break;
                }     //del switch


            }// DEL MENU 3
            if (e.MenuIndex == 4)
            {
                //aqui es que copiamos la grafica a la memoria osea el pesgo.
                try
                {
                    Gigasoft.ProEssentials.Pesgo G = (Gigasoft.ProEssentials.Pesgo)sender;

                    G.PeFunction.SaveObjectToFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\file.out");


                }
                catch { }
            }
            if (e.MenuIndex == 5)
            {
                Gigasoft.ProEssentials.Pesgo G = (Gigasoft.ProEssentials.Pesgo)sender;
                switch (e.SubmenuIndex)
                {



                    case 1:
                        G.PeUserInterface.Allow.Zooming = AllowZooming.Horizontal;
                        G.PeUserInterface.Scrollbar.ScrollingHorzZoom = true;

                        break;
                    case 2:
                        G.PeUserInterface.Allow.Zooming = AllowZooming.Vertical;
                        G.PeUserInterface.Scrollbar.ScrollingHorzZoom = true;
                        break;
                    case 3:
                        G.PeUserInterface.Allow.Zooming = AllowZooming.HorzAndVert;
                        G.PeUserInterface.Scrollbar.ScrollingHorzZoom = true;
                        break;
                }
            }
            if (e.MenuIndex == 6)
            {

                Gigasoft.ProEssentials.Pesgo G = (Gigasoft.ProEssentials.Pesgo)sender;
                //*creamos un datagridview */

                dataGridViewX2.AllowUserToAddRows = false;



               // Entrada_Texto_Estado entrada = new Entrada_Texto_Estado(dataGridViewX2, ko, G);
               // entrada.comboBox1.SelectedItem = 0;

  //              entrada.Focus();
//                entrada.ShowDialog();


            }
            if (e.MenuIndex == 7)
            {
                // aqui es para la posicion de la grafica
                Gigasoft.ProEssentials.Pesgo G = (Gigasoft.ProEssentials.Pesgo)sender;
                //  G.PeLegend.Style = LegendStyle.OneLine;
                // G.PeLegend.Location = LegendLocation.Right;
                switch (e.SubmenuIndex)
                {



                    case 1:
                        G.PeLegend.Location = LegendLocation.Top;

                        break;
                    case 2:
                        G.PeLegend.Location = LegendLocation.Bottom;
                        break;
                    case 3:
                        G.PeLegend.Location = LegendLocation.Right;
                        break;
                    case 4:
                        G.PeLegend.Location = LegendLocation.Left;
                        break;
                    case 5:
                        G.PeLegend.Style = LegendStyle.OneLineInsideAxis;
                        break;
                    case 6:
                        G.PeLegend.Style = LegendStyle.OneLine;
                        break;
                    case 7:
                        G.PeLegend.Style = LegendStyle.TwoLine;
                        break;
                    case 8:
                        G.PeLegend.Style = LegendStyle.OneLineInsideOverlap;
                        break;
                }
                G.Refresh();

            }
            if (e.MenuIndex == 8)
            {
                Gigasoft.ProEssentials.Pesgo G = (Gigasoft.ProEssentials.Pesgo)sender;
                //aqui va lo de la declianacion;
                List<ObjetoSelecionable> Series = new List<ObjetoSelecionable>();
                for (int i = 0; i < G.PeData.Subsets; i++)
                {

                    MessageBox.Show(  G.PeLegend.SubsetLineTypes[i].ToString());
                    Series.Add(new ObjetoSelecionable(G.PeString.SubsetLabels[i], false, i));



                }
                if (Series.Count > 0)
                {
                    //aquitendre las series y creare un objeto selecioanble
                    try
                    {
                        userFiltroElemento fm = new userFiltroElemento(ref Series);
                        List<int> series_acargar = new List<int>();
                        if (Series.Count == 1)
                        {
                            Series[0].Estado = true;
                        }
                        else
                        {

                            fm.EnForm(true);
                        }
                        //ahora saquemos los subset.



                        foreach (ObjetoSelecionable obj in Series)
                        {
                            if (obj.Estado)
                                series_acargar.Add(obj.Index);
                        }

                        if (series_acargar.Count > 0)
                        {
                           // frmdeclinacionv2 Declina = new frmdeclinacionv2(G, series_acargar, Notificacion);
                          //  Declina.Show();
                        }
                        /*

                        int serie = 0;
                        userSeleccionaSerie s = new userSeleccionaSerie(Series, ref serie);
                        s.EnForm();
                        serie = s.serie;

                        frmDeclinacionV1 Declina = new frmDeclinacionV1(G, serie, Notificacion);
                        Declina.ShowDialog();
                         * */
                    }
                    catch (Exception Ex) { MessageBox.Show("No se pudo declinar :( \n" + Ex.Message); }

                }


            }
            if (e.MenuIndex == 9)
            {

                Gigasoft.ProEssentials.Pesgo Pesgo1 = (Gigasoft.ProEssentials.Pesgo)sender;
                if (Pesgo1.PeUserInterface.Menu.CustomMenuState[9, 0] == CustomMenuState.Checked)
                {
                    Pesgo1.PeUserInterface.Menu.CustomMenuState[9, 0] = CustomMenuState.UnChecked;
                    Pesgo1.PeAnnotation.Table.Show = false;
                    Pesgo1.PeUserInterface.Cursor.Mode = CursorMode.NoCursor;  // Enable Vertical Cursor Mode.
                }
                else
                {
                    Pesgo1.PeUserInterface.Menu.CustomMenuState[9, 0] = CustomMenuState.Checked;
                    Pesgo1.PeAnnotation.Table.Show = true;
                    Pesgo1.PeUserInterface.Cursor.Mode = CursorMode.FloatingXY;  // Enable Vertical Cursor Mode.
                }



                Pesgo1.PeFunction.Reinitialize();
            }
        }

        void CreaTabla()
        {
            // Construct a simple table annotation //
            Pesgo1.PeAnnotation.Table.Working = 0;
            Pesgo1.PeAnnotation.Table.Rows = Pesgo1.PeData.Subsets + 2;
            Pesgo1.PeAnnotation.Table.Columns = 1;   // 12 is same number as PEP_nPOINTS

            // Pass the table text //
            Pesgo1.PeAnnotation.Table.Text[0, 0] = "Datos";
            Pesgo1.PeAnnotation.Table.Justification[0, 0] = TAJustification.Center;
            Pesgo1.PeAnnotation.Table.Bold[0, 0] = true;
            Pesgo1.PeAnnotation.Table.Text[1, 0] = "";
            for (int i = 0; i < Pesgo1.PeData.Subsets; i++)
            {
                Pesgo1.PeAnnotation.Table.Color[i + 2, 0] = Pesgo1.PeColor.SubsetColors[i];
                Pesgo1.PeAnnotation.Table.HotSpot[i + 2, 0] = true;
                Pesgo1.PeAnnotation.Table.Justification[i + 2, 0] = TAJustification.Left;
            }
            Pesgo1.PeAnnotation.Table.ColumnWidth[0] = 8;

            // Set Table Location //
            Pesgo1.PeAnnotation.Table.Location = SGraphTALocation.LeftCenter;

            // Other Table Related Properties ///
            Pesgo1.PeAnnotation.Table.Show = false;
            Pesgo1.PeAnnotation.Table.Border = TABorder.SingleLine;
            Pesgo1.PeAnnotation.Table.BackColor = Color.FromArgb(255, 255, 255);
            Pesgo1.PeAnnotation.Table.ForeColor = Color.FromArgb(0, 0, 0);
            Pesgo1.PeAnnotation.Table.HeaderRows = 1;
            Pesgo1.PeAnnotation.Table.TextSize = 100;



            Pesgo1.PeUserInterface.Cursor.MouseCursorControl = true;
            Pesgo1.PeUserInterface.HotSpot.Data = true;
            Pesgo1.PeUserInterface.Cursor.PromptTracking = false;
            Pesgo1.PeUserInterface.Cursor.PromptStyle = CursorPromptStyle.None;
            Pesgo1.PeUserInterface.HotSpot.Size = HotSpotSize.Large;

            Pesgo1.PeGrid.Option.ShowXAxis = ShowAxis.All;
        }
        //////////////////////////////////
        ///////////////////////////////////
        // DataHotSpot Event Handler      //
        ////////////////////////////////////
        private void Pego1_PeDataHotSpot(object sender, Gigasoft.ProEssentials.EventArg.DataHotSpotEventArgs e)
        {
            // Example 030 is only example with data hot spots //

            Pesgo1.PeUserInterface.Cursor.Point = e.PointIndex;  // Set Cursor's focus selected point.


        }



        private void Pego1_PeCursorMoved(object sender, System.EventArgs e)
        {

            float[] Valores = new float[Pesgo1.PeData.Subsets];
            Int32 nX;

            nX = Pesgo1.PeUserInterface.Cursor.Point;

            // Get Data at closest point //
            double fecha = 0;
            for (int i = 0; i < Pesgo1.PeData.Subsets; i++)
            {
                if (Pesgo1.PeData.Y[i, nX] != Pesgo1.PeData.NullDataValue)
                    Valores[i] = Pesgo1.PeData.Y[i, nX];
                else
                    Pesgo1.PeData.Y[i, nX] = 0;
            }

            fecha = Pesgo1.PeData.X[0, nX];
            // Get numeric precision //
            nX = Convert.ToInt32(Pesgo1.PeData.Precision);
            Pesgo1.PeAnnotation.Table.Rows = Pesgo1.PeData.Subsets + 2;


            // Place text in table annotation //
            Pesgo1.PeAnnotation.Table.Working = 0;
            Pesgo1.PeAnnotation.Table.Text[0, 0] = "Datos";
            Pesgo1.PeAnnotation.Table.Text[1, 0] = DateTime.FromOADate(fecha).ToShortDateString();


            // Pesgo1.PeAnnotation.Table.Text[0, 0] = "Series";
            for (int i = 0; i < Pesgo1.PeData.Subsets; i++)
            {

                Pesgo1.PeAnnotation.Table.Color[i + 2, 0] = Pesgo1.PeColor.SubsetColors[i];
                Pesgo1.PeAnnotation.Table.Text[i + 2, 0] = String.Format("{0:###.####}", Valores[i]);
            }
            Pesgo1.PeFunction.DrawTable(0);
        }



        DataGridView dataGridViewX2 = new DataGridView();

        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();

        private System.Windows.Forms.DataGridViewCheckBoxColumn Visible = new DataGridViewCheckBoxColumn();
        private System.Windows.Forms.DataGridViewTextBoxColumn Texto = new DataGridViewTextBoxColumn();
        private System.Windows.Forms.DataGridViewTextBoxColumn Color1 = new DataGridViewTextBoxColumn();
        private System.Windows.Forms.DataGridViewTextBoxColumn Tamaño = new DataGridViewTextBoxColumn();
        private System.Windows.Forms.DataGridViewTextBoxColumn Workin = new DataGridViewTextBoxColumn();
        private System.Windows.Forms.DataGridViewTextBoxColumn X = new DataGridViewTextBoxColumn();
        private System.Windows.Forms.DataGridViewTextBoxColumn Y = new DataGridViewTextBoxColumn();
        private System.Windows.Forms.DataGridViewTextBoxColumn Color_rgb = new DataGridViewTextBoxColumn();
        private System.Windows.Forms.DataGridViewTextBoxColumn wiht = new DataGridViewTextBoxColumn();
        void Crea()
        {
            // dataGridViewX2
            // 
            this.dataGridViewX2.AllowUserToAddRows = false;
            this.dataGridViewX2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewX2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Visible,
            this.Texto,
            this.Color1,
            this.Tamaño,
            this.Workin,
            this.X,
            this.Y,
            this.Color_rgb,
            this.wiht});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewX2.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.dataGridViewX2.Location = new System.Drawing.Point(300, 300);
            this.dataGridViewX2.Name = "dataGridViewX2";
            this.dataGridViewX2.Size = new System.Drawing.Size(217, 49);
            this.dataGridViewX2.TabIndex = 30;
            this.dataGridViewX2.Visible = false;
            /*
            this.dataGridViewX2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX2_CellClick);
            this.dataGridViewX2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX2_CellContentClick);
            this.dataGridViewX2.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX2_CellContentDoubleClick);
            this.dataGridViewX2.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX2_CellValueChanged);
            this.dataGridViewX2.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridViewX2_UserDeletedRow);
            this.dataGridViewX2.Click += new System.EventHandler(this.dataGridViewX2_Click);*/
            // 
            // Visible
            // 
            this.Visible.HeaderText = "Visible";
            this.Visible.Name = "Visible";
            // 
            // Texto
            // 
            this.Texto.HeaderText = "Texto";
            this.Texto.Name = "Texto";
            // 
            // Color1
            // 
            this.Color1.HeaderText = "Color";
            this.Color1.Name = "Color1";
            // 
            // Tamaño
            // 
            this.Tamaño.HeaderText = "Tamaño";
            this.Tamaño.Name = "Tamaño";
            // 
            // Workin
            // 
            this.Workin.HeaderText = "Workin";
            this.Workin.Name = "Workin";
            this.Workin.Visible = false;
            // 
            // X
            // 
            this.X.HeaderText = "X";
            this.X.Name = "X";
            this.X.Visible = false;
            // 
            // Y
            // 
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            this.Y.Visible = false;
            // 
            // Color_rgb
            // 
            this.Color_rgb.HeaderText = "Color_rgb";
            this.Color_rgb.Name = "Color_rgb";
            this.Color_rgb.Visible = false;
            // 
            // wiht
            // 
            this.wiht.HeaderText = "wiht";
            this.wiht.Name = "wiht";
            this.wiht.Visible = false;
        }

    }
    class EventoPego
    {
        Gigasoft.ProEssentials.Pego Pego1;
        NotifyIcon Notificacion;
        int ko = 0;

        public int Alto;
        public int Ancho;
        public EventoPego(Gigasoft.ProEssentials.Pego Pego_menu, NotifyIcon noti)
        {
            Pego1 = Pego_menu;

            Notificacion = noti;

            Crea();
            OpcionesSubmenu();
            Pego1.PeCustomMenu += new Gigasoft.ProEssentials.Pego.CustomMenuEventHandler(Pego1_PeCustomMenu);
            Pego1.KeyUp += new KeyEventHandler(Pego1_KeyUp);
            Pego1.PeDataHotSpot += new Gigasoft.ProEssentials.Pego.DataHotSpotEventHandler(Pego1_PeDataHotSpot);
            Pego1.PeCursorMoved += new Gigasoft.ProEssentials.Pego.CursorMovedEventHandler(Pego1_PeCursorMoved);
            Pego1.PeTableAnnotation += new Gigasoft.ProEssentials.Pego.TableAnnotationEventHandler(Pego1_PeTableAnnotation);
            Ancho = 725;
            Alto = 350;
            CreaTabla();

        }

        void Pego1_PeTableAnnotation(object sender, Gigasoft.ProEssentials.EventArg.TableAnnotationEventArgs e)
        {

            Int32 i;
            if (e.WorkingTable == 0)
            {  // zero represents first table annotation
                Pego1.PeGrid.Zoom.Mode = false;

                // Change color of selected table item //			
                for (i = 2; i < Pego1.PeAnnotation.Table.Rows; i++)
                    Pego1.PeAnnotation.Table.Color[i, 0] = Color.FromArgb(142, 142, 142);
                Pego1.PeAnnotation.Table.Color[e.RowIndex, e.ColumnIndex] = Color.FromArgb(198, 0, 0);

                String szSym;
                szSym = Pego1.PeAnnotation.Table.Text[e.RowIndex, e.ColumnIndex];
                szSym = szSym.Trim();



                Pego1.PeFunction.Reinitialize();
                Pego1.PeFunction.ResetImage(0, 0);
            }
        }

        void Pego1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Pego1.PeFunction.UndoZoom();

            }
            if (e.KeyData == Keys.Delete)
            {
                // se supone que se borrar.
                Control papa = (Control)Pego1.Parent;

                papa.Controls.Remove(Pego1);
                Pego1.Dispose();
                papa.Refresh();

            }
        }



        void OpcionesSubmenu()
        {
            Pego1.PeUserInterface.Menu.CustomMenuText[0] = "|";
            Pego1.PeUserInterface.Menu.CustomMenuText[1] = "1.- Ver Tabla de Datos";
            Pego1.PeUserInterface.Menu.CustomMenuText[2] = "2.- Editar Titulos";
            Pego1.PeUserInterface.Menu.CustomMenuText[3] = "3.- Copiar Imagen | Copiar a *.JPEG | Copiar a *.Png  | Copiar a *.meta | Copiar a *.bmp | Copiar a *.emf | Asistente";
            Pego1.PeUserInterface.Menu.CustomMenuText[4] = "4.- Copiar Grafica";
            Pego1.PeUserInterface.Menu.CustomMenuText[5] = "5.- Zoom | Zoom H | Zoom V| Zoom Area";
            Pego1.PeUserInterface.Menu.CustomMenuText[6] = "6.- Anotaciones";
            Pego1.PeUserInterface.Menu.CustomMenuText[7] = "7.- Leyenda Posicion | Arriba | Abajo| Derecha | Izquierda | InsideAxis | OneLine| TwoLine | InsideOverLap";
            Pego1.PeUserInterface.Menu.CustomMenuText[8] = "|";
        }
        private void Pego1_PeCustomMenu(object sender, Gigasoft.ProEssentials.EventArg.CustomMenuEventArgs e)
        {
            if (e.MenuIndex == 1)
            {
                try
                {
                    //  dgv_tabla_grafica.AutoGenerateColumns=false;
                    Gigasoft.ProEssentials.Pego G = (Gigasoft.ProEssentials.Pego)sender;
                    Visor_Datos visor = new Visor_Datos();

                    visor.Text = G.PeString.MainTitle;
                    visor.EstableceDatos((DataTable)G.Tag);
                    visor.NoOrdenable();
                    visor.EstableceFromato();
                    //visor.ModificaTamanio();
                    //visor.ShowDialog();
                    visor.Rapida(false);



                    //aqui 

                }
                catch { }
            }
            if (e.MenuIndex == 2)
            {
                //Aqui editaremos las columnas
                //pirmero a obtener todas las columnas antes de culaquien cosa
                Gigasoft.ProEssentials.Pego G = (Gigasoft.ProEssentials.Pego)sender;
               
                Visor_Datos visor = new Visor_Datos();
                DataTable t = new DataTable();
                t.Columns.Add("Etiqueta");
                t.Columns[0].ReadOnly = true;
                t.Columns.Add("Valor");
                t.Columns.Add("Color");
                //recoremos toda la tabla.
                int Inicio = 0;

                DataRow titulo = t.NewRow();
                titulo[0] = "Titulo";
                titulo[1] = G.PeString.MainTitle;
                t.Rows.Add(titulo);
                DataRow Subtitulo = t.NewRow();
                Subtitulo[0] = "Subtitulo";
                Subtitulo[1] = G.PeString.SubTitle;
                t.Rows.Add(Subtitulo);

                Inicio = 2;

                for (int i = 0; i < G.PeData.Points; i++)
                {
                    DataRow reglon = t.NewRow();
                    reglon[0] = "Variable";
                    reglon[1] = G.PeString.PointLabels[i];
                    reglon[2] = G.PeColor. SubsetColors[i].ToArgb();
                    t.Rows.Add(reglon);


                }
                //sumamos el eje X

                for (int i = 0; i < 1; i++)
                {
                    DataRow reglon = t.NewRow();
                    reglon[0] = "Eje X";
                    reglon[1] = G.PeString.XAxisLabel;
                    t.Rows.Add(reglon);


                }
                //subamos los ejes Y.
                int ejey = 0;
                for (int i = 0; i < 5; i++)
                {
                    G.PeGrid.WorkingAxis = i;
                    if (!G.PeString.YAxisLabel.Contains("Y Axis #"))
                    {
                        ejey++;
                        DataRow reglon = t.NewRow();
                        reglon[0] = "Eje Y" + ejey.ToString();
                        reglon[1] = G.PeString.YAxisLabel;
                        t.Rows.Add(reglon);

                    }
                }

                visor.Text = G.PeString.MainTitle;
                visor.EstableceDatos(t, true);
                visor.NoOrdenable();
                visor.ModificaTamanio();
                visor.Add(false);
                visor.ShowDialog();
                //si se modifico ahora veremos que paso
                if (t.Rows.Count > 0)
                {
                    //subamos el titulo y el subtitulo


                    G.PeString.MainTitle = t.Rows[0][1].ToString();
                    G.PeString.SubTitle = t.Rows[1][1].ToString();



                    //optenemos las variables
                    for (int i = 0; i < G.PeData.Points; i++)
                    {

                        G.PeString.PointLabels[i] = t.Rows[i + Inicio][1].ToString();

                        //como aqui es por subset tambien trataremos de sacar el color
                        G.PeColor.SubsetColors[i] = Color.FromArgb(Convert.ToInt32(t.Rows[i + Inicio][2]));

                    }

                    //Guardamos el eje X
                    G.PeString.XAxisLabel = t.Rows[G.PeData.Points + Inicio][1].ToString();
                    //optenemos el eje y
                    for (int i = 0; i < ejey; i++)
                    {
                        G.PeGrid.WorkingAxis = i;


                        G.PeString.YAxisLabel = t.Rows[i + G.PeData.Points + 1 + Inicio][1].ToString(); ;

                    }
                }
                G.Refresh();
                G.PeFunction.ReinitializeResetImage();
            }

            if (e.MenuIndex == 3)
            {
                Gigasoft.ProEssentials.Pego G = (Gigasoft.ProEssentials.Pego)sender;
                switch (e.SubmenuIndex)
                {
                    case 1:


                        G.PeFunction.Image.ExportImageDpi = 180;//dpi
                        G.PeFunction.Image.ExportImageLargeFont = false; //font
                        G.PeFunction.Image.JpegToClipboard(Ancho, Alto);
                        Notificacion.ShowBalloonTip(2000, "Informacion - " + Application.ProductName, "Copiado al portapapeles jpeg " + G.PeString.MainTitle, ToolTipIcon.Warning);
                        break;

                    case 2:
                        //Gigasoft.ProEssentials.Pego G = (Gigasoft.ProEssentials.Pego)sender;
                        G.PeFunction.Image.ExportImageDpi = 180;//dpi
                        G.PeFunction.Image.ExportImageLargeFont = false; //font
                        G.PeFunction.Image.PngToClipboard(Ancho, Alto);
                        Notificacion.ShowBalloonTip(2000, "Informacion - " + Application.ProductName, "Copiado al portapapeles png" + G.PeString.MainTitle, ToolTipIcon.Warning);
                        break;
                    case 3:
                        //  Gigasoft.ProEssentials.Pego G = (Gigasoft.ProEssentials.Pego)sender;
                        G.PeFunction.Image.ExportImageDpi = 180;//dpi
                        G.PeFunction.Image.ExportImageLargeFont = false; //font
                        G.PeFunction.Image.MetafileToClipboard(Ancho, Alto);

                        Notificacion.ShowBalloonTip(2000, "Informacion - " + Application.ProductName, "Copiado al portapapeles Meta" + G.PeString.MainTitle, ToolTipIcon.Warning);
                        break;
                    case 4:
                        // Gigasoft.ProEssentials.Pego G = (Gigasoft.ProEssentials.Pego)sender;
                        G.PeFunction.Image.ExportImageDpi = 180;//dpi
                        G.PeFunction.Image.ExportImageLargeFont = false; //font
                        G.PeFunction.Image.BitmapToClipboard(Ancho, Alto);
                        Notificacion.ShowBalloonTip(2000, "Informacion - " + Application.ProductName, "Copiado al portapapeles Bitmap" + G.PeString.MainTitle, ToolTipIcon.Warning);
                        break;
                    case 5:
                        // Gigasoft.ProEssentials.Pego G = (Gigasoft.ProEssentials.Pego)sender;
                        G.PeFunction.Image.ExportImageDpi = 180;//dpi
                        G.PeFunction.Image.ExportImageLargeFont = false; //font
                        G.PeFunction.Image.BitmapToClipboard(Ancho, Alto);
                        Notificacion.ShowBalloonTip(2000, "Informacion - " + Application.ProductName, "Copiado al portapapeles efm" + G.PeString.MainTitle, ToolTipIcon.Warning);
                        break;
                    case 6:
                        List<Control> Controles = new List<Control>();
                        Controles.Add(G);
                        ExportaGraficas Exporta = new ExportaGraficas(ref Controles);
                        Exporta.ShowDialog();
                        break;
                }     //del switch


            }// DEL MENU 3
            if (e.MenuIndex == 4)
            {
                //aqui es que copiamos la grafica a la memoria osea el Pego.
                try
                {
                    Gigasoft.ProEssentials.Pego G = (Gigasoft.ProEssentials.Pego)sender;

                    G.PeFunction.SaveObjectToFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\file.out");


                }
                catch { }
            }
            if (e.MenuIndex == 5)
            {
                Gigasoft.ProEssentials.Pego G = (Gigasoft.ProEssentials.Pego)sender;
                switch (e.SubmenuIndex)
                {



                    case 1:
                        G.PeUserInterface.Allow.Zooming = AllowZooming.Horizontal;
                        G.PeUserInterface.Scrollbar.ScrollingVertZoom = true;

                        break;
                    case 2:
                        G.PeUserInterface.Allow.Zooming = AllowZooming.Vertical;
                        G.PeUserInterface.Scrollbar.ScrollingVertZoom = true;
                        break;
                    case 3:
                        G.PeUserInterface.Allow.Zooming = AllowZooming.HorzAndVert;
                        G.PeUserInterface.Scrollbar.ScrollingVertZoom = true;
                        break;
                }
            }
            if (e.MenuIndex == 6)
            {

                Gigasoft.ProEssentials.Pego G = (Gigasoft.ProEssentials.Pego)sender;
                //*creamos un datagridview */

                dataGridViewX2.AllowUserToAddRows = false;



                //Entrada_Texto_Estado entrada = new Entrada_Texto_Estado(dataGridViewX2, ko, G);
                //entrada.comboBox1.SelectedItem = 0;

                //entrada.Focus();
                //entrada.ShowDialog();


            }
            if (e.MenuIndex == 7)
            {
                // aqui es para la posicion de la grafica
                Gigasoft.ProEssentials.Pego G = (Gigasoft.ProEssentials.Pego)sender;
                //  G.PeLegend.Style = LegendStyle.OneLine;
                // G.PeLegend.Location = LegendLocation.Right;
                switch (e.SubmenuIndex)
                {



                    case 1:
                        G.PeLegend.Location = LegendLocation.Top;

                        break;
                    case 2:
                        G.PeLegend.Location = LegendLocation.Bottom;
                        break;
                    case 3:
                        G.PeLegend.Location = LegendLocation.Right;
                        break;
                    case 4:
                        G.PeLegend.Location = LegendLocation.Left;
                        break;
                    case 5:
                        G.PeLegend.Style = LegendStyle.OneLineInsideAxis;
                        break;
                    case 6:
                        G.PeLegend.Style = LegendStyle.OneLine;
                        break;
                    case 7:
                        G.PeLegend.Style = LegendStyle.TwoLine;
                        break;
                    case 8:
                        G.PeLegend.Style = LegendStyle.OneLineInsideOverlap;
                        break;
                }
                G.Refresh();

            }

        }

        void CreaTabla()
        {
            // Construct a simple table annotation //
            Pego1.PeAnnotation.Table.Working = 0;
            Pego1.PeAnnotation.Table.Rows = Pego1.PeData.Subsets + 2;
            Pego1.PeAnnotation.Table.Columns = 1;   // 12 is same number as PEP_nPOINTS

            // Pass the table text //
            Pego1.PeAnnotation.Table.Text[0, 0] = "Datos";
            Pego1.PeAnnotation.Table.Justification[0, 0] = TAJustification.Center;
            Pego1.PeAnnotation.Table.Bold[0, 0] = true;
            Pego1.PeAnnotation.Table.Text[1, 0] = "";
            for (int i = 0; i < Pego1.PeData.Subsets; i++)
            {
                Pego1.PeAnnotation.Table.Color[i + 2, 0] = Pego1.PeColor.SubsetColors[i];
                Pego1.PeAnnotation.Table.HotSpot[i + 2, 0] = true;
                Pego1.PeAnnotation.Table.Justification[i + 2, 0] = TAJustification.Left;
            }
            Pego1.PeAnnotation.Table.ColumnWidth[0] = 8;

            // Set Table Location //
            Pego1.PeAnnotation.Table.Location = GraphTALocation.LeftCenter;

            // Other Table Related Properties ///
            Pego1.PeAnnotation.Table.Show = false;
            Pego1.PeAnnotation.Table.Border = TABorder.SingleLine;
            Pego1.PeAnnotation.Table.BackColor = Color.FromArgb(255, 255, 255);
            Pego1.PeAnnotation.Table.ForeColor = Color.FromArgb(0, 0, 0);
            Pego1.PeAnnotation.Table.HeaderRows = 1;
            Pego1.PeAnnotation.Table.TextSize = 100;



            Pego1.PeUserInterface.Cursor.MouseCursorControl = true;
            Pego1.PeUserInterface.HotSpot.Data = true;
            Pego1.PeUserInterface.Cursor.PromptTracking = false;
            Pego1.PeUserInterface.Cursor.PromptStyle = CursorPromptStyle.None;
            Pego1.PeUserInterface.HotSpot.Size = HotSpotSize.Large;

            Pego1.PeGrid.Option.ShowXAxis = ShowAxis.All;
        }
        //////////////////////////////////
        ///////////////////////////////////
        // DataHotSpot Event Handler      //
        ////////////////////////////////////
        private void Pego1_PeDataHotSpot(object sender, Gigasoft.ProEssentials.EventArg.DataHotSpotEventArgs e)
        {
            // Example 030 is only example with data hot spots //

            Pego1.PeUserInterface.Cursor.Point = e.PointIndex;  // Set Cursor's focus selected point.


        }



        private void Pego1_PeCursorMoved(object sender, System.EventArgs e)
        {

            float[] Valores = new float[Pego1.PeData.Subsets];
            Int32 nX;

            nX = Pego1.PeUserInterface.Cursor.Point;

            // Get Data at closest point //
            double fecha = 0;
            for (int i = 0; i < Pego1.PeData.Subsets; i++)
            {
                if (Pego1.PeData.Y[i, nX] != Pego1.PeData.NullDataValue)
                    Valores[i] = Pego1.PeData.Y[i, nX];
                else
                    Pego1.PeData.Y[i, nX] = 0;
            }

            fecha = Pego1.PeData.X[0, nX];
            // Get numeric precision //
            nX = Convert.ToInt32(Pego1.PeData.Precision);
            Pego1.PeAnnotation.Table.Rows = Pego1.PeData.Subsets + 2;


            // Place text in table annotation //
            Pego1.PeAnnotation.Table.Working = 0;
            Pego1.PeAnnotation.Table.Text[0, 0] = "Datos";
            Pego1.PeAnnotation.Table.Text[1, 0] = DateTime.FromOADate(fecha).ToShortDateString();


            // Pego1.PeAnnotation.Table.Text[0, 0] = "Series";
            for (int i = 0; i < Pego1.PeData.Subsets; i++)
            {

                Pego1.PeAnnotation.Table.Color[i + 2, 0] = Pego1.PeColor.SubsetColors[i];
                Pego1.PeAnnotation.Table.Text[i + 2, 0] = String.Format("{0:###.####}", Valores[i]);
            }
            Pego1.PeFunction.DrawTable(0);
        }



        DataGridView dataGridViewX2 = new DataGridView();

        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();

        private System.Windows.Forms.DataGridViewCheckBoxColumn Visible = new DataGridViewCheckBoxColumn();
        private System.Windows.Forms.DataGridViewTextBoxColumn Texto = new DataGridViewTextBoxColumn();
        private System.Windows.Forms.DataGridViewTextBoxColumn Color1 = new DataGridViewTextBoxColumn();
        private System.Windows.Forms.DataGridViewTextBoxColumn Tamaño = new DataGridViewTextBoxColumn();
        private System.Windows.Forms.DataGridViewTextBoxColumn Workin = new DataGridViewTextBoxColumn();
        private System.Windows.Forms.DataGridViewTextBoxColumn X = new DataGridViewTextBoxColumn();
        private System.Windows.Forms.DataGridViewTextBoxColumn Y = new DataGridViewTextBoxColumn();
        private System.Windows.Forms.DataGridViewTextBoxColumn Color_rgb = new DataGridViewTextBoxColumn();
        private System.Windows.Forms.DataGridViewTextBoxColumn wiht = new DataGridViewTextBoxColumn();
        void Crea()
        {
            // dataGridViewX2
            // 
            this.dataGridViewX2.AllowUserToAddRows = false;
            this.dataGridViewX2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewX2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Visible,
            this.Texto,
            this.Color1,
            this.Tamaño,
            this.Workin,
            this.X,
            this.Y,
            this.Color_rgb,
            this.wiht});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewX2.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.dataGridViewX2.Location = new System.Drawing.Point(300, 300);
            this.dataGridViewX2.Name = "dataGridViewX2";
            this.dataGridViewX2.Size = new System.Drawing.Size(217, 49);
            this.dataGridViewX2.TabIndex = 30;
            this.dataGridViewX2.Visible = false;
            /*
            this.dataGridViewX2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX2_CellClick);
            this.dataGridViewX2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX2_CellContentClick);
            this.dataGridViewX2.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX2_CellContentDoubleClick);
            this.dataGridViewX2.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX2_CellValueChanged);
            this.dataGridViewX2.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridViewX2_UserDeletedRow);
            this.dataGridViewX2.Click += new System.EventHandler(this.dataGridViewX2_Click);*/
            // 
            // Visible
            // 
            this.Visible.HeaderText = "Visible";
            this.Visible.Name = "Visible";
            // 
            // Texto
            // 
            this.Texto.HeaderText = "Texto";
            this.Texto.Name = "Texto";
            // 
            // Color1
            // 
            this.Color1.HeaderText = "Color";
            this.Color1.Name = "Color1";
            // 
            // Tamaño
            // 
            this.Tamaño.HeaderText = "Tamaño";
            this.Tamaño.Name = "Tamaño";
            // 
            // Workin
            // 
            this.Workin.HeaderText = "Workin";
            this.Workin.Name = "Workin";
            this.Workin.Visible = false;
            // 
            // X
            // 
            this.X.HeaderText = "X";
            this.X.Name = "X";
            this.X.Visible = false;
            // 
            // Y
            // 
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            this.Y.Visible = false;
            // 
            // Color_rgb
            // 
            this.Color_rgb.HeaderText = "Color_rgb";
            this.Color_rgb.Name = "Color_rgb";
            this.Color_rgb.Visible = false;
            // 
            // wiht
            // 
            this.wiht.HeaderText = "wiht";
            this.wiht.Name = "wiht";
            this.wiht.Visible = false;
        }

    }


   

}
