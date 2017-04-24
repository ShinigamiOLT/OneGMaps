using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Maps;

namespace Maps
{
    public partial class Visor_Datos : Form
    {
        public int IndexRow=0;
        public int Col = 0;
        public bool Save = false;
        int Id=0;
        NotifyIcon Notificacion = new NotifyIcon();
        EventosDGv dg;
        public Visor_Datos()
        {
            InitializeComponent();
            dg= new EventosDGv (dgv_tabla_grafica,new NotifyIcon());
        }

        private void Visor_Datos_Load(object sender, EventArgs e)
        {
            if (Id == 1)
            {
                Extra();
            }
            this.Text+= " Rows"+ dgv_tabla_grafica.Rows.Count.ToString();
            dg.Especial();
          
        }

        public void NoOrdenable()
        {

            foreach (DataGridViewColumn column in dgv_tabla_grafica.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            }
        }

        public void EstableceDatos(object Objeto)
        {
            dgv_tabla_grafica.DataSource = Objeto;
            Text = ((DataTable)Objeto).TableName;
            dg.IsOrigen = true;
           
        }
        public void EstableceDatos(object Objeto,bool Special)
        {
            dgv_tabla_grafica.AutoGenerateColumns = false;

            dgv_tabla_grafica.DataSource = (DataTable)Objeto;  
            DataTable tabla = ((DataTable)Objeto);
            
            //apliquemos solo las dos primeras.
            DataGridViewTextBoxColumn Columna = new  DataGridViewTextBoxColumn();
            Columna.DataPropertyName = tabla.Columns[0].ColumnName;
            Columna.HeaderText = "Etiqueta";
            dgv_tabla_grafica.Columns.Add(Columna);

            DataGridViewTextBoxColumn Columna2 = new DataGridViewTextBoxColumn();
            Columna2.DataPropertyName = tabla.Columns[1].ColumnName;
            Columna2.HeaderText = "Valor";
            dgv_tabla_grafica.Columns.Add(Columna2);

            DataGridViewTextBoxColumn Columna3 = new DataGridViewTextBoxColumn();
           // Columna3.DataPropertyName = tabla.Columns[2].ColumnName;
            Columna3.HeaderText = "Color";
            dgv_tabla_grafica.Columns.Add(Columna3);

            
            Text = ((DataTable)Objeto).TableName;

            //aqui recoremos y sacamos los valores.
            dg.IsOrigen = true;
         
            Id = 1;
            this.dgv_tabla_grafica.CellClick += new DataGridViewCellEventHandler(dgv_tabla_grafica_CellClick2);

        }
        void Extra()
    {
          DataTable tabla = (DataTable)dgv_tabla_grafica.DataSource;
            for (int i = 0; i < tabla.Rows.Count; i++ )
            {
                try
                {
                    dgv_tabla_grafica.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(Convert.ToInt32(tabla.Rows[i][2]));
                }
                catch
                {
                    dgv_tabla_grafica.Rows[i].Cells[2].Style.BackColor = Color.White;
                }
            }
}
        private void dgv_tabla_grafica_CellClick2(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //    IndexRow = e.RowIndex;
            //if (e.ColumnIndex >= 0)
            //    Col = e.ColumnIndex;

            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                ColorDialog Colornuevo = new ColorDialog();
                if (Colornuevo.ShowDialog() == DialogResult.OK)
                {
                    dgv_tabla_grafica[e.ColumnIndex, e.RowIndex].Style.BackColor = Colornuevo.Color;
                    dgv_tabla_grafica[e.ColumnIndex, e.RowIndex].Tag = Colornuevo.Color;
                    DataTable t = (DataTable)dgv_tabla_grafica.DataSource;
                    t.Rows[e.RowIndex][e.ColumnIndex] = Colornuevo.Color.ToArgb();

                }
            }
        }
        public void Rapida()
        {
            
           EstableceFromato();
            Add(false);
            WindowState = FormWindowState.Maximized;
            Show();
        }
        public void Rapida(bool  otro)
        {

            EstableceFromato();
            Add(otro);
            AutoColum();
            WindowState = FormWindowState.Normal;
          
            ShowDialog();
        }

        public void EstableceFromato()
        {
            foreach (DataGridViewColumn column in dgv_tabla_grafica.Columns)
            {
                column.DefaultCellStyle.Format = "F3";
            }
        }
        public void ModificaTamanio()
        {
          //  Modifica();
        //   this.Size = new System.Drawing.Size ( dgv_tabla_grafica.Width+10,dgv_tabla_grafica.Height+10);
        }

        void Modifica_()
        {
            try
            {
                //if (dgv.Rows.Count > 0)
                {

                    int Largo = (dgv_tabla_grafica.RowCount + 4) * (dgv_tabla_grafica.RowTemplate.Height) + 5;
                    dgv_tabla_grafica.Size = new Size(dgv_tabla_grafica.Size.Width, Largo);

                    this.Size = new Size(this.Width, Largo);
                   

                }
            }
            catch { }
        }

        internal void Add(bool p)
        {
            dgv_tabla_grafica.AllowUserToAddRows = p;
        }

        internal void AutoColum()
        {
            foreach (DataGridViewColumn column in dgv_tabla_grafica.Columns)
            {
                column.AutoSizeMode= DataGridViewAutoSizeColumnMode.None;
            }
        }

        private void dgv_tabla_grafica_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           if( e.RowIndex>=0)
            IndexRow = e.RowIndex;
           if (e.ColumnIndex >= 0)
               Col = e.ColumnIndex;
        }

        private void Visor_Datos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                dgv_tabla_grafica.CommitEdit(DataGridViewDataErrorContexts.Commit);
                
                DataTable t = (DataTable)dgv_tabla_grafica.DataSource;

                if (Save)
                {
                    if (MessageBox.Show(" Seguro de guardar los cambios ?", "Alerta!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        t.AcceptChanges();
                    else
                        t.RejectChanges();
                }

                //
               
            }
            catch { }
        }

        private void cmdAddRows_Click(object sender, EventArgs e)
        {
            InsertaReglon();
        }
        void InsertaReglon()
        {
           
            try
            {
               
           
                DataTable t = (DataTable)dgv_tabla_grafica.DataSource;
                DataRow reglon = t.NewRow();
                t.Rows.InsertAt(reglon, dgv_tabla_grafica.CurrentCell.RowIndex);
                // dgv.Rows.Insert(, 1);
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
                dgv = dgv_tabla_grafica;
               
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

        private void cmdRowDel_Click(object sender, EventArgs e)
        {
            DataGridView dgv;

            try
            {
                dgv = dgv_tabla_grafica;

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

        private void buttonItem67_Click(object sender, EventArgs e)
        {
          

            try
            {
                 DataTable t = (DataTable)dgv_tabla_grafica.DataSource;
               

               Maps.mainClasificador EmpezamosAclsificar2 = new Maps.mainClasificador( t.Copy(), Notificacion);
                EmpezamosAclsificar2.Show();

            }
            catch (Exception ex)
            {


            }
        }

        private void cmd_Filtrar_Click(object sender, EventArgs e)
        {
            //aqui mandamos a filtar los valores.
            cmd_Filtrar.Checked = true;
            DataGridView dgv;

            try
            {
                dgv = dgv_tabla_grafica;
                DataTable t = (DataTable)dgv.DataSource;
                filtratabla filtro = new filtratabla(t, Notificacion);
                
                if (filtro != null)
                    filtro.ShowDialog();
                t.AcceptChanges();
                 // UsandoAutoFiltro usafiltro = new UsandoAutoFiltro( t);
                //  usafiltro.ShowDialog(this);

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
                dgv = dgv_tabla_grafica;
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

              
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }

        private void cmdReemplazarPorColumna_Click(object sender, EventArgs e)
        {
            //DataGridView dgv;

            //try
            //{
            //    dgv = dgv_tabla_grafica;
            //    DataTable t = (DataTable)dgv.DataSource;
            //    int dolactual = dgv.CurrentCell.ColumnIndex;
            //    //vamos a crear una lista
            //    userReemplazar2 reem = new userReemplazar2(t, dolactual,Notificacion);
            //    // AutoCompleteStringCollection x = new AutoCompleteStringCollection();
            //    //x.AddRange(Core_produccion.Unidad.RetornaPozos().ToArray());
            //    //reem.AplicaOrigen(x);
            //    reem.EnForm();

            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);

            //}
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBox1.Text.Length > 0)
                Buscar();
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


                dgv_actual = dgv_tabla_grafica;
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

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            //DataGridView dgv;

            //try
            //{
            //    dgv = dgv_tabla_grafica;
            //    DataTable t = (DataTable)dgv.DataSource;
            //    if (dgv.CurrentCell == null)
            //        MessageBox.Show("Seleccione una celda de la columna que desea rellenar!!!");
            //    else
            //    {
            //        int dolactual = dgv.CurrentCell.ColumnIndex;
            //        //vamos a crear una lista
            //     userReemplazar2    userremp = new userReemplazar2 (t, dolactual, Notificacion);

            //        // AutoCompleteStringCollection x = new AutoCompleteStringCollection();
            //        //x.AddRange(Core_produccion.Unidad.RetornaPozos().ToArray());
            //        //reem.AplicaOrigen(x);
            //        userremp.EnForm();
            //    }

            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);

            //}
        }

        private void Visor_Datos_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                Close();
        }

        private void buttonItem47_Click(object sender, EventArgs e)
        {
           
            //DataGridView dgv;

            //try
            //{
            //    dgv = dgv_tabla_grafica;
            //    DataTable t = (DataTable)dgv.DataSource;
            //    if (t != null)
            //    { 
            //     BusquedadeDatos bd = new BusquedadeDatos(t);
            //bd.ShowDialog();
                
            //    }
                   

            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);

            //}
        }
       


    }
}
