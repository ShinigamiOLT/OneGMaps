using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maps
{
    public partial class contenedor : Form
    {
       public bool cerrado = false;
        public contenedor()
        {
            InitializeComponent();
        }

        private void contenedor_FormClosed(object sender, FormClosedEventArgs e)
        {
            cerrado = false;
        }
    }
}
