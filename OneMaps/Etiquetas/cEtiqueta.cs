using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Maps.Etiquetas
{
    public class cEtiqueta
    {
        enum TipoEtiqueta { Marca, Anotacion, Circulo };
        DataTable dtPrincipal;
        Dictionary<string, DataTable>  Dic;

        public cEtiqueta(Dictionary<string, DataTable> dicPrincipal)
        {
            Dic = dicPrincipal;
        }

        public DataTable CambiaTabla(string name)
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Visible", typeof(bool));
            tabla.Columns.Add("Nombre");
            tabla.Columns.Add("Tipo");
            tabla.Columns.Add("Texto");
            foreach(KeyValuePair<string, DataTable> ta in Dic)
            {
                if (ta.Key.Contains(name.ToUpper()))
                {
                    tabla.Rows.Add(true, ta.Key.Split(':')[1], name.ToUpper(), "");
                }
            }
            return tabla;
        }
    }
}
