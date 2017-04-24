using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data;

namespace Maps
{
    class MetodosUtiles
    {
        /// <summary>
        /// Convierte a las imagenes en bytes los carga a la memoria y luego los muestra para evitar bloqueos
        /// </summary>
        /// <param name="filename">la direccion del texto</param>
        /// <returns>Regresa un objeto de tipo Image</returns>
        public Image NonLockingOpen(string filename)
        {
            Image result;

            #region Save file to byte array

            long size = (new FileInfo(filename)).Length;
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[size];
            try
            {
                fs.Read(data, 0, (int)size);
            }
            finally
            {
                fs.Close();
                fs.Dispose();
            }

            #endregion

            #region Convert bytes to image

            MemoryStream ms = new MemoryStream();
            ms.Write(data, 0, (int)size);
            result = new Bitmap(ms);
            ms.Close();

            #endregion

            return result;
        }
        public FileInfo Serializa(object Guardar, string Ruta)
        {
            FileInfo Archivo = new FileInfo(Ruta);
            try
            {
                System.IO.FileStream Stream = new System.IO.FileStream(Ruta, System.IO.FileMode.Create);

                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter Formato = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                Formato.Serialize(Stream, Guardar);

                Stream.Close();
                return Archivo;
            }
            catch (SystemException er)
            {

                MessageBox.Show(er.Message);

            }

            return Archivo;
        }
      
        #region metodos para copiar y pegar

        #region  Keydown en el datagridview
        //DataGridView tabla = (DataGridView)sender;

        //    if (e.KeyValue == 86 && e.Control)
        //    {
        //        Pegar_Datgridview(tabla,true);//Actual);
        //    }

        //    if (e.KeyValue == 67 && e.Control)
        //    {
        //        try
        //        {

        //            Clipboard.SetDataObject(tabla.GetClipboardContent(), true);
        //        }
        //        catch { MessageBox.Show("Error al copiar"); }
        //    }



        #endregion


        public void dgvAgregar_Proyecto_KeyDown_1(object sender, KeyEventArgs e)
        {

            DataGridView tabla = (DataGridView)sender;

            if (e.KeyValue == 86 && e.Control)
            {
                Pegar_Datgridview(tabla, true);//Actual);
            }

            if (e.KeyValue == 67 && e.Control)
            {
                try
                {

                    Clipboard.SetDataObject(tabla.GetClipboardContent(), true);
                }
                catch { MessageBox.Show("Error al copiar"); }
            }
        }


        public void Pegar_Datgridview(DataGridView dgv, bool bandera)
        {
            try
            {
                char[] rowSplitter = new char[] { '\n' };
                //'\r'
                char[] columnSplitter = new char[] { '\t' };
                //obtengo el texto desde clipboard
                IDataObject dataInClipboard = Clipboard.GetDataObject();
                String stringInClipboard = Convert.ToString(dataInClipboard.GetData(DataFormats.Text));
                // MessageBox.Show(stringInCl  ipboard + " " + stringInClipboard.Length.ToString() + "Primera letra = " + stringInClipboard[0]);

                //MessageBox.Show("Numero " + ((int)(char)stringInClipboard[0]).ToString() + "  " + "segundo  = " + ((int)(char)stringInClipboard[1]).ToString());

                //int unicode2=((int)(char)stringInClipboard[0]);



                String[] rowsInClipboard = stringInClipboard.Split(rowSplitter, StringSplitOptions.None);

                //'get the row and column of selected cell in grid 
                int r, c;
                try
                {
                    r = dgv.SelectedCells[0].RowIndex;
                    c = dgv.SelectedCells[0].ColumnIndex;
                }
                catch
                {
                    r = c = 0;
                }
                //add rows into grid to fit clipboard lines
                if (dgv.Rows.Count < (r + rowsInClipboard.Length))
                    dgv.Rows.Add(r + rowsInClipboard.Length - dgv.Rows.Count);
                // ' loop through the lines, split them into cells and place the values in the corresponding cell.
                int iRow = 0;
                while (iRow < rowsInClipboard.Length)
                {//split row into cell values
                    String[] valuesInRow = rowsInClipboard[iRow].Split(columnSplitter);
                    //cycle through cell values
                    int iCol = 0;
                    while (iCol < valuesInRow.Length)
                    {
                        // 'assign cell value, only if it within columns of the grid
                        if (dgv.ColumnCount - 1 >= c + iCol)


                            dgv.Rows[r + iRow].Cells[c + iCol].Value = valuesInRow[iCol];


                        iCol += 1;
                    }
                    iRow += 1;

                }

            }
            catch
            {


            }
        }


        public DataTable ConvertirDatagridview_Datatable(DataGridView Dgv)
        {

            DataTable Tabla = new DataTable();

            if (Dgv != null && Dgv.Columns.Count > 0)
            {
                //en esta parte pongo  las columnas en el datatable para que coincidan
                foreach (DataGridViewColumn dgv_Columna in Dgv.Columns)
                {
                    if (dgv_Columna.ValueType != null)
                        Tabla.Columns.Add(dgv_Columna.HeaderText, dgv_Columna.ValueType);
                    else
                        Tabla.Columns.Add(dgv_Columna.HeaderText, Type.GetType("System.String"));

                }

                int aux;
                //recorro cada renglon
                foreach (DataGridViewRow Renglon in Dgv.Rows)
                {
                    DataRow nue = Tabla.NewRow();

                    //pongo las el valor de las celdas en cada renglon
                    aux = 0;
                    foreach (DataGridViewCell celda in Renglon.Cells)
                    {
                        nue[aux++] = celda.Value;

                    }

                    Tabla.Rows.Add(nue);
                }
            }



            return Tabla;

        }



        #endregion


        //#region para aparecer la ventana flotante de cualquier picture box

        //public void PictureBox_Personalizado_MouseDown(object sender, MouseEventArgs e)
        //{

        //    if (e.Button == System.Windows.Forms.MouseButtons.Right)
        //    {

        //        Poner_descripcion_pictureBox ImagenalPictureBox = new Poner_descripcion_pictureBox(((PictureBox)sender));


        //        ImagenalPictureBox.Show();
        //    }
        //}


        //#endregion



        //public void ClickderechoPersonalizado_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (((PictureBox)sender).Image != null)
        //    {
        //        if (e.Button == System.Windows.Forms.MouseButtons.Right)
        //        {

        //            WorkSpaceManagmentV3.Poner_descripcion_pictureBox tempo = new WorkSpaceManagmentV3.Poner_descripcion_pictureBox((PictureBox)sender);

        //            tempo.ShowDialog();

        //        }

        //    }

       // }
    //
       





        class ImagenPicture
        {
            PictureBox pictureBox1;


            public ImagenPicture()
            { }

            public ImagenPicture(PictureBox px)
            {
                pictureBox1 = px;
            }


            public void ActivatArrastrar()
            {

                pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);

            }
            /// <summary>
            /// te da una clase PictureBox de una locacion o direccion de la maquina 
            /// </summary>
            /// <param name="imagen"></param>
            /// <returns></returns>
            public PictureBox creaPicturaBox(String imagen)
            {


                pictureBox1 = new System.Windows.Forms.PictureBox();
                ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();


                // 
                // pictureBox1
                // 
                pictureBox1.Location = new System.Drawing.Point(179, -10);
                pictureBox1.Name = "pictureBox1";
                pictureBox1.Size = new System.Drawing.Size(254, 195);
                pictureBox1.TabIndex = 0;
                pictureBox1.TabStop = false;

                try
                {
                    pictureBox1.ImageLocation = imagen;
                }
                catch { }
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;


                return pictureBox1;
            }

            ///*http://alegozalves.blogspot.mx/2004/10/artculo-implementacin-simple-de-drag.html */
            public void pictureBox1_MouseDown(object sender, MouseEventArgs e)
            {
                PictureBox temp = ((PictureBox)sender);
                temp.DoDragDrop(((PictureBox)sender), DragDropEffects.Copy);

            }



            public PictureBox creaPicturaBox(Image imagen)
            {
                PictureBox pictureBox1;
                pictureBox1 = new System.Windows.Forms.PictureBox();
                ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();


                // 
                // pictureBox1
                // 
                pictureBox1.Location = new System.Drawing.Point(179, -10);
                pictureBox1.Name = "pictureBox1";
                pictureBox1.Size = new System.Drawing.Size(254, 195);
                pictureBox1.TabIndex = 0;
                pictureBox1.TabStop = false;

                pictureBox1.Image = imagen;
                //pictureBox1.
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                return pictureBox1;

            }


          


        }}
    public class Punto
    {
        public Punto(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

    }



  
    
       
    }
