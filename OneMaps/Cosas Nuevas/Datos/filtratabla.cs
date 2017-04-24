using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using One_Produccion;


namespace Maps
{
    public partial class filtratabla : Form
    {
        int ID = 99;// significa que es cualquiert tipo
        One_Core_Perfiles CorePerfiles;
        One_Core_Produccion CoreProduccion;
        DataTable Tabla;
        DataTable TablaResultado;
        List<List<ObjetoSelecionable>> Lista = new List<List<ObjetoSelecionable>>();
        public NotifyIcon Notificacion;
        int indiceLista = 0;
        List<Filtro> miFiltro = new List<Filtro>();
        

        List<Maps.InterevalosCondiciones> ListaIntervalos = new List<Maps.InterevalosCondiciones>();
        List<Maps.InterevalosFecha> ListaFechas = new List<Maps.InterevalosFecha>();
        Maps.ClasificacionXvalor porvalor;
        Maps.Clasificadorxfecha porfecha;
        int ControlActual = 0; //o es por elemento 1.- es por valor 2.- es por facha.


        public filtratabla(DataTable t, NotifyIcon Noti )
        {
            InitializeComponent();
            Notificacion = Noti;
            Tabla = t;
            TablaResultado = Tabla.Clone();
            ID = 99;
            //aqui mandemos a poner los valosres que necesitamos
            
            porvalor = new Maps.ClasificacionXvalor(ListaIntervalos);
            porvalor.Cerrar = 1;
            porvalor.cmdAplicar.Visible = false;
            porfecha = new Maps.Clasificadorxfecha(ref ListaFechas);
            porfecha.Cerrar = 1;
            porfecha.cmdAplicar.Visible = false;
        }
        public filtratabla(DataTable t, NotifyIcon Noti,One_Core_Perfiles CorePerfiles_ )
        {
            InitializeComponent();
            Notificacion = Noti;
            Tabla = t;
            TablaResultado = Tabla.Clone();
            ID = 1;
            CorePerfiles = CorePerfiles_;
            //aqui mandemos a poner los valosres que necesitamos

            porvalor = new Maps.ClasificacionXvalor(ListaIntervalos);
            porvalor.Cerrar = 1;
            porvalor.cmdAplicar.Visible = false;
            porfecha = new Maps.Clasificadorxfecha(ref ListaFechas);
            porfecha.Cerrar = 1;
            porfecha.cmdAplicar.Visible = false;
        }
        public filtratabla(DataTable t, NotifyIcon Noti, One_Core_Produccion CoreProduccion_)
        {
            InitializeComponent();
            Notificacion = Noti;
            Tabla = t;
            TablaResultado = Tabla.Clone();
            ID = 0;
            //aqui mandemos a poner los valosres que necesitamos
            CoreProduccion = CoreProduccion_;
            porvalor = new Maps.ClasificacionXvalor(ListaIntervalos);
            porvalor.Cerrar = 1;
            porvalor.cmdAplicar.Visible = false;
            porfecha = new Maps.Clasificadorxfecha(ref ListaFechas);
            porfecha.Cerrar = 1;
            porfecha.cmdAplicar.Visible = false;
        }


        public userboton CreaBoton(string Texto)
        {
            // cuando es tipo form trataremos de crear el control
           
            //Button boton = new Button();
         
            //boton.Size = new System.Drawing.Size(100, 30);
            //boton.FlatStyle = FlatStyle.Flat;
           
            //boton.Anchor = AnchorStyles.Bottom;
            //boton.Text =Texto;
            //boton.Click +=
           
            //return boton;
            userboton temp = new userboton(); 
            temp.button1.Text = Texto;
            temp.Size = new System.Drawing.Size(92,Divisor_primario.Panel1.Height-5); 
          
            temp.button1.Click+= new EventHandler(boton_Click);
            temp.Cerrar.Click += new EventHandler(Cerrar_Click);
           // temp.Dock = DockStyle.Left;
            return temp;
        }

        void Cerrar_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("QUita");
            Button boton1 = (Button)sender;
           indiceLista = (int)boton1.Tag;
            
            if (Existe(indiceLista))
            {
                //si existe lo quietaremos
                //quita del filtro el elemnto qu etengo.
                int x = Posicion(indiceLista);
                miFiltro.RemoveAt(x);
                //Aqui desmarcaremos esa de los elemtos que hay
                ObjetosMarcados(Lista[indiceLista], false);
              //  RecargarActual();
                int tmp = indiceLista;

              //  Proceso();
              //  A_Eliminar();
                foreach (userboton boton in Divisor_primario.Panel1.Controls)
                {
                    int valor = (int)boton.button1.Tag;
                    if (valor == tmp)
                    {
                        Divisor_primario.Panel1.Controls.Remove(boton);
                        Divisor_primario.Panel1.Refresh();
                        break;
                    }
                }

                //indiceLista = -1;
            }
            A_Eliminar();

        }

        void boton_Click(object sender, EventArgs e)
        {
            Button boton = (Button)sender;
           int clave = (int )boton.Tag;
            Activa_criterio(clave);
          
        }
                                                                                                                                                                                                                 
       
        void Proceso()
        {
            //aqui agregaremos solo como objetos que se pàrecen.
            try
            {

                if(indiceLista>=0)
                {
                List<ObjetoSelecionable> lis = ObjetosMarcados(Lista[indiceLista]);

                //cada aque aplico un filtro Busco si exite en la tabla qu etengo si no ps no hay anda.

                if (!Existe(indiceLista))
                {

                    Filtro f = new Filtro();
                    f.TipoFiltro = ControlActual;
                    f.NombreCriterio = ElementoFiltro[0,indiceLista].Value.ToString();

                    switch (ControlActual)
                    {
                        case 0: f.Lista = lis.ToList();
                            break;
                        case 1:
                            f.ListaV = ListaIntervalos.ToList();
                            break;
                        case 2:
                            f.ListaF = ListaFechas.ToList();
                            break;
                    }



                    miFiltro.Add(f);
                 userboton x=  CreaBoton(f.NombreCriterio);

                 x.button1.Tag = indiceLista;
                 x.Cerrar.Tag = indiceLista;
                 Divisor_primario.Panel1.Controls.Add(x);
                 x.Dock = DockStyle.Left;
                 foreach (Control control in Divisor_primario.Panel1.Controls)
                 {
                     control.Size = new System.Drawing.Size(92, Divisor_primario.Panel1.Height - 5);
                     control.Dock = DockStyle.Left;
                 }
                 Divisor_primario.Panel1.Refresh();
                }
                else
                {

                    miFiltro[Posicion(indiceLista)].TipoFiltro = ControlActual;
                    switch (ControlActual)
                    {
                        case 0:
                            miFiltro[Posicion(indiceLista)].Lista = lis.ToList();
                            break;
                        case 1:
                            miFiltro[Posicion(indiceLista)].ListaV = ListaIntervalos.ToList();
                            break;
                        case 2:
                            porfecha.AplicaCambios();
                           ListaFechas= porfecha.Lista;
                            miFiltro[Posicion(indiceLista)].ListaF = ListaFechas.ToList();
                            break;
                    }
                }

                  
                   
                }

                A_Eliminar();

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Text = ejemplo.Rows.Count+" Rows";
        }
        void A_Eliminar()
        {
             try
            {
                 AplicaFiltro();
                if (miFiltro.Count == 0)
                {
                    Tabla.Merge(TablaResultado);
                    TablaResultado.Clear();
                    ActualizaSinFiltroLista();
                    RecargarActual();

                }

                
                //Visor_Datos vd = new Visor_Datos();
                //vd.EstableceDatos(TablaResultado);
                //vd.Rapida(true);
                foreach (DataGridViewColumn col in ejemplo.Columns)
                {
                    col.SortMode = DataGridViewColumnSortMode.Programmatic;// = DataGridViewAutoSizeColumnMode.Fill;
                }
           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int Posicion(int clave)
        {
            string Nombre = ElementoFiltro[0, clave].Value.ToString();
            int i = 0;
            foreach (Filtro f in miFiltro)
            { 
                if(f.NombreCriterio== Nombre)
                    return i;
                i++;
             
            }
            return 0;
         
        }
        bool Existe (int clave)
        {
            if (clave < 0 && clave >= ElementoFiltro.Rows.Count)
                return false;
            var exi = from x in miFiltro.AsEnumerable() where x.NombreCriterio ==  ElementoFiltro[0,clave].Value.ToString() select x;
            if(  exi.Count()>0)
                return true;

           
        
            return false;

        
        }
        int  TipoBusqueda(string cam)
        {

            var exi = from x in miFiltro.AsEnumerable().Where (x=> x.NombreCriterio == cam) select x;
            if (exi.Count() > 0)
            {
                List<Filtro> L = exi.ToList();
                return L[0].TipoFiltro;


            }
          

            return 0;


        }
        void AplicaFiltro()
        {
            try
            { // ok hagamos el primera copia fase del filtrado.
                Tabla.Merge(TablaResultado);


                TablaResultado.Clear();

                //antes que nada deberiasmo de mandar los valores que tengo y visualizar 

                IEnumerable<DataRow> campoincial = Tabla.AsEnumerable();
                Visor_Datos vd = new Visor_Datos();

                foreach (Filtro f in miFiltro)
                {
                    if (campoincial.Count() <= 0)
                        break;

                    //dependiendo del tipo de busqueda.
                    switch (f.TipoFiltro)
                    {
                        case 0:
                            if (f.Lista.Count > 0)
                                campoincial = BuscaLinq(f.NombreCriterio, f.Lista, campoincial);
                            //vd.EstableceDatos(campoincial.CopyToDataTable());
                            //vd.Text = f.NombreCriterio;
                            // vd.Rapida(true);

                            break;
                        case 1:
                            foreach (Maps.InterevalosCondiciones inter in f.ListaV)
                            {
                                if (campoincial.Count() <= 0)
                                    break;
                                else
                                    campoincial = ValoresLinq(inter, f.NombreCriterio, campoincial);
                            }
                            //vd.EstableceDatos(campoincial.CopyToDataTable());
                            //vd.Text = f.NombreCriterio;
                            //  vd.Rapida(true);

                            break;
                        case 2:
                            foreach (Maps.InterevalosFecha inter in f.ListaF)
                            {
                                if (campoincial.Count() <= 0)
                                    break;
                                campoincial = FechasLinq(inter, f.NombreCriterio, campoincial);
                            }
                            break;
                    }
                }
                if (miFiltro.Count >0 &&  campoincial.Count() > 0)
                {
                    List<DataRow> ListaRows = campoincial.ToList();
                    Notificacion.ShowBalloonTip(500, "Informacion - " + Application.ProductName, "Aplicando Flitros\n Puede tardar varios minutos", ToolTipIcon.Warning);
                            

                    //  TablaResultado.Merge(campoincial.CopyToDataTable());
                    foreach (DataRow row in ListaRows)
                    {
                        try
                        {

                            //  MessageBox.Show(row.Table.ToString());
                            TablaResultado.ImportRow(row);
                        
                            row.Table.Rows.Remove(row);  
                            //row.Delete();
                         //   Tabla.Rows.Remove(row);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    TablaResultado.AcceptChanges();
                    Tabla.AcceptChanges();
                    Notificacion.ShowBalloonTip(500, "Informacion - " + Application.ProductName, "Filtro terminado", ToolTipIcon.Warning);
                            
                }
                else
                {
                    MessageBox.Show("Sin Resultado");
                }
                //vd.Text = "Resultado";
                //vd.EstableceDatos(TablaResultado);
                //vd.Rapida(true);

                //depues de haber aplicado el filtro pondremos los valores en las columnas que no estan marcadas.
                ActualizaLista();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private bool IsNumeric(string number)
        //{
        //    if (number.Length < 1)
        //        return false;
        //    Regex pattern = new Regex(@"^(-)?[0-9]*(.)?[0-9]*$");//(@"^(-)?[\d]?([.]+(\d))?$");//("[^0-9][.]?[^0-9]");

        //    return pattern.IsMatch(number);
        //}

        IEnumerable<DataRow> ValoresLinq(Maps.InterevalosCondiciones interevalosCondiciones, string Campo, IEnumerable<DataRow> reglon)
        {
          
                 var  CampoInicial = from order in reglon where  A_valor( order[Campo].ToString(),interevalosCondiciones.Operador , interevalosCondiciones.Cantidad)   select order;

                 return CampoInicial;          

        }
        bool A_valor(string cad, string Operador,double Cantidad)
        {
                                    
            cad = cad.Replace(",", string.Empty);
            if (IsNumeric(cad))
            {
                double val = Numero(cad);
                switch (Operador)
                {
                    case "<":
                        return (val < Cantidad);


                       
                    case ">=":
                        return (val >= Cantidad);
                   
                    case "=":
                        return (val == Cantidad);
                    
                    case "!=":
                        return (val != Cantidad);
                      
                }


            }
            return false;
        }
        private bool IsNumeric(string number)
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
        
        IEnumerable<DataRow> FechasLinq(Maps.InterevalosFecha interevalosCondiciones, string Campo, IEnumerable<DataRow> reglon)
        {



            reglon = from order in reglon where( A_fecha( order[Campo].ToString(), interevalosCondiciones.Operador, interevalosCondiciones.Fecha)) select order;
            

            return reglon;

        }
        bool A_fecha(string cad, string Operador, DateTime Fecha)
        {
            DateTime val = DateTime.Now;
            cad = cad.Replace(",", string.Empty);
            if (DateTime.TryParse(cad, out val))
            {
                switch (Operador)
                {
                    case "<":
                        return (val < Fecha);


                    case ">=":
                        return (val >= Fecha);
                       
                    case "=":
                        return (val == Fecha);
                      
                    case "!=":
                        return (val != Fecha);
                      
                }


            }
            return false;
        }


        void ActualizaLista()
        {
            for (int i = 0; i < Lista.Count; i++)
            {

                if (!Existe(i))
                {
                    List<ObjetoSelecionable> lista = Lista[i];
                    lista.Clear();
                    lista.AddRange(Distintos_x_Columna_Resultado(Tabla.Columns[i].ColumnName));
                    //if (indiceLista == i)
                    //{
                    //    RecargarActual();

                    //}
                }
                else
                {
                    //aqui estan en el filtro. pero necesitamos sub actualizar con los nuevos
                    List<ObjetoSelecionable> temp = Distintos_x_Columna_Resultado(Tabla.Columns[i].ColumnName);
                    //aqui los cruzaremos con los elemntos que ya tenemos para ver si exite uno nuevo.
                    List<ObjetoSelecionable> lista = Lista[i];
                    //mejro ve<mos que hay adentro.
                    //userFiltroElemento quehay = new userFiltroElemento(ref temp);
                    //quehay.EnForm();
                    //quehay.NuevaLista(ref lista);
                    //quehay.EnForm();

                    bool ban = false;
                    //que pasa si intentamos agregar por si hay alguno que resulto del filtro.
                    //aremos una union. lista mas distino.
                    foreach (ObjetoSelecionable elem in temp)
                    {
                        //existe en este objeto?
                        ban = false;
                        foreach (ObjetoSelecionable l in lista)
                        {
                            if (elem.ToString() == l.ToString())
                                ban = true;

                        }
                        if (!ban)
                        {

                            lista.Add(elem);
                        }
                    }//foreach

                }
            }
            Activa_criterio(indiceLista);
        }
        void RecargarActual()
        {
            try
            {
                if (indiceLista >= 0)
                {
                    //aqui estan en el filtro. pero necesitamos sub actualizar con los nuevos
                    List<ObjetoSelecionable> temp = Distintos_x_Columna_Resultado(Tabla.Columns[indiceLista].ColumnName);
                    //aqui los cruzaremos con los elemntos que ya tenemos para ver si exite uno nuevo.
                    List<ObjetoSelecionable> lista = Lista[indiceLista];

                    bool ban = false;
                    //que pasa si intentamos agregar por si hay alguno que resulto del filtro.
                    //aremos una union. lista mas distino.
                    foreach (ObjetoSelecionable elem in temp)
                    {
                        //existe en este objeto?
                        ban = false;
                        foreach (ObjetoSelecionable l in lista)
                        {
                            if (elem.ToString() == l.ToString())
                                ban = true;

                        }
                        if (!ban)
                        {

                            lista.Add(elem);
                        }
                    }//foreach

                    //recargar este tambien.
                    List<ObjetoSelecionable> L = Lista[indiceLista];
                    userFiltroElemento1.NuevaLista(ref  L);
                }
            }
            catch { }
            

        }
        Visor_Datos global = new Visor_Datos();
        IEnumerable<DataRow> BuscaLinq(string Campo, List<ObjetoSelecionable> Valores, IEnumerable<DataRow> rows_linq)
        {

            //MessageBox.Show("esto es lo que recibimos");
            //global.EstableceDatos(rows_linq.CopyToDataTable());
            //global.Rapida(true);
                var query = from order in rows_linq.Where(p=> p!=null) where (esta(order[ Campo].ToString().ToUpper(), Valores)) select order;
                
            return query;

        }


        bool esta(string Campo, List<ObjetoSelecionable> Valores)
        {
            try
            {
                var a = from b in Valores.AsEnumerable() where( b.ToString().ToUpper()== Campo) select b;
                if (a.Count() > 0)
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;

        }

        List<ObjetoSelecionable> ObjetosMarcados(List<ObjetoSelecionable> lis)
        {
            var Objetos = from a in lis.AsEnumerable().Where(p => p.Estado) select a;
            if (Objetos.Count() > 0)
                return Objetos.ToList();
            return new List<ObjetoSelecionable>();
        }



        private void filtratabla_Load(object sender, EventArgs e)
        {
            //ahora rellenemos los valores donde 
            userFiltroElemento1.panel_aceptar.Visible = false;
            ejemplo.DataSource = TablaResultado;
            EventosDGv dg = new EventosDGv(ejemplo, Notificacion);
            dg.IsOrigen = true;
            dg.Especial();
            foreach (DataGridViewColumn col in ejemplo.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Programmatic;// = DataGridViewAutoSizeColumnMode.Fill;
            }
            foreach (DataColumn col in Tabla.Columns)
            {
                DataGridViewRow reglon = new DataGridViewRow();
                reglon.CreateCells(ElementoFiltro);

                reglon.Cells[0].Value = col.ColumnName;
                ElementoFiltro.Rows.Add(reglon);


            }

            ActualizaSinFiltroLista();


        }
        void ActualizaSinFiltroLista()
        {
            Lista.Clear();
            foreach (DataColumn col in Tabla.Columns)
            {

                Lista.Add(Distintos_x_Columna(col.ColumnName));

            }

        }
        public List<ObjetoSelecionable> Distintos_x_Columna_(string Campo)
        {
            List<ObjetoSelecionable> combopozo = new List<ObjetoSelecionable>();
            try
            {


                try
                {


                    IEnumerable<string> CampoInicial =
                     (from pozo in Tabla.AsEnumerable().Where(p => p.Field<string>(Campo) != null) select pozo.Field<string>(Campo).ToUpper()).Distinct();


                    foreach (string p in CampoInicial)
                    {
                        combopozo.Add(new ObjetoSelecionable(p, false));

                    }

                }
                catch { }

            }
            catch { }
            return combopozo;
        }

        public List<ObjetoSelecionable> Distintos_x_Columna_Resultado_(string Campo)
        {
            List<ObjetoSelecionable> combopozo = new List<ObjetoSelecionable>();
            try
            {


                try
                {

                    //(from pozo in TablaResultado.AsEnumerable().Where(p => p.Field<string>(Campo) != null) select pozo.Field<string>(Campo).ToUpper()).Distinct();
                    IEnumerable<string> CampoInicial =
                     (from pozo in TablaResultado.AsEnumerable() select pozo.Field<string>(Campo).ToUpper()).Distinct();


                    foreach (string p in CampoInicial)
                    {
                        combopozo.Add(new ObjetoSelecionable(p, false));

                    }

                }
                catch { }

            }
            catch { }
            return combopozo;
        }

        //CamposDiferentes.
        public List<ObjetoSelecionable> Distintos_x_Columna_Resultado(string Columna)
        {
            List<ObjetoSelecionable> combopozo = new List<ObjetoSelecionable>();
           

                //aqui rellenaremos pero 
                try
                {


                    IEnumerable<string> CampoInicial =
                         (from pozo in TablaResultado.AsEnumerable() select pozo[Columna].ToString()).Distinct();
                    /*
                      IEnumerable<string> CampoInicial =
                           (from pozo in Tabla.AsEnumerable().Where(p => p.Field<string>(Columna) != null) select pozo.Field<string>(Columna).ToUpper()).Distinct();

      
                     */

                    foreach (string p in CampoInicial)
                    {
                        combopozo.Add(new ObjetoSelecionable(p, false));


                    }
                    

                }
                catch { }

                return combopozo;
        }
        public List<ObjetoSelecionable> Distintos_x_Columna(string Campo)
        {
            List<ObjetoSelecionable> combopozo = new List<ObjetoSelecionable>();


            //aqui rellenaremos pero 
            try
            {


                IEnumerable<string> CampoInicial =
                     (from pozo in Tabla.AsEnumerable() select pozo[Campo].ToString()).Distinct();
                /*
                  IEnumerable<string> CampoInicial =
                       (from pozo in Tabla.AsEnumerable().Where(p => p.Field<string>(Columna) != null) select pozo.Field<string>(Columna).ToUpper()).Distinct();

      
                 */

                foreach (string p in CampoInicial)
                {
                    combopozo.Add(new ObjetoSelecionable(p, false));


                }


            }
            catch { }

            return combopozo;
        }
        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {

                Activa_criterio(e.RowIndex);
               
            }
        }
        void Activa_criterio(int Clave)
        {
           



            indiceLista = Clave;
          
           string  sName = ElementoFiltro[0, Clave].Value.ToString();
            ListaIntervalos.Clear();
            ListaFechas.Clear();
            if (Existe(indiceLista))
            {
                switch (TipoBusqueda(sName))
                {

                    case 0:  
                        RecargarActual(); 
                      //  List<ObjetoSelecionable> lis = Lista[Clave];

          //  userFiltroElemento1.NuevaLista(ref lis);
                        ColocaTipo();
                        break;
                    case 1:
                        //como ya existe entonces pondremos el valor que esta guardado
                        ListaIntervalos = miFiltro[Posicion(indiceLista)].ListaV.ToList();
                        ColocaValor();
                        break;
                    case 2:
                        //como ya existe entonces pondremos el valor que esta guardado
                        ListaFechas = miFiltro[Posicion(indiceLista)].ListaF.ToList();
                        ColocaFecha();
                        break;

                }
            }
            else
            {
                RecargarActual(); 
                ColocaTipo();
            }
        }
        private void userVista1_Load(object sender, EventArgs e)
        {

        }

        private void cmdTipos_Click(object sender, EventArgs e)
        {
            ColocaTipo();

        }
        void ColocaTipo()
        {
            //aqui cuando es por tipo ps mostraremos la respectiva control.
            //aquitemos todo.

            PanelDerecho.Controls.Remove(userFiltroElemento1);
            PanelDerecho.Controls.Remove(porvalor);
            PanelDerecho.Controls.Remove(porfecha);

            PanelDerecho.Controls.Add(userFiltroElemento1, 0, 1);
            ControlActual = 0;
        }

        private void cmdValor_Click(object sender, EventArgs e)
        {
            ColocaValor();
        }
        void ColocaValor()
        {
            //aqui cuando es por tipo ps mostraremos la respectiva control.
            //aquitemos todo.

            PanelDerecho.Controls.Remove(userFiltroElemento1);
            PanelDerecho.Controls.Remove(porvalor);
            PanelDerecho.Controls.Remove(porfecha);

            PanelDerecho.Controls.Add(porvalor, 0, 1);
            
            porvalor.Actualiza(ListaIntervalos);
            ControlActual = 1;
        }

        void ColocaFecha()
        {
            //aqui cuando es por tipo ps mostraremos la respectiva control.
            //aquitemos todo.

            PanelDerecho.Controls.Remove(userFiltroElemento1);
            PanelDerecho.Controls.Remove(porvalor);
            PanelDerecho.Controls.Remove(porfecha);


            PanelDerecho.Controls.Add(porfecha, 0, 1);
            ControlActual = 2;
        }
        private void buttonX2_Click(object sender, EventArgs e)
        {
            Proceso();
            //aqui guardamos los valores para ver que pasa.
            switch (ControlActual)
            {
                case 0: 
                    
                    break;
                case 1:
                    //aqui mostremos lo que hay cuando sea por valor.

                   
                    break;
                case 2:
                    break;
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            ColocaFecha();
        }
        void ObjetosMarcados(List<ObjetoSelecionable> lista,bool Estado)
        {
            foreach (ObjetoSelecionable elemento in lista)
            {
                elemento.Estado = Estado;
            }
           
        }

       

        private void ElementoFiltro_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void filtratabla_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Tabla.Merge(TablaResultado);
            }
            catch
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //aqui empecemos con la modificaciones. ggg
            if (TablaResultado.Rows.Count > 0)
            {
                switch (ID)
                {
                    case 0:
                        //para producciio.
                        int cont=1; 
                        string Nombre = "tab";
                        do
                        {
                            while (CoreProduccion.Unidad.dtTablas_Informacion.Tables.Contains(TablaResultado.TableName + "_F" + cont.ToString()))
                            {
                                cont++;
                            }
                           Nombre= TablaResultado.TableName + "_F" + cont.ToString();
                            SoloNombre soloname = new SoloNombre(Nombre);
                            soloname.ShowDialog();
                            Nombre = soloname.txtxName.Text;
                            //como ya cambio el nombre volvamos a ver si no esta ocupado.
                            if (CoreProduccion.Unidad.dtTablas_Informacion.Tables.Contains(Nombre))
                                MessageBox.Show("El nombre para esta tabla ya existe!!! escriba otra");
                        }
                        while (CoreProduccion.Unidad.dtTablas_Informacion.Tables.Contains(Nombre));
                      
                       DataTable dt = TablaResultado.Copy();
                       dt.TableName = Nombre.ToUpper();
                       Point pos = CoreProduccion.Unidad.PosicionDiccionario(TablaResultado.TableName);
                     if (pos.X != -1)
                     {
                         CoreProduccion.Unidad.dtTablas_Informacion.Tables.Add(dt);
                         CoreProduccion.Unidad.Diccionario.Add(dt.TableName, pos);
                         CoreProduccion.Unidad.LeerTablas();

                     }
                     MessageBox.Show("Tabla de Resultado Guardado Correctamente");
                        break;
                    case 1: break;
                }
             
            }
            
        }

     

       


    }

   
    class Filtro
    {

         public string NombreCriterio
        {
            get;
            set;
        }
         public string Titulo;
        

        public int TipoFiltro;
        public bool Unir; 
       public  List<ObjetoSelecionable> Lista;
        public List<Maps.InterevalosFecha> ListaF;
        public List<Maps.InterevalosCondiciones> ListaV;
        public Filtro()
           {
               Titulo = "";
               Unir = false;
            
            }
        public override string ToString()
        {
           return NombreCriterio;
        }
       


    }

}