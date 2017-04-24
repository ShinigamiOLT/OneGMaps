using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluarExpresionesMatematicas
{
    enum Tipo
    {
        Invalido,
        Operador,
        Operando,
        ParentesisA,
        ParentesisB,
        Funcion,
        Constante
    }

    /// <summary>
    /// Evalua una expresion matematica
    /// </summary>
    public class Evaluador
    {

        public Evaluador()
        {
        }
        #region Propiedades y Campos
        private string expresion;
        private string postfija;

        /// <summary>
        /// Cadena que representa la expresion en notacion postfija
        /// </summary>
        public string PostFija
        {
            get { return postfija; }
        }

        /// <summary>
        /// Cadena que representa la expresion en forma normal
        /// </summary>
        public string InFija
        {
            get { return expresion; }
            set
            {
                expresion = value;
                postfija = Convertir_Infija_A_Posfija(value);
            }
        }

        #endregion

        #region Constructor
        public Evaluador(string expresion)
        {
            this.expresion = expresion;
            this.postfija = Convertir_Infija_A_Posfija(expresion);
        }
        #endregion

        #region funciones
        /// <summary>
        /// Evalua la expresion
        /// </summary>
        public double F()
        {
            double resul = 0;
            EvaluarPostfija(postfija, out resul);
            return resul;
        }

        /// <summary>
        /// Evalua la expresion en x
        /// </summary>
        public double F(double x)
        {
            double resul = 0;
            EvaluarPostfija(postfija, out resul);
            return resul;
        }

        /// <summary>
        /// Evalua la expresion en x,y
        /// </summary>
        public double F(double x, double y)
        {
            double resul = 0;
            EvaluarPostfija(postfija, out resul);
            return resul;
        }

        /// <summary>
        /// Evalua la expresion en x,y,z
        /// </summary>
        public double F(double x, double y, double z)
        {
            double resul = 0;
            EvaluarPostfija(postfija, out resul);
            return resul;
        }
        #endregion


        /// <summary>
        /// Evalua una expresion postfija
        /// </summary>
        /// <param name="expresion"></param>
        public bool EvaluarPostfija(string expresion, out double resultado)
        {
            Stack<double> pila = new Stack<double>();
            string[] tokens = expresion.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            double operando = 0.0;
            resultado = 0;
            bool valRegresar = true;
            foreach (string token in tokens)
            {
                Tipo tipo = GetTipo(token);

                if (tipo == Tipo.Operador || tipo == Tipo.Funcion)
                    valRegresar= EjecutarOperacion(pila, token);
                else if (double.TryParse(token, out operando))
                    pila.Push(operando);
                else if (token == "pi")
                    pila.Push(Math.PI);
                else if (token == "e")
                    pila.Push(Math.E);
                else return false;//throw new Exception("Error formato numerico no valido");
            }

            if (pila.Count != 1)
                return false;//throw new Exception("No se ha podido evaluar la expresion");

            resultado = pila.Pop();
            return valRegresar;
        }

        /// <summary>
        /// Ejecuta la operacion especificada por el operador
        /// </summary>
        /// <param name="pila">pila de operandos</param>
        /// <param name="operacion">operacion a ejecutar</param>
        //private void EjecutarOperacion(Stack<double> pila, string operacion)
        private bool EjecutarOperacion(Stack<double> pila, string operacion)
        {
            double first = GetOperando(pila);

            switch (operacion)
            {
                case "+":
                    pila.Push(GetOperando(pila) + first);
                    break;
                case "-":
                    pila.Push(GetOperando(pila) - first);
                    break;
                case "*":
                    pila.Push(GetOperando(pila) * first);
                    break;
                case "/":
                    if (first == 0) return false;//throw new DivideByZeroException("Error division por cero");
                    pila.Push(GetOperando(pila) / first);
                    break;
                case "^":
                    pila.Push(Math.Pow(GetOperando(pila), first));
                    break;
                case "pow":
                    pila.Push(Math.Pow(GetOperando(pila), first));
                    break;
                case "%":
                    pila.Push(GetOperando(pila) % first);
                    break;
                case "sqrt":
                    pila.Push(Math.Sqrt(first));
                    break;
                case "sin":
                    pila.Push(Math.Sin(first));
                    break;
                case "cos":
                    pila.Push(Math.Cos(first));
                    break;
                case "tan":
                    pila.Push(Math.Tan(first));
                    break;
                case "sinh":
                    pila.Push(Math.Sinh(first));
                    break;
                case "cosh":
                    pila.Push(Math.Cosh(first));
                    break;
                case "tanh":
                    pila.Push(Math.Tanh(first));
                    break;
                case "ln":
                    pila.Push(Math.Log(first));
                    break;
                case "log":
                    pila.Push(Math.Log10(first));
                    break;
                case "abs":
                    pila.Push(Math.Abs(first));
                    break;
                case "sec":
                    pila.Push(1 / Math.Cos(first));
                    break;
                case "csc":
                    pila.Push(1 / Math.Sin(first));
                    break;
                case "cot":
                    pila.Push(1 / Math.Tan(first));
                    break;
                case "sech":
                    pila.Push(2 / (Math.Exp(first) + Math.Exp(-first)));
                    break;
                case "csch":
                    pila.Push(2 / (Math.Exp(first) - Math.Exp(-first)));
                    break;
                case "coth":
                    pila.Push((Math.Exp(first) + Math.Exp(-first)) / (Math.Exp(first) - Math.Exp(-first)));
                    break;
                default: return false;
                    //throw new Exception("Error operacion no admitida");
            }
            return true;
        }

        /// <summary>
        /// Devuelve un operando de la pila
        /// </summary>
        private double GetOperando(Stack<double> pila)
        {
            if (pila.Count == 0) throw new Exception("Error faltan operandos");
            return pila.Pop();
        }

        /// <summary>
        /// Convierte una expresion infija en una expresion postfija
        /// </summary>
        /// <param name="expresion">expresion matamatica valida que se desea convertir</param>
        /// <returns>Devuelve la expresion en notacion postfija.</returns>
        public string Convertir_Infija_A_Posfija(string expresion)
        {
            expresion = expresion.ToLower();
            StringBuilder salida = new StringBuilder();
            Stack<string> operadores = new Stack<string>();
            string token = string.Empty;
            Tipo tipo = Tipo.Invalido;
            Tipo last = Tipo.Invalido;

            for (int i = 0; i < expresion.Length; i++)
            {
                token = expresion[i].ToString();

                if (string.IsNullOrWhiteSpace(token)) continue;

                if (i + 1 < expresion.Length && expresion[i] == 'p' && expresion[i + 1] == 'i')
                {
                    token = "pi";
                    i++;
                }

                tipo = GetTipo(token);

                if (tipo == Tipo.Operando)
                {
                    if (last == Tipo.Constante || last == Tipo.ParentesisB)
                        ApilarOperador(salida, operadores, "*");

                    salida.Append(token);
                }
                else if (tipo == Tipo.Constante)
                {
                    VerificarMultiplicacionOculta(salida, operadores, last);
                    salida.Append(" " + token + " ");
                }
                else if (tipo == Tipo.Operador)
                {
                    if (last == Tipo.Operador && token != "-")
                        throw new Exception("Error falta operando");

                    if ((last == Tipo.Operador && token == "-") ||
                        (last == Tipo.Invalido && token == "-") || 
                        (last == Tipo.ParentesisA && token == "-"))
                    {
                        salida.Append(" -1 ");
                        operadores.Push("*");
                    }
                    else ApilarOperador(salida, operadores, token);
                }
                else if (tipo == Tipo.ParentesisA)
                {
                    VerificarMultiplicacionOculta(salida, operadores, last);
                    operadores.Push("(");
                }
                else if (tipo == Tipo.ParentesisB) DesapilarParentesis(salida, operadores);
                else
                {
                    int index = expresion.IndexOf('(', i);
                    if (index < 0) throw new Exception("Error en sintaxis");
                    string newtoken = expresion.Substring(i, index - i);
                    i = index - 1;

                    tipo = GetTipo(newtoken);

                    if (tipo == Tipo.Funcion)
                    {
                        VerificarMultiplicacionOculta(salida, operadores, last);
                        operadores.Push(newtoken);
                    }
                    else throw new Exception("Error de sintaxis");
                }

                last = tipo;
            }

            VaciarOperandos(salida, operadores);
            RemoverEspaciosVacios(salida);

            return salida.ToString();
        }


        public bool Convertir_Infija_A_Posfija(string expresion, out string val)
        {
            val = "";
            expresion = expresion.ToLower();
            StringBuilder salida = new StringBuilder();
            Stack<string> operadores = new Stack<string>();
            string token = string.Empty;
            Tipo tipo = Tipo.Invalido;
            Tipo last = Tipo.Invalido;

            for (int i = 0; i < expresion.Length; i++)
            {
                token = expresion[i].ToString();

                if (string.IsNullOrWhiteSpace(token)) continue;

                if (i + 1 < expresion.Length && expresion[i] == 'p' && expresion[i + 1] == 'i')
                {
                    token = "pi";
                    i++;
                }

                tipo = GetTipo(token);

                if (tipo == Tipo.Operando)
                {
                    if (last == Tipo.Constante || last == Tipo.ParentesisB)
                        ApilarOperador(salida, operadores, "*");

                    salida.Append(token);
                }
                else if (tipo == Tipo.Constante)
                {
                    VerificarMultiplicacionOculta(salida, operadores, last);
                    salida.Append(" " + token + " ");
                }
                else if (tipo == Tipo.Operador)
                {
                    if (last == Tipo.Operador && token != "-")
                        return false;

                    if ((last == Tipo.Operador && token == "-") ||
                        (last == Tipo.Invalido && token == "-") ||
                        (last == Tipo.ParentesisA && token == "-"))
                    {
                        salida.Append(" -1 ");
                        operadores.Push("*");
                    }
                    else ApilarOperador(salida, operadores, token);
                }
                else if (tipo == Tipo.ParentesisA)
                {
                    VerificarMultiplicacionOculta(salida, operadores, last);
                    operadores.Push("(");
                }
                else if (tipo == Tipo.ParentesisB) DesapilarParentesis(salida, operadores);
                else
                {
                    int index = expresion.IndexOf('(', i);
                    if (index < 0) return false;
                    string newtoken = expresion.Substring(i, index - i);
                    i = index - 1;

                    tipo = GetTipo(newtoken);

                    if (tipo == Tipo.Funcion)
                    {
                        VerificarMultiplicacionOculta(salida, operadores, last);
                        operadores.Push(newtoken);
                    }
                    else return false;
                }

                last = tipo;
            }

            VaciarOperandos(salida, operadores);
            RemoverEspaciosVacios(salida);

            val = salida.ToString();
             return true;
        }


        /// <summary>
        /// Elimina espacios en blanco innecesarios
        /// </summary>
        private void RemoverEspaciosVacios(StringBuilder salida)
        {
            if (salida[0] == ' ') salida.Remove(0, 1);
            if (salida[salida.Length - 1] == ' ') salida.Remove(salida.Length - 1, 1);

            for (int i = 0; i < salida.Length - 1; i++)
            {
                if (salida[i] == ' ' && salida[i + 1] == ' ')
                    salida.Remove(i, 1);
            }
        }

        /// <summary>
        /// Verifica si hay una multiplicacion oculta y la procesa
        /// </summary>
        private void VerificarMultiplicacionOculta(StringBuilder salida, Stack<string> operadores, Tipo last)
        {
            if (last == Tipo.Constante || last == Tipo.Operando || last == Tipo.ParentesisB)
                ApilarOperador(salida, operadores, "*");
        }

        /// <summary>
        /// Agrega un aperador a la pila de operandos
        /// </summary>
        private void ApilarOperador(StringBuilder salida, Stack<string> operadores, string token)
        {
            salida.Append(" ");
            if (operadores.Count > 0) DesapilarOperando(token, operadores, salida);
            operadores.Push(token);
        }

        /// <summary>
        /// Envia a la salida todos los operadores hasta encontrar un parentesis
        /// </summary>
        private void DesapilarParentesis(StringBuilder salida, Stack<string> operadores)
        {
            string sop = operadores.Pop();
            salida.Append(" ");

            while (sop != "(")
            {
                salida.Append(sop + " ");

                if (operadores.Count == 0) throw new Exception("Error falta parentesis de apertura (");

                sop = operadores.Pop();
            }
        }

        /// <summary>
        /// Envia todos los operadores restantes en la pila a la salida
        /// </summary>
        private void VaciarOperandos(StringBuilder salida, Stack<string> operadores)
        {
            salida.Append(" ");

            while (operadores.Count > 0)
            {
                string sop = operadores.Pop();

                if (sop == "(")
                    throw new Exception("Error falta parentesis de cierre )");

                salida.Append(sop + " ");
            }
        }

        /// <summary>
        /// Agrega a la salida todos aquellos operandos de mayor presedencia
        /// </summary>
        private void DesapilarOperando(string operador, Stack<string> operadores, StringBuilder salida)
        {
            int pc = Prioridad(operador);
            string op = operadores.Pop();
            int po = Prioridad(op);

            while (pc <= po)
            {
                salida.Append(op + " ");

                if (operadores.Count == 0) break;

                op = operadores.Pop();
                po = Prioridad(op);
            }

            if (pc > po)
                operadores.Push(op);
        }

        /// <summary>
        /// Determina la prioridad de un operando
        /// </summary>
        private byte Prioridad(string operando)
        {
            switch (operando)
            {
                case "(":
                    return 0;
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                case "%":
                    return 2;
                case "^":
                    return 3;
                default:
                    return 4;
            }
        }

        /// <summary>
        /// Devuelve el tipo que define a un token
        /// </summary>
        private Tipo GetTipo(string token)
        {
            if (char.IsDigit(token[0]) || token == ".") return Tipo.Operando;

            switch (token)
            {
                case "/":
                case "*":
                case "-":
                case "+":
                case "^":
                case "%":
                    return Tipo.Operador;
                case "e":
                case "x":
                case "y":
                case "z":
                case "pi":
                    return Tipo.Constante;
                case "(":
                    return Tipo.ParentesisA;
                case ")":
                    return Tipo.ParentesisB;
                case "sin":
                case "cos":
                case "tan":
                case "sqrt":
                case "sinh":
                case "cosh":
                case "tanh":
                case "ln":
                case "log":
                case "pow":
                case "abs":
                case "sec":
                case "csc":
                case "cot":
                case "sech":
                case "csch":
                case "coth":
                //agregar una nueva funcion aqui.
                    return Tipo.Funcion;
                default:
                    return Tipo.Invalido;
            }
        }
    }
}
