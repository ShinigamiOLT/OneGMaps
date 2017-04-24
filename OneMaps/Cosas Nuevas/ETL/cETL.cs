using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

public class cETL
{
    public cETL()
    {
    }

    #region Opciones de Remplazar
    public string LN(string cad, string oldChar)
    {
        for (int i = 0; i < cad.Length; i++)
            if ((i + 1) < cad.Length && char.IsLetter(cad[i]) && char.IsNumber(cad[i + 1]))
                return cad.Insert(i+1, oldChar);
        return cad;
    }

    public bool LN(string cad)
    {
        for (int i = 0; i < cad.Length; i++)
            if ((i + 1) < cad.Length && char.IsLetter(cad[i]) && char.IsNumber(cad[i + 1]))
                return true;
        return false;
    }

    public string NL(string cad, string oldChar)
    {
        for (int i = 0; i < cad.Length; i++)
            if ((i + 1) < cad.Length && char.IsNumber(cad[i]) && char.IsLetter(cad[i + 1]))
                return cad.Insert(i + 1, oldChar);
        return cad;
    }

    public bool NL(string cad)
    {
        for (int i = 0; i < cad.Length; i++)
            if ((i + 1) < cad.Length && char.IsNumber(cad[i]) && char.IsLetter(cad[i + 1]))
                return true;
        return false;
    }

    public string EliminaHasta(string cad , char oldChar)
    {
        int hasta=0;
        if (cad.Contains(oldChar))
        {
            for (int i = 0; i < cad.Length; i++)
            {
                if (cad[i] == oldChar) break;
                else hasta++;
            }

            return cad.Substring(0, hasta);
        }
        else return cad;
    }

    public string[] separarPalabras(string palabra, char oldchar)
    {
        string[] valaux = new string[2];
        int i=0;
        for (i = 0; i < palabra.Length; i++)
        {
            if (palabra[i] == oldchar) break;
        }

        valaux[0] = palabra.Substring(0, i);
        valaux[1] = palabra.Substring(i+1, palabra.Length - i-1);
        return valaux;
    }

    public string EliminaDesde(string cad, char oldChar)
    {
        int hasta = 0;
        if (cad.Contains(oldChar))
        {
            for (int i = 0; i < cad.Length; i++)
            {
                if (cad[i] == oldChar) break;
                else hasta++;
            }
            int limite = cad.Length - hasta;
            return cad.Substring(hasta+1, limite-1);
        }
        else return cad;
    }

    public string[] Delimita(string cad, char oldChar)
    {
        int hasta = 0;
        return cad.Split(oldChar);
    }
    #endregion

    public String CleanInput(string strIn)
    {
        // Replace invalid characters with empty strings.
        return Regex.Replace(strIn, @"[^\w\.@-]", "");
    }

    public string DoblesEspacios(string cad)
    {
        for (int i = 0; i < cad.Length; i++)
        {
            if (cad[i] == ' ')
            {
                int j = 0;
                int z = i;
                for (j = 0; z < cad.Length && cad[z] == ' '; z++, j++) ;
                
                if (z == cad.Length || i == 0) cad = cad.Remove(i, j);
                else if (j >= 2)
                    cad = cad.Remove(i, j - 1);
            }
            else if ((i + 1) == cad.Length && cad[i] == ' ')
                return cad.Remove(i, 1);
        }
        return cad;
    }

    /// <summary>
    /// Retorna verdadero si encentra MM/DD/AAAA ne caso contrario retorna falso 
    /// </summary>
    /// <param name="cad"></param>
    /// <returns></returns>
    public bool queFormatoFecha(string cad)
    {
        try
        {
            if (cad != "" && cad.Contains('/') && cad.Length > 6)
            {
                string dia = cad.Substring(0, 2);
                cad = cad.Remove(0, 3);
                string mes = cad.Substring(0, 2);
                cad = cad.Remove(0, 3);
                if (int.Parse(mes) <= 12) return false;
                else return true;
            }
            return true;
        }
        catch { return false; }
    }

    /// <summary>
    /// Metodo que intenta contruir una fracciona apartir de una cadena
    /// </summary>
    /// <param name="cad"> es un string con basura</param>
    /// <returns></returns>
    public  string Fraccion( string cad)
    {
        DateTime fecha = new DateTime();
        cad = cad.Contains('-') && cad.Contains("mar") ? cad.Replace("mar", "marzo") : cad;
        string lg ="Es-MX";
        IFormatProvider cultura = new System.Globalization.CultureInfo(lg, true);
        if (!cad.Contains(' ') && DateTime.TryParse(cad, cultura, DateTimeStyles.AssumeLocal, out fecha))
        {
            return esFecha(cad.Contains('-') && cad.Contains("marzo") ? cad.Replace("marzo", "mar") : cad);
        }
        else
        {
            if (cad.Contains("100") && cad.Contains('%'))
                return "1";

            string cadAux = "";
            int Estado = 0;
            char caracter = '\0';

            bool huboEspacio = true;
            bool huboDiv = false;
            bool huboNumero = false;

            for (int i = 0; i < cad.Length; )
            {
                caracter = cad[i];
                switch (Estado)
                {
                    case 0: //Caso Base
                        if (char.IsNumber(caracter))
                        {
                            if (caracter == '0' && !huboNumero)
                            {
                                i++;
                                Estado = 0;
                            }
                            else
                            {
                                huboNumero = true;
                                Estado = 1;
                                cadAux += caracter;
                                i++;
                            }
                        }
                        else if (caracter == ' ')
                        {
                            if (huboEspacio)
                                huboEspacio = true;
                            Estado = 2;
                        }
                        else if (caracter == '/')
                        {
                            if (huboNumero && !huboDiv)
                            {

                                huboDiv = true;
                                cadAux += caracter;
                                huboNumero = false;
                                i++;
                                Estado = 3;
                            }
                            else
                            {
                                i++;
                                Estado = 0;
                            }
                        }
                        else if (caracter == '\\')
                        {
                            if (huboDiv)
                            {
                                huboDiv = false;
                                cadAux += caracter;
                                i++;
                                Estado = 3;
                            }
                            else
                            {
                                i++;
                                Estado = 0;
                            }
                        }
                        else
                        {
                            i++;
                            if (i == cad.Length)
                                if (!huboNumero && huboDiv)
                                {
                                    //cadAux = cadAux.Substring(0, cadAux.Length - 1);
                                }
                        }
                        break;

                    case 1: //caso Numero
                        if (char.IsNumber(caracter))
                        {
                            Estado = 0;
                            cadAux += caracter;
                            i++;
                        }
                        else Estado = 0;
                        break;

                    case 2: //Caso espacio
                        if (huboNumero && huboEspacio && !huboDiv)
                        {
                            huboEspacio = false;
                            cadAux += caracter;
                            i++;
                        }
                        else i++;//Estado = 0;
                        Estado = 0;
                        break;

                    case 3: //Caso /
                        if (char.IsNumber(caracter))
                        {
                            if (caracter == '0' && !huboNumero)
                            {
                                i++;
                                Estado = 0;
                            }
                            else
                            {
                                cadAux += caracter;
                                i++;
                                Estado = 0;
                            }
                        }
                        else Estado = 0;
                        break;
                    default: //Cualquier Anomalia
                        break;
                }
            }
            return cadAux;
        }
    }

    /// <summary>
    /// Devuelve una fraccion apartir de una fecha
    /// </summary>
    /// <param name="cad"></param>
    /// <returns></returns>
    public  string esFecha( string cad)
    {
        string cadAux = "";
        int Estado = 0;
        char caracter = '\0';
        string cadenaBuscar = "";
        bool huboNumero = false;
        bool huboDiv = false;
        for (int i = 0; i < cad.Length; )
        {
            caracter = cad[i];
            switch (Estado)
            {
                case 0://CAso General
                    if (char.IsNumber(caracter))
                    {
                        if (caracter == '0' && !huboNumero)
                        {
                            huboNumero = true;
                            Estado = 0;
                            i++;
                        }
                        else
                        {
                            cadAux += caracter;
                            Estado = 0;
                            i++;
                            huboNumero = true;
                        }
                    }
                    else if (caracter == '/')
                    {
                        if (huboDiv)
                        {
                            i = cad.Length;
                            break;
                        }
                        else
                        {
                            Estado = 0;
                            huboNumero = false;
                            huboDiv = true;
                            cadAux += caracter;
                            i++;
                        }
                    }
                    else if (char.IsLetter(caracter))
                    {
                        cadenaBuscar += caracter;
                        Estado = 1;
                        i++;

                    }
                    else i++;


                    break;
                case 1:
                    if (char.IsLetter(caracter))
                    {
                        cadenaBuscar += caracter;
                        Estado = 1;
                        i++;
                        if (i == cad.Length)
                            cadAux += "/" + numeroMes(cadenaBuscar).ToString();
                    }
                    else
                    {
                        numeroMes(cadenaBuscar);
                        Estado = 0;
                    }


                    break;
            }
        }

        return cadAux;
    }
    
    /// <summary>
    /// Convierte fecha texto a numero, "Ejemplo feb = 2"
    /// </summary>
    /// <param name="cad"></param>
    /// <returns></returns>
    static int numeroMes(string cad)
    {
        switch (cad)
        {
            case "ene": return 1;
            case "feb": return 2;
            case "mar": return 3;
            case "abr": return 4;
            case "may": return 5;
            case "jun": return 6;
            case "jul": return 7;
            case "ago": return 8;
            case "sep": return 9;
            case "oct": return 10;
            case "nov": return 11;
            case "dic": return 12;
        }
        return 0;
    }

    /// <summary>
    /// Construye un numero apartir de una cadena
    /// </summary>
    /// <param name="cad"></param>
    public  string Numero(string cad)
    {
        if (cad != "")
        {
            string cadAux = "";

            char caracter = '\0';
            int Estado = 0;
            bool huboPunto = false;
            bool huboGuion = false;
            for (int i = 0; i < cad.Length; )
            {
                caracter = cad[i];
                switch (Estado)
                {
                    case 0://CAso General
                        if (char.IsNumber(caracter))
                        {
                            huboGuion = true;
                            cadAux += caracter;
                            i++;
                            Estado = 0;
                        }
                        else if (caracter == '.' || caracter == ',')
                        {
                            huboGuion = true;
                            if (!huboPunto)
                            {
                                huboPunto = true;
                                cadAux += caracter;
                                i++;
                                Estado = 0;
                            }
                            else i++;
                        }
                        else if (caracter == '-' && !huboGuion)
                        {
                            huboGuion = true;
                            cadAux += caracter;
                            i++;
                            Estado = 0;
                        }
                        else i++;
                        break;
                }
            }
            if (cadAux != "")
                return cadAux;
            else return "";
        }
        else return "";
    }
    
    /// <summary>
    /// convierte fecha en DD/MM/AAAA, Se supone que que la fecha esta dada en MM/DD/AAAA
    /// </summary>
    /// <param name="cad"></param>
    /// <returns></returns>
    public string convierteFechaMDA(string cad)
    {
        if (cad != "" && cad.Length > 5)
        {
            int i0;
            string dia = cad.Substring(0, 2);
            if (int.TryParse(dia, out i0))
            {
                cad = cad.Remove(0, 3);


                string mes = cad.Substring(0, 2);
                cad = cad.Remove(0, 3);


                string anio = cad;
                return mes + "/" + dia + "/" + anio;
            }
            else return cad;
        }
        else return "";
    }

    public string convierteFechaDMA(string cad)
    {
        if (cad != "" && cad.Length > 5)
        {
            int i0;
            string dia = cad.Substring(0, 2);
            if (int.TryParse(dia, out i0))
            {
                cad = cad.Remove(0, 3);


                string mes = cad.Substring(0, 2);
                cad = cad.Remove(0, 3);


                string anio = cad;
                return dia + "/" + mes + "/" + anio;
            }
            else return cad;
        }
        else return "";
    }

    /// <summary>
    /// Convierte una fecha en un numero estilo EXCEL
    /// </summary>
    /// <param name="nDay"></param>
    /// <param name="nMonth"></param>
    /// <param name="nYear"></param>
    /// <returns></returns>
    public int fechaAnumero(int nDay, int nMonth, int nYear)
    {
        if (nDay == 29 && nMonth == 02 && nYear == 1900)
            return 60;

        long nSerialDate =
        (int)((1461 * (nYear + 4800 + (int)((nMonth - 14) / 12))) / 4) +
        (int)((367 * (nMonth - 2 - 12 * ((nMonth - 14) / 12))) / 12) -
        (int)((3 * ((int)((nYear + 4900 + (int)((nMonth - 14) / 12)) / 100))) / 4) +
        nDay - 2415019 - 32075;

        if (nSerialDate < 60)
        {
            nSerialDate--;
        }
        return (int)nSerialDate;
    }

    /// <summary>
    /// Recupera una fecha apartir de un numero
    /// </summary>
    /// <param name="fecha"></param>
    /// <returns></returns>
    public DateTime numeroAfecha(int fecha)
    {
        if (fecha > 59) fecha -= 1; 
        return new DateTime(1899, 12, 31).AddDays(fecha);
    }

    public string limpia(string textoOriginal)
    {
        string textoNormalizado = textoOriginal.Normalize(NormalizationForm.FormD);
        Regex reg = new Regex("[. - _ ; . , ; : { [ ] } + * ' ¡ ? ]");
        return reg.Replace(textoNormalizado, "");
    }

    /// <summary>
    /// Quita acentos de un texto
    /// </summary>
    /// <param name="textoOriginal"></param>
    /// <returns></returns>
    public string quitaAcentos(string textoOriginal)
    {
        string textoNormalizado = textoOriginal.Normalize(NormalizationForm.FormD);
        Regex reg = new Regex("[^a-zA-Z0-9-().+-/:_ ]");
        return reg.Replace(textoNormalizado, "");
    }

    /// <summary>
    /// Divide fecha y hora
    /// </summary>
    /// <param name="cad"></param>
    /// <returns></returns>
    public List<string> divideFechaHora(string cad)
    {
        DateTime oi=new DateTime();
        List<string> l1 = new List<string>();
        if (DateTime.TryParse(cad, out oi))
        {
            l1.Add(oi.ToString().Split(' ')[0]);
            l1.Add(oi.ToString().Split(' ')[1] + " " + oi.ToString().Split(' ')[2]);
            return l1;
        }
        else if (cad.Contains('/') && queFormatoFecha(cad))
        {
            l1.Add(cad);
            l1.Add(oi.ToString().Split(' ')[1] + " " + oi.ToString().Split(' ')[2]);
            return l1;
        }
        else
        {
            l1.Add(cad);
            if (cad.ToUpper() == "FECHA")
                l1.Add("HORA");
            else l1.Add("");
            return l1;
        }
    }

    /// <summary>
    /// Construye una fecha, quita elemento no validos
    /// </summary>
    /// <param name="cad"></param>
    /// <returns></returns>
    public string construyeFecha(string cad)
    {
        bool huboError = false;
        string fechaAux = "";
        char caracter='\0';
        int estado = 0;
        int barras = 0;
        int numeros = 0;
        for (int i = 0; i < cad.Length; )
        {
            caracter = cad[i];
            switch(estado)
            {
                case 0:
                    if (char.IsNumber(caracter))
                    {
                        if (barras == 2 && numeros ==2)
                        {
                            barras++;
                            numeros = 0;
                        }
                        if (numeros < 2)
                        {
                            fechaAux += caracter;
                            estado = 0;
                            i++;
                            numeros++;
                        }
                        else
                        {
                            huboError = true;
                            estado = 0;
                            i++;
                        }

                    }
                    else if (caracter == '/')
                    {
                        if (barras < 2)
                        {
                            numeros = 0;
                            fechaAux += caracter;
                            estado = 0;
                            i++;
                            barras++;
                        }
                        else
                        {
                            estado = 0;
                            i++;
                        }
                    }
                    else
                    {
                        huboError = true;
                        i++;
                    }
                    break;
            }
        }



        return fechaAux;
    }


    public bool comparaPalabras(string x, string y)
    {
        int valint = 0;
        if (x == y) return true;
        for (int i = 0; i < x.Length; i++)
            if (y.Contains(x[i]))
                valint++;

        float xc = (float.Parse(valint.ToString())/ float.Parse(x.Length.ToString())) * 100;

        if (  xc > 80)
            return true;
        else return false;
    }

    /// <summary>
    /// Divide una palabra apartir de . - _ ) 
    /// </summary>
    /// <param name="cad"></param>
    /// <returns></returns>
    public List<string> contruyeYseparaPalabras(string cad)
    {
        List<string> lista = new List<string>();
        if (cad != "")
        {   
            string fechaAux = "";
            char caracter = '\0';
            int estado = 0;
            int barras = 0;
            for (int i = 0; i < cad.Length; )
            {
                caracter = cad[i];
                switch (estado)
                {
                    case 0:
                        if (char.IsLetter(caracter) || char.IsNumber(caracter))
                        {
                            fechaAux += caracter;
                            estado = 0;
                            i++;
                            if (i == cad.Length)
                                lista.Add(fechaAux);
                        }
                        else if (caracter == '-' || caracter == ' ' || caracter == '_' || caracter == '.' || caracter == ')')
                        {
                            lista.Add(fechaAux);
                            fechaAux = "";
                            estado = 0;
                            i++;
                        }
                        else if (caracter == '/')
                        {
                            if (barras < 2)
                            {
                                fechaAux += caracter;
                                estado = 0;
                                i++;
                                barras++;
                            }
                            else
                            {
                                estado = 0;
                                i++;
                            }
                        }

                        else i++;
                        break;
                }
            }
        }
        else
        {
            lista.Add("");
            lista.Add("");
        }
        if (lista.Count < 2)
            lista.Add("");
        return lista;
    }
    
}

