using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maps
{
    public class cDiccionarios
    {
        public Dictionary<string, List<string>> Diccionario;
        public cDiccionarios()
        {
            Diccionario = new Dictionary<string, List<string>>();
        }

        public void addDictionary(string nombre, List<string> lista)
        {
            if (!Diccionario.ContainsKey(nombre))
                Diccionario.Add(nombre, lista);
            else updateDictionary(nombre, lista);
        }

        public void removeDictionary(string id)
        { Diccionario.Remove(id); }

        public void updateDictionary(string id, List<string> lista)
        { Diccionario[id] = lista; }

        public bool ContainsDictionary(string id)
        { return Diccionario.ContainsKey(id); }

        public string queTipoEs(string id)
        { return Diccionario[id][0]; }

        public List<string> valueDictionary(string id)
        { return Diccionario[id]; }
    }
}
