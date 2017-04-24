using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Maps
{
    public partial class UserNompozos : UserControl
    {
        public UserNompozos()
        {
            InitializeComponent();
        }
        Point CurPos;
        Point punto;
        private void splitContainer1_Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            //var gdvResize = (((sender as SplitterPanel).Controls[0].Parent.Parent as SplitContainer).Panel1.Controls[1] as DataGridView);
            CurPos = e.Location;

            punto = new Point((sender as SplitterPanel).ClientSize.Width - 5, (sender as SplitterPanel).ClientSize.Height - 5);
            if (CurPos.Y > punto.Y - 10 && CurPos.Y < punto.Y + 10)
            {
                this.Cursor = Cursors.SizeWE;
            }
            else this.Cursor = Cursors.Default;
        }

        private void splitContainer1_Panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CurPos.Y > punto.Y - 10 && CurPos.Y < punto.Y + 10)
                {
                    this.SuspendLayout();
                    (this.Parent as Panel).Width = e.Location.X + 3;
                    (this.Parent as Panel).Refresh();
                }
            }
            else this.Cursor = Cursors.Default;
                this.ResumeLayout();
        }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void splitContainer2_Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Cross;
            ReleaseCapture();
            SendMessage((this.Parent as Panel).Handle, 0x112, 0xf012, 0);
            //salir.Focus();
        }

        private void splitContainer2_Panel1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
    }
}
