using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Maps
{
    public partial class Clasificadorxfecha : UserControl
    {
       public List<InterevalosFecha> Lista;
        public bool existe=false;
        public int Cerrar = 0;
        public Clasificadorxfecha(ref List<InterevalosFecha> Lista)
        {
            InitializeComponent();
            this.Lista = Lista;
        }

        private void Clasificadorxfecha_Load(object sender, EventArgs e)
        {
            dgv_inicio.RowHeadersVisible = false;
            dgv_fin.RowHeadersVisible = false;

            Operadores.Items.Clear();
            string[] nombre = new string[] { "<", ">=", "=", "!=" };



            Operadores.Items.AddRange(nombre);

           operador2.Items.Clear();


           operador2.Items.AddRange(nombre);

            dgv_inicio.Rows.Add();
            dgv_inicio.Rows[0].Cells[0].Value = "<";
            dgv_inicio.Rows[0].Cells[1].Value = DateTime.Now.ToShortDateString();


            dgv_fin.Rows.Add();
            dgv_fin.Rows[0].Cells[0].Value = "<";
            dgv_fin.Rows[0].Cells[1].Value = DateTime.Now.ToShortDateString();


           
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            dgv_fin.Enabled=checkBoxX1.Checked  ;
            AplicaCambios();
        }

        private void dataGridViewX2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //aqui para la segunda.
          //  Valida(dataGridViewX2);
           //colocar los valosres
            Valores_text();
        }

        private void dataGridViewX1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //aqui para la primera.
          //  Valida(dataGridViewX1);
            Valores_text();
        }

        void Valores_text()
        {     string opera2 ="",opera1="";  
            DateTime FechaA= DateTime.Now,FechaB=DateTime.Now;
            if (checkBoxX1.Checked)
            {
                //para un solo segundo Valor sera
              opera2  = dgv_fin.Rows[0].Cells[0].Value.ToString();
              FechaB  = Convert.ToDateTime(dgv_fin.Rows[0].Cells[1].Value);
            }

            
            //para un solo valor sera
               opera1 = dgv_inicio.Rows[0].Cells[0].Value.ToString();
              FechaA=  Convert.ToDateTime( dgv_inicio.Rows[0].Cells[1].Value);
              if (checkBoxX1.Checked)
                  labelX2.Text = "Valor " + " " + opera1 + " " + FechaA.ToShortDateString() + " y Valores " + opera2 + " " + FechaB.ToShortDateString();

              else
                  labelX2.Text = "Valor " + " " + opera1 + " " + FechaA.ToShortDateString();

          AplicaCambios();  
        }

        void Valida(DataGridView dgv)
        {    
            for (int i = 0; i < dgv.Rows.Count ; i++)
            {
                try
                {
                    if (!IsNumeric(dgv[1, i].Value.ToString()) && dgv[0, i].Value != null)
                    {
                        dgv.Rows.Remove(dgv.Rows[i]);
                        i--;
                    }
                }
                catch { }
            }
        }
        private bool IsNumeric(string number)
        {
            if (number.Length < 1)
                return false;
           // Regex pattern = new Regex(@"^(-)?[0-9]*(.)?[0-9]*$");//(@"^(-)?[\d]?([.]+(\d))?$");//("[^0-9][.]?[^0-9]");
            Regex pattern = new Regex("^(0?[1-9]|1[0-9]|2|2[0-9]|3[0-1])/(0?[1-9]|1[0-2])/(d{2}|d{4})$");

            
         
            return pattern.IsMatch(number);
        }

        private void cmdAplicar_Click(object sender, EventArgs e)
        {
            AplicaCambios();
        }
       public void  AplicaCambios()
    {
            Lista.Clear();
            //como ya se que todos los reglones que quedan son numero ahora generare la lista de Valores por la cual estare selecioanndo;
            for (int i = 0; i < dgv_inicio.Rows.Count ; i++)
            {
                try
                {
                    Lista.Add(new InterevalosFecha(Convert.ToDateTime(dgv_inicio[1, i].Value), dgv_inicio[0, i].Value.ToString()));
                }
                catch { }

            }
            if (checkBoxX1.Checked)
            {       //si esta marcado la segunda fecha
                for (int i = 0; i < dgv_fin.Rows.Count; i++)
                {
                    try
                    {
                        Lista.Add(new InterevalosFecha(Convert.ToDateTime(dgv_fin[1, i].Value), dgv_fin[0, i].Value.ToString()));
                    }
                    catch { }

                }
            }

            if (Lista.Count > 0)
                existe = true;
            if (Cerrar == 0)
            {
                Form padre = (Form)this.Parent;
                padre.Close();
            }
        }

        public void EnForm()
        {
            Form fm = new Form();

            fm.MaximizeBox = false;
            fm.MinimizeBox = false;
            fm.Controls.Add(this);
            fm.StartPosition = FormStartPosition.CenterParent;
            this.Dock = DockStyle.Fill;

            fm.ShowDialog();
        }

        private void labelX2_Click(object sender, EventArgs e)
        {

        }



    }
}
