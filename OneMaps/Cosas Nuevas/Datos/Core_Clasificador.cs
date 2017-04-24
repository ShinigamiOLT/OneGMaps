using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;

namespace Maps
{
    public class Core_Clasificador
    {
        //aqui sera mi contenedor principal de la clase

        public Core_Clasificador()
        {
            Origen = new Origen_Datos();
        }
        public Core_Clasificador(DataSet dsOrigen, string NameTable)
        {
            Origen = new Origen_Datos(dsOrigen, NameTable);

        }
        public Core_Clasificador(DataTable dsOrigentabla)
        {
            Origen = new Origen_Datos(dsOrigentabla);

        }
        public class Origen_Datos
        {
            string File;
            DataSet dsBase;
            public DataTable Tabla;
            public Origen_Datos()
            {
                dsBase = new DataSet();
            }
            public Origen_Datos(DataSet dsOrigen, string Name)
            {
                dsBase = dsOrigen;
                Tabla = dsOrigen.Tables[Name];

            }
            public Origen_Datos(DataTable dsOrigen)
            {

                Tabla = dsOrigen.Copy();

            }
            public void Lee(string File)
            {
                dsBase.ReadXml(File);
                this.File = File;
                if (dsBase.Tables.Count > 0)
                {
                    Tabla = dsBase.Tables[0];
                }
            }






            internal void ColocaDatos(System.Windows.Forms.DataGridView Tabla_Inicial)
            {
                if (dsBase != null)
                {
                    Tabla_Inicial.DataSource = dsBase;
                    Tabla_Inicial.DataMember = dsBase.Tables[0].TableName;
                }
                else
                {
                    Tabla_Inicial.DataSource = Tabla;
                }

            }
            internal DataTable TablaActual()
            {
                if (dsBase != null)
                {

                    return (dsBase.Tables[0]);
                }
                return Tabla;


            }
            //CamposDiferentes.
            public List<string> DistintosDeUnaColumna(string Columna)
            {
                List<string> combopozo = new List<string>();
                try
                {

                    //aqui rellenaremos pero 
                    try
                    {


                        IEnumerable<string> CampoInicial =
                             (from pozo in Tabla.AsEnumerable() select pozo[Columna].ToString()).Distinct();
                        /*
                          IEnumerable<string> CampoInicial =
                               (from pozo in Tabla.AsEnumerable().Where(p => p.Field<string>(Columna) != null) select pozo.Field<string>(Columna).ToUpper()).Distinct();

      
                         */

                        foreach (string p in CampoInicial)
                        {
                            if (!combopozo.Contains(p))
                                combopozo.Add(p);

                        }
                        // combopozo.Add(String.Empty);

                    }
                    catch { }

                }
                catch { }
                return combopozo;
            }

        }// de la clase
        public class ObjetoTablas
        {

            public DataTable Tabla_unidad;
            public int Num_Operaciones;
            public DataTable Tabla_ValoresCalculo;
            public string Cadena;
            public List<int> indices_Activados;
            public string ix,xoriginal;
            public string iy,yoriginal;
            
            public ObjetoTablas()
            {
                Tabla_unidad = new DataTable();

               xoriginal= ix = "IX";
               yoriginal= iy = "IY";


            }
             ~ObjetoTablas()
            {
                Tabla_unidad.Dispose();
                Tabla_ValoresCalculo.Dispose();
            }
            public int X
            {
                get { return Tabla_unidad.Columns[ix].Ordinal; }

            }
            public int Y
            {
                get { return Tabla_unidad.Columns[iy].Ordinal; }

            }
            public void Agrega(DataTable valores)
            {
                DataRow reglon;
                foreach (DataRow row in Tabla_unidad.Rows)
                {
                    reglon = valores.NewRow();
                    reglon[0] = row[ix];
                    reglon[1] = row[iy];
                    reglon[2] = Cadena;
                    valores.Rows.Add(reglon);

                }
            }
            public DataTable RegresaT( bool val )
            {
                if (val)
                    return RegresaTOrigen();
                DataTable valores = new DataTable();
                valores.Clear();
                valores.Columns.Add(ix);
                valores.Columns.Add(iy);
                DataRow reglon;
                foreach (DataRow row in Tabla_unidad.Rows)
                {
                    reglon = valores.NewRow();
                    reglon[0] = row[ix];
                    reglon[1] = row[iy];
                    valores.Rows.Add(reglon);

                }

                return valores;

            }
             DataTable RegresaTOrigen()
            {
                string x = xoriginal.Substring(1);
                string y = yoriginal.Substring(1);
                DataTable valores = new DataTable();
                valores.Clear();
                valores.Columns.Add(x);
                valores.Columns.Add(y);
                DataRow reglon;
                foreach (DataRow row in Tabla_unidad.Rows)
                {
                    reglon = valores.NewRow();
                    reglon[0] = row[x];
                    reglon[1] = row[y];
                    valores.Rows.Add(reglon);

                }

                return valores;

            }
            public DataTable Esquema()
        {
            DataTable esquema = new DataTable();

            esquema.Columns.Add("Operacion_");
            esquema.Columns.Add(xoriginal.Remove(0,1));
            esquema.Columns.Add(yoriginal.Remove(0, 1));
            esquema.Columns.Add(ix);
            esquema.Columns.Add(iy);
            return esquema;
         
        }
            public void AgregaIndex(string X, string Y)
            {

                xoriginal = X;
                yoriginal = Y;

                if (Tabla_unidad.Columns.Contains(ix))
                {

                    Tabla_unidad.Columns.Remove(ix);
                    ix = X;

                    //  
                }
                if (Tabla_unidad.Columns.Contains(iy))
                {

                    Tabla_unidad.Columns.Remove(iy);
                    //Tabla_unidad.Columns[iy].ColumnName = Y;
                    iy = Y;
                    // 
                }

                ix = X;
                Tabla_unidad.Columns.Add(ix);




                iy = Y;
                Tabla_unidad.Columns.Add(iy);

                foreach (DataRow reglon in Tabla_unidad.Rows)
                {
                    reglon[ix] = "";
                    reglon[iy] = "";
                }
            }

            public void CalcularIndexacion()
            {
                //en Nombre de la Columna 
                string ColumnaY = iy.Substring(1);
                string ColumnaX = ix.Substring(1);
                double PromedioX = 1, PromedioY = 1;
                try
                {

                    PromedioX = Convert.ToDouble(Tabla_ValoresCalculo.Rows[2][ColumnaX]);
                    PromedioY = Convert.ToDouble(Tabla_ValoresCalculo.Rows[2][ColumnaY]);
                }
                catch { }
                //como ya tengo los Valores ahora inicire la indexacion
                try
                {
                    foreach (DataRow reglon in Tabla_unidad.Rows)
                    {
                        if (reglon[ColumnaX] != null)
                        {
                            if (IsNumeric(reglon[ColumnaX].ToString()))
                                reglon[ix] = Math.Round((Numero(reglon[ColumnaX].ToString()) - 1) / PromedioX, 4);
                        }
                        if (reglon[ColumnaY] != null)
                        {
                            if (IsNumeric(reglon[ColumnaY].ToString()))
                                reglon[iy] = Math.Round((Numero(reglon[ColumnaY].ToString()) - 1) / PromedioY, 4);
                        }
                    }
                }
                catch { }

            }
            private bool IsNumeric(string number)
            {
                //chequemos si no es fecha
                //DateTime date = DateTime.Now)

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


            private void AplicaSuma1(string Campo, int Tam)
            {
                try
                {
                   
                    //modifiquemos esta parte para ver que tenemos
                    
                    var ValoresDoule = from rows_ in Tabla_unidad.AsEnumerable().Where(p =>!string.IsNullOrWhiteSpace (p[Campo].ToString() )) where (IsNumeric(rows_[Campo].ToString())) select rows_;

                    //ya tengo supuestamnte los valores que son nummeros


                    List<double> ValoresObtenidos = new List<double>();
                    foreach (DataRow rows_ in ValoresDoule)
                    {
                        if(rows_[Campo].ToString().Length>0)
                        ValoresObtenidos.Add(Numero(rows_[Campo].ToString()));
                    }
                    //aqui ya tenemos los numeros que necesitamos.
                    try
                    {
                        

                        if (ValoresObtenidos.Count > 0)
                        {
                            Tabla_ValoresCalculo.Rows[Tam][Campo] =  Math.Round(ValoresObtenidos.Sum(),4);//temp;
                            //pa el conteo
                            Tabla_ValoresCalculo.Rows[Tam + 1][Campo] = ValoresObtenidos.Count;// j;
                            Tabla_ValoresCalculo.Rows[Tam + 2][Campo] = Math.Round(ValoresObtenidos.Average(),4);// promedio
                            Tabla_ValoresCalculo.Rows[Tam + 3][Campo] = Math.Round( ValoresObtenidos.Min(),4);// minimo
                            Tabla_ValoresCalculo.Rows[Tam + 4][Campo] =  Math.Round(ValoresObtenidos.Max(),4);// maximo
                            Tabla_ValoresCalculo.Rows[Tam + 5][Campo] = Math.Round(DesviacionStadar(ValoresObtenidos, ValoresObtenidos.Average()), 4);// desviacion standar
                        }
                        else
                        {
                            //busquemos otra forma de calcular  
                           int contador= (from rows_ in Tabla_unidad.AsEnumerable().Where(p => !string.IsNullOrWhiteSpace(p[Campo].ToString()))
                                          where (!IsNumeric(rows_[Campo].ToString())) select rows_).Count();
                            Tabla_ValoresCalculo.Rows[Tam][Campo] = 0;//temp;
                            //pa el conteo
                            Tabla_ValoresCalculo.Rows[Tam + 1][Campo] = contador;// j;
                            Tabla_ValoresCalculo.Rows[Tam + 2][Campo] = 0;// promedio
                            Tabla_ValoresCalculo.Rows[Tam + 3][Campo] =0;// minimo
                            Tabla_ValoresCalculo.Rows[Tam + 4][Campo] =0;// maximo
                            Tabla_ValoresCalculo.Rows[Tam + 5][Campo] = 0;// desviacion standar
                     
                        }

                    }
                    catch (Exception Error) { MessageBox.Show(Error.Message); }

                    //pa la suma




                    /*
                        if (numero_N > 0)
                        {
                            //pa el promedio:
                            Tabla_ValoresCalculo.Rows[Tam + 2][Campo] = Math.Round(temp / numero_N, 4);

                            //pa el Minimo
                            Tabla_ValoresCalculo.Rows[Tam + 3][Campo] = Minimo;
                            //pa el Maximo
                            Tabla_ValoresCalculo.Rows[Tam + 4][Campo] = Maximo;
                            //pa la desviasion 
                            Tabla_ValoresCalculo.Rows[Tam + 5][Campo] = Math.Round(DesviacionStadar(ValoresObtenidos, temp / numero_N), 4);

                        }
                        else
                        {
                            //->cuando son letras o algo no cuantificable
                            //pa el promedio:
                            Tabla_ValoresCalculo.Rows[Tam + 2][Campo] = 0;

                            //pa el Minimo
                            Tabla_ValoresCalculo.Rows[Tam + 3][Campo] = 0;
                            //pa el Maximo
                            Tabla_ValoresCalculo.Rows[Tam + 4][Campo] = 0;
                            //pa la desviasion 
                            Tabla_ValoresCalculo.Rows[Tam + 5][Campo] = 0;

                        }*/
                }
                catch { }
            }
            private double DesviacionStadar(List<double> ValoresObtenidos, double Promedio)
            {
                double Sumatoria = 0;
                if (ValoresObtenidos.Count == 1)
                    return 0;
                for (int i = 0; i < ValoresObtenidos.Count; i++)
                {
                    Sumatoria += Math.Pow(ValoresObtenidos[i] - Promedio, 2);
                }


                return Math.Sqrt(Sumatoria / (ValoresObtenidos.Count - 1));
            }

            internal void Calculos()
            {
                //if (Tabla_ValoresCalculo != null)
                //    return;
                Tabla_ValoresCalculo = Tabla_unidad.Clone();
                if (!Tabla_ValoresCalculo.Columns.Contains("Operacion_"))
                    Tabla_ValoresCalculo.Columns.Add("Operacion_");
                string[] NombreCalculos = new string[] { ".SUM", ".CONT", ".PRO", ".MIN", ".MAX", ".DESV" };
                DataRow Reglon;
                for (int i = 0; i < NombreCalculos.Length; i++)
                {
                    Reglon = Tabla_ValoresCalculo.NewRow();

                    Reglon["Operacion_"] = NombreCalculos[i];
                    Tabla_ValoresCalculo.Rows.Add(Reglon);
                }
                foreach (DataColumn col in Tabla_unidad.Columns)
                {
                    AplicaSuma1(col.ColumnName, 0);
                }

                for (int i = 0; i < NombreCalculos.Length; i++)
                {


                    Tabla_ValoresCalculo.Rows[i]["Operacion_"] = NombreCalculos[i];

                }



            }

            public void imprimeValores()//DataGridView dgv)
            {
                DataTable Temp = Tabla_unidad.Copy();
                Temp.Merge(Tabla_ValoresCalculo,true,MissingSchemaAction.Add);
               
                Maps.Visor_Datos vd = new Maps.Visor_Datos();
                vd.EstableceDatos(Temp);
                vd.Rapida();


            }
            public void imprimeValores(DataGridView dgv)
            {

                try
                {//aqui es para los datos

                    for (int reg = 0; reg < Tabla_unidad.Rows.Count; reg++)
                    {
                        dgv.Rows.Add();

                        for (int columa = 0; columa < Tabla_unidad.Columns.Count; columa++)
                        {
                            try
                            {

                                //dgv.Rows[dgv.Rows.Count - 2].Cells[Tabla_unidad.Columns[columa].ColumnName].Value = Tabla_unidad.Rows[reg][columa];
                                dgv.Rows[dgv.Rows.Count - 2].Cells[Tabla_unidad.Columns[columa].ColumnName].Value = Tabla_unidad.Rows[reg][columa];

                            }
                            catch { }// (SystemException er) { MessageBox.Show(er.Message); }
                        }

                    }
                }
                catch { }

                try
                {
                    //aquie es para los calculos que se generaron.
                    for (int reg = 0; reg < Tabla_ValoresCalculo.Rows.Count; reg++)
                    {

                        dgv.Rows.Add();
                        dgv.Rows[dgv.Rows.Count - 2].DefaultCellStyle.BackColor = Color.Silver;
                        dgv.Rows[dgv.Rows.Count - 2].DefaultCellStyle.ForeColor = Color.Red;

                        for (int columa = 0; columa < Tabla_ValoresCalculo.Columns.Count; columa++)
                        {
                            try
                            {

                                dgv.Rows[dgv.Rows.Count - 2].Cells[Tabla_ValoresCalculo.Columns[columa].ColumnName].Value = Tabla_ValoresCalculo.Rows[reg][columa];

                            }
                            catch { }// (SystemException er) { MessageBox.Show(er.Message); }
                        }
                        if (!indices_Activados.Contains(reg))
                        {
                            dgv.Rows[dgv.Rows.Count - 2].Visible = false;
                        }

                    }

                }
                catch { }
                dgv.Rows.Add();
                dgv.Rows[dgv.Rows.Count - 2].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;

            }
            public void imprimeValores(DataTable dgv)
            {

                try
                {//aqui es para los datos

                    for (int reg = 0; reg < Tabla_unidad.Rows.Count; reg++)
                    {
                        dgv.ImportRow(Tabla_unidad.Rows[reg]);

                    }
                }
                catch { }

                try
                {
                    //aquie es para los calculos que se generaron.
                    for (int reg = 0; reg < Tabla_ValoresCalculo.Rows.Count; reg++)
                    {

                        dgv.ImportRow(Tabla_ValoresCalculo.Rows[reg]);

                    }

                }
                catch { }


            }
            public void Resumen(DataTable dgv)
            {
                try
                {

                    //aquie es para los calculos que se generaron.
                    for (int reg = 0; reg < Tabla_ValoresCalculo.Rows.Count; reg++)
                    {

                        dgv.ImportRow(Tabla_ValoresCalculo.Rows[reg]);
                        dgv.Rows[dgv.Rows.Count - 1]["Operacion_"] = Cadena;

                    }

                }
                catch { }


            }

            public void Resumen(DataGridView dgv, bool Par)
            {


                try
                {
                    //aquie es para los calculos que se generaron.
                    for (int reg = 0; reg < Tabla_ValoresCalculo.Rows.Count; reg++)
                    {
                        dgv.Rows.Add();
                        if (Par)
                            dgv.Rows[dgv.Rows.Count - 2].DefaultCellStyle.BackColor = Color.Silver;
                        dgv.Rows[dgv.Rows.Count - 2].DefaultCellStyle.ForeColor = Color.Red;

                        for (int columa = 0; columa < Tabla_ValoresCalculo.Columns.Count; columa++)
                        {
                            try
                            {

                                dgv.Rows[dgv.Rows.Count - 2].Cells[Tabla_ValoresCalculo.Columns[columa].ColumnName].Value = Tabla_ValoresCalculo.Rows[reg][columa];

                            }
                            catch { }// (SystemException er) { MessageBox.Show(er.Message); }
                        }
                        if (!indices_Activados.Contains(reg))
                        {
                            dgv.Rows[dgv.Rows.Count - 2].Visible = false;
                        }
                        dgv.Rows[dgv.Rows.Count - 2].Cells["Operacion_"].Value = Cadena;
                    }
                    // dgv.Rows.Add();
                    // dgv.Rows[dgv.Rows.Count - 2].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;

                }
                catch { }
            }
            internal double Maximo(string campo)
            {
                double Maximo = 0;
                try
                {
                    Maximo = Convert.ToDouble(Tabla_ValoresCalculo.Rows[4][campo]);
                }
                catch { }
                return Maximo;
            }






            internal void Ordena(string campo, bool Orden)
            {
                //DataRow reglonTemp;
                try
                {
                    if (Tabla_unidad.Columns.Contains(campo))
                    {
                        //se supone qeu podrian haber campos que no son numericos. asi que lo que haremos primero sera sacar en una tablita los que son letras
                        DataTable Letras = Tabla_unidad.Clone();
                        DataTable Numeros = Tabla_unidad.Clone();
                        foreach (DataRow reglon1 in Tabla_unidad.Rows)
                        {
                            if (!IsNumeric(reglon1[campo].ToString()))
                            {
                                Letras.ImportRow(reglon1);
                            }
                            else
                            {
                                reglon1[campo] = Numero(reglon1[campo].ToString());
                                Numeros.ImportRow(reglon1);

                            }

                        }
                        Tabla_unidad.Rows.Clear();
                        //Tabla_unidad = Numeros.Copy();


                        if (Orden)
                        {

                            try
                            {

                                IEnumerable<DataRow> CampoInicial = from row in Numeros.AsEnumerable().Where(p => p.Field<string>(campo) != null)
                                                                    orderby Convert.ToDouble(row.Field<string>(campo)) ascending
                                                                    select row;

                                if (CampoInicial.Count() > 0)
                                    Tabla_unidad.Merge(CampoInicial.CopyToDataTable());

                                IEnumerable<DataRow> CampoLetra = from row in Letras.AsEnumerable().Where(p => p.Field<string>(campo) != null)
                                                                  orderby row.Field<string>(campo) ascending
                                                                  select row;

                                if (CampoLetra.Count() > 0)
                                    Tabla_unidad.Merge(CampoLetra.CopyToDataTable());
                            }
                            catch { }


                        }
                        //para decendente
                        else
                        {
                            IEnumerable<DataRow> CampoInicial = from row in Numeros.AsEnumerable().Where(p => p.Field<string>(campo) != null)
                                                                orderby Convert.ToDouble(row.Field<string>(campo)) descending
                                                                select row;

                            if (CampoInicial.Count() > 0)
                                Tabla_unidad.Merge(CampoInicial.CopyToDataTable());
                            IEnumerable<DataRow> CampoLetra = from row in Letras.AsEnumerable().Where(p => p.Field<string>(campo) != null)
                                                              orderby row.Field<string>(campo) descending
                                                              select row;

                            if (CampoLetra.Count() > 0)
                                Tabla_unidad.Merge(CampoLetra.CopyToDataTable());

                        }





                    }//de las columnas
                }
                catch (SystemException er) { MessageBox.Show(er.Message); }

            }

            internal object Maximo(string campo, int LineaOperacion)
            {
                double Maximo = 0;
                try
                {
                    Maximo = Convert.ToDouble(Tabla_ValoresCalculo.Rows[LineaOperacion][campo]);
                }
                catch { }
                return Maximo;
            }


            internal void ColocaCampos(List<string> lbCriterios)
            {
                if (!Tabla_unidad.Columns.Contains("Operacion_"))
                    Tabla_unidad.Columns.Add("Operacion_");
                Cadena = "";
                foreach (string cadenita in lbCriterios)//aqui son los criterios 
                {
                    
                    //  cad += cadenita + " : ";
                    if (!cadenita.StartsWith("_"))
                    {
                        DataRow row1 = Tabla_unidad.Rows[0];
                        Cadena += row1[cadenita].ToString() + " : ";
                    }
                    else
                    {
                        Cadena += cadenita.Remove(0, 1) + ":";
                    }
                }
                foreach (DataRow row in Tabla_unidad.Rows)
                {
                       row["Operacion_"] = Cadena;
                }
                Calculos();
                ColocaResumen(lbCriterios);

            }
            internal void ColocaResumen(List<string> lbCriterios)
            {
                List<string> Valores=new List<string>();
                 
                foreach (string cadenita in lbCriterios)//aqui son los criterios 
                {
                    if (!Tabla_ValoresCalculo.Columns.Contains("P_" + cadenita))
                        Tabla_ValoresCalculo.Columns.Add("P_" + cadenita);
                    //  cad += cadenita + " : ";
                    if (!cadenita.StartsWith("_"))
                    {
                        DataRow row1 = Tabla_unidad.Rows[0];
                        Valores.Add(row1[cadenita].ToString());
                    }
                    else
                    {
                       Valores.Add(cadenita.Remove(0, 1));
                    }
                }
                int i = 0;
                foreach (string cadenita in lbCriterios )//aqui son los criterios 
                {
                    
                    foreach (DataRow row in Tabla_ValoresCalculo.Rows)
                    {
                        row["P_"+cadenita] = Valores[i];
                       
                    } 
                    i++;
                }

            }

           
            internal DataTable Resumen()
            {
                DataTable temp = Tabla_ValoresCalculo.Copy();
                string[] NombreCalculos = new string[] { "SUM", "CONT", "PRO", "MIN", "MAX", "DESV" };
               

                for (int i = 0; i < NombreCalculos.Length; i++)
                {


                    temp.Rows[i]["Operacion_"] = NombreCalculos[i] + " " + Cadena;

                }
                //foreach (DataRow row in temp.Rows)
                //{
                //    row["Operacion_"] =  Cadena;
                //}
                return temp;
            }
        }



        public Origen_Datos Origen;
    }//dela superclase
}
