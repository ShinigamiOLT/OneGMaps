using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneGMaps
{
    class OneMapsDatos
    {
        public DataSet TablaInfo;
        string Ruta="";
       public Dictionary<string, Tuple<string,double,double>> DiccionarioPozos;
        public OneMapsDatos()
        {
            Ruta = Environment.CurrentDirectory + @"\LocalizacionPozo.OneClass";
            DiccionarioPozos = new Dictionary<string, Tuple<string,double, double>>();
            CargaInformacion();
        }
        void CargaInformacion()
        {
            TablaInfo = new DataSet();
            //  TablaInfo.ReadXml (Environment.CurrentDirectory  + )
           if (File.Exists(Ruta))
                {
                TablaInfo.ReadXml(Ruta);
                RetornaTriplete();
            }
        }
        public void RetornaTriplete()
        {
            DiccionarioPozos.Clear();
            int i = 0;
            var lineas = from a in TablaInfo.Tables[0].AsEnumerable() where a["LAT"] != null && !string.IsNullOrEmpty( a["LAT"].ToString()) select a;
            foreach (DataRow linea in lineas)
            {
                try
                {
                    DiccionarioPozos.Add(linea["UWI"].ToString(), new Tuple<string, double, double>(linea["Pozo"].ToString(), Convert.ToDouble(linea["LAT"]), Convert.ToDouble(linea["LON"])));
                }
                catch {
                    i++;
                }
                if (DiccionarioPozos.Count > 100)
                {
                    break;
                }
            }


        }
       

    }
    public class Tuple1<T1, T2>
    {
        public T1 First { get; private set; }
        public T2 Second { get; private set; }
        internal Tuple1(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }
    }

    public static class Tuple1
    {
        public static Tuple1<T1, T2> New<T1, T2>(T1 first, T2 second)
        {
            var tuple = new Tuple1<T1, T2>(first, second);
            return tuple;
        }
    }
}
