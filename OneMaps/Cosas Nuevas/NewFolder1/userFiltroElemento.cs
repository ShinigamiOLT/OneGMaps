using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Maps
{
    public partial class userFiltroElemento : UserControl
    {
        int iD = 0; //0.- Normal 1.- Control listbox
        public bool cerrado = false;

        #region variable dela tabla
        public List<ObjetoSelecionable> ListaPozosTotales;
        public ObjetoSelecionable Seleccionado;
        public int Cerrar = 0; //0.- si 1.- no

        //public List<ClasificacionOne.InterevalosCondiciones> ListaIntervalos = new List<ClasificacionOne.InterevalosCondiciones>();
        //public List<ClasificacionOne.InterevalosFecha> ListaFechas = new List<ClasificacionOne.InterevalosFecha>();
        //ClasificacionOne.ClasificacionXvalor porvalor;
        //ClasificacionOne.Clasificadorxfecha porfecha;
        public int ControlActual = 0; //o es por elemento 1.- es por valor 2.- es por facha.
        public Form formulario;
        #endregion
        /// <summary>
        /// Numero de Elementos
        /// </summary>
        public int Count
        {
            get
            {
                if (ListaPozosTotales == null)
                    return -1;
                return ListaPozosTotales.Count;
            }
        }
        //Aqui es para actualizar la lista de Valores.

        public void NuevaLista(ref List<ObjetoSelecionable> Lista)
        {

            ListaPozosTotales = Lista;
            Recargar();
        }
        //resibiendo un list<string>
        public void NuevaLista(List<string> Listas)
        {
            List<ObjetoSelecionable> Lista = new List<ObjetoSelecionable>();
            foreach (string s in Listas)
            {
                Lista.Add(new ObjetoSelecionable(s));
            }
            ListaPozosTotales = Lista;
            Recargar();
        }

        public userFiltroElemento(int ID)
        {
            InitializeComponent();
            iD = ID;
            //porvalor = new ClasificacionOne.ClasificacionXvalor(ListaIntervalos);
            //porvalor.Cerrar = 1;
            //porvalor.cmdAplicar.Visible = false;
            //porfecha = new ClasificacionOne.Clasificadorxfecha(ref ListaFechas);
            //porfecha.Cerrar = 1;
            //porfecha.cmdAplicar.Visible = false;
            ControlActual = 0;
        }
        public userFiltroElemento()
        {
            InitializeComponent();
            iD = 0;
            //porvalor = new ClasificacionOne.ClasificacionXvalor(ListaIntervalos);
            //porvalor.Cerrar = 1;
            //porvalor.cmdAplicar.Visible = false;
            //porfecha = new ClasificacionOne.Clasificadorxfecha(ref ListaFechas);
            //porfecha.Cerrar = 1;
            //porfecha.cmdAplicar.Visible = false;
            ControlActual = 0;
        }

        public userFiltroElemento(ref List<ObjetoSelecionable> Lista, int ID)
        {
            InitializeComponent();
            ListaPozosTotales = Lista;
            iD = ID;
        }
        public userFiltroElemento(ref List<ObjetoSelecionable> Lista)
        {
            InitializeComponent();
            ListaPozosTotales = Lista;
            iD = 0;
            //porvalor = new ClasificacionOne.ClasificacionXvalor(ListaIntervalos);
            //porvalor.Cerrar = 1;
            //porvalor.cmdAplicar.Visible = false;
            //porfecha = new ClasificacionOne.Clasificadorxfecha(ref ListaFechas);
            //porfecha.Cerrar = 1;
            //porfecha.cmdAplicar.Visible = false;
            ControlActual = 0;
        }
        public void Desmarcar(bool sentido)
        {
            foreach (ObjetoSelecionable obj in ListaPozosTotales)
            {
                obj.Estado = sentido;
            }
        }
        public void EnForm(bool libre)
        {

            if (!cerrado)
            {
                Form fm = new Form();
                formulario = fm;
                fm.Size = new System.Drawing.Size(265, 634);
                fm.MaximizeBox = false;
                fm.MinimizeBox = false;
                fm.Controls.Add(this);
                fm.StartPosition = FormStartPosition.CenterScreen;


                //  CreaBoton(fm);
                panel_aceptar.Visible = true;
                fm.AcceptButton = cmdAceptar;
                fm.TopMost = true;

                cmdAceptar.Tag = fm;
                fm.ShowInTaskbar = false;
                fm.ShowIcon = false;
                this.Dock = DockStyle.Fill;
                if (this.Tag != null)
                {
                    DevComponents.DotNetBar.Controls.ComboBoxEx combopozo = (DevComponents.DotNetBar.Controls.ComboBoxEx)this.Tag;
                    formulario.Location = new Point((this.PointToScreen(combopozo.Location)).X, (combopozo.PointToScreen(combopozo.Location)).X);//;78, 50);
                }

                fm.FormClosing += (o, e) =>
                    {
                        cerrado = true;
                    };

                if (libre)
                    fm.Show();
                else fm.ShowDialog();
            }

        }

        public void Actualiza(List<ObjetoSelecionable> aux, int ID)
        {
            ListaPozosTotales = aux;
            BuscarPalabra();
            iD = ID;
        }

        public void EnForm2()
        {


            if (formulario == null)
            {
                formulario = new Form();
                formulario.Load += new EventHandler(formulario_Load);
            }
            formulario.Size = new System.Drawing.Size(265, 634);
            formulario.MaximizeBox = false;
            formulario.MinimizeBox = false;
            formulario.Controls.Add(this);
            formulario.StartPosition = FormStartPosition.CenterParent;

            //  CreaBoton(fm);
            panel_aceptar.Visible = true;
            formulario.AcceptButton = cmdAceptar;

            cmdAceptar.Tag = formulario;
            formulario.ShowInTaskbar = false;
            formulario.ShowIcon = false;
            this.Dock = DockStyle.Fill;
            formulario.TopLevel = true;

            formulario.Show();

        }

        void formulario_Load(object sender, EventArgs e)
        {
            BuscarPalabra();
        }
        public void CreaBoton(Form fm)
        {
            // cuando es tipo form trataremos de crear el control
            this.Dock = DockStyle.Fill;
            Button boton = new Button();
            boton.Size = new System.Drawing.Size(100, 30);
            boton.FlatStyle = FlatStyle.Flat;
            this.Controls.Add(boton);
            Lista_eventos.Size = new System.Drawing.Size(Lista_eventos.Size.Width, Lista_eventos.Size.Height - 30);
            Marcador.Location = new Point(Marcador.Location.X, Marcador.Location.Y - 35);

            // boton.Location = new Point(Marcador.Location.X  , Marcador.Location.Y+7);
            boton.Location = new Point(((int)fm.Size.Width / 2) - 50, Marcador.Location.Y + 20);
            boton.Anchor = AnchorStyles.Bottom;
            boton.Text = "&Aceptar";
            boton.Click += new EventHandler(boton_Click);
            fm.AcceptButton = boton;
        }
        void boton_Click(object sender, EventArgs e)
        {
            if (iD == 1)
            {



                if (Lista_Unica.SelectedIndex == -1)
                {
                    Seleccionado = new ObjetoSelecionable("");
                }

            }

            try
            {
                Form papa = (Form)cmdAceptar.Tag;
                papa.Close();
                cerrado = false;
            }
            catch { }
        }

        private void userFiltroElemento_Load(object sender, EventArgs e)
        {
            Recargar();
            Seleccionado = new ObjetoSelecionable("");
            switch (iD)
            {
                case 0:
                    Lista_Unica.Hide();
                    Lista_eventos.Show();
                    break;
                case 1:
                    Lista_Unica.Show();
                    Lista_eventos.Hide();
                    Marcador.Hide();
                    BuscarPalabra();
                    break;
            }

            cmbBuscar.Focus();

        }
        void Recargar()
        {
            if (ListaPozosTotales != null)
            {
                switch (iD)
                {
                    case 0:

                        Lista_eventos.Items.Clear();

                        foreach (ObjetoSelecionable cadena in ListaPozosTotales)
                        {
                            if (cadena.Visible)
                                Lista_eventos.Items.Add(new ItemsPersonal(cadena), cadena.Estado);
                        }
                        Lista_eventos.Refresh();
                        break;
                    case 1:
                        Lista_Unica.Items.Clear();

                        foreach (ObjetoSelecionable cadena in ListaPozosTotales)
                        {
                            if (cadena.Visible)
                                Lista_Unica.Items.Add(new ItemsPersonal(cadena));
                        }
                        break;
                }
                AutoCompleteStringCollection x = new AutoCompleteStringCollection();
                x.AddRange(Valores().ToArray());
                cmbBuscar.AutoCompleteCustomSource = x;
                //   if (ListaPozosTotales.Count > 0)
                //     Marcador.Checked = ListaPozosTotales[0].Estado;
            }
        }

        private void Marcador_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Lista_eventos.Items.Count; i++)
            {
                // ItemsPersonal elemento = Lista_eventos.Items[i] as ItemsPersonal;
                // elemento.objeto.Estado = Marcador.Checked;
                Lista_eventos.SetItemChecked(i, Marcador.Checked);

            }

        }
        List<string> Valores()
        {
            List<string> Lista = new List<string>();

            if (ListaPozosTotales != null)
            {
                foreach (ObjetoSelecionable cadena in ListaPozosTotales)
                {
                    if (cadena.Visible)
                    {
                        Lista.Add(cadena.ToString());
                    }
                }
            }
            else
                Lista.Add("Vacio");
            return Lista;
        }

        public void BuscarPalabra()
        {
            string Palabra = cmbBuscar.Text.Trim().ToUpper();
            // aqui tengo el texto que busco.
            Text = Palabra;
            Lista_Unica.SuspendLayout();
            // ahora tratare de buscar los elementos.
            if (ListaPozosTotales == null)
                return;
            switch (iD)
            {
                case 0:
                    Lista_eventos.Items.Clear();

                    if (Palabra.Length > 0)
                        foreach (ObjetoSelecionable cadena in ListaPozosTotales)
                        {
                            if (cadena.Visible == true && EliminaAcentos(cadena.ToString().ToUpper()).Contains(Palabra))
                            {
                                Lista_eventos.Items.Add(new ItemsPersonal(cadena), cadena.Estado);
                            }
                        }
                    else     //si esta vacio
                        foreach (ObjetoSelecionable cadena in ListaPozosTotales)
                        {
                            if (cadena.Visible)
                            {
                                Lista_eventos.Items.Add(new ItemsPersonal(cadena), cadena.Estado);
                            }

                        }
                    Lista_eventos.Visible = true;
                    Lista_Unica.Visible = false;
                    break;
                case 1:
                    Lista_Unica.Items.Clear();

                    if (Palabra.Length > 0)
                        foreach (ObjetoSelecionable cadena in ListaPozosTotales)
                        {
                            if (cadena.Visible && EliminaAcentos(cadena.ToString().ToUpper()).Contains(Palabra))
                            {
                                Lista_Unica.Items.Add(new ItemsPersonal(cadena));
                            }
                        }
                    else     //si esta vacio
                        foreach (ObjetoSelecionable cadena in ListaPozosTotales)
                        {
                            if (cadena.Visible)
                            {
                                Lista_Unica.Items.Add(new ItemsPersonal(cadena));
                            }

                        }
                    Lista_eventos.Visible = false;
                    Lista_Unica.Visible = true;
                    break;
                    Lista_Unica.ResumeLayout();
            }


            //para ver si exite algo que marcar.
            //    if (Lista_eventos.Items.Count > 0)
            //      Marcador.Checked= Lista_eventos.GetItemChecked(0);
        }

        private void cmbBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarPalabra();
        }
        public static string EliminaAcentos(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            byte[] tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(texto);
            return System.Text.Encoding.UTF8.GetString(tempBytes);
        }

        private void Lista_eventos_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ItemsPersonal objeto = Lista_eventos.Items[e.Index] as ItemsPersonal;
            if (e.NewValue == CheckState.Checked)
                objeto.objeto.Estado = true;
            else
                objeto.objeto.Estado = false;
            if (objeto.objeto.objeto.GetType().Name.Contains("DataGridView"))
            {
                ObjetoSelecionable obj = objeto.objeto;
                DataGridViewColumn columna = ((DataGridViewColumn)objeto.objeto.objeto);
                columna.Visible = objeto.objeto.Estado;
                //veamos que pasa 
                if (columna.Tag != null)
                {
                    Control con = (Control)columna.Tag;
                    con.Visible = objeto.objeto.Estado;
                }
            }

        }

        private void Lista_Unica_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Lista_Unica.SelectedItem != null)
                {

                    ItemsPersonal objeto = Lista_Unica.SelectedItem as ItemsPersonal;

                    Seleccionado = objeto.objeto;
                    Form papa = (Form)this.Parent;
                    papa.Close();
                    cerrado = false;

                    //  DevComponents.DotNetBar.Controls.ComboBoxEx combopozo = (DevComponents.DotNetBar.Controls.ComboBoxEx)this.Tag;
                    // combopozo.Text = objeto.objeto.ToString();

                }
            }
            catch
            {

            }


        }
        private void Lista_eventos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdTipos_Click(object sender, EventArgs e)
        {
            //aqui sera para los tipos.
            this.Controls.Clear();
            this.Controls.Add(Panel_Elementos);
            this.Controls.Add(PanelTipo);
            ControlActual = 0;
        }

        private void cmdValor_Click(object sender, EventArgs e)
        {
            //aqui para valor
            this.Controls.Clear();
            //this.Controls.Add(porvalor);
            //porvalor.Dock = DockStyle.Top;

            this.Controls.Add(PanelTipo);
            PanelTipo.Dock = DockStyle.Top;
            ControlActual = 1;


        }

        private void cmdfechas_Click(object sender, EventArgs e)
        {
            //aqui para valor
            this.Controls.Clear();
            //this.Controls.Add(porfecha);
            //porfecha.Dock = DockStyle.Top;


            this.Controls.Add(PanelTipo);
            PanelTipo.Dock = DockStyle.Top;
            ControlActual = 2;


        }
    }

    //para poder meter los elmentos en el listbox
    class ItemsPersonal
    {
        public ObjetoSelecionable objeto;
        public ItemsPersonal(ObjetoSelecionable ob)
        {
            objeto = ob;
        }

        public override string ToString()
        {
            return objeto.ToString();
        }

    }
    //objeto de tipo selecionable
    public class ObjetoSelecionable
    {
        public object objeto;
        bool estado;
        bool visible;
        PointF coordenada;

        public Color Color_
        {
            get;
            set;
        }
        public ObjetoSelecionable(object obj)
        {

            objeto = obj;
            estado = true;
            visible = true;
            Color_ = Color.Red;

        }
        public PointF Coord
        {
            get
            {
                return coordenada;
            }
            set
            {
                coordenada = value;
            }
        }
        public int Index
        {
            get;
            set;
        }
        public ObjetoSelecionable(object obj, bool est)
        {

            objeto = obj;
            estado = est;
            visible = true;
            Color_ = Color.Red;
            coordenada = new PointF(-1, -1);
        }
        public ObjetoSelecionable(object obj, bool est, int index)
        {

            objeto = obj;
            estado = est;
            visible = true;
            Color_ = Color.Red;
            Index = index;
            coordenada = new PointF(-1, -1);
        }

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }

        }
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }

        }
        public override string ToString()
        {
            if (objeto.GetType().Name.Contains("DataGridView"))
                return (((DataGridViewColumn)objeto).Name);
            return objeto.ToString().ToUpper();
        }
        //public override bool Equals(object obj)
        //{
        //    if (objeto == obj)
        //        return true;
        //    return false;
        //}
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public bool Igual(object obj)
        {
            try
            {
                if (objeto.ToString() == obj.ToString())
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
    }

    //esta es una clase para almacenar los valores.
    class MyPoint
    {
        /// <summary>
        /// Funcion que devuelve los valroes X,Y en un arreglo
        /// </summary>
        /// <returns></returns>
        public double[] ToArray()
        {
            return new double[] { X, Y };
        }
        public MyPoint(double x, double y)
        {
            X = x;
            Y = y;

        }
        public MyPoint(MyPoint PointTemp)
        {
            X = PointTemp.X;
            Y = PointTemp.Y;

        }
        public MyPoint()
        {
            X = Y = 0;

        }
        public double X
        {
            get;
            set;
        }
        public double Y
        {
            get;
            set;
        }

        public override string ToString()
        {
            return (DateTime.FromOADate(X).ToString() + " : " + Math.Round(Y, 3).ToString());
        }
        public string ToString2()
        {
            return ((X).ToString() + " : " + Math.Round(Y, 3).ToString());
        }
    }
    class cDeclinacion
    {
        public DataTable Tabla;
        public double D = 0;
        public double Di = 0;
        public double Dn = 0;
        public double DD = 0;
        public double M = 0;
        public double Fraccion = 30;
        public double D_Pro
        {
            get { return Math.Round(D * 100 * Fraccion, 5); }
        }
        public double M_Pro
        {
            get { return Math.Round(M * Fraccion, 5); }
        }
        public string Hiper_Pro
        {
            get { return "D: " + Dn.ToString() + " Di: " + DD.ToString(); }
        }
        public string Armo_pro
        {
            get { return "D: " + Di.ToString(); }
        }
        public MyPoint Rango
        {
            get;
            set;
        }
        public void Actualiza(DataTable t)
        {

            Tabla.Clear();
            //antes de meter datos lo que haremos es ordenarlos

            var query = from a in t.AsEnumerable().Where(p => p != null) orderby Convert.ToDateTime(a[0]) ascending select a;

            Tabla.Merge(query.CopyToDataTable());
        }

        public string Estado()
        {
            return "M: " + M.ToString() + " D: " + D.ToString() + "\nDD " + DD.ToString() + " N: " + Dn.ToString();
        }
        public cDeclinacion()
        {

        }
        public cDeclinacion(DataTable t)
        {
            Tabla = t.Copy();
            // cuando creamos esta tabla ageragremos los valores que se necesitan 
            // T, DG;
            Tabla.Columns.Add("T");
            Tabla.Columns.Add("Exponencial");
            Tabla.Columns.Add("Lineal");
            Tabla.Columns.Add("Hiperbolica");
            Tabla.Columns.Add("Armonica");
            Rango = new MyPoint(0, 0);
            Dn = 0.77;
            DD = 0.12;
            Fraccion = 1;

        }
        void LimpiaDatos()
        {
            foreach (DataRow reg in Tabla.Rows)
            {
                reg[2] = reg[3] = reg[4] = reg[5] = reg[6] = "";
            }
        }
        InterpolacionLIneal CInterLineal;
        public void Coeficiente()
        {
        }
        public double Acumulada = 0;
        /*  public void CoeficienteE()
          {
              // chequemos si estan bien 
              if (Rango.X > Rango.Y)
              {
                  double tem = Rango.X;
                  Rango.X = Rango.Y;
                  Rango.Y = tem;
              }
              LimpiaDatos();
              //cortemos de la tabla el rango;
          
              double Qo = 0, Qf = 0;
              double T0 = 0 ;
              double Dias = 0;
              DateTime Inicial = DateTime.Now;
              /*  vd.EstableceDatos(Tabla);
                vd.Text = Rango.ToString2();
                vd.Rapida(false);* /
              IEnumerable<DataRow> Query2 = from a in Tabla.AsEnumerable()
                                            where (Convert.ToDouble(a[0]) >= Rango.X)
                                            select a;


              // vd.EstableceDatos(Query2.CopyToDataTable());
              // vd.Rapida(false);
              if (Query2.Count() > 0)
              {
                  Inicial = DateTime.FromOADate(Convert.ToDouble(Query2.ElementAt(0)[0]));

                  //empezemos calculando la diferencia;
                  foreach (DataRow reg in Query2)
                  {
                      reg[2] = (DateTime.FromOADate(Convert.ToDouble(reg[0])) - Inicial).TotalDays;
                  }

              }
              //vd.EstableceDatos(Query2.CopyToDataTable());
              //vd.Rapida(false);
              IEnumerable<DataRow> Query = from a in Tabla.AsEnumerable()
                                           where (Convert.ToDouble(a[0]) >= Rango.X &&
                                                   Convert.ToDouble(a[0]) <= Rango.Y)
                                           select a;


              int cont = Query.Count();
              if (cont > 0)
              {

                  Qo = Convert.ToDouble(Query.ElementAt(0)[1]); //qo
                  Qf = Convert.ToDouble(Query.ElementAt(cont - 1)[1]);//qf
                  Dias = Convert.ToDouble(Query.ElementAt(cont - 1)[2]);// t en qf
                  //si obengo las datos ps hare que tome la pendiente que necsito 
                  T0 = Convert.ToDouble(Query.ElementAt(0)[2]);
                  CInterLineal = new InterpolacionLIneal(T0, Qo, Dias, Qf);
                

              }

              CInterLineal.M = M;

              double qe = 0;



              // despues de la declinacion empezaremos a caluar todos los valores de la neva DG;
              foreach (DataRow reg in Query2)
              {
                  //DG = Qo *  exp ( - D * T ));
                  //   Qo = Convert.ToDouble(reg[1]);
                  Dias = Convert.ToDouble(reg[2]);

                  qe = Qo * Math.Exp(-D * Dias);
                  reg[3] = qe;

                  //aqui evaluamos la lineal
                  reg[4] = CInterLineal.Evalua2(Dias);
              }

              DataTable temp = Tabla.Copy();
              foreach (DataRow reg in temp.Rows)
              {
                  reg[0] = DateTime.FromOADate(Convert.ToDouble(reg[0]));
              }


          }
  */
        class InterpolacionLIneal
        {

            double X1;
            double X2;

            double fX1;
            double fX2;
            /// <summary>
            /// pendiente
            /// </summary>
            public double M = 0;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="x"> Fecha Inicial </param>
            /// <param name="fx1"> Gasto Inicial</param>
            /// <param name="x2"> fecha final</param>
            /// <param name="fx2">Gasto Final</param>
            public InterpolacionLIneal(double x, double fx1, double x2, double fx2)
            {


                X1 = x;
                fX1 = fx1;

                X2 = x2;
                fX2 = fx2;

                CalculaPendiente();


            }




            public void CalculaPendiente()
            {
                if (X2 != X1)
                {
                    M = ((fX2 - fX1) / (X2 - X1));
                }
                else
                {
                    M = 1;
                }

            }



            /// <summary>
            /// USANDO LA ECUACION DE LA LINEA RECTA. TIENE UN ALTO MARGEN DE ERROR
            /// </summary>
            /// <param name="x"></param>
            /// <returns></returns>
            public double Evalua2(double x)
            {


                double Y = fX1 + (x - X1) * M;


                return Y;

            }
        }

        internal cDeclinacion Copy()
        {
            cDeclinacion temp = new cDeclinacion();
            temp.Tabla = Tabla.Copy();
            temp.Rango = new MyPoint(Rango);
            temp.D = D;
            temp.DD = DD;
            temp.Di = Di;
            temp.Dn = Di;
            temp.M = M;
            return temp;
        }
    }
}
