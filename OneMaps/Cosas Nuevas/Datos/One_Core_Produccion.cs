using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using Gigasoft.ProEssentials.Enums;
using System.Windows.Forms;
using One_Produccion.Editar_Tabla;

namespace Maps
{

    public class UnidadChan
    {

       
        public DataTable Tablas;
        public DateTime Inicio;
        public DateTime Fin;

        public UnidadChan()
        {
            Inicio = DateTime.Now;
            Fin = DateTime.Now;

        }
    }


    public class One_Core_Produccion: origenCore
    {
        //subclase que contiene las tablas por Pozo.
        public One_Core_Produccion()
        {
            Unidad = new Unidad_Graficacion_Pozo();
      
     
        }
     
        public class Unidad_Graficacion_Pozo
        {
           public List<ObjetoSelecionable> TablasUtilizar;
            public DataTable DtEventos;// dt que sontendra solamente los eventos de este pozo respcto al tiempo
            public DataTable DtGrupos; // dt que tendra los pozos respecto al tipo.


            public DataSet dtTablas_Informacion; // aqui contendra toda la informacion y tablas que el user cree.
            //public Hashtable Indices = new Hashtable();//aqui gurdaremos el nombre de la Tabla.
            public Dictionary<String, Point> Diccionario = new Dictionary<string, Point>();
            public string File = "Nuevo";
            public Unidad_Graficacion_Pozo()
            {
                
                DtEventos = new DataTable();
                dtTablas_Informacion = new DataSet();// List<DataTable>();
                CreaEventos();
                DtGrupos = new DataTable("Grupos");
                CreaGrupos();
               TablasUtilizar = new List<ObjetoSelecionable>();

               

            }

            void CreaEventos()
            {
                DtEventos.Columns.Clear();
                DtEventos.Columns.Add("Visible",Type.GetType("System.Boolean"));
                DtEventos.Columns.Add("Pozo");
                DtEventos.Columns.Add("Fecha");
                DtEventos.Columns.Add("Evento");
                DtEventos.Columns.Add("Abreviacion");
                DtEventos.TableName = "Tabla_Eventos";
            }
            void CreaGrupos()
            {
                DtGrupos.Columns.Clear();
                DtGrupos.Columns.Add("Num");
                DtGrupos.Columns.Add("Pozo");
                DtGrupos.Columns.Add("Color");
            }
            void VerificaEventos()
            {

                string[] Columnas = new string[] { "Visible", "Pozo", "Fecha", "Evento", "Abreviacion" };
                int i = 0;
                foreach (string col in Columnas)
                {
                    if (!DtEventos.Columns.Contains(col))
                    {
                        DtEventos.Columns.Add(col);
                    }
                    DtEventos.Columns[col].SetOrdinal (i);
                    i++;

                }

                DtEventos.TableName = "Tabla_Eventos";
            }
            void VerificaGrupos()
            {

                string[] Columnas = new string[] { "Num", "Pozo", "Color" };
                foreach (string col in Columnas)
                {
                    if (!DtGrupos.Columns.Contains(col))
                        DtGrupos.Columns.Add(col);
                }

                DtGrupos.TableName = "Grupos";
            }
            /// <summary>
            /// Agregar una Nueva Tabla de Informacion
            /// </summary>
            /// <param name="Schema"></param>
            public void CreaTablaInformacion_esquema(DataTable Schema)
            {
                dtTablas_Informacion.Tables.Add(Schema.Clone());
                TablasUtilizar.Add ( new ObjetoSelecionable (Schema.TableName,true));
            }
            public void CargaTablaInformacion_poreferencia(DataTable Schema)
            {
                dtTablas_Informacion.Tables.Add(Schema);
                TablasUtilizar.Add(new ObjetoSelecionable(Schema.TableName, true));
            }
            //public bool ExistePozoTabla(DataTable dtTabla_Medicion, String NombrePozo)
            //{
            //    try
            //    {
            //        int x = 0;
            //        x = Diccionario[dtTabla_Medicion.TableName].X;
            //        IEnumerable<DataRow> Selecionado = from Tabla in dtTabla_Medicion.AsEnumerable().Where(p => p.Field<string>(x).ToUpper() == NombrePozo.ToUpper()) select Tabla;
                

            //    if (Selecionado.Count()>0)
            //        return true;
            //    return false; 
            //    }
            //    catch {
            //       return  false;
            //    }
            //}
            public List<string> RetornaPozos()
            {
                List<string> combopozo = new List<string>();
                try
                {

                    //aqui rellenaremos pero 

                    foreach (ObjetoSelecionable obj in TablasUtilizar)
                    {

                        string name = obj.ToString();
                        if (obj.Estado)
                        {
                            DataTable dtTabla_Medicion = dtTablas_Informacion.Tables[name];
                            dtTabla_Medicion.AcceptChanges();
                            if (dtTabla_Medicion.Namespace != "PlantillasTabla")
                            {

                                int x = 0;
                                x = Diccionario[dtTabla_Medicion.TableName].X;
                                IEnumerable<string> CampoInicial =
                                 (from pozo in dtTabla_Medicion.AsEnumerable().Where(p => !string.IsNullOrWhiteSpace(p.Field<string>(x))) select pozo[x].ToString().ToUpper()).Distinct();
                                
                                foreach (string p in CampoInicial)
                                {
                                    if (!combopozo.Contains(p))
                                        combopozo.Add(p);

                                }
                            }

                        }
                    }


                }
                catch { }
                return combopozo;
            }
          
            /// <summary>
            /// aqui casi carga todo excepto que no cargara la plantilla en si.
            /// </summary>
            /// <param name="Plantillas_Salvar"></param>
            /// <returns></returns>
            public bool Guarda_Todo(List<DataTable> Plantillas_Salvar,string File_name)
            {
                //  dtTablas_Informacion.
                DataSet Nuevo = new DataSet();
              //  Nuevo = SinPlantillas();
                Nuevo = dtTablas_Informacion.Copy();
                Nuevo.Tables.Add(DtEventos.Copy());
                Nuevo.Tables.Add(DtGrupos.Copy());
                Nuevo.Tables.Add(GuardaDiccionario());
                int cont = 0;
                foreach (DataTable tab in Plantillas_Salvar)
                {
                    cont++;
                   // tab.Namespace = "PlantillasTabla";
                   
                    if (!Nuevo.Tables.Contains(tab.TableName, tab.Namespace))
                    {
                        Nuevo.Tables.Add(tab.Copy());
                    }
                    else
                    {
                        tab.TableName = tab.TableName + cont.ToString();
                        Nuevo.Tables.Add(tab.Copy());
                    }
                }
                Nuevo.WriteXml(File_name, XmlWriteMode.WriteSchema);
                Nuevo.Dispose();
                return true;
            }
            


            DataTable GuardaDiccionario()
            {

                DataTable Dic = new DataTable("DiccionarioActual");
                Dic.Rows.Clear();
                Dic.Columns.Add("Clave", Type.GetType("System.String"));

                Dic.Columns.Add("PuntoX", Type.GetType("System.Int32"));
                Dic.Columns.Add("PuntoY", Type.GetType("System.Int32"));
                DataRow Reglon;
                foreach (KeyValuePair<string, Point> Par in Diccionario)
                {
                    Reglon = Dic.NewRow();
                    Reglon[0] = Par.Key;
                    Reglon[1] = Par.Value.X;
                    Reglon[2] = Par.Value.Y;
                    Dic.Rows.Add(Reglon);


                }
            
                return Dic;
                /*Dictionary<string,string> ConceptosCertificado)                       

StringBuilder CertBuilder = new StringBuilder();      

// Recorre los pares           

foreach (KeyValuePair<string,string> Par in ConceptosCertificado)
{
  CertBuilder.AppendLine(string.Format("{0}:{1}",Par.Key,Par.Value));
}
 
// Recorre solo los valores

foreach (string Valor in ConceptosCertificado.Values) 

{ 

CertBuilder.AppendLine(Par.Value); 

} 
                 */


            }
            
            //devuelve la ifnormacion de un solo po<o.

            public DataTable Info(string NombrePozo, String origen)
            {
                NombrePozo = NombrePozo.ToUpper();
                IEnumerable<DataRow> Selecionado = from Tabla in dtTablas_Informacion.Tables[origen].AsEnumerable().Where(p => p.Field<string>(Diccionario[origen].X) != null).Where(p => p.Field<string>(Diccionario[origen].X).ToUpper() == NombrePozo) select Tabla;

                    if(Selecionado.Count()>0)
                return Selecionado.CopyToDataTable();
                    return dtTablas_Informacion.Tables[origen].Clone();

            }

            void  ModificandoCargados(DataTable eventos_pre)
            {
               
               CreaEventos();
               VerificaEventos();
                
               DtEventos.Merge(eventos_pre, true, MissingSchemaAction.Ignore);
                //aqui veremos que todo este en falso.
                var estados = from a in DtEventos.AsEnumerable() where ( a["Visible"] == DBNull.Value  ) select a ;
                foreach (DataRow rows in estados)
                { 
                //aqui vermos todos aquellos valores raros y los volveremos a poner en false
                    rows["Visible"] = false;
                }
              
                
            }
            public void LeeArchivo(string RutaXML, System.Windows.Forms.DataGridView dgvPlantillas)
            {
                
                try
                {
                    // System.Windows.Forms.MessageBox.Show("esto es lo que antes en el datagridview");
                
                    dtTablas_Informacion.Clear();
                   
                    foreach (DataTable dt in dtTablas_Informacion.Tables)
                        dt.Dispose();

                    dtTablas_Informacion.Tables.Clear();
                   
                    dtTablas_Informacion.ReadXml(RutaXML);
                    File = RutaXML;
                    if (dtTablas_Informacion.Tables.Contains("Tabla_Eventos"))
                    {

                      //  dtEventos = dtTablas_Informacion.Tables["Tabla_Eventos"].Copy();
                        ModificandoCargados(dtTablas_Informacion.Tables["Tabla_Eventos"]);
                        dtTablas_Informacion.Tables.Remove("Tabla_Eventos");
                        VerificaEventos();

                    }
                    else
                    {
                        CreaEventos();
                    }
                   
                    if (dtTablas_Informacion.Tables.Contains("Grupos"))
                    {

                        DtGrupos = dtTablas_Informacion.Tables["Grupos"].Copy();
                        dtTablas_Informacion.Tables.Remove("Grupos");
                        VerificaEventos();

                    }
                    else
                    {
                      CreaGrupos  ();
                    }
                    if (dtTablas_Informacion.Tables.Contains("DiccionarioActual"))
                    {

                        //aqui cargaremos el diccionario
                        foreach (DataRow reglon in dtTablas_Informacion.Tables["DiccionarioActual"].Rows)
                        {
                            try
                            {
                                Diccionario.Add(reglon[0].ToString().ToUpper(), new Point(Convert.ToInt32(reglon[1]), Convert.ToInt32(reglon[2])));
                            }
                            catch { }
                        }
                        dtTablas_Informacion.Tables.Remove("DiccionarioActual");
                    }
                    else
                    {
                        //si no tenemos el diccionario crearemos uno asumienyo qque la columna pozo es el 0 y la fecha es el 1.

                        foreach (DataTable dtTablas in dtTablas_Informacion.Tables)
                        {

                            try
                            {
                                Diccionario.Add(dtTablas.TableName, new Point(0, 1));
                            }
                            catch { }
                        }
                    }
                    try
                    {
                        //como se supone que ya saque el dicionario y los eventos nos queda sacar las Plantillas.
                        dgvPlantillas.Rows.Clear();
                        int conp = 0;
                        dgvPlantillas.Visible = false;
                        dgvPlantillas.SuspendLayout();

                        List<string> PlantillasEliminar = new List<string>();
                        foreach (DataTable tab in dtTablas_Informacion.Tables)
                        {
                            if (tab.Namespace == "PlantillasTabla" || tab.TableName.StartsWith("P_") )
                            {
                                //  One_Registro_Presion.VistaPrevia vp = new One_Registro_Presion.VistaPrevia(tab);
                                // vp.ShowDialog();
                              
                                 MiPlantilla mipla = new  MiPlantilla(tab);
                                {


                                    System.Windows.Forms.DataGridViewRow R = new System.Windows.Forms.DataGridViewRow();
                                    R.CreateCells(dgvPlantillas);

                                    R.Cells[0].Value = false;

                                    R.Cells[1].Value = R.Cells[1].ToolTipText = tab.TableName;//dgvPlantillas.Rows[dgvPlantillas.Rows.Count - 1].Cells[1].Tag.ToString();
                                    R.Cells[1].Tag = mipla;
                                    R.Cells[2].Value = "X";
                                    dgvPlantillas.Rows.Add(R);
                                    conp++;
                                    
                                }

                                PlantillasEliminar.Add(tab.TableName);

                            }
                        }
                        dgvPlantillas.ResumeLayout();
                        dgvPlantillas.Visible = true;
                        //ahora que ya cargue las pantillas las borrare.
                        foreach (string sPlantilla in PlantillasEliminar)
                        {
                           DataTable t= dtTablas_Informacion.Tables[sPlantilla];
                            dtTablas_Informacion.Tables.Remove(sPlantilla);
                            t.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }


                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
               
                LeerTablas();
            }
            //Hagamos inteligentes esta tabla
            public void LeerTablas()
            {
                TablasUtilizar.Clear();

                //busquemos si existe o no.
                foreach (DataTable dt in dtTablas_Informacion.Tables)
                {
                    if (!ExisteTabla(dt.TableName))
                    TablasUtilizar.Add(new ObjetoSelecionable(dt.TableName, true));
                }
            }

            bool ExisteTabla(string Tabla)
            {
                var existe = from a in TablasUtilizar.AsEnumerable() where (a.ToString()== Tabla) select a;
                if (existe.Count() > 0)
                    return true;
                return false;
            }

            public void SoloPlatillas(string RutaXML, System.Windows.Forms.DataGridView dgvPlantillas)
            {
                try
                {
                    DataSet Local_dt = new DataSet("Local");
                    // System.Windows.Forms.MessageBox.Show("esto es lo que antes en el datagridview");
                    Local_dt.Clear();
                    Local_dt.Tables.Clear();
                   
                    Local_dt.ReadXml(RutaXML);

                    //como se supone que ya saque el dicionario y los eventos nos queda sacar las Plantillas.
                    
                    int conp = 0;

                    foreach (DataTable tab in Local_dt.Tables)
                    {
                        if (tab.Namespace == "PlantillasTabla" || tab.TableName.StartsWith("P_"))
                        {
                            //  One_Registro_Presion.VistaPrevia vp = new One_Registro_Presion.VistaPrevia(tab);
                            // vp.ShowDialog();
                             MiPlantilla mipla = new  MiPlantilla(tab);
                            {


                                System.Windows.Forms.DataGridViewRow R = new System.Windows.Forms.DataGridViewRow();
                                R.CreateCells(dgvPlantillas);

                                R.Cells[0].Value = false;

                                R.Cells[1].Value = R.Cells[1].ToolTipText = tab.TableName;//dgvPlantillas.Rows[dgvPlantillas.Rows.Count - 1].Cells[1].Tag.ToString();
                                R.Cells[1].Tag = mipla;
                                dgvPlantillas.Rows.Add(R);
                                conp++;
                               
                            }

                        }
                    }
                    Local_dt.Dispose();


                }
                catch { }

            }
            public Point PosicionDiccionario(string Tabla)
            {
                if (DtEventos.TableName == Tabla)
                    return new Point(1, 2);
                else

                    if (Diccionario.ContainsKey(Tabla))
                    {
                        return (new Point(Diccionario[Tabla].X, Diccionario[Tabla].Y));
                    }

                return new Point(-1, -1);
            }
            public void OrdenaPorPozo()
            {

             //   OrdenaEvento();
                //aqui rellenaremos pero 
                foreach (DataTable dtTabla_Medicion in dtTablas_Informacion.Tables)
                {
                    try
                    {
                      //  if (dtTabla_Medicion.Namespace != "PlantillasTabla")
                        {
                            int x = 0;
                            int y = 0;
                            x = Diccionario[dtTabla_Medicion.TableName].X;
                            y = Diccionario[dtTabla_Medicion.TableName].Y;
                            IEnumerable<DataRow> CampoInicial =( from pozo in dtTabla_Medicion.AsEnumerable() where(pozo.Field<string>(x) != null) orderby pozo.Field<string>(x)select pozo) ;

                            //     var queryCustomersByCity =         from cust in customers           group cust by cust.City;

                            var agrupados = from grupo in CampoInicial group grupo by grupo.Field<string>(x);

                            //     var queryCustomersByCity =         from cust in customers           group cust by cust.City;


                            DataTable temp = dtTabla_Medicion.Clone();

                            foreach (var ungrupo in agrupados)
                            {


                                try
                                {
                                    var nuevo = from pozo in ungrupo
                                                orderby Convert.ToDateTime(pozo.Field<string>(y))
                                                select pozo;
                                   
                                    temp.Merge(nuevo.CopyToDataTable());
                                }
                                catch
                                {
                                  
                                    temp.Merge(ungrupo.CopyToDataTable());
                                }

                            }
                            dtTabla_Medicion.Rows.Clear();
                            dtTabla_Medicion.Merge(temp);

                        }
                    }
                    catch { }
                  
                }


            }
            public void OrdenaPorPozo(DataTable dtTabla_Medicion)
            {

                if (dtTabla_Medicion == DtEventos)
                {
                    OrdenaEvento();
                    return ;
                }
                //aqui rellenaremos pero 
                
                    try
                    {
                        //  if (dtTabla_Medicion.Namespace != "PlantillasTabla")
                        {
                            int x = 0;
                            int y = 0;
                            x = Diccionario[dtTabla_Medicion.TableName].X;
                            y = Diccionario[dtTabla_Medicion.TableName].Y;
                            IEnumerable<DataRow> CampoInicial = (from pozo in dtTabla_Medicion.AsEnumerable() where (pozo.Field<string>(x) != null) orderby pozo.Field<string>(x) select pozo);

                            //     var queryCustomersByCity =         from cust in customers           group cust by cust.City;

                            var agrupados = from grupo in CampoInicial group grupo by grupo.Field<string>(x);

                            //     var queryCustomersByCity =         from cust in customers           group cust by cust.City;


                            DataTable temp = dtTabla_Medicion.Clone();

                            foreach (var ungrupo in agrupados)
                            {


                                try
                                {
                                    var nuevo = from pozo in ungrupo
                                                orderby Convert.ToDateTime(pozo.Field<string>(y))
                                                select pozo;

                                    temp.Merge(nuevo.CopyToDataTable());
                                }
                                catch
                                {

                                    temp.Merge(ungrupo.CopyToDataTable());
                                }

                            }
                            dtTabla_Medicion.Rows.Clear();
                            dtTabla_Medicion.Merge(temp);

                        }
                    }
                    catch { }
                


            }
      public   void OrdenaEvento()
          {

              try
              {

                  IEnumerable<DataRow> CampoInicial = from pozo in DtEventos.AsEnumerable().Where(p =>!string.IsNullOrWhiteSpace(p.Field<string>("Pozo"))) orderby pozo.Field<string>("Pozo") select pozo;


                  var agrupados = from grupo in CampoInicial group grupo by grupo.Field<string>("Pozo");

                  //     var queryCustomersByCity =         from cust in customers           group cust by cust.City;


                  DataTable temp = DtEventos.Clone();

                  foreach (var ungrupo in agrupados)
                  {
                      var nuevo = from pozo in ungrupo
                                  orderby Convert.ToDateTime(pozo.Field<string>("Fecha"))
                                  select pozo;
                      //foreach (DataRow r in nuevo)
                      //{
                      //    temp.ImportRow(r);
                      //}
                    temp.Merge(nuevo.CopyToDataTable());
                  }
                 
                  //DataTable temp = CampoInicial.CopyToDataTable(); 
                  DtEventos.Rows.Clear();
                  DtEventos.Merge(temp);
                                                                    
               

              }
              catch { }


          }
       public  List<List<ObjetoSelecionable>> PorGrupos()
        {
 List<List<ObjetoSelecionable>> SuperListas = new List<List<ObjetoSelecionable>>();
               
            try
            {

              //  IEnumerable<DataRow> CampoInicial = from pozo in dtGrupos.AsEnumerable() orderby pozo.Field<string>("Num") select pozo;


                var agrupados = from grupo in DtGrupos.AsEnumerable() group grupo by Convert.ToInt32( grupo["Num"]);

                //     var queryCustomersByCity =         from cust in customers           group cust by cust.City;


                foreach (var ungrupo in agrupados)
                {
                    List<ObjetoSelecionable> temp = new List<ObjetoSelecionable>();
                    foreach (DataRow r in ungrupo)
                    {
                        ObjetoSelecionable obj = new ObjetoSelecionable(r[1].ToString(), true);
                        obj.Color_ = Color.FromArgb(Convert.ToInt32(r[2]));
                        temp.Add(obj);
                    }
                    SuperListas.Add(temp);
                }

                


            }
            catch { }
return SuperListas;

        }
            public DataTable Variables()
            {

                DataTable Elementos = new DataTable("ListaVariables");
                Elementos.Columns.Add("Variable");
                Elementos.Columns.Add("Origen");
                Elementos.Columns.Add("Pos");
                Elementos.Namespace = "Otros";
                DataRow Reglon;

                foreach (ObjetoSelecionable tabla_ver in TablasUtilizar)
                {
                    if (tabla_ver.Estado)
                    {
                        //aqui deberiamos de quitar la columna pozo.
                        string Nametable = tabla_ver.ToString();
                        Point punto = PosicionDiccionario(tabla_ver.objeto.ToString());
                        if (punto.X
                            > -1)
                        {
                            string nombre = dtTablas_Informacion.Tables[Nametable].Columns[punto.X].ColumnName;
                            foreach (DataColumn Col in dtTablas_Informacion.Tables[Nametable].Columns)
                            {
                                if (Col.ColumnName != nombre)
                                {
                                    Reglon = Elementos.NewRow();

                                    Reglon[0] = Col.ColumnName;
                                    Reglon[1] = dtTablas_Informacion.Tables[Nametable].TableName;
                                    Reglon[2] = Col.Ordinal;
                                    Elementos.Rows.Add(Reglon);
                                }
                            }
                        }
                    }
                }

              
                return Elementos;
            }
            
            public List<string> Variables_Lista(string TablaInfo)
            {
           
                List<string> Elementos = new List<string>();
                if (! dtTablas_Informacion.Tables.Contains(TablaInfo))
                    return Elementos;
                  if (dtTablas_Informacion.Tables[TablaInfo].Namespace != "PlantillasTabla"  )

                      foreach (DataColumn Col in dtTablas_Informacion.Tables[TablaInfo].Columns)
                        {


                            Elementos.Add(Col.ColumnName);

                        }

                
                return Elementos;
            }
          
            public DataTable TablaXY(string NombrePozo, String origen, string ColX, string ColY)
            {

  DataTable L = new DataTable();// dtTablas_Informacion.Tables[origen].Clone();
                L.Columns.Add(ColX);
                L.Columns.Add(ColY);
                L.Columns.Add("Pozo");
                if (NombrePozo != "")
                {

                    IEnumerable<DataRow> Selecionado = from Tabla in dtTablas_Informacion.Tables[origen].AsEnumerable().Where(p => p.Field<string>(Diccionario[origen].X) != null).Where(p => p.Field<string>(Diccionario[origen].X).ToUpper() == NombrePozo.ToUpper()) select Tabla;



                    var Query = from product in Selecionado.AsEnumerable()
                                select new
                                    {
                                        X = product[ColX],
                                        Y = product[ColY],


                                    };




                    DataRow Reglon;
                    foreach (var p in Query)
                    {
                        //     MessageBox.Show(p.Field<string>(dtTabla_Medicion.Columns[0].ColumnName));
                        //    L.ImportRow(p);
                        Reglon = L.NewRow();
                        Reglon[0] = p.X;

                        Reglon[1] = p.Y;
                        Reglon[2] = NombrePozo;
                        L.Rows.Add(Reglon);
                    }
                }

               
                return L;
            }

            public DataTable TablaXY(String origen, string ColX, string ColY,DataTable tablaAbuscar)
            {

                DataTable L = new DataTable();// dtTablas_Informacion.Tables[origen].Clone();
                L.Columns.Add(ColX);
                L.Columns.Add(ColY);
                L.Columns.Add("Pozo");
            
                {

                    IEnumerable<DataRow> Selecionado = from Tabla in tablaAbuscar.AsEnumerable().Where(p => p.Field<string>(Diccionario[origen].X) != null) select Tabla;



                    var Query = from product in Selecionado.AsEnumerable()
                                select new
                                {
                                    X = product[ColX],
                                    Y = product[ColY],


                                };




                    DataRow Reglon;
                    foreach (var p in Query)
                    {
                        //     MessageBox.Show(p.Field<string>(dtTabla_Medicion.Columns[0].ColumnName));
                        //    L.ImportRow(p);
                        Reglon = L.NewRow();
                        Reglon[0] = p.X;

                        Reglon[1] = p.Y;
                        Reglon[2] = "";
                        L.Rows.Add(Reglon);
                    }
                }


                return L;
            }


            public UnidadChan TablaXY_Fecha(string NombrePozo, String origen, string ColX, string ColY)
            {

                UnidadChan Chan = new UnidadChan();

                IEnumerable<DataRow> Selecionado_1 = from Tabla in dtTablas_Informacion.Tables[origen].AsEnumerable().Where(p => p.Field<string>(Diccionario[origen].X) != null).Where(p => p.Field<string>(ColX) != null).Where(p => p.Field<string>(ColY) != null).Where(p => p.Field<string>(Diccionario[origen].X).ToUpper() == NombrePozo.ToUpper()) select Tabla;
                DateTime tiempo = DateTime.Now;
                IEnumerable<DataRow> Selecionado = from Tab in Selecionado_1.AsEnumerable().Where(p => DateTime.TryParse(p.Field<string>(Diccionario[origen].Y), out tiempo)) select Tab;
                DataTable L = new DataTable();// dtTablas_Informacion.Tables[origen].Clone();
                L.Columns.Add("Fecha");
                L.Columns.Add(ColX);
                L.Columns.Add(ColY);
                int x = Diccionario[dtTablas_Informacion.Tables[origen].TableName].Y;

                var Query = from product in Selecionado.AsEnumerable()
                            select new
                            {
                                w= product[x],
                                X = product[ColX],
                                Y = product[ColY],
                                

                            };




                DataRow Reglon;
                foreach (var p in Query)
                {
                    //     MessageBox.Show(p.Field<string>(dtTabla_Medicion.Columns[0].ColumnName));
                    //    L.ImportRow(p);
                    try
                    {
                        if (Convert.ToDouble(p.X) > 0)
                        {
                            Reglon = L.NewRow();
                            Reglon[0] = p.w;
                            Reglon[1] = p.X;
                            Reglon[2] = p.Y;
                            L.Rows.Add(Reglon);
                        }
                    }
                    catch (Exception ex)
                    {
                       // System.Windows.Forms.MessageBox.Show(ex.Message+"  "+ p.X.ToString());
                       // return
                    }
                }

                Chan.Tablas = L;
                return Chan;
            }

            public UnidadChan Inicio_Fin_x_Pozo(string NombrePozo, String origen, string ColX, string ColY)
            {

                UnidadChan Chan = new UnidadChan();

                IEnumerable<DataRow> Selecionado = from Tabla in dtTablas_Informacion.Tables[origen].AsEnumerable().Where(p => p.Field<string>(Diccionario[origen].X) != null).Where(p => p.Field<string>(ColX) != null).Where(p => p.Field<string>(ColY) != null).Where(p => p.Field<string>(Diccionario[origen].X).ToUpper() == NombrePozo.ToUpper()) select Tabla;


                    System.Nullable<DateTime> Inicio =
    (from emp in Selecionado.AsEnumerable() 
    select emp.Field<DateTime>(Diccionario[dtTablas_Informacion.Tables[origen].TableName].Y))    .Min();


                    Chan.Inicio = Inicio.Value;
                System.Nullable<DateTime> Fin =
    (from emp in Selecionado.AsEnumerable() 
    select emp.Field<DateTime>(Diccionario[dtTablas_Informacion.Tables[origen].TableName].Y))    .Max();
                Chan.Fin = Fin.Value;


                return Chan;
            }

            public UnidadChan Inicio_Fin_x_Pozo(string NombrePozo, String origen)
            {

                UnidadChan Chan = new UnidadChan();
               DateTime tiempo = DateTime.Now;
                IEnumerable<DataRow> Selecionado_1 = from Tabla in dtTablas_Informacion.Tables[origen].AsEnumerable().Where(p => p.Field<string>(Diccionario[origen].X) != null).Where(p => p.Field<string>(Diccionario[origen].X).ToString() != "").Where(p => p.Field<string>(Diccionario[origen].Y) != null).Where(p => p.Field<string>(Diccionario[origen].Y).ToString() != "").Where(p => p.Field<string>(Diccionario[origen].X).ToUpper() == NombrePozo.ToUpper()) select Tabla;
                IEnumerable<DataRow> Selecionado = from Tab in Selecionado_1.AsEnumerable().Where(p => DateTime.TryParse(p.Field<string>(Diccionario[origen].Y), out tiempo)) select Tab;

                System.Nullable<DateTime> Inicio =
(from emp in Selecionado.AsEnumerable()
 select Convert.ToDateTime( emp.Field<string>(Diccionario[dtTablas_Informacion.Tables[origen].TableName].Y))).Min();


                Chan.Inicio = Inicio.Value;
                System.Nullable<DateTime> Fin =
    (from emp in Selecionado.AsEnumerable()
     select Convert.ToDateTime( emp.Field<string>(Diccionario[dtTablas_Informacion.Tables[origen].TableName].Y))).Max();
                Chan.Fin = Fin.Value;


                return Chan;
            }
            DataTable Ordena(DataTable dt, int Col)
            {


                //  Ver(dt);
                IEnumerable<DataRow> Selecionado = from Tabla in dt.AsEnumerable().Where(p => p.Field<DateTime>(Col) != null) orderby Tabla[Col] descending select Tabla;
                //  Ver(Selecionado.CopyToDataTable());
                return Selecionado.CopyToDataTable();
            }

            public DataTable EventosxPozo(string NombrePozo)
            {
                    DataTable L = new DataTable();// dtTablas_Informacion.Tables[origen].Clone();
                    L.Columns.Add("X", Type.GetType("System.DateTime"));
                    L.Columns.Add("Y");
                    L.Columns.Add("Z");
                try
                {

                    IEnumerable<DataRow> Activados = from rows in DtEventos.AsEnumerable() .Where(p=> p.Field<bool>("Visible")!=null) where (Convert.ToBoolean(rows["Visible"])) select rows;

                    IEnumerable<DataRow> Pozo_sinNull = from Tabla in Activados.AsEnumerable().Where(p => !String.IsNullOrWhiteSpace(p.Field<string>("Pozo"))) where (Tabla["Pozo"].ToString().ToUpper() == NombrePozo.ToUpper()) select Tabla;



                    IEnumerable<DataRow> Selecionado = from reglon in Pozo_sinNull.AsEnumerable() where (!String.IsNullOrWhiteSpace(reglon["Fecha"].ToString())) select reglon;



                    //var Query = from product in Selecionado.AsEnumerable()
                    //            select new
                    //            {

                    //                X = Convert.ToDateTime(product["Fecha"]),
                    //                Y = product["Evento"],
                    //                Z = product["Abreviacion"],


                    //            };




                    DataRow Reglon;
                    foreach (var p in Selecionado)
                    {
                        //     MessageBox.Show(p.Field<string>(dtTabla_Medicion.Columns[0].ColumnName));
                        //    L.ImportRow(p);
                        Reglon = L.NewRow();
                        Reglon["X"] = Convert.ToDateTime(p["Fecha"]) ;        //


                        Reglon["Y"] = p["Evento"];
                        Reglon["Z"] = p["Abreviacion"];
                        L.Rows.Add(Reglon);
                    }
                    if (L.Rows.Count > 0)
                        L = Ordena(L, 0);
                    //   Ver.Ver2(L);
                  
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);

                }  return L;

            }
            internal void GuardaCambios()
            {
                DtEventos.AcceptChanges();
                dtTablas_Informacion.AcceptChanges();
            }
            internal void NoGuardaCambios()
            {
                DtEventos.RejectChanges();
                dtTablas_Informacion.RejectChanges();
            }

           
            internal UnidadChan IniciFin(string p,string t)
            {
                return Inicio_Fin_x_Pozo(p,t);                       
            }

            internal bool Esfecha(string p, string p_2)
            {
               //if(p_2.StartsWith("P_"))
               //    p_2= p_2.Remove(0,2);
               // //exites la tabla
                if (!dtTablas_Informacion.Tables.Contains(p))
                    return false;

                Point punto = Diccionario[p];
                if (punto.Y == -1)
                    return false;
                if (dtTablas_Informacion.Tables[p].Columns[punto.Y].ColumnName == p_2)
                    return true;

                return false;
            }

            internal void RevisaDiccionario()
            {
                var lista = new List<ObjetoSelecionable>();
                foreach (var clave in TablasUtilizar)
                {
                    if (!dtTablas_Informacion.Tables.Contains(clave.ToString()))
                    {
                        lista.Add(clave);
                    }
                }

                foreach (var clave in lista)
                {
                    TablasUtilizar.Remove(clave);
                }

                //modifiquemos ese diccioanrio.


               


            }
        }

       

        //variable de la clase principal.
        public Unidad_Graficacion_Pozo Unidad;
        public void Clear()
        {
            try
            {
                foreach (DataTable det in Unidad.dtTablas_Informacion.Tables)
                    det.Dispose();

            }
            catch
            { }
        }

    }
 public class Graficacion_Serie
        {
            /// <summary>
            /// Contructor de la clase
            /// </summary>
            public Graficacion_Serie()
            {
                ColorGrafica = Color.Black;
                TipoGrafica = SGraphPlottingMethods.Line;
                EsfechaX = false;
                Eje = "Y1";
            }
            public Graficacion_Serie Clone()
            {
                Graficacion_Serie Temp= new Graficacion_Serie ();
                if(Informacion!=null)
                Temp. Informacion= Informacion.Clone();
            Temp. ColorGrafica= ColorGrafica;
            Temp. NombrePozo=NombrePozo;
            Temp. SubFiltro=SubFiltro;
           Temp.TipoGrafica=TipoGrafica;
         Temp. TGrafica=TGrafica;
           Temp.EsfechaX=EsfechaX;
            Temp. Eje=Eje;
           Temp.Origen=Origen;
            Temp. Variable=Variable;
            Temp. Indextabla= Indextabla;

                return Temp;
            }
            public override string ToString()
            {
                StringBuilder cadena = new StringBuilder();
                cadena.Append(Origen);
                cadena.Append(".");
                cadena.Append(Variable);
                return cadena.ToString();
            }
            public void PasarAReglon(DataRow Reglon, bool Direcion)
            {
                try
                {
                    if (Direcion)
                    {
                        Reglon[0] = Origen;
                        Reglon[1] = Variable;
                        Reglon[2] = Indextabla;
                        Reglon[3] = ColorGrafica.ToArgb();
                        Reglon[4] = Convert.ToInt32(TipoGrafica);
                        Reglon[5] = TGrafica;
                        Reglon[6] = Eje;
                    }
                    else
                    {
                        Origen = Reglon[0].ToString();
                        Variable = Reglon[1].ToString();
                        Indextabla = Convert.ToInt32(Reglon[2]);
                        ColorGrafica = Color.FromArgb(Convert.ToInt32(Reglon[3].ToString()));
                        TipoGrafica = (SGraphPlottingMethods)Convert.ToInt32(Reglon[4]);
                        TGrafica = Reglon[5].ToString();
                        Eje = Reglon[6].ToString();

                    }
                }
                catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.Message); }
            }

            #region Variables
            /// <summary>
            /// Contendra la informacion.
            /// </summary>
            public DataTable Informacion;
            public Color ColorGrafica;
            public string NombrePozo;
            public string SubFiltro;
            public SGraphPlottingMethods TipoGrafica;
            public string TGrafica;
            public bool EsfechaX;
            public string Eje;
            public string Origen;
            public string Variable;
            public int Indextabla;
            #endregion


          
        }
       public class MiPlantilla
        {

            public List<Graficacion_Serie> Serie;
            public string NombrePlantilla;
            public string Tooltip;
            public MiPlantilla(List<Graficacion_Serie> SeriePlantilla)
            {
                // TODO: Complete member initialization
                // this.SeriePlantilla = SeriePlantilla;
                Serie = new List<Graficacion_Serie>();
                string[] EjeY = new string[] { "Y1", "Y2", "Y3", "Y4", "Y5" };
                Serie.Add(SeriePlantilla[0]);
                foreach (string eje in EjeY)
                    for (int i = 1; i < SeriePlantilla.Count; i++)
                    {
                        if (SeriePlantilla[i].Eje == eje)
                        {
                            Serie.Add(SeriePlantilla[i]);
                        }

                    }
               Tooltip = NombrePlantilla = "P_";

            }

            public MiPlantilla(List<Graficacion_Serie> SeriePlantilla, string Name)
            {
                // TODO: Complete member initialization
                // this.SeriePlantilla = SeriePlantilla;
                Serie = new List<Graficacion_Serie>();
                string[] EjeY = new string[] { "Y1", "Y2", "Y3", "Y4", "Y5" };
                Serie.Add(SeriePlantilla[0]);
                foreach (string eje in EjeY)
                    for (int i = 1; i < SeriePlantilla.Count; i++)
                    {
                        if (SeriePlantilla[i].Eje == eje)
                        {
                            Serie.Add(SeriePlantilla[i]);
                        }

                    }
                NombrePlantilla = Name;
                if (Name.Trim().Length <= 0)
                {
                    NombrePlantilla = Serie[0].Origen + "_" + Serie[0].Variable;
                }
                Tooltip = NombrePlantilla;

            }
            public MiPlantilla(DataTable TablaPlantilla)
            {
                // TODO: Complete member initialization
                // this.SeriePlantilla = SeriePlantilla;
                Serie = new List<Graficacion_Serie>();

                NombrePlantilla = TablaPlantilla.TableName;
                foreach (DataRow reglon in TablaPlantilla.Rows)
                {
                    Graficacion_Serie S = new Graficacion_Serie();
                    S.PasarAReglon(reglon, false);
                    Serie.Add(S);
                }
            }
            /// <summary>
            /// Aqui guradremos toda la plantilla en un datatable
            /// </summary>
            public DataTable tranformaDatatable()
            {
                //creamos la tabla
                if (!NombrePlantilla.StartsWith("P_"))
                    NombrePlantilla = "P_" + NombrePlantilla;
                DataTable VistaPlantilla = new DataTable(NombrePlantilla);
                VistaPlantilla.Columns.Add("Origen");
                VistaPlantilla.Columns.Add("Variable");
                VistaPlantilla.Columns.Add("Indextabla");
                VistaPlantilla.Columns.Add("ColorGrafica");
                VistaPlantilla.Columns.Add("TipoGrafica");
                VistaPlantilla.Columns.Add("TGrafica");
                VistaPlantilla.Columns.Add("Eje");
                DataRow reglon;
                foreach (Graficacion_Serie s in Serie)
                {
                    reglon = VistaPlantilla.NewRow();
                    s.PasarAReglon(reglon, true);
                    VistaPlantilla.Rows.Add(reglon);
                }
                //One_Registro_Presion.VistaPrevia  vp= new One_Registro_Presion.VistaPrevia(VistaPlantilla);
                //vp.ShowDialog();
                return VistaPlantilla;
            }
            ~MiPlantilla()
            {
                Serie.Clear();

            }
            string Nombre()
            {
                //primero la Variable X.

                StringBuilder cadena = new StringBuilder();
                cadena.Append(Serie[0].Origen + "." + Serie[0].Variable + " / ");

                for (int i = 1; i < Serie.Count; i++)
                    cadena.Append(Serie[i].Origen + "." + Serie[i].Variable);
                return cadena.ToString();
            }
            public override string ToString()
            {
                return Nombre();
            }

        }

}
