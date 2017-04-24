using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EvaluarExpresionesMatematicas;
using System.Text.RegularExpressions;
using System.CodeDom.Compiler;
using System.Reflection;

namespace Maps
{
    public partial class fAritmetica : Form
    {
        private List<string> listaComplete;
        private DataTable dtPrincipal;
        public bool unicaVez { get; set; }
        bool huboSeleccion = false; //variable utilizada para saber si ya hubo alguna variable seleccionada.
        public string keyword { get; set; } //cadena donde se va formando la palabra 
        public bool listShow { get; set; }
        public int count { get; set; }
        List<string> ListaNombre;
        string[] listaRemove = { "+", "-", "*", "/", "EXP", "POW", "SIN","^", ",", "COS", "TAN", "LOG", "SQRT", "AND", "OR" };
        System.Windows.Forms.ToolTip tip;
        Evaluador evE = new EvaluarExpresionesMatematicas.Evaluador();
        DataTable original;

        public fAritmetica(List<string> lista, DataTable dgvBase)
        {
            InitializeComponent();
            this.listaComplete = lista;
            this.dtPrincipal = dgvBase;
            original = dgvBase.Copy();
            SuperTooltipInfo superTooltip = new SuperTooltipInfo();
            superTooltip.BodyText = "<strong>Funciones:</strong>SIN(X), COS(X), TAN(X), SQRT(X), POW(X, N), EXP, LOG ";
            superTooltip.FooterText = "";
            superTooltip.Color = eTooltipColor.BlueMist;
            superTooltip1.SetSuperTooltip(btnAyuda, superTooltip);
        }

        private void fAritmetica_Load(object sender, EventArgs e)
        {
            tip = new System.Windows.Forms.ToolTip();
            unicaVez = true;
            ListaNombre = new List<string>();
            keyword = "";
            listaComplete.Add("EXP");
            listaComplete.Add("POW");
            listaComplete.Add("SIN");
            listaComplete.Add("COS");
            listaComplete.Add("TAN");
            listaComplete.Add("LOG");
            listaComplete.Add("SQRT");
            listaComplete.Add("AND");
            listaComplete.Add("OR");
            listaComplete.Add("SI");
        }

        private void lbAuto_DoubleClick(object sender, EventArgs e)
        {
            if (lbAuto.SelectedItem != null && lbAuto.Visible)
            {
                string autoText = "";
                if (listaRemove.Contains(lbAuto.SelectedItem)) autoText = lbAuto.SelectedItem.ToString();
                else autoText = "'" + lbAuto.SelectedItem.ToString() + "'";
                RichTextBox rtb = new RichTextBox();
                rtb.Text = rtbFormula.Text.ToUpper();
                rtb.Find(palabraFormanado, RichTextBoxFinds.Reverse);
                rtb.SelectedText = autoText;
                rtbFormula.Text = rtb.Text;
                rtbFormula.SelectionStart = rtbFormula.Text.Length;
                if (!listaRemove.Contains(autoText))
                    ListaNombre.Add(autoText);
                lbAuto.Visible = false;
                palabraFormanado = "";

            }
        }

        string palabraFormanado = "";
        private void rtbFormula_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                palabraFormanado = "";
                lbAuto.Visible = false;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (lbAuto.SelectedItem != null && lbAuto.Visible)
                {
                    string autoText = "";
                    if (listaRemove.Contains(lbAuto.SelectedItem)) autoText = lbAuto.SelectedItem.ToString();
                     else   autoText = "'" + lbAuto.SelectedItem.ToString() + "'";
                    RichTextBox rtb = new RichTextBox();
                    rtb.Text = rtbFormula.Text.ToUpper();
                    rtb.Find(palabraFormanado, RichTextBoxFinds.Reverse);
                    rtb.SelectedText = autoText;
                    rtbFormula.Text = rtb.Text;
                    rtbFormula.SelectionStart = rtbFormula.Text.Length;
                    if (!listaRemove.Contains(autoText))
                        ListaNombre.Add(autoText);
                    lbAuto.Visible = false;
                    palabraFormanado = "";

                }
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode== Keys.Left)
            {
                if (e.KeyCode == Keys.Up)
                {
                    lbAuto.Focus();
                    if (lbAuto.SelectedIndex != 0)
                    {
                        lbAuto.SelectedIndex -= 1;
                    }
                    else
                    {
                        lbAuto.SelectedIndex = 0;
                    }
                    rtbFormula.Focus();

                }
                else if (e.KeyCode == Keys.Down)
                {
                    lbAuto.Focus();
                    try
                    {
                        lbAuto.SelectedIndex += 1;
                    }
                    catch
                    {
                    }
                    rtbFormula.Focus();
                }
                else if (e.KeyCode == Keys.Left) e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == Keys.Back)
            {
                if (rtbFormula.SelectedText.Length == rtbFormula.Text.Length)
                {
                    palabraFormanado = "";
                    ListaNombre.Clear();
                    return;
                }
                if (ListaNombre.Contains(rtbFormula.SelectedText))
                {
                    ListaNombre.Remove(rtbFormula.SelectedText.Replace("'", ""));
                    return;
                }
                string auxiliar = "";
                int deli = indexDelimitador(rtbFormula.Text, out auxiliar);
                if (deli > 0)
                {
                    int num1 = rtbFormula.Text.Length - deli;
                    rtbFormula.Text = rtbFormula.Text.Substring(0, num1);

                    if (ListaNombre.Count > 0)
                        ListaNombre.Remove(ListaNombre[ListaNombre.Count - 1]);
                    rtbFormula.SelectionStart = rtbFormula.Text.Length;
                    e.SuppressKeyPress = true;
                    lbAuto.Visible = false;
                    palabraFormanado = "";
                    return;
                }
                else if (palabraFormanado != "")
                    palabraFormanado = palabraFormanado.Substring(0, palabraFormanado.Length - 1).Replace('\b', '\0');
                if (palabraFormanado.Length > 0)
                {
                    Point point = this.rtbFormula.GetPositionFromCharIndex(rtbFormula.SelectionStart);
                    point.X += rtbFormula.Location.X;
                    point.Y = rtbFormula.Location.Y + 23;
                    lbAuto.Location = point;
                    string[] x = (from cad in listaComplete
                                  where cad.ToUpper().Contains(palabraFormanado.ToUpper())
                                  select cad).ToArray();
                    lbAuto.Items.Clear();
                    lbAuto.Items.AddRange(x);
                    if (x.Count() > 0)
                    {
                        lbAuto.Show();
                        lbAuto.Sorted = true;
                        lbAuto.SelectedIndex = lbAuto.FindString(palabraFormanado);
                    }
                    else lbAuto.Hide();

                    rtbFormula.Focus();
                }
            }
        }

        public int indexDelimitador(string cad, out string cadOut)
        {
            cadOut = "";
            if (cad[cad.Length - 1] == '\'')
            {
                for (int i = cad.Length - 2; i >= 0; i--)
                    if (cad[i] == '\'')
                        return (cad.Length - i);
                    else cadOut += cad[i];
                if (ListaNombre.Count == 1) return ListaNombre[0].Length;
            }
            return 0;
        }

        char[] sim = { '+', '-', '*', '(', '/', ')', ',', '\0', ' ', '\b', '<', '>', '.', '=', '$' };
        private bool simbolo(char p)
        {
            int num1 = (int)p;
            if (ModifierKeys.ToString() == "Shift")
            {
                if (num1 == 187) p = (char)(num1 - 145);
                else if (num1 >= 48 && num1 <= 57) p = (char)((int)p - 16);
            }
            else if((int)p >= 144)
            {
                p = (char)((int)p - 144);
            }
            return sim.Contains(p);
        }



        public string cadenaOriginal { get; set; }

        bool contineOperacionesFunciones(string cad)
        {
            bool elemEnviar = false;

            foreach (string ele in listaRemove)
                if (cad.Contains(ele) && ele != "OR")
                    return true;
            //if (cad.Contains('+') || cad.Contains('-') || cad.Contains('*') || cad.Contains('*'))
            //    return true;

            return elemEnviar;
        }

        private void cmdDatosEventos_Click(object sender, EventArgs e)
        {
            double resultadoMayor = 0;
            try
            {
                llenaLista();

                if (cbFiltro.Checked)
                {

                    DataTable tabla = new DataTable();

                    var va = rtbFormula.Text.Replace("'", "").Split();

                    
                    
                    foreach (string col in ListaNombre)
                        tabla.Columns.Add(col, typeof(System.String));

                    foreach (DataColumn col in dtPrincipal.Columns)
                    {
                        if (!tabla.Columns.Contains(col.ColumnName))
                            tabla.Columns.Add(col.ColumnName);
                    }
                    double algo = 0;
                        int u =0;
                        foreach (DataRow row in dtPrincipal.Rows)
                        {
                            tabla.Rows.Add();
                            
                            foreach (string col in ListaNombre)
                            {
                                double.TryParse(dtPrincipal.Rows[u][col].ToString(), out algo);
                                tabla.Rows[tabla.Rows.Count - 1][col] = algo;
                            }u++;
                        }

                        int jp = 0;
                        foreach (DataRow row in dtPrincipal.Rows)
                        {


                            foreach (DataColumn col in dtPrincipal.Columns)
                            {
                                if (!ListaNombre.Contains(col.ColumnName))
                                {
                                    tabla.Rows[jp][col.ColumnName] = dtPrincipal.Rows[jp][col.ColumnName].ToString();
                                }
                            }
                            jp++;
                        }

                        DataView vistaPrincipal = new DataView(tabla);
                        string expression = rtbFormula.Text.Replace("'", "");
                        try
                        {


                            string cadExterna = expression.Replace("$", "'");
                            foreach (string li in ListaNombre)
                            {
                                //expression = cadExterna.Replace(li.ToUpper(), li.Contains("(") || li.Contains(")") || li.Contains(".") || li.Contains("/") ? "Convert.To" + cvT.Split('.')[1] + "(" + li + ")" : "CONVERT(" + li + "," + cvT + ")");
                                
                                //expression = cadExterna.Replace(li.ToUpper(), (li.Contains("  ") || li.Contains("%") || li.Contains("(") || li.Contains("-") || li.Contains(".") || li.Contains("/")) ? "[" + li + "]" : li);
                                cadExterna = expression;
                            }

                            expression = string.Format(expression.Replace(" AND", " and").Replace(" OR", " or"));
                            vistaPrincipal.RowFilter = expression;
                            tabla = vistaPrincipal.ToTable();

                            for (int col = 0; col < tabla.Columns.Count; col++)
                                tabla.Columns[col].SetOrdinal(ordinal(tabla.Columns[col].ColumnName));

                            Form fn = new Form();
                            DataGridView view = new DataGridView();
                            view.Dock = DockStyle.Fill;
                            view.DataSource = tabla;
                            fn.Controls.Add(view);
                            fn.ShowDialog(this);
                        }
                        catch (Exception ex)
                        {
                            var conver = ex.Message.Split();
                            string cvT = conver[conver.Length - 1].Substring(0, conver[conver.Length - 1].Length - 1);
                            //myDataColumn.Expression="Convert(total,'" ctv+"')";
                            string cadExterna = expression = rtbFormula.Text.Replace("'", "");
                            foreach (string li in ListaNombre)
                            {
                                expression = cadExterna.Replace(li.ToUpper(), li.Contains("(") || li.Contains(")") || li.Contains(".") || li.Contains("/") ? "Convert.To" + cvT.Split('.')[1] + "(" + li + ")" : "CONVERT(" + li + "," + cvT + ")");
                                cadExterna = expression;
                            }
                            vistaPrincipal.RowFilter = expression;
                            dtPrincipal = vistaPrincipal.ToTable();
                        }

                }
                else
                {

                    if (textBox1.Text != "" && rtbFormula.Text != "")
                    {

                        Notificacion.Visible = true;
                        Notificacion.ShowBalloonTip(2900000);
                        List<double> cadLista = new List<double>();
                        string valor = rtbFormula.Text;
                        if (dtPrincipal.Columns.Contains(textBox1.Text))
                        {
                            DialogResult dialogResult = MessageBox.Show("Deseas reemplazar", "Atención", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes) dtPrincipal.Columns.Remove(textBox1.Text);
                        }
                        if (!dtPrincipal.Columns.Contains(textBox1.Text))
                        {
                            dtPrincipal.Columns.Add(textBox1.Text);
                            int j = 0; Notificacion.ShowBalloonTip(2900000);

                            foreach (DataRow row in dtPrincipal.Rows)
                            {
                                string textoEvalua = valor.Replace("'", "");
                                textoEvalua = textoEvalua.Replace("POW", "pow");
                                textoEvalua = textoEvalua.Replace("EXP", "exp");
                                textoEvalua = textoEvalua.Replace("SIN", "sin");
                                textoEvalua = textoEvalua.Replace("COS", "cos");
                                textoEvalua = textoEvalua.Replace("TAN", "tan");
                                textoEvalua = textoEvalua.Replace("LOG", "log");
                                textoEvalua = textoEvalua.Replace("SQRT", "sqrt");
                                int i = 0;
                                foreach (string col in ListaNombre)
                                {
                                    double valorPoner = 0;
                                    if (!double.TryParse(row[col.Replace("'", "")].ToString(), out valorPoner))
                                        valorPoner = 1;
                                    textoEvalua = textoEvalua.Replace(col.Replace("'", "").ToUpper(), valorPoner < 0 ? "(" + valorPoner.ToString() + ")" : valorPoner.ToString());
                                }
                                textoEvalua = evE.Convertir_Infija_A_Posfija(textoEvalua);

                                

                                if (evE.EvaluarPostfija(textoEvalua, out resultadoMayor))
                                    dtPrincipal.Rows[j++][textBox1.Text] = resultadoMayor;
                                else dtPrincipal.Rows[j++][textBox1.Text] = "Error!";
                            }

                            Notificacion.ShowBalloonTip(2900000);
                            Notificacion.Visible = false;
                            Notificacion.BalloonTipText = "Proceso Terminado";
                            Notificacion.Visible = true;
                            Notificacion.ShowBalloonTip(50);
                        }
                    }
                    else MessageBox.Show("Verifica el nombre de la columna");

                }
            }
            catch (Exception ex)
            {
                //dtPrincipal.Columns.Remove(textBox1.Text);
                MessageBox.Show("Verifica La Formula " + ex.Message);
            }
        }

        int ordinal(string nom)
        {
            foreach(DataColumn col in dtPrincipal.Columns)
            {
                if (col.ColumnName == nom) return col.Ordinal;
            }
            return 0;
        }

        public static string Eval(string sCSCode)
        {
             CodeDomProvider c = CodeDomProvider.CreateProvider("CSharp");
             CompilerParameters cp = new CompilerParameters();
 
             cp.ReferencedAssemblies.Add("system.dll");
             cp.ReferencedAssemblies.Add("system.xml.dll");
             cp.ReferencedAssemblies.Add("system.data.dll");
             cp.ReferencedAssemblies.Add("system.windows.forms.dll");
             cp.ReferencedAssemblies.Add("system.drawing.dll");
 
             cp.CompilerOptions = "/t:library";
             cp.GenerateInMemory = true;
 
             StringBuilder sb = new StringBuilder(""); //Texto del codigo
             sb.Append("using System;\n");
             sb.Append("using System.Xml;\n");
             sb.Append("using System.Data;\n");
             sb.Append("using System.Data.SqlClient;\n");
             sb.Append("using System.Windows.Forms;\n");
             sb.Append("using System.Drawing;\n");
 
             sb.Append("namespace CSCodeEvaler{ \n");
             sb.Append("public class CSCodeEvaler{ \n");
             sb.Append("public string Yuhuu(){\n");
             sb.Append("return " + sCSCode +  " \n");
             sb.Append("} \n");
             sb.Append("} \n");
             sb.Append("}\n");
 
             CompilerResults cr = c.CompileAssemblyFromSource(cp, sb.ToString()); //icc.CompileAssemblyFromSource(cp, sb.ToString());
             if (cr.Errors.Count > 0)
             {
             MessageBox.Show("ERROR: " + cr.Errors[0].ErrorText,
             "Error evaluating cs code", MessageBoxButtons.OK,
             MessageBoxIcon.Error);
             return "Error";
             }
 
             System.Reflection.Assembly a = cr.CompiledAssembly;
             object o = a.CreateInstance("CSCodeEvaler.CSCodeEvaler");
 
             Type t = o.GetType();
             MethodInfo mi = t.GetMethod("Yuhuu"); //Nombre de la funcion a ejecutar
 
             object s = mi.Invoke(o, null);
             return s.ToString(); 
        }

        string verificaAsterisco(string cad)
        {
            for (int i = 0; i < cad.Length - 1; i++)
            {
                if (cad[i] =='*' && i < cad.Length - 1 && cad[i + 1] == '(')
                   cad = cad.Remove(i, 1);
            }
            return cad;
        }

        string nombreReal(string cad)
        {
            foreach (DataColumn col in dtPrincipal.Columns)
            {
                if (cad == col.Caption.ToUpper())
                    return col.Caption;
            }
            return "";
        }

        private void lbAuto_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is ListBox)
            {
                
                ListBox listBox = (ListBox)sender;
                Point point = new Point(e.X, e.Y);
                int hoverIndex = listBox.IndexFromPoint(point);
                if (hoverIndex >= 0 && hoverIndex < listBox.Items.Count)
                {
                    string xValor = funcionesTexto(listBox.Items[hoverIndex].ToString());
                    tip.SetToolTip(listBox, xValor == "" ? listBox.Items[hoverIndex].ToString() : xValor);
                }
            }    
        }

        string funcionesTexto(string cad)
        {
            switch (cad)
            {
                case "SIN": return "Ejemplo SIN(X)";
                case "COS": return "Ejemplo COS(X)";
                case "TAN": return "Ejemplo TAN(X)";
                case "SQR": return "Ejemplo SQR(X)";
                case "LOG": return "Ejemplo LOG(X)";
                case "POW": return "Ejemplo POW(X, n) n = Es la pontencia";
                default: return "";
            }
        }

        private void rtbFormula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!simbolo(e.KeyChar) && e.KeyChar != ' ')
            {
                if (char.IsDigit(e.KeyChar) && (cadenaOriginal != "" && cadenaOriginal != null)) palabraFormanado += e.KeyChar.ToString();
                else if (e.KeyChar != ' ' && !char.IsDigit(e.KeyChar))
                    palabraFormanado += e.KeyChar.ToString();
                if (palabraFormanado != "")
                {
                    listShow = true;
                    unicaVez = true;
                    Point point = this.rtbFormula.GetPositionFromCharIndex(rtbFormula.SelectionStart);
                    point.X += rtbFormula.Location.X;
                    point.Y = rtbFormula.Location.Y + 23;
                    lbAuto.Location = point;
                    count++;
                    string[] x = (from cad in listaComplete
                                  where cad.ToUpper().Contains(palabraFormanado.ToUpper())
                                  select cad).ToArray();
                    lbAuto.Items.Clear();
                    lbAuto.Items.AddRange(x);
                    if (x.Count() > 0)
                    {
                        lbAuto.Show();
                        lbAuto.Sorted = true;
                        lbAuto.SelectedIndex = lbAuto.FindString(palabraFormanado);
                    }
                    else lbAuto.Hide();

                    rtbFormula.Focus();
                }
                else lbAuto.Visible = false;

            }
            else
            {
                for (int i = 0; i < listaComplete.Count && palabraFormanado != ""; i++)
                {
                    if (palabraFormanado.ToUpper() == listaComplete[i].ToUpper())
                    {
                        string autoText = "";
                        if (listaRemove.Contains(lbAuto.SelectedItem)) autoText = lbAuto.SelectedItem.ToString();
                        else autoText = "'" + lbAuto.SelectedItem.ToString() + "'";

                        rtbFormula.Text = rtbFormula.Text.Replace(palabraFormanado.ToLower(), autoText);
                        rtbFormula.SelectionStart = rtbFormula.Text.Length;
                        lbAuto.Visible = false;
                        ListaNombre.Add(listaComplete[i]);
                        palabraFormanado = "";
                        lbAuto.Visible = false;
                        break;
                    }
                }
                if (palabraFormanado == "")
                    lbAuto.Visible = false;

                //palabraFormanado = "";
            }
        }

        void llenaLista()
        {
            ListaNombre.Clear();
            string cadEvaluar = rtbFormula.Text;
            string valor="";
            for (int i = 0; i < cadEvaluar.Length - 1; i++)
            {
                var ty = cadEvaluar[i];
                if (ty == '\'')
                {
                    i++;
                    valor = "";
                    for (int j = i; j < cadEvaluar.Length; i++,j++)
                    {
                        
                        ty = cadEvaluar[j];
                        if (ty == '\'')
                        {
                            ListaNombre.Add(nombreReal(valor));
                            break;
                        }
                        else valor += ty;
                    }
                }
            }
        }

        private void rtbFormula_TextChanged(object sender, EventArgs e)
        {
            if (rtbFormula.Text == "")
            {
                ListaNombre.Clear();
                palabraFormanado = "";
            }
        }
    }
}
