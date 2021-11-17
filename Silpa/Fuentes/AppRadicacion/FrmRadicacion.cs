using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO; 

namespace AppRadicacion
{
    public partial class FrmRadicacion : Form
    {
        private Logica.AutoridadAmbiental _aa;
        private bool _carga = false;

        private ServicioRadicacion.WSPQ04 _servicio;
        public FrmRadicacion()
        {
            InitializeComponent();
             _servicio  = new AppRadicacion.ServicioRadicacion.WSPQ04();
        }

        private void FrmRadicacion_Load(object sender, EventArgs e)
        {
            
        }

        private void CargarCombo()
        {
            MessageBox.Show(Comun.Utilidades.utl.cadena);  
            _aa = new Logica.AutoridadAmbiental();
            CboAA.DataSource = _aa.DatosAA();
            CboAA.DisplayMember = "AUT_NOMBRE";
            CboAA.ValueMember = "AUT_ID";
        }

        private void BtnCarga_Click(object sender, EventArgs e)
        {
            if (TxtCadena.Text != "")
            {
                Comun.Utilidades.Init(TxtCadena.Text);
                _carga = true;
                CargarCombo();
            }
            else
            {
                _carga = false;
            }
        }
        
        private void BtnSolicitudRadicacion_Click(object sender, EventArgs e)
        {
            //StringBuilder s = new StringBuilder();

            //s.Append("<?xml version=\"1.0\"encoding=\"utf-8\"?>");
            //s.Append("<autoridad id =" + Convert.ToInt32(CboAA.SelectedValue) + " nombre=" + CboAA.SelectedText.ToString() + " permiteRadicar=\"true\"/>");

            //try
            //{
            //    MessageBox.Show(_servicio.EnviarDatosRadicacion(s.ToString()));
            //}
            //finally
            //{
            //    _servicio.Dispose();
            //    _servicio = null; 
            //}
        }

        
    }
}