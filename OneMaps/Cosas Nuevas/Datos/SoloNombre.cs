using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Maps
{
    public partial class SoloNombre : Form
    {
        public SoloNombre(string Nombre)
        {
            InitializeComponent();
            txtxName.Text = Nombre;
        }

        private void cmdGuardarfiltro_Click(object sender, EventArgs e)
        {
            if (txtxName.Text.Trim().Length > 0)
            {
                Close();
            }
        }
    }
}
