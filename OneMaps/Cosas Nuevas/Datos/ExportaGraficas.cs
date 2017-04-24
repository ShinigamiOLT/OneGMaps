using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevComponents.DotNetBar;
using System.Diagnostics;

namespace Maps
{
    public partial class ExportaGraficas : Form
    {
      //  private List<Control> Controles;
 
        Constantes_Conf config = new Constantes_Conf();

        public ExportaGraficas()
        {
            InitializeComponent();
        }

        int j = 0;
      //  private List<Control> Controles;
       
        //private List<Control> Controles;
        //private One_Produccion.User_Produccion Padre;

        //public ExportaGraficas(ref List<Control> Controles, One_Produccion.User_Produccion user_Produccion)
        //{

        //    // TODO: Complete member initialization 
        //    InitializeComponent();
        //    this.Controles = Controles;
        //    Padre = user_Produccion;

        //    //  this.Controles = Controles;

        //    foreach (Control col in Controles)
        //    {

        //        if (col is Gigasoft.ProEssentials.Pesgo)
        //        {
        //            Gigasoft.ProEssentials.Pesgo TipoPesgo = (Gigasoft.ProEssentials.Pesgo)col;
        //            dgvGraficas.Rows.Add();
        //            dgvGraficas.Rows[dgvGraficas.Rows.Count - 1].Cells[0].Value = true;
        //            dgvGraficas.Rows[dgvGraficas.Rows.Count - 1].Cells[1].Value = TipoPesgo.PeString.MainTitle;
        //            dgvGraficas.Rows[dgvGraficas.Rows.Count - 1].Cells[1].Tag = TipoPesgo;
        //        }
        //        if (col is Gigasoft.ProEssentials.Pego)
        //        {
        //            Gigasoft.ProEssentials.Pego TipoPesgo = (Gigasoft.ProEssentials.Pego)col;
        //            dgvGraficas.Rows.Add();
        //            dgvGraficas.Rows[dgvGraficas.Rows.Count - 1].Cells[0].Value = true;
        //            dgvGraficas.Rows[dgvGraficas.Rows.Count - 1].Cells[1].Value = TipoPesgo.PeString.MainTitle;
        //            dgvGraficas.Rows[dgvGraficas.Rows.Count - 1].Cells[1].Tag = TipoPesgo;
        //        }
        //    }
        //}
        public ExportaGraficas(ref List<Control> Controles)
        {

            // TODO: Complete member initialization 
            InitializeComponent();
            this.Controles = Controles;
           

            //  this.Controles = Controles;

            foreach (Control col in Controles)
            {

                if (col is Gigasoft.ProEssentials.Pesgo)
                {
                    Gigasoft.ProEssentials.Pesgo TipoPesgo = (Gigasoft.ProEssentials.Pesgo)col;
                    dgvGraficas.Rows.Add();
                    dgvGraficas.Rows[dgvGraficas.Rows.Count - 1].Cells[0].Value = true;
                    dgvGraficas.Rows[dgvGraficas.Rows.Count - 1].Cells[1].Value = TipoPesgo.PeString.MainTitle;
                    dgvGraficas.Rows[dgvGraficas.Rows.Count - 1].Cells[1].Tag = TipoPesgo;
                }
                if (col is Gigasoft.ProEssentials.Pego)
                {
                    Gigasoft.ProEssentials.Pego TipoPesgo = (Gigasoft.ProEssentials.Pego)col;
                    dgvGraficas.Rows.Add();
                    dgvGraficas.Rows[dgvGraficas.Rows.Count - 1].Cells[0].Value = true;
                    dgvGraficas.Rows[dgvGraficas.Rows.Count - 1].Cells[1].Value = TipoPesgo.PeString.MainTitle;
                    dgvGraficas.Rows[dgvGraficas.Rows.Count - 1].Cells[1].Tag = TipoPesgo;
                }
            }
        }

      

        private void ExportaGraficas_Load(object sender, EventArgs e)
        {
            string[] Elementos = new string[] { "Ancho (Pixeles)", "Alto (Pixeles)", "DPI" };
            string[] ValDefault = new string[] { "725", "350", "180" };
            for (int i = 0; i < Elementos.Length; i++)
            {
                dgvValores.Rows.Add();
                dgvValores.Rows[i].Cells[0].Value = Elementos[i];
                dgvValores.Rows[i].Cells[1].Value = ValDefault[i];
            }

          
        }

        public string Cambiar(string Texto)
        {
            Texto = Texto.Replace('ó', 'o');
            Texto = Texto.Replace(':', ' ');
            Texto = Texto.Replace('/', '-');
            return Texto;
        }
        private void cmdExportar_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if(Padre!=null)
            //   Padre.Notificacion.ShowBalloonTip(2000, "Exportando...", "Espere mientras que se exportan las graficas :>", ToolTipIcon.Warning);

            //    //pesgo6.PeFunction.Image.ExportImageLargeFont =true;
            //    //  Destino = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\";
            //    Directory.CreateDirectory(config.Produccion + "\\" + "Produccion" + "\\");
            //    string Destino = config.Produccion + "\\" + "Produccion" + "\\";

            //    foreach (DataGridViewRow reglon in dgvGraficas.Rows)
            //    {
            //        try
            //        {
            //            if ((bool)reglon.Cells[0].Value == true)
            //            {
            //                if (reglon.Cells[1].Tag is Gigasoft.ProEssentials.Pesgo)
            //                {
            //                    Gigasoft.ProEssentials.Pesgo pesgo1 = (Gigasoft.ProEssentials.Pesgo)reglon.Cells[1].Tag;
            //                    pesgo1.PeFunction.Image.ExportImageDpi = Convert.ToInt32(dgvValores[1, 2].Value);
            //                    pesgo1.PeFunction.Image.ExportImageLargeFont = false;
            //                    pesgo1.PeFunction.Image.JpegToFile(Convert.ToInt32(dgvValores[1, 0].Value), Convert.ToInt32(dgvValores[1, 1].Value), Destino + Cambiar(pesgo1.PeString.MainTitle + pesgo1.PeString.XAxisLabel) + j.ToString() + ".jpg");
            //                }
            //                if (reglon.Cells[1].Tag is Gigasoft.ProEssentials.Pego)
            //                {
            //                    Gigasoft.ProEssentials.Pego pesgo1 = (Gigasoft.ProEssentials.Pego)reglon.Cells[1].Tag;
            //                    pesgo1.PeFunction.Image.ExportImageDpi = Convert.ToInt32(dgvValores[1, 2].Value);
            //                    pesgo1.PeFunction.Image.ExportImageLargeFont = false;
            //                    pesgo1.PeFunction.Image.JpegToFile(Convert.ToInt32(dgvValores[1, 0].Value), Convert.ToInt32(dgvValores[1, 1].Value), Destino + Cambiar(pesgo1.PeString.MainTitle + pesgo1.PeString.XAxisLabel) + j.ToString() + ".jpg");
            //                }
            //               // pesgo1.PeFunction.Image.BitmapToFile(Convert.ToInt32(dgvValores[1, 0].Value), Convert.ToInt32(dgvValores[1, 1].Value), Destino + Cambiar(pesgo1.PeString.MainTitle + pesgo1.PeString.XAxisLabel) + j.ToString() + ".bmp");

            //              //  pesgo1.PeFunction.Image.EmfToFile(Convert.ToInt32(dgvValores[1, 0].Value), Convert.ToInt32(dgvValores[1, 1].Value), Destino + Cambiar(pesgo1.PeString.MainTitle + pesgo1.PeString.XAxisLabel) + j.ToString() + ".emf",Gigasoft.ProEssentials.Enums.EmfType.Gdi,Gigasoft.ProEssentials.Enums.EmfDC.DisplayDpiAdjusted,true);

            //              //  pesgo1.PeFunction.Image.PngToFile(Convert.ToInt32(dgvValores[1, 0].Value), Convert.ToInt32(dgvValores[1, 1].Value), Destino + Cambiar(pesgo1.PeString.MainTitle + pesgo1.PeString.XAxisLabel) + j.ToString() + ".png");
            //             //   pesgo1.PeFunction.Image.SvgToFile(Convert.ToInt32(dgvValores[1, 0].Value), Convert.ToInt32(dgvValores[1, 1].Value), Destino + Cambiar(pesgo1.PeString.MainTitle + pesgo1.PeString.XAxisLabel) + j.ToString() + ".svg",true);
                            

            //                j++;
            //            }
            //        }
            //        catch (Exception Ex)
            //        {
            //            MessageBoxEx.Show(Ex.Message);
            //        }

            //    }

            //    MessageBoxEx.Show("Graficas Exportadas con Exito... ", "Exito!!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            //    Process.Start(Destino);//' esto abrira la ruta
            //}
            //catch (Exception ex)
            //{
            //    MessageBoxEx.Show(ex.Message);
            //}
        }

        private void cmdCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvGraficas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvGraficas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (Padre != null)
            // if (e.ColumnIndex == 1 && e.RowIndex > -1)
            //{
            //    if (Padre.reloj.Enabled)
            //    {
            //        Padre.Primario_eliminar.BackColor = Color.White;
            //    }

            //    Control control = dgvGraficas[1, e.RowIndex].Tag as Control;
            //   Padre.panel2.ScrollControlIntoView(control);
            //   Padre.Primario_eliminar = control;
            //   Padre.reloj.Enabled = true;
            //}
        }

        private void dgvValores_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==1 && e.RowIndex==0)
            {
                try {

                    int Val = Convert.ToInt32(dgvValores[e.ColumnIndex, e.RowIndex].Value);
                    dgvValores[e.ColumnIndex, e.RowIndex+1].Value = (int)(Val / 1.618);
                }
                catch { }
            }
        }

        public List<Control> Controles { get; set; }
    }
}
