using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Maps
{
    public partial class Editor_de_Columnas : Form
    {
        DataTable Tabla;
        object Objetoenediccion;
        DataGridView DGV;

        private Point point;
        public Editor_de_Columnas(DataTable Temp,    DataGridView DGV_)
        {
            InitializeComponent();
            Tabla = Temp;
            DGV = DGV_;
        }

        public Editor_de_Columnas(DataTable Temp, Point point,    DataGridView DGV_)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            Tabla = Temp;
            this.point = point;
            DGV = DGV_;
        }
        public bool RowEdit
        {
            get;
            set;
        }
        public bool EditarNew
        {
            get;
            set;
        }
        public bool EditarDel
        {
            get;
            set;
        }
       List<DataColumn> x = new List<DataColumn>();
        private void Editor_de_Columnas_Load(object sender, EventArgs e)
        {
            dgv_editor.Rows.Clear();
            //mandamos a ver los nombres.
            if (point == null)
            {
                foreach (DataColumn columna in Tabla.Columns)
                {
                    DataGridViewRow reglon = new DataGridViewRow();
                    reglon.CreateCells(dgv_editor);


                    reglon.Cells[0].Value = columna.Caption;
                    reglon.Cells[0].Tag = columna;
                    dgv_editor.Rows.Add(reglon);
                }
            }
            else
            {
         
                x.Add (Tabla.Columns[point.X]);
                x.Add (Tabla.Columns[point.Y]);

                foreach (DataColumn columna in Tabla.Columns)
                {
                    
                        DataGridViewRow reglon = new DataGridViewRow();
                        reglon.CreateCells(dgv_editor);


                        reglon.Cells[0].Value = columna.Caption;
                        reglon.Cells[0].Tag = columna;
                        dgv_editor.Rows.Add(reglon);
                    
                }
            }

            textBox1.Text = Tabla.TableName;
            this.Text = "Editar Columnas: " + Tabla.TableName;
            dgv_editor.AllowUserToAddRows = EditarNew;
            dgv_editor.AllowUserToDeleteRows = EditarDel;
            dgv_editor.ReadOnly = !RowEdit;
            EventosDGv evento = new EventosDGv(dgv_editor, new NotifyIcon());
            evento.IsOrigen = false;

        }

        private void dataGridViewX1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_editor[e.ColumnIndex, e.RowIndex].Tag != null && dgv_editor[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    DataColumn col = (DataColumn)dgv_editor[e.ColumnIndex, e.RowIndex].Tag;
                    string val = dgv_editor[e.ColumnIndex, e.RowIndex].Value.ToString();
                    if (!Tabla.Columns.Contains(val))
                    {
                        col.Caption = val;
                        col.ColumnName = val;
                    }
                    else
                    {
                        dgv_editor[e.ColumnIndex, e.RowIndex].Value = col.ColumnName;
                    }
                }
                else
                {
                    //implica que no hay esa columna
                    string cadena = dgv_editor[e.ColumnIndex, e.RowIndex].Value.ToString();
                    if (cadena.Length > 0)
                    {
                        if (!Tabla.Columns.Contains(cadena))
                        {
                            

                            DataColumn inser = new DataColumn(cadena);
                            Tabla.Columns.Add(inser); 
                            dgv_editor[e.ColumnIndex, e.RowIndex].Tag = inser;
                              int   Posicion = 0;
                            for (int i = 0; i < dgv_editor.Rows.Count; i++)
                            {
                                if (Posicion == point.X)
                                    Posicion++;
                                    if(Posicion == point.Y)
                                   Posicion++;
                                

                                {
                                    DataColumn temp = Tabla.Columns[dgv_editor.Rows[i].Cells[0].Value.ToString()];
                                    temp.SetOrdinal(Posicion);
                                    DGV.Columns[temp.ColumnName].DisplayIndex = Posicion; 
                                    Posicion++;
                                }
                                
                                 
                                


                            }
                          

                        }
                        else
                        {
                            MessageBox.Show(Error(1));
                        }
                    }
                }

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                dgv_editor[e.ColumnIndex, e.RowIndex].Value = Objetoenediccion;
            }
        }

        string Error(int i)
        {
            switch (i)
            {
                case 1: return "Esta Columna ya existe!!!";
            }
            return "Desconocido";

        }
        private void dataGridViewX1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Objetoenediccion = dgv_editor[e.ColumnIndex, e.RowIndex].Value;
        }

        private void dataGridViewX1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
          

        }

        private void dataGridViewX1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {

                if (EditarDel)
                {
                    DataColumn Col = (DataColumn)e.Row.Cells[0].Tag;
                    //  MessageBox.Show("Eliminado: "+Col.ColumnName);
                    Tabla.Columns.Remove(Col);

                }



            }
            catch { }

        }

        private void dataGridViewX1_KeyUp(object sender, KeyEventArgs e)
        {
            if (EditarNew)
            {
                DataGridView dgv = (DataGridView)sender;
                int celda = dgv.CurrentCell.RowIndex;

                if (e.Control == true && (e.KeyValue == 187))//|| e.KeyValue == 187))
                {
                    try
                    {
                        dgv.Rows.Insert(celda, 1);
                    }
                    catch
                    {

                    }
                }
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length > 0)
                Tabla.TableName = textBox1.Text;
        }

        private void Editor_de_Columnas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dgv_editor.IsCurrentCellDirty)
                dgv_editor.CommitEdit(DataGridViewDataErrorContexts.Commit);

        }
    }
}
