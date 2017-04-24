using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;

namespace One_Produccion.Editar_Tabla
{
    abstract public class origenCore
    {
        public origenCore()
        {

        }
        public void ColocaCoordenada(DataTable t, int indexPozo)
        {
            //tratemos de colocar todos los datos para 
            //creamos Una Columna para poner la formacion y la fecha

            if (!t.Columns.Contains("CoordenadaX"))
                t.Columns.Add("CoordenadaX");
            if (!t.Columns.Contains("CoordenadaY"))
                t.Columns.Add("CoordenadaY");
            t.AcceptChanges();

            DataTable sinCoordenadas = t.Clone();

            DataTable temp = DistintosDeUnaColumna(indexPozo, t);

            foreach (DataRow rows_ in temp.Rows)
            {
                string NombrePozo = rows_[indexPozo].ToString().ToUpper();
                PointF punto = new PointF(1, 1);
                if (punto.X != -1)
                {
                    var reglonesAfectados = from a in t.AsEnumerable() where (a[indexPozo].ToString().ToUpper() == NombrePozo) select a;
                    foreach (DataRow rows in reglonesAfectados)
                    {
                        rows["CoordenadaX"] = punto.X;
                        rows["CoordenadaY"] = punto.Y;
                    }
                }
                else
                    sinCoordenadas.ImportRow(rows_);
            }




        }
        public static string EliminaAcentos(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            byte[] tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(texto);
            string tex = System.Text.Encoding.UTF8.GetString(tempBytes);
            tex = tex.Trim().Replace(' ', '-');
            return tex;
        }
        static public DataTable DistintosDeUnaColumna(int Columna, DataTable tabla)
        {
            DataTable Elementos = new DataTable();
            Elementos.Columns.Add("Valor");
            try
            {

                //aqui rellenaremos pero 
                try
                {


                    IEnumerable<string> CampoInicial =
                         (from pozo in tabla.AsEnumerable() select origenCore.EliminaAcentos(pozo[Columna].ToString())).Distinct();


                    foreach (string p in CampoInicial)
                    {
                        DataRow reg = Elementos.NewRow();
                        reg[0] = p.ToUpper();
                        Elementos.Rows.Add(reg);


                    }


                }
                catch { }

            }
            catch { }
            return Elementos;

        }
    }
}
