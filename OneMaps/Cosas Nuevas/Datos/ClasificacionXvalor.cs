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
    public partial class ClasificacionXvalor : UserControl
    {
        public bool existe = false;
        List<InterevalosCondiciones> Lista;
        public ClasificacionXvalor(List<InterevalosCondiciones> Lista)
        {
            InitializeComponent();
             this. Lista = Lista;
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

        private void ClasificacionXvalor_Load(object sender, EventArgs e)
        {
            
            label1.Text = "Marque los intervalos de datos para clasificarlos de la Columna:" + Text;
            Operadores.Items.Clear();
            string[] nombre = new string[] { "<", ">=", "=", "!=" };

            Operadores.Items.AddRange(nombre);
            //ahora visualizaremos lo que tengo cargado
            foreach (InterevalosCondiciones inter in Lista)
            {
                dataGridViewX1.Rows.Add();
                dataGridViewX1.Rows[dataGridViewX1.Rows.Count - 2].Cells[0].Value = inter.Operador;
                dataGridViewX1.Rows[dataGridViewX1.Rows.Count - 2].Cells[1].Value = inter.Cantidad;
            }
        }

       
        bool Validar()
        {
            for (int i = 0; i < dataGridViewX1.Rows.Count - 1; i++)
            {
                if (!IsNumeric(dataGridViewX1[1, i].Value.ToString()) && dataGridViewX1[0, i].Value!=null)
                {
                    dataGridViewX1.Rows.Remove(dataGridViewX1.Rows[i]);
                    i--;
                 
                }
            }
            return true;
         

        }
        private bool IsNumeric            (string number)
        {


            number = number.Replace(',', ' ');
            while (number.Contains(' '))
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
            try
            {
                return Convert.ToDouble(number);
            }
            catch { }
            return 0;
            //  return pattern.IsMatch(number.Trim());
        }

        private void dataGridViewX1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
           // dataGridViewX1[0, e.RowIndex].Value = "<";
        }

        private void cmdAplicar_Click(object sender, EventArgs e)
        {
            Aplicar();
        }
        void Aplicar()
        {
            if (Validar())
            {
                Lista.Clear();
                //como ya se que todos los reglones que quedan son numero ahora generare la lista de Valores por la cual estare selecioanndo;
                for (int i = 0; i < dataGridViewX1.Rows.Count - 1; i++)
                {
                    try
                    {
                        Lista.Add(new InterevalosCondiciones(Numero(dataGridViewX1[1, i].Value.ToString()), dataGridViewX1[0, i].Value.ToString()));
                    }
                    catch { }

                }

                if (Lista.Count > 0)
                    existe = true;

                if (Cerrar == 0)
                {
                    Form Padre = ParentForm;
                    Padre.Close();
                }
            }
        }
        public int Cerrar = 0;


        internal void Actualiza(List<InterevalosCondiciones> Lista)
         
            
        {
            
            this. Lista = Lista;
           //si actualzia volvemos a reeler los valores
            //ahora visualizaremos lo que tengo cargado
            dataGridViewX1.Rows.Clear();
            foreach (InterevalosCondiciones inter in Lista)
            {
                dataGridViewX1.Rows.Add();
                dataGridViewX1.Rows[dataGridViewX1.Rows.Count - 2].Cells[0].Value = inter.Operador;
                dataGridViewX1.Rows[dataGridViewX1.Rows.Count - 2].Cells[1].Value = inter.Cantidad;
            }
        }

        private void dataGridViewX1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
             // cuando se termine de editar la segunda celda
            if(e.ColumnIndex== 1 && dataGridViewX1.Rows.Count >0)
            Aplicar();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }//class

    public class InterevalosCondiciones
    {
        double Valor;
        string operador;
        public InterevalosCondiciones(double Val, string Oper)
        {
            Valor = Val;
            operador = Oper;
        }
        /// <summary>
        /// el valor de la Comparacion
        /// </summary>
        public double Cantidad
        {
            get { return Valor; }
            set { Valor = value; }
        }
        /// <summary>
        ///  Si es "<",>,!=,=
        /// </summary>
        public string Operador
        {
            get { return operador; }
            set { operador = value; }
        }
        public override string ToString()
        {
            return  operador + " "+Valor.ToString();

        }
    }

    public class InterevalosFecha
    {
        DateTime FechaA;
       
        string operador;
        public InterevalosFecha(DateTime Val, string Oper)
        {
           FechaA = Val;
            operador = Oper;
        }
        
        /// <summary>
        /// el valor de la Comparacion
        /// </summary>
        public DateTime Fecha
        {
            get { return FechaA; }
            set { FechaA = value; }
        }
        /// <summary>
        ///  Si es "<",>,!=,=
        /// </summary>
        public string Operador
        {
            get { return operador; }
            set { operador = value; }
        }
        public override string ToString()
        {
            return operador + " " + FechaA.ToShortDateString();

        }
    }

}//name espace
