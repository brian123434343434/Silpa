using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AppRadicacion
{
    public partial class FrmRadicacion : Form
    {
        private Logica.AutoridadAmbiental _aa;  
        public FrmRadicacion()
        {
            InitializeComponent();
        }

        private void FrmRadicacion_Load(object sender, EventArgs e)
        {
            CargarCombo();
        }

        private void CargarCombo()
        {
            _aa = new Logica.AutoridadAmbiental();
            CboAA.DataSource = _aa.DatosAA();
            CboAA.DisplayMember = "AUT_NOMBRE";
            CboAA.ValueMember = "AUT_ID";
        }
    }
}