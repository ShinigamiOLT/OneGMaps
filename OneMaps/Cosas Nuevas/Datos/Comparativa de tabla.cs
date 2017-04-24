using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevComponents.DotNetBar;
using Maps;

namespace Maps
{
    public partial class Comparativa_de_tabla : Form
    {
        //public Comparativa_de_tabla(NotifyIcon Noti)
        //{
        //    InitializeComponent();
        //    Notificacion = Noti;
        //}

        public Comparativa_de_tabla(NotifyIcon Notificacion, DataTable t, List<DataTable> Lista_tablas)
        {
            // TODO: Complete member initialization
            this.Notificacion = Notificacion;
            this.TablaOrigen = t;
            TablaOrigen_1 = TablaOrigen.Clone();
            this.Lista_tablas = Lista_tablas;
            InitializeComponent();
        }
        NotifyIcon Notificacion;
        string File1 = "";
        string File2 = "";
        private DataTable TablaOrigen, TablaDestino, TablaOrigen_1, TablaDestino_1;
        private List<DataTable> Lista_tablas;
       

        private void Comparativa_de_tabla_Load(object sender, EventArgs e)
        {
            CargaInicial();
            foreach (DataTable dt in Lista_tablas)
            {
                if (dt != TablaOrigen)
                {
                   
                    cmbTablasCom.Items.Add(dt.TableName);
                }
            }
      
            Personaliza(dgv_origen);
            Personaliza(dgv_secundario);  
            Maps.EventosDGv dgv = new Maps.EventosDGv(dgv_origen, Notificacion);
            dgv.Especial();
            dgv.IsOrigen=(true);

            Maps.EventosDGv dgv1 = new Maps.EventosDGv(dgv_secundario, Notificacion);
            dgv1.Especial();
            dgv1.IsOrigen = (true);

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
                dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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



        private void MarcarIguales(List<String> Lista_Pozo, string Campo, DataGridView dgv,DataTable dt_,DataTable tab)
        {
            dt_.Clear();
            var Sireglones = from a in tab.AsEnumerable() where (Lista_Pozo.Contains(a[Campo].ToString())) orderby a[Campo].ToString()  select a;

            if (Sireglones.Any())
            {
               dt_.Merge(   Sireglones.CopyToDataTable());
              
            }
             dgv.DataSource = dt_;
            ////como ya tengo la lista ahora lo marcare eb la segunda tabla;
            //for (int i = 0; i < dgv.Rows.Count - 1; i++)
            //{
            //    if (dgv.Rows[i].Cells[Campo].Value != null)
            //    {
            //        dgv.CurrentCell = null;
            //        dgv.Rows[i].Visible = false;
            //        dgv.Rows[i].DefaultCellStyle.BackColor = Color.White;
            //        if (Lista_Pozo.Contains(dgv.Rows[i].Cells[Campo].Value.ToString()))
            //        {
            //            dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
            //            //Tabla_Inicial2.Rows[i].Cells[comboBoxEx1.Text].Style.BackColor = Color.Red;
            //            dgv.CurrentCell = null;
            //            dgv.Rows[i].Visible = true;
            //        }



            //    }
            //}
        }//del if combo 1 y combo2
        private void NOMarcarIguales(List<String> Lista_Pozo, string Campo, DataGridView dgv, DataTable dt_, DataTable tab)
        {
            //aqui haremos que 
            dt_.Clear();
            var Noreglones = from a in tab.AsEnumerable() where (!Lista_Pozo.Contains(a[Campo].ToString())) orderby a[Campo].ToString() select a;

            if (Noreglones.Any())
            {
                dt_.Merge(Noreglones.CopyToDataTable());
               
            }
 dgv.DataSource = dt_;
            ////como ya tengo la lista ahora lo marcare eb la segunda tabla;
            //for (int i = 0; i < dgv.Rows.Count - 1; i++)
            //{
            //    try
            //    {

            //        if (dgv.Rows[i].Cells[Campo].Value != null)
            //        {
            //            if (!Lista_Pozo.Contains(dgv.Rows[i].Cells[Campo].Value.ToString()))
            //            {
            //                dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
            //                //Tabla_Inicial2.Rows[i].Cells[comboBoxEx1.Text].Style.BackColor = Color.Red;
            //                dgv.CurrentCell = null;
            //                dgv.Rows[i].Visible = true;
            //            }
            //            else
            //            {
            //                dgv.CurrentCell = null;
            //                dgv.Rows[i].Visible = false;
            //                dgv.Rows[i].DefaultCellStyle.BackColor = Color.White;
            //            }

            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
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

        void CargaInicial()
        {
            comboBoxEx1.Items.Add(TablaOrigen.TableName);
            comboBoxEx1.SelectedIndex = 0;
            dgv_origen.DataSource = TablaOrigen;
            foreach (DataGridViewColumn Col in dgv_origen.Columns)
            {
                cmbIzquierda.Items.Add(Col.Name);
            }
            this.Text = Application.ProductName + " V. " + Application.ProductVersion + " " + File1 + " <<->>" + File2;

        }

        private void cmdCarga2_Click(object sender, EventArgs e)
        {

          
        }
        DataTable elemnto(string nombre)
        {
            foreach (DataTable dt in Lista_tablas)
                if (dt.TableName == nombre)
                {
                    TablaDestino = dt;
                    TablaDestino_1 = dt.Clone();
                    return dt; 
                }
            return null;
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
         

        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            List<Maps.ObjetoSelecionable> ListaColumna = new List<Maps.ObjetoSelecionable>();
            userFiltroElemento fmt;
            DataTable dt_nueva = new DataTable();

            if (rbizq_der.Checked && TablaOrigen_1.Rows.Count>0)// de izqueirda a .derecha
            {


                // como copiaremos de izuqierda  a derecha.
                foreach (DataColumn obj in TablaOrigen_1.Columns)
                {
                    ListaColumna.Add(new Maps.ObjetoSelecionable(obj));
                }


                fmt = new userFiltroElemento(ref ListaColumna, 0);
                fmt.EnForm(true);

                //ahora hagamos una tabla solo con las tablas seleccionadas.

                foreach (ObjetoSelecionable obj in ListaColumna)
                {
                    if (obj.Estado)
                    {
                        DataColumn Col = (DataColumn)obj.objeto;
                        dt_nueva.Columns.Add(Col.ColumnName, Col.DataType);
                        if (!TablaDestino.Columns.Contains(Col.ColumnName))
                        {
                            TablaDestino.Columns.Add(Col.ColumnName, Col.DataType);
                        }

                        if (!TablaDestino_1.Columns.Contains(Col.ColumnName))
                        {
                            TablaDestino_1.Columns.Add(Col.ColumnName, Col.DataType);
                        }
                    }
                }



                dt_nueva.Merge(TablaOrigen_1, true, MissingSchemaAction.Ignore);

                TablaDestino.Merge(dt_nueva, false, MissingSchemaAction.Ignore);
                TablaDestino_1.Merge(dt_nueva, false, MissingSchemaAction.Ignore);
                TablaOrigen_1.Rows.Clear();


            }
            if (rbder_izq.Checked && TablaDestino_1.Rows.Count >0)
            {
                // como copiaremos de izuqierda  a derecha.
                foreach (DataColumn obj in TablaDestino_1.Columns)
                {
                    ListaColumna.Add(new Maps.ObjetoSelecionable(obj));
                }


                fmt = new userFiltroElemento(ref ListaColumna, 0);
                fmt.EnForm(true);

                //ahora hagamos una tabla solo con las tablas seleccionadas.

                foreach (ObjetoSelecionable obj in ListaColumna)
                {
                    if (obj.Estado)
                    {
                        DataColumn Col = (DataColumn)obj.objeto;
                        dt_nueva.Columns.Add(Col.ColumnName, Col.DataType);

                        if (!TablaOrigen.Columns.Contains(Col.ColumnName))
                        {
                            TablaOrigen.Columns.Add(Col.ColumnName, Col.DataType);
                        }

                        if (!TablaOrigen_1.Columns.Contains(Col.ColumnName))
                        {
                            TablaOrigen_1.Columns.Add(Col.ColumnName, Col.DataType);
                        }
                    }
                }



                dt_nueva.Merge(TablaDestino_1, true, MissingSchemaAction.Ignore);




                TablaOrigen.Merge(dt_nueva, true, MissingSchemaAction.Ignore);
                TablaOrigen_1.Merge(dt_nueva, true, MissingSchemaAction.Ignore);
                TablaDestino_1.Rows.Clear();
            }
        }

        private void Tabla_Inicial_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
         dgv_secundario.FirstDisplayedScrollingColumnIndex = dgv_origen.FirstDisplayedScrollingColumnIndex;
            }
            catch { }
        }

        private void Tabla_Inicial2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Tabla_Inicial2_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
             dgv_origen.FirstDisplayedScrollingColumnIndex = dgv_secundario.FirstDisplayedScrollingColumnIndex;
            }
            catch { }
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            SaveFileDialog SALVAR = new SaveFileDialog();
            SALVAR.AddExtension = true;
            SALVAR.Filter = "Tabla de Datos (*.xml) | *.xml";
            if (SALVAR.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataSet Tablas = new DataSet("Datos");
                Tablas.Tables.Clear();
                //  Tablas.Tables.Add(Resultado);
                Tablas.Tables.Add( TablaOrigen_1.Copy() );

                Tablas.WriteXml(SALVAR.FileName,XmlWriteMode.WriteSchema);

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
                for (int i = 0; i < GridTabla.Rows.Count; i++)
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

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            SaveFileDialog SALVAR = new SaveFileDialog();
            SALVAR.AddExtension = true;
            SALVAR.Filter = "Tabla de Datos (*.xml) | *.xml";
            if (SALVAR.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataSet Tablas = new DataSet("Datos");
                Tablas.Tables.Clear();
                //  Tablas.Tables.Add(Resultado);
                Tablas.Tables.Add(TablaDestino_1.Copy());

                Tablas.WriteXml(SALVAR.FileName,XmlWriteMode.WriteSchema);

            }
        }

        private void rbDiferentes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (cmbTablasCom.SelectedItem != null)
            {

                string cad = cmbTablasCom.SelectedItem.ToString();
                dgv_secundario.DataSource = elemnto(cad);
                cmbDerecha.Items.Clear();
                File2 = cad;
                foreach (DataGridViewColumn Col in dgv_secundario.Columns)
                {
                    cmbDerecha.Items.Add(Col.Name);
                }
                this.Text = Application.ProductName + " V. " + Application.ProductVersion + " " + File1 + " <<->>" + File2;

            }
        }

        private void cmdcargaD_Click(object sender, EventArgs e)
        {
            if (cmbIzquierda.SelectedItem != null && cmbDerecha.SelectedItem != null)
            {
                List<string> Lista_Pozo = new List<string>();
                Notificacion.ShowBalloonTip(500, "Comenzando Busqueda: !!!", "Buscando en " + File1, ToolTipIcon.Info);
                // aqui saca los valores 

                //for (int i = 0; i < dgv_origen.Rows.Count - 1; i++)
                //{
                //    if (dgv_origen.Rows[i].Cells[cmbIzquierda.Text].Value != null)
                //    {
                //        if (!Lista_Pozo.Contains(dgv_origen.Rows[i].Cells[cmbIzquierda.Text].Value.ToString()))
                //            Lista_Pozo.Add(dgv_origen.Rows[i].Cells[cmbIzquierda.Text].Value.ToString());
                //    }
                //}

                Lista_Pozo.AddRange((from a in TablaOrigen.AsEnumerable() select a[cmbIzquierda.Text].ToString()).Distinct());
                Notificacion.ShowBalloonTip(1000, "Comparando con la Tablas: !!!", "recorriendo " + TablaOrigen.TableName, ToolTipIcon.Info);
                TablaDestino_1.Clear();

                if (rbIguales.Checked)
                    MarcarIguales(Lista_Pozo, cmbDerecha.Text, dgv_secundario, TablaDestino_1, TablaDestino);
                if (rbDiferentes.Checked)
                    NOMarcarIguales(Lista_Pozo, cmbDerecha.Text, dgv_secundario, TablaDestino_1, TablaDestino);



                Lista_Pozo.Clear();

                Notificacion.ShowBalloonTip(1000, "Comenzando Busqueda: !!!", "Buscando en " + File2, ToolTipIcon.Info);
                Lista_Pozo.AddRange((from a in TablaDestino.AsEnumerable() select a[cmbDerecha.Text].ToString()).Distinct());
                Notificacion.ShowBalloonTip(1000, "Comparando con la Tablas: !!!", "recorriendo " + TablaDestino.TableName, ToolTipIcon.Info);

                this.TablaOrigen_1.Clear();
                if (rbIguales.Checked)
                    MarcarIguales(Lista_Pozo, cmbIzquierda.Text, dgv_origen, TablaOrigen_1, TablaOrigen);
                if (rbDiferentes.Checked)
                    NOMarcarIguales(Lista_Pozo, cmbIzquierda.Text, dgv_origen, TablaOrigen_1, TablaOrigen);


                //for (int i = 0; i < dgv_secundario.Rows.Count - 1; i++)
                //{
                //    if (dgv_secundario.Rows[i].Cells[cmbDerecha.Text].Value != null)
                //    {
                //        if (!Lista_Pozo.Contains(dgv_secundario.Rows[i].Cells[cmbDerecha.Text].Value.ToString()))
                //            Lista_Pozo.Add(dgv_secundario.Rows[i].Cells[cmbDerecha.Text].Value.ToString());
                //    }
                //}

                //if (rbIguales.Checked)
                //    MarcarIguales(Lista_Pozo, cmbIzquierda.Text, dgv_origen);
                //if (rbDiferentes.Checked)
                //    NOMarcarIguales(Lista_Pozo, cmbIzquierda.Text, dgv_origen);



                Notificacion.ShowBalloonTip(1000, "Completado", "Su tabla esta generada.", ToolTipIcon.Info);
                dgv_origen.FirstDisplayedScrollingColumnIndex = dgv_origen.Columns[cmbIzquierda.Text].DisplayIndex;
                dgv_secundario.FirstDisplayedScrollingColumnIndex = dgv_secundario.Columns[cmbDerecha.Text].DisplayIndex;
            }
            else
            {

            }
        }

        private void labelItem6_Click(object sender, EventArgs e)
        {

        }

    }
}
