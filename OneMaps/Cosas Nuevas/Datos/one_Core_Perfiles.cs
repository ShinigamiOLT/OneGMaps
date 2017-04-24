using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using One_Produccion;
using One_Produccion.Editar_Tabla;

namespace Maps
{
    public class One_Core_Perfiles : origenCore
    {

        #region Variable
        public DataSet Core;
        public string NombreFile;
        public double NMDva;
        public string Prescom = "Prescom";
        public string Registro = "Registro";

        string[] Campos = new string[] {    "Pozo","Fecha","Edo. Pozo","Profundidad (m)."	,"Temperatura ºC","Temperatura ºF",	"Presion (Lbs/Pulg2)",	"Presion (Kg/Cm2)"	,"Inc.Presion (Kg/Cm2)",	"Gradiente (Kg/Cm2/M)" ,"Int. Perforado" ,"Estrangulador (in)"
};

        string[] CamposAEliminar = new string[] { "Num.", "Prof. Interior", "Tub. De Produccion", "Sonda De Memoria No", "Temp.Fondo Y Boca Del Pozo", "Inicio De Registro", "Archivo" };

        string[] CampoPrescom = new string[] { 
             "Pozo",   "Fecha",
 "Tipo de Prueba",
"UE Profundidad [mD]",
"UE Profundidad [mV]",
"UE Presión [kg/cm2]",
 "Gradiente [kg/cm2/m]",
 "Temperatura [°C]",
"NMD Profundidad [mD]",
"NMD Profundidad [mV]",
"NMD Presión [kg/cm2]",
 "M.R. [m]",
 "Presión @ P.R. [kg/cm2]",
 "Presión @ Calculada NMD [kg/cm2]"
        };

        string[] CamposReales = new string[] { "Temp.Fondo Y Boca Del Pozo", "Profundidad (m).", "Presion (Lbs/Pulg2)", "Presion (Kg/Cm2)", "Inc.Presion (Kg/Cm2)", "Gradiente (Kg/Cm2/M)" };
        string[] CamposBasura = new string[] { "Temp.Fondo Y Boca Pozo", "Profundidad M.", "Presion Lbs/Pulg2", "Presion Kg/Cm2", "Inc.Presion Kg/Cm2.", "Gradiente Kg/Cm2/M" };

        public List<DataTable> tabla_ahora;
        #endregion
        public One_Core_Perfiles()
        {
            Core = new DataSet("REGISTRO_1");

            DataTable Registro_ = new DataTable(Registro);
            DataTable prescom_ = new DataTable(Prescom);
            Core.Tables.Add(Registro_);
            Core.Tables.Add(prescom_);
            NombreFile = "Nuevo";
            ValidaP();
            ValidaR();
            tabla_ahora = new List<DataTable>();


        }
        public DataSet Guarda_Todo()
        {

            return Core;
        }
        public DataTable TablaXY(string NombrePozo, String origen, string ColX, string ColY, string ColZ)
        {



            IEnumerable<DataRow> Selecionado = from Tabla in Core.Tables[Prescom].AsEnumerable().Where(p => p.Field<string>(0) != null).Where(p => p.Field<string>(0).ToUpper() == NombrePozo.ToUpper()) select Tabla;

            DataTable L = new DataTable();// dtTablas_Informacion.Tables[origen].Clone();
            L.Columns.Add(ColX);
            L.Columns.Add(ColY);
            L.Columns.Add(ColZ);
            L.Columns.Add("Tipo Prueba");


            var Query = from product in Selecionado.AsEnumerable()
                        select new
                        {
                            X = product[ColX],
                            Y = product[ColY],
                            Y1 = product[ColZ],
                            Z = product[2]

                        };




            DataRow Reglon;
            foreach (var p in Query)
            {
                Reglon = L.NewRow();
                Reglon[0] = p.X;

                Reglon[1] = p.Y;
                Reglon[2] = p.Y1;
                Reglon[3] = p.Z;
                L.Rows.Add(Reglon);
            }
            return L;
        }

        public DataTable TablaXY(DataTable T, string NombrePozo, String origen, string ColX, string ColY, List<string> CamposAdicionales)
        {
            DataTable L = new DataTable(T.TableName);// dtTablas_Informacion.Tables[origen].Clone();


            if (NombrePozo != "")
            {
                IEnumerable<DataRow> Selecionado = from Tabla in T.AsEnumerable().Where(p => p.Field<string>(0) != null).Where(p => p.Field<string>(0).ToUpper() == NombrePozo.ToUpper()) select Tabla;

                L.Columns.Add(ColX);
                L.Columns.Add(ColY);
                DataRow Reglon;
                foreach (string col in CamposAdicionales)
                {
                    L.Columns.Add(col);
                }
                foreach (var p in Selecionado)
                {
                    Reglon = L.NewRow();
                    Reglon[0] = p[ColX];
                    Reglon[1] = p[ColY];
                    foreach (string col in CamposAdicionales)
                    {
                        Reglon[col] = p[col];
                    }
                    L.Rows.Add(Reglon);
                }



            }
            else
            {
                IEnumerable<DataRow> Selecionado = from Tabla in T.AsEnumerable().Where(p => p.Field<string>(0) != null) select Tabla;

                L.Columns.Add(ColX);
                L.Columns.Add(ColY);
                DataRow Reglon;
                foreach (string col in CamposAdicionales)
                {
                    L.Columns.Add(col);
                }
                foreach (var p in Selecionado)
                {
                    Reglon = L.NewRow();
                    Reglon[0] = p[ColX];
                    Reglon[1] = p[ColY];
                    foreach (string col in CamposAdicionales)
                    {
                        Reglon[col] = p[col];
                    }
                    L.Rows.Add(Reglon);
                }


            }



            return L;

        }



        public DataTable Info2(DataTable P, string ValorPrimario, int X, string ValorSecundario, int X2)
        {

            IEnumerable<DataRow> Selecionado = from Tabla in P.AsEnumerable().Where(p => p.Field<string>(X) != null).Where(p => p.Field<string>(X).ToUpper() == ValorPrimario.ToUpper()).Where(p => p.Field<string>(X2).ToLower() == ValorSecundario.ToLower()) select Tabla;
            if (Selecionado.Count() > 0)

                return Selecionado.CopyToDataTable();
            return P.Clone();
        }


        public double NMD(string Pozo)
        {
            DataTable t = TablaXY(Pozo, Prescom, "Fecha", "NMD Profundidad [mD]", "NMD Profundidad [mV]");

            Visor_Datos LD = new Visor_Datos();
            LD.EstableceDatos(t);
            LD.AutoColum();
            LD.ShowDialog();
            //deosues de Mostrar sacaremos el Valor que hay ahi.
            try
            {
                if (LD.IndexRow > 0)
                {
                    if (LD.Col == 2 && t.Rows[LD.IndexRow][2] != null && t.Rows[LD.IndexRow][2].ToString() != "")

                        return Convert.ToDouble(t.Rows[LD.IndexRow][2]);
                    return Convert.ToDouble(t.Rows[LD.IndexRow][1]);
                }
            }
            catch { }
            return 0;
        }

        public void LeerXML(string direccion)
        {
            try
            {
                Limpia();

                DataSet temp = new DataSet();


                if (File.Exists(direccion))
                {
                    temp.ReadXml(direccion);
                    NombreFile = direccion;
                    if (temp.Tables.Count > 0)
                    {

                        if (temp.Tables.Contains("Prescom"))
                        {
                            Core.Tables.Remove(Prescom);
                            Core.Tables.Add(temp.Tables["Prescom"].Copy());
                            Prescom = temp.Tables["Prescom"].TableName;
                        }

                        if (temp.Tables.Contains("Registro  Presion UE."))
                        {
                            Core.Tables.Remove(Prescom);
                            Core.Tables.Add(temp.Tables["Registro  Presion UE."].Copy());
                            Prescom = temp.Tables["Registro  Presion UE."].TableName;
                        }

                        if (temp.Tables.Contains("RPFC y RPFF"))
                        {
                            Core.Tables.Remove(Registro);
                            Core.Tables.Add(temp.Tables["RPFC y RPFF"].Copy());
                            Registro = temp.Tables["RPFC y RPFF"].TableName;
                        }
                        if (temp.Tables.Contains("Table1"))
                        {
                            Core.Tables.Remove(Registro);
                            Core.Tables.Add(temp.Tables["Table1"].Copy());
                            Registro = temp.Tables["Table1"].TableName;
                        }
                        else
                        {
                            if (temp.Tables.Contains("REGISTRO"))
                            {
                                Core.Tables.Remove(Registro);
                                Core.Tables.Add(temp.Tables["REGISTRO"].Copy());
                                Registro = temp.Tables["REGISTRO"].TableName;

                            }
                        }
                    }
                }

                ValidaP();
                ValidaR();
                Renombrar();

            }
            catch (Exception Ex) { MessageBoxEx.Show(Ex.Message); }

        }

        public List<DataTable> Graficar_por_pozo(string NombrePozo)
        {

            List<DataTable> Lista_Pozo_x_fecha = new List<DataTable>();


            IEnumerable<DataRow> Selecionado_1 = from Tabla in Core.Tables[Registro].AsEnumerable().Where(p => p.Field<string>("Pozo") != null).Where(p => p.Field<string>("Pozo").ToUpper() == NombrePozo.ToUpper()) select Tabla;
            DateTime tiempo = DateTime.Now;



            var agrupados = from grupo in Selecionado_1 group grupo by grupo.Field<string>("Edo. Pozo"); // aqui separra lso tipos de pozos o abiertos o cerrados.

            foreach (var ungrupo in agrupados)  //aqui recorre para tener la lista de abierto y cerrados
            {
                var nuevo = from pozo in ungrupo.Where(p => DateTime.TryParse(p.Field<string>("Fecha"), out tiempo))
                            orderby Convert.ToDateTime(pozo.Field<string>("Fecha"))
                            select pozo;

                var porfechas = from grupo in nuevo group grupo by grupo.Field<string>("Fecha");

                foreach (var porunafecha in porfechas)
                {
                    //como ya lo tengo por poxo. por tipo y por fecha
                    DataTable temp = porunafecha.CopyToDataTable();
                    temp.Namespace = ungrupo.Key;
                    temp.TableName = porunafecha.Key;
                    Lista_Pozo_x_fecha.Add(temp);
                }




            }

            return Lista_Pozo_x_fecha;


        }

        public List<string> RetornaPozosGlobal()
        {
            List<string> combopozo = new List<string>();
            try
            {


                try
                {


                    IEnumerable<string> CampoInicial =
                     (from pozo in Core.Tables[Prescom].AsEnumerable().Where(p => p.Field<string>(0) != null) select pozo.Field<string>(0).ToUpper()).Distinct();


                    foreach (string p in CampoInicial)
                    {
                        if (!combopozo.Contains(p))
                            combopozo.Add(p);

                    }
                    IEnumerable<string> CampoInicial2 =
                     (from pozo in Core.Tables[Registro].AsEnumerable().Where(p => p.Field<string>("Pozo") != null) select pozo.Field<string>("Pozo").ToUpper()).Distinct();


                    foreach (string p in CampoInicial2)
                    {
                        if (!combopozo.Contains(p))
                            combopozo.Add(p);

                    }

                }
                catch { }

            }
            catch { }
            return combopozo;
        }

        public List<string> RetornaPozos()
        {
            List<string> combopozo = new List<string>();
            try
            {


                try
                {


                    IEnumerable<string> CampoInicial =
                     (from pozo in Core.Tables[Prescom].AsEnumerable().Where(p => p.Field<string>(0) != null) select pozo.Field<string>(0).ToUpper()).Distinct();


                    foreach (string p in CampoInicial)
                    {
                        if (!combopozo.Contains(p))
                            combopozo.Add(p);

                    }

                }
                catch { }

            }
            catch { }
            return combopozo;
        }
        public List<string> RetornaPozosR()
        {
            List<string> combopozo = new List<string>();
            try
            {


                try
                {


                    IEnumerable<string> CampoInicial =
                     (from pozo in Core.Tables[Registro].AsEnumerable().Where(p => p.Field<string>("Pozo") != null) select pozo.Field<string>("Pozo").ToUpper()).Distinct();


                    foreach (string p in CampoInicial)
                    {
                        if (!combopozo.Contains(p))
                            combopozo.Add(p);

                    }

                }
                catch { }

            }
            catch { }
            return combopozo;
        }
        public List<string> RetornaPozos(int i)
        {
            List<string> combopozo = new List<string>();
            try
            {


                try
                {


                    IEnumerable<string> CampoInicial =
                     (from pozo in Core.Tables[Prescom].AsEnumerable().Where(p => p.Field<string>(i) != null) select pozo.Field<string>(i).ToUpper()).Distinct();


                    foreach (string p in CampoInicial)
                    {
                        if (!combopozo.Contains(p))
                            combopozo.Add(p);

                    }

                }
                catch { }

            }
            catch { }
            return combopozo;
        }
        void ValidaP()
        {


            Hola(Core.Tables[Prescom], CampoPrescom, false);

            //aqui veremos si esta en el dataset o no.

        }
        void ValidaR()
        {


            Hola(Core.Tables[Registro], Campos, true);

        }
        public void Hola(DataTable Tabla_Temp, string[] ListaCampos, bool num_es)
        {
            int cont = 0;

            //antes que nada buscaremos esa basura y le pondremos los nombres reales
            if (Tabla_Temp == Core.Tables[Registro])
            {
                for (int k = 0; k < CamposBasura.Length; k++)
                {
                    if (Core.Tables[Registro].Columns.Contains(CamposBasura[k]))
                    {
                        Core.Tables[Registro].Columns[CamposBasura[k]].ColumnName = CamposReales[k];

                    }
                }
                //aqui eliminaremos las columnas que no usamos;

                foreach (string coleliminar in CamposAEliminar)
                {
                    if (Core.Tables[Registro].Columns.Contains(coleliminar))
                    {
                        Core.Tables[Registro].Columns.Remove(coleliminar);
                    }

                }
            }

            foreach (DataColumn col in Tabla_Temp.Columns)
            {
                col.SetOrdinal(cont);
                cont++;

            }
            cont = 0;


            foreach (DataColumn col in Tabla_Temp.Columns)
            {
                col.SetOrdinal(cont);
                cont++;

            }

            for (int i = 0; i < ListaCampos.Length; i++)
            {

                if (!Tabla_Temp.Columns.Contains(ListaCampos[i]))
                {
                    Tabla_Temp.Columns.Add(ListaCampos[i]);
                    Tabla_Temp.Columns[Tabla_Temp.Columns.Count - 1].SetOrdinal(cont);
                    cont++;
                }

            }

        }

        //aqui mandamos a actualizar cada valor para obtenerlo por pozo
        public void NMD_X_Pozo(string Pozo)
        {
            double A = 0, B = 0, C = 0, D = 0;
            int i = 0;
            foreach (DataRow reglon in Core.Tables[Prescom].Rows)
            {
                //guardar cada Valor en la celda "NMD Compracion"
                try
                {
                    if (reglon[0].ToString().ToUpper() == Pozo)
                    {
                        if (reglon[5] != null && reglon[5].ToString() != "")
                            B = Convert.ToDouble(reglon[5]);
                        if (reglon[6] != null && reglon[6].ToString() != "")
                            C = Convert.ToDouble(reglon[6]);
                        i++;
                        //zqui la 3 o 4

                        if (reglon[4] != null && reglon[4].ToString() != "")
                            A = Convert.ToDouble(reglon[4]);
                        else
                            if (reglon[3] != null && reglon[3].ToString() != "")
                                A = Convert.ToDouble(reglon[3]);

                        if (reglon[9] != null && reglon[9].ToString() != "")
                            D = Convert.ToDouble(reglon[9]);
                        else
                            if (reglon[8] != null && reglon[8].ToString() != "")
                                D = Convert.ToDouble(reglon[8]);

                        reglon["Presión @ Calculada NMD [kg/cm2]"] = (D - A) * C + B;

                    }


                }
                catch (Exception error) { }
            }
        }
        //aqui mandamos a actualizar cada valor para obtenerlo por pozo
        public void NMD_X_Pozo()
        {
            double A = 0, B = 0, C = 0, D = 0;
            int i = 0;
            NMDva = 0;

            foreach (DataRow reglon in Core.Tables[Prescom].Rows)
            {
                //guardar cada Valor en la celda "NMD Compracion"
                try
                {

                    if (reglon[5] != null && reglon[5].ToString() != "")
                        B = Convert.ToDouble(reglon[5]);
                    if (reglon[6] != null && reglon[6].ToString() != "")
                        C = Convert.ToDouble(reglon[6]);
                    i++;
                    //zqui la 3 o 4

                    if (reglon[4] != null && reglon[4].ToString() != "")
                        A = Convert.ToDouble(reglon[4]);
                    else
                        if (reglon[3] != null && reglon[3].ToString() != "")
                            A = Convert.ToDouble(reglon[3]);

                    if (reglon[9] != null && reglon[9].ToString() != "")
                        D = Convert.ToDouble(reglon[9]);
                    else
                        if (reglon[8] != null && reglon[8].ToString() != "")
                            D = Convert.ToDouble(reglon[8]);

                    reglon["Presión @ Calculada NMD [kg/cm2]"] = (D - A) * C + B;



                }
                catch (Exception error) { }

            }

        }

        //aqui mandamos a actualizar cada valor para obtenerlo por pozo
        public void NMD_X_Pozo_parametros()
        {
            double A = 0, B = 0, C = 0, D = 0;
            int i = 0;
            NMDva = 0;


            foreach (DataRow reglon in Core.Tables[Prescom].Rows)
            {
                //guardar cada Valor en la celda "NMD Compracion"
                try
                {

                    if (reglon[5] != null && reglon[5].ToString() != "")
                        B = Convert.ToDouble(reglon[5]);
                    if (reglon[6] != null && reglon[6].ToString() != "")
                        C = Convert.ToDouble(reglon[6]);
                    i++;
                    //zqui la 3 o 4

                    if (reglon[4] != null && reglon[4].ToString() != "")
                        A = Convert.ToDouble(reglon[4]);
                    else
                        if (reglon[3] != null && reglon[3].ToString() != "")
                            A = Convert.ToDouble(reglon[3]);

                    if (reglon[9] != null && reglon[9].ToString() != "")
                        D = Convert.ToDouble(reglon[9]);
                    else
                        if (reglon[8] != null && reglon[8].ToString() != "")
                            D = Convert.ToDouble(reglon[8]);

                    reglon["Presión @ Calculada NMD [kg/cm2]"] = (D - A) * C + B;



                }
                catch (Exception error) { }
            }

        }

        public void NMD_X_Pozo(double NMD)
        {
            double A = 0, B = 0, C = 0;
            this.NMDva = NMD;



            foreach (DataRow reglon in Core.Tables[Prescom].Rows)
            {
                //guardar cada Valor en la celda "NMD Compracion"
                try
                {
                    {
                        if (reglon[5].ToString() != "")
                            B = Convert.ToDouble(reglon[5]);
                        if (reglon[6].ToString() != "")
                            C = Convert.ToDouble(reglon[6]);

                        //zqui la 3 o 4

                        if (reglon[4].ToString() != "")
                            A = Convert.ToDouble(reglon[4]);
                        else
                            if (reglon[3].ToString() != "")
                                A = Convert.ToDouble(reglon[3]);

                        reglon["Presión @ Calculada NMD [kg/cm2]"] = (NMD - A) * C + B;

                    }

                }
                catch (Exception error) { }
            }

        }
        public void NMD_X_Pozo_X_Gradiente(double Gradiente, bool Valor_Unico)
        {
            double A = 0, B = 0, C = 0, D = 0;
            //  this.NMDva =   ;

            foreach (DataRow reglon in Core.Tables[Prescom].Rows)
            {
                //guardar cada Valor en la celda "NMD Compracion"
                try
                {
                    {
                        if (reglon[5].ToString() != "")
                            B = Convert.ToDouble(reglon[5]);
                        //if (reglon[6].ToString() != "")
                        //    C = Convert.ToDouble(reglon[6]);

                        //zqui la 3 o 4

                        if (reglon[4].ToString() != "")
                            A = Convert.ToDouble(reglon[4]);
                        else
                            if (reglon[3].ToString() != "")
                                A = Convert.ToDouble(reglon[3]);

                        if (Valor_Unico)
                        {
                            if (reglon[9] != null && reglon[9].ToString() != "")
                                D = Convert.ToDouble(reglon[9]);
                            else
                                if (reglon[8] != null && reglon[8].ToString() != "")
                                    D = Convert.ToDouble(reglon[8]);
                        }
                        else
                        {
                            D = NMDva;
                        }

                        reglon["Presión @ Calculada NMD [kg/cm2]"] = (D - A) * Gradiente + B;

                    }

                }
                catch (Exception error) { }
            }

        }
        public void EstablecePrescom(DataGridView x)
        {
            x.DataSource = null;
            //  x.Rows.Clear();
            //   ValidaP();
            x.SuspendLayout();
            x.DataSource = Prescom;
            x.ResumeLayout(true);

        }

        public void EstableceRegistro(DataGridView x)
        {
            x.DataSource = null;
            x.Rows.Clear();
            //  ValidaR();

            x.DataSource = Registro;
        }
        internal void Limpia()
        {
            foreach (DataTable t in Core.Tables)
                t.Clear();

            NombreFile = "Nuevo";

            ValidaP();
            ValidaR();
        }

        internal int PosicionCol(string p)
        {
            return Core.Tables[Registro].Columns[p].Ordinal;
        }

        internal void Renombrar()
        {
            Core.Tables[Prescom].TableName = "Registro  Presion UE.";
            Prescom = "Registro  Presion UE.";
            Core.Tables[Registro].TableName = "RPFC y RPFF";
            Registro = "RPFC y RPFF";
        }
    }

    
}
