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

namespace Maps
{
    public partial class BusquedadeDatos : Form
    {
        DataTable Origen;
        DataTable Resultado;
        public BusquedadeDatos(DataTable origen)
        {
            InitializeComponent();
            try
            {
                Origen = origen;
                Resultado = origen.Clone();
              //  colCampos.Items.Clear();
                foreach (DataColumn Col in Origen.Columns)
                {
                    cbCampo.Items.Add(Col.ColumnName);
                //    colCampos.Items.Add(Col.ColumnName);
                }

            }
            catch (Exception error) { MessageBox.Show(error.Message); }

        }
        public void Personaliza(DataGridView Tabla_Elementos)
        {
            try
            {
                DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
                Tabla_Elementos.BackgroundColor = System.Drawing.Color.White;
                Tabla_Elementos.BorderStyle = System.Windows.Forms.BorderStyle.None;
                Tabla_Elementos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                Tabla_Elementos.RowHeadersWidth = 24;
                Tabla_Elementos.Cursor = System.Windows.Forms.Cursors.Default;
                dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
                dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
                dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
                dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
                dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
                dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
                Tabla_Elementos.DefaultCellStyle = dataGridViewCellStyle1;
                Tabla_Elementos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            }
            catch
            {
            }
        }
      
        private void BusquedadeDatos_Load(object sender, EventArgs e)
        {
            dgv_resulatdo.DataSource = Origen;
          Personaliza(  dgv_resulatdo);
          Personaliza(dataGridView1);
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
                    // Notificacion.ShowBalloonTip(2000, "Informacion - " + Application.ProductName, "Espere... leyendo valores del portapapeles. \n Puede tardar varios minutos", ToolTipIcon.Warning);
                    DateTime Inicio = DateTime.Now;

                    if (dgv.Rows.Count < (r + Temp.Rows.Count))
                        dgv.Rows.Add(r + Temp.Rows.Count - dgv.Rows.Count + 1);

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
            catch { }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }

        private DataTable SelectDataTable(DataTable dt, string filtro)
        {
            DataRow[] rows;
            DataTable dtNew = new DataTable();
            dtNew = dt.Clone();
            rows = dt.Select(filtro);
            foreach (DataRow dr in rows)
            {
                dtNew.ImportRow(dr);
            }

            return dtNew;
        }
        private void SelectDataTable1(DataTable Resultado, DataTable dt, string filtro)
        {
            DataRow[] rows;
            DataTable dtNew = new DataTable();

            rows = dt.Select(filtro);
            foreach (DataRow dr in rows)
            {
                Resultado.ImportRow(dr);
            }


        }
        private void SelectDataTable2(DataTable Resultado, DataTable dt, string filtro)
        {
            Resultado.Rows.Clear();
            DataRow[] rows;
            DataTable dtNew = new DataTable();

            rows = dt.Select(filtro);
            foreach (DataRow dr in rows)
            {
                Resultado.ImportRow(dr);
            }


        }
        void buequedainversa()
        {
 
                        String filtro = "";
                        DataTable temp = Origen.Copy();
                        Resultado.Clear();
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            try
                            {
                                Resultado.Clear();
                                string frase = dataGridView1[0, i].Value.ToString().Trim();
                                if (rbContenga.Checked)
                                    //busca caulquiera que contenga
                                    filtro = "[" + cbCampo.SelectedItem.ToString() + "]" + "NOT Like '%" + frase + "%'";
                                //aqui que solo comienza
                                if (rbComienze.Checked)
                                    filtro = "[" + cbCampo.SelectedItem.ToString() + "]" + "NOT Like '" + frase + "%'";
                                //palabra eXacta
                                if (rbExacto.Checked)
                                    filtro = "[" + cbCampo.SelectedItem.ToString() + "]" + "NOT Like '" + frase + "'";

                                SelectDataTable1(Resultado, temp, filtro);
                                temp.Rows.Clear();
                                temp = Resultado.Copy();

                            }
                            catch { }
                        }
                        dgv_resulatdo.DataSource = Resultado;
                        //  dgv_resulatdo.Sort(dgv_resulatdo.Columns[cbCampo.SelectedItem.ToString()], System.ComponentModel.ListSortDirection.Ascending);
                   
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            //aqui es dond ebuscaremos los datos

            try
            {
                if (cbCampo.SelectedItem != null)
                {
                    if (dataGridView1.Rows.Count <= 1)
                    {
                        expandableSplitter2.Expanded = true;
                        MessageBox.Show("Debe de introducir un elemento a buscar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Resultado.Clear();

                        if (!Invertido.Checked)
                        {//busqueda Ordinaria
                            Busqueda_Ordinaria();
                        }
                        else
                        {
                            buequedainversa();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se puede hacer la busqueda, Selecione un campo para buscar el dato", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch { }
        }

        void Busqueda_Ordinaria()
        {
            try
    {

                String filtro = "";
                Resultado.Clear();
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    try
                    {

                        string frase = dataGridView1[0, i].Value.ToString().Trim();
                        if (rbContenga.Checked)
                            //busca caulquiera que contenga
                            filtro = "[" + cbCampo.SelectedItem.ToString() + "]" + "Like '%" + frase + "%'";
                        //aqui que solo comienza
                        if (rbComienze.Checked)
                            filtro = "[" + cbCampo.SelectedItem.ToString() + "]" + "Like '" + frase + "%'";
                        //palabra eXacta
                        if (rbExacto.Checked)
                            filtro = "[" + cbCampo.SelectedItem.ToString() + "]" + "Like '" + frase + "'";

                        SelectDataTable1(Resultado, Origen, filtro);


                    }
                    catch { }
                }
                dgv_resulatdo.DataSource = Resultado;
                //  dgv_resulatdo.Sort(dgv_resulatdo.Columns[cbCampo.SelectedItem.ToString()], System.ComponentModel.ListSortDirection.Ascending);

            }
            catch { }
        }
        private void buttonItem8_Click(object sender, EventArgs e)
        {
            SaveFileDialog salvar = new SaveFileDialog();
            salvar.DefaultExt = ".xls";
            salvar.Filter = "Hoja de Excel (*.xls) | *.xls";
            if (salvar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ExportaAExcelDLL.GuardaTablaenExcel Nuevo = new ExportaAExcelDLL.GuardaTablaenExcel();

                Nuevo.DataTableToExcelSave(dgv_resulatdo, salvar.FileName);
            }
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
                for (int i = 0; i < GridTabla.Rows.Count ; i++)
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

        private void buttonItem10_Click(object sender, EventArgs e)
        {
            SaveFileDialog SALVAR = new SaveFileDialog();
            SALVAR.AddExtension = true;
            SALVAR.Filter = "Tipo One Registro (*.OneClass) | *.OneClass";
            if (SALVAR.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataSet Tablas = new DataSet("REGISTRO_1");
                Tablas.Tables.Clear();
              //  Tablas.Tables.Add(Resultado);
                Tablas.Tables.Add(GuardaDatos(dgv_resulatdo));

                Tablas.WriteXml(SALVAR.FileName);

            }
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            expandableSplitter2.Expanded = true;
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 86 && e.Control)
            {




                Pegar_Datgridview(dataGridView1);//Actual);

            }
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_resulatdo.SelectAll();
                Clipboard.SetDataObject(dgv_resulatdo.GetClipboardContent(), true);
            }
            catch { MessageBoxEx.Show("Error al copiar", "Alerta!!!" + Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            //aqui es dond ebuscaremos los datos
            dgv_resulatdo.DataSource = null;
            try
            {
                if (cbCampo.SelectedItem != null)
                {
                    String filtro = "";
                    Resultado.Clear();


                    filtro = "[" + cbCampo.SelectedItem.ToString() + "] " + textBoxItem1.Text;
                   
                   // SelectDataTable1(Resultado, Origen, filtro);
                    int ope=0;
                    if (menor.Checked)
                        ope = 0;
                    if (mayorigual.Checked)
                        ope = 1;
                    if (igual.Checked)
                        ope = 2;
                    if (diferente.Checked)
                        ope = 3;


                    dgv_resulatdo.DataSource = info(Convert.ToDouble(textBoxItem1.Text), cbCampo.SelectedItem.ToString(), Origen,ope,true) ;// Resultado;
                    //  dgv_resulatdo.Sort(dgv_resulatdo.Columns[cbCampo.SelectedItem.ToString()], System.ComponentModel.ListSortDirection.Ascending);
                }
                else
                {
                    MessageBox.Show("No se puede hacer la busqueda, Selecione un campo para buscar el dato", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
            catch { }
        }
     


       

        DataTable info(double Dato, string IndexColumna, DataTable origen,int Operacion_realizar,bool sentido)
        {
            DataTable L = origen.Clone();
            try
            {
                if (origen.Rows.Count > 0)
                {
                    
                    IEnumerable<DataRow> CampoInicial =
                        from pozo in origen.AsEnumerable()
                       // where pozo.Field<double>(IndexColumna) == Dato
                        select pozo;


                    //  IEnumerable<DataRow> Selecionado = CampoInicial.Where(p => p.Field<double>(IndexColumna)< Dato);
                    int i = 0;
                   
                    try
                    {
                        foreach (DataRow p in CampoInicial)//  Selecionado)
                        {
                            try
                            {
                                double val;
                                if (IsNumeric(p.Field<string>(IndexColumna)))
                                {
                                    string ejemplo = p.Field<string>(IndexColumna).Trim();
                                    double.TryParse(p.Field<string>(IndexColumna), out val);
                                    if (sentido)
                                    {
                                      
                                        switch (Operacion_realizar)
                                        {
                                            case 0:
                                                if (val < Dato)
                                                {
                                                    L.ImportRow(p);
                                                    i++;
                                                }
                                                break;
                                            case 1:
                                                if (val >= Dato)
                                                {
                                                    L.ImportRow(p);
                                                    i++;
                                                }
                                                break;
                                            case 2:
                                                if (val == Dato)
                                                {
                                                    L.ImportRow(p);
                                                    i++;
                                                }
                                                break;
                                            case 3:
                                                if (val != Dato)
                                                {
                                                    L.ImportRow(p);
                                                    i++;
                                                }
                                                break;
                                        }//del switch
                                    }
                                    else
                                    {
                                          switch (Operacion_realizar)
                                        {
                                            case 0:
                                                if (!(val < Dato))
                                                {
                                                    L.ImportRow(p);
                                                    i++;
                                                }
                                                break;
                                            case 1:
                                                if (!(val >= Dato))
                                                {
                                                    L.ImportRow(p);
                                                    i++;
                                                }
                                                break;
                                            case 2:
                                                if (!(val == Dato))
                                                {
                                                    L.ImportRow(p);
                                                    i++;
                                                }
                                                break;
                                            case 3:
                                                if (!(val != Dato))
                                                {
                                                    L.ImportRow(p);
                                                    i++;
                                                }
                                                break;
                                        
                                        }//del switch
                                    }

                                }
                                
                            }
                            catch (Exception Error) { MessageBox.Show(Error.Message+p.Field<string>("POZO No.") +" : "+i.ToString() +p.Field<string>(IndexColumna)); }

                        }
                    }
                    catch (Exception Error) { MessageBox.Show(Error.Message + "\n Datos: " + Dato + "Numero: " + i.ToString()); }
                }
            }
            catch (Exception Error) { MessageBox.Show(Error.Message); }

            L.Namespace += Dato + "  ";

            return L;
        }

     

        private void labelItem3_Click(object sender, EventArgs e)
        {

        }

        private void dgv_resulatdo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            //aqui generare mi propio regex para validar mi cadena.
            String filtro = "";

            filtro = textBoxItem1.Text;
            //veremos si la cadena es valida si contiene
            if(EsExpresionValida(filtro))
                MessageBox.Show("si es valida");
                  

        }
        private bool EsExpresionValida(string number)
        {
            if (number.Length < 1)
                return false;
            Regex pattern = new Regex(@"^[\+-]?[\d](.+[\d])?\s?[(!)(<)(>)(=)]{1}\s?$");//("[^0-9][.]?[^0-9]");
            return pattern.IsMatch(number);
        }
        private bool IsNumeric(string number)
        {
            if (number.Length < 1)
                return false;
            Regex pattern = new Regex(@"^[-]?[\d](.\d)?$");//("[^0-9][.]?[^0-9]");
            return pattern.IsMatch(number);
        }

    }//class


}//namespace
