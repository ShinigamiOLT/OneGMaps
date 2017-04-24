using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using System.IO;
using Gigasoft.ProEssentials.Enums;

namespace Maps
{
    class OpenData
    {
        internal List<ObjetoSelecionable> Lista;
        ObjetoSelecionable Objeto;
        public OpenData()
        {
            Lista = new List<ObjetoSelecionable>();
        }
        /// <summary>
        /// Tipo de Filtro a Leer (*.file)
        /// </summary>
        public string Filtro
        {
            get;
            set;
        }
        public string DirectoryWorking
        {
            get;
            set;
        }
        public void LoadArchive()
        {

            DirectoryInfo g = new DirectoryInfo(DirectoryWorking);
            FileInfo[] x = g.GetFiles("*" + Filtro + "*");

            foreach (FileInfo info in x)
            {
                Lista.Add(new ObjetoSelecionable(info, false));

            }
        }
        public void Show(int id_)
        {
            userFiltroElemento fm = new userFiltroElemento(ref Lista, id_);
            fm.EnForm(true);
            Objeto = null;
            if (fm.Seleccionado != null)
                Objeto = fm.Seleccionado;
        }
        public void Show()
        {
            userFiltroElemento fm = new userFiltroElemento(ref Lista, 1);
            fm.EnForm(true);
            Objeto = null;
            if (fm.Seleccionado != null)
                Objeto = fm.Seleccionado;
        }
        public string Archivo
        {
            get
            {
                if (Objeto == null || Objeto.ToString() == "")
                    return "";
                return DirectoryWorking + "\\" + Objeto.ToString();

            }
        }
    }
    
    class vector_especial
    {
        public ArrayList A;
        public ArrayList B;

        public vector_especial()
        {
            A = new ArrayList();
            B = new ArrayList();
        }

        public void Insert(int position, object a_valor, object b_valor)
        {
            A.Insert(position, a_valor);
            B.Insert(position, b_valor);
        }
        public void Add(object a_valor, object b_valor)
        {
            A.Add(a_valor);
            B.Add(b_valor);
        }

        public void Remove(int position)
        {
            A.Remove(position);
            B.Remove(position);
        }



    }

    public struct Tablaescala
    {
        public double Max;
        public double Min;
        public bool Auto;
    }
    class Datos_diametro_profundidad_Inicia
    {

        private double profundidad;
        private double inicia;
        private double diametro;

        public Datos_diametro_profundidad_Inicia(double profundidad_in, double inicia_in, double diametro_in)
        {
            profundidad = profundidad_in;
            inicia = inicia_in;
            diametro = diametro_in;
        }

        public double get_profundidad()
        {
            return profundidad;
        }

        public double get_inicia()
        {
            return inicia;
        }

        public double get_diametro_in()
        {
            return diametro;
        }

    }
    class Vector_Plano
    {
        double x;
        double y;
        public Vector_Plano()
        {
            x = 0;
            y = 0;
        }

        public Vector_Plano(double x_in, double y_in)
        {
            x = x_in;
            y = y_in;
        }

        public double get_x()
        {
            return x;
        }

        public double get_y()
        {
            return y;
        }

        public void set_x(double x_in)
        {
            x = x_in;
        }

        public void set_y(double y_in)
        {
            y = y_in;
        }

    }
    #region Clase para la interpolacion
    public class Spline_Metodo
    {
        double[] X;
        double[] Y;
        double[] b;
        double[] c;
        double[] d;
        int Val_Max;
        public Spline_Metodo(double[] X1, double[] Y1)
        {
            Val_Max = 0;
            if (X1.Count<double>() < Y1.Count<double>())
            {
                Val_Max = X1.Count<double>();

            }
            else
            {
                Val_Max = Y1.Count<double>();
            }
            if (Val_Max > 2)
            {
                X = new double[Val_Max];
                Y = new double[Val_Max];
                b = new double[Val_Max];
                c = new double[Val_Max];
                d = new double[Val_Max];

                for (int i = 0; i < Val_Max; i++)
                {
                    X[i] = X1[i];
                    Y[i] = Y1[i];
                    b[i] = 0;
                    c[i] = 0;
                    d[i] = 0;

                }
                formula_Spline();
            }
            else
            {
                MessageBox.Show("Hay que haber mas de 2 Valores Puntos para interpolar");
            }
        }
        private void formula_Spline()
        {
            int NM1 = Val_Max - 1;
            if (Val_Max < 1)
            {
                MessageBox.Show("el no de datos debe ser mayor de 2!!!");
                return;
            }
            if (Val_Max < 2)
            {
                b[0] = (Y[1] - Y[0]) / (X[1] - X[0]);
                // b(1) = (y(2) - y(1)) / (x(2) - x(1))
                c[0] = 0;
                d[0] = 0;
                b[1] = b[0];
                c[1] = 0;
                d[1] = 0;
                return;
            }
            //creando la diagonal
            d[0] = X[1] - X[0];
            c[1] = (Y[1] - Y[0]) / d[0];
            for (int i = 1; i < NM1; i++)
            {
                d[i] = X[i + 1] - X[i];
                b[i] = 2 * (d[i - 1] + d[i]);
                c[i + 1] = (Y[i + 1] - Y[i]) / d[i];
                c[i] = c[i + 1] - c[i];

            }
            //condiciones finales. la tercera derivada de X[1] y X[N] resultan del cociente de las diferentes
            b[0] = -d[0];
            b[Val_Max - 1] = -d[Val_Max - 2];
            c[0] = 0;
            c[Val_Max - 1] = 0;
            if ((Val_Max - 1) == 3)

                goto sp10;

            c[0] = c[2] / (X[3] - X[1]) - c[1] / (X[2] - X[0]);
            c[Val_Max - 1] = c[Val_Max - 2] / (X[Val_Max - 1] - X[Val_Max - 3]) - c[Val_Max - 3] / (X[Val_Max - 2] - X[Val_Max - 4]);
            c[0] = c[0] * Math.Pow(d[0], 2) / (X[3] - X[0]);
            c[Val_Max - 1] = -c[Val_Max - 1] * Math.Pow(d[Val_Max - 2], 2) / (X[Val_Max - 1] - X[Val_Max - 4]);
        //suatitucion hacia adelante.
        sp10:
            double T;
            for (int i = 1; i < Val_Max; i++)
            {
                T = d[i - 1] / b[i - 1];
                b[i] = b[i] - T * d[i - 1];
                c[i] = c[i] - T * c[i - 1];
            }
            //sustituciona hacia atras.
            c[Val_Max - 1] = c[Val_Max - 1] / b[Val_Max - 1];
            for (int IB = 0; IB < NM1; IB++)
            {
                int i = Val_Max - IB - 2;
                c[i] = (c[i] - d[i] * c[i + 1]) / b[i];

            }
            //calculo de los coeficiente del polinomio
            b[Val_Max - 1] = (Y[Val_Max - 1] - Y[NM1 - 1]) / d[NM1 - 1] + d[NM1 - 1] * (c[NM1 - 1] + 2 * c[Val_Max - 1]);
            for (int i = 0; i < NM1; i++)
            {
                b[i] = (Y[i + 1] - Y[i]) / d[i] - d[i] * (c[i + 1] + 2 * c[i]);
                d[i] = (c[i + 1] - c[i]) / d[i];
                c[i] = 3 * c[i];
            }


            c[Val_Max - 1] = 3 * c[Val_Max - 1];
            d[Val_Max - 1] = d[Val_Max - 2];
        }

        public double EvaluaSpline(double U, int OP)
        {
            //funcion que evalua la funcion cubica spline
            int i = 0;
            int k = 0;
            if (i >= Val_Max)
            {
                i = 0;
            }
            switch (OP)
            {
                case 1:
                    if (U <= (X[i + 1]))
                        goto se10;

                    k = 0;
                    do
                    {

                        if (U >= X[k])
                        {
                            i = k;
                            goto se10;
                        }
                        k = k + 1;
                    } while (k < Val_Max - 1);
                    break;
                default:
                    if (U <= X[i + 1])
                        goto se10;
                    {
                        i = 0;
                        int j = Val_Max;
                        do
                        {
                            k = Convert.ToInt32((i + j) / 2);

                            if (U < X[k])
                                j = k;
                            if (U >= X[k])
                                i = k;
                        } while (j > i + 1);
                    }
                    break;

            }
        se10:
            double DX = U - X[i];
            double r = Y[i] + DX * (b[i] + DX * (c[i] + DX * d[i]));
            return r;
        }

    }
    public class Spline_Metodo_Datatable
    {
        double[] X;
        double[] Y;
        double[] b;
        double[] c;
        double[] d;
        int Val_Max;
        public Spline_Metodo_Datatable(DataTable Datos, int iX, int iY)
        {
            Val_Max = 0;
            Val_Max = Datos.Rows.Count;
            //  One_Registro_Presion.VistaPrevia l = new One_Registro_Presion.VistaPrevia(Datos);
            //  l.ShowDialog();

            if (Val_Max > 2)
            {
                X = new double[Val_Max];
                Y = new double[Val_Max];
                b = new double[Val_Max];
                c = new double[Val_Max];
                d = new double[Val_Max];

                for (int i = 0; i < Val_Max; i++)
                {
                    X[i] = Convert.ToDouble(Datos.Rows[i][iX]);
                    Y[i] = Convert.ToDouble(Datos.Rows[i][iY]);
                    b[i] = 0;
                    c[i] = 0;
                    d[i] = 0;

                }
                formula_Spline();
            }
            else
            {
                MessageBox.Show("Hay que haber mas de 2 Valores Puntos para interpolar");
            }
        }
        //esta no tiene la exception de el numero de datos debe de ser mayor a 2...
        public Spline_Metodo_Datatable(DataTable Datos, int iX, int iY, int sin_excepcion)
        {
            Val_Max = 0;
            Val_Max = Datos.Rows.Count;
            //  One_Registro_Presion.VistaPrevia l = new One_Registro_Presion.VistaPrevia(Datos);
            //  l.ShowDialog();

            if (Val_Max > 2)
            {
                X = new double[Val_Max];
                Y = new double[Val_Max];
                b = new double[Val_Max];
                c = new double[Val_Max];
                d = new double[Val_Max];

                for (int i = 0; i < Val_Max; i++)
                {
                    X[i] = Convert.ToDouble(Datos.Rows[i][iX]);
                    Y[i] = Convert.ToDouble(Datos.Rows[i][iY]);
                    b[i] = 0;
                    c[i] = 0;
                    d[i] = 0;

                }
                formula_Spline();
            }
            else
            {
                //  MessageBox.Show("Hay que haber mas de 2 Valores Puntos para interpolar");
            }
        }

        private void formula_Spline()
        {
            int NM1 = Val_Max - 1;
            if (Val_Max < 1)
            {
                MessageBox.Show("el no de datos debe ser mayor de 2!!!");
                return;
            }
            if (Val_Max < 2)
            {
                b[0] = (Y[1] - Y[0]) / (X[1] - X[0]);
                // b(1) = (y(2) - y(1)) / (x(2) - x(1))
                c[0] = 0;
                d[0] = 0;
                b[1] = b[0];
                c[1] = 0;
                d[1] = 0;
                return;
            }
            //creando la diagonal
            d[0] = X[1] - X[0];
            c[1] = (Y[1] - Y[0]) / d[0];
            for (int i = 1; i < NM1; i++)
            {
                d[i] = X[i + 1] - X[i];
                b[i] = 2 * (d[i - 1] + d[i]);
                c[i + 1] = (Y[i + 1] - Y[i]) / d[i];
                c[i] = c[i + 1] - c[i];

            }
            //condiciones finales. la tercera derivada de X[1] y X[N] resultan del cociente de las diferentes
            b[0] = -d[0];
            b[Val_Max - 1] = -d[Val_Max - 2];
            c[0] = 0;
            c[Val_Max - 1] = 0;
            if ((Val_Max - 1) == 3)

                goto sp10;

            c[0] = c[2] / (X[3] - X[1]) - c[1] / (X[2] - X[0]);
            c[Val_Max - 1] = c[Val_Max - 2] / (X[Val_Max - 1] - X[Val_Max - 3]) - c[Val_Max - 3] / (X[Val_Max - 2] - X[Val_Max - 4]);
            c[0] = c[0] * Math.Pow(d[0], 2) / (X[3] - X[0]);
            c[Val_Max - 1] = -c[Val_Max - 1] * Math.Pow(d[Val_Max - 2], 2) / (X[Val_Max - 1] - X[Val_Max - 4]);
        //suatitucion hacia adelante.
        sp10:
            double T;
            for (int i = 1; i < Val_Max; i++)
            {
                T = d[i - 1] / b[i - 1];
                b[i] = b[i] - T * d[i - 1];
                c[i] = c[i] - T * c[i - 1];
            }
            //sustituciona hacia atras.
            c[Val_Max - 1] = c[Val_Max - 1] / b[Val_Max - 1];
            for (int IB = 0; IB < NM1; IB++)
            {
                int i = Val_Max - IB - 2;
                c[i] = (c[i] - d[i] * c[i + 1]) / b[i];

            }
            //calculo de los coeficiente del polinomio
            b[Val_Max - 1] = (Y[Val_Max - 1] - Y[NM1 - 1]) / d[NM1 - 1] + d[NM1 - 1] * (c[NM1 - 1] + 2 * c[Val_Max - 1]);
            for (int i = 0; i < NM1; i++)
            {
                b[i] = (Y[i + 1] - Y[i]) / d[i] - d[i] * (c[i + 1] + 2 * c[i]);
                d[i] = (c[i + 1] - c[i]) / d[i];
                c[i] = 3 * c[i];
            }


            c[Val_Max - 1] = 3 * c[Val_Max - 1];
            d[Val_Max - 1] = d[Val_Max - 2];
        }
        //esta no tiene la exception de el numero de datos debe de ser mayor a 2...
        private void formula_Spline2()
        {
            int NM1 = Val_Max - 1;
            if (Val_Max < 1)
            {
                // MessageBox.Show("el no de datos debe ser mayor de 2!!!");
                return;
            }
            if (Val_Max < 2)
            {
                b[0] = (Y[1] - Y[0]) / (X[1] - X[0]);
                // b(1) = (y(2) - y(1)) / (x(2) - x(1))
                c[0] = 0;
                d[0] = 0;
                b[1] = b[0];
                c[1] = 0;
                d[1] = 0;
                return;
            }
            //creando la diagonal
            d[0] = X[1] - X[0];
            c[1] = (Y[1] - Y[0]) / d[0];
            for (int i = 1; i < NM1; i++)
            {
                d[i] = X[i + 1] - X[i];
                b[i] = 2 * (d[i - 1] + d[i]);
                c[i + 1] = (Y[i + 1] - Y[i]) / d[i];
                c[i] = c[i + 1] - c[i];

            }
            //condiciones finales. la tercera derivada de X[1] y X[N] resultan del cociente de las diferentes
            b[0] = -d[0];
            b[Val_Max - 1] = -d[Val_Max - 2];
            c[0] = 0;
            c[Val_Max - 1] = 0;
            if ((Val_Max - 1) == 3)

                goto sp10;

            c[0] = c[2] / (X[3] - X[1]) - c[1] / (X[2] - X[0]);
            c[Val_Max - 1] = c[Val_Max - 2] / (X[Val_Max - 1] - X[Val_Max - 3]) - c[Val_Max - 3] / (X[Val_Max - 2] - X[Val_Max - 4]);
            c[0] = c[0] * Math.Pow(d[0], 2) / (X[3] - X[0]);
            c[Val_Max - 1] = -c[Val_Max - 1] * Math.Pow(d[Val_Max - 2], 2) / (X[Val_Max - 1] - X[Val_Max - 4]);
        //suatitucion hacia adelante.
        sp10:
            double T;
            for (int i = 1; i < Val_Max; i++)
            {
                T = d[i - 1] / b[i - 1];
                b[i] = b[i] - T * d[i - 1];
                c[i] = c[i] - T * c[i - 1];
            }
            //sustituciona hacia atras.
            c[Val_Max - 1] = c[Val_Max - 1] / b[Val_Max - 1];
            for (int IB = 0; IB < NM1; IB++)
            {
                int i = Val_Max - IB - 2;
                c[i] = (c[i] - d[i] * c[i + 1]) / b[i];

            }
            //calculo de los coeficiente del polinomio
            b[Val_Max - 1] = (Y[Val_Max - 1] - Y[NM1 - 1]) / d[NM1 - 1] + d[NM1 - 1] * (c[NM1 - 1] + 2 * c[Val_Max - 1]);
            for (int i = 0; i < NM1; i++)
            {
                b[i] = (Y[i + 1] - Y[i]) / d[i] - d[i] * (c[i + 1] + 2 * c[i]);
                d[i] = (c[i + 1] - c[i]) / d[i];
                c[i] = 3 * c[i];
            }


            c[Val_Max - 1] = 3 * c[Val_Max - 1];
            d[Val_Max - 1] = d[Val_Max - 2];
        }

        public double EvaluaSpline(double U, int OP)
        {
            //funcion que evalua la funcion cubica spline
            int i = 0;
            int k = 0;

            switch (OP)
            {
                case 1:
                    if (U <= (X[i + 1]))
                        goto se10;

                    k = 0;
                    do
                    {

                        if (U >= X[k])
                        {
                            i = k;
                            goto se10;
                        }
                        k = k + 1;
                    } while (k < Val_Max - 1);
                    break;
                default:
                    if (U <= X[i + 1])
                        goto se10;
                    {
                        i = 0;
                        int j = Val_Max;
                        do
                        {
                            k = Convert.ToInt32((i + j) / 2);

                            if (U < X[k])
                                j = k;
                            if (U >= X[k])
                                i = k;
                        } while (j > i + 1);
                    }
                    break;

            }
        se10:
            double DX = U - X[i];
            double r = Y[i] + DX * (b[i] + DX * (c[i] + DX * d[i]));
            return r;
        }

    }
    #endregion
    #region Clase para la informacion de los pozos
    class Pozo_Clave
    {
        public double od;
        public double id;
        public double profundidad_inicia;
        public double profundidad_filnal;

        public Pozo_Clave(double inicia_prof, double termina_prof, double in_od, double in_id)
        {
            od = in_od;
            id = in_id;
            profundidad_inicia = inicia_prof;
            profundidad_filnal = termina_prof;

        }



    }

    class Pozo_Clave2 : Pozo_Clave
    {

        public double Weight;
        public double WT;

        public Pozo_Clave2(double inicia_prof, double termina_prof, double in_od, double in_id, double Weight_in, double WT_in)
            : base(inicia_prof, termina_prof, in_od, in_id)
        {


            WT = WT_in;
            Weight = Weight_in;
        }
    }
    #endregion
    public class lineal_Metodo_Datatable
    {
        double[] X;
        double[] Y;
        int Val_Max;
        //double pendiente;

        public lineal_Metodo_Datatable(DataTable Datos, int iX, int iY)
        {
            Val_Max = 0;
            Val_Max = Datos.Rows.Count;
            //  One_Registro_Presion.VistaPrevia l = new One_Registro_Presion.VistaPrevia(Datos);
            //  l.ShowDialog();

            if (Val_Max >= 2)
            {
                X = new double[Val_Max];
                Y = new double[Val_Max];


                for (int i = 0; i < Val_Max; i++)
                {
                    X[i] = Convert.ToDouble(Datos.Rows[i][iX]);
                    Y[i] = Convert.ToDouble(Datos.Rows[i][iY]);

                }
            }

        }
        //esta no tiene la exception de el numero de datos debe de ser mayor a 2...

        //esta no tiene la exception de el numero de datos debe de ser mayor a 2...

        public double Buscar_Valor_f_x(double x)
        {
            double m_ecu = 0;
            double x_ecu = 0;
            double y_ecu = 0;
            double n_ecu = 0;

            for (int i = 0; i < X.Length; i++)
            {
                if (x == X[i])
                {
                    return Y[i];
                }
            }


            if (x > X[0] && x < X[1])
            {
                m_ecu = (Y[1] - Y[0]) / (X[1] - X[0]);
                x_ecu = X[0];
                y_ecu = Y[0];

                n_ecu = y_ecu - (m_ecu * x_ecu);
            }
            else
            {
                m_ecu = (Y[2] - Y[1]) / (X[2] - X[1]);
                x_ecu = X[1];
                y_ecu = Y[1];

                n_ecu = y_ecu - (m_ecu * x_ecu);
            }

            return (m_ecu * x) + n_ecu;

        }



    }
    public class Rehacer_class
    {
        public int tipo;
        public int row;
        public int cell;
        public object dato_celda;
        public int num_colum;

        public Rehacer_class()
        {
            tipo = -1;
            row = -1;
            cell = -1;
            dato_celda = new object();
            //id_rehacer = -1;
        }

        public Rehacer_class(int row_in, int cell_in, object celda_in, int id_rehacer_in, int num_col_de_la_fila)
        {
            row = row_in;
            cell = cell_in;
            dato_celda = celda_in;
            tipo = id_rehacer_in;
            num_colum = num_col_de_la_fila;
            //es el tipo de rehacer que se utilizara, puede ser editacion de celdas, borrado tabla, y borrado de filas, ocultamiento de columnas
        }

        public Rehacer_class(int row_in, int cell_in, object celda_in, int id_rehacer_in)
        {
            row = row_in;
            cell = cell_in;
            dato_celda = celda_in;
            tipo = id_rehacer_in;
            num_colum = -1;
            //es el tipo de rehacer que se utilizara, puede ser editacion de celdas, borrado tabla, y borrado de filas, ocultamiento de columnas


        }

        // explicacion de la clase,, en id_rehacer_in hay que insertar 1 si es solo modificacion de celda
        //  2 si es para filas
        // 3 si es para tabla


    }

    class Combinacion
    {
        List<List<string>> Contenedor_de_listas = new List<List<string>>();

        public Combinacion()
        {

        }

        public void Push_Lista(List<string> Lista_ent)
        {
            Contenedor_de_listas.Add(Lista_ent);
        }

        public List<string> Pop_Lista()
        {
            if (Contenedor_de_listas.Count > 0)
            {
                List<string> lista = Contenedor_de_listas[Contenedor_de_listas.Count - 1];
                Contenedor_de_listas.RemoveAt(Contenedor_de_listas.Count - 1);
                return lista;
            }
            else
            {
                return null;
            }
        }


        public DataTable Combinacion_de_Lista()
        {
            DataTable Tabla_Combinada = new DataTable();

            for (int i = 0; i < Contenedor_de_listas.Count; i++)
            {
                DataColumn column;
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Column" + i;
                column.ReadOnly = false;
                Tabla_Combinada.Columns.Add(column);
            }

            int numero_de_veces_a_repetir = 1;

            for (int i = 0; i < Contenedor_de_listas.Count; i++)
            {
                numero_de_veces_a_repetir *= Contenedor_de_listas[i].Count;

            }




            for (int k = 0; k < numero_de_veces_a_repetir; k++)
            {
                DataRow row;
                row = Tabla_Combinada.NewRow();

                Tabla_Combinada.Rows.Add(row);

            }



            List<string> lista_0 = Contenedor_de_listas[0];
            int numero_de_veces = numero_de_veces_a_repetir / lista_0.Count;

            int l = 0;
            for (int i = 0; i < lista_0.Count; i++)
            {
                for (int j = 0; j < numero_de_veces; j++)
                {
                    DataRow ro = Tabla_Combinada.Rows[l];
                    ro[0] = lista_0[i];
                    l++;
                }

            }


            int nume = numero_de_veces_a_repetir / lista_0.Count;

            for (int i = 1; i < Contenedor_de_listas.Count; i++)
            {
                List<string> lista_n = Contenedor_de_listas[i];

                int k = 0;
                nume = nume / lista_n.Count;
                while (k < numero_de_veces_a_repetir)
                {


                    for (int f = 0; f < lista_n.Count; f++)
                    {
                        for (int j = 0; j < nume; j++)
                        {
                            DataRow rowx = Tabla_Combinada.Rows[k];
                            rowx[i] = lista_n[f];
                            k++;
                        }
                    }



                };

            }




            return Tabla_Combinada;

        }

    }

    #region Para abrir tipos de archivo

    #endregion

    #region Clase para la lectura de Archivos Las

    internal class Las
    {
        public DataTable Dt_Tabla_datos_5;
        public DataTable dt_CURVE_INFORMATION_BLOCK_2_4;
        public DataTable dt_OTHER_INFORMATION_BLOCK_4_4;
        public DataTable dt_PARAMETER_INFORMATION_BLOCK_3_4;

        /// <summary>
        ///     well Informacion
        /// </summary>
        public DataTable dt_WELL_INFORMATION_BLOCK_1_4;

        private string textoLas;

        public Las(string TextoPlano)
        {
            textoLas = TextoPlano;
            ClasificaTablas();
        }


        private void ClasificaTablas()
        {
            #region Selecciona la version del Archivo

            bool Sin_simbolo_de_gato;

            if (textoLas.Contains("#"))
            {
                Sin_simbolo_de_gato = false;

                string[] lineas = textoLas.Split('\n');

                string[] sinfgato = (from string lin in lineas
                                     where !lin.Contains("#") && !lin.Contains("--")
                                     select lin + '\n').ToArray<string>();

                textoLas = "";

                textoLas = string.Concat(sinfgato);

                //Form Te = new Form();
                //Te.Controls.Add(new RichTextBox());
                //Te.Controls[0].Dock = DockStyle.Fill;

                //(Te.Controls[0] as RichTextBox).Text = textoLas;

                //Te.Show();

                Sin_simbolo_de_gato = true;
            }
            else
            {
                Sin_simbolo_de_gato = true;
            }

            #endregion

            if (!Sin_simbolo_de_gato) return;

            #region se trata el texto plano cuando no incluyen Comillas..

            string Texto = textoLas;

            string[] Tablas = Texto.Trim().Split('~');

            #region Separamos las Tablas

            //string InformacionBlok = (from string Tab in Tablas.AsEnumerable()
            //                          where Tab.ToUpper().Contains("VERSION INFORMATION")
            //                          select Tab).SingleOrDefault();


            string wellInformation = (from string Tab in Tablas.AsEnumerable()
                                      where Tab.ToUpper().Contains("WELL INFORMATION")
                                      select Tab).SingleOrDefault();

            string Curve_Informacion_Block = (from string Tab in Tablas.AsEnumerable()
                                              where Tab.ToUpper().Contains("CURVE INFORMATION")
                                              select Tab).SingleOrDefault();

            string Pparameter_Informacion_Block = (from string Tab in Tablas.AsEnumerable()
                                                   where Tab.ToUpper().Contains("PARAMETER INFORMATION")
                                                   select Tab).SingleOrDefault();

            string Other_Informacion = (from string Tab in Tablas.AsEnumerable()
                                        where Tab.ToUpper().Contains("OTHER INFORMATION")
                                        select Tab).SingleOrDefault();

            string Datos_enBruto = (from string Tab in Tablas.AsEnumerable()
                                    where Tab.Length > 1 && Tab[0].ToString().ToUpper().Contains("A")
                                    select Tab).SingleOrDefault();

            #endregion

            #region Para Informacion Block // que no interesa

            //if (InformacionBlok.Length > 0)
            //{

            // //no necesito esta informacion
            //}

            #endregion

            #region Para Informacion del Pozo

            try
            {
                if (!string.IsNullOrEmpty(wellInformation))
                {
                    Tabla_WellInformacion2(wellInformation);
                }
                if (!string.IsNullOrEmpty(Curve_Informacion_Block))
                {
                    Tabla_CURVE_INFORMATION_BLOCK2(Curve_Informacion_Block);
                }
                if (!string.IsNullOrEmpty(Pparameter_Informacion_Block))
                {
                    Tabla_PARAMETE_INFORMATION_BLOCK2(Pparameter_Informacion_Block);
                }
                if (!string.IsNullOrEmpty(Other_Informacion))
                {
                    Tabla_OTHER_INFORMATION2(Other_Informacion);
                }
                if (!string.IsNullOrEmpty(Datos_enBruto))
                {
                    //  tabla_Generica(Datos_enBruto);
                    Dt_Datos(Datos_enBruto);
                }

            #endregion
            }
            catch (SystemException er)
            {
                MessageBox.Show("Error leyendo Las" + er.Message);
            }

            #endregion
        }


        private void Tabla_WellInformacion(string WellInformation)
        {
            List<string> Lineas = WellInformation.TrimEnd('\n').Split('\n').AsEnumerable().ToList();
            dt_WELL_INFORMATION_BLOCK_1_4 = new DataTable();
            dt_WELL_INFORMATION_BLOCK_1_4.TableName = Lineas[0];
            dt_WELL_INFORMATION_BLOCK_1_4.Columns.Add("MNEM");
            dt_WELL_INFORMATION_BLOCK_1_4.Columns.Add("UNIT");
            dt_WELL_INFORMATION_BLOCK_1_4.Columns.Add("DATA");
            dt_WELL_INFORMATION_BLOCK_1_4.Columns.Add("DESCRIPTION");

            string NMem, Unit, Data, Descripcion;
            Lineas.RemoveAt(0); // elimins el primer renglon que tiene el titulo

            foreach (string a in Lineas)
            {
                try
                {
                    #region Analisis de la linea

                    int i = 0;

                    #region Mnem - Unit

                    while (a[i] != '.')
                    {
                        i++;
                    }

                    if (a[i] == ' ')
                    {
                        i++;
                    }

                    while (a[i] != ' ')
                    {
                        i++;
                    }

                    string[] Parte = a.Substring(0, i).Split('.');
                    NMem = Parte[0];
                    Unit = Parte[1];

                    #endregion

                    #region  Zona de Data

                    while (a[i] == ' ')
                    {
                        i++;
                    }

                    int Empieza2segmento = i;


                    while (a[i] != ':')
                    {
                        i++;
                    }

                    #endregion

                    if (a[i - 1] == ' ' && a[i - 2] == ' ')
                    {
                        Data = " ";
                    }
                    else
                    {
                        Data = a.Substring(Empieza2segmento, i - Empieza2segmento);
                    }

                    Descripcion = a.Substring(++i, a.Length - i);

                    dt_WELL_INFORMATION_BLOCK_1_4.Rows.Add(NMem, Unit, Data, Descripcion.Trim());

                    #endregion
                }
                catch (SystemException er)
                {
                    MessageBox.Show("Error al Procesar la linea" + er.Message);
                }
            }


            var temp = new Form();

            temp.Text = dt_WELL_INFORMATION_BLOCK_1_4.TableName;
            var Temp = new DataGridView();
            Temp.DataSource = dt_WELL_INFORMATION_BLOCK_1_4.Copy();
            temp.Controls.Add(Temp);
            Temp.Dock = DockStyle.Fill;
            temp.Show();
        }

        private void Tabla_WellInformacion2(string WellInformation)
        {
            dt_WELL_INFORMATION_BLOCK_1_4 = Saca_Tablas_Generica(WellInformation);
        }

        private void Tabla_CURVE_INFORMATION_BLOCK(string curveInformacionBlock)
        {
            List<string> Lineas = curveInformacionBlock.TrimEnd('\n').Split('\n').AsEnumerable().ToList();
            dt_CURVE_INFORMATION_BLOCK_2_4 = new DataTable();
            dt_CURVE_INFORMATION_BLOCK_2_4.TableName = Lineas[0];
            dt_CURVE_INFORMATION_BLOCK_2_4.Columns.Add("MNEM");
            dt_CURVE_INFORMATION_BLOCK_2_4.Columns.Add("UNIT");
            dt_CURVE_INFORMATION_BLOCK_2_4.Columns.Add("DATA");
            dt_CURVE_INFORMATION_BLOCK_2_4.Columns.Add("DESCRIPTION");

            string NMem, Unit, Data, Descripcion;
            List<string> nombreColumnas = new List<string>();

            Lineas.RemoveAt(0); // elimins el primer renglon que tiene el titulo

            foreach (string a in Lineas)
            {
                #region Analisis de la linea

                int i = 0;

                #region Mnem - Unit

                while (a[i] != '.')
                {
                    i++;
                }

                if (a[i] == ' ')
                {
                    i++;
                }

                while (a[i] != ' ')
                {
                    i++;
                }

                string[] Parte = a.Substring(0, i).Split('.');
                while (nombreColumnas.Contains(Parte[0]))
                {
                    Parte[0] = Parte[0] + "_";

                }

                NMem = Parte[0];
                Unit = Parte[1];
                nombreColumnas.Add(NMem);
                #endregion

                #region  Zona de Data

                while (a[i] == ' ')
                {
                    i++;
                }

                int Empieza2segmento = i;


                while (a[i] != ':')
                {
                    i++;
                }

                #endregion

                if (a[i - 1] == ' ' && a[i - 2] == ' ')
                {
                    Data = " ";
                }
                else
                {
                    Data = a.Substring(Empieza2segmento, i - Empieza2segmento);
                }

                Descripcion = a.Substring(++i, a.Length - i);

                dt_CURVE_INFORMATION_BLOCK_2_4.Rows.Add(NMem, Unit, Data, Descripcion.Trim());

                #endregion
            }


            var temp = new Form();

            temp.Text = dt_CURVE_INFORMATION_BLOCK_2_4.TableName;
            var Temp = new DataGridView();
            Temp.DataSource = dt_CURVE_INFORMATION_BLOCK_2_4.Copy();
            temp.Controls.Add(Temp);
            Temp.Dock = DockStyle.Fill;
            temp.Show();
        }
        private DataTable Saca_Tablas_Generica(string informacion)
        {
            var TablaGenerica = new DataTable();

            List<string> Lineas = informacion.TrimEnd('\n').Split('\n').AsEnumerable().ToList();
            TablaGenerica = new DataTable();
            TablaGenerica.TableName = Lineas[0];
            TablaGenerica.Columns.Add("MNEM");
            TablaGenerica.Columns.Add("UNIT");
            TablaGenerica.Columns.Add("DATA");
            TablaGenerica.Columns.Add("DESCRIPTION");

            string NMem, Unit, Data, Descripcion;
            List<string> nombreColumnas = new List<string>();


            Lineas.RemoveAt(0); // elimins el primer renglon que tiene el titulo

            foreach (string a in Lineas)
            {
                if (a.Length > 2)
                {
                    #region Analisis de la linea

                    int i = 0;

                    #region Mnem - Unit

                    while (a[i] != '.' && i < a.Length)
                    {
                        i++;
                    }

                    if (a[i] == ' ')
                    {
                        i++;
                    }

                    while (a[i] != ' ')
                    {
                        i++;
                    }

                    string[] Parte = a.Substring(0, i).Split('.');
                    //  NMem = Parte[0];
                    // Unit = Parte[1];
                    while (nombreColumnas.Contains(Parte[0]))
                    {
                        Parte[0] = Parte[0] + "_";

                    }

                    NMem = Parte[0];
                    Unit = Parte[1];
                    nombreColumnas.Add(NMem);


                    #endregion

                    #region  Zona de Data

                    while (a[i] == ' ')
                    {
                        i++;
                    }

                    int Empieza2segmento = i;


                    while (a[i] != ':')
                    {
                        i++;
                    }

                    #endregion

                    if (a[i - 1] == ' ' && a[i - 2] == ' ')
                    {
                        Data = " ";
                    }
                    else
                    {
                        Data = a.Substring(Empieza2segmento, i - Empieza2segmento);
                    }

                    Descripcion = a.Substring(++i, a.Length - i);

                    TablaGenerica.Rows.Add(NMem, Unit, Data, Descripcion.Trim());
                }

                    #endregion
            }


            //Form temp = new Form();

            //temp.Text = TablaGenerica.TableName;
            //DataGridView Temp = new DataGridView();
            //Temp.DataSource = TablaGenerica.Copy();
            //temp.Controls.Add(Temp);
            //Temp.Dock = DockStyle.Fill;
            //temp.Show();

            return TablaGenerica;
        }

        private void Tabla_CURVE_INFORMATION_BLOCK2(string curveInformacionBlock)
        {
            dt_CURVE_INFORMATION_BLOCK_2_4 = Saca_Tablas_Generica(curveInformacionBlock);
        }

        private void Tabla_PARAMETE_INFORMATION_BLOCK(string parameteInforacionBlock)
        {
            List<string> Lineas = parameteInforacionBlock.TrimEnd('\n').Split('\n').AsEnumerable().ToList();
            dt_PARAMETER_INFORMATION_BLOCK_3_4 = new DataTable();
            dt_PARAMETER_INFORMATION_BLOCK_3_4.TableName = Lineas[0];
            dt_PARAMETER_INFORMATION_BLOCK_3_4.Columns.Add("MNEM");
            dt_PARAMETER_INFORMATION_BLOCK_3_4.Columns.Add("UNIT");
            dt_PARAMETER_INFORMATION_BLOCK_3_4.Columns.Add("DATA");
            dt_PARAMETER_INFORMATION_BLOCK_3_4.Columns.Add("DESCRIPTION");

            string Unit, Data, Descripcion;

            Lineas.RemoveAt(0); // elimins el primer renglon que tiene el titulo

            foreach (string a in Lineas)
            {
                #region Analisis de la linea

                int i = 0;

                #region Mnem - Unit

                while (a[i] != '.')
                {
                    i++;
                }

                if (a[i] == ' ')
                {
                    i++;
                }

                while (a[i] != ' ')
                {
                    i++;
                }

                string[] Parte = a.Substring(0, i).Split('.');
                string NMem = Parte[0];
                Unit = Parte[1];

                #endregion

                #region  Zona de Data

                while (a[i] == ' ')
                {
                    i++;
                }

                int Empieza2segmento = i;


                while (a[i] != ':')
                {
                    i++;
                }

                #endregion

                if (a[i - 1] == ' ' && a[i - 2] == ' ')
                {
                    Data = " ";
                }
                else
                {
                    Data = a.Substring(Empieza2segmento, i - Empieza2segmento);
                }

                Descripcion = a.Substring(++i, a.Length - i);

                dt_PARAMETER_INFORMATION_BLOCK_3_4.Rows.Add(NMem, Unit, Data, Descripcion);

                #endregion
            }


            var temp = new Form();

            temp.Text = dt_PARAMETER_INFORMATION_BLOCK_3_4.TableName;
            var Temp = new DataGridView();
            Temp.DataSource = dt_PARAMETER_INFORMATION_BLOCK_3_4.Copy();
            temp.Controls.Add(Temp);
            Temp.Dock = DockStyle.Fill;
            temp.Show();
        }

        private void Tabla_PARAMETE_INFORMATION_BLOCK2(string parameteInforacionBlock)
        {
            dt_PARAMETER_INFORMATION_BLOCK_3_4 = Saca_Tablas_Generica(parameteInforacionBlock);
        }

        private void Tabla_OTHER_INFORMATION(string OtherInfor)
        {
            List<string> Lineas = OtherInfor.TrimEnd('\n').Split('\n').AsEnumerable().ToList();
            dt_OTHER_INFORMATION_BLOCK_4_4 = new DataTable();
            dt_OTHER_INFORMATION_BLOCK_4_4.TableName = Lineas[0];
            dt_OTHER_INFORMATION_BLOCK_4_4.Columns.Add("MNEM");
            dt_OTHER_INFORMATION_BLOCK_4_4.Columns.Add("UNIT");
            dt_OTHER_INFORMATION_BLOCK_4_4.Columns.Add("DATA");
            dt_OTHER_INFORMATION_BLOCK_4_4.Columns.Add("DESCRIPTION");

            string NMem, Unit, Data, Descripcion;

            Lineas.RemoveAt(0); // elimins el primer renglon que tiene el titulo

            foreach (string a in Lineas)
            {
                #region Analisis de la linea

                int i = 0;

                #region Mnem - Unit

                while (a[i] != '.')
                {
                    i++;
                }

                if (a[i] == ' ')
                {
                    i++;
                }

                while (a[i] != ' ')
                {
                    i++;
                }

                string[] Parte = a.Substring(0, i).Split('.');
                NMem = Parte[0];
                Unit = Parte[1];

                #endregion

                #region  Zona de Data

                while (a[i] == ' ')
                {
                    i++;
                }

                int Empieza2segmento = i;


                while (a[i] != ':')
                {
                    i++;
                }

                #endregion

                if (a[i - 1] == ' ' && a[i - 2] == ' ')
                {
                    Data = " ";
                }
                else
                {
                    Data = a.Substring(Empieza2segmento, i - Empieza2segmento);
                }

                Descripcion = a.Substring(++i, a.Length - i);

                dt_OTHER_INFORMATION_BLOCK_4_4.Rows.Add(NMem, Unit, Data, Descripcion);

                #endregion
            }


            var temp = new Form();

            temp.Text = dt_OTHER_INFORMATION_BLOCK_4_4.TableName;
            var Temp = new DataGridView();
            Temp.DataSource = dt_OTHER_INFORMATION_BLOCK_4_4.Copy();
            temp.Controls.Add(Temp);
            Temp.Dock = DockStyle.Fill;
            temp.Show();
        }

        private void Tabla_OTHER_INFORMATION2(string OtherInfor)
        {
            dt_OTHER_INFORMATION_BLOCK_4_4 = Saca_Tablas_Generica(OtherInfor);
        }


        private void Dt_Datos(string Texto)
        {
            var tablaGen = new DataTable();
            List<string> lineas = Texto.Split('\n').ToList();// Texto.TrimEnd('\n').Split('\n').AsEnumerable().ToList();//

            tablaGen.TableName = lineas[0];
            int i = 0;
            foreach (DataRow reng in dt_CURVE_INFORMATION_BLOCK_2_4.Rows)
            {
                if (tablaGen.Columns.Contains(reng["MNEM"].ToString()))
                {
                    tablaGen.Columns.Add(reng["MNEM"].ToString() + " " + reng[1].ToString() + i.ToString());
                    i++;
                }
                else
                {
                    tablaGen.Columns.Add(reng["MNEM"].ToString() + " " + reng[1].ToString());

                }
            }


            //  string nMem, unit, data, descripcion;

            lineas.RemoveAt(0); // elimins el primer renglon que tiene el titulo

            int numeroColumnas = tablaGen.Columns.Count;
            foreach (string a in lineas)
            {
                string[] elementos = a.Trim(' ').Split(' ');

                List<object> list = elementos.Cast<string>().Where(a1 => a1 != "").Cast<object>().ToList();

                object[] listos = list.ToArray<object>();

                if (listos.Length == numeroColumnas)
                    tablaGen.Rows.Add(listos);
            }

            Dt_Tabla_datos_5 = tablaGen;
        }
    }

    #endregion
    #region Colores Generacion aleatoria
    class GeneraColores
    {
        public List<Color> Colores;
        static int index = 0;
        public GeneraColores()
        {
            Colores = new List<Color>();
            CargaPorDefault();
            CreaColores();
        }

        ~GeneraColores()
        {

        }
        void CreaColores()
        {
            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                Color temp = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));

                while (Colores.Contains(temp))
                {
                    temp = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
                }
                Colores.Add(temp);
            }
        }

        public void CargaPorDefault()
        {
            Colores.Clear();
            Colores.Add(Color.Magenta);
            Colores.Add(Color.Red);
            Colores.Add(Color.Green);
            Colores.Add(Color.BlueViolet);
            Colores.Add(Color.LightBlue);
            Colores.Add(Color.Orange);
            Colores.Add(Color.Violet);
            Colores.Add(Color.Black);
            Colores.Add(Color.Aquamarine);
            Colores.Add(Color.Cyan);
            Colores.Add(Color.Gold);

            Colores.Add(Color.FromArgb(74, 126, 187));


            Colores.Add(Color.DeepSkyBlue);

            Colores.Add(Color.CornflowerBlue);
            Colores.Add(Color.Chocolate);
            Colores.Add(Color.Firebrick);
            Colores.Add(Color.Indigo);


        }

        public Color ColoresR()
        {
            if (index >= Colores.Count)
                index = 0;

            return Colores[index++];
        }

    }
    #endregion

    #region Clase Final para graficar
    /// <summary>
    /// clase para graficar 
    /// </summary>
    class Char_Pro : GeneraColores
    {

        Gigasoft.ProEssentials.Pesgo Pesgo1;
        public bool TipoFechaX;

        bool simbolos = true;
        float valnull = 0;
        float factor = 1;
        public bool Ingles = false;
        public string[] Campos = new string[] { "X1", "Y1", "Y2", "Y3", "Y4", "Y5" };


        /// <summary>
        /// Si tendra Zoom o no
        /// </summary>
        public void Zoom(Zoom X)
        {
            switch (X)
            {
                case Maps.Zoom.None:
                    Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.None;
                    Pesgo1.PeUserInterface.Allow.ZoomStyle = ZoomStyle.FramedRect;
                    break;
                case Maps.Zoom.ZoomA:
                    Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.HorzAndVert;
                    Pesgo1.PeUserInterface.Allow.ZoomStyle = ZoomStyle.Ro2Not;
                    Pesgo1.PePlot.ZoomWindow.Show = true;
                    break;
                case Maps.Zoom.ZoomH:
                    Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.Horizontal;
                    Pesgo1.PeUserInterface.Scrollbar.ScrollingHorzZoom = true;
                    Pesgo1.PePlot.ZoomWindow.Show = true;

                    break;
                case Maps.Zoom.ZoomV:
                    Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.Vertical;
                    Pesgo1.PeUserInterface.Scrollbar.ScrollingHorzZoom = true;
                    Pesgo1.PePlot.ZoomWindow.Show = true;

                    break;
            }


        }



        public Char_Pro(Gigasoft.ProEssentials.Pesgo Pesgo)
        {
            Pesgo1 = Pesgo;
            Pesgo1.PeData.NullDataValueX = -999;
            Pesgo1.PeData.NullDataValue = -999;
            TipoFechaX = false;

            CargaPorDefault();
            Pt = PointType.DotSolid;
            lp = LineType.Dot;
            PlotPesgo.PeGrid.Option.ShowTickMarkX = ShowTickMarks.TicksOutside;
            PlotPesgo.PeGrid.Option.ShowTickMarkY = ShowTickMarks.TicksOutside;
            Pesgo1.PeColor.GridBands = true;
            Normalizar = false;


        }
        public Char_Pro()
        {
            Pesgo1 = new Gigasoft.ProEssentials.Pesgo();
            Pesgo1.PeData.NullDataValueX = -999;
            Pesgo1.PeData.NullDataValue = -999;
            TipoFechaX = false;

            CargaPorDefault();
            Pt = PointType.DotSolid;
            lp = LineType.Dot;
            PlotPesgo.PeGrid.Option.ShowTickMarkX = ShowTickMarks.TicksOutside;
            PlotPesgo.PeGrid.Option.ShowTickMarkY = ShowTickMarks.TicksOutside;

       Pesgo1.PeColor.GridBands = true;

       Normalizar = false;

        }
        /// <summary>
        /// Esta funcion realiza la visualicion de la anotacion en la grafica
        /// </summary>
        /// <remarks>Permite ver las anotaciones en el caso de la tabla Eventos o declinacion</remarks>
        public void ActivaAnotaciones(bool A)
        {
            Pesgo1.PePlot.MarkDataPoints = false;

            Pesgo1.PeAnnotation.InFront = A;
            Pesgo1.PeAnnotation.Line.TextSize = 80;// Convert.ToInt32(txtFont.Text);
            Pesgo1.PeAnnotation.Show = A;

            Pesgo1.PeFont.GraphAnnotationTextSize = 100;



            // Give user ability to show or hide annotations //
            Pesgo1.PeUserInterface.Menu.AnnotationControl = A;

            Pesgo1.PeAnnotation.Show = A;
            Pesgo1.PeAnnotation.ShowAllTableAnnotations = A;

            Pesgo1.PeFont.FontSize = FontSize.Large;

            Pesgo1.PeUserInterface.Menu.AnnotationControl = A;
        }
        /// <summary>
        /// Aqui es para apuntar a un nuevo pesgo o obtener el actual.
        /// </summary>
        public Gigasoft.ProEssentials.Pesgo PlotPesgo
        {
            get { return Pesgo1; }
            set { Pesgo1 = value; }
        }

        /// <summary>
        /// estableceremos propiedades basicas como el tipo de ploteo.
        /// </summary>

        public Gigasoft.ProEssentials.Enums.SGraphPlottingMethod TipoGrafica
        {
            set { Pesgo1.PePlot.Method = value; }
            get { return Pesgo1.PePlot.Method; }
        }
        /// <summary>
        /// eje Y sera normal o financiero
        /// </summary>
        public SpecialScaling TipoescalaY
        {
            set { Pesgo1.PeGrid.Option.SpecialScalingY = value; }
            get { return Pesgo1.PeGrid.Option.SpecialScalingY; }
        }
        /// <summary>
        /// eje Y2 sera normal o financiero
        /// </summary>
        public SpecialScaling TipoescalaY2
        {
            set { Pesgo1.PeGrid.Option.SpecialScalingRY = value; }
            get { return Pesgo1.PeGrid.Option.SpecialScalingRY; }
        }

        public bool Simbolos
        {
            set { simbolos = value; }
            get { return simbolos; }
        }
        /// <summary>
        /// Establer el valor conla cual la grafica no pintara
        /// </summary>
        /// <param name="ValorNulo"></param>
        public void estableceNulo(double ValorNulo)
        {
            Pesgo1.PeData.Precision = DataPrecision.SixDecimals;
            Pesgo1.PeData.NullDataValue = ValorNulo;
            Pesgo1.PeData.NullDataValueX = ValorNulo;
            Pesgo1.PeData.NullDataValue = ValorNulo;
            valnull = Convert.ToSingle(ValorNulo);
            Pesgo1.PePlot.Option.NullDataGaps = true;
        }
        /// <summary>
        /// si el eje x sera de tipo tiempo.
        /// </summary>
        public bool TipoTiempoX
        {
            set { Pesgo1.PeData.DateTimeMode = value; }
            get { return Pesgo1.PeData.DateTimeMode; }
        }
        /// <summary>
        /// Tamaño de los puntos mstrados.
        /// </summary>
        public PointSize TamPunto
        {
            set { Pesgo1.PePlot.PointSize = value; }
            get { return Pesgo1.PePlot.PointSize; }
        }
        /// <summary>
        /// Limpiando todos los valores
        /// </summary>
        public void LimpiaDatos()
        {
            Pesgo1.PeData.X.Clear();
            Pesgo1.PeData.Y.Clear();


        }
        /// <summary>
        /// cantidad de serie de datos a graficar
        /// </summary>
        public int Series
        {
            set { Pesgo1.PeData.Subsets = value; }
            get { return Pesgo1.PeData.Subsets; }
        }
        /// <summary>
        /// puntos por serie.
        /// </summary>
        public int MaximoPuntos
        {
            set { Pesgo1.PeData.Points = value; }
            get { return Pesgo1.PeData.Points; }
        }

        public bool Invertir_eje_Y
        {
            set { Pesgo1.PeGrid.Option.InvertedYAxis = value; }
            get { return Pesgo1.PeGrid.Option.InvertedYAxis; }
        }
        public bool Indistintos
        {
            set { Pesgo1.PeData.SubsetByPoint = value; }
            get { return Pesgo1.PeData.SubsetByPoint; }
        }


        public string Nombre
        {
            set { Pesgo1.PeString.MainTitle = value; }
            get { return Pesgo1.PeString.MainTitle; }
        }
        public string Subtitulo
        {
            set { Pesgo1.PeString.SubTitle = value; }
            get { return Pesgo1.PeString.SubTitle; }
        }
        public float Factor
        {
            set { factor = value; }
            get { return factor; }
        }


        /// <summary>
        /// Este trata de colocar las serie cada una en cada eje
        /// </summary>
        /// <remarks>aun esta mal no grafica como debiera</remarks>
        public void Plot_Serie_X_EjeV2(DataTable Tabla, int X, int Y, int Serie)
        {
            Pesgo1.PeLegend.Style = LegendStyle.OneLine;
            Pesgo1.PeUserInterface.Scrollbar.MouseDraggingX = true;
            Pesgo1.PeUserInterface.Scrollbar.MouseDraggingY = true;
            Pesgo1.PeData.AutoScaleData = false;// para quitar la m

            // Enable Bar Glass Effect //
            Pesgo1.PePlot.Option.BarGlassEffect = false;

            // Enable Plotting style gradient and bevel features //
            Pesgo1.PePlot.Option.AreaGradientStyle = PlotGradientStyle.RadialBottomRight;
            Pesgo1.PePlot.Option.AreaBevelStyle = BevelStyle.MediumSmooth;
            Pesgo1.PePlot.Option.SplineGradientStyle = PlotGradientStyle.RadialBottomRight;
            Pesgo1.PePlot.Option.SplineBevelStyle = SplineBevelStyle.MediumSmooth;

            // Prepare images in memory //
            Pesgo1.PeConfigure.PrepareImages = true;

            // Pass Data //

            try
            {

                Coloca_Datos_PorSerieV2(Serie, Tabla, X, Y);


            }
            catch { }
            // Set DataShadows to show 3D //
            Pesgo1.PePlot.DataShadows = DataShadows.None;

            // Enable ZoomWindow //

            //            Pesgo1.PePlot.ZoomWindow.Show = true;

            Pesgo1.PeUserInterface.Allow.FocalRect = false;
            // Pesgo1.PePlot.Method = SGraphPlottingMethod.PointsPlusLine;
            Pesgo1.PeGrid.LineControl = GridLineControl.Both;
            Pesgo1.PeGrid.Style = GridStyle.Dot;


            Pesgo1.PeLegend.SimplePoint = false;
            Pesgo1.PeLegend.SimpleLine = false;
            Pesgo1.PeLegend.Style = LegendStyle.OneLineInsideAxis;
            Pesgo1.PeGrid.Option.MultiAxisStyle = MultiAxisStyle.GroupAllAxes;

            Pesgo1.PePlot.Option.GradientBars = 8;
            Pesgo1.PeConfigure.TextShadows = TextShadows.BoldText;
            Pesgo1.PeFont.MainTitle.Bold = true;
            Pesgo1.PeFont.SizeTitleCntl = 0.72f;
            Pesgo1.PeFont.FontSize = FontSize.Medium;
            Pesgo1.PeFont.SubTitle.Bold = true;
            Pesgo1.PeFont.Label.Bold = true;
            Pesgo1.PePlot.Option.LineShadows = true;
            Pesgo1.PeFont.FontSize = FontSize.Large;
            Pesgo1.PeUserInterface.Scrollbar.ScrollingHorzZoom = true;
            Pesgo1.PeData.Precision = DataPrecision.SevenDecimals;

            // Various other features //
            Pesgo1.PeFont.Fixed = true;

            // Improves Metafile Export //
            Pesgo1.PeSpecial.DpiX = 600;
            Pesgo1.PeSpecial.DpiY = 600;

            Pesgo1.PeConfigure.RenderEngine = RenderEngine.GdiPlus;
            Pesgo1.PeConfigure.AntiAliasGraphics = true;
            Pesgo1.PeConfigure.AntiAliasText = true;

            //    Pesgo1.PeColor.BitmapGradientMode = true;


            Pesgo1.PeLegend.Show = true;


        }
        public void Plot_Serie_X_EjeV2(DataTable Tabla, string Nombre,int X, int Y, int Serie)
        {
            Pesgo1.PeLegend.Style = LegendStyle.OneLine;
            Pesgo1.PeUserInterface.Scrollbar.MouseDraggingX = true;
            Pesgo1.PeUserInterface.Scrollbar.MouseDraggingY = true;
            Pesgo1.PeData.AutoScaleData = false;// para quitar la m

            // Enable Bar Glass Effect //
            Pesgo1.PePlot.Option.BarGlassEffect = false;

            // Enable Plotting style gradient and bevel features //
            Pesgo1.PePlot.Option.AreaGradientStyle = PlotGradientStyle.RadialBottomRight;
            Pesgo1.PePlot.Option.AreaBevelStyle = BevelStyle.MediumSmooth;
            Pesgo1.PePlot.Option.SplineGradientStyle = PlotGradientStyle.RadialBottomRight;
            Pesgo1.PePlot.Option.SplineBevelStyle = SplineBevelStyle.MediumSmooth;

            // Prepare images in memory //
            Pesgo1.PeConfigure.PrepareImages = true;

            // Pass Data //

            try
            {

                Coloca_Datos_PorSerieV2(Serie, Tabla, X, Y);
                Pesgo1.PeString.SubsetLabels[Serie] = Nombre;

            }
            catch { }
            // Set DataShadows to show 3D //
            Pesgo1.PePlot.DataShadows = DataShadows.None;

            // Enable ZoomWindow //

            //            Pesgo1.PePlot.ZoomWindow.Show = true;

            Pesgo1.PeUserInterface.Allow.FocalRect = false;
            // Pesgo1.PePlot.Method = SGraphPlottingMethod.PointsPlusLine;
            Pesgo1.PeGrid.LineControl = GridLineControl.Both;
            Pesgo1.PeGrid.Style = GridStyle.Dot;


            Pesgo1.PeLegend.SimplePoint = false;
            Pesgo1.PeLegend.SimpleLine = false;
            Pesgo1.PeLegend.Style = LegendStyle.OneLineInsideAxis;
            Pesgo1.PeGrid.Option.MultiAxisStyle = MultiAxisStyle.GroupAllAxes;

            Pesgo1.PePlot.Option.GradientBars = 8;
            Pesgo1.PeConfigure.TextShadows = TextShadows.BoldText;
            Pesgo1.PeFont.MainTitle.Bold = true;
            Pesgo1.PeFont.SizeTitleCntl = 0.72f;
            Pesgo1.PeFont.FontSize = FontSize.Medium;
            Pesgo1.PeFont.SubTitle.Bold = true;
            Pesgo1.PeFont.Label.Bold = true;
            Pesgo1.PePlot.Option.LineShadows = true;
            Pesgo1.PeFont.FontSize = FontSize.Large;
            Pesgo1.PeUserInterface.Scrollbar.ScrollingHorzZoom = true;
            Pesgo1.PeData.Precision = DataPrecision.SevenDecimals;

            // Various other features //
            Pesgo1.PeFont.Fixed = true;

            // Improves Metafile Export //
            Pesgo1.PeSpecial.DpiX = 600;
            Pesgo1.PeSpecial.DpiY = 600;

            Pesgo1.PeConfigure.RenderEngine = RenderEngine.GdiPlus;
            Pesgo1.PeConfigure.AntiAliasGraphics = true;
            Pesgo1.PeConfigure.AntiAliasText = true;

            //    Pesgo1.PeColor.BitmapGradientMode = true;


            Pesgo1.PeLegend.Show = true;


        }



        public Gigasoft.ProEssentials.Enums.PointType Pt
        {
            get { return pt; }
            set { pt = value; }
        }
        public Gigasoft.ProEssentials.Enums.LineType Lp
        {
            get { return lp; }
            set { lp = value; }
        }
        public bool RellenarPuntos = true;
        PointType pt = 0;
        LineType lp = 0;


        DataTable QuitaVaciosNulos(DataTable dt, int Col)
        {


            //  Ver(dt);
            IEnumerable<DataRow> Selecionado = from Tabla in dt.AsEnumerable().Where(p => p[Col] != null).Where(p => p[Col].ToString() != "").Where(p => p[Col].ToString() != "#¡DIV/0!") select Tabla;
            //  Ver(Selecionado.CopyToDataTable());
            if (Selecionado.Count() > 0)
                return Selecionado.CopyToDataTable();
            return dt.Clone();

        }
        public bool Normalizar
        {
            get;
            set;
        }
        void Coloca_Datos_PorSerieV2(int Lineas, DataTable Actual, int X, int Y)
        {

            try
            {
               
                double valX = 0;

                Pesgo1.PeData.Points = MaximoPuntos;

                Actual = QuitaVaciosNulos(QuitaVaciosNulos(Actual, Y), X);

                if (Actual.Rows.Count > 0)
                {
                    if (Simbolos)
                    {
                        if (pt == PointType.UpTriangleSolid)
                            pt = 0;
                    }

                    string Lenguaje = "es-MX";
                    if (Ingles)
                    {
                        Lenguaje = "en-US";
                    }
                   

                    IFormatProvider culture = new System.Globalization.CultureInfo(Lenguaje, true);
                    for (int s = Lineas; s < Lineas + 1; s++)
                    {
                        try
                        {
                            Pesgo1.PeString.SubsetLabels[s] = Actual.Columns[Y].ColumnName + Actual.TableName;
                            Pesgo1.PeString.XAxisLabel = Actual.Columns[X].ColumnName;
                            Pesgo1.PeString.YAxisLabel = Actual.Columns[Y].ColumnName + "  " + Actual.TableName;
                            Pesgo1.PePlot.SubsetPointTypes[s] = pt;
                            if (Actual.Rows.Count > 0)
                            {
                                DateTime dato = DateTime.Now ;
                               TipoTiempoX=  DateTime.TryParse(Actual.Rows[0][X].ToString(),out dato);


                                if (TipoTiempoX && !Normalizar)
                                    for (int p = 0; p < Actual.Rows.Count && p < MaximoPuntos; p++)
                                    {
                                        try
                                        {

                                            valX = DateTime.Parse(Actual.Rows[p][X].ToString(), culture, System.Globalization.DateTimeStyles.AssumeLocal).ToOADate();
                                            Pesgo1.PeData.X[s, p] = (float)valX;
                                            Pesgo1.PePlot.PointTypes[s, p] = pt;
                                        }
                                        catch
                                        {

                                            Pesgo1.PeData.X[s, p] = valnull;
                                        }

                                        // pt++;
                                        //aqui saco las Y;
                                        try
                                        {

                                            Pesgo1.PeData.Y[s, p] = Factor * Convert.ToSingle(Actual.Rows[p][Y]);


                                        }
                                        catch
                                        {
                                            MessageBox.Show(Actual.Rows[p][Y].ToString() + "<>" + Actual.Rows[p][Y].ToString().Length.ToString());

                                            Pesgo1.PeData.Y[s, p] = valnull;
                                        }
                                    }
                                else //cuando no es tiempo
                                    for (int p = 0; p < Actual.Rows.Count && p < MaximoPuntos; p++)
                                    {
                                        // reglon = L.NewRow();
                                        //saco las X

                                        var valores = Actual.Rows[p][X];
                                        try
                                        {

                                            Pesgo1.PeData.X[s, p] = Convert.ToSingle(valores);


                                        }
                                        catch (Exception Ex)
                                        {

                                            Pesgo1.PeData.X[s, p] = valnull;
                                        }
                                        //aqui saco las Y;
                                        try
                                        {

                                            Pesgo1.PeData.Y[s, p] = Factor * Convert.ToSingle(Actual.Rows[p][Y]);


                                        }
                                        catch
                                        {
                                            //    MessageBox.Show(Actual.Rows[p][Y].ToString());
                                            Pesgo1.PeData.Y[s, p] = valnull;
                                        }
                                        //  pt++;
                                        // L.Rows.Add(reglon);
                                    }



                            }
                            Pesgo1.PeLegend.SubsetLineTypes[s] = lp; //LineType.Dot;

                            Pesgo1.PeColor.SubsetColors[s] = ColoresR();
                            //aqui se agregan los puntos mas grandes que no tenga yo dato

                            if (Actual.Rows.Count > 0 && RellenarPuntos)
                            {

                                if (TipoTiempoX)
                                    for (int p = Actual.Rows.Count; p < MaximoPuntos; p++)
                                    {
                                        Pesgo1.PeData.X[s, p] = (float)valX;//Valnull; 
                                        Pesgo1.PeData.Y[s, p] = valnull;
                                    }
                                else

                                    for (int p = Actual.Rows.Count; p < MaximoPuntos; p++)
                                    {

                                        //saco las X
                                        try
                                        {

                                            Pesgo1.PeData.X[s, p] = valnull;

                                        }
                                        catch (Exception ex)
                                        {
                                            //  System.Windows.Forms.MessageBox.Show(ex.Message);
                                            Pesgo1.PeData.X[s, p] = (float)valX;//Valnull; 

                                        }
                                        try
                                        {

                                            Pesgo1.PeData.Y[s, p] = valnull;


                                        }
                                        catch 
                                        {
                                            // System.Windows.Forms.MessageBox.Show(ex.Message);
                                            Pesgo1.PeData.Y[s, p] = valnull;
                                        }

                                    }//del for para llenar vacios


                            }

                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message + "  " + ex.StackTrace);
                            throw (ex);
                        }
                    }//for para toda la serie
                }//del if si tiene rows
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + "  " + ex.StackTrace);
                throw (ex);
            }

        }




        public void configurar_hoja2()
        {
            Pesgo1.PeGrid.Option.InvertedYAxis = Invertir_eje_Y;
            Pesgo1.PeGrid.Option.ShowXAxis = ShowAxis.All;
            Pesgo1.PeColor.GraphBackground = Color.White;
            Pesgo1.PeColor.GraphForeground = Color.Black;
            Pesgo1.PeColor.Desk = Color.White;
            Pesgo1.PeConfigure.BorderTypes = TABorder.NoBorder;
            Pesgo1.PeColor.GridBands = false;
            Pesgo1.PeConfigure.BorderTypes = TABorder.SingleLine;
           // Pesgo1.PeGrid.Style = GridStyle.OnePixel;
            Pesgo1.Refresh();
        }
        public void configurar_Mapa()
        {
            Pesgo1.PeGrid.Option.InvertedYAxis = false;
            Pesgo1.PeGrid.Option.ShowXAxis = ShowAxis.All;
            Pesgo1.PeColor.GraphBackground = Color.White;
            Pesgo1.PeColor.GraphForeground = Color.Black;
            Pesgo1.PeColor.Desk = Color.White;
            Pesgo1.PeConfigure.BorderTypes = TABorder.NoBorder;
            Pesgo1.PeColor.GridBands = false;
            Pesgo1.PeConfigure.BorderTypes = TABorder.SingleLine;
            Pesgo1.PeGrid.Style = GridStyle.OnePixel;


        }
        public void EstableceMinMaxY(double Minimo, double Maximo)
        {

            Pesgo1.PeGrid.Configure.ManualScaleControlY = ManualScaleControl.MinMax;
            Pesgo1.PeGrid.Configure.ManualMinY = Minimo;
            Pesgo1.PeGrid.Configure.ManualMaxY = Maximo;



        }
        public void EstableceMinMaxX(double Minimo, double Maximo)
        {

            Pesgo1.PeGrid.Configure.ManualScaleControlX = ManualScaleControl.MinMax;
            Pesgo1.PeGrid.Configure.ManualMinX = Minimo;
            Pesgo1.PeGrid.Configure.ManualMaxX = Maximo;



        }
        public void Pinta_Tabla_en_Grafica(int columna_para_eje_x, int columna_para_eje_y, DataGridView Tabla)
        {
            for (int i = 0; i < MaximoPuntos; i++)
            {
                Pesgo1.PeData.X[0, i] = (float)(Tabla[columna_para_eje_x, i].Value);
                Pesgo1.PeData.Y[0, i] = (float)(Tabla[columna_para_eje_y, i].Value);
            }
            Pesgo1.Refresh();

        }

        public void Pinta_Tabla_en_Grafica(int columna_para_eje_x, int columna_para_eje_y, DataTable Tabla)
        {
            int i = 0;
            foreach (DataRow row in Tabla.Rows)
            {
                Pesgo1.PeData.X[0, i] = (float)Convert.ToDouble(row[columna_para_eje_x]);
                Pesgo1.PeData.Y[0, i] = (float)Convert.ToDouble(row[columna_para_eje_y]);
                i++;
            }



            Pesgo1.Refresh();

        }
        public void Pinta_Tabla_en_Grafica3(int nombre_columna_para_eje_x, int nombre_columna_para_eje_y, DataGridView Tabla, int points)
        {


            Pesgo1.PeData.Points = points;


            try
            {
                for (int i = 0; i < points; i++)
                {
                    //si el eje es invertidoo
                    if (Pesgo1.PeGrid.Option.InvertedYAxis == true)
                    {
                        try
                        {
                            Pesgo1.PeData.X[Pesgo1.PeData.Subsets - 1, i] = (float)Convert.ToDouble(Tabla[nombre_columna_para_eje_x, i].Value);
                        }
                        catch (Exception ex)
                        {
                            Pesgo1.PeData.X[Pesgo1.PeData.Subsets - 1, i] = 0;
                        }

                        try
                        {
                            Pesgo1.PeData.Y[Pesgo1.PeData.Subsets - 1, i] = (-1) * ((float)Convert.ToDouble(Tabla[nombre_columna_para_eje_y, i].Value));
                        }
                        catch (Exception ex)
                        {
                            Pesgo1.PeData.Y[Pesgo1.PeData.Subsets - 1, i] = 0;
                        }
                        // Pesgo1.PeData.Z[0, i] = (float)((float)Convert.ToDouble(Tabla[nombre_columna_para_eje_x, i].Value) / 10);
                    }//si no lo es se grafica normal
                    else
                    {
                        Pesgo1.PeData.X[Pesgo1.PeData.Subsets - 1, i] = (float)Convert.ToDouble(Tabla[nombre_columna_para_eje_x, i].Value);
                        Pesgo1.PeData.Y[Pesgo1.PeData.Subsets - 1, i] = (float)Convert.ToDouble(Tabla[nombre_columna_para_eje_y, i].Value);
                        // Pesgo1.PeData.Z[0, i] = (float)((float)Convert.ToDouble(Tabla[nombre_columna_para_eje_x, i].Value) / 10);
                    }
                }

                Pesgo1.PePlot.SubsetLineTypes[Pesgo1.PeData.Subsets - 1] = LineType.ThinSolid;


                Pesgo1.PeString.YAxisLabel = Tabla.Columns[nombre_columna_para_eje_y].Name;
                Pesgo1.PeString.XAxisLabel = Tabla.Columns[nombre_columna_para_eje_x].Name;
                // Pesgo1.PeString.SubsetLabels = ;

                //  Pesgo1.PeData.Subsets=1;
                Pesgo1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        public void Pinta_Tabla_en_Grafica(string nombre_columna_para_eje_x, string nombre_columna_para_eje_y, DataGridView Tabla, int tamaño_puntos)
        {

            try
            {
                for (int i = 0; i < MaximoPuntos; i++)
                {
                    //si el eje es invertidoo
                    if (Pesgo1.PeGrid.Option.InvertedYAxis == true)
                    {
                        Pesgo1.PeData.X[0, i] = (float)Convert.ToDouble(Tabla[nombre_columna_para_eje_x, i].Value);
                        Pesgo1.PeData.Y[0, i] = (-1) * ((float)Convert.ToDouble(Tabla[nombre_columna_para_eje_y, i].Value));
                        Pesgo1.PeData.Z[0, i] = (float)((float)Convert.ToDouble(Tabla[nombre_columna_para_eje_x, i].Value) / 10);
                    }//si no lo es se grafica normal
                    else
                    {
                        Pesgo1.PeData.X[0, i] = (float)Convert.ToDouble(Tabla[nombre_columna_para_eje_x, i].Value);
                        Pesgo1.PeData.Y[0, i] = (float)Convert.ToDouble(Tabla[nombre_columna_para_eje_y, i].Value);
                        Pesgo1.PeData.Z[0, i] = (float)((float)Convert.ToDouble(Tabla[nombre_columna_para_eje_x, i].Value) / 10);
                    }
                }


                // Set the PlottingMethod to bubble //
                Pesgo1.PePlot.Method = SGraphPlottingMethod.Bubble;

                // Enable data hot spots //
                Pesgo1.PeUserInterface.HotSpot.Data = true;

                // Make Data hots spot locations larger //
                Pesgo1.PeUserInterface.HotSpot.Size = HotSpotSize.Large;

                // Set Various Other Properties ///
                Pesgo1.PeColor.BitmapGradientMode = true;
                Pesgo1.PeColor.QuickStyle = QuickStyle.LightInset;

                // Disable some types of plotting methods //
                Pesgo1.PePlot.Allow.Bubble = true;
                Pesgo1.PePlot.Allow.Spline = false;
                Pesgo1.PePlot.Allow.PointsPlusSpline = false;
                Pesgo1.PePlot.Allow.BestFitLine = false;
                Pesgo1.PePlot.Allow.BestFitCurve = false;
                Pesgo1.PePlot.Allow.Area = false;


                Pesgo1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

     
        public void CreaBurbujas()
        {

            Pesgo1.PePlot.Method = SGraphPlottingMethod.Bubble;

            // Enable data hot spots //
            Pesgo1.PeUserInterface.HotSpot.Data = true;

            // Make Data hots spot locations larger //
            //   Pesgo1.PeUserInterface.HotSpot.Size = HotSpotSize.Large;


            // Disable some types of plotting methods //
            Pesgo1.PePlot.Allow.Bubble = true;

            Pesgo1.PePlot.Allow.Spline = false;
            Pesgo1.PePlot.Allow.PointsPlusSpline = false;
            Pesgo1.PePlot.Allow.BestFitLine = false;
            Pesgo1.PePlot.Allow.BestFitCurve = false;
            Pesgo1.PePlot.Allow.Area = false;

        }
        public void TamanioPersonal(TamanioCirculos tam)
        {
            TamPunto = PointSize.Small;
            switch (tam)
            {
                case TamanioCirculos.chico:
                    TamPunto = PointSize.Small;
                    break;
                case TamanioCirculos.mediano:
                    break;
                case TamanioCirculos.micro:
                    TamPunto = PointSize.Micro;
                    break;
                case TamanioCirculos.largo:
                    Pesgo1.PePlot.Option.MaximumPointSize = MinimumPointSize.MediumLarge;
                    break;
                case TamanioCirculos.MedioSmall:
                    Pesgo1.PePlot.Option.MaximumPointSize = MinimumPointSize.MediumSmall;
                    break;
            }
        }
#region aqui esta lo de la arrastrar
        public void activar_drag_drop(bool activar)
        {
            if (activar)
            {
                Pesgo1.AllowDrop = true;

                Pesgo1.DragDrop += new System.Windows.Forms.DragEventHandler(this.pesgo_DragDrop);
                Pesgo1.DragEnter += new System.Windows.Forms.DragEventHandler(this.pesgo_DragEnter);
                Pesgo1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pesgo_MouseDown);
                Pesgo1.KeyDown += new KeyEventHandler(Pesgo1_KeyDown);
                Pesgo1.KeyUp += new KeyEventHandler(Pesgo1_KeyUp);

            }
            else
            {
                Pesgo1.AllowDrop = false;
                Pesgo1.DragDrop -= null;
                Pesgo1.DragEnter -= null;
                Pesgo1.MouseDown -= null;
                Pesgo1.KeyDown -= null;
                Pesgo1.KeyUp -= null;
            }
        }
        void Pesgo1_KeyUp(object sender, KeyEventArgs e)
        {
            ctrl_presionado = false;
            this.Zoom(Maps. Zoom.None);

        }

        void Pesgo1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                this.Zoom(Maps.Zoom.None);
                ctrl_presionado = true;
            }
        }
        public bool ctrl_presionado = false;

        private void pesgo_MouseDown(object sender, MouseEventArgs e)
        {
            ///Objeto de origen de la imagen la cual es la que empieza el evento drag and drog
            Gigasoft.ProEssentials.Pesgo origen = (Gigasoft.ProEssentials.Pesgo)sender;

            if (ctrl_presionado)
            {
                if (e.Button == MouseButtons.Left)
                {
                    origen.DoDragDrop(origen, DragDropEffects.Move);

                }
            }
        }

        private void pesgo_DragEnter(object sender, DragEventArgs e)
        {
            //si es pesgo el que inicia el drag 
            if (e.Data.GetDataPresent(((Gigasoft.ProEssentials.Pesgo)sender).GetType().ToString()))
            {

                e.Effect = DragDropEffects.Move;
            }
            //si es de excel
            else if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void pesgo_DragDrop(object sender, DragEventArgs e)
        {



            if (e.Data.GetDataPresent(((Gigasoft.ProEssentials.Pesgo)sender).GetType().ToString()))
            {


                Gigasoft.ProEssentials.Pesgo pic = (Gigasoft.ProEssentials.Pesgo)sender;
                Gigasoft.ProEssentials.Pesgo org = (Gigasoft.ProEssentials.Pesgo)e.Data.GetData(((Gigasoft.ProEssentials.Pesgo)sender).GetType().ToString());


                if (pic.TabIndex != org.TabIndex)
                {
                    Copia_Series_de_Pesgo_a_Pesgo(org, pic);
                }

            }
            else if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {


                string valornuevo = Convert.ToString(e.Data.GetData(DataFormats.StringFormat));
                //richTextBox1.Text = valornuevo;

                string[] cadena1 = valornuevo.Split('\n');

                List<string> carlo = new List<string>();
                string[] cadena2;

                for (int i = 0; i < cadena1.Length - 1; i++)
                {
                    cadena2 = cadena1[i].Split(Convert.ToChar(9));


                    for (int k = 0; k < cadena2.Length; k++)
                    {
                        MessageBox.Show("fila " + i + "--" + cadena2[k]);

                    }


                }

            }


        }
        private void Copia_Series_de_Pesgo_a_Pesgo(Gigasoft.ProEssentials.Pesgo origen, Gigasoft.ProEssentials.Pesgo destino)
        {
            /*
            destino.PePlot.RYAxisComparisonSubsets = 3;
            destino.PeString.RYAxisLabel = "Gamma Ray(.API)";
            destino.PeGrid.Configure.ManualScaleControlRY = ManualScaleControl.MinMax;
            destino.PeGrid.Configure.ManualMinRY = 0;
            destino.PeGrid.Configure.ManualMaxRY = 300;
             */


            // Seperate subsets into separate axes ///
            // destino.PeGrid.MultiAxesSubsets[0] = 1;
            // destino.PeGrid.MultiAxesSubsets[1] = 1;
            // Overlap both multi axes ///
            // destino.PeGrid.OverlapMultiAxes[0] = 2;

            //destino.PeUserInterface.Allow.MultiAxesSizing = true;

            // destino.PeGrid.WorkingAxis = 0;
            // destino.PeColor.YAxis = Color.Black;
            //destino.PeString.YAxisLabel = "Florida";
            // destino.PePlot.Method = SGraphPlottingMethod.SplineArea;

            if (origen.PeData.Points > destino.PeData.Points)
            {
                destino.PeData.Points = origen.PeData.Points;
            }


            for (int i = 0; i < origen.PeData.Subsets; i++)
            {
                destino.PeData.Subsets++;

                for (int k = 0; k < destino.PeData.Points; k++)
                {
                    if (k < origen.PeData.Points)
                    {
                        destino.PeData.X[destino.PeData.Subsets - 1, k] = origen.PeData.X[i, k];
                        destino.PeData.Y[destino.PeData.Subsets - 1, k] = origen.PeData.Y[i, k];
                    }
                    else
                    {
                        destino.PeData.X[destino.PeData.Subsets - 1, k] = (float)destino.PeData.NullDataValue;
                        destino.PeData.Y[destino.PeData.Subsets - 1, k] = (float)destino.PeData.NullDataValue;

                    }
                }

                destino.PePlot.SubsetLineTypes[destino.PeData.Subsets - 1] = origen.PePlot.SubsetLineTypes[i];
                destino.PePlot.SubsetColors[destino.PeData.Subsets - 1] = origen.PePlot.SubsetColors[i];
                destino.PeString.SubsetLabels[destino.PeData.Subsets - 1] = origen.PeString.SubsetLabels[i];
                destino.PePlot.Methods[destino.PeData.Subsets - 1] = origen.PePlot.Methods[i];

            }
            destino.Refresh();


        }
      



#endregion



    }
    enum TamanioCirculos
    {

        chico, mediano, micro, largo, MedioSmall,
    }
    enum Zoom
    {
        None,
        ZoomV,
        ZoomH,
        ZoomA

    }
    #endregion
}
