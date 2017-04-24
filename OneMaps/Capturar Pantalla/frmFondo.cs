using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace CapturaPantallaII
{
    public partial class frmFondo : Form
    {
        #region [ Variables ]
        /// <summary>
        /// Esquina superior izquierda del rectangulo
        /// </summary>
        Point origen;
        /// <summary>
        /// Esquina inferior derecha del rectangulo
        /// </summary>
        Point destino;
        /// <summary>
        /// Punto actual donde damos clic
        /// </summary>
        Point actual;
        /// <summary>
        /// Dibuja el área que estamos seleccionando para copiar
        /// </summary>
        Graphics areaSeleccionada;
        /// <summary>
        /// Control_ necesario para dibujar el rectangulo en la pantalla
        /// </summary>
        Pen lapizActual = new Pen(Color.YellowGreen,3);

        Pen lapizAnterior = new Pen(Color.WhiteSmoke, 3);
        /// <summary>
        /// Indica si fue presionado el botón Izquierda del ratón
        /// </summary>
        bool BotonIzq;
        #endregion

        #region [ Constructor ]
        public frmFondo()
        {
            InitializeComponent();
            
            //Establecemos el ancho de la forma a la resolución actual de pantalla 
            this.Bounds = Screen.GetBounds(this.ClientRectangle);
            //Inicializamos las variables
            origen = new Point(0, 0);
            destino = new Point(0, 0);
            actual = new Point(0, 0);
            //De momento nuestro gráfico es nulo
            areaSeleccionada = this.CreateGraphics();
            //El estilo de línea del lápiz serán puntos
            lapizActual.DashStyle = DashStyle.Solid;
            //Establecemos falsa la variable que nos indica si el boton presionado fue el boton izquierdo
            BotonIzq = false;

            this.MouseDown += new MouseEventHandler(mouse_Click);
            this.MouseUp += new MouseEventHandler(mouse_Up);
            this.MouseMove += new MouseEventHandler(mouse_Move);
         //   label1.Location = new Point(this.Width / 2, this.Height / 2);
          
           }
        #endregion       

        #region [ Eventos ]
        /// <summary>
        /// Captura el punto en el cual se hace la selección tanto del origen como del destino.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouse_Click(object sender, MouseEventArgs e)
        {
            //Si fue presionado el botón izquierdo
            if (e.Button == MouseButtons.Left)
            {
                //Indicamos que el botón izquiero fue presionado
                BotonIzq = true;
                //Guardamos la posición actual
                actual = e.Location;
                //
                areaSeleccionada.Clear(Color.WhiteSmoke);
            }
        }
        #endregion

        /// <summary>
        /// Mediante este evento podemos realizar la selección del área a copiar.
        /// 
        /// Es necesario borrar primero el rectangulo anterior y posteriormente se dibuja el rectangulo nuevo; 
        /// en caso de que no realicemos el borrado anterior se va quedando el rectangulo previamente dibujado
        /// y da un efecto de sombra y para el efecto que necesitamos no es funcional de esta manera.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouse_Move(object sender, MouseEventArgs e)
        {
            if (BotonIzq)
            {
                //Borrar anterior
                areaSeleccionada.DrawRectangle(lapizAnterior, origen.X, origen.Y, destino.X - origen.X, destino.Y - origen.Y);
                SolidBrush solid1 = new SolidBrush(Color.WhiteSmoke);
                areaSeleccionada.FillRectangle(solid1, origen.X, origen.Y, destino.X - origen.X, destino.Y - origen.Y);
             
                //Dibujar el area seleccionada actual
                if (Cursor.Position.X < actual.X)
                {
                    origen.X = Cursor.Position.X;
                    destino.X = actual.X;
                }
                else
                {
                    origen.X = actual.X;
                    destino.X = Cursor.Position.X;
                }

                if (Cursor.Position.Y < actual.Y)
                {
                    origen.Y = Cursor.Position.Y;
                    destino.Y = actual.Y;
                }
                else
                {
                    origen.Y = actual.Y;
                    destino.Y = Cursor.Position.Y;
                }

                areaSeleccionada.DrawRectangle(lapizActual, origen.X, origen.Y, destino.X - origen.X, destino.Y - origen.Y);
                SolidBrush solid = new SolidBrush(Color.CadetBlue);
                areaSeleccionada.FillRectangle(solid, origen.X, origen.Y, destino.X - origen.X, destino.Y - origen.Y);
             
            }
        }

        /// <summary>
        /// Detecta el momento en el que el botón izquierdo es liberado; es decir, la selección del área a copiar es finalizada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouse_Up(object sender, MouseEventArgs e)
        {
            //Verificamos que sea el boton izquierdo el que invoca este método
            if (e.Button == MouseButtons.Left)
            {
                //Indicamos que el boton izquierdo fue liberado
                BotonIzq = false;
                //Ocultamos la forma actual
                this.Visible = true;
                //Guardar la imagen capturada
                this.ScreenShot();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        #region [ Métodos ]
        private void ScreenShot()
        {
            try
            {
                SolidBrush solid1 = new SolidBrush(Color.White);
                areaSeleccionada.FillRectangle(solid1, origen.X, origen.Y, destino.X - origen.X, destino.Y - origen.Y);
               
                //Obtenemos la resolución de pantalla
                Rectangle seleccion = new Rectangle(origen.X, origen.Y, destino.X - origen.X, destino.Y - origen.Y);
                //Creamos un Bitmap del tamaño de nuestra pantalla
                using (Bitmap b = new Bitmap(seleccion.Width, seleccion.Height))
                {
                    //Creamos el gráifco en base al Bitmap 
                    using (Graphics g = Graphics.FromImage(b))
                    {
                        //Transferimos la captura al objeto g en base a las mediad del bitmap
                        g.CopyFromScreen(origen, Point.Empty, seleccion.Size);
                        //Almacenamos la captura
                       // b.Save(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CapturaPantallaII.png", ImageFormat.Png);


                        Clipboard.SetImage(b);

                    }
                }
            }
            catch (InvalidEnumArgumentException ieae)
            {
                MessageBox.Show(ieae.ToString());
            }
            catch (Win32Exception we)
            {
                MessageBox.Show(we.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Show();
            }
        }
        #endregion

        private void frmFondo_Load(object sender, EventArgs e)
        {
         
        }

   


    }
}
