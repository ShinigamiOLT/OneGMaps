using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maps
{
    public class cCargaDatos
    {
        DevComponents.DotNetBar.Controls.DataGridViewX dgvVecinos;
        DevComponents.DotNetBar.Controls.DataGridViewX dgvNuevoPunto;
        DevComponents.DotNetBar.Controls.DataGridViewX dgvPuntoSeleccionado;
        DevComponents.DotNetBar.Controls.DataGridViewX TablaBurbujas;
        DevComponents.DotNetBar.Controls.DataGridViewX anta;
        DevComponents.DotNetBar.Controls.DataGridViewX Area;
        DataTable dtAreasPuntos;
        public cCargaDatos(DevComponents.DotNetBar.Controls.DataGridViewX vecinos, DevComponents.DotNetBar.Controls.DataGridViewX nuevopunto, DevComponents.DotNetBar.Controls.DataGridViewX puntoSelecionado, DataTable dtArea, DevComponents.DotNetBar.Controls.DataGridViewX Burbujas, DevComponents.DotNetBar.Controls.DataGridViewX Anotacion, DevComponents.DotNetBar.Controls.DataGridViewX Areas)
        {
            dgvVecinos = vecinos;
            dgvNuevoPunto = nuevopunto;
            dgvPuntoSeleccionado = puntoSelecionado;
            dtAreasPuntos = dtArea;
            TablaBurbujas = Burbujas;
            anta = Anotacion;
            Area = Areas;


            DataGridViewCheckBoxColumn colArea = new DataGridViewCheckBoxColumn(); colArea.HeaderText = "Visible"; colArea.Name = "Visible";
            DataGridViewButtonColumn colBontonArea = new DataGridViewButtonColumn(); colBontonArea.HeaderText = "Eliminar"; colBontonArea.Name = "Eliminar";
           // Area.RowHeadersVisible = false;
            Area.AllowUserToAddRows = false;
            Area.Columns.Add(colArea);
            Area.Columns.Add("Nombre", "Nombre");
            Area.Columns.Add("M2","M2");
            Area.Columns.Add("KM2", "KM2");
            Area.Columns.Add("ACRES", "ACRES");
            Area.Columns.Add("HA", "HA");
            Area.Columns.Add("Perimetro", "Perimetro");
            Area.Columns.Add("Color", "Color");
            Area.Columns.Add("Datos", "Datos");
            Area.Columns.Add(colBontonArea);


            DataGridViewCheckBoxColumn colAnotacion = new DataGridViewCheckBoxColumn(); colAnotacion.HeaderText = "Visible"; colAnotacion.Name = "Visible";
            DataGridViewButtonColumn colBontonAnota = new DataGridViewButtonColumn(); colBontonAnota.HeaderText = "Eliminar"; colBontonAnota.Name = "Eliminar";

            anta.Columns.Add(colAnotacion);
            anta.Columns.Add("Nombre", "Nombre");
            anta.Columns.Add("Color", "Color");
            anta.Columns.Add("LAT", "Latitud");
            anta.Columns.Add("LON", "Longitud");
            anta.Columns.Add("ZONA", "Zona UTM");
            anta.Columns.Add(colBontonAnota);



            DataGridViewCheckBoxColumn col1 = new DataGridViewCheckBoxColumn(); col1.HeaderText = "Visible"; col1.Name = "Visible";
            DataGridViewButtonColumn colBontonSeleccionado = new DataGridViewButtonColumn(); colBontonSeleccionado.HeaderText = "Eliminar"; colBontonSeleccionado.Name = "Eliminar";
            dgvPuntoSeleccionado.Columns.Add(col1); 
            dgvPuntoSeleccionado.Columns.Add("ID", "ID");
            dgvPuntoSeleccionado.Columns.Add("Nombre", "Nombre");
            dgvPuntoSeleccionado.Columns.Add("Color", "Color");
            dgvPuntoSeleccionado.Columns.Add("LAT", "LAT");
            dgvPuntoSeleccionado.Columns.Add("LON", "LON");
            dgvPuntoSeleccionado.Columns.Add("Radio (KM)", "Radio (KM)");
            dgvPuntoSeleccionado.Columns.Add(colBontonSeleccionado);

            DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn(); col.HeaderText = "Visible"; col.Name = "Visible";
            dgvNuevoPunto.Columns.Add(col);
            dgvNuevoPunto.Columns.Add("Nombre", "Nombre");
            dgvNuevoPunto.Columns.Add("Color", "Color");
            dgvNuevoPunto.Columns.Add("LAT", "LAT");
            dgvNuevoPunto.Columns.Add("LON", "LON");
            dgvNuevoPunto.Columns.Add("Radio (KM)", "Radio (KM)");

            DataGridViewCheckBoxColumn col2 = new DataGridViewCheckBoxColumn(); col2.HeaderText = "Visible"; col2.Name = "Visible";
            dgvVecinos.Columns.Add(col2);
            dgvVecinos.Columns.Add("Nombre", "Nombre");
            dgvVecinos.Columns.Add("Pozos Vecinos", "Vecinos");
            DataGridViewButtonColumn colBonton = new DataGridViewButtonColumn(); colBonton.HeaderText = "Eliminar"; colBonton.Name = "Eliminar";
            dgvVecinos.Columns.Add(colBonton);

            dtAreasPuntos.Columns.Add("ID", typeof(string));
            dtAreasPuntos.Columns.Add("Pozo", typeof(string));
            dtAreasPuntos.Columns.Add("Latitud", typeof(double));
            dtAreasPuntos.Columns.Add("Longitud", typeof(double));

            


            // 
            // Visible
            // 
            Visible = new DataGridViewCheckBoxColumn();
            this.Visible.HeaderText = "Visible";
            this.Visible.Name = "Visible";
            // 
            // Tabla
            // 
            Tabla = new DataGridViewTextBoxColumn();
            this.Tabla.HeaderText = "Tabla";
            this.Tabla.Name = "Tabla";
            this.Tabla.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Tabla.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Titulo
            // 
            Titulo = new DataGridViewTextBoxColumn();
            this.Titulo.HeaderText = "Titulo";
            this.Titulo.Name = "Titulo";
            // 
            // LAT
            // 
            LAT = new DataGridViewTextBoxColumn();
            this.LAT.HeaderText = "Latitud";
            this.LAT.Name = "LAT";
            // 
            // LON
            // 
            LON = new DataGridViewTextBoxColumn();
            this.LON.HeaderText = "Longitud";
            this.LON.Name = "LON";
            // 
            // Variable
            // 
            Variable = new DataGridViewTextBoxColumn();
            this.Variable.HeaderText = "Variable";
            this.Variable.Name = "Variable";
            // 
            // Color
            // 
            Color = new DataGridViewTextBoxColumn();
            this.Color.HeaderText = "Color";
            this.Color.Name = "Color";


            Grosor = new DataGridViewTextBoxColumn();
            this.Grosor.HeaderText = "Grosor";
            this.Grosor.Name = "Grosor";
            // 
            // Eliminar
            // 
            Eliminar = new DataGridViewButtonColumn();
            this.Eliminar.HeaderText = "Eliminar";
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.Resizable = System.Windows.Forms.DataGridViewTriState.True;

            VisibleRow = new DataGridViewCheckBoxColumn();
            this.VisibleRow.HeaderText = "Marcas Visibles";
            this.VisibleRow.Name = "VisibleRow";
            this.VisibleRow.Resizable = System.Windows.Forms.DataGridViewTriState.True;

            ID = new DataGridViewTextBoxColumn();
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;

            this.TablaBurbujas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Visible,
            VisibleRow,
            ID,
            this.Tabla,
            this.Titulo,
            this.LAT,
            this.LON,
            this.Variable,
            this.Color,
            Grosor,
            this.Eliminar});
        }
        private System.Windows.Forms.DataGridViewCheckBoxColumn VisibleRow;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Visible;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tabla;
        private System.Windows.Forms.DataGridViewTextBoxColumn Titulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn LON;
        private System.Windows.Forms.DataGridViewTextBoxColumn Variable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Color;
        private System.Windows.Forms.DataGridViewTextBoxColumn Grosor;
        private System.Windows.Forms.DataGridViewButtonColumn Eliminar;
    }
}
